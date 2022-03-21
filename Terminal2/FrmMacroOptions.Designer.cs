
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
            this.grpShift = new System.Windows.Forms.GroupBox();
            this.rbAltPlus = new System.Windows.Forms.RadioButton();
            this.rbControlPlus = new System.Windows.Forms.RadioButton();
            this.rbShiftPlus = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.grpKeys = new System.Windows.Forms.GroupBox();
            this.F12 = new System.Windows.Forms.RadioButton();
            this.F11 = new System.Windows.Forms.RadioButton();
            this.F10 = new System.Windows.Forms.RadioButton();
            this.F9 = new System.Windows.Forms.RadioButton();
            this.F8 = new System.Windows.Forms.RadioButton();
            this.F7 = new System.Windows.Forms.RadioButton();
            this.F6 = new System.Windows.Forms.RadioButton();
            this.F5 = new System.Windows.Forms.RadioButton();
            this.F4 = new System.Windows.Forms.RadioButton();
            this.F3 = new System.Windows.Forms.RadioButton();
            this.F2 = new System.Windows.Forms.RadioButton();
            this.F1 = new System.Windows.Forms.RadioButton();
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
            this.tbDelayBetweenLines = new System.Windows.Forms.TextBox();
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
            this.helpMacros = new System.Windows.Forms.TextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.cbRepeat = new System.Windows.Forms.CheckBox();
            this.grpShift.SuspendLayout();
            this.grpKeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpShift
            // 
            this.grpShift.Controls.Add(this.rbAltPlus);
            this.grpShift.Controls.Add(this.rbControlPlus);
            this.grpShift.Controls.Add(this.rbShiftPlus);
            this.grpShift.Controls.Add(this.rbNormal);
            this.grpShift.Location = new System.Drawing.Point(17, 43);
            this.grpShift.Name = "grpShift";
            this.grpShift.Size = new System.Drawing.Size(99, 119);
            this.grpShift.TabIndex = 1;
            this.grpShift.TabStop = false;
            this.grpShift.Text = "Shift State";
            // 
            // rbAltPlus
            // 
            this.rbAltPlus.AutoSize = true;
            this.rbAltPlus.Location = new System.Drawing.Point(17, 87);
            this.rbAltPlus.Name = "rbAltPlus";
            this.rbAltPlus.Size = new System.Drawing.Size(46, 17);
            this.rbAltPlus.TabIndex = 3;
            this.rbAltPlus.TabStop = true;
            this.rbAltPlus.Tag = "36";
            this.rbAltPlus.Text = "Alt +";
            this.rbAltPlus.UseVisualStyleBackColor = true;
            this.rbAltPlus.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // rbControlPlus
            // 
            this.rbControlPlus.AutoSize = true;
            this.rbControlPlus.Location = new System.Drawing.Point(17, 64);
            this.rbControlPlus.Name = "rbControlPlus";
            this.rbControlPlus.Size = new System.Drawing.Size(49, 17);
            this.rbControlPlus.TabIndex = 2;
            this.rbControlPlus.TabStop = true;
            this.rbControlPlus.Tag = "24";
            this.rbControlPlus.Text = "Ctrl +";
            this.rbControlPlus.UseVisualStyleBackColor = true;
            this.rbControlPlus.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // rbShiftPlus
            // 
            this.rbShiftPlus.AutoSize = true;
            this.rbShiftPlus.Location = new System.Drawing.Point(17, 42);
            this.rbShiftPlus.Name = "rbShiftPlus";
            this.rbShiftPlus.Size = new System.Drawing.Size(55, 17);
            this.rbShiftPlus.TabIndex = 1;
            this.rbShiftPlus.TabStop = true;
            this.rbShiftPlus.Tag = "12";
            this.rbShiftPlus.Text = "Shift +";
            this.rbShiftPlus.UseVisualStyleBackColor = true;
            this.rbShiftPlus.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Location = new System.Drawing.Point(17, 19);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(58, 17);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Tag = "0";
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            this.rbNormal.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // grpKeys
            // 
            this.grpKeys.Controls.Add(this.F12);
            this.grpKeys.Controls.Add(this.F11);
            this.grpKeys.Controls.Add(this.F10);
            this.grpKeys.Controls.Add(this.F9);
            this.grpKeys.Controls.Add(this.F8);
            this.grpKeys.Controls.Add(this.F7);
            this.grpKeys.Controls.Add(this.F6);
            this.grpKeys.Controls.Add(this.F5);
            this.grpKeys.Controls.Add(this.F4);
            this.grpKeys.Controls.Add(this.F3);
            this.grpKeys.Controls.Add(this.F2);
            this.grpKeys.Controls.Add(this.F1);
            this.grpKeys.Location = new System.Drawing.Point(122, 43);
            this.grpKeys.Name = "grpKeys";
            this.grpKeys.Size = new System.Drawing.Size(191, 119);
            this.grpKeys.TabIndex = 2;
            this.grpKeys.TabStop = false;
            this.grpKeys.Text = "Key";
            // 
            // F12
            // 
            this.F12.AutoSize = true;
            this.F12.Location = new System.Drawing.Point(141, 75);
            this.F12.Name = "F12";
            this.F12.Size = new System.Drawing.Size(43, 17);
            this.F12.TabIndex = 11;
            this.F12.TabStop = true;
            this.F12.Tag = "11";
            this.F12.Text = "F12";
            this.F12.UseVisualStyleBackColor = true;
            this.F12.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F11
            // 
            this.F11.AutoSize = true;
            this.F11.Location = new System.Drawing.Point(98, 75);
            this.F11.Name = "F11";
            this.F11.Size = new System.Drawing.Size(43, 17);
            this.F11.TabIndex = 10;
            this.F11.TabStop = true;
            this.F11.Tag = "10";
            this.F11.Text = "F11";
            this.F11.UseVisualStyleBackColor = true;
            this.F11.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F10
            // 
            this.F10.AutoSize = true;
            this.F10.Location = new System.Drawing.Point(55, 75);
            this.F10.Name = "F10";
            this.F10.Size = new System.Drawing.Size(43, 17);
            this.F10.TabIndex = 9;
            this.F10.TabStop = true;
            this.F10.Tag = "9";
            this.F10.Text = "F10";
            this.F10.UseVisualStyleBackColor = true;
            this.F10.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F9
            // 
            this.F9.AutoSize = true;
            this.F9.Location = new System.Drawing.Point(12, 75);
            this.F9.Name = "F9";
            this.F9.Size = new System.Drawing.Size(37, 17);
            this.F9.TabIndex = 8;
            this.F9.TabStop = true;
            this.F9.Tag = "8";
            this.F9.Text = "F9";
            this.F9.UseVisualStyleBackColor = true;
            this.F9.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F8
            // 
            this.F8.AutoSize = true;
            this.F8.Location = new System.Drawing.Point(141, 52);
            this.F8.Name = "F8";
            this.F8.Size = new System.Drawing.Size(37, 17);
            this.F8.TabIndex = 7;
            this.F8.TabStop = true;
            this.F8.Tag = "7";
            this.F8.Text = "F8";
            this.F8.UseVisualStyleBackColor = true;
            this.F8.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F7
            // 
            this.F7.AutoSize = true;
            this.F7.Location = new System.Drawing.Point(98, 52);
            this.F7.Name = "F7";
            this.F7.Size = new System.Drawing.Size(37, 17);
            this.F7.TabIndex = 6;
            this.F7.TabStop = true;
            this.F7.Tag = "6";
            this.F7.Text = "F7";
            this.F7.UseVisualStyleBackColor = true;
            this.F7.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F6
            // 
            this.F6.AutoSize = true;
            this.F6.Location = new System.Drawing.Point(55, 52);
            this.F6.Name = "F6";
            this.F6.Size = new System.Drawing.Size(37, 17);
            this.F6.TabIndex = 5;
            this.F6.TabStop = true;
            this.F6.Tag = "5";
            this.F6.Text = "F6";
            this.F6.UseVisualStyleBackColor = true;
            this.F6.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F5
            // 
            this.F5.AutoSize = true;
            this.F5.Location = new System.Drawing.Point(12, 52);
            this.F5.Name = "F5";
            this.F5.Size = new System.Drawing.Size(37, 17);
            this.F5.TabIndex = 4;
            this.F5.TabStop = true;
            this.F5.Tag = "4";
            this.F5.Text = "F5";
            this.F5.UseVisualStyleBackColor = true;
            this.F5.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F4
            // 
            this.F4.AutoSize = true;
            this.F4.Location = new System.Drawing.Point(141, 29);
            this.F4.Name = "F4";
            this.F4.Size = new System.Drawing.Size(37, 17);
            this.F4.TabIndex = 3;
            this.F4.TabStop = true;
            this.F4.Tag = "3";
            this.F4.Text = "F4";
            this.F4.UseVisualStyleBackColor = true;
            this.F4.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F3
            // 
            this.F3.AutoSize = true;
            this.F3.Location = new System.Drawing.Point(98, 29);
            this.F3.Name = "F3";
            this.F3.Size = new System.Drawing.Size(37, 17);
            this.F3.TabIndex = 2;
            this.F3.TabStop = true;
            this.F3.Tag = "2";
            this.F3.Text = "F3";
            this.F3.UseVisualStyleBackColor = true;
            this.F3.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F2
            // 
            this.F2.AutoSize = true;
            this.F2.Location = new System.Drawing.Point(55, 29);
            this.F2.Name = "F2";
            this.F2.Size = new System.Drawing.Size(37, 17);
            this.F2.TabIndex = 1;
            this.F2.TabStop = true;
            this.F2.Tag = "1";
            this.F2.Text = "F2";
            this.F2.UseVisualStyleBackColor = true;
            this.F2.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F1
            // 
            this.F1.AutoSize = true;
            this.F1.Location = new System.Drawing.Point(12, 29);
            this.F1.Name = "F1";
            this.F1.Size = new System.Drawing.Size(37, 17);
            this.F1.TabIndex = 0;
            this.F1.TabStop = true;
            this.F1.Tag = "0";
            this.F1.Text = "F1";
            this.F1.UseVisualStyleBackColor = true;
            this.F1.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(199, 9);
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
            this.tbMacroText.Location = new System.Drawing.Point(20, 200);
            this.tbMacroText.Multiline = true;
            this.tbMacroText.Name = "tbMacroText";
            this.tbMacroText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMacroText.Size = new System.Drawing.Size(516, 200);
            this.tbMacroText.TabIndex = 4;
            this.tbMacroText.WordWrap = false;
            this.tbMacroText.TextChanged += new System.EventHandler(this.MacroText_TextChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.ForeColor = System.Drawing.Color.Blue;
            this.tbTitle.Location = new System.Drawing.Point(51, 175);
            this.tbTitle.MaxLength = 40;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(136, 20);
            this.tbTitle.TabIndex = 5;
            this.tbTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTitle.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Title";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(20, 406);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.ForeColor = System.Drawing.Color.Red;
            this.btnApply.Location = new System.Drawing.Point(448, 406);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Delay between characters";
            // 
            // tbDelayBetweenChars
            // 
            this.tbDelayBetweenChars.Location = new System.Drawing.Point(455, 48);
            this.tbDelayBetweenChars.MaxLength = 5;
            this.tbDelayBetweenChars.Name = "tbDelayBetweenChars";
            this.tbDelayBetweenChars.Size = new System.Drawing.Size(42, 20);
            this.tbDelayBetweenChars.TabIndex = 10;
            this.tbDelayBetweenChars.Text = "0";
            this.tbDelayBetweenChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenChars.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(503, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(504, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "ms";
            // 
            // tbDelayBetweenLines
            // 
            this.tbDelayBetweenLines.Location = new System.Drawing.Point(455, 74);
            this.tbDelayBetweenLines.MaxLength = 5;
            this.tbDelayBetweenLines.Name = "tbDelayBetweenLines";
            this.tbDelayBetweenLines.Size = new System.Drawing.Size(42, 20);
            this.tbDelayBetweenLines.TabIndex = 13;
            this.tbDelayBetweenLines.Text = "0";
            this.tbDelayBetweenLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenLines.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(355, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Delay between lines";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(503, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "ms";
            // 
            // tbResendEveryMs
            // 
            this.tbResendEveryMs.Enabled = false;
            this.tbResendEveryMs.Location = new System.Drawing.Point(455, 100);
            this.tbResendEveryMs.MaxLength = 5;
            this.tbResendEveryMs.Name = "tbResendEveryMs";
            this.tbResendEveryMs.Size = new System.Drawing.Size(42, 20);
            this.tbResendEveryMs.TabIndex = 16;
            this.tbResendEveryMs.Text = "0";
            this.tbResendEveryMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbResendEveryMs.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(504, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "repeats";
            // 
            // tbStopAfterRepeats
            // 
            this.tbStopAfterRepeats.Enabled = false;
            this.tbStopAfterRepeats.Location = new System.Drawing.Point(455, 126);
            this.tbStopAfterRepeats.MaxLength = 5;
            this.tbStopAfterRepeats.Name = "tbStopAfterRepeats";
            this.tbStopAfterRepeats.Size = new System.Drawing.Size(42, 20);
            this.tbStopAfterRepeats.TabIndex = 19;
            this.tbStopAfterRepeats.Text = "0";
            this.tbStopAfterRepeats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbStopAfterRepeats.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(396, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Stop after";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearAll.Location = new System.Drawing.Point(101, 406);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 21;
            this.btnClearAll.Text = "Clear &All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(335, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Append";
            // 
            // cbAddCR
            // 
            this.cbAddCR.AutoSize = true;
            this.cbAddCR.Location = new System.Drawing.Point(386, 177);
            this.cbAddCR.Name = "cbAddCR";
            this.cbAddCR.Size = new System.Drawing.Size(41, 17);
            this.cbAddCR.TabIndex = 25;
            this.cbAddCR.Text = "CR";
            this.cbAddCR.UseVisualStyleBackColor = true;
            this.cbAddCR.CheckedChanged += new System.EventHandler(this.CbAddCR_CheckedChanged);
            // 
            // cbAddLF
            // 
            this.cbAddLF.AutoSize = true;
            this.cbAddLF.Location = new System.Drawing.Point(432, 177);
            this.cbAddLF.Name = "cbAddLF";
            this.cbAddLF.Size = new System.Drawing.Size(38, 17);
            this.cbAddLF.TabIndex = 26;
            this.cbAddLF.Text = "LF";
            this.cbAddLF.UseVisualStyleBackColor = true;
            this.cbAddLF.CheckedChanged += new System.EventHandler(this.CbAddCR_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(469, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "to every line.";
            // 
            // helpMacros
            // 
            this.helpMacros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.helpMacros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpMacros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.helpMacros.Location = new System.Drawing.Point(33, 210);
            this.helpMacros.Multiline = true;
            this.helpMacros.Name = "helpMacros";
            this.helpMacros.ReadOnly = true;
            this.helpMacros.Size = new System.Drawing.Size(394, 84);
            this.helpMacros.TabIndex = 28;
            this.helpMacros.Text = resources.GetString("helpMacros.Text");
            this.helpMacros.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(495, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(47, 23);
            this.btnHelp.TabIndex = 29;
            this.btnHelp.Text = "&Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // cbRepeat
            // 
            this.cbRepeat.AutoSize = true;
            this.cbRepeat.Location = new System.Drawing.Point(370, 101);
            this.cbRepeat.Name = "cbRepeat";
            this.cbRepeat.Size = new System.Drawing.Size(87, 17);
            this.cbRepeat.TabIndex = 21;
            this.cbRepeat.Text = "Resend after";
            this.cbRepeat.UseVisualStyleBackColor = true;
            this.cbRepeat.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // FrmMacroOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 441);
            this.Controls.Add(this.tbDelayBetweenChars);
            this.Controls.Add(this.tbDelayBetweenLines);
            this.Controls.Add(this.tbResendEveryMs);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRepeat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbStopAfterRepeats);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.helpMacros);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.grpKeys);
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
            this.Controls.Add(this.grpShift);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMacroOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Macros";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMacroOptions_FormClosing);
            this.Load += new System.EventHandler(this.FrmMacroOptions_Load);
            this.grpShift.ResumeLayout(false);
            this.grpShift.PerformLayout();
            this.grpKeys.ResumeLayout(false);
            this.grpKeys.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grpShift;
        private System.Windows.Forms.GroupBox grpKeys;
        private System.Windows.Forms.RadioButton rbShiftPlus;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbAltPlus;
        private System.Windows.Forms.RadioButton rbControlPlus;
        private System.Windows.Forms.RadioButton F12;
        private System.Windows.Forms.RadioButton F11;
        private System.Windows.Forms.RadioButton F10;
        private System.Windows.Forms.RadioButton F9;
        private System.Windows.Forms.RadioButton F8;
        private System.Windows.Forms.RadioButton F7;
        private System.Windows.Forms.RadioButton F6;
        private System.Windows.Forms.RadioButton F5;
        private System.Windows.Forms.RadioButton F4;
        private System.Windows.Forms.RadioButton F3;
        private System.Windows.Forms.RadioButton F2;
        private System.Windows.Forms.RadioButton F1;
        private System.Windows.Forms.TextBox tbMacroText;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDelayBetweenChars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDelayBetweenLines;
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
        private System.Windows.Forms.TextBox helpMacros;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox cbRepeat;
    }
}