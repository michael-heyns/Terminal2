
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 108);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Directory";
            // 
            // tbLogDirectory
            // 
            this.tbLogDirectory.Location = new System.Drawing.Point(163, 103);
            this.tbLogDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbLogDirectory.Name = "tbLogDirectory";
            this.tbLogDirectory.Size = new System.Drawing.Size(564, 26);
            this.tbLogDirectory.TabIndex = 2;
            this.tbLogDirectory.TextChanged += new System.EventHandler(this.TbLogDirectory_TextChanged);
            // 
            // btnLogfile
            // 
            this.btnLogfile.Location = new System.Drawing.Point(113, 100);
            this.btnLogfile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogfile.Name = "btnLogfile";
            this.btnLogfile.Size = new System.Drawing.Size(45, 35);
            this.btnLogfile.TabIndex = 3;
            this.btnLogfile.Text = "...";
            this.btnLogfile.UseVisualStyleBackColor = true;
            this.btnLogfile.Click += new System.EventHandler(this.BtnLogfile_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(617, 169);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 35);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "&Apply";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Prefix for every file";
            // 
            // tbPrefix
            // 
            this.tbPrefix.Location = new System.Drawing.Point(163, 63);
            this.tbPrefix.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(148, 26);
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
            this.btnSuggest1.Location = new System.Drawing.Point(419, 60);
            this.btnSuggest1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSuggest1.Name = "btnSuggest1";
            this.btnSuggest1.Size = new System.Drawing.Size(150, 35);
            this.btnSuggest1.TabIndex = 20;
            this.btnSuggest1.Text = "Suggestion #1";
            this.btnSuggest1.UseVisualStyleBackColor = true;
            this.btnSuggest1.Click += new System.EventHandler(this.BtnSuggest1_Click);
            // 
            // btnSuggest2
            // 
            this.btnSuggest2.Location = new System.Drawing.Point(579, 60);
            this.btnSuggest2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSuggest2.Name = "btnSuggest2";
            this.btnSuggest2.Size = new System.Drawing.Size(150, 35);
            this.btnSuggest2.TabIndex = 21;
            this.btnSuggest2.Text = "Suggestion #2";
            this.btnSuggest2.UseVisualStyleBackColor = true;
            this.btnSuggest2.Click += new System.EventHandler(this.BtnSuggest2_Click);
            // 
            // lblExample
            // 
            this.lblExample.AutoSize = true;
            this.lblExample.Location = new System.Drawing.Point(108, 140);
            this.lblExample.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(291, 20);
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
            // FrmLogOptions
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 218);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
    }
}