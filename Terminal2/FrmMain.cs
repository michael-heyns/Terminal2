﻿/* 
 * Terminal2
 *
 * Copyright © 2022-23 Michael Heyns
 * 
 * This file is part of Terminal2.
 * 
 * Terminal2 is free software: you  can redistribute it and/or  modify it 
 * under the terms of the GNU General Public License as published  by the 
 * Free Software Foundation, either version 3 of the License, or (at your
 * option) any later version.
 * 
 * Terminal2 is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the  implied  warranty  of MERCHANTABILITY or 
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
 * more details.
 * 
 * You should have  received a copy of the GNU General Public License along 
 * with Terminal2. If not, see <https://www.gnu.org/licenses/>. 
 *
 */


using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using static System.Net.WebRequestMethods;

namespace Terminal
{
    public partial class FrmMain : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_CONTINUOUS = 0x80000000
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE flags);

        static string STAY_ALIVE_NAME = "Terminal2_LT";

        private const int MAX_INPUT_LINE_COUNT = 10000;
        private const int MAX_OUTPUT_LINE_COUNT = 1000;

        public delegate void EnqueuedDelegate();
        public EnqueuedDelegate UIInputQHandlerInstance;
        private readonly EventQueue _uiInputQueue = null;
        public EnqueuedDelegate UIOutputQHandlerInstance;
        private readonly EventQueue _uiOutputQueue = null;

        public bool ExitFlag = false;
        private Profile _activeProfile = new Profile();
        private FrmHelp _help = new FrmHelp();

        private int _ticks = 0;
        private int _tock = 0;
        private readonly Comms _comms = null;

        private readonly static object _dollarLock = new object();

        private readonly FrmSearch _search = new FrmSearch();
        private enum State { Changing, Disconnected, Connected, Listening };
        private State _state = State.Disconnected;

        // MACRO threads
        private readonly Thread[] _macroThread = new Thread[48 * 4];
        private static readonly AutoResetEvent[] _macroTrigger = new AutoResetEvent[48 * 4];
        private static readonly bool[] _macroBusy = new bool[48 * 4];
        private static readonly bool[] _macroTriggeredByFilter = new bool[48 * 4];

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
        private int _macroOffset = 0;
        private char _macroLabel = 'A';

        private volatile bool _tickBusy = false;
        private static bool _terminateFlag = false;
        private static int _localThreadCount = 0;

        private volatile bool _frozen = false;

        private const int SEARCH_INDEX = 11;
        private Brush[] _filterFrontBrush = new Brush[12];
        private Brush[] _filterBackBrush = new Brush[12];
        private string[] _filterMacro = new string[12];
        private Brush _inputDefaultFrontBrush;
        private Brush _inputBackBrush;
        private Brush _outputFrontBrush;
        private Brush _outputBackBrush;
        private bool _cancelRestart = false;
        private Color _startupButtonColor;
        private bool _lineFinished = true;
        private ToolTip toolTip = new ToolTip();
        private int _stopLine = -1;
        private bool _interceptAltF4 = false;

        private int _typingDollarValueIndex = 0;
        private char _typingDollarValueCh1;
        private string _typingHistoryBuffer = string.Empty;
        private bool _typingPaused = false;
        private bool _typingWarnedAboutDelete = false;

        private bool _freezeTriggeredFromMacro = false;

        private bool _resetHistoryIndex = true;
        private StringBuilder _macroTriggerString = new StringBuilder();

        private Button[] _btnGroups = new Button[4];

        private string[] _LogLimits = { "No limits", "1000", "5000", "10000", "50000", "100000", "500000", "1000000"};

        // ==============================================
        private void SetSearchText(string str)
        {
            if (Utils.HasTimestamp(str))
                _activeProfile.displayOptions.filter[SEARCH_INDEX].text = str.Substring(Utils.TimestampLength());
            else
                _activeProfile.displayOptions.filter[SEARCH_INDEX].text = str;
            _activeProfile.displayOptions.filter[SEARCH_INDEX].mode = 1;
            lbInput.Refresh();
        }

        //private int FindApplicableFilter(string str, int offset)
        //{
        //    if (offset >= str.Length)
        //        return -1;

        //    if (_activeProfile.displayOptions.IgnoreCase)
        //    {
        //        for (int i = 0; i < _activeProfile.displayOptions.filter.Length; i++)
        //        {
        //            if (_activeProfile.displayOptions.filter[i].text.Length > 0)
        //            {
        //                // starts with
        //                if (_activeProfile.displayOptions.filter[i].mode == 0)
        //                {
        //                    if (str.ToLower().Substring(offset).StartsWith(_activeProfile.displayOptions.filter[i].text.ToLower()))
        //                        return i;
        //                }

        //                // contains
        //                else if (_activeProfile.displayOptions.filter[i].mode == 1)
        //                {
        //                    if (str.ToLower().Substring(offset).Contains(_activeProfile.displayOptions.filter[i].text.ToLower()))
        //                        return i;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < _activeProfile.displayOptions.filter.Length; i++)
        //        {
        //            if (_activeProfile.displayOptions.filter[i].text.Length > 0)
        //            {
        //                // starts with
        //                if (_activeProfile.displayOptions.filter[i].mode == 0)
        //                {
        //                    if (str.Substring(offset).StartsWith(_activeProfile.displayOptions.filter[i].text))
        //                        return i;
        //                }

        //                // contains
        //                else if (_activeProfile.displayOptions.filter[i].mode == 1)
        //                {
        //                    if (str.Substring(offset).Contains(_activeProfile.displayOptions.filter[i].text))
        //                        return i;
        //                }
        //            }
        //        }
        //    }
        //    return -1;
        //}

        private void UpdatePendingCounter()
        {
            if (_uiInputQueue.Count > 3000)
            {
                _uiInputQueue.Clear();
                UnFreeze();
                DoConnectDisconnect();
                stsPending.ForeColor = SystemColors.ControlText;
                stsPending.BackColor = SystemColors.Control;
            }
            if (_uiInputQueue.Count > 2000)
            {
                stsPending.ForeColor = Color.Yellow;
                stsPending.BackColor = Color.Red;
            }
            else if (_uiInputQueue.Count > 1000)
            {
                stsPending.ForeColor = Color.Red;
                stsPending.BackColor = Color.Yellow;
            }
            else
            {
                stsPending.ForeColor = SystemColors.ControlText;
                stsPending.BackColor = SystemColors.Control;
            }
            stsPending.Text = _uiInputQueue.Count.ToString();
        }

        private void UpdateLineCounter()
        {
            stsLineCount.Text = lbInput.Items.Count.ToString();
        }

        private void ScrollToBottom()
        {
            if (!_frozen)
                lbInput.TopIndex = lbInput.Items.Count - 1;
        }

        private void TestForFreeze()
        {
            if (!cbFreezeAt.Checked || freezeText.Text.Length == 0)
                return;

            int line = lbInput.Items.Count - 1;
            if (cbFreezeCase.Checked)
            {
                string strC = lbInput.Items[lbInput.Items.Count - 1].ToString();
                if (!strC.Contains(freezeText.Text))
                    return;
            }
            else
            {
                string str = lbInput.Items[lbInput.Items.Count - 1].ToString().ToLower();
                if (!str.Contains(freezeText.Text.ToLower()))
                    return;

            }

            // freeze
            if (!_frozen)
            {
                if (line > _stopLine)
                {
                    _stopLine = line;
                    Freeze(_stopLine - 5);

                    // add it to our archive list
                    if (cbFreezeAt.Checked)
                    {
                        int index = freezeText.FindString(freezeText.Text);
                        if (index < 0)
                            freezeText.Items.Add(freezeText.Text);
                    }
                }
            }
        }

        private void LogAdd(string msg)
        {
            if (Log.Add(msg))
            {
                stsLogfile.Text = "   " + Log.Filename + "   ";
            }
        }

        private void AddToMacropActivationString(string str)
        {
            Filters filters = new Filters();
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (ch < ' ')
                {
                    string line = _macroTriggerString.ToString();
                    int offset = 0;
                    if (Utils.HasTimestamp(line))
                        offset += Utils.TimestampLength();
                    int filter = filters.FindApplicableFilter(_activeProfile, line, offset);
                    if (filter >= 0)
                    {
                        int m = FindMacro(_filterMacro[filter]);
                        if (m >= 0)
                        {
                            if (!_macroTriggeredByFilter[m])
                            {
                                _macroTriggeredByFilter[m] = true;
                                if (!_macroBusy[m])
                                    _macroTrigger[m].Set();
                            }
                        }
                    }
                    _macroTriggerString.Clear();
                }
                else
                {
                    _macroTriggerString.Append(ch);
                }
            }

        }

        private void UIInputQueueHandler()
        {
            if (_frozen)
            {
                UpdatePendingCounter();
                return;
            }

            while (!_terminateFlag && _uiInputQueue.Count > 0)
            {
                string str = (string)_uiInputQueue.Dequeue();
                LogAdd(str);

                AddToMacropActivationString(str);

                char[] delim = { '\n' };
                string[] lines = str.Split(delim);

                int start = 0;
                if (!_lineFinished)
                {
                    lbInput.BeginUpdate();
                    {
                        try
                        {
                            lbInput.Items[lbInput.Items.Count - 1] += lines[0];
                        }
                        catch { }
                    }
                    lbInput.EndUpdate();
                    TestForFreeze();
                    start = 1;
                }

                if (str.EndsWith("\n"))
                {
                    lbInput.BeginUpdate();
                    {
                        try
                        {
                            for (int i = start; i < lines.Length - 1; i++)
                            {
                                lbInput.Items.Add(lines[i]);
                                TestForFreeze();
                            }
                        }
                        catch { }
                    }
                    lbInput.EndUpdate();
                    _lineFinished = true;
                }
                else
                {
                    lbInput.BeginUpdate();
                    {
                        try
                        {
                            for (int i = start; i < lines.Length; i++)
                            {
                                lbInput.Items.Add(lines[i]);
                                TestForFreeze();
                            }
                        }
                        catch { }
                    }
                    lbInput.EndUpdate();
                    _lineFinished = false;
                }

                if (lbInput.Items.Count >= MAX_INPUT_LINE_COUNT)
                {
                    lbInput.BeginUpdate();
                    {
                        try
                        {
                            while (lbInput.Items.Count >= MAX_INPUT_LINE_COUNT)
                                lbInput.Items.RemoveAt(0);
                        }
                        catch { }
                    }
                    lbInput.EndUpdate();
                }
                ScrollToBottom();
                UpdateLineCounter();
                UpdatePendingCounter();
                Application.DoEvents();
            }
        }
        private void UIOutputQueueHandler()
        {
            while (!_terminateFlag && _uiOutputQueue.Count > 0)
            {
                string str = (string)_uiOutputQueue.Dequeue();
                string tm = Utils.Timestamp();
                LogAdd("\r\n----> Tx: " + tm + str + "\r\n");

                lbOutput.BeginUpdate();
                {
                    if (_activeProfile.displayOptions.ShowOutputTimestamp)
                    {
                        if (_typingHistoryBuffer.Length == 0)
                            _typingHistoryBuffer += tm;

                        _typingHistoryBuffer += str;
                        if (str.EndsWith("$0D") || str.EndsWith("$0A"))
                        {
                            lbOutput.Items.Add(_typingHistoryBuffer);
                            _typingHistoryBuffer = string.Empty;
                        }

                    }
                    else
                    {
                        _typingHistoryBuffer += str;
                        if (str.EndsWith("$0D") || str.EndsWith("$0A"))
                        {
                            if (lbOutput.Items.IndexOf(_typingHistoryBuffer) < 0)
                                lbOutput.Items.Add(_typingHistoryBuffer);
                            _typingHistoryBuffer = string.Empty;
                        }
                    }

                    while (lbOutput.Items.Count > MAX_OUTPUT_LINE_COUNT)
                        lbOutput.Items.RemoveAt(0);
                    lbOutput.TopIndex = lbOutput.Items.Count - 1;
                }
                lbOutput.EndUpdate();
            }
        }

        public FrmMain(string[] args)
        {
            InitializeComponent();

            this.Text = VersionLabel;

            dgMacroTable.ReadOnly = true;
            dgMacroTable.RowCount = 5;
            for (int f = 1; f <= 12; f++)
                dgMacroTable.Rows[0].Cells[f].Style.BackColor = btnGroup1.BackColor;

            for (int r = 0; r <= 4; r++)
                dgMacroTable.Rows[r].Cells[0].Style.BackColor = btnGroup1.BackColor;

            //dgMacroTable.Rows[0].Cells[0].Value = " <--  --> ";
            dgMacroTable.Rows[1].Cells[0].Value = "Plain";	// "1 - 48";
            dgMacroTable.Rows[2].Cells[0].Value = "Shift+";	// "49 - 96";
            dgMacroTable.Rows[3].Cells[0].Value = "Ctrl+";	// "97 - 144";
            dgMacroTable.Rows[4].Cells[0].Value = "Alt+";	// "145 - 192";
            dgMacroTable.ClearSelection();
            stsLogfile.Text = "----";

            UIInputQHandlerInstance = new EnqueuedDelegate(UIInputQueueHandler);
            _uiInputQueue = new EventQueue(this, UIInputQHandlerInstance);

            UIOutputQHandlerInstance = new EnqueuedDelegate(UIOutputQueueHandler);
            _uiOutputQueue = new EventQueue(this, UIOutputQHandlerInstance);

            Database.Initialise();

            if (!Database.Find("Default"))
            {
                Profile s = new Profile
                {
                    name = "Default"
                };

                FrmDisplayOptions conf = new FrmDisplayOptions();
                for (int i = 0; i < conf.ForePanelList.Length; i++)
                    s.displayOptions.filter[i].foreColor = conf.ForePanelList[i].BackColor;

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

                TopMost = false;

                FrmDisplayOptions DialogOptions = new FrmDisplayOptions();
                DialogOptions.MacroNames.Clear();
                foreach (Macro m in _activeProfile.macros)
                {
                    if (m != null)
                    {
                        if (m.title.Length > 0)
                            DialogOptions.MacroNames.Add(m.title);
                    }
                }

                DialogOptions.Options = _activeProfile.displayOptions.Clone();
                DialogOptions.ShowDialog();

                TopMost = cbStayOnTop.Checked;

                if (DialogOptions.Result == DialogResult.OK)
                {
                    _activeProfile.displayOptions = DialogOptions.Options.Clone();
                    SaveThisProfile();

                    lbInput.BackColor = _activeProfile.displayOptions.inputBackground;
                    lbInput.Font = _activeProfile.displayOptions.inputFont;
                    _inputBackBrush = new SolidBrush(_activeProfile.displayOptions.inputBackground);
                    _inputDefaultFrontBrush = new SolidBrush(_activeProfile.displayOptions.inputDefaultForeground);
                    lbInput.Refresh();

                    lbOutput.BackColor = _activeProfile.displayOptions.outputBackground;
                    lbOutput.Font = _activeProfile.displayOptions.outputFont;
                    _outputBackBrush = new SolidBrush(_activeProfile.displayOptions.outputBackground);
                    _outputFrontBrush = Brushes.Black;
                    lbOutput.Refresh();

                    for (int f = 0; f < _filterFrontBrush.Length; f++)
                    {
                        _filterFrontBrush[f] = new SolidBrush(_activeProfile.displayOptions.filter[f].foreColor);
                        _filterBackBrush[f] = new SolidBrush(_activeProfile.displayOptions.filter[f].backColor);
                        _filterMacro[f] = _activeProfile.displayOptions.filter[f].macro;
                    }
                }
                DialogOptions = null;
            }
            btnColorConfig.Enabled = true;
            lbInput.Refresh();
            lbOutput.Refresh();
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
            btnConnect.Text = "------";
            Application.DoEvents();

            _cancelRestart = false;
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
                btnRescan.Enabled = false;
                btnProfile.Enabled = false;

                LogAdd(Utils.Timestamp() + "{LISTENING}");
                _state = State.Listening;

                _threadDone = false;
                _connectThread = new Thread(new ThreadStart(ConnectionThread));
                _connectThread.Start();
            }
            else
            {
                bool rc = _comms.Connect(_activeProfile);
                if (rc)
                {
                    btnConnect.Text = "Dis&connect";
                    btnConnectOptions.Enabled = false;
                    pdPort.Enabled = false;
                    btnRescan.Enabled=false;
                    btnProfile.Enabled = false;
                    btnFile.Enabled = true;
                    btnConnect.BackColor = Color.Lime;

                    LogAdd(Utils.Timestamp() + "{CONNECTED}");
                    _state = State.Connected;
                    this.Text = pdPort.Text;
                }
                else
                {
                    btnConnect.Text = "&Connect";
                    MessageBox.Show("The port may be used by another application\n** or **\nWindows is preventing access at the moment.\n\nWait a few seconds and try again", "Cannot connect right now", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogAdd(Utils.Timestamp() + "{FAILED}");
                    _state = State.Disconnected;
                }
            }
        }

        private void ActionDisconnect()
        {
            btnConnect.Text = "------";
            Application.DoEvents();
            _comms.Disconnect();

            if (_state == State.Listening)
            {
                if (!_threadDone)
                    _connectThread.Abort();
                _connectThread.Join();
                _connectThread = null;
            }

            btnConnect.BackColor = _startupButtonColor;
            RefreshPortPulldown();
            btnConnectOptions.Enabled = true;
            pdPort.Enabled = true;
            btnRescan.Enabled = true;
            btnProfile.Enabled = true;
            btnFile.Enabled = false;
            Application.DoEvents();

            RestartAllMacros();
            this.Text = VersionLabel;

            LogAdd(Utils.Timestamp() + "{DISCONNECTED}");
            _state = State.Disconnected;
        }

        private void DoConnectDisconnect()
        {
            if (pdPort.Text.Length == 0)
            {
                MessageBox.Show("Please configure a port", "Port not configured", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _macroTriggerString.Clear();
            btnConnect.Enabled = false;
            _cancelRestart = true;
            if (_state == State.Disconnected)
            {
                ActionConnect();
            }
            else
            {
                _abortFileSend = true;
                _pauseFileSend = false;

                ActionDisconnect();
                RefreshPortPulldown();

                if (_frozen)
                {
                    lbInput.BeginUpdate();
                    {
                        _uiInputQueue.Clear();
                        UnFreeze();
                        UpdateLineCounter();
                        UpdatePendingCounter();
                        lbInput.Items.Add(string.Empty);
                        _lineFinished = false;
                    }
                    lbInput.EndUpdate();
                }
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
                string str;
                if (cbSendAsIType.Checked && !_typingPaused)
                    str = string.Empty; // send only CR/LF
                else
                    str = tbCommand.Text;

                if (cbSendCR.Checked)
                    str += "$0D";
                if (cbSendLF.Checked)
                    str += "$0A";
                SendDollarString(str, 0);
                if (cbClearCMD.Checked || cbSendAsIType.Checked)
                    tbCommand.Text = string.Empty;  // clear on send
                _typingPaused = false;
            }
            tbCommand.Focus();
        }

        private string EncapsulateSTXETX(string line)
        {
            STXETX stxetx = new STXETX();
            return stxetx.Encode(line);
        }

        private void btnSTXETX_Click(object sender, EventArgs e)
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
                str = EncapsulateSTXETX(str);
                if (cbSendCR.Checked)
                    str += "$0D";
                if (cbSendLF.Checked)
                    str += "$0A";
                SendDollarString(str, 0);
                if (cbClearCMD.Checked)
                    tbCommand.Text = string.Empty;
                _typingPaused = false;
            }
            tbCommand.Focus();

        }


        private void BtnSettings_Click(object sender, EventArgs e)
        {
            btnConnectOptions.Enabled = false;
            pdPort.Enabled = false;
            btnRescan.Enabled = false;
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
            btnRescan.Enabled=true;
            tbCommand.Focus();
        }

        private void BtnStartLog_Click(object sender, EventArgs e)
        {
            if (!Log.Enabled)
            {
                string dir = _activeProfile.logOptions.Directory;
                if (!Directory.Exists(dir))
                    MessageBox.Show($"The directory {dir} does not exist.  Please reconfigure logging", "Directory does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Log.Start(dir, _activeProfile.logOptions.Prefix, _activeProfile.logOptions.MaxLogSize);
                }
            }
            else
                Log.Stop();

            if (Log.Enabled)
            {
                btnLogOptions.Enabled = false;
                btnStartLog.BackColor = Color.Lime;
                btnStartLog.Text = "Stop &Log";
                stsLogfile.Text = "   " + Log.Filename + "   ";
                LogAdd("---------------------------------------------------------------------\n");
                LogAdd(VersionLabel + "\n");
                LogAdd("Logging started  : " + DateTime.Now.ToString() + "\n");
                LogAdd("Log file name    : " + Log.Filename + "\n");
                LogAdd("Profile          : " + btnProfile.Text + "\n");
                LogAdd("Username         : " + Environment.GetEnvironmentVariable("USERNAME") + "\n");
                LogAdd("---------------------------------------------------------------------\n");
            }
            else
            {
                btnLogOptions.Enabled = true;
                btnStartLog.BackColor = _startupButtonColor;
                btnStartLog.Text = "Start &Log";
            }
            tbCommand.Focus();
        }

        #region EMBELLISHMENTS
        private void cbTimestamp_CheckedChanged(object sender, EventArgs e)
        {
            _activeProfile.embellishments.ShowInputTimestamp = cbTimestamp.Checked;
            _comms.SetEmbellishments(_activeProfile.embellishments);
            tbCommand.Focus();
        }
        private void CbASCII_CheckedChanged(object sender, EventArgs e)
        {
            _comms.ResetHexColumn();
            if (!cbASCII.Checked && !cbHEX.Checked)
                cbHEX.Checked = true;
            _activeProfile.embellishments.ShowASCII = cbASCII.Checked;
            _comms.SetEmbellishments(_activeProfile.embellishments);
            tbCommand.Focus();
        }
        private void CbHEX_CheckedChanged(object sender, EventArgs e)
        {
            _comms.ResetHexColumn();
            if (!cbASCII.Checked && !cbHEX.Checked)
                cbASCII.Checked = true;
            _activeProfile.embellishments.ShowHEX = cbHEX.Checked;
            _comms.SetEmbellishments(_activeProfile.embellishments);
            tbCommand.Focus();
        }
        private void cbShowCR_CheckedChanged(object sender, EventArgs e)
        {
            _activeProfile.embellishments.ShowCR = cbShowCR.Checked;
            _comms.SetEmbellishments(_activeProfile.embellishments);
            tbCommand.Focus();
        }

        private void cbShowLF_CheckedChanged(object sender, EventArgs e)
        {
            _activeProfile.embellishments.ShowLF = cbShowLF.Checked;
            _comms.SetEmbellishments(_activeProfile.embellishments);
            tbCommand.Focus();
        }
        #endregion

        private void Freeze(int topLine)
        {
            _frozen = true;
            btnFreeze.ForeColor = Color.Blue;
            btnFreeze.BackColor = Color.IndianRed;
            btnFreeze.Text = "&GO";
            lbInput.TopIndex = topLine;
        }
        private void UnFreeze()
        {
            btnFreeze.ForeColor = Color.Black;
            btnFreeze.BackColor = _startupButtonColor;
            btnFreeze.Text = "&Freeze";
            _frozen = false;
            _stopLine = lbInput.Items.Count - 1;
            UIInputQueueHandler();
        }

        private void BtnFreeze_Click(object sender, EventArgs e)
        {
            btnFreeze.Enabled = false;
            if (!_frozen)
            {
                if (_comms.Connected() && lbInput.Items.Count > 0)
                {
                    Freeze(lbInput.Items.Count - 1);
                }
            }
            else
            {
                UnFreeze();
            }
            btnFreeze.Enabled = true;
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

        private void ChangeMacroButtonColor(int m, Color color)
        {
            if (m < _macroOffset || m > (_macroOffset + 48)) return;

            int num = m - _macroOffset;
            int col = (num % 12) + 1;
            int row = (num / 12) + 1;
            dgMacroTable.Rows[row].Cells[col].Style.BackColor = color;
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
                    _macroTriggeredByFilter[m] = false;
                    _macroBusy[m] = false;
                    _macroTrigger[m].WaitOne();
                    Thread.Sleep(10);
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

                    ChangeMacroButtonColor(m, Color.Tomato);

                    _macroBusy[m] = true;
                    int repeatCount = 0;
                    while (!_terminateFlag)
                    {
                        for (int x = 0; x < mac.runLines.Length; x++)
                        {
                            string line = mac.runLines[x];

                            // detect #MACRO name
                            int cmdIndex = line.IndexOf("#MACRO ");
                            if (cmdIndex >= 0)
                            {
                                line = line.TrimEnd();
                                if (line.StartsWith("#MACRO "))
                                {
                                    string title = line.Substring(7).Trim();
                                    int mm = FindMacro(title);
                                    if (mm >= 0)
                                        DoMacro(mm);
                                    continue;
                                }

                                // restore and continue
                                line = mac.runLines[x];
                            }

                            // detect #DELAY nn
                            cmdIndex = line.IndexOf("#DELAY ");
                            if (cmdIndex >= 0)
                            {
                                line = line.TrimEnd();
                                if (line.StartsWith("#DELAY "))
                                {
                                    string strNN = line.Substring(7);
                                    try
                                    {
                                        int NN = int.Parse(strNN);
                                        if (NN > 0)
                                        {
                                            Thread.Sleep(NN);   // sleep for N milliseconds
                                            continue;
                                        }
                                    }
                                    catch 
                                    { 
                                        // fall through
                                    }
                                }

                                // restore and continue
                                line = mac.runLines[x];
                            }

                            // detect #FREEZE nn
                            cmdIndex = line.IndexOf("#FREEZE");
                            if (cmdIndex >= 0)
                            {
                                line = line.TrimEnd();
                                if (line.StartsWith("#FREEZE"))
                                {
                                    _freezeTriggeredFromMacro = true;
                                }

                                // restore and continue
                                line = mac.runLines[x];
                            }

                            // detect #DTR nn
                            cmdIndex = line.IndexOf("#DTR");
                            if (cmdIndex >= 0)
                            {
                                line = line.TrimEnd();
                                if (line.StartsWith("#DTR"))
                                {
                                    _comms.ControlPressed(0);
                                }

                                // restore and continue
                                line = mac.runLines[x];
                            }

                            // detect #RTS nn
                            cmdIndex = line.IndexOf("#RTS");
                            if (cmdIndex >= 0)
                            {
                                line = line.TrimEnd();
                                if (line.StartsWith("#RTS"))
                                {
                                    _comms.ControlPressed(1);
                                }

                                // restore and continue
                                line = mac.runLines[x];
                            }

                            // double comments go to input stream as well
                            int noteStart = line.IndexOf("##");
                            if (noteStart >= 0)
                                _uiInputQueue.Enqueue("-NOTE-> " + line.Substring(noteStart + 2) + "\n");

                            // detect #STX
                            bool encapsulateSTXETX = false;
                            cmdIndex = line.IndexOf("#STX ");
                            if (cmdIndex >= 0)
                            {
                                line = line.TrimEnd();
                                if (line.StartsWith("#STX "))
                                {
                                    line = line.Substring(5);   // throw away the #STX
                                    encapsulateSTXETX = true;
                                }
                            }

                            // remove comments altogether
                            int commentStart = line.IndexOf('#');
                            if (commentStart >= 0)
                                line = line.Substring(0, commentStart);

                            // trim
                            line = line.TrimEnd();

                            // encapsulate in STX/ETX packet
                            if (encapsulateSTXETX)
                            {
                                line = EncapsulateSTXETX(line);
                            }

                            // add CR/LF
                            if (line.Length > 0)
                            {
                                if (mac.addCR)
                                    line += "$0D";
                                if (mac.addLF)
                                    line += "$0A";
                            }

                            // send
                            if (line.Length > 0)
                            {
                                SendDollarString(line, mac.delayBetweenChars);
                                Thread.Sleep(mac.delayBetweenLinesMs);
                            }
                        }

                        if (!mac.repeatEnabled)
                            break;
                        repeatCount++;
                        if (mac.stopAfterRepeats > 0 && repeatCount > mac.stopAfterRepeats)
                            break;
                        Thread.Sleep(mac.resendEveryMs);
                    }
                    ChangeMacroButtonColor(m, Color.White);
                }
            }
            catch { }

            if (dgMacroTable.SelectedCells.Count > 0)
                dgMacroTable.ClearSelection();

            _localThreadCount--;
        }

        private void PokeDeviceToKeepAwake(object extra)
        {
            try
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_AWAYMODE_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            }
            catch
            {

            }
        }

        private bool TickTock = false;
        private void KeepAlive_Tick(object sender, EventArgs e)
        {
            POINT cursorPos;
            if (GetCursorPos(out cursorPos))
            {
                // Inject a mouse movement one pixel to the right
                INPUT input = new INPUT();
                input.type = 0; // Mouse input

                if (TickTock)
                {
                    input.mi.dx = 1;
                    input.mi.dy = 1;
                    TickTock = false;
                }
                else
                {
                    input.mi.dx = -1;
                    input.mi.dy = -1;
                    TickTock = true;
                }
                input.mi.dwFlags = 0x0001; // MOUSEEVENTF_MOVE
                SendInput(1, ref input, Marshal.SizeOf(typeof(INPUT)));
            }
        }

        private System.Threading.Timer preventSleepTimer = null;
        private bool StayAliveActive = false;
        private void DisableDeviceSleep()
        {
            try
            {
                string local = Environment.GetEnvironmentVariable("LOCALAPPDATA");
                if (local == null) return;
                string dir = local + "\\" + STAY_ALIVE_NAME;
                if (!Directory.Exists(dir)) return;

                // switch on the timer
                KeepAlive.Enabled = true;

                SetThreadExecutionState(EXECUTION_STATE.ES_AWAYMODE_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
                preventSleepTimer = new System.Threading.Timer(new TimerCallback(PokeDeviceToKeepAwake), null, 0, 30 * 1000);
                StayAliveActive = true;
            }
            catch { }
        }

        private void EnableDeviceSleep()
        {
            try
            {
                if (StayAliveActive)
                {
                    preventSleepTimer.Dispose();
                    preventSleepTimer = null;
                }
            }
            catch { }
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            _startupButtonColor = btnClear.BackColor;
            btnFreeze.BackColor = _startupButtonColor;
            toolTip.AutomaticDelay = 1000; 
            toolTip.InitialDelay = 500; // dT till show
            toolTip.ReshowDelay = 100;  // dT from one tip to another
            toolTip.UseFading = true;
            toolTip.UseAnimation = true;
            //toolTip.SetToolTip(this.pdPort, "A list of available communication channels - this list is updated in whenever this puldown is opened");
            //toolTip.SetToolTip(this.btnConnect, "Click to Connect or Disconnect the selected communications channel");
            //toolTip.SetToolTip(this.btnStartLog, "Click to start logging data to file");
            toolTip.SetToolTip(this.ctrlOne, "Click this LED to toggle it's outgoing state");
            toolTip.SetToolTip(this.ctrlTwo, "Click this LED to toggle it's outgoing state");
            toolTip.SetToolTip(this.LED1, "View-only status of the incoming hardware line");
            toolTip.SetToolTip(this.LED2, "View-only status of the incoming hardware line");
            toolTip.SetToolTip(this.LED3, "View-only status of the incoming hardware line");
            toolTip.SetToolTip(this.LED4, "View-only status of the incoming hardware line");
            toolTip.SetToolTip(this.cbStayOnTop, "Check this to prevent this program (window) from being hidden by other windows");
            toolTip.SetToolTip(this.cbFreezeAt, "Check this to trigger FREEZE when next the search string is received");
            toolTip.SetToolTip(this.freezeText, "Enter the search string which will trigger a FREEZE.  It will be added to the list once it has caused a freeze.");
            toolTip.SetToolTip(this.cbFreezeCase, "Check this to enable Case Sensitive search");
            toolTip.SetToolTip(this.btnConnectOptions, "Click to define Connection Options");
            toolTip.SetToolTip(this.btnLogOptions, "Click to configure Logging Options");
            toolTip.SetToolTip(this.btnNew, "Click to add a NEW profile (without closing this one)");
            toolTip.SetToolTip(this.btnQuickLaunch, "Click to launch ANOTHER session (without closing this one)");
            toolTip.SetToolTip(this.btnColorConfig, "Click to change the Look-and-Feel");

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
                Thread.Sleep(2);
                Application.DoEvents();
            }
            lbInput.Items.Add(string.Empty);
            _lineFinished = false;

            _btnGroups[0] = btnGroup1;
            _btnGroups[1] = btnGroup2;
            _btnGroups[2] = btnGroup3;
            _btnGroups[3] = btnGroup4;
            this.CenterToScreen();

            DisableDeviceSleep();
        }

        private int FindMacro(string title)
        {
            if (title.Length == 0) return -1;

            for (int m = 0; m < _activeProfile.macros.Length; m++)
            {
                Macro mac = _activeProfile.macros[m];
                if (mac != null)
                {
                    if (mac.title.Equals(title))
                        return m;
                }
            }
            return -1;
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
            if (e.RowIndex == 0 && e.ColumnIndex >= 1 && e.ColumnIndex <= 12)
            {
                int index = (_macroOffset / 4) + e.ColumnIndex - 1;
                FrmTitle frmTitle = new FrmTitle();
                frmTitle.eTitle.Text = _activeProfile.titles[index];
                frmTitle.eTitle.SelectAll();
                frmTitle.ShowDialog();
                if (frmTitle.PressedOK)
                {
                    _activeProfile.titles[index] = frmTitle.eTitle.Text;
                }
                ShowColumnTitles();
                return;
            }

            _macroIsMoving = false;
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

            int m = ((e.RowIndex - 1) * 12) + (e.ColumnIndex - 1) + _macroOffset;
            if (_activeProfile.macros[m] == null)
                _activeProfile.macros[m] = new Macro();
            Macro mac = _activeProfile.macros[m];
            mac.uiColumn = e.ColumnIndex;
            mac.uiRow = e.RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                if (_macroBusy[m])
                    RestartMacro(m);

                FrmMacroOptions macros = new FrmMacroOptions(_activeProfile, m);
                TopMost = false;
                macros.macroLabel = _macroLabel;
                macros.macroGroupStart = _macroOffset;
                macros.ShowDialog();
                if (macros.Modified)
                    SaveThisProfile();
                ShowColumnTitles();
                ShowMacroTitles();
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
            dgMacroTable.Enabled = false;

            if (!_macroIsMoving)
            {
                dgMacroTable.Enabled = true;
                return;
            }

            if (e.RowIndex < 1 || e.RowIndex > 4)
            {
                tbCommand.Focus();
                dgMacroTable.Enabled = true;
                return;
            }
            if (e.ColumnIndex < 1 || e.ColumnIndex > 12)
            {
                tbCommand.Focus();
                dgMacroTable.Enabled = true;
                return;
            }

            int m = ((e.RowIndex - 1) * 12) + (e.ColumnIndex - 1) + _macroOffset;
            if (_activeProfile.macros[m] == null)
                _activeProfile.macros[m] = new Macro();
            Macro dst = _activeProfile.macros[m];
            if (dst.title.Length > 0)
            {
                MessageBox.Show("You can only move macros to empty spots on the table", "Target spot is not empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgMacroTable.Enabled = true;
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
            dgMacroTable.Enabled = true;
        }
        private void SaveThisProfile()
        {
            lblSaving.Text = "*";
            Application.DoEvents();
            _activeProfile.stayontop = cbStayOnTop.Checked;
            _activeProfile.sendCR = cbSendCR.Checked;
            _activeProfile.sendLF = cbSendLF.Checked;
            _activeProfile.clearCMD = cbClearCMD.Checked;
            _activeProfile.sendAsIType = cbSendAsIType.Checked;
            Database.SaveProfile(_activeProfile);
            Database.SaveAsActiveProfile(_activeProfile.name);
            Thread.Sleep(50);
            lblSaving.Text = string.Empty;
        }

        private void ShowColumnTitles()
        {
            int index = (_macroOffset / 4);
            for (int col = 0; col < 12; col++)
            {
                if (_activeProfile.titles[index + col].Length > 0)
                    dgMacroTable.Rows[0].Cells[col + 1].Value = _activeProfile.titles[index + col];
                else
                    dgMacroTable.Rows[0].Cells[col + 1].Value = $"F{col+1}";
            }
        }
        private void ShowMacroTitles()
        {
            int i = _macroOffset;
            for (int mRow = 1; mRow <= 4; mRow++)
            {
                for (int mCol = 1; mCol <= 12; mCol++)
                {
                    Macro m = _activeProfile.macros[i++];
                    if (m == null)
                        dgMacroTable.Rows[mRow].Cells[mCol].Value = string.Empty;
                    else
                        dgMacroTable.Rows[mRow].Cells[mCol].Value = m.title;
                }
            }
        }

        private void SwitchToProfile(string name)
        {
            if (name.Length == 0)
                return;

            _firstActivation = !Database.HasActiveProfile();

            if (Database.Find(name))
            {
                string currentPort = pdPort.Text;
                Log.Stop();

                _activeProfile = Database.ReadProfile(name);
                Database.SaveAsActiveProfile(name);

                lbInput.BackColor = _activeProfile.displayOptions.inputBackground;
                lbInput.Font = _activeProfile.displayOptions.inputFont;
                _inputBackBrush = new SolidBrush(_activeProfile.displayOptions.inputBackground);
                _inputDefaultFrontBrush = new SolidBrush(_activeProfile.displayOptions.inputDefaultForeground);

                lbOutput.BackColor = _activeProfile.displayOptions.outputBackground;
                lbOutput.Font = _activeProfile.displayOptions.outputFont;
                _outputBackBrush = new SolidBrush(_activeProfile.displayOptions.outputBackground);
                _outputFrontBrush = Brushes.Black;

                for (int f = 0; f < _filterFrontBrush.Length; f++)
                {
                    _filterFrontBrush[f] = new SolidBrush(_activeProfile.displayOptions.filter[f].foreColor);
                    _filterBackBrush[f] = new SolidBrush(_activeProfile.displayOptions.filter[f].backColor);
                    _filterMacro[f] = _activeProfile.displayOptions.filter[f].macro;
                }

                btnProfile.Text = _activeProfile.name;
                cbStayOnTop.Checked = _activeProfile.stayontop;

                cbSendCR.Checked = _activeProfile.sendCR;
                cbSendLF.Checked = _activeProfile.sendLF;
                cbClearCMD.Checked = _activeProfile.clearCMD;
                cbSendAsIType.Checked = _activeProfile.sendAsIType;

                cbTimestamp.Checked = _activeProfile.embellishments.ShowInputTimestamp;
                cbASCII.Checked = _activeProfile.embellishments.ShowASCII;
                cbHEX.Checked = _activeProfile.embellishments.ShowHEX;
                cbShowCR.Checked = _activeProfile.embellishments.ShowCR;
                cbShowLF.Checked = _activeProfile.embellishments.ShowLF;

                for (int f = 1; f <= 12; f++)
                    dgMacroTable.Rows[0].Cells[f].Style.BackColor = btnGroup1.BackColor;

                for (int r = 0; r <= 4; r++)
                    dgMacroTable.Rows[r].Cells[0].Style.BackColor = btnGroup1.BackColor;

                UnFreeze();
                ShowColumnTitles();
                ShowMacroTitles();
                RefreshPortPulldown();

                // restore the visible port
                int i = pdPort.Items.IndexOf(currentPort);
                if (i >= 0)
                    pdPort.SelectedIndex = i;

                _comms.SetEmbellishments(_activeProfile.embellishments);
            }
        }
        private void TbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                if ((lbInput.TopIndex - 10) > 0)
                    lbInput.TopIndex -= 10;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                if ((lbInput.TopIndex + 10) < lbInput.Items.Count)
                    lbInput.TopIndex += 10;
                else
                    lbInput.TopIndex = lbInput.Items.Count - 1;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (_resetHistoryIndex)
                {
                    if (lbOutput.Items.Count > 0)
                        lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
                    _resetHistoryIndex = false;
                }
                else
                {
                    if (lbOutput.SelectedIndex > 0)
                        lbOutput.SelectedIndex--;
                }
                tbCommand.Focus();
                SendKeys.Send("{RIGHT}");
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_resetHistoryIndex)
                {
                    if (lbOutput.Items.Count > 0)
                        lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
                    _resetHistoryIndex = false;
                }
                else
                {
                    if (lbOutput.SelectedIndex >= 0)
                    {
                        if (lbOutput.SelectedIndex < lbOutput.Items.Count - 1)
                            lbOutput.SelectedIndex++;
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
                _resetHistoryIndex = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                RestartAllMacros();
                tbCommand.Text = string.Empty;
                _typingPaused = false;
                _resetHistoryIndex = true;
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

                        // intercept <Alt>-F4
                        if (index == 39 && row == 4)
                            _interceptAltF4 = true;
                    }

                    Macro mac = _activeProfile.macros[index];
                    mac.uiColumn = column;
                    mac.uiRow = row;
                    DoMacro(index);
                }
                else if (key == (int)Keys.Back || key == (int)Keys.Delete)
                {
                    if (cbSendAsIType.Checked && !_typingPaused)
                    {
                        if (!_typingWarnedAboutDelete && tbCommand.Text.Length > 0)
                        {
                            MessageBox.Show("You cannot retract data already sent out", "DEL and Backspace not allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _typingWarnedAboutDelete = true;
                        }
                    }
                }
            }
            dgMacroTable.ClearSelection();
            tbCommand.Focus();
        }
        private void tbCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSendAsIType.Checked && !_typingPaused)
            {
                char ch = e.KeyChar;
                if (ch >= ' ' && ch <= '~')
                {
                    switch (_typingDollarValueIndex)
                    {
                        default:
                        case 0:
                            if (ch == '$')
                            {
                                _typingDollarValueIndex++;
                            }
                            else
                            {
                                SendDollarString(ch.ToString(), 0);
                            }
                            break;

                        case 1:
                            if (ch == '$')
                            {
                                _typingDollarValueIndex = 0;
                                SendDollarString("$", 0);
                            }
                            else
                            {
                                if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f')
                                {
                                    _typingDollarValueCh1 = ch;
                                    _typingDollarValueIndex++;
                                }
                                else
                                {
                                    string cmd = "$" + ch.ToString();
                                    SendDollarString(cmd, 0);
                                    _typingDollarValueIndex = 0;
                                }
                            }
                            break;

                        case 2:
                            {
                                string cmd = "$" + _typingDollarValueCh1.ToString() + ch.ToString();
                                SendDollarString(cmd, 0);
                                _typingDollarValueIndex = 0;
                            }
                            break;
                    }
                }
                else
                {
                    e.Handled = true;   // this way we stop DEL or BACK from deleting the display
                }
            }
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
            _resetHistoryIndex = true;

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

            if (_freezeTriggeredFromMacro)
            {
                _freezeTriggeredFromMacro = false;
                BtnFreeze_Click(new object(), new EventArgs());
            }

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

                        if (!_cancelRestart && _activeProfile.conOptions.RestartServer)
                            ActionConnect();
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
                        btnConnect.Text = "Dis&connect";
                        btnConnectOptions.Enabled = false;
                        pdPort.Enabled = false;
                        btnRescan.Enabled = false;
                        btnProfile.Enabled = false;
                        btnFile.Enabled = true;
                        btnConnect.BackColor = Color.Lime;
                        LogAdd(Utils.Timestamp() + "{CONNECTED}");
                    }
                    else
                    {
                        if (_state == State.Listening)
                            ActionDisconnect();
                        else
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
            _help.ShowDialog();
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
            btnClear.Enabled = false;
            {
                lbInput.BeginUpdate();
                {
                    _comms.ResetHexColumn();
                    _uiInputQueue.Clear();
                    lbInput.Items.Clear();
                    UnFreeze();
                    UpdateLineCounter();
                    UpdatePendingCounter();
                    lbInput.Items.Add(string.Empty);
                    _lineFinished = false;
                }
                lbInput.EndUpdate();
            }
            btnClear.Enabled = true;
        }

        /* Calculate the 16bit CRC for XMODEM */
        /* buffer = data buffer in the xmodem block */
        /* count = number of data bytes */
        /* offset = where to start the calculation */
        private ushort xmodemCalcrc(byte[] buffer, int offset, int count)
        {
            ushort crc, i;
            int j;
            crc = 0;
            for (j = offset; j < count + offset; j++)
            {
                crc = (ushort)(crc ^ (buffer[j] << 8));
                for (i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) > 0)
                    {
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    }
                    else
                    {
                        crc = (ushort)(crc << 1);
                    }
                }
            }
            return (ushort)(crc & 0xFFFF);
        }

        private const int XMODEM_SOH = 1;
        private const int XMODEM_ACK = 6;
        private const int XMODEM_NAK = 21;
        private const int XMODEM_EOT = 4;

        enum SEND_RESPONSE
        {
            RES_OK = 0,
            RES_TIMEOUT,
            RES_RESEND,
            RES_FAIL,
        };

        private SEND_RESPONSE SendBlock(byte[] block)
        {
            try
            {
                _comms.Write(block);
            }
            catch (Exception)
            {
                return SEND_RESPONSE.RES_FAIL;
            }
            try
            {
                Byte res = _comms.ReadByte();
                while (res == 'C')
                {
                    res = _comms.ReadByte();
                }

                if (res == XMODEM_ACK)
                {
                    return SEND_RESPONSE.RES_OK;
                }
                else if (res == XMODEM_NAK)
                {
                    return SEND_RESPONSE.RES_RESEND;
                }
                else
                {
                    return SEND_RESPONSE.RES_FAIL;
                }
            }
            catch (TimeoutException)
            {
                return SEND_RESPONSE.RES_TIMEOUT;
            }
        }
        private bool XModemSend(byte[] data)
        {
            byte blockCnt = 0;
            byte[] block = new Byte[133];
            int bytesRead;
            int remaining = data.Length;
            int index = 0;
            do
            {
                bytesRead = Math.Min(128, remaining);
                remaining -= bytesRead;

                for (int i = 0; i < bytesRead; i++)
                    block[i + 3] = (byte)(data[index++] & 0xff);
                for (int i = bytesRead; i < 128; i++)
                    block[i + 3] = 0;

                blockCnt++;

                block[0] = XMODEM_SOH;
                block[1] = blockCnt;
                block[2] = (byte)(255 - blockCnt);

                ushort crc = xmodemCalcrc(block, 3, 128);
                block[131] = (Byte)((crc >> 8) & 0xFF);
                block[132] = (Byte)(crc & 0xFF);

                SEND_RESPONSE res = SendBlock(block);
                while (res == SEND_RESPONSE.RES_RESEND)
                    res = SendBlock(block);

                if (res != SEND_RESPONSE.RES_OK)
                    return false;

                lblLineCounter.Text = remaining.ToString();
                progressBar.Value += bytesRead;
                Application.DoEvents();

                if (_abortFileSend)
                {
                    DialogResult yn = MessageBox.Show("Aborting halfway like this could have serious consequences. ARE YOU SURE?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (yn == DialogResult.Yes)
                    {
                        LogAdd("XMODEM file transfer aborted by user");
                        return false;
                    }
                }

            } while (remaining > 0);

            // send EOT
            byte[] eot = new byte[1];
            eot[0] = XMODEM_EOT;
            try
            {
                _comms.Write(eot);
                Byte res = _comms.ReadByte();
                if (res == XMODEM_ACK)
                    return true;
            }
            catch { }
            return false;
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
            fs.cbXmodem.Enabled = (_activeProfile.conOptions.Type == ConOptions.ConType.Serial);
            fs.ShowDialog();
            TopMost = cbStayOnTop.Checked;
            if (fs.Result == DialogResult.OK)
            {
                _lastFileSend = fs.Filename;

                if (fs.XModem)
                {
                    try
                    {
                        byte[] data = System.IO.File.ReadAllBytes(fs.Filename);
                        btnPauseFile.Text = "Pause";
                        lblLineCounter.Text = "0";
                        progressBar.Value = 0;
                        progressBar.Maximum = data.Length;
                        progressBar.Visible = true;
                        btnAbortFile.Visible = true;
                        btnPauseFile.Visible = true;
                        btnPauseFile.Enabled = false;
                        lblLineCounter.Visible = true;
                        SendPanel.Enabled = false;
                        _abortFileSend = false;
                        Application.DoEvents();

                        _comms.FreezeReaderThread();
                        XModemSend(data);
                        _comms.UnFreezeReaderThread();

                    }
                    catch
                    {
                        MessageBox.Show($"The file could not be sent successfully", "Send error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _lastFileSendILD = fs.InterLineDelay;
                    _lastSendCR = fs.SendCR;
                    _lastSendLF = fs.SendLF;

                    try
                    {
                        string data = System.IO.File.ReadAllText(fs.Filename);
                        string[] lines = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        btnPauseFile.Text = "Pause";
                        lblLineCounter.Text = "0";
                        progressBar.Value = 0;
                        progressBar.Maximum = lines.Length;
                        progressBar.Visible = true;
                        btnAbortFile.Visible = true;
                        btnPauseFile.Visible = true;
                        btnPauseFile.Enabled = true;
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
            }
            lblLineCounter.Visible = false;
            btnFile.Enabled = true;
            SendPanel.Enabled = true;
            progressBar.Visible = false;
            btnAbortFile.Visible = false;
            btnPauseFile.Visible = false;
            Application.DoEvents();
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
                string heading = "Give a descriptive name for the new profile";
                FrmProfileName askname = new FrmProfileName("MyProfile", "Add a New Profile", heading);
                DialogResult rc = askname.ShowDialog();
                if (rc == DialogResult.OK)
                {
                    if (!Database.Find(askname.NewName))
                        Database.NewProfile(askname.NewName);

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

        private void StsLogfile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Log.Filename);
            }
            catch 
            { 
            }
        }

        private void LbOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbOutput.SelectedIndex < 0)
                return;

            string line = lbOutput.SelectedItem.ToString();
            string cmd;
            if (Utils.HasTimestamp(line))
            {
                int i = Utils.TimestampLength();
                cmd = line.Substring(i);
            }
            else
                cmd = line;

            if (cmd.EndsWith("$0A"))
                cmd = cmd.Substring(0, cmd.Length - 3);
            if (cmd.EndsWith("$0D"))
                cmd = cmd.Substring(0, cmd.Length - 3);
            tbCommand.Text = cmd;
            _typingPaused = true;

            Application.DoEvents();
            tbCommand.Focus();
            tbCommand.SelectionStart = cmd.Length;
            tbCommand.SelectionLength = 0;
        }
        private void DgMacroTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgMacroTable.Enabled = false;
            dgMacroTable.ClearSelection();
            dgMacroTable.Enabled = true; ;
            tbCommand.Focus();
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

        private void BtnAbortFile_Click(object sender, EventArgs e)
        {
            btnAbortFile.Enabled = false;
            {
                _abortFileSend = true;
                _pauseFileSend = false;
            }
            btnAbortFile.Enabled = true;
            tbCommand.Focus();
        }
        private void BtnPauseFile_Click(object sender, EventArgs e)
        {
            btnPauseFile.Enabled = false;
            {
                _pauseFileSend = !_pauseFileSend;
                btnPauseFile.Text = (_pauseFileSend) ? "GO" : "Pause";
            }
            btnPauseFile.Enabled = true;
            tbCommand.Focus();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnableDeviceSleep();

            if (_interceptAltF4)
            {
                _interceptAltF4 = false;
                e.Cancel = true;
                return;
            }

            try
            {
                _terminateFlag = true;
                SaveThisProfile();

                this.Visible = false;
                Application.DoEvents();

                timer.Stop();
                timer.Enabled = false;

                if (_comms.Connected())
                {
                    _comms.Disconnect();
                    Thread.Sleep(50);
                    lock (_uiInputQueue)
                    {
                        _uiInputQueue.Clear();
                    }
                }

                for (int retry = 0; retry < 2; retry++)
                {
                    for (int t = 0; t < _macroThread.Length; t++)
                        _macroTrigger[t].Set();
                    Application.DoEvents();
                }

                Thread.Sleep(20);
                if (_localThreadCount != 0)
                {
                    for (int t = 0; t < _macroThread.Length; t++)
                    {
                        _macroThread[t].Interrupt();
                        if (_macroThread[t].IsAlive)
                        {
                            _macroThread[t].Abort();
                        }
                    }
                }
                if (_connectThread != null)
                    _connectThread.Join();
            }
            catch { }
        }

        private void lbInput_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            string str = lbInput.Items[e.Index].ToString();

            int offset = 0;
            if (Utils.HasTimestamp(str))
                offset += Utils.TimestampLength();

            e.DrawBackground();
            Graphics g = e.Graphics;

            Filters filters = new Filters();
            int filter = filters.FindApplicableFilter(_activeProfile, str, offset);

            if (filter >= 0)
            {
                g.FillRectangle(_filterBackBrush[filter], e.Bounds);
                e.Graphics.DrawString(str.Substring(Math.Min(hScrollBar.Value, str.Length)), e.Font, _filterFrontBrush[filter], e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                g.FillRectangle(_inputBackBrush, e.Bounds);
                e.Graphics.DrawString(str.Substring(Math.Min(hScrollBar.Value, str.Length)), e.Font, _inputDefaultFrontBrush, e.Bounds, StringFormat.GenericDefault);
            }
        }

        private void lbOutput_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();
            Graphics g = e.Graphics;
            g.FillRectangle(_outputBackBrush, e.Bounds);

            e.Graphics.DrawString(lbOutput.Items[e.Index].ToString(), e.Font, _outputFrontBrush, e.Bounds, StringFormat.GenericDefault);
        }
        private void lbInput_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbInput.SelectedIndex < 0)
                return;

            string line = lbInput.SelectedItem.ToString();
            if (Utils.HasTimestamp(line))
            {
                int i = Utils.TimestampLength();
                tbCommand.Text = line.Substring(i);
            }
            else
                tbCommand.Text = line;
            _typingPaused = true;
            tbCommand.Focus();
        }

        private void lbInput_Resize(object sender, EventArgs e)
        {
            ScrollToBottom();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            stsColumnStart.Text = hScrollBar.Value.ToString();
            lbInput.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnFreeze.Enabled = false;
            btnSearch.Enabled = false;
            {
                if (!_frozen)
                {
                    if (_comms.Connected() && lbInput.Items.Count > 0)
                    {
                        Freeze(lbInput.Items.Count - 1);
                    }
                }

                DialogResult rc = _search.ShowDialog();
                if (rc == DialogResult.OK)
                {
                    int line = -1;
                    for (int i = lbInput.Items.Count - 1; i > 0; i--)
                    {
                        if (_search.IgnoreCase.Checked)
                        {
                            if (lbInput.Items[i].ToString().ToLower().Contains(_search.SearchText.Text.ToLower()))
                            {
                                line = i;
                                break;
                            }
                        }
                        else
                        {
                            if (lbInput.Items[i].ToString().Contains(_search.SearchText.Text))
                            {
                                line = i;
                                break;
                            }
                        }
                    }

                    if (line > -1)
                    {
                        SetSearchText(_search.SearchText.Text);

                        if (line > 5)
                            lbInput.TopIndex = line - 5;
                        else
                            lbInput.TopIndex = 0;
                    }
                    else if (lbInput.Items.Count > 10)
                    {
                        MessageBox.Show("Not found", "String \"" + _search.SearchText.Text + "\"", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            btnSearch.Enabled = true;
            btnFreeze.Enabled = true;
            tbCommand.Focus();
        }

        private void backupTimer_Tick(object sender, EventArgs e)
        {
            backupTimer.Interval = 36000000;    // every hour from now on
            Database.Backup();
        }

        private void btnASCII_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmASCII>().Count() == 1)
                return;

            frmASCII frmASCII = new frmASCII();
            frmASCII.Left = (this.Left + this.Width) - 50;
            frmASCII.Top = this.Top + 100;
            frmASCII.Show();
            tbCommand.Focus();
        }

        private void btnRescan_Click(object sender, EventArgs e)
        {
            pdPort.Enabled = false;
            btnRescan.Enabled = false;
            {
                RefreshPortPulldown();
            }
            btnRescan.Enabled=true;
            pdPort.Enabled = true;
            tbCommand.Focus();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.ExitThread();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            btnFreeze.Enabled = false;
            btnInspect.Enabled = false;
            btnEdit.Enabled = false;
            {
                bool initFreeze = _frozen;
                if (!initFreeze)
                    Freeze(lbInput.Items.Count - 1);

                try
                {
                    string fileName = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Terminal2.txt";
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < lbInput.Items.Count; i++)
                        sb.Append(lbInput.Items[i].ToString() + "\r\n");
                    System.IO.File.WriteAllText(fileName, sb.ToString());
                    System.Diagnostics.Process.Start(fileName);
                }
                catch
                {
                    MessageBox.Show("Could not open the editor.  Make sure you have an editor associated with a .TXT file", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!initFreeze)
                    UnFreeze();
            }
            btnEdit.Enabled = true;
            btnInspect.Enabled = true;
            btnFreeze.Enabled = true;
            tbCommand.Focus();
        }

        private void Inspect(string fileName)
        {
            FrmInspect inspect = new FrmInspect(_filterFrontBrush, _filterBackBrush, _activeProfile);
            inspect.rtb.Font = lbInput.Font;
            inspect.rtb.BackColor = lbInput.BackColor;

            try
            {
                inspect.rtb.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                inspect.rtb.SelectionStart = inspect.rtb.Text.Length;
                inspect.rtb.ScrollToCaret();
                inspect.Text = "Inspector: " + this.Text;
                inspect.Height = (this.Height * 2) / 3;
                inspect.Show();
                inspect.BringToFront();
            }
            catch
            {
                //
            }
        }

        private void BtnInspect_Click(object sender, EventArgs e)
        {
            btnFreeze.Enabled = false;
            btnInspect.Enabled = false;
            btnEdit.Enabled = false;
            {
                bool initFreeze = _frozen;
                if (!initFreeze)
                    Freeze(lbInput.Items.Count - 1);

                string fileName = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Terminal2.txt";
                try
                {
                    StringBuilder sb = new StringBuilder();
                    int N = Math.Min(lbInput.Items.Count, 1000);
                    int start = lbInput.Items.Count - N;
                    for (int i = start; i < start + N; i++)
                        sb.Append(lbInput.Items[i].ToString() + "\r\n");
                    System.IO.File.WriteAllText(fileName, sb.ToString());
                    Inspect(fileName);
                }
                catch
                {
                    MessageBox.Show("Could not open the editor.  Make sure you have an editor associated with a .TXT file", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!initFreeze)
                    UnFreeze();
            }
            btnEdit.Enabled = true;
            btnInspect.Enabled = true;
            btnFreeze.Enabled = true;
            tbCommand.Focus();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            SaveThisProfile();
            FrmProfileDatabase db = new FrmProfileDatabase(_activeProfile.name);
            TopMost = false;
            db.StartOnly = false;
            db.ShowDialog();
            TopMost = cbStayOnTop.Checked;
            if (!db.SelectedProfile.Equals(_activeProfile.name))
                SwitchToProfile(db.SelectedProfile);
            tbCommand.Focus();
        }

        private void btnClearOutput_Click(object sender, EventArgs e)
        {
            btnClearOutput.Enabled = false;
            lbOutput.Items.Clear();
            btnClearOutput.Enabled = true;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            Rectangle rec = Screen.GetWorkingArea(System.Windows.Forms.Cursor.Position);
            this.Width = (rec.Width * 4) / 5;
            this.Height = (rec.Height * 4) / 5;

            this.CenterToScreen();
        }

        private void cbSendAsIType_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSendAsIType.Checked)
            {
                cbClearCMD.Checked = true;
                cbClearCMD.Enabled = false;
            }
            else
            {
                cbClearCMD.Enabled = true;
            }
            tbCommand.Text = string.Empty;
            tbCommand.Focus();
        }

        private void ShowButtonColours(int N)
        {
            for (int f = 1; f <= 12; f++)
                dgMacroTable.Rows[0].Cells[f].Style.BackColor = _btnGroups[N].BackColor;

            for (int r = 0; r <= 4; r++)
                dgMacroTable.Rows[r].Cells[0].Style.BackColor = _btnGroups[N].BackColor;
        }

        private void SetMacroOffset(int N)
        {
            _macroLabel = (char)('A' + N);
            _macroOffset = N * 48;

            for (int f = 1; f <= 12; f++)
            {
                dgMacroTable.Rows[0].Cells[f].Value = $"{_macroLabel}{f}";
            }
            ShowButtonColours(N);
            ShowColumnTitles();
            ShowMacroTitles();
            tbCommand.Focus();
        }

        private void btnGroup1_MouseDown(object sender, MouseEventArgs e)
        {
            SetMacroOffset(0);
        }

        private void btnGroup2_MouseDown(object sender, MouseEventArgs e)
        {
            SetMacroOffset(1);
        }

        private void btnGroup3_MouseDown(object sender, MouseEventArgs e)
        {
            SetMacroOffset(2);
        }

        private void btnGroup4_MouseDown(object sender, MouseEventArgs e)
        {
            SetMacroOffset(3);
        }

        private void cbStayOnTop_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.Button == MouseButtons.Right)
            {
                try
                {
                    string local = Environment.GetEnvironmentVariable("LOCALAPPDATA");
                    if (local == null) return;
                    string dir = local + "\\" + STAY_ALIVE_NAME;
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                        DisableDeviceSleep();
                    }
                    if (Directory.Exists(dir))
                        MessageBox.Show("Long-term testing enabled", "Long-term Testing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch { }
            }
        }

    }
    internal class FlickerFreeListBox : System.Windows.Forms.ListBox
    {
        public FlickerFreeListBox()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            try
            {
                this.ItemHeight = this.FontHeight;
                base.OnDrawItem(e); // owner draw only
            }
            catch
            {
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                this.ItemHeight = this.FontHeight;
                Region iRegion = new Region(e.ClipRectangle);
                e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
                if (this.Items.Count > 0)
                {
                    for (int i = this.Items.Count - 1; i >= 0; i--)
                    {
                        System.Drawing.Rectangle irect = this.GetItemRectangle(i);
                        if (e.ClipRectangle.IntersectsWith(irect))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                            iRegion.Complement(irect);
                        }
                        else if (irect.Top < 0)
                        {
                            break;
                        }
                    }
                }
                base.OnPaint(e);
            }
            catch
            {
            }
        }
    }
}