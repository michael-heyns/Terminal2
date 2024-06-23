/* 
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



namespace Terminal
{
    partial class FrmMacroOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMacroOptions));
            this.label1 = new System.Windows.Forms.Label();
            this.tbMacroText = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDelayBetweenChars = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDelayBetweenLinesMs = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbResendEveryMs = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbStopAfterRepeats = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbAddCR = new System.Windows.Forms.CheckBox();
            this.cbAddLF = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbRepeat = new System.Windows.Forms.CheckBox();
            this.tbDelayBetweenLinesSec = new System.Windows.Forms.TextBox();
            this.tbResendEverySec = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.localHelp = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClearGroup = new System.Windows.Forms.Button();
            this.localHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(241, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Function Key Macros";
            // 
            // tbMacroText
            // 
            this.tbMacroText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMacroText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMacroText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMacroText.Location = new System.Drawing.Point(20, 136);
            this.tbMacroText.Multiline = true;
            this.tbMacroText.Name = "tbMacroText";
            this.tbMacroText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMacroText.Size = new System.Drawing.Size(601, 255);
            this.tbMacroText.TabIndex = 1;
            this.tbMacroText.Tag = "27";
            this.tbMacroText.WordWrap = false;
            this.tbMacroText.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.ForeColor = System.Drawing.Color.Blue;
            this.tbTitle.Location = new System.Drawing.Point(51, 110);
            this.tbTitle.MaxLength = 40;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(136, 20);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.Tag = "24";
            this.tbTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTitle.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Title";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.Blue;
            this.btnClear.Location = new System.Drawing.Point(21, 426);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(130, 23);
            this.btnClear.TabIndex = 27;
            this.btnClear.Tag = "28";
            this.btnClear.Text = "&Clear THIS macro";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.Blue;
            this.btnApply.Location = new System.Drawing.Point(480, 426);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(130, 23);
            this.btnApply.TabIndex = 26;
            this.btnApply.Tag = "30";
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Delay between characters";
            // 
            // tbDelayBetweenChars
            // 
            this.tbDelayBetweenChars.Location = new System.Drawing.Point(155, 41);
            this.tbDelayBetweenChars.MaxLength = 6;
            this.tbDelayBetweenChars.Name = "tbDelayBetweenChars";
            this.tbDelayBetweenChars.Size = new System.Drawing.Size(57, 20);
            this.tbDelayBetweenChars.TabIndex = 17;
            this.tbDelayBetweenChars.Tag = "17";
            this.tbDelayBetweenChars.Text = "0";
            this.tbDelayBetweenChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenChars.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(582, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "ms";
            // 
            // tbDelayBetweenLinesMs
            // 
            this.tbDelayBetweenLinesMs.Location = new System.Drawing.Point(541, 41);
            this.tbDelayBetweenLinesMs.MaxLength = 3;
            this.tbDelayBetweenLinesMs.Name = "tbDelayBetweenLinesMs";
            this.tbDelayBetweenLinesMs.Size = new System.Drawing.Size(37, 20);
            this.tbDelayBetweenLinesMs.TabIndex = 19;
            this.tbDelayBetweenLinesMs.Tag = "19";
            this.tbDelayBetweenLinesMs.Text = "0";
            this.tbDelayBetweenLinesMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenLinesMs.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(352, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Delay between lines";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(294, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "ms";
            // 
            // tbResendEveryMs
            // 
            this.tbResendEveryMs.Enabled = false;
            this.tbResendEveryMs.Location = new System.Drawing.Point(244, 69);
            this.tbResendEveryMs.MaxLength = 3;
            this.tbResendEveryMs.Name = "tbResendEveryMs";
            this.tbResendEveryMs.Size = new System.Drawing.Size(44, 20);
            this.tbResendEveryMs.TabIndex = 22;
            this.tbResendEveryMs.Tag = "22";
            this.tbResendEveryMs.Text = "0";
            this.tbResendEveryMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbResendEveryMs.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(511, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "repeats";
            // 
            // tbStopAfterRepeats
            // 
            this.tbStopAfterRepeats.Enabled = false;
            this.tbStopAfterRepeats.Location = new System.Drawing.Point(452, 69);
            this.tbStopAfterRepeats.MaxLength = 6;
            this.tbStopAfterRepeats.Name = "tbStopAfterRepeats";
            this.tbStopAfterRepeats.Size = new System.Drawing.Size(57, 20);
            this.tbStopAfterRepeats.TabIndex = 23;
            this.tbStopAfterRepeats.Tag = "23";
            this.tbStopAfterRepeats.Text = "0";
            this.tbStopAfterRepeats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbStopAfterRepeats.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(393, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Stop after";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClearAll.FlatAppearance.BorderSize = 0;
            this.btnClearAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.ForeColor = System.Drawing.Color.Blue;
            this.btnClearAll.Location = new System.Drawing.Point(327, 426);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(130, 23);
            this.btnClearAll.TabIndex = 28;
            this.btnClearAll.Tag = "29";
            this.btnClearAll.Text = "Clear &ALL macros";
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(401, 398);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Append";
            // 
            // cbAddCR
            // 
            this.cbAddCR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAddCR.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbAddCR.Location = new System.Drawing.Point(452, 397);
            this.cbAddCR.Name = "cbAddCR";
            this.cbAddCR.Size = new System.Drawing.Size(41, 17);
            this.cbAddCR.TabIndex = 24;
            this.cbAddCR.Tag = "25";
            this.cbAddCR.Text = "CR";
            this.cbAddCR.UseVisualStyleBackColor = true;
            this.cbAddCR.CheckedChanged += new System.EventHandler(this.CbAddCR_CheckedChanged);
            // 
            // cbAddLF
            // 
            this.cbAddLF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAddLF.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbAddLF.Location = new System.Drawing.Point(498, 397);
            this.cbAddLF.Name = "cbAddLF";
            this.cbAddLF.Size = new System.Drawing.Size(38, 17);
            this.cbAddLF.TabIndex = 25;
            this.cbAddLF.Tag = "26";
            this.cbAddLF.Text = "LF";
            this.cbAddLF.UseVisualStyleBackColor = true;
            this.cbAddLF.CheckedChanged += new System.EventHandler(this.CbAddCR_CheckedChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(535, 398);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "to every line.";
            // 
            // cbRepeat
            // 
            this.cbRepeat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbRepeat.Location = new System.Drawing.Point(70, 71);
            this.cbRepeat.Name = "cbRepeat";
            this.cbRepeat.Size = new System.Drawing.Size(87, 17);
            this.cbRepeat.TabIndex = 20;
            this.cbRepeat.Tag = "20";
            this.cbRepeat.Text = "Resend after";
            this.cbRepeat.UseVisualStyleBackColor = true;
            this.cbRepeat.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // tbDelayBetweenLinesSec
            // 
            this.tbDelayBetweenLinesSec.Location = new System.Drawing.Point(452, 41);
            this.tbDelayBetweenLinesSec.MaxLength = 6;
            this.tbDelayBetweenLinesSec.Name = "tbDelayBetweenLinesSec";
            this.tbDelayBetweenLinesSec.Size = new System.Drawing.Size(57, 20);
            this.tbDelayBetweenLinesSec.TabIndex = 18;
            this.tbDelayBetweenLinesSec.Tag = "18";
            this.tbDelayBetweenLinesSec.Text = "0";
            this.tbDelayBetweenLinesSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenLinesSec.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // tbResendEverySec
            // 
            this.tbResendEverySec.Enabled = false;
            this.tbResendEverySec.Location = new System.Drawing.Point(155, 69);
            this.tbResendEverySec.MaxLength = 6;
            this.tbResendEverySec.Name = "tbResendEverySec";
            this.tbResendEverySec.Size = new System.Drawing.Size(57, 20);
            this.tbResendEverySec.TabIndex = 21;
            this.tbResendEverySec.Tag = "21";
            this.tbResendEverySec.Text = "0";
            this.tbResendEverySec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbResendEverySec.TextChanged += new System.EventHandler(this.Common_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(511, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "sec";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(214, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "sec";
            // 
            // localHelp
            // 
            this.localHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.localHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.localHelp.Controls.Add(this.textBox3);
            this.localHelp.Controls.Add(this.textBox2);
            this.localHelp.Controls.Add(this.textBox1);
            this.localHelp.Location = new System.Drawing.Point(177, 108);
            this.localHelp.Name = "localHelp";
            this.localHelp.Size = new System.Drawing.Size(414, 212);
            this.localHelp.TabIndex = 36;
            this.localHelp.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox3.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox3.ForeColor = System.Drawing.Color.Red;
            this.textBox3.Location = new System.Drawing.Point(393, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(18, 20);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "X";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox3.Click += new System.EventHandler(this.textBox3_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox2.Location = new System.Drawing.Point(104, 14);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(292, 179);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.Location = new System.Drawing.Point(10, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(111, 148);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "-> Empty lines\r\n->  $nn\r\n->  # comment \r\n-> ## comments\r\n-> #DELAY nn \r\n-> #STX t" +
    "ext\r\n-> #FREEZE\r\n-> #DTR\r\n-> #RTS\r\n-> #MACRO title\r\n    ";
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.ForeColor = System.Drawing.Color.Blue;
            this.btnHelp.Location = new System.Drawing.Point(204, 108);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(61, 23);
            this.btnHelp.TabIndex = 37;
            this.btnHelp.Tag = "28";
            this.btnHelp.Text = "&Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnClearGroup
            // 
            this.btnClearGroup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClearGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClearGroup.FlatAppearance.BorderSize = 0;
            this.btnClearGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnClearGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearGroup.ForeColor = System.Drawing.Color.Blue;
            this.btnClearGroup.Location = new System.Drawing.Point(174, 426);
            this.btnClearGroup.Name = "btnClearGroup";
            this.btnClearGroup.Size = new System.Drawing.Size(130, 23);
            this.btnClearGroup.TabIndex = 38;
            this.btnClearGroup.Tag = "29";
            this.btnClearGroup.Text = "xxxxxx";
            this.btnClearGroup.UseVisualStyleBackColor = false;
            this.btnClearGroup.Click += new System.EventHandler(this.btnClearGroup_Click);
            // 
            // FrmMacroOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 461);
            this.Controls.Add(this.btnClearGroup);
            this.Controls.Add(this.localHelp);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbDelayBetweenLinesSec);
            this.Controls.Add(this.tbResendEverySec);
            this.Controls.Add(this.tbDelayBetweenChars);
            this.Controls.Add(this.tbDelayBetweenLinesMs);
            this.Controls.Add(this.tbResendEveryMs);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRepeat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbStopAfterRepeats);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbAddLF);
            this.Controls.Add(this.cbAddCR);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.tbMacroText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMacroOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Macros";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMacroOptions_FormClosing);
            this.Load += new System.EventHandler(this.FrmMacroOptions_Load);
            this.localHelp.ResumeLayout(false);
            this.localHelp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMacroText;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDelayBetweenChars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDelayBetweenLinesMs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbResendEveryMs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbStopAfterRepeats;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbAddCR;
        private System.Windows.Forms.CheckBox cbAddLF;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbRepeat;
        private System.Windows.Forms.TextBox tbDelayBetweenLinesSec;
        private System.Windows.Forms.TextBox tbResendEverySec;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel localHelp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnHelp;
        protected System.Windows.Forms.Button btnClearGroup;
    }
}