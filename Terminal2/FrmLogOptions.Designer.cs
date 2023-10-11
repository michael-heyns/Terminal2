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
    partial class FrmLogOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogOptions));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLogDirectory = new System.Windows.Forms.TextBox();
            this.btnLogfile = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSuggest1 = new System.Windows.Forms.Button();
            this.btnSuggest2 = new System.Windows.Forms.Button();
            this.lblExample = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxLogSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(175, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Directory";
            // 
            // tbLogDirectory
            // 
            this.tbLogDirectory.Location = new System.Drawing.Point(109, 67);
            this.tbLogDirectory.Name = "tbLogDirectory";
            this.tbLogDirectory.Size = new System.Drawing.Size(377, 20);
            this.tbLogDirectory.TabIndex = 2;
            this.tbLogDirectory.TextChanged += new System.EventHandler(this.TbLogDirectory_TextChanged);
            // 
            // btnLogfile
            // 
            this.btnLogfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLogfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnLogfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogfile.ForeColor = System.Drawing.Color.Blue;
            this.btnLogfile.Location = new System.Drawing.Point(75, 67);
            this.btnLogfile.Name = "btnLogfile";
            this.btnLogfile.Size = new System.Drawing.Size(30, 20);
            this.btnLogfile.TabIndex = 3;
            this.btnLogfile.Text = "- -";
            this.btnLogfile.UseVisualStyleBackColor = false;
            this.btnLogfile.Click += new System.EventHandler(this.BtnLogfile_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.Enabled = false;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.Color.Blue;
            this.btnOk.Location = new System.Drawing.Point(214, 162);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "&Apply";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Prefix for every file";
            // 
            // tbPrefix
            // 
            this.tbPrefix.Location = new System.Drawing.Point(109, 41);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(100, 20);
            this.tbPrefix.TabIndex = 19;
            this.tbPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPrefix.TextChanged += new System.EventHandler(this.TbPrefix_TextChanged);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Folder where log file will be placed";
            // 
            // btnSuggest1
            // 
            this.btnSuggest1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSuggest1.FlatAppearance.BorderSize = 0;
            this.btnSuggest1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnSuggest1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuggest1.ForeColor = System.Drawing.Color.Blue;
            this.btnSuggest1.Location = new System.Drawing.Point(279, 39);
            this.btnSuggest1.Name = "btnSuggest1";
            this.btnSuggest1.Size = new System.Drawing.Size(100, 23);
            this.btnSuggest1.TabIndex = 20;
            this.btnSuggest1.Text = "Suggestion #1";
            this.btnSuggest1.UseVisualStyleBackColor = false;
            this.btnSuggest1.Click += new System.EventHandler(this.BtnSuggest1_Click);
            // 
            // btnSuggest2
            // 
            this.btnSuggest2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSuggest2.FlatAppearance.BorderSize = 0;
            this.btnSuggest2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnSuggest2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuggest2.ForeColor = System.Drawing.Color.Blue;
            this.btnSuggest2.Location = new System.Drawing.Point(386, 39);
            this.btnSuggest2.Name = "btnSuggest2";
            this.btnSuggest2.Size = new System.Drawing.Size(100, 23);
            this.btnSuggest2.TabIndex = 21;
            this.btnSuggest2.Text = "Suggestion #2";
            this.btnSuggest2.UseVisualStyleBackColor = false;
            this.btnSuggest2.Click += new System.EventHandler(this.BtnSuggest2_Click);
            // 
            // lblExample
            // 
            this.lblExample.AutoSize = true;
            this.lblExample.Location = new System.Drawing.Point(20, 91);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(203, 13);
            this.lblExample.TabIndex = 22;
            this.lblExample.Text = "Sample logfile: xxxxxxxxxxxxxxxxxxxxxxxxx";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "EXE";
            this.openFileDialog1.FileName = "openFileDialog";
            this.openFileDialog1.Filter = "EXE files|*.exe";
            this.openFileDialog1.Title = "My favourite Text Editor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Maximum size of a single log file before a new log in started:";
            // 
            // txtMaxLogSize
            // 
            this.txtMaxLogSize.Location = new System.Drawing.Point(305, 121);
            this.txtMaxLogSize.MaxLength = 9;
            this.txtMaxLogSize.Name = "txtMaxLogSize";
            this.txtMaxLogSize.Size = new System.Drawing.Size(74, 20);
            this.txtMaxLogSize.TabIndex = 25;
            this.txtMaxLogSize.Text = "0";
            this.txtMaxLogSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(385, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "(0 = No limit)";
            // 
            // FrmLogOptions
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 197);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMaxLogSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblExample);
            this.Controls.Add(this.btnSuggest2);
            this.Controls.Add(this.btnSuggest1);
            this.Controls.Add(this.tbPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnLogfile);
            this.Controls.Add(this.tbLogDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logging Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogfile;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbLogDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnSuggest1;
        private System.Windows.Forms.Button btnSuggest2;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaxLogSize;
        private System.Windows.Forms.Label label5;
    }
}