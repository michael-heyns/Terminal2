﻿
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
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Location = new System.Drawing.Point(599, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Handshaking";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Location = new System.Drawing.Point(504, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Stop bits";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Location = new System.Drawing.Point(443, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
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
            this.pdDataBits.Location = new System.Drawing.Point(364, 100);
            this.pdDataBits.Name = "pdDataBits";
            this.pdDataBits.Size = new System.Drawing.Size(49, 21);
            this.pdDataBits.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Location = new System.Drawing.Point(365, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Data bits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Location = new System.Drawing.Point(278, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
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
            this.pdHandshaking.Location = new System.Drawing.Point(558, 100);
            this.pdHandshaking.Name = "pdHandshaking";
            this.pdHandshaking.Size = new System.Drawing.Size(137, 21);
            this.pdHandshaking.TabIndex = 12;
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
            this.pdStopBits.Location = new System.Drawing.Point(504, 100);
            this.pdStopBits.Name = "pdStopBits";
            this.pdStopBits.Size = new System.Drawing.Size(49, 21);
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
            this.pdParity.Location = new System.Drawing.Point(418, 100);
            this.pdParity.Name = "pdParity";
            this.pdParity.Size = new System.Drawing.Size(81, 21);
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
            "256000"});
            this.pdBaudrate.Location = new System.Drawing.Point(258, 100);
            this.pdBaudrate.Name = "pdBaudrate";
            this.pdBaudrate.Size = new System.Drawing.Size(101, 21);
            this.pdBaudrate.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Location = new System.Drawing.Point(236, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
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
            this.pdSerialPort.Location = new System.Drawing.Point(150, 100);
            this.pdSerialPort.Name = "pdSerialPort";
            this.pdSerialPort.Size = new System.Drawing.Size(80, 21);
            this.pdSerialPort.TabIndex = 7;
            // 
            // cbSerial
            // 
            this.cbSerial.AutoSize = true;
            this.cbSerial.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbSerial.Location = new System.Drawing.Point(26, 101);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(122, 17);
            this.cbSerial.TabIndex = 6;
            this.cbSerial.Text = "Serial connection to ";
            this.cbSerial.UseVisualStyleBackColor = true;
            this.cbSerial.CheckedChanged += new System.EventHandler(this.OptSerial_CheckedChanged);
            // 
            // tbTcpConnectPort
            // 
            this.tbTcpConnectPort.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbTcpConnectPort.Enabled = false;
            this.tbTcpConnectPort.Location = new System.Drawing.Point(552, 76);
            this.tbTcpConnectPort.Name = "tbTcpConnectPort";
            this.tbTcpConnectPort.Size = new System.Drawing.Size(68, 20);
            this.tbTcpConnectPort.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(527, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // tbTcpConnectAddress
            // 
            this.tbTcpConnectAddress.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbTcpConnectAddress.Enabled = false;
            this.tbTcpConnectAddress.Location = new System.Drawing.Point(376, 76);
            this.tbTcpConnectAddress.Name = "tbTcpConnectAddress";
            this.tbTcpConnectAddress.Size = new System.Drawing.Size(136, 20);
            this.tbTcpConnectAddress.TabIndex = 3;
            // 
            // cbTcpClient
            // 
            this.cbTcpClient.AutoSize = true;
            this.cbTcpClient.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbTcpClient.Location = new System.Drawing.Point(26, 77);
            this.cbTcpClient.Name = "cbTcpClient";
            this.cbTcpClient.Size = new System.Drawing.Size(349, 17);
            this.cbTcpClient.TabIndex = 2;
            this.cbTcpClient.Text = "TCP socket connection as CLIENT connecting to master at address ";
            this.cbTcpClient.UseVisualStyleBackColor = true;
            this.cbTcpClient.CheckedChanged += new System.EventHandler(this.OptTcpClient_CheckedChanged);
            // 
            // tbTcpListenPort
            // 
            this.tbTcpListenPort.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbTcpListenPort.Enabled = false;
            this.tbTcpListenPort.Location = new System.Drawing.Point(303, 52);
            this.tbTcpListenPort.Name = "tbTcpListenPort";
            this.tbTcpListenPort.Size = new System.Drawing.Size(68, 20);
            this.tbTcpListenPort.TabIndex = 1;
            // 
            // cbTcpServer
            // 
            this.cbTcpServer.AutoSize = true;
            this.cbTcpServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbTcpServer.Location = new System.Drawing.Point(26, 53);
            this.cbTcpServer.Name = "cbTcpServer";
            this.cbTcpServer.Size = new System.Drawing.Size(275, 17);
            this.cbTcpServer.TabIndex = 0;
            this.cbTcpServer.TabStop = true;
            this.cbTcpServer.Text = "TCP socket connection as SERVER listening on port";
            this.cbTcpServer.UseVisualStyleBackColor = true;
            this.cbTcpServer.CheckedChanged += new System.EventHandler(this.OptTcpServer_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(620, 157);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "&Apply";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(300, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(158, 18);
            this.label9.TabIndex = 22;
            this.label9.Text = "Connection Options";
            // 
            // FrmConnectOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 205);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnectOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
    }
}