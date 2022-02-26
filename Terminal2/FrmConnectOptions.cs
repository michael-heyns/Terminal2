﻿using System;
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
            _conOptions.TCPPort = tbTcpListenPort.Text;
            _conOptions.TCPPort = tbTcpConnectPort.Text;
            _conOptions.TCPAdress = tbTcpConnectAddress.Text;
            _conOptions.SerialPort = pdSerialPort.Text;
            _conOptions.Baudrate = pdBaudrate.Text;
            _conOptions.DataBits = pdDataBits.Text;
            _conOptions.Parity = pdParity.Text;
            _conOptions.StopBits = pdStopBits.Text;
            _conOptions.Handshaking = pdHandshaking.Text;

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
            }
        }
        private void FrmConnectOptions_Load(object sender, EventArgs e)
        {
            _conOptions.Modified = false;

            // options --> screen
            tbTcpListenPort.Text = _conOptions.TCPPort;
            tbTcpConnectPort.Text = _conOptions.TCPPort;
            tbTcpConnectAddress.Text = _conOptions.TCPAdress;
            pdSerialPort.Text = _conOptions.SerialPort;
            pdBaudrate.Text = _conOptions.Baudrate;
            pdDataBits.Text = _conOptions.DataBits;
            pdParity.Text = _conOptions.Parity;
            pdStopBits.Text = _conOptions.StopBits;
            pdHandshaking.Text = _conOptions.Handshaking;
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
    }
}