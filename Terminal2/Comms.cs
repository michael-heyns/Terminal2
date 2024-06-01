/* 
 * Terminal2
 *
 * Copyright © 2022-23-23 Michael Heyns
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

// see https://docs.microsoft.com/en-us/dotnet/api/system.io.ports.serialport.close?view=dotnet-plat-ext-6.0

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Xml.Linq;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Text;

namespace Terminal
{
    internal class Comms
    {
        private static EventQueue _inputQueue = null;

        private static Label[] _leds;
        private static Label[] _controls;

        private static SerialPort _com = null;

        private static bool _connected = false;
        private static string _connectionString = "Disconnected";
        private static CommType _comType = CommType.cNONE;
        private static TcpListener _server = null;
        private static Socket _socket = null;
        private static Profile _profile;
        private static Embellishments _embellishments;

        private static Thread _tcpThread = null;
        private static Thread _serialThread = null;

        private static bool _halfCRLF = false;
        private static bool _showTimestamp = false;
        private static int _hexColumn = 0;

        private static bool _showServerFailure = false;
        private static bool _suspendReadThread = false;

        public static bool _terminateFlag = false;

        private void Enqueue(byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            // only HEX
            if (!_embellishments.ShowASCII && _embellishments.ShowHEX)
            {
                foreach (byte b in data)
                {
                    if (_hexColumn == 0 && _embellishments.ShowTimestamp)
                        sb.Append(Utils.Timestamp());

                    sb.Append($"{b:X2} ");

                    _hexColumn++;
                    if (_hexColumn == 8 || _hexColumn == 16 || _hexColumn == 24)
                    {
                        sb.Append(" - ");
                    }
                    else if (_hexColumn == 32)
                    {
                        sb.Append("\n");
                        _hexColumn = 0;
                    }
                }
            }

            // ASCI with or without HEX
            else
            {
                foreach (byte b in data)
                {
                    char c = (char)b;

                    // if we did not complete the previous CRLF, do it now
                    if (_halfCRLF)
                    {
                        switch (c)
                        {
                            case '\n':
                                _showTimestamp = false;
                                break;

                            default:
                                // it was NOT a CR-LF sequence - so treat it as an EOL
                                sb.Append('\n');
                                if (_embellishments.ShowTimestamp)
                                    _showTimestamp = true;
                                break;
                        }
                        _halfCRLF = false;
                    }

                    // add timestamp
                    if (_showTimestamp)
                    {
                        sb.Append(Utils.Timestamp());
                        _showTimestamp = false;
                    }

                    // deal with CR only
                    switch (c)
                    {
                        case '\r':
                            // add the ASCII version of the character
                            if (_embellishments.ShowCR)
                                sb.Append("{CR}");

                            // add the HEX version of the character
                            if (_embellishments.ShowHEX)
                            {
                                sb.Append($"{b:X2} ");
                            }

                            _halfCRLF = true;
                            break;

                        case '\n':
                            // add the ASCII version of the character
                            if (_embellishments.ShowLF)
                                sb.Append("{LF}");

                            // add the HEX version of the character
                            if (_embellishments.ShowHEX)
                            {
                                sb.Append($"{b:X2} ");
                            }

                            sb.Append("\n");
                            _halfCRLF = false;
                            if (_embellishments.ShowTimestamp)
                                _showTimestamp = true;
                            break;

                        default:
                            // add the ASCII version of the character
                            sb.Append(c);

                            // add the HEX version of the character
                            if (_embellishments.ShowHEX)
                            {
                                sb.Append($"{b:X2} ");
                            }

                            // back to normal
                            _halfCRLF = false;
                            break;
                    }
                }
            }
            _inputQueue.Enqueue(sb.ToString());

        }
        private void SerialReaderThread()
        {
            if (_com == null)
                return;

            try
            {
                // do the work
                if (_com.IsOpen)
                {
                    _terminateFlag = false;
                    _com.PinChanged += new SerialPinChangedEventHandler(SerialPinChangeHandler);
                    while (_com.IsOpen && !_terminateFlag)
                    {
                        int count = _com.BytesToRead;
                        if (count > 0)
                        {
                            byte[] ser_data = new byte[count];
                            _com.Read(ser_data, 0, count);
                            Enqueue(ser_data);
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }

                        while (_suspendReadThread)
                            Thread.Sleep(10);
                    }
                }
            }
            catch { }
            _connected = false;
        }

        private void TCPReaderThread()
        {
            try
            {
                while (SocketOpen(_socket))
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
                            if (!_socket.Connected) break;
                            _socket.Receive(tcp_data);
                            Enqueue(tcp_data);
                        }
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            catch { }
            _connected = false;
        }

        public Comms(Label[] leds, Label[] controls, EventQueue inputQueue)
        {
            _leds = leds;
            _controls = controls;
            _inputQueue = inputQueue;
            foreach (Label led in _leds)
            {
                led.Text = "-";
                led.Visible = true;
                led.Enabled = false;
                led.BackColor = Color.LightGray;
            }
            foreach (Label control in _controls)
            {
                control.Text = "-";
                control.Visible = true;
                control.Enabled = false;
                control.BackColor = Color.LightGray;
            }
        }

        ~Comms()
        {
        }

        public void SetEmbellishments(Embellishments embellishments)
        {
            _embellishments = embellishments;
            _showTimestamp = _embellishments.ShowTimestamp;
        }

        private enum CommType
        { cNONE, ctSERIAL, ctSERVER, ctCLIENT };

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public void ResetHexColumn()
        {
            _hexColumn = 0;
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

                _com = new SerialPort();
                _com.PortName = _profile.conOptions.SerialPort;
                _com.BaudRate = Utils.Int(_profile.conOptions.Baudrate);
                _com.DataBits = Utils.Int(_profile.conOptions.DataBits);
                _com.ReadBufferSize = 100000;
                _com.WriteBufferSize = 100000;
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

                // set RTS
                {
                    bool SetInitialRTS = false;
                    switch (_profile.conOptions.Handshaking)
                    {
                        default:
                        case "None":
                            _com.Handshake = Handshake.None;
                            _controls[1].Enabled = true;
                            SetInitialRTS = true;
                            break;

                        case "RTS/CTS":
                            _com.Handshake = Handshake.RequestToSend;
                            _controls[1].Enabled = false;
                            break;

                        case "RTS/CTS + Xon/Xoff":
                            _com.Handshake = Handshake.RequestToSendXOnXOff;
                            _controls[1].Enabled = false;
                            break;

                        case "Xon/Xoff":
                            _com.Handshake = Handshake.XOnXOff;
                            _controls[1].Enabled = true;
                            SetInitialRTS = true;
                            break;
                    }

                    if (SetInitialRTS)
                    {
                        if (_profile.conOptions.InitialRTS)
                            _com.RtsEnable = true;
                        else
                            _com.RtsEnable = false;
                    }

                    // set initial DTR
                    if (_profile.conOptions.InitialDTR)
                        _com.DtrEnable = true;
                    else
                        _com.DtrEnable = false;
                }

                // open the port
                _connected = false;
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        _com.Open();
                        if (_com.IsOpen)
                        {
                            _connected = true;
                            break;
                        }
                    }
                    catch { }
                }

                // start the thread
                if (_connected)
                {
                    _serialThread = new Thread(new ThreadStart(SerialReaderThread));
                    _serialThread.Start();
                    _connectionString = $"Connected to: {_profile.conOptions.SerialPort},{_profile.conOptions.Baudrate},{_profile.conOptions.DataBits},{_profile.conOptions.Parity},{_profile.conOptions.StopBits},{_profile.conOptions.Handshaking}";
                    SetupOutgoingLEDs();
                    SetupIncomingLEDs();
                    return true;
                }

                // connection failed
                Disconnect();
                return false;
            }
            else if (_profile.conOptions.Type == ConOptions.ConType.TCPServer)
            {
                _comType = CommType.ctSERVER;
                try
                {
                    int port = Utils.Int(_profile.conOptions.TCPListenPort);
                    _server = new TcpListener(IPAddress.Any, port);
                    _server.Start();

                    _showServerFailure = true;
                    TcpClient _client = _server.AcceptTcpClient();
                    string clientEndPoint = _client.Client.RemoteEndPoint.ToString();
                    _socket = _client.Client;
                    _socket.SendTimeout = 200;
                    _connected = true;
                    _tcpThread = new Thread(new ThreadStart(TCPReaderThread));
                    _tcpThread.Start();
                }
                catch
                {
                    if (_showServerFailure)
                        MessageBox.Show("The server cannot start.\nAnother server may be listening on the same port.", "Cannot Start Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else if (_profile.conOptions.Type == ConOptions.ConType.TCPClient)
            {
                _comType = CommType.ctCLIENT;
                try
                {
                    int port = Utils.Int(_profile.conOptions.TCPConnectPort);
                    TcpClient _client = new TcpClient(_profile.conOptions.TCPConnectAdress, port);
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

        public void FreezeReaderThread()
        {
            if (_profile.conOptions.Type != ConOptions.ConType.Serial)
                return;

            if (_com.IsOpen && _connected)
            {
                _suspendReadThread = true;
                Thread.Sleep(1);
            }
        }
        public void UnFreezeReaderThread()
        {
            if (_profile.conOptions.Type != ConOptions.ConType.Serial)
                return;

            if (_com.IsOpen && _connected)
            {
                _suspendReadThread = false;
                Thread.Sleep(1);
            }
        }

        public void Disconnect()
        {
            // don't check - always force - might be waiting

            foreach (Label led in _leds)
            {
                led.Text = "-";
                led.Visible = true;
                led.Enabled = false;
                led.BackColor = Color.LightGray;
            }
            foreach (Label control in _controls)
            {
                control.Text = "-";
                control.Visible = true;
                control.Enabled = false;
                control.BackColor = Color.LightGray;
            }
            Application.DoEvents();

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        _com.PinChanged -= new SerialPinChangedEventHandler(SerialPinChangeHandler);
                        Stream s = _com.BaseStream;
                        s.Flush();
                        s.Close();
                        _com.DtrEnable = false;
                        _com.RtsEnable = false;
                        _com.Close();
                        if (_serialThread != null)
                        {
                            _terminateFlag = true;
                            while (_serialThread.ThreadState == ThreadState.Running)
                                Thread.Sleep(20);
                            Thread.Sleep(20);
                            _serialThread.Abort();
                            Thread.Sleep(20);
                            _serialThread.Join();
                            _serialThread = null;
                        }
                        _com = null;
                        _connected = false;
                        break;

                    case CommType.ctSERVER:
                        _showServerFailure = false;
                        if (_socket != null)
                        {
                            try
                            {
                                _socket.Shutdown(SocketShutdown.Both);
                            }
                            finally
                            {
                                _socket.Close();
                            }
                        }

                        if (_tcpThread != null)
                        {
                            while (_tcpThread.ThreadState == ThreadState.Running)
                                Thread.Sleep(20);
                            Thread.Sleep(20);
                            _tcpThread.Abort();
                            Thread.Sleep(20);
                            _tcpThread.Join();
                            _tcpThread = null;
                        }

                        if (_server != null)
                            _server.Stop();

                        _socket = null;
                        _server = null;
                        _connected = false;
                        _connectionString = "Disconnected";

                        break;

                    case CommType.ctCLIENT:
                        if (_socket != null)
                        {
                            try
                            {
                                _socket.Shutdown(SocketShutdown.Both);
                            }
                            finally
                            {
                                _socket.Close();
                            }
                        }

                        if (_tcpThread != null)
                        {
                            while (_tcpThread.ThreadState == ThreadState.Running)
                                Thread.Sleep(20);
                            _tcpThread.Join();
                            _tcpThread = null;
                        }

                        _socket = null;
                        _server = null;
                        _connected = false;
                        _connectionString = "Disconnected";

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

        private void SetupOutgoingLEDs()
        {
            // set DTR LED
            _controls[0].Text = "DTR";
            _controls[0].Enabled = true;
            if (_profile.conOptions.InitialDTR)
                _controls[0].BackColor = Color.Lime;
            else
                _controls[0].BackColor = Color.White;

            // set RTS LED
            _controls[1].Text = "RTS";
            _controls[1].Enabled = true;
            if (_profile.conOptions.InitialRTS)
                _controls[1].BackColor = Color.Lime;
            else
                _controls[1].BackColor = Color.White;
        }

        private void SetupIncomingLEDs()
        {
            _leds[0].Text = "CTS";
            _leds[0].Enabled = true;
            if (_com.CtsHolding)
                _leds[0].BackColor = Color.Lime;
            else
                _leds[0].BackColor = Color.LightGray;

            _leds[1].Text = "DSR";
            _leds[1].Enabled = true;
            if (_com.DsrHolding)
                _leds[1].BackColor = Color.Lime;
            else
                _leds[1].BackColor = Color.LightGray;

            _leds[2].Text = "CD";
            _leds[2].Enabled = true;
            if (_com.CDHolding)
                _leds[2].BackColor = Color.Lime;
            else
                _leds[2].BackColor = Color.LightGray;

            _leds[3].Text = "RI";
            _leds[3].Enabled = true;
            _leds[3].BackColor = Color.LightGray;
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
                        int tcp_count = _socket.Available;
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

        public byte ReadByte()
        {
            if (!_connected)
                return 0;

            try
            {
                switch (_comType)
                {
                    case CommType.ctSERIAL:
                        byte[] ser_data = new byte[1];
                        _com.Read(ser_data, 0, 1);
                        return ser_data[0];

                    case CommType.ctSERVER:
                    case CommType.ctCLIENT:
                        byte[] tcp_data = null;
                        int tcp_count = _socket.Available;
                        if (tcp_count > 0)
                        {
                            tcp_data = new byte[1];
                            _socket.Receive(tcp_data, 1, SocketFlags.None);
                        }
                        return tcp_data[0];

                    default:
                        break;
                }
            }
            catch { }
            return 0;
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