/* 
 * Terminal2
 *
 * Copyright © 2022 Michael Heyns
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


namespace Terminal
{
    partial class FrmConnectOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnectOptions));
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pdDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pdHandshaking = new System.Windows.Forms.ComboBox();
            this.pdStopBits = new System.Windows.Forms.ComboBox();
            this.pdParity = new System.Windows.Forms.ComboBox();
            this.pdBaudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pdSerialPort = new System.Windows.Forms.ComboBox();
            this.cbSerial = new System.Windows.Forms.RadioButton();
            this.tbTcpConnectPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTcpConnectAddress = new System.Windows.Forms.TextBox();
            this.cbTcpClient = new System.Windows.Forms.RadioButton();
            this.tbTcpListenPort = new System.Windows.Forms.TextBox();
            this.cbTcpServer = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbInitialDTR = new System.Windows.Forms.CheckBox();
            this.cbInitialRTS = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Location = new System.Drawing.Point(878, 138);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 20);
            this.label8.TabIndex = 19;
            this.label8.Text = "Handshaking";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Location = new System.Drawing.Point(736, 138);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Stop bits";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Location = new System.Drawing.Point(644, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Parity";
            // 
            // pdDataBits
            // 
            this.pdDataBits.Cursor = System.Windows.Forms.Cursors.Default;
            this.pdDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pdDataBits.Enabled = false;
            this.pdDataBits.FormattingEnabled = true;
            this.pdDataBits.Items.AddRange(new object[] {
            "6",
            "7",
            "8"});
            this.pdDataBits.Location = new System.Drawing.Point(528, 162);
            this.pdDataBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdDataBits.Name = "pdDataBits";
            this.pdDataBits.Size = new System.Drawing.Size(72, 28);
            this.pdDataBits.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Location = new System.Drawing.Point(528, 138);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Data bits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Location = new System.Drawing.Point(397, 138);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Baud rate";
            // 
            // pdHandshaking
            // 
            this.pdHandshaking.Cursor = System.Windows.Forms.Cursors.Default;
            this.pdHandshaking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pdHandshaking.Enabled = false;
            this.pdHandshaking.FormattingEnabled = true;
            this.pdHandshaking.Items.AddRange(new object[] {
            "None",
            "RTS/CTS",
            "Xon/Off",
            "RTS/CTS + Xon/Off"});
            this.pdHandshaking.Location = new System.Drawing.Point(818, 162);
            this.pdHandshaking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdHandshaking.Name = "pdHandshaking";
            this.pdHandshaking.Size = new System.Drawing.Size(204, 28);
            this.pdHandshaking.TabIndex = 12;
            this.pdHandshaking.SelectedIndexChanged += new System.EventHandler(this.PdHandshaking_SelectedIndexChanged);
            // 
            // pdStopBits
            // 
            this.pdStopBits.Cursor = System.Windows.Forms.Cursors.Default;
            this.pdStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pdStopBits.Enabled = false;
            this.pdStopBits.FormattingEnabled = true;
            this.pdStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.pdStopBits.Location = new System.Drawing.Point(738, 162);
            this.pdStopBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdStopBits.Name = "pdStopBits";
            this.pdStopBits.Size = new System.Drawing.Size(72, 28);
            this.pdStopBits.TabIndex = 11;
            // 
            // pdParity
            // 
            this.pdParity.Cursor = System.Windows.Forms.Cursors.Default;
            this.pdParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pdParity.Enabled = false;
            this.pdParity.FormattingEnabled = true;
            this.pdParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.pdParity.Location = new System.Drawing.Point(608, 162);
            this.pdParity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdParity.Name = "pdParity";
            this.pdParity.Size = new System.Drawing.Size(120, 28);
            this.pdParity.TabIndex = 10;
            // 
            // pdBaudrate
            // 
            this.pdBaudrate.Cursor = System.Windows.Forms.Cursors.Default;
            this.pdBaudrate.Enabled = false;
            this.pdBaudrate.FormattingEnabled = true;
            this.pdBaudrate.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "230400",
            "256000",
            "460800",
            "921600"});
            this.pdBaudrate.Location = new System.Drawing.Point(368, 162);
            this.pdBaudrate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdBaudrate.Name = "pdBaudrate";
            this.pdBaudrate.Size = new System.Drawing.Size(150, 28);
            this.pdBaudrate.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Location = new System.Drawing.Point(336, 167);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "at";
            // 
            // pdSerialPort
            // 
            this.pdSerialPort.Cursor = System.Windows.Forms.Cursors.Default;
            this.pdSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pdSerialPort.Enabled = false;
            this.pdSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pdSerialPort.FormattingEnabled = true;
            this.pdSerialPort.Location = new System.Drawing.Point(206, 162);
            this.pdSerialPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pdSerialPort.Name = "pdSerialPort";
            this.pdSerialPort.Size = new System.Drawing.Size(118, 28);
            this.pdSerialPort.TabIndex = 7;
            // 
            // cbSerial
            // 
            this.cbSerial.AutoSize = true;
            this.cbSerial.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbSerial.Location = new System.Drawing.Point(20, 164);
            this.cbSerial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(178, 24);
            this.cbSerial.TabIndex = 6;
            this.cbSerial.Text = "Serial connection to ";
            this.cbSerial.UseVisualStyleBackColor = true;
            this.cbSerial.CheckedChanged += new System.EventHandler(this.OptSerial_CheckedChanged);
            // 
            // tbTcpConnectPort
            // 
            this.tbTcpConnectPort.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbTcpConnectPort.Enabled = false;
            this.tbTcpConnectPort.Location = new System.Drawing.Point(808, 91);
            this.tbTcpConnectPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTcpConnectPort.Name = "tbTcpConnectPort";
            this.tbTcpConnectPort.Size = new System.Drawing.Size(100, 26);
            this.tbTcpConnectPort.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(770, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // tbTcpConnectAddress
            // 
            this.tbTcpConnectAddress.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbTcpConnectAddress.Enabled = false;
            this.tbTcpConnectAddress.Location = new System.Drawing.Point(544, 91);
            this.tbTcpConnectAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTcpConnectAddress.Name = "tbTcpConnectAddress";
            this.tbTcpConnectAddress.Size = new System.Drawing.Size(202, 26);
            this.tbTcpConnectAddress.TabIndex = 3;
            // 
            // cbTcpClient
            // 
            this.cbTcpClient.AutoSize = true;
            this.cbTcpClient.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbTcpClient.Location = new System.Drawing.Point(19, 93);
            this.cbTcpClient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTcpClient.Name = "cbTcpClient";
            this.cbTcpClient.Size = new System.Drawing.Size(514, 24);
            this.cbTcpClient.TabIndex = 2;
            this.cbTcpClient.Text = "TCP socket connection as CLIENT connecting to master at address ";
            this.cbTcpClient.UseVisualStyleBackColor = true;
            this.cbTcpClient.CheckedChanged += new System.EventHandler(this.OptTcpClient_CheckedChanged);
            // 
            // tbTcpListenPort
            // 
            this.tbTcpListenPort.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbTcpListenPort.Enabled = false;
            this.tbTcpListenPort.Location = new System.Drawing.Point(434, 56);
            this.tbTcpListenPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTcpListenPort.Name = "tbTcpListenPort";
            this.tbTcpListenPort.Size = new System.Drawing.Size(100, 26);
            this.tbTcpListenPort.TabIndex = 1;
            // 
            // cbTcpServer
            // 
            this.cbTcpServer.AutoSize = true;
            this.cbTcpServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbTcpServer.Location = new System.Drawing.Point(19, 58);
            this.cbTcpServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTcpServer.Name = "cbTcpServer";
            this.cbTcpServer.Size = new System.Drawing.Size(406, 24);
            this.cbTcpServer.TabIndex = 0;
            this.cbTcpServer.TabStop = true;
            this.cbTcpServer.Text = "TCP socket connection as SERVER listening on port";
            this.cbTcpServer.UseVisualStyleBackColor = true;
            this.cbTcpServer.CheckedChanged += new System.EventHandler(this.OptTcpServer_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(910, 225);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 35);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "&Apply";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(365, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 29);
            this.label9.TabIndex = 22;
            this.label9.Text = "Connection Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(366, 205);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Initial ON state of ";
            // 
            // cbInitialDTR
            // 
            this.cbInitialDTR.AutoSize = true;
            this.cbInitialDTR.Location = new System.Drawing.Point(505, 204);
            this.cbInitialDTR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbInitialDTR.Name = "cbInitialDTR";
            this.cbInitialDTR.Size = new System.Drawing.Size(68, 24);
            this.cbInitialDTR.TabIndex = 24;
            this.cbInitialDTR.Text = "DTR";
            this.cbInitialDTR.UseVisualStyleBackColor = true;
            // 
            // cbInitialRTS
            // 
            this.cbInitialRTS.AutoSize = true;
            this.cbInitialRTS.Location = new System.Drawing.Point(574, 204);
            this.cbInitialRTS.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbInitialRTS.Name = "cbInitialRTS";
            this.cbInitialRTS.Size = new System.Drawing.Size(67, 24);
            this.cbInitialRTS.TabIndex = 25;
            this.cbInitialRTS.Text = "RTS";
            this.cbInitialRTS.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(649, 205);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 20);
            this.label10.TabIndex = 26;
            this.label10.Text = "before the port opens";
            // 
            // FrmConnectOptions
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 286);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbInitialRTS);
            this.Controls.Add(this.cbInitialDTR);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbTcpServer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbTcpListenPort);
            this.Controls.Add(this.pdDataBits);
            this.Controls.Add(this.cbTcpClient);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbTcpConnectAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTcpConnectPort);
            this.Controls.Add(this.pdHandshaking);
            this.Controls.Add(this.cbSerial);
            this.Controls.Add(this.pdStopBits);
            this.Controls.Add(this.pdSerialPort);
            this.Controls.Add(this.pdParity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pdBaudrate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnectOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection Options";
            this.Load += new System.EventHandler(this.FrmConnectOptions_Load);
            this.Shown += new System.EventHandler(this.FrmConnectOptions_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton cbSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton cbTcpClient;
        private System.Windows.Forms.RadioButton cbTcpServer;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox pdDataBits;
        private System.Windows.Forms.ComboBox pdHandshaking;
        private System.Windows.Forms.ComboBox pdStopBits;
        private System.Windows.Forms.ComboBox pdParity;
        private System.Windows.Forms.ComboBox pdBaudrate;
        private System.Windows.Forms.ComboBox pdSerialPort;
        private System.Windows.Forms.TextBox tbTcpConnectPort;
        private System.Windows.Forms.TextBox tbTcpConnectAddress;
        private System.Windows.Forms.TextBox tbTcpListenPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbInitialDTR;
        private System.Windows.Forms.CheckBox cbInitialRTS;
        private System.Windows.Forms.Label label10;
    }
}