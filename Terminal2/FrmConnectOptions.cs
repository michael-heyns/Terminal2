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


using System;
using System.IO.Ports;
using System.Windows.Forms;

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
            _conOptions.RestartServer= cbRestartServer.Checked;

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
            cbRestartServer.Enabled = false;

            if (_conOptions.Type == ConOptions.ConType.TCPServer)
            {
                tbTcpListenPort.Enabled = true;
                cbRestartServer.Enabled = true;
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
            cbRestartServer.Checked = _conOptions.RestartServer;

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

            if (pdHandshaking.SelectedIndex == 0 || pdHandshaking.SelectedIndex == 2)
            {
                cbInitialRTS.Enabled = true;
            }
            else
            {
                cbInitialRTS.Enabled = false;
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
                cbInitialRTS.Enabled = true;
            }
            else
            {
                cbInitialRTS.Enabled = false;
            }
        }
    }
}