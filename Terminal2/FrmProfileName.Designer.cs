
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
            this.lblBigHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBigHeading.Location = new System.Drawing.Point(31, 23);
            this.lblBigHeading.Name = "lblBigHeading";
            this.lblBigHeading.Size = new System.Drawing.Size(269, 20);
            this.lblBigHeading.TabIndex = 9;
            this.lblBigHeading.Text = "XXXXXXXXXXXXX";
            this.lblBigHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbEditString
            // 
            this.tbEditString.Location = new System.Drawing.Point(31, 69);
            this.tbEditString.MaxLength = 40;
            this.tbEditString.Name = "tbEditString";
            this.tbEditString.Size = new System.Drawing.Size(269, 20);
            this.tbEditString.TabIndex = 10;
            this.tbEditString.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEditString.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.tbEditString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // btnApply
            // 
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(225, 95);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // lblSmallHeading
            // 
            this.lblSmallHeading.Location = new System.Drawing.Point(31, 43);
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
            this.ClientSize = new System.Drawing.Size(334, 141);
            this.Controls.Add(this.lblSmallHeading);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tbEditString);
            this.Controls.Add(this.lblBigHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProfileName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Name";
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