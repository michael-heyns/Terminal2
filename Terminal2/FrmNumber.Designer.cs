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
    partial class FrmNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNumber));
            this.HexValue = new System.Windows.Forms.Label();
            this.DecValue = new System.Windows.Forms.Label();
            this.ASCIIcharacter = new System.Windows.Forms.Label();
            this.Original = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HexValue
            // 
            this.HexValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HexValue.Location = new System.Drawing.Point(247, 21);
            this.HexValue.Name = "HexValue";
            this.HexValue.Size = new System.Drawing.Size(164, 23);
            this.HexValue.TabIndex = 0;
            this.HexValue.Text = "= 123456";
            this.HexValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DecValue
            // 
            this.DecValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecValue.Location = new System.Drawing.Point(247, 44);
            this.DecValue.Name = "DecValue";
            this.DecValue.Size = new System.Drawing.Size(164, 23);
            this.DecValue.TabIndex = 1;
            this.DecValue.Text = "= 123456";
            this.DecValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ASCIIcharacter
            // 
            this.ASCIIcharacter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ASCIIcharacter.Location = new System.Drawing.Point(247, 67);
            this.ASCIIcharacter.Name = "ASCIIcharacter";
            this.ASCIIcharacter.Size = new System.Drawing.Size(164, 23);
            this.ASCIIcharacter.TabIndex = 2;
            this.ASCIIcharacter.Text = "= 123456";
            this.ASCIIcharacter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Original
            // 
            this.Original.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Original.Location = new System.Drawing.Point(11, 44);
            this.Original.Name = "Original";
            this.Original.Size = new System.Drawing.Size(141, 23);
            this.Original.TabIndex = 3;
            this.Original.Text = "1234567890123456";
            this.Original.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(158, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "decimal --> hex";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(158, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "hex --> decimal";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(158, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "--> ASCII";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(436, 110);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Original);
            this.Controls.Add(this.ASCIIcharacter);
            this.Controls.Add(this.DecValue);
            this.Controls.Add(this.HexValue);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNumber";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label HexValue;
        public System.Windows.Forms.Label DecValue;
        public System.Windows.Forms.Label ASCIIcharacter;
        public System.Windows.Forms.Label Original;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}