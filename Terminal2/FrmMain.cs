using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Terminal
{
    public partial class FrmMain : Form
    {
        public delegate void EnqueuedDelegate();
        public EnqueuedDelegate UIInputQHandlerInstance;
        public EnqueuedDelegate UIOutputQHandlerInstance;
        private readonly EventQueue _uiInputQueue;

        private readonly Queue<string> _cmdQueue = new Queue<string>();
        private readonly Queue<int> _macroQueue = new Queue<int>();
        private readonly String _timLock = string.Empty;

        public bool ExitFlag = false;
        private Profile _activeProfile = new Profile();

        private int _ticks = 0;
        private int _tock = 0;
        private Comms _comms = null;

        private enum State { Changing, Disconnected, Connected, Listening };
        private State _state = State.Disconnected;

        private Thread _connectThread = null;
        private bool _threadResult;
        private bool _threadDone = false;

        // File transmission
        private string _lastFileSend = string.Empty;
        private int _lastFileSendILD = 0;
        private bool _lastSendCR = true;
        private bool _lastSendLF = true;

        private int x = 0;
        private string _logLine = string.Empty;
        private string _timeAtX0;
        private Color _activeColor = Color.Black;
        private bool _colorHasChanged = false;

        private bool _firstActivation = true;

        private void DetectColourChange(string str)
        {
            if (_colorHasChanged)
                return;

            for (int i = 0; i < _activeProfile.displayOptions.lines.Length; i++)
            {
                if (_activeProfile.displayOptions.lines[i].text.Length > 0)
                {
                    // starts with
                    if (_activeProfile.displayOptions.lines[i].mode == 0)
                    {
                        if (str.StartsWith(_activeProfile.displayOptions.lines[i].text))
                        {
                            _activeColor = _activeProfile.displayOptions.lines[i].color;
                            _colorHasChanged = true;
                            break;
                        }
                    }

                    // contains
                    else if (_activeProfile.displayOptions.lines[i].mode == 1)
                    {
                        if (str.Contains(_activeProfile.displayOptions.lines[i].text))
                        {
                            _activeColor = _activeProfile.displayOptions.lines[i].color;
                            _colorHasChanged = true;
                            break;
                        }
                    }
                }
            }
        }

        private void ShowText(string text)
        {
            if (_activeProfile.displayOptions.colorFiltersEnabled)
                rtb.AppendText(text, _activeColor);
            else
                rtb.AppendText(text);
        }

        private void UIInputQueueHandler()
        {
            if (cbFreeze.Checked)
                return;

            while (_uiInputQueue.Count > 0)
            {
                string str = (string)_uiInputQueue.Dequeue();

                foreach (char c in str)
                {
                    switch (c)
                    {
                        case (char)'\r':
                            if (cbShowCR.Checked)
                            {
                                ShowText("{CR}");
                                _logLine += "{CR}";
                            }
                            if (rbEndOnCR.Checked)
                            {
                                if (x == 0)
                                {
                                    _timeAtX0 = Utils.Timestamp();
                                    ShowText(_timeAtX0);
                                }

                                Log.Add("R : " + _timeAtX0 + _logLine);
                                _logLine = string.Empty;
                                x = 0;
                                ShowText("\r");
                                _activeColor = _activeProfile.displayOptions.inputText;
                                _colorHasChanged = false;
                            }
                            break;

                        case (char)'\n':
                            if (cbShowLF.Checked)
                            {
                                ShowText("{LF}");
                                _logLine += "{CR}";
                            }
                            if (rbEndOnLF.Checked)
                            {
                                if (x == 0)
                                {
                                    _timeAtX0 = Utils.Timestamp();
                                    ShowText(_timeAtX0);
                                }

                                Log.Add("R : " + _timeAtX0 + _logLine);
                                _logLine = string.Empty;
                                x = 0;
                                ShowText("\r");
                                _activeColor = _activeProfile.displayOptions.inputText;
                                _colorHasChanged = false;
                            }
                            break;

                        default:
                            _logLine += c;

                            if (x == 0)
                            {
                                _timeAtX0 = Utils.Timestamp();
                                ShowText(_timeAtX0);
                            }

                            if (cbASCII.Checked)
                                ShowText(c.ToString());
                            if (cbHEX.Checked)
                            {
                                byte b = Convert.ToByte(c);
                                ShowText($"{b:X2} ");
                            }

                            if (_activeProfile.displayOptions.colorFiltersEnabled)
                                DetectColourChange(_logLine);

                            x++;
                            break;
                    }
                }
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

            helpASCII.Items.Add("ASCII table");
            for (int i = 0; i <= 255; i++)
            {
                string str = $"{i:d3} = {i:x2} = {Convert.ToChar(i)}";
                helpASCII.Items.Add(str);
            }

            UIInputQHandlerInstance = new EnqueuedDelegate(UIInputQueueHandler);
            _uiInputQueue = new EventQueue(this, UIInputQHandlerInstance);

            Database.Initialise();

            if (!Database.Find("Default"))
            {
                Profile s = new Profile
                {
                    name = "Default"
                };

                FrmDisplayOptions conf = new FrmDisplayOptions();
                for (int i = 0; i < conf.PanelList.Length; i++)
                    s.displayOptions.lines[i].color = conf.PanelList[i].BackColor;

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
            _comms.GetPortNames(pdPort);
            SwitchToProfile(preferred);
        }

        private void BtnColorConfig_Click(object sender, EventArgs e)
        {
            btnColorConfig.Enabled = false;
            {
                SaveActiveProfile();
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
                    SaveActiveProfile();

                    rtb.BackColor = _activeProfile.displayOptions.inputBackground;
                    lbOutput.BackColor = _activeProfile.displayOptions.outputBackground;
                    rtb.Font = _activeProfile.displayOptions.inputFont;
                    lbOutput.Font = _activeProfile.displayOptions.outputFont;
                    _activeColor = _activeProfile.displayOptions.inputText;
                }
            }
            btnColorConfig.Enabled = true;
            tbCommand.Focus();
        }

        private void StopMacro(Macro mac)
        {
            if (mac != null)
            {
                lock (mac)
                {
                    mac.run = false;
                    dgMacroTable.Rows[mac.uiRow].Cells[mac.uiColumn].Style.BackColor = Color.White;
                    Application.DoEvents();
                }
            }
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
                    btnConnect.Text = "Disconnect";
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
                while (!_threadDone)
                    Thread.Sleep(1);
                _connectThread = null;
            }
            btnConnect.BackColor = Color.Transparent;
            SetPortAndConnectLabels();
            btnConnectOptions.Enabled = true;
            pdPort.Enabled = true;
            lblProfileName.Enabled = true;
            btnProfileSelect.Enabled = true;
            btnFile.Enabled = false;

            foreach (Macro mac in _activeProfile.macros)
                StopMacro(mac);

            Log.Add(Utils.Timestamp() + "{DISCONNECTED}");
            _state = State.Disconnected;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
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
                ActionDisconnect();
            btnConnect.Enabled = true;
            tbCommand.Focus();
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
                    SaveActiveProfile();
            }
            btnEditMacro.Enabled = true;
            tbCommand.Focus();
        }

        private void BtnLogfile_Click(object sender, EventArgs e)
        {
            btnLogOptions.Enabled = false;
            {
                SaveActiveProfile();
                FrmLogOptions options = new FrmLogOptions(_activeProfile.logOptions);
                TopMost = false;
                options.ShowDialog();
                TopMost = cbStayOnTop.Checked;
                if (_activeProfile.logOptions.Modified)
                    SaveActiveProfile();
            }
            btnLogOptions.Enabled = true;
            tbCommand.Focus();
        }

        private void SetPortAndConnectLabels()
        {
            _comms.GetPortNames(pdPort);
            if (_activeProfile.conOptions.Type == ConOptions.ConType.Serial)
            {
                pdPort.SelectedIndex = pdPort.Items.IndexOf(_activeProfile.conOptions.SerialPort);
                if (pdPort.SelectedIndex < 0 && pdPort.Items.Count > 0)
                    pdPort.SelectedIndex = 0;

                // test again (we may not have not have COM ports)
                if (pdPort.SelectedIndex < 0)
                {
                    _activeProfile.conOptions.Type = ConOptions.ConType.TCPClient;
                    pdPort.Text = "TCP Client";
                }
                btnConnect.Text = "Connect";
            }
            else if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPServer)
            {
                pdPort.Text = "TCP Server";
                btnConnect.Text = "Start";
            }
            else
            {
                pdPort.Text = "TCP Client";
                btnConnect.Text = "Connect";
            }
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
                lock (_cmdQueue)
                {
                    _cmdQueue.Enqueue(str);
                }
            }
            tbCommand.Focus();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            btnConnectOptions.Enabled = false;
            pdPort.Enabled = false;
            {
                SaveActiveProfile();

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
                    _comms.GetPortNames(pdPort);
                    SaveActiveProfile();
                }
            }
            SetPortAndConnectLabels();
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
                stsLogfile.Text = Log.Filename;
            }
            else
            {
                btnLogOptions.Enabled = true;
                btnStartLog.BackColor = Color.Transparent;
                btnStartLog.Text = "Start Log";
            }
            tbCommand.Focus();
        }

        private void CbASCII_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbASCII.Checked && !cbHEX.Checked)
                cbHEX.Checked = true;
            tbCommand.Focus();
        }

        private void CbFreeze_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFreeze.Checked)
            {
                cbFreeze.BackColor = Color.Tomato;
            }
            else
            {
                cbFreeze.BackColor = PanelOne.BackColor;
                this.Invoke(this.UIInputQHandlerInstance);
            }

            tbCommand.Focus();
        }

        private void CbHEX_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbASCII.Checked && !cbHEX.Checked)
                cbASCII.Checked = true;
            tbCommand.Focus();
        }

        private void CbStayOnTop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = cbStayOnTop.Checked;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveActiveProfile();
            if (_state == State.Connected)
                _comms.Disconnect();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            MacroPanel.Height = (dgMacroTable.Rows[0].Height * 5) + 2; // +2 for horizontal lines
            dgMacroTable.Dock = DockStyle.Fill;
            dgMacroTable.ClearSelection();
        }
        private void DgMacroTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 1 || e.RowIndex > 4)
            {
                dgMacroTable.ClearSelection();
                tbCommand.Focus();
                return;
            }
            if (e.ColumnIndex < 1 || e.ColumnIndex > 12)
            {
                dgMacroTable.ClearSelection();
                tbCommand.Focus();
                return;
            }

            int id = ((e.RowIndex - 1) * 12) + (e.ColumnIndex - 1);
            if (_activeProfile.macros[id] == null)
                _activeProfile.macros[id] = new Macro();
            Macro mac = _activeProfile.macros[id];
            mac.uiColumn = e.ColumnIndex;
            mac.uiRow = e.RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                if (_activeProfile.macros[id].run)
                {
                    dgMacroTable.ClearSelection();
                    tbCommand.Focus();
                    return;
                }

                FrmMacroOptions macros = new FrmMacroOptions(_activeProfile, id, dgMacroTable);
                TopMost = false;
                macros.ShowDialog();
                if (macros.Modified)
                    SaveActiveProfile();
                TopMost = cbStayOnTop.Checked;
            }
            else
            {
                if (mac.title.Length != 0)
                {
                    if (_state == State.Disconnected)
                    {
                        DialogResult yn = MessageBox.Show("Go online?", "Is offline", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (yn == DialogResult.Yes)
                            BtnConnect_Click(sender, e);
                     }
                    if (_state == State.Connected)
                    {
                        lock (mac)
                        {
                            if (mac.run)
                            {
                                StopMacro(mac);
                            }
                            else
                            {
                                lock (_macroQueue)
                                {
                                    _macroQueue.Enqueue(id);
                                }
                            }
                        }
                    }
                }
            }
            dgMacroTable.ClearSelection();
            tbCommand.Focus();
        }
        private void MacroTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgMacroTable.ClearSelection();
            tbCommand.Focus();
        }

        private void SaveActiveProfile()
        {
            _activeProfile.stayontop = cbStayOnTop.Checked;
            _activeProfile.timestampInput = cbTimestamp.Checked;
            _activeProfile.ascii = cbASCII.Checked;
            _activeProfile.hex = cbHEX.Checked;
            _activeProfile.endOnCR = rbEndOnCR.Checked;
            _activeProfile.showCurlyCR = cbShowCR.Checked;
            _activeProfile.showCurlyLF = cbShowLF.Checked;
            _activeProfile.sendCR = cbSendCR.Checked;
            _activeProfile.sendLF = cbSendLF.Checked;
            Database.SaveProfile(_activeProfile);
        }

        private void ShowOutgoingData(string line)
        {
            string tm = Utils.Timestamp();
            if (_activeProfile.displayOptions.timestampOutputLines)
            {
                lbOutput.Items.Add(tm + line);
            }
            else
                lbOutput.Items.Add(line);

            Log.Add(" T: " + tm + line);
            lbOutput.TopIndex = lbOutput.Items.Count - 1;
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

                rtb.BackColor = _activeProfile.displayOptions.inputBackground;
                lbOutput.BackColor = _activeProfile.displayOptions.outputBackground;
                rtb.Font = _activeProfile.displayOptions.inputFont;
                lbOutput.Font = _activeProfile.displayOptions.outputFont;
                _activeColor = _activeProfile.displayOptions.inputText;

                lblProfileName.Text = _activeProfile.name;
                cbStayOnTop.Checked = _activeProfile.stayontop;
                cbTimestamp.Checked = _activeProfile.timestampInput;
                cbASCII.Checked = _activeProfile.ascii;
                cbHEX.Checked = _activeProfile.hex;

                if (_activeProfile.endOnCR)
                    rbEndOnCR.Checked = true;
                else
                    rbEndOnLF.Checked = true;
                cbShowCR.Checked = _activeProfile.showCurlyCR;
                cbShowLF.Checked = _activeProfile.showCurlyLF;
                cbSendCR.Checked = _activeProfile.sendCR;
                cbSendLF.Checked = _activeProfile.sendLF;
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
                SetPortAndConnectLabels();
            }
        }
        private void TbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                if (e.Control)
                    rtb.Copy();
            }
            else if (e.KeyCode == Keys.A)
            {
                if (e.Control)
                    rtb.SelectAll();
            }
            else if (e.KeyCode == Keys.X)
            {
                if (e.Control)
                {
                    rtb.Copy();
                    rtb.Clear();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (helpCmdLine.Visible)
                    BtnHelp_Click(sender, e);
                tbCommand.Text = string.Empty;
                foreach (Macro mac in _activeProfile.macros)
                    StopMacro(mac);
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
                    if (mac == null)
                    {
                        dgMacroTable.ClearSelection();
                        tbCommand.Focus();
                        return;
                    }

                    if (_state == State.Connected)
                    {
                        lock (mac)
                        {
                            if (!mac.run)
                            {
                                mac.uiColumn = column;
                                mac.uiRow = row;
                                _macroQueue.Enqueue(index);
                            }
                        }
                    }
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
            byte[] array = Encoding.UTF8.GetBytes(cmd);
            byte[] oneByte = new byte[1];

            ShowOutgoingData(cmd);

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

        private void PrepareMacro(int id)
        {
            Macro mac = _activeProfile.macros[id];
            if (mac == null)
                return;
            if (mac.run)
                return;

            string[] delim = { "{0D}", "{0A}" };
            lock (mac)
            {
                int dbc = mac.delayBetweenChars;
                int dbl = mac.delayBetweenLines;
                bool repeat = mac.repeatEnabled;
                int resendTO = mac.resendEveryMs;
                int maxsends = mac.stopAfterRepeats;

                mac.runLines = mac.macro.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                if (mac.runLines.Length == 0)
                    return;

                if (mac.runLines.Length == 1)
                    dbl = 0;

                for (int i = 0; i < mac.runLines.Length; i++)
                {
                    if (mac.addCR)
                        mac.runLines[i] += "$0D";
                    if (mac.addLF)
                        mac.runLines[i] += "$0A";
                }

                if (dbc == 0 && dbl == 0 && resendTO == 0 && maxsends == 0)
                {
                    foreach (string s in mac.runLines)
                        SendDollarString(s, 0);
                }
                else
                {
                    if (!mac.run)
                    {
                        if (repeat)
                            mac.runRepeats = mac.stopAfterRepeats;
                        else
                            mac.runRepeats = 0;
                        mac.runNextLine = 0;
                        mac.runTimeout = 0;
                        mac.run = true;
                        dgMacroTable.Rows[mac.uiRow].Cells[mac.uiColumn].Style.BackColor = Color.Lime;
                        Application.DoEvents();
                    }
                }
            }
        }

        private void ServiceRunningMacros()
        {
            long tnow = DateTime.Now.Ticks;
            foreach (Macro mac in _activeProfile.macros)
            {
                if (mac == null)
                    continue;

                lock (mac)
                {
                    if (mac.run)
                    {
                        if (tnow >= mac.runTimeout)
                        {
                            SendDollarString(mac.runLines[mac.runNextLine], mac.delayBetweenChars);
                            mac.runNextLine++;
                            if (mac.runNextLine == mac.runLines.Length)
                            {
                                if (mac.repeatEnabled)
                                {
                                    mac.runNextLine = 0;
                                    mac.runTimeout = DateTime.Now.AddMilliseconds(mac.resendEveryMs).Ticks;

                                    if (mac.runRepeats > 0)
                                    {
                                        mac.runRepeats--;
                                        if (mac.runRepeats == 0)
                                            StopMacro(mac);
                                        if (mac.resendEveryMs > 0)
                                            Thread.Sleep(mac.resendEveryMs);
                                    }
                                }
                                else
                                {
                                    StopMacro(mac);
                                }
                            }
                            else
                            {
                                mac.runTimeout = DateTime.Now.AddMilliseconds(mac.delayBetweenLines).Ticks;
                            }
                        }
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_comms == null)
                return;
            else if (_state == State.Changing || _state == State.Disconnected)
                return;
            else if (_state == State.Connected)
            {
                if (!_comms.Connected())
                {
                    ActionDisconnect();
                }
                else
                {
                    if (_activeProfile.conOptions.Type == ConOptions.ConType.TCPClient ||
                        _activeProfile.conOptions.Type == ConOptions.ConType.TCPServer)
                    {
                        int count = _comms.DataWaiting();
                        if (count > 0)
                        {
                            byte[] data = _comms.Read();
                            string str = System.Text.Encoding.Default.GetString(data);
                            lock (_uiInputQueue)
                            {
                                _uiInputQueue.Enqueue(str);
                            }
                        }
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
                        _comms = null;
                        btnConnect.BackColor = Color.Transparent;
                        SetPortAndConnectLabels();
                        btnConnectOptions.Enabled = true;
                        pdPort.Enabled = true;
                        lblProfileName.Enabled = true;
                        btnProfileSelect.Enabled = true;
                        btnFile.Enabled = false;
                        Log.Add(Utils.Timestamp() + "{DISCONNECTED}");
                    }
                    return;
                }

                _ticks++;
                if (_ticks == (100 / timer.Interval))
                {
                    string[] chr = { "|", "/", "---", "\\" };
                    btnConnect.Text = chr[_tock++];
                    if (_tock == 4)
                        _tock = 0;
                    _ticks = 0;
                }
            }

            lock (_timLock)
            {
                lock (_cmdQueue)
                {
                    while (_cmdQueue.Count > 0)
                        SendDollarString(_cmdQueue.Dequeue(), 0);
                }
                lock (_macroQueue)
                {
                    while (_macroQueue.Count > 0)
                        PrepareMacro(_macroQueue.Dequeue());
                }
                ServiceRunningMacros();
            }
        }

        private void ResetFocus(object sender, EventArgs e)
        {
            tbCommand.Focus();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            if (!helpCmdLine.Visible)
            {
                helpSource.BackColor = rtb.BackColor;
                helpSource.Visible = true;
                helpCmdLine.Visible = true;
                helpMacroTable.Visible = true;
                helpSplitline.Visible = true;
                helpProfile.Visible = true;
                helpOutput.Visible = true;
                helpASCII.Visible = true;
                helpMacroIcon.Visible = true;
                helpLogfile.Visible = true;
                helpColors.Visible = true;
                helpClickHere.Visible = true;
                helpInput.Visible = true;
                btnHelp.BackColor = Color.Lime;
            }
            else
            {
                helpSource.Visible = false;
                helpCmdLine.Visible = false;
                helpMacroTable.Visible = false;
                helpSplitline.Visible = false;
                helpProfile.Visible = false;
                helpOutput.Visible = false;
                helpASCII.Visible = false;
                helpMacroIcon.Visible = false;
                helpLogfile.Visible = false;
                helpColors.Visible = false;
                helpClickHere.Visible = false;
                helpInput.Visible = false;
                btnHelp.BackColor = Color.Transparent;
            }
            tbCommand.Focus();
            Application.DoEvents();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            if (_firstActivation)
            {
                BtnHelp_Click(sender, e);
                Application.DoEvents();
                MessageBox.Show($"This is a one-time message.\nThe Help button has been pressed for you.\nPress it whenever you need reminders of the shortcuts.\n(Please read the information carefully and press Help to close)", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tbCommand.Focus();
            Application.DoEvents();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            rtb.Text = string.Empty;
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
                    progressBar.Value = 0;
                    progressBar.Maximum = lines.Length;
                    progressBar.Visible = true;
                    SendPanel.Enabled = false;
                    Application.DoEvents();

                    foreach (string line in lines)
                    {
                        string str = line;
                        if (fs.SendCR)
                            str += "$0D";
                        if (fs.SendLF)
                            str += "$0A";
                        lock (_cmdQueue)
                        {
                            _cmdQueue.Enqueue(str);
                        }
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
            btnFile.Enabled = true;
            SendPanel.Enabled = true;
            progressBar.Visible = false;
            Application.DoEvents();
        }

        private void LogFlusher_Tick(object sender, EventArgs e)
        {
            Log.Flush();
        }

        private void BtnProfileSelect_Click(object sender, EventArgs e)
        {
            btnProfileSelect.Enabled = false;
            {
                SaveActiveProfile();
                FrmProfileDatabase db = new FrmProfileDatabase(_activeProfile.name);
                TopMost = false;
                db.ShowDialog();
                TopMost = cbStayOnTop.Checked;
                if (!db.SelectedProfile.Equals(_activeProfile.name))
                    SwitchToProfile(db.SelectedProfile);
            }
            btnProfileSelect.Enabled = true;
            tbCommand.Focus();
        }

        private void LblProfileName_DoubleClick(object sender, EventArgs e)
        {
            BtnProfileSelect_Click(sender, e);
        }

        private void BtnNew_Click(object sender, EventArgs e)
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

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb.Copy();
        }

        private void StsLogfile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Log.Filename);
            }
            catch { }
        }

        private void LbOutput_Click(object sender, EventArgs e)
        {
            if (lbOutput.SelectedIndex < 0)
                return;

            string line = lbOutput.SelectedItem.ToString();
            string cmd;
            if (_activeProfile.displayOptions.timestampOutputLines && line.Length >= 14)
                cmd = line.Substring(14);
            else
                cmd = line;

            if (cmd.EndsWith("$0A"))
                cmd = cmd.Substring(0, cmd.Length - 3);
            if (cmd.EndsWith("$0D"))
                cmd = cmd.Substring(0, cmd.Length - 3);
            tbCommand.Text = cmd;

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