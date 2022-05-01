using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Terminal
{
    public partial class FrmMain : Form
    {
        public delegate void EnqueuedDelegate();
        public EnqueuedDelegate UIInputQHandlerInstance;
        private readonly EventQueue _uiInputQueue = null;
        public EnqueuedDelegate UIOutputQHandlerInstance;
        private readonly EventQueue _uiOutputQueue = null;

        private delegate void ColoringDelegate();
        private readonly ColoringDelegate ColoringInstance;
        private volatile bool _stopColoring = false;

        public bool ExitFlag = false;
        private Profile _activeProfile = new Profile();

        private int _ticks = 0;
        private int _tock = 0;
        private readonly Comms _comms = null;

        private readonly static object _dollarLock = new object();
        private readonly static object _rtbLock = new object();

        private enum State { Changing, Disconnected, Connected, Listening };
        private State _state = State.Disconnected;

        // Coloring thread
        private Thread _colorThread = null;
        private static readonly AutoResetEvent _colorSemaphore = new AutoResetEvent(false);
        private int _colorEnd = 0;

        // MACRO threads
        private readonly Thread[] _macroThread = new Thread[48];
        private static readonly AutoResetEvent[] _macroTrigger = new AutoResetEvent[48];
        private static readonly bool[] _macroBusy = new bool[48];

        // TCP Server listening thread
        private Thread _connectThread = null;
        private bool _threadResult;
        private bool _threadDone = false;

        // File transmission
        private string _lastFileSend = string.Empty;
        private int _lastFileSendILD = 0;
        private bool _lastSendCR = true;
        private bool _lastSendLF = true;
        private bool _abortFileSend;
        private bool _pauseFileSend;

        private bool _firstActivation = true;

        private bool _macroIsMoving = false;
        private int _macroBeingMoved;

        private volatile bool _tickBusy = false;
        private static bool _terminateFlag = false;
        private static int _localThreadCount = 0;

        private bool _frozen = false;

        // ==============================================
        private int FindApplicableFilter(string str, int offset)
        {
            if (offset >= str.Length)
                return -1;

            for (int i = 0; i < _activeProfile.displayOptions.filter.Length; i++)
            {
                if (_stopColoring)
                    return -1;

                if (_activeProfile.displayOptions.filter[i].text.Length > 0)
                {
                    // starts with
                    if (_activeProfile.displayOptions.filter[i].mode == 0)
                    {
                        if (str.Substring(offset).StartsWith(_activeProfile.displayOptions.filter[i].text))
                            return i;
                    }

                    // contains
                    else if (_activeProfile.displayOptions.filter[i].mode == 1)
                    {
                        if (str.Substring(offset).Contains(_activeProfile.displayOptions.filter[i].text))
                            return i;
                    }
                }
            }
            return -1;
        }

        private void ColoringHandler()
        {
            lock (_rtbLock)
            {
                if (rtb.Lines.Length == 0)
                    return;

                int topIndex = rtb.GetCharIndexFromPosition(new Point(1, 1));
                int bottomIndex = rtb.GetCharIndexFromPosition(new Point(1, rtb.Height - 1));
                int topLine = rtb.GetLineFromCharIndex(topIndex);
                int bottomLine = rtb.GetLineFromCharIndex(bottomIndex);

                for (int i = bottomLine; i >= topLine; i--)
                {
                    // if something changed, quit immediately
                    if (i >= rtb.Lines.Length)
                        i = Math.Max(0, rtb.Lines.Length - 1);

                    string str = rtb.Lines[i];
                    if (str.Length > 0)
                    {
                        int offset = 0;
                        if (Utils.HasTimestamp(str))
                            offset += Utils.TimestampLength();

                        int index = FindApplicableFilter(str, offset);
                        if (index >= 0)
                        {
                            int start = rtb.GetFirstCharIndexFromLine(i) + offset;
                            int len = str.Length - offset;
                            rtb.Select(start, len);
                            rtb.SelectionColor = _activeProfile.displayOptions.filter[index].color;
                            Application.DoEvents();

                            if (_activeProfile.displayOptions.filter[index].freeze)
                            {
                                if (!_frozen)
                                {
                                    cbFreeze.Checked = true;
                                    Application.DoEvents();
                                    int scrollTo = Math.Min(i + 5, rtb.Lines.Length - 1);
                                    rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(scrollTo);
                                    rtb.ScrollToCaret();
                                    MessageBox.Show("The text \"" + _activeProfile.displayOptions.filter[index].text + "\"" +
                                        " has been detected at\n" +
                                        rtb.Lines[i].Substring(0, 40) + "...", 
                                        "Freeze condition detected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    if (i == _colorEnd)
                        break;
                }
                _colorEnd = bottomLine;
            }
        }

        private void ColorThread()
        {
            _localThreadCount++;
            try
            {
                while (!_terminateFlag)
                {
                    _colorSemaphore.WaitOne();
                    if (_terminateFlag)
                        break;

                    _stopColoring = false;
                    this.Invoke(this.ColoringInstance);
                }
            }
            catch { }
            _localThreadCount--;
        }

        // ==============================================

        private void UpdateLineCounter()
        {
            lock (_rtbLock)
            {
                stsSize.Text = $"  {rtb.Text.Length}  ";
                stsLineCount.Text = $"  {rtb.Lines.Length}  ";
            }
        }

        private void ProcessChunk(string chunk)
        {
            Log.Add(chunk);
            if (!_frozen)
            {
                lock (_rtbLock)
                {
                    int lastLine = Math.Max(0, rtb.Lines.Length - 1);
                    rtb.AppendText(chunk, _activeProfile.displayOptions.inputText);
                    if (rtb.Lines.Length < 100)
                        _colorSemaphore.Set();

                    // limit our line count
                    if (rtb.Lines.Length > _activeProfile.displayOptions.maxLines)
                    {
                        // mandatory cut
                        int tocut = rtb.Lines.Length - _activeProfile.displayOptions.maxLines;

                        // plus additional cut
                        tocut += _activeProfile.displayOptions.cutXtraLines;
                        if (tocut >= rtb.Lines.Length)
                        {
                            rtb.Clear();
                            lastLine = 0;
                            _colorEnd = 0;
                        }
                        else
                        {
                            rtb.ReadOnly = false;
                            {
                                rtb.Select(0, rtb.GetFirstCharIndexFromLine(tocut));
                                rtb.SelectedText = string.Empty;
                            }
                            rtb.ReadOnly = true;
                            lastLine -= tocut;
                            _colorEnd = lastLine;
                        }
                    }
                }
            }
        }

        private void UIInputQueueHandler()
        {
            while (!_terminateFlag && _uiInputQueue.Count > 0)
            {
                string str = (string)_uiInputQueue.Dequeue();
                ProcessChunk(str);
            }

            // and put the caret back to the bottom
            if (!_frozen)
            {
                lock (_rtbLock)
                {
                    rtb.SelectionStart = rtb.Text.Length;
                    rtb.ScrollToCaret();
                }
                UpdateLineCounter();
            }
        }
        private void UIOutputQueueHandler()
        {
            while (!_terminateFlag && _uiOutputQueue.Count > 0)
            {
                string str = (string)_uiOutputQueue.Dequeue();
                string tm = Utils.Timestamp();
                if (_activeProfile.displayOptions.timestampOutputLines)
                {
                    lbOutput.Items.Add(tm + str);
                }
                else
                    lbOutput.Items.Add(str);

                Log.Add("\r\n----> Tx: " + tm + str + "\r\n");
                lbOutput.TopIndex = lbOutput.Items.Count - 1;
                Application.DoEvents();
            }
        }

        public FrmMain(string[] args)
        {
            InitializeComponent();

            this.Text = VersionLabel;

            dgMacroTable.ReadOnly = true;
            dgMacroTable.RowCount = 5;
            for (int f = 1; f <= 12; f++)
            {
                dgMacroTable.Rows[0].Cells[f].Value = $"F{f}";
                dgMacroTable.Rows[0].Cells[f].Style.BackColor = Color.Ivory;
            }

            for (int r = 0; r <= 4; r++)
                dgMacroTable.Rows[r].Cells[0].Style.BackColor = Color.Ivory;

            dgMacroTable.Rows[1].Cells[0].Value = "Plain";
            dgMacroTable.Rows[2].Cells[0].Value = "Shift+";
            dgMacroTable.Rows[3].Cells[0].Value = "Ctrl+";
            dgMacroTable.Rows[4].Cells[0].Value = "Alt+";
            dgMacroTable.ClearSelection();
            stsLogfile.Text = string.Empty;

            UIInputQHandlerInstance = new EnqueuedDelegate(UIInputQueueHandler);
            _uiInputQueue = new EventQueue(this, UIInputQHandlerInstance);

            UIOutputQHandlerInstance = new EnqueuedDelegate(UIOutputQueueHandler);
            _uiOutputQueue = new EventQueue(this, UIOutputQHandlerInstance);

            ColoringInstance = new ColoringDelegate(ColoringHandler);

            Database.Initialise();

            if (!Database.Find("Default"))
            {
                Profile s = new Profile
                {
                    name = "Default"
                };

                FrmDisplayOptions conf = new FrmDisplayOptions();
                for (int i = 0; i < conf.PanelList.Length; i++)
                    s.displayOptions.filter[i].color = conf.PanelList[i].BackColor;

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                s.displayOptions.inputFont = (Font)converter.ConvertFromString(Utils.DefaultInputFont);
                s.displayOptions.outputFont = (Font)converter.ConvertFromString(Utils.DefaultOutputFont);

                Database.SaveProfile(s);
            }

            string preferred = Database.GetActiveProfile();
            if (!Database.Find(preferred))
                preferred = "Default";

            if (args.Length == 1)
            {
                if (Database.Find(args[0]))
                    preferred = args[0];
            }

            Label[] leds = new Label[] { LED1, LED2, LED3, LED4 };
            Label[] controls = new Label[] { ctrlOne, ctrlTwo };
            _comms = new Comms(leds, controls, _uiInputQueue);
            SwitchToProfile(preferred);
        }

        private void BtnColorConfig_Click(object sender, EventArgs e)
        {
            btnColorConfig.Enabled = false;
            {
                SaveThisProfile();
                FrmDisplayOptions cfg = new FrmDisplayOptions
                {
                    Options = _activeProfile.displayOptions.Clone()
                };

                TopMost = false;
                cfg.ShowDialog();
                TopMost = cbStayOnTop.Checked;

                if (cfg.Result == DialogResult.OK)
                {
                    _activeProfile.displayOptions = cfg.Options.Clone();
                    SaveThisProfile();

                    lock (_rtbLock)
                    {
                        rtb.BackColor = _activeProfile.displayOptions.inputBackground;
                        lbOutput.BackColor = _activeProfile.displayOptions.outputBackground;
                        rtb.Font = _activeProfile.displayOptions.inputFont;
                        lbOutput.Font = _activeProfile.displayOptions.outputFont;
                    }
                    _colorEnd = 0;
                    _stopColoring = false;
                    this.Invoke(this.ColoringInstance);
                }
            }
            stsMaxSize.Text = $"  Max: {_activeProfile.displayOptions.maxLines} lines  ";
            btnColorConfig.Enabled = true;
            tbCommand.Focus();
        }

        private void ConnectionThread()
        {
            try
            {
                _threadResult = _comms.Connect(_activeProfile);
                _threadDone = true;
            }
            catch { }
        }

        private void ActionConnect()
        {
            _state = State.Changing;

            if (pdPort.Text.Equals("TCP Server"))
                _activeProfile.conOptions.Type = ConOptions.ConType.TCPServer;
            else if (pdPort.Text.Equals("TCP Client"))
                _activeProfile.conOptions.Type = ConOptions.ConType.TCPClient;
            else
            {
                _activeProfile.conOptions.Type = ConOptions.ConType.Serial;
                _activeProfile.conOptions.SerialPort = pdPort.Text;
            }

            if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPServer)
            {
                _ticks = 0;
                _tock = 1;
                btnConnect.Text = "|";
                btnConnect.BackColor = Color.Yellow;
                btnConnectOptions.Enabled = false;
                pdPort.Enabled = false;
                lblProfileName.Enabled = false;
                btnProfileSelect.Enabled = false;

                _threadDone = false;
                _connectThread = new Thread(new ThreadStart(ConnectionThread));
                _connectThread.Start();

                Log.Add(Utils.Timestamp() + "{LISTENING}");
                _state = State.Listening;
            }
            else
            {
                bool rc = _comms.Connect(_activeProfile);
                if (rc)
                {
                    btnConnect.Text = "&Disconnect";
                    btnConnectOptions.Enabled = false;
                    pdPort.Enabled = false;
                    lblProfileName.Enabled = false;
                    btnProfileSelect.Enabled = false;
                    btnFile.Enabled = true;
                    btnConnect.BackColor = Color.Lime;

                    Log.Add(Utils.Timestamp() + "{CONNECTED}");
                    _state = State.Connected;
                }
                else
                {
                    MessageBox.Show("Cannot connect.  The port may be used by another application.  Please verify the configuration options", "Cannot connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.Add(Utils.Timestamp() + "{FAILED}");
                    _state = State.Disconnected;
                }
            }
        }

        private void ActionDisconnect()
        {
            _comms.Disconnect();
            if (_state == State.Listening)
            {
                Thread.Sleep(10);
                if (!_threadDone)
                    _connectThread.Abort();
                _connectThread.Join();
                _connectThread = null;
            }

            btnConnect.BackColor = Color.Transparent;
            RefreshPortPulldown();
            btnConnectOptions.Enabled = true;
            pdPort.Enabled = true;
            lblProfileName.Enabled = true;
            btnProfileSelect.Enabled = true;
            btnFile.Enabled = false;
            Application.DoEvents();

            RestartAllMacros();

            Log.Add(Utils.Timestamp() + "{DISCONNECTED}");
            _state = State.Disconnected;
        }

        private void DoConnectDisconnect()
        {
            if (pdPort.Text.Length == 0)
            {
                MessageBox.Show("Please configure a port", "Port not configured", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnConnect.Enabled = false;
            if (_state == State.Disconnected)
                ActionConnect();
            else
            {
                _abortFileSend = true;
                _pauseFileSend = false;

                ActionDisconnect();
                RefreshPortPulldown();
            }
            btnConnect.Enabled = true;
            tbCommand.Focus();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            DoConnectDisconnect();
        }

        private void ControlOne_Click(object sender, EventArgs e)
        {
            _comms.ControlPressed(0);
        }

        private void ControlTwo_Click(object sender, EventArgs e)
        {
            _comms.ControlPressed(1);
        }

        private void BtnEditMacro_Click(object sender, EventArgs e)
        {
            btnEditMacro.Enabled = false;
            {
                FrmMacroOptions macros = new FrmMacroOptions(_activeProfile, 0, dgMacroTable);
                TopMost = false;
                macros.ShowDialog();
                TopMost = cbStayOnTop.Checked;

                if (macros.Modified)
                    SaveThisProfile();
            }
            btnEditMacro.Enabled = true;
            tbCommand.Focus();
        }

        private void BtnLogfile_Click(object sender, EventArgs e)
        {
            btnLogOptions.Enabled = false;
            {
                SaveThisProfile();
                FrmLogOptions options = new FrmLogOptions(_activeProfile.logOptions);
                TopMost = false;
                options.ShowDialog();
                TopMost = cbStayOnTop.Checked;
                if (_activeProfile.logOptions.Modified)
                    SaveThisProfile();
            }
            btnLogOptions.Enabled = true;
            tbCommand.Focus();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (_state == State.Disconnected)
            {
                DialogResult yn = MessageBox.Show("Go online?", "Is offline", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (yn == DialogResult.Yes)
                    BtnConnect_Click(sender, e);
            }
            if (_state == State.Connected)
            {
                string str = tbCommand.Text;
                if (cbSendCR.Checked)
                    str += "$0D";
                if (cbSendLF.Checked)
                    str += "$0A";
                SendDollarString(str, 0);
                if (cbClearCMD.Checked)
                    tbCommand.Text = string.Empty;
            }
            tbCommand.Focus();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            btnConnectOptions.Enabled = false;
            pdPort.Enabled = false;
            {
                SaveThisProfile();

                if (pdPort.Text.Equals("TCP Server"))
                    _activeProfile.conOptions.Type = ConOptions.ConType.TCPServer;
                else if (pdPort.Text.Equals("TCP Client"))
                    _activeProfile.conOptions.Type = ConOptions.ConType.TCPClient;
                else
                {
                    _activeProfile.conOptions.Type = ConOptions.ConType.Serial;
                    _activeProfile.conOptions.SerialPort = pdPort.Text;
                }

                FrmConnectOptions port = new FrmConnectOptions(_activeProfile.conOptions, _comms.GetSerialPortNames());
                TopMost = false;
                port.ShowDialog();
                TopMost = cbStayOnTop.Checked;
                if (_activeProfile.conOptions.Modified)
                {
                    RefreshPortPulldown();
                    SaveThisProfile();
                }
            }
            RefreshPortPulldown();
            btnConnectOptions.Enabled = true;
            pdPort.Enabled = true;
            tbCommand.Focus();
        }

        private void BtnStartLog_Click(object sender, EventArgs e)
        {
            if (!Log.Enabled)
            {
                string dir = _activeProfile.logOptions.Directory;
                Log.Start(dir + @"\" + _activeProfile.logOptions.Prefix + Utils.TimestampForFilename() + ".log");
            }
            else
                Log.Stop();

            if (Log.Enabled)
            {
                btnLogOptions.Enabled = false;
                btnStartLog.BackColor = Color.Lime;
                btnStartLog.Text = "Stop Log";
                stsLogfile.Text = "   " + Log.Filename + "   ";
            }
            else
            {
                btnLogOptions.Enabled = true;
                btnStartLog.BackColor = Color.Transparent;
                btnStartLog.Text = "Start Log";
            }
            tbCommand.Focus();
        }

        #region EMBELISHMENTS
        private void cbTimestamp_CheckedChanged(object sender, EventArgs e)
        {
            _activeProfile.embelishments.ShowTimestamp = cbTimestamp.Checked;
            _comms.SetEmbelishments(_activeProfile.embelishments);
            tbCommand.Focus();
        }
        private void CbASCII_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbASCII.Checked && !cbHEX.Checked)
                cbHEX.Checked = true;
            _activeProfile.embelishments.ShowASCII = cbASCII.Checked;
            _comms.SetEmbelishments(_activeProfile.embelishments);
            tbCommand.Focus();
        }
        private void CbHEX_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbASCII.Checked && !cbHEX.Checked)
                cbASCII.Checked = true;
            _activeProfile.embelishments.ShowHEX = cbHEX.Checked;
            _comms.SetEmbelishments(_activeProfile.embelishments);
            tbCommand.Focus();
        }
        private void cbShowCR_CheckedChanged(object sender, EventArgs e)
        {
            _activeProfile.embelishments.ShowCR = cbShowCR.Checked;
            _comms.SetEmbelishments(_activeProfile.embelishments);
            tbCommand.Focus();
        }

        private void cbShowLF_CheckedChanged(object sender, EventArgs e)
        {
            _activeProfile.embelishments.ShowLF = cbShowLF.Checked;
            _comms.SetEmbelishments(_activeProfile.embelishments);
            tbCommand.Focus();
        }
        #endregion

        private void CbFreeze_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFreeze.Checked)
            {
                _frozen = true;
                rtb.AppendText("\n====== 'Freeze' activated here ======\n", _activeProfile.displayOptions.inputText);
                cbFreeze.BackColor = Color.Tomato;
                cbFreeze.Text = "    Run   ";
                rtb.SelectionStart = rtb.Text.Length;
                rtb.ScrollToCaret();
            }
            else
            {
                _uiInputQueue.Clear();
                cbFreeze.Text = " Freeze ";
                cbFreeze.BackColor = PanelOne.BackColor;
                this.Invoke(this.UIInputQHandlerInstance);
                _frozen = false;

                _colorSemaphore.Set();
            }
            tbCommand.Focus();
        }

        private void CbStayOnTop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = cbStayOnTop.Checked;
        }

        private void RestartMacro(int m)
        {
            if (_macroBusy[m])
            {
                Macro mac = _activeProfile.macros[m];
                dgMacroTable.Rows[mac.uiRow].Cells[mac.uiColumn].Style.BackColor = Color.White;
                _macroThread[m].Abort();
                _macroThread[m] = new Thread(new ThreadStart(MacroThread))
                {
                    Name = m.ToString()
                };
                _macroThread[m].Start();
            }
        }

        private void RestartAllMacros()
        {
            for (int m = 0; m < _macroThread.Length; m++)
                RestartMacro(m);
        }

        private void MacroThread()
        {
            int m = Utils.Int(Thread.CurrentThread.Name);
            string[] delim = { "{0D}", "{0A}" };

            _localThreadCount++;
            try // Thread.Abort will cause an exception
            {
                while (!_terminateFlag)
                {
                    _macroBusy[m] = false;
                    _macroTrigger[m].WaitOne();
                    if (_terminateFlag)
                        break;

                    // -------------------------------------------
                    // the thread will wait here till activated
                    // -------------------------------------------

                    Macro mac = _activeProfile.macros[m];
                    if (mac == null)
                        continue;

                    mac.runLines = mac.macro.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                    if (mac.runLines.Length == 0)
                        continue;

                    dgMacroTable.Rows[mac.uiRow].Cells[mac.uiColumn].Style.BackColor = Color.Tomato;

                    _macroBusy[m] = true;
                    for (int i = 0; i < mac.runLines.Length; i++)
                    {
                        mac.runLines[i] = mac.runLines[i].TrimStart();
                        int commentStart = mac.runLines[i].IndexOf('#');
                        if (commentStart >= 0)
                                mac.runLines[i] = mac.runLines[i].Substring(0, commentStart);
                        mac.runLines[i] = mac.runLines[i].TrimEnd();
                        if (mac.runLines[i].Length > 0)
                        {
                            if (mac.addCR)
                                mac.runLines[i] += "$0D";
                            if (mac.addLF)
                                mac.runLines[i] += "$0A";
                        }
                    }

                    int repeatCount = 0;
                    while (!_terminateFlag)
                    {
                        foreach (string s in mac.runLines)
                        {
                            if (s.Length > 0)
                            {
                                SendDollarString(s, mac.delayBetweenChars);
                                Thread.Sleep(mac.delayBetweenLines);
                            }
                        }

                        if (!mac.repeatEnabled)
                            break;
                        repeatCount++;
                        if (mac.stopAfterRepeats > 0 && repeatCount > mac.stopAfterRepeats)
                            break;
                        Thread.Sleep(mac.resendEveryMs);
                    }
                    dgMacroTable.Rows[mac.uiRow].Cells[mac.uiColumn].Style.BackColor = Color.White;
                }
            }
            catch { }
            if (!_terminateFlag)
            {
                dgMacroTable.ClearSelection();
                _localThreadCount--;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblSaving.Text = string.Empty;
            MacroPanel.Height = (dgMacroTable.Rows[0].Height * 5) + 2; // +2 for horizontal lines
            dgMacroTable.Dock = DockStyle.Fill;
            dgMacroTable.ClearSelection();
            for (int t = 0; t < _macroThread.Length; t++)
            {
                _macroTrigger[t] = new AutoResetEvent(false);
                _macroThread[t] = new Thread(new ThreadStart(MacroThread))
                {
                    Name = t.ToString()
                };
                _macroThread[t].Start();
            }
            _colorThread = new Thread(new ThreadStart(ColorThread));
            _colorThread.Start();
        }

        private void DoMacro(int m)
        {
            Macro mac = _activeProfile.macros[m];
            if (mac.title.Length != 0)
            {
                if (_state == State.Disconnected)
                {
                    DialogResult yn = MessageBox.Show("Go online?", "Is offline", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (yn == DialogResult.Yes)
                        DoConnectDisconnect();
                }

                // may or may NOT be connected
                if (_state == State.Connected)
                {
                    if (_macroBusy[m])
                        RestartMacro(m);
                    else
                        _macroTrigger[m].Set();
                }
            }
        }
        private void DgMacroTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            _macroIsMoving = false;
            cbFreeze.Checked = false;
            if (e.RowIndex < 1 || e.RowIndex > 4)
            {
                tbCommand.Focus();
                return;
            }
            if (e.ColumnIndex < 1 || e.ColumnIndex > 12)
            {
                tbCommand.Focus();
                return;
            }

            int m = ((e.RowIndex - 1) * 12) + (e.ColumnIndex - 1);
            if (_activeProfile.macros[m] == null)
                _activeProfile.macros[m] = new Macro();
            Macro mac = _activeProfile.macros[m];
            mac.uiColumn = e.ColumnIndex;
            mac.uiRow = e.RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                if (_macroBusy[m])
                    RestartMacro(m);

                FrmMacroOptions macros = new FrmMacroOptions(_activeProfile, m, dgMacroTable);
                TopMost = false;
                macros.ShowDialog();
                if (macros.Modified)
                    SaveThisProfile();
                TopMost = cbStayOnTop.Checked;
            }
            else
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (mac.title.Length > 0)
                    {
                        _macroIsMoving = true;
                        _macroBeingMoved = m;
                    }
                }
                else
                {
                    DoMacro(m);
                }
            }
        }

        private void DgMacroTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!_macroIsMoving)
                return;

            if (e.RowIndex < 1 || e.RowIndex > 4)
            {
                tbCommand.Focus();
                return;
            }
            if (e.ColumnIndex < 1 || e.ColumnIndex > 12)
            {
                tbCommand.Focus();
                return;
            }

            int m = ((e.RowIndex - 1) * 12) + (e.ColumnIndex - 1);
            if (_activeProfile.macros[m] == null)
                _activeProfile.macros[m] = new Macro();
            Macro dst = _activeProfile.macros[m];
            if (dst.title.Length > 0)
            {
                MessageBox.Show("You can only move macros to empty spots on the table", "Target spot is not empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // MOVING MACROS
            {
                Macro src = _activeProfile.macros[_macroBeingMoved];
                _activeProfile.macros[m] = src.Clone();
                dst = _activeProfile.macros[m];
                dst.uiColumn = e.ColumnIndex;
                dst.uiRow = e.RowIndex;

                dgMacroTable.Rows[src.uiRow].Cells[src.uiColumn].Value = string.Empty;
                _activeProfile.macros[_macroBeingMoved] = null;
                dgMacroTable.Rows[dst.uiRow].Cells[dst.uiColumn].Value = dst.title;

                SaveThisProfile();
            }
        }
        private void SaveThisProfile()
        {
            lblSaving.Text = "*";
            Application.DoEvents();
            _activeProfile.stayontop = cbStayOnTop.Checked;
            _activeProfile.sendCR = cbSendCR.Checked;
            _activeProfile.sendLF = cbSendLF.Checked;
            _activeProfile.clearCMD = cbClearCMD.Checked;
            Database.SaveProfile(_activeProfile);
            Database.SaveAsActiveProfile(_activeProfile.name);
            Thread.Sleep(50);
            lblSaving.Text = string.Empty;
        }

        private void SwitchToProfile(string name)
        {
            if (name.Length == 0)
                return;

            _firstActivation = !Database.HasActiveProfile();

            if (Database.Find(name))
            {
                Log.Stop();

                _activeProfile = Database.ReadProfile(name);
                Database.SaveAsActiveProfile(name);

                lock (_rtbLock)
                {
                    rtb.BackColor = _activeProfile.displayOptions.inputBackground;
                    lbOutput.BackColor = _activeProfile.displayOptions.outputBackground;
                    rtb.Font = _activeProfile.displayOptions.inputFont;
                }
                lbOutput.Font = _activeProfile.displayOptions.outputFont;

                lblProfileName.Text = _activeProfile.name;
                cbStayOnTop.Checked = _activeProfile.stayontop;

                cbSendCR.Checked = _activeProfile.sendCR;
                cbSendLF.Checked = _activeProfile.sendLF;
                cbClearCMD.Checked = _activeProfile.clearCMD;

                cbTimestamp.Checked = _activeProfile.embelishments.ShowTimestamp;
                cbASCII.Checked = _activeProfile.embelishments.ShowASCII;
                cbHEX.Checked = _activeProfile.embelishments.ShowHEX;
                cbShowCR.Checked = _activeProfile.embelishments.ShowCR;
                cbShowLF.Checked = _activeProfile.embelishments.ShowLF;

                cbFreeze.Checked = false;

                int i = 0;
                for (int r = 1; r <= 4; r++)
                {
                    for (int f = 1; f <= 12; f++)
                    {
                        Macro m = _activeProfile.macros[i++];
                        if (m == null)
                            dgMacroTable.Rows[r].Cells[f].Value = string.Empty;
                        else
                            dgMacroTable.Rows[r].Cells[f].Value = m.title;
                    }
                }
                RefreshPortPulldown();
                _comms.SetEmbelishments(_activeProfile.embelishments);
                stsMaxSize.Text = $"  Max: {_activeProfile.displayOptions.maxLines} lines  ";
            }
        }
        private void TbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (lbOutput.SelectedIndex > 0)
                    lbOutput.SelectedIndex--;
                else
                    lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (lbOutput.SelectedIndex > 0)
                {
                    if (lbOutput.SelectedIndex < lbOutput.Items.Count - 1)
                        lbOutput.SelectedIndex++;
                }
                else
                    lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
            }
            else if (e.KeyCode == Keys.C)
            {
                if (e.Control)
                {
                    lock (_rtbLock)
                    {
                        rtb.Copy();
                    }
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                if (e.Control)
                {
                    lock (_rtbLock)
                    {
                        rtb.SelectAll();
                    }
                }
            }
            else if (e.KeyCode == Keys.X)
            {
                if (e.Control)
                {
                    lock (_rtbLock)
                    {
                        rtb.Copy();
                        rtb.Clear();
                        _colorEnd = 0;
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                RestartAllMacros();
                tbCommand.Text = string.Empty;
            }
            else
            {
                int key = (int)e.KeyCode;
                if (key >= (int)Keys.F1 && key <= (int)Keys.F12)
                {
                    int index = key - (int)Keys.F1;
                    int column = index + 1;
                    int row = 1;
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        index += 12;
                        row += 1;
                    }
                    else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        index += 24;
                        row += 2;
                    }
                    else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        index += 36;
                        row += 3;
                    }

                    Macro mac = _activeProfile.macros[index];
                    mac.uiColumn = column;
                    mac.uiRow = row;
                    DoMacro(index);
                }
            }
            dgMacroTable.ClearSelection();
            tbCommand.Focus();
        }

        private string VersionLabel
        {
            get
            {
                AssemblyInfo info = new AssemblyInfo();
                return $"{info.Title} - v{info.AssemblyVersion} - {info.Copyright}";
            }
        }

        private bool IsHex(byte ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;
            else if (ch >= 'A' && ch <= 'F')
                return true;
            else if (ch >= 'a' && ch <= 'h')
                return true;
            return true;
        }
        private int GetHex(byte ch)
        {
            if (ch >= '0' && ch <= '9')
                return ch - '0';
            else if (ch >= 'A' && ch <= 'F')
                return (ch - 'A') + 10;
            else if (ch >= 'a' && ch <= 'h')
                return (ch - 'a') + 10;
            return 0;
        }

        private void SendDollarString(string cmd, int icd)
        {
            lock (_dollarLock)
            {
                if (cmd.Length == 0)
                    return;

                byte[] array = Encoding.UTF8.GetBytes(cmd);
                byte[] oneByte = new byte[1];

                _uiOutputQueue.Enqueue(cmd);

                int i = 0;
                int len = array.Length;
                while (i < len)
                {
                    if (i < len - 1 && array[i] == '$' && array[i + 1] == '$')
                    {
                        oneByte[0] = array[i];
                        _comms.Write(oneByte);
                        i++;
                    }
                    else if (i < len - 2 && array[i] == '$' && IsHex(array[i + 1]) && IsHex(array[i + 2]))
                    {
                        int ch = (GetHex(array[i + 1]) << 4) | GetHex(array[i + 2]);
                        oneByte[0] = (byte)ch;
                        _comms.Write(oneByte);
                        i += 2;
                    }
                    else
                    {
                        oneByte[0] = array[i];
                        _comms.Write(oneByte);
                    }
                    i++;

                    if (icd > 0)
                        Thread.Sleep(icd);
                }
            }
        }

        private void TickHandler()
        {
            if (_terminateFlag)
                return;

            dgMacroTable.ClearSelection();
            if (_comms == null)
                return;
            else if (_state == State.Changing || _state == State.Disconnected)
                return;
            else if (_state == State.Connected)
            {
                if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPClient ||
                    _activeProfile.conOptions.Type == ConOptions.ConType.TCPServer)
                {
                    if (!_comms.Connected())
                    {
                        ActionDisconnect();
                    }
                }
            }
            else if (_state == State.Listening)
            {
                if (_threadDone)
                {
                    if (_threadResult)
                    {
                        _state = State.Connected;
                        btnConnect.Text = "Disconnect";
                        btnConnectOptions.Enabled = false;
                        pdPort.Enabled = false;
                        lblProfileName.Enabled = false;
                        btnProfileSelect.Enabled = false;
                        btnFile.Enabled = true;
                        btnConnect.BackColor = Color.Lime;
                        Log.Add(Utils.Timestamp() + "{CONNECTED}");
                    }
                    else
                    {
                        _state = State.Disconnected;
                    }
                    return;
                }

                _ticks++;
                if ((timer.Interval * _ticks) >= 100)
                {
                    string[] chr = { "|", "/", "---", "\\" };
                    btnConnect.Text = chr[_tock++];
                    if (_tock == 4)
                        _tock = 0;
                    _ticks = 0;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_tickBusy)
                return;
            _tickBusy = true;
            TickHandler();
            _tickBusy = false;
        }

        private void ResetFocus(object sender, EventArgs e)
        {
            tbCommand.Focus();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            FrmHelp help = new FrmHelp();
            help.ShowDialog();
            tbCommand.Focus();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            if (_firstActivation)
            {
                MessageBox.Show($"This is a once-only message.\nPress the HELP button for some tips.\nThe key concept is to create a profile for every kind of application you may have.\n\nStart by pressing the [(+)] button (top middle) to create your first profile", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tbCommand.Focus();
            Application.DoEvents();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lock (_rtbLock)
            {
                _uiInputQueue.Clear();
                rtb.Clear();
                rtb.ZoomFactor = 1;
                _colorEnd = 0;
            }
            cbFreeze.Checked = false;
            UpdateLineCounter();
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            btnFile.Enabled = false;
            FrmFileSend fs = new FrmFileSend
            {
                Filename = _lastFileSend,
                InterLineDelay = _lastFileSendILD,
                SendCR = _lastSendCR,
                SendLF = _lastSendLF
            };
            TopMost = false;
            fs.ShowDialog();
            TopMost = cbStayOnTop.Checked;
            if (fs.Result == DialogResult.OK)
            {
                _lastFileSend = fs.Filename;
                _lastFileSendILD = fs.InterLineDelay;
                _lastSendCR = fs.SendCR;
                _lastSendLF = fs.SendLF;

                try
                {
                    string data = File.ReadAllText(fs.Filename);
                    string[] lines = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    btnPauseFile.Text = "Pause";
                    lblLineCounter.Text = "0";
                    progressBar.Value = 0;
                    progressBar.Maximum = lines.Length;
                    progressBar.Visible = true;
                    btnAbortFile.Visible = true;
                    btnPauseFile.Visible = true;
                    lblLineCounter.Visible = true;
                    SendPanel.Enabled = false;
                    _abortFileSend = false;
                    Application.DoEvents();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        while (!_terminateFlag && _pauseFileSend)
                        {
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }
                        if (_abortFileSend)
                            break;
                        string str = lines[i];
                        if (fs.SendCR)
                            str += "$0D";
                        if (fs.SendLF)
                            str += "$0A";
                        SendDollarString(str, 0);
                        lblLineCounter.Text = (i + 1).ToString();
                        progressBar.Value++;
                        Application.DoEvents();
                        Thread.Sleep(fs.InterLineDelay);
                    }
                }
                catch
                {
                    MessageBox.Show($"The file could not be sent successfully", "Send error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            lblLineCounter.Visible = false;
            btnFile.Enabled = true;
            SendPanel.Enabled = true;
            progressBar.Visible = false;
            btnAbortFile.Visible = false;
            btnPauseFile.Visible = false;
            Application.DoEvents();
        }

        private void BtnProfileSelect_Click(object sender, EventArgs e)
        {
            btnProfileSelect.Enabled = false;
            {
                SaveThisProfile();
                FrmProfileDatabase db = new FrmProfileDatabase(_activeProfile.name);
                TopMost = false;
                db.StartOnly = false;
                db.ShowDialog();
                TopMost = cbStayOnTop.Checked;
                if (!db.SelectedProfile.Equals(_activeProfile.name))
                    SwitchToProfile(db.SelectedProfile);
            }
            btnProfileSelect.Enabled = true;
            tbCommand.Focus();
        }

        private void BtnQuickLaunch_Click(object sender, EventArgs e)
        {
            btnQuickLaunch.Enabled = false;
            {
                FrmProfileDatabase db = new FrmProfileDatabase(_activeProfile.name);
                TopMost = false;
                db.StartOnly = true;
                db.ShowDialog();
                TopMost = cbStayOnTop.Checked;
                btnQuickLaunch.Enabled = true;
            }
            tbCommand.Focus();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            {
                string source = "(Your Default profile will be copied)";
                FrmProfileName askname = new FrmProfileName(source, "Name of the New Profile");
                askname.ShowDialog();
                if (!askname.NewName.Equals(source))
                {
                    if (!Database.Find(askname.NewName))
                        Database.Copy("Default", askname.NewName);

                    try
                    {
                        string currentExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                        System.Diagnostics.Process.Start(currentExecutable, "\"" + askname.NewName + "\"");
                    }
                    catch { }
                }
            }
            btnNew.Enabled = true;
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (_rtbLock)
            {
                rtb.Copy();
            }
        }

        private void StsLogfile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Log.Filename);
            }
            catch { }
        }

        private void LbOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbOutput.SelectedIndex < 0)
                return;

            string line = lbOutput.SelectedItem.ToString();
            string cmd;
            if (Utils.HasTimestamp(line))
            {
                int i = line.IndexOf(": ");
                cmd = line.Substring(i + 2);
            }
            else
                cmd = line;

            if (cmd.EndsWith("$0A"))
                cmd = cmd.Substring(0, cmd.Length - 3);
            if (cmd.EndsWith("$0D"))
                cmd = cmd.Substring(0, cmd.Length - 3);
            tbCommand.Text = cmd;
            tbCommand.Focus();
        }
        private void LblProfileName_Click(object sender, EventArgs e)
        {
            BtnProfileSelect_Click(sender, e);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            {
                bool initFreeze = cbFreeze.Checked;
                if (!initFreeze)
                    cbFreeze.Checked = true;

                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    try
                    {
                        string fname = saveFileDialog.FileName;
                        lock (_rtbLock)
                        {
                            if (fname.ToLower().EndsWith(".rtf"))
                                rtb.SaveFile(saveFileDialog.FileName);
                            else
                            {
                                string[] lines = rtb.Lines;
                                File.WriteAllLines(fname, lines);
                            }
                        }
                        MessageBox.Show("The buffer has been exported to file", "Buffer Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred while exporting the buffer.  Verify the filename and try again", "Export failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (!initFreeze)
                    cbFreeze.Checked = false;
            }
            btnExport.Enabled = true;
        }

        private void DgMacroTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgMacroTable.ClearSelection();
        }

        private void Rtb_MouseClick(object sender, MouseEventArgs e)
        {
            string line = string.Empty;
            lock (_rtbLock)
            {
                if (rtb.SelectionLength > 0)
                    return;
                if (rtb.Lines.Length == 0)
                    return;

                line = rtb.Lines[rtb.GetLineFromCharIndex(rtb.SelectionStart)];
            }
            if (Utils.HasTimestamp(line))
            {
                int i = line.IndexOf(": ");
                tbCommand.Text = line.Substring(i + 2);
            }
            else
                tbCommand.Text = line;
            tbCommand.Focus();
        }

        private void CbFreeze_MouseDown(object sender, MouseEventArgs e)
        {
            bool newState = !cbFreeze.Checked;
            cbFreeze.Checked = newState;
            Application.DoEvents();
        }

        private void PdPort_SelectedValueChanged(object sender, EventArgs e)
        {
            if (pdPort.Text.Equals("TCP Server"))
            {
                _activeProfile.conOptions.Type = ConOptions.ConType.TCPServer;
                btnConnect.Text = "&Start";
            }
            else if (pdPort.Text.Equals("TCP Client"))
            {
                _activeProfile.conOptions.Type = ConOptions.ConType.TCPClient;
                btnConnect.Text = "&Connect";
            }
            else
            {
                _activeProfile.conOptions.Type = ConOptions.ConType.Serial;
                _activeProfile.conOptions.SerialPort = pdPort.Text;
                btnConnect.Text = "&Connect";
            }

            if (_activeProfile.conOptions.Type == ConOptions.ConType.Serial)
            {
                stsConnectionDetail.Text = $"   Serial: {_activeProfile.conOptions.SerialPort},{_activeProfile.conOptions.Baudrate},{_activeProfile.conOptions.DataBits},{_activeProfile.conOptions.Parity},{_activeProfile.conOptions.StopBits} -- Handshake:{_activeProfile.conOptions.Handshaking}";
                if (_activeProfile.conOptions.InitialRTS)
                    stsConnectionDetail.Text += " + RTS:ON";
                if (_activeProfile.conOptions.InitialDTR)
                    stsConnectionDetail.Text += " + DTR:ON";
                stsConnectionDetail.Text += "   ";
            }
            else if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPServer)
                stsConnectionDetail.Text = $"   TCP Server: localhost:{_activeProfile.conOptions.TCPListenPort}   ";
            else
                stsConnectionDetail.Text = $"   TCP Client: {_activeProfile.conOptions.TCPConnectAdress}:{_activeProfile.conOptions.TCPConnectPort}   ";

            SaveThisProfile();
        }

        private void RefreshPortPulldown()
        {
            _comms.FillPortNames(pdPort);

            if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPClient)
            {
                pdPort.SelectedIndex = pdPort.Items.IndexOf("TCP Client");
            }
            else if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPServer)
            {
                pdPort.SelectedIndex = pdPort.Items.IndexOf("TCP Server");
            }
            else
            {
                int i = pdPort.Items.IndexOf(_activeProfile.conOptions.SerialPort);
                if (i >= 0)
                    pdPort.SelectedIndex = i;
                else if (pdPort.Items.Count > 0)
                    pdPort.SelectedIndex = 0;
            }
        }

        private void PdPort_MouseEnter(object sender, EventArgs e)
        {
            RefreshPortPulldown();
        }

        private void BtnAbortFile_Click(object sender, EventArgs e)
        {
            btnAbortFile.Enabled = false;
            {
                _abortFileSend = true;
                _pauseFileSend = false;
            }
            btnAbortFile.Enabled = true;
        }
        private void BtnPauseFile_Click(object sender, EventArgs e)
        {
            btnPauseFile.Enabled = false;
            {
                _pauseFileSend = !_pauseFileSend;
                btnPauseFile.Text = (_pauseFileSend) ? "GO" : "Pause";
            }
            btnPauseFile.Enabled = true;
        }

        private void Rtb_VScroll(object sender, EventArgs e)
        {
            try
            {
                _stopColoring = false;
                this.Invoke(this.ColoringInstance);
            }
            catch { }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _terminateFlag = true;
                SaveThisProfile();

                this.Visible = false;
                Application.DoEvents();

                timer.Stop();
                timer.Enabled = false;

                if (_comms.Connected())
                    DoConnectDisconnect();
                Thread.Sleep(50);
                lock (_uiInputQueue)
                {
                    _uiInputQueue.Clear();
                }

                _colorSemaphore.Set();
                for (int t = 0; t < _macroThread.Length; t++)
                    _macroTrigger[t].Set();
                Thread.Sleep(20);
                if (_localThreadCount != 0)
                {
                    for (int t = 0; t < _macroThread.Length; t++)
                        _macroThread[t].Abort();

                }
                for (int t = 0; t < _macroThread.Length; t++)
                {
                    if (_macroThread[t] != null)
                        _macroThread[t].Join();
                }

                if (_connectThread != null)
                    _connectThread.Join();
            }
            catch { }
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}