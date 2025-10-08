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
    partial class FrmProfileName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProfileName));
            this.lblBigHeading = new System.Windows.Forms.Label();
            this.tbEditString = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblSmallHeading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBigHeading
            // 
            this.lblBigHeading.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBigHeading.Location = new System.Drawing.Point(32, 18);
            this.lblBigHeading.Name = "lblBigHeading";
            this.lblBigHeading.Size = new System.Drawing.Size(269, 20);
            this.lblBigHeading.TabIndex = 9;
            this.lblBigHeading.Text = "XXXXXXXXXXXXX";
            this.lblBigHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbEditString
            // 
            this.tbEditString.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEditString.Location = new System.Drawing.Point(32, 64);
            this.tbEditString.MaxLength = 40;
            this.tbEditString.Name = "tbEditString";
            this.tbEditString.Size = new System.Drawing.Size(269, 23);
            this.tbEditString.TabIndex = 10;
            this.tbEditString.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEditString.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.tbEditString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Enabled = false;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.Blue;
            this.btnApply.Location = new System.Drawing.Point(221, 102);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 26);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // lblSmallHeading
            // 
            this.lblSmallHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallHeading.Location = new System.Drawing.Point(32, 38);
            this.lblSmallHeading.Name = "lblSmallHeading";
            this.lblSmallHeading.Size = new System.Drawing.Size(269, 23);
            this.lblSmallHeading.TabIndex = 12;
            this.lblSmallHeading.Text = "xxxxxxx";
            this.lblSmallHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmProfileName
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 143);
            this.Controls.Add(this.lblSmallHeading);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tbEditString);
            this.Controls.Add(this.lblBigHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProfileName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Name";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBigHeading;
        private System.Windows.Forms.TextBox tbEditString;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblSmallHeading;
    }
}