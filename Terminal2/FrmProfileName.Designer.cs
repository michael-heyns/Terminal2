
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbSessionName = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblSourceName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(31, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(269, 20);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "XXXXXXXXXXXXX";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbSessionName
            // 
            this.tbSessionName.Location = new System.Drawing.Point(31, 69);
            this.tbSessionName.MaxLength = 40;
            this.tbSessionName.Name = "tbSessionName";
            this.tbSessionName.Size = new System.Drawing.Size(269, 20);
            this.tbSessionName.TabIndex = 10;
            this.tbSessionName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSessionName.TextChanged += new System.EventHandler(this.TbSessionName_TextChanged);
            this.tbSessionName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbSessionName_KeyDown);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(225, 95);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // lblSourceName
            // 
            this.lblSourceName.Location = new System.Drawing.Point(31, 43);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(269, 23);
            this.lblSourceName.TabIndex = 12;
            this.lblSourceName.Text = "xxxxxxx";
            this.lblSourceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSessionName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 141);
            this.Controls.Add(this.lblSourceName);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tbSessionName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSessionName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox tbSessionName;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblSourceName;
    }
}