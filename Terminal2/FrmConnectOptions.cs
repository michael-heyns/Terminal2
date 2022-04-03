using System;
using System.IO.Ports;
using System.Windows.Forms;

using Microsoft.Extensions.Options;

namespace Terminal
{
    public partial class FrmConnectOptions : Form
    {
        private readonly ConOptions _conOptions;

        public FrmConnectOptions(ConOptions options, string[] portList)
        {
            InitializeComponent();
            _conOptions = options;

            pdSerialPort.Items.Clear();
            foreach (string p in portList)
                pdSerialPort.Items.Add(p);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // screen --> options
            _conOptions.TCPListenPort = tbTcpListenPort.Text;
            _conOptions.TCPConnectPort = tbTcpConnectPort.Text;
            _conOptions.TCPConnectAdress = tbTcpConnectAddress.Text;
            _conOptions.SerialPort = pdSerialPort.Text;
            _conOptions.Baudrate = pdBaudrate.Text;
            _conOptions.DataBits = pdDataBits.Text;
            _conOptions.Parity = pdParity.Text;
            _conOptions.StopBits = pdStopBits.Text;
            _conOptions.Handshaking = pdHandshaking.Text;
            _conOptions.InitialDTR = cbInitialDTR.Checked;
            _conOptions.InitialRTS = cbInitialRTS.Checked;

            if (cbTcpServer.Checked)
            {
                _conOptions.Type = ConOptions.ConType.TCPServer;
            }
            else if (cbTcpClient.Checked)
            {
                _conOptions.Type = ConOptions.ConType.TCPClient;
            }
            else
            {
                _conOptions.Type = ConOptions.ConType.Serial;
            }

            _conOptions.Modified = true;
            Close();
        }

        private void EnableOptionFields()
        {
            tbTcpListenPort.Enabled = false;
            tbTcpConnectPort.Enabled = false;
            tbTcpConnectAddress.Enabled = false;
            pdSerialPort.Enabled = false;
            pdBaudrate.Enabled = false;
            pdDataBits.Enabled = false;
            pdParity.Enabled = false;
            pdStopBits.Enabled = false;
            pdHandshaking.Enabled = false;
            cbInitialDTR.Enabled = false;
            cbInitialRTS.Enabled = false;

            if (_conOptions.Type == ConOptions.ConType.TCPServer)
            {
                tbTcpListenPort.Enabled = true;
            }
            else if (_conOptions.Type == ConOptions.ConType.TCPClient)
            {
                tbTcpConnectPort.Enabled = true;
                tbTcpConnectAddress.Enabled = true;
            }
            else
            {
                pdSerialPort.Enabled = true;
                pdBaudrate.Enabled = true;
                pdDataBits.Enabled = true;
                pdParity.Enabled = true;
                pdStopBits.Enabled = true;
                pdHandshaking.Enabled = true;
                cbInitialDTR.Enabled = true;
                cbInitialRTS.Enabled = true;
            }
        }
        private void FrmConnectOptions_Load(object sender, EventArgs e)
        {
            _conOptions.Modified = false;

            // options --> screen
            tbTcpListenPort.Text = _conOptions.TCPListenPort;
            tbTcpConnectPort.Text = _conOptions.TCPConnectPort;
            tbTcpConnectAddress.Text = _conOptions.TCPConnectAdress;
            pdSerialPort.Text = _conOptions.SerialPort;
            pdBaudrate.Text = _conOptions.Baudrate;
            pdDataBits.Text = _conOptions.DataBits;
            pdParity.Text = _conOptions.Parity;
            pdStopBits.Text = _conOptions.StopBits;
            pdHandshaking.Text = _conOptions.Handshaking;
            cbInitialDTR.Checked = _conOptions.InitialDTR;
            cbInitialRTS.Checked = _conOptions.InitialRTS;

            switch (_conOptions.Type)
            {
                default:
                case ConOptions.ConType.Serial:
                    cbSerial.Checked = true;
                    break;

                case ConOptions.ConType.TCPClient:
                    cbTcpClient.Checked = true;
                    break;

                case ConOptions.ConType.TCPServer:
                    cbTcpServer.Checked = true;
                    break;
            }
        }

        private void FrmConnectOptions_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void OptSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSerial.Checked)
            {
                _conOptions.Type = ConOptions.ConType.Serial;
                EnableOptionFields();
            }
        }

        private void OptTcpClient_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTcpClient.Checked)
            {
                _conOptions.Type = ConOptions.ConType.TCPClient;
                EnableOptionFields();
            }
        }

        private void OptTcpServer_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTcpServer.Checked)
            {
                _conOptions.Type = ConOptions.ConType.TCPServer;
                EnableOptionFields();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PdHandshaking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pdHandshaking.SelectedIndex == 0 || pdHandshaking.SelectedIndex == 2)
            {
                cbInitialDTR.Enabled = true;
                cbInitialRTS.Enabled = true;
            }
            else
            {
                cbInitialDTR.Enabled = false;
                cbInitialRTS.Enabled = false;
            }
        }
    }
}