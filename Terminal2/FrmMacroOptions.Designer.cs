
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
            this.cbRepeat = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
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
            this.grpShift.Location = new System.Drawing.Point(26, 66);
            this.grpShift.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpShift.Name = "grpShift";
            this.grpShift.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpShift.Size = new System.Drawing.Size(148, 183);
            this.grpShift.TabIndex = 1;
            this.grpShift.TabStop = false;
            this.grpShift.Text = "Shift State";
            // 
            // rbAltPlus
            // 
            this.rbAltPlus.AutoSize = true;
            this.rbAltPlus.Location = new System.Drawing.Point(26, 134);
            this.rbAltPlus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbAltPlus.Name = "rbAltPlus";
            this.rbAltPlus.Size = new System.Drawing.Size(66, 24);
            this.rbAltPlus.TabIndex = 12;
            this.rbAltPlus.TabStop = true;
            this.rbAltPlus.Tag = "36";
            this.rbAltPlus.Text = "Alt +";
            this.rbAltPlus.UseVisualStyleBackColor = true;
            this.rbAltPlus.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // rbControlPlus
            // 
            this.rbControlPlus.AutoSize = true;
            this.rbControlPlus.Location = new System.Drawing.Point(26, 98);
            this.rbControlPlus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbControlPlus.Name = "rbControlPlus";
            this.rbControlPlus.Size = new System.Drawing.Size(71, 24);
            this.rbControlPlus.TabIndex = 11;
            this.rbControlPlus.TabStop = true;
            this.rbControlPlus.Tag = "24";
            this.rbControlPlus.Text = "Ctrl +";
            this.rbControlPlus.UseVisualStyleBackColor = true;
            this.rbControlPlus.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // rbShiftPlus
            // 
            this.rbShiftPlus.AutoSize = true;
            this.rbShiftPlus.Location = new System.Drawing.Point(26, 65);
            this.rbShiftPlus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbShiftPlus.Name = "rbShiftPlus";
            this.rbShiftPlus.Size = new System.Drawing.Size(80, 24);
            this.rbShiftPlus.TabIndex = 10;
            this.rbShiftPlus.TabStop = true;
            this.rbShiftPlus.Tag = "12";
            this.rbShiftPlus.Text = "Shift +";
            this.rbShiftPlus.UseVisualStyleBackColor = true;
            this.rbShiftPlus.CheckedChanged += new System.EventHandler(this.Base_CheckedChanged);
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Location = new System.Drawing.Point(26, 29);
            this.rbNormal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(84, 24);
            this.rbNormal.TabIndex = 9;
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
            this.grpKeys.Location = new System.Drawing.Point(183, 66);
            this.grpKeys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpKeys.Name = "grpKeys";
            this.grpKeys.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpKeys.Size = new System.Drawing.Size(286, 183);
            this.grpKeys.TabIndex = 2;
            this.grpKeys.TabStop = false;
            this.grpKeys.Text = "Key";
            // 
            // F12
            // 
            this.F12.AutoSize = true;
            this.F12.Location = new System.Drawing.Point(212, 115);
            this.F12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F12.Name = "F12";
            this.F12.Size = new System.Drawing.Size(62, 24);
            this.F12.TabIndex = 24;
            this.F12.TabStop = true;
            this.F12.Tag = "11";
            this.F12.Text = "F12";
            this.F12.UseVisualStyleBackColor = true;
            this.F12.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F11
            // 
            this.F11.AutoSize = true;
            this.F11.Location = new System.Drawing.Point(147, 115);
            this.F11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F11.Name = "F11";
            this.F11.Size = new System.Drawing.Size(62, 24);
            this.F11.TabIndex = 23;
            this.F11.TabStop = true;
            this.F11.Tag = "10";
            this.F11.Text = "F11";
            this.F11.UseVisualStyleBackColor = true;
            this.F11.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F10
            // 
            this.F10.AutoSize = true;
            this.F10.Location = new System.Drawing.Point(82, 115);
            this.F10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F10.Name = "F10";
            this.F10.Size = new System.Drawing.Size(62, 24);
            this.F10.TabIndex = 22;
            this.F10.TabStop = true;
            this.F10.Tag = "9";
            this.F10.Text = "F10";
            this.F10.UseVisualStyleBackColor = true;
            this.F10.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F9
            // 
            this.F9.AutoSize = true;
            this.F9.Location = new System.Drawing.Point(18, 115);
            this.F9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F9.Name = "F9";
            this.F9.Size = new System.Drawing.Size(53, 24);
            this.F9.TabIndex = 21;
            this.F9.TabStop = true;
            this.F9.Tag = "8";
            this.F9.Text = "F9";
            this.F9.UseVisualStyleBackColor = true;
            this.F9.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F8
            // 
            this.F8.AutoSize = true;
            this.F8.Location = new System.Drawing.Point(212, 80);
            this.F8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F8.Name = "F8";
            this.F8.Size = new System.Drawing.Size(53, 24);
            this.F8.TabIndex = 20;
            this.F8.TabStop = true;
            this.F8.Tag = "7";
            this.F8.Text = "F8";
            this.F8.UseVisualStyleBackColor = true;
            this.F8.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F7
            // 
            this.F7.AutoSize = true;
            this.F7.Location = new System.Drawing.Point(147, 80);
            this.F7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F7.Name = "F7";
            this.F7.Size = new System.Drawing.Size(53, 24);
            this.F7.TabIndex = 19;
            this.F7.TabStop = true;
            this.F7.Tag = "6";
            this.F7.Text = "F7";
            this.F7.UseVisualStyleBackColor = true;
            this.F7.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F6
            // 
            this.F6.AutoSize = true;
            this.F6.Location = new System.Drawing.Point(82, 80);
            this.F6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F6.Name = "F6";
            this.F6.Size = new System.Drawing.Size(53, 24);
            this.F6.TabIndex = 18;
            this.F6.TabStop = true;
            this.F6.Tag = "5";
            this.F6.Text = "F6";
            this.F6.UseVisualStyleBackColor = true;
            this.F6.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F5
            // 
            this.F5.AutoSize = true;
            this.F5.Location = new System.Drawing.Point(18, 80);
            this.F5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F5.Name = "F5";
            this.F5.Size = new System.Drawing.Size(53, 24);
            this.F5.TabIndex = 17;
            this.F5.TabStop = true;
            this.F5.Tag = "4";
            this.F5.Text = "F5";
            this.F5.UseVisualStyleBackColor = true;
            this.F5.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F4
            // 
            this.F4.AutoSize = true;
            this.F4.Location = new System.Drawing.Point(212, 45);
            this.F4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F4.Name = "F4";
            this.F4.Size = new System.Drawing.Size(53, 24);
            this.F4.TabIndex = 16;
            this.F4.TabStop = true;
            this.F4.Tag = "3";
            this.F4.Text = "F4";
            this.F4.UseVisualStyleBackColor = true;
            this.F4.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F3
            // 
            this.F3.AutoSize = true;
            this.F3.Location = new System.Drawing.Point(147, 45);
            this.F3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F3.Name = "F3";
            this.F3.Size = new System.Drawing.Size(53, 24);
            this.F3.TabIndex = 15;
            this.F3.TabStop = true;
            this.F3.Tag = "2";
            this.F3.Text = "F3";
            this.F3.UseVisualStyleBackColor = true;
            this.F3.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F2
            // 
            this.F2.AutoSize = true;
            this.F2.Location = new System.Drawing.Point(82, 45);
            this.F2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F2.Name = "F2";
            this.F2.Size = new System.Drawing.Size(53, 24);
            this.F2.TabIndex = 14;
            this.F2.TabStop = true;
            this.F2.Tag = "1";
            this.F2.Text = "F2";
            this.F2.UseVisualStyleBackColor = true;
            this.F2.CheckedChanged += new System.EventHandler(this.Offset_CheckChanged);
            // 
            // F1
            // 
            this.F1.AutoSize = true;
            this.F1.Location = new System.Drawing.Point(18, 45);
            this.F1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F1.Name = "F1";
            this.F1.Size = new System.Drawing.Size(53, 24);
            this.F1.TabIndex = 13;
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
            this.label1.Location = new System.Drawing.Point(302, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 29);
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
            this.tbMacroText.Location = new System.Drawing.Point(30, 342);
            this.tbMacroText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbMacroText.Multiline = true;
            this.tbMacroText.Name = "tbMacroText";
            this.tbMacroText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMacroText.Size = new System.Drawing.Size(781, 339);
            this.tbMacroText.TabIndex = 1;
            this.tbMacroText.WordWrap = false;
            this.tbMacroText.TextChanged += new System.EventHandler(this.MacroText_TextChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.ForeColor = System.Drawing.Color.Blue;
            this.tbTitle.Location = new System.Drawing.Point(76, 269);
            this.tbTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTitle.MaxLength = 40;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(202, 26);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTitle.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 274);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Title";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(31, 691);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(176, 35);
            this.btnClear.TabIndex = 26;
            this.btnClear.Text = "&Clear THIS macro";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.ForeColor = System.Drawing.Color.Red;
            this.btnApply.Location = new System.Drawing.Point(674, 691);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(112, 35);
            this.btnApply.TabIndex = 25;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Delay between characters";
            // 
            // tbDelayBetweenChars
            // 
            this.tbDelayBetweenChars.Location = new System.Drawing.Point(682, 74);
            this.tbDelayBetweenChars.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDelayBetweenChars.MaxLength = 5;
            this.tbDelayBetweenChars.Name = "tbDelayBetweenChars";
            this.tbDelayBetweenChars.Size = new System.Drawing.Size(61, 26);
            this.tbDelayBetweenChars.TabIndex = 4;
            this.tbDelayBetweenChars.Text = "0";
            this.tbDelayBetweenChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenChars.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(754, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(756, 118);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "ms";
            // 
            // tbDelayBetweenLines
            // 
            this.tbDelayBetweenLines.Location = new System.Drawing.Point(682, 114);
            this.tbDelayBetweenLines.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDelayBetweenLines.MaxLength = 5;
            this.tbDelayBetweenLines.Name = "tbDelayBetweenLines";
            this.tbDelayBetweenLines.Size = new System.Drawing.Size(61, 26);
            this.tbDelayBetweenLines.TabIndex = 5;
            this.tbDelayBetweenLines.Text = "0";
            this.tbDelayBetweenLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelayBetweenLines.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(532, 117);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Delay between lines";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(754, 158);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "ms";
            // 
            // tbResendEveryMs
            // 
            this.tbResendEveryMs.Enabled = false;
            this.tbResendEveryMs.Location = new System.Drawing.Point(682, 154);
            this.tbResendEveryMs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbResendEveryMs.MaxLength = 5;
            this.tbResendEveryMs.Name = "tbResendEveryMs";
            this.tbResendEveryMs.Size = new System.Drawing.Size(61, 26);
            this.tbResendEveryMs.TabIndex = 6;
            this.tbResendEveryMs.Text = "0";
            this.tbResendEveryMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbResendEveryMs.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(756, 198);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "repeats";
            // 
            // tbStopAfterRepeats
            // 
            this.tbStopAfterRepeats.Enabled = false;
            this.tbStopAfterRepeats.Location = new System.Drawing.Point(682, 194);
            this.tbStopAfterRepeats.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStopAfterRepeats.MaxLength = 5;
            this.tbStopAfterRepeats.Name = "tbStopAfterRepeats";
            this.tbStopAfterRepeats.Size = new System.Drawing.Size(61, 26);
            this.tbStopAfterRepeats.TabIndex = 8;
            this.tbStopAfterRepeats.Text = "0";
            this.tbStopAfterRepeats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbStopAfterRepeats.TextChanged += new System.EventHandler(this.Title_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(594, 200);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Stop after";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClearAll.Location = new System.Drawing.Point(353, 691);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(178, 35);
            this.btnClearAll.TabIndex = 27;
            this.btnClearAll.Text = "Clear &ALL macros";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(502, 274);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 20);
            this.label11.TabIndex = 24;
            this.label11.Text = "Append";
            // 
            // cbAddCR
            // 
            this.cbAddCR.AutoSize = true;
            this.cbAddCR.Location = new System.Drawing.Point(579, 272);
            this.cbAddCR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAddCR.Name = "cbAddCR";
            this.cbAddCR.Size = new System.Drawing.Size(58, 24);
            this.cbAddCR.TabIndex = 2;
            this.cbAddCR.Text = "CR";
            this.cbAddCR.UseVisualStyleBackColor = true;
            this.cbAddCR.CheckedChanged += new System.EventHandler(this.CbAddCR_CheckedChanged);
            // 
            // cbAddLF
            // 
            this.cbAddLF.AutoSize = true;
            this.cbAddLF.Location = new System.Drawing.Point(648, 272);
            this.cbAddLF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAddLF.Name = "cbAddLF";
            this.cbAddLF.Size = new System.Drawing.Size(54, 24);
            this.cbAddLF.TabIndex = 3;
            this.cbAddLF.Text = "LF";
            this.cbAddLF.UseVisualStyleBackColor = true;
            this.cbAddLF.CheckedChanged += new System.EventHandler(this.CbAddCR_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(704, 274);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 20);
            this.label12.TabIndex = 27;
            this.label12.Text = "to every line.";
            // 
            // cbRepeat
            // 
            this.cbRepeat.AutoSize = true;
            this.cbRepeat.Location = new System.Drawing.Point(555, 155);
            this.cbRepeat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbRepeat.Name = "cbRepeat";
            this.cbRepeat.Size = new System.Drawing.Size(128, 24);
            this.cbRepeat.TabIndex = 7;
            this.cbRepeat.Text = "Resend after";
            this.cbRepeat.UseVisualStyleBackColor = true;
            this.cbRepeat.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 317);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(733, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "Empty lines will be ignored.  Comments starts with \'#\'.  Insert control character" +
    "s with $nn. (eg $23 for \'#\')";
            // 
            // FrmMacroOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 740);
            this.Controls.Add(this.label8);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "FrmMacroOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.CheckBox cbRepeat;
        private System.Windows.Forms.Label label8;
    }
}