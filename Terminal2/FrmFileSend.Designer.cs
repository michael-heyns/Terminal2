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
    partial class FrmFileSend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFileSend));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.cbSendCR = new System.Windows.Forms.CheckBox();
            this.cbSendLF = new System.Windows.Forms.CheckBox();
            this.msDelay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(239, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Send File";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(444, 139);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(112, 35);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "All files|*.*";
            this.openFileDialog.Title = "File to send";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Name";
            // 
            // tbFilename
            // 
            this.tbFilename.Location = new System.Drawing.Point(73, 63);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(426, 26);
            this.tbFilename.TabIndex = 13;
            this.tbFilename.TextChanged += new System.EventHandler(this.TbFilename_TextChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(510, 60);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(46, 35);
            this.btnSelect.TabIndex = 14;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // cbSendCR
            // 
            this.cbSendCR.AutoSize = true;
            this.cbSendCR.Location = new System.Drawing.Point(133, 113);
            this.cbSendCR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSendCR.Name = "cbSendCR";
            this.cbSendCR.Size = new System.Drawing.Size(58, 24);
            this.cbSendCR.TabIndex = 15;
            this.cbSendCR.Text = "CR";
            this.cbSendCR.UseVisualStyleBackColor = true;
            // 
            // cbSendLF
            // 
            this.cbSendLF.AutoSize = true;
            this.cbSendLF.Location = new System.Drawing.Point(190, 113);
            this.cbSendLF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSendLF.Name = "cbSendLF";
            this.cbSendLF.Size = new System.Drawing.Size(54, 24);
            this.cbSendLF.TabIndex = 16;
            this.cbSendLF.Text = "LF";
            this.cbSendLF.UseVisualStyleBackColor = true;
            // 
            // msDelay
            // 
            this.msDelay.Location = new System.Drawing.Point(76, 148);
            this.msDelay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msDelay.MaxLength = 5;
            this.msDelay.Name = "msDelay";
            this.msDelay.Size = new System.Drawing.Size(80, 26);
            this.msDelay.TabIndex = 17;
            this.msDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msDelay.TextChanged += new System.EventHandler(this.MsDelay_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Send";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "at the end of every line";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(168, 153);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "ms Delay between lines";
            // 
            // FrmFileSend
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 206);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.msDelay);
            this.Controls.Add(this.cbSendLF);
            this.Controls.Add(this.cbSendCR);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFileSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Send";
            this.Shown += new System.EventHandler(this.FileSend_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.CheckBox cbSendCR;
        private System.Windows.Forms.CheckBox cbSendLF;
        private System.Windows.Forms.TextBox msDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}