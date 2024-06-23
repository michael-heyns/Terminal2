/* 
 * Terminal2
 *
 * Copyright © 2022 - 23 Michael Heyns
 * 
 * This file is part of Terminal2.
 * 
 * Terminal2 is free software: you can redistribute it and/or  modify it 
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
    partial class frmASCII
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmASCII));
            this.helpASCII = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // helpASCII
            // 
            this.helpASCII.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpASCII.FormattingEnabled = true;
            this.helpASCII.ItemHeight = 15;
            this.helpASCII.Location = new System.Drawing.Point(12, 11);
            this.helpASCII.Name = "helpASCII";
            this.helpASCII.Size = new System.Drawing.Size(131, 409);
            this.helpASCII.TabIndex = 2;
            // 
            // frmASCII
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(157, 436);
            this.Controls.Add(this.helpASCII);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmASCII";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ASCII table";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ASCII_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox helpASCII;
    }
}