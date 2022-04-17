using System;
using System.Collections;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Xml.Linq;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Terminal
{
    internal class Comms
    {
        private readonly EventQueue _inputQueue = null;

        private readonly Label[] _leds;
        private readonly Label[] _controls;

        private bool _connected = false;
        private string _connectionString = "Disconnected";
        private CommType _comType = CommType.cNONE;
        private SerialPort _com = null;
        private TcpListener _server = null;
        private TcpClient _client = null;
        private Socket _socket = null;
        private Profile _profile;

        private Thread _tcpThread = null;
        private Thread _serialThread = null;
        private static int _localThreadCount = 0;

        private void SerialReaderThread()
        {
            _localThreadCount++;
            try
            {
                while (_connected && _com != null)
                {
                    int count = _com.BytesToRead;
                    if (count > 0)
                    {
                        string str = _com.ReadExisting();
                        _inputQueue.Enqueue(str);
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch { }
            _localThreadCount--;
        }

        private void TCPReaderThread()
        {
            _localThreadCount++;
            try
            {
                while (_connected && _socket != null)
                {
                    int count = DataWaiting();
                    if (count > 0)
                    {
                        string str = string.Empty;
                        byte[] tcp_data = null;
                        int tcp_count = 0;
                        tcp_count = _socket.Available;
                        if (tcp_count > 0)
                        {
                            tcp_data = new byte[tcp_count];
                            _socket.Receive(tcp_data);
                            str = System.Text.Encoding.Default.GetString(tcp_data);
                        }
                        _inputQueue.Enqueue(str);
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch { }
            _localThreadCount--;
        }

        public Comms(Label[] leds, Label[] controls, EventQueue inputQueue)
        {
            _leds = leds;
            _controls = controls;
            _inputQueue = inputQueue;
            foreach (Label led in _leds)
                led.Visible = false;
            foreach (Label control in _controls)
                control.Visible = false;
        }

        private enum CommType
        { cNONE, ctSERIAL, ctSERVER, ctCLIENT };

        public string ConnectionString
        {
            get { return _connectionString; }
        }
        public bool Connect(Profile profile)
        {
            if (_connected)
                return false;

            _profile = profile;
            _connectionString = "Disconnected";
            if (_profile.conOptions.Type == ConOptions.ConType.Serial)
            {
                _comType = CommType.ctSERIAL;
                _com = new SerialPort
                {
                    PortName = _profile.conOptions.SerialPort,
                    BaudRate = Utils.Int(_profile.conOptions.Baudrate),
                    DataBits = Utils.Int(_profile.conOptions.DataBits)
                };
                _com.ReadBufferSize = 10000000;
                _com.WriteBufferSize = 1000000;
                _com.WriteTimeout = 1000;
                _com.ReadTimeout = 1000;
                switch (_profile.conOptions.Parity)
                {
                    default:
                    case "None":
                        _com.Parity = Parity.None;
                        break;

                    case "Even":
                        _com.Parity = Parity.Even;
                        break;

                    case "Odd":
                        _com.Parity = Parity.Odd;
                        break;

                    case "Mark":
                        _com.Parity = Parity.Mark;
                        break;

                    case "Space":
                        _com.Parity = Parity.Space;
                        break;
                }

                switch (_profile.conOptions.StopBits)
                {
                    default:
                    case "1":
                        _com.StopBits = StopBits.One;
                        break;

                    case "1.5":
                        _com.StopBits = StopBits.OnePointFive;
                        break;

                    case "2":
                        _com.StopBits = StopBits.Two;
                        break;
                }

                SetupSerialControls();
                bool SetInitialRTSCTR = false;
                switch (_profile.conOptions.Handshaking)
                {
                    default:
                    case "None":
                        _com.Handshake = Handshake.None;
                        _controls[0].Enabled = true;
                        _controls[1].Enabled = true;
                        SetInitialRTSCTR = true;
                        break;

                    case "RTS/CTS":
                        _com.Handshake = Handshake.RequestToSend;
                        _controls[0].Enabled = false;
                        _controls[1].Enabled = false;
                        break;

                    case "RTS/CTS + Xon/Xoff":
                        _com.Handshake = Handshake.RequestToSendXOnXOff;
                        _controls[0].Enabled = false;
                        _controls[1].Enabled = false;
                        break;

                    case "Xon/Xoff":
                        _com.Handshake = Handshake.XOnXOff;
                        _controls[0].Enabled = true;
                        _controls[1].Enabled = true;
                        SetInitialRTSCTR = false;
                        break;
                }

                if (SetInitialRTSCTR)
                {
                    if (_profile.conOptions.InitialDTR)
                    {
                        _com.DtrEnable = true;
                        _controls[0].BackColor = Color.Lime;
                    }
                    else
                    {
                        _com.DtrEnable = false;
                        _controls[0].BackColor = Color.White;
                    }

                    if (_profile.conOptions.InitialRTS)
                    {
                        _com.RtsEnable = true;
                        _controls[1].BackColor = Color.Lime;
                    }
                    else
                    {
                        _com.RtsEnable = false;
                        _controls[1].BackColor = Color.White;
                    }
                }

                try
                {
                    _com.PinChanged += new SerialPinChangedEventHandler(SerialPinChangeHandler);
                    _com.Open();
                }
                catch
                {
                    _com.PinChanged -= new SerialPinChangedEventHandler(SerialPinChangeHandler);
                }

                if (_com.IsOpen)
                {
                    SetupSerialLEDs();
                    _connectionString = $"Connected to: {_profile.conOptions.SerialPort},{_profile.conOptions.Baudrate},{_profile.conOptions.DataBits},{_profile.conOptions.Parity},{_profile.conOptions.StopBits},{_profile.conOptions.Handshaking}";
                    _connected = true;
                    _serialThread = new Thread(new ThreadStart(SerialReaderThread));
                    _serialThread.Start();
                }
            }
            else if (_profile.conOptions.Type == ConOptions.ConType.TCPServer)
            {
                _comType = CommType.ctSERVER;
                try
                {
                    int port = Utils.Int(_profile.conOptions.TCPListenPort);
                    _server = new TcpListener(IPAddress.Any, port);
                    _server.Start();

                    _client = _server.AcceptTcpClient();
                    string clientEndPoint = _client.Client.RemoteEndPoint.ToString();
                    _socket = _client.Client;
                    _socket.SendTimeout = 200;
                    _connected = true;
                    _tcpThread = new Thread(new ThreadStart(TCPReaderThread));
                    _tcpThread.Start();
                }
                catch
                {
                }
            }
            else if (_profile.conOptions.Type == ConOptions.ConType.TCPClient)
            {
                _comType = CommType.ctCLIENT;
                try
                {
                    int port = Utils.Int(_profile.conOptions.TCPConnectPort);
                    _client = new TcpClient(_profile.conOptions.TCPConnectAdress, port);
                    string clientEndPoint = _client.Client.RemoteEndPoint.ToString();
                    _socket = _client.Client;
                    _socket.SendTimeout = 200;
                    _connected = true;
                    _tcpThread = new Thread(new ThreadStart(TCPReaderThread));
                    _tcpThread.Start();
                }
                catch
                {
                }
            }
            return _connected;
        }

        // always performs a clean close
        private void CloseSerialOnExit()
        {
            _localThreadCount++;
            try
            {
                if (_com.Handshake == Handshake.None)
                {
                    _com.DtrEnable = false;
                    _com.RtsEnable = false;
                }
                _com.DiscardInBuffer();
                _com.DiscardOutBuffer();
                _com.Close();
                _com = null;
            }
            catch { }
            _localThreadCount--;
        }
        public void Disconnect()
        {
            // don't check - always force - might be waiting

            foreach (Label led in _leds)
                led.Visible = false;
            foreach (Label control in _controls)
                control.Visible = false;

            _connected = false;
            _connectionString = "Disconnected";

            Thread.Sleep(10);
            if (_localThreadCount != 0)
            {
                if (_tcpThread != null)
                    _tcpThread.Abort();
                if (_serialThread != null)
                    _serialThread.Abort();
            }

            if (_tcpThread != null)
                _tcpThread.Join();
            if (_serialThread != null)
                _serialThread.Join();

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        if (_localThreadCount != 0)
                            _serialThread.Abort();

                        if (_com != null && _com.IsOpen)
                        {
                            Thread CloseDown = new System.Threading.Thread(new System.Threading.ThreadStart(CloseSerialOnExit));
                            CloseDown.Start();
                        }
                        break;

                    case CommType.ctSERVER:
                        if (_localThreadCount != 0)
                            _tcpThread.Abort();

                        if (_server != null)
                            _server.Stop();

                        if (_socket != null)
                            _socket.Shutdown(SocketShutdown.Both);

                        _socket = null;
                        _server = null;
                        break;

                    case CommType.ctCLIENT:
                        if (_localThreadCount != 0)
                            _tcpThread.Abort();

                        if (_socket != null)
                        {
                            _socket.Shutdown(SocketShutdown.Both);
                            _client.Close();
                        }
                        _socket = null;
                        _server = null;
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        public bool Write(byte[] data)
        {
            if (!_connected)
                return false;

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        _com.Write(data, 0, data.Length);
                        return true;

                    case CommType.ctSERVER:
                    case CommType.ctCLIENT:
                        _socket.Send(data);
                        break;

                    default:
                        break;
                }
            }
            catch { }
            return false;
        }

        private bool SocketOpen(Socket socket)
        {
            if (socket == null)
                return false;

            try
            {
                if (socket.Poll(0, SelectMode.SelectRead))
                {
                    byte[] buff = new byte[1];
                    if (socket.Receive(buff, SocketFlags.Peek) == 0)
                        return false;
                }
                return true;
            }
            catch { }
            return false;
        }

        public bool Connected()
        {
            if (!_connected)
                return false;

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        return _connected;

                    case CommType.ctSERVER:
                    case CommType.ctCLIENT:
                        _connected = SocketOpen(_socket);
                        return _connected;

                    default:
                        break;
                }
            }
            catch { }
            return false;
        }

        public void ControlPressed(int id)
        {
            if (!_connected)
                return;

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        if (id == 0)
                        {
                            if (_controls[0].BackColor == Color.White)
                            {
                                _com.DtrEnable = true;
                                _controls[0].BackColor = Color.Lime;
                            }
                            else
                            {
                                _com.DtrEnable = false;
                                _controls[0].BackColor = Color.White;
                            }
                        }
                        else if (id == 1)
                        {
                            if (_controls[1].BackColor == Color.White)
                            {
                                _com.RtsEnable = true;
                                _controls[1].BackColor = Color.Lime;
                            }
                            else
                            {
                                _com.RtsEnable = false;
                                _controls[1].BackColor = Color.White;
                            }
                        }
                        break;

                    case CommType.ctSERVER:
                        break;

                    case CommType.ctCLIENT:
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }
        private void SerialPinChangeHandler(object sender, SerialPinChangedEventArgs e)
        {
            if (e.EventType == SerialPinChange.CtsChanged)
            {
                if (_leds[0].BackColor == Color.White)
                    _leds[0].BackColor = Color.Lime;
                else
                    _leds[0].BackColor = Color.White;
            }
            else if (e.EventType == SerialPinChange.DsrChanged)
            {
                if (_leds[1].BackColor == Color.White)
                    _leds[1].BackColor = Color.Lime;
                else
                    _leds[1].BackColor = Color.White;
            }
            else if (e.EventType == SerialPinChange.CDChanged)
            {
                if (_leds[2].BackColor == Color.White)
                    _leds[2].BackColor = Color.Lime;
                else
                    _leds[2].BackColor = Color.White;
            }
            else if (e.EventType == SerialPinChange.Ring)
            {
                if (_leds[3].BackColor == Color.White)
                    _leds[3].BackColor = Color.Lime;
                else
                    _leds[3].BackColor = Color.White;
            }
        }

        private void SetupSerialControls()
        {
            _controls[0].Text = "DTR";
            _controls[0].Visible = true;
            _controls[0].BackColor = Color.White;

            _controls[1].Text = "RTS";
            _controls[1].Visible = true;
            _controls[1].BackColor = Color.White;
        }

        private void SetupSerialLEDs()
        {
            _leds[0].Text = "CTS";
            _leds[0].Visible = true;
            if (_com.CtsHolding)
                _leds[0].BackColor = Color.Lime;
            else
                _leds[0].BackColor = Color.White;

            _leds[1].Text = "DSR";
            _leds[1].Visible = true;
            if (_com.DsrHolding)
                _leds[1].BackColor = Color.Lime;
            else
                _leds[1].BackColor = Color.White;

            _leds[2].Text = "CD";
            _leds[2].Visible = true;
            if (_com.CDHolding)
                _leds[2].BackColor = Color.Lime;
            else
                _leds[2].BackColor = Color.White;

            _leds[3].Text = "RI";
            _leds[3].Visible = true;
            _leds[3].BackColor = Color.White;
        }
 
        public int DataWaiting()
        {
            if (!_connected)
                return 0;

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        return _com.BytesToRead;

                    case CommType.ctSERVER:
                    case CommType.ctCLIENT:
                        return _socket.Available;

                    default:
                        break;
                }
            }
            catch { }
            return 0;
        }
        public byte[] Read()
        {
            if (!_connected)
                return null;

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        int ser_count = _com.BytesToRead;
                        byte[] ser_data = new byte[ser_count];
                        _com.Read(ser_data, 0, ser_count);
                        return ser_data;

                    case CommType.ctSERVER:
                    case CommType.ctCLIENT:
                        byte[] tcp_data = null;
                        int tcp_count = 0;
                        tcp_count = _socket.Available;
                        if (tcp_count > 0)
                        {
                            tcp_data = new byte[tcp_count];
                            _socket.Receive(tcp_data);
                        }
                        return tcp_data;

                    default:
                        break;
                }
            }
            catch { }
            return null;
        }

        public void FillPortNames(ComboBox cb)
        {
            cb.Items.Clear();
            string[] list = SerialPort.GetPortNames();
            foreach (string name in list)
                cb.Items.Add(name);
            cb.Items.Add("TCP Client");
            cb.Items.Add("TCP Server");
        }
        public string[] GetSerialPortNames()
        {
            return SerialPort.GetPortNames();
        }
    }
}