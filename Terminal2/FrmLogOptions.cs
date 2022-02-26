using System;
using System.IO;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Terminal
{
    public partial class FrmLogOptions : Form
    {
        private readonly LoggingOptions _logOptions;

        public FrmLogOptions(LoggingOptions options)
        {
            InitializeComponent();
            _logOptions = options;

            // options --> screen
            _logOptions.Modified = false;
            tbLogDirectory.Text = _logOptions.Directory;
            tbPrefix.Text = _logOptions.Prefix;
        }

        private void BtnLogfile_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = _logOptions.Directory;
            DialogResult rc = folderBrowserDialog.ShowDialog();
            if (rc == DialogResult.OK)
                tbLogDirectory.Text = folderBrowserDialog.SelectedPath;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(tbLogDirectory.Text);
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("The directory does not exist", "Invalid directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // screen --> options
            _logOptions.Modified = true;
            _logOptions.Directory = tbLogDirectory.Text;
            _logOptions.Prefix = tbPrefix.Text;
            Close();
        }

        private void BtnAuto_Click(object sender, EventArgs e)
        {
            if (tbLogDirectory.Text.Length > 0)
            {
                string dir = Path.GetDirectoryName(tbLogDirectory.Text);
                if (!Directory.Exists(dir))
                {
                    MessageBox.Show("The directory does not exist", "Invalid directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dt = $"{DateTime.Now.Year:d4}{DateTime.Now.Month:d2}{DateTime.Now.Day:d2}_{DateTime.Now.Hour:d2}{DateTime.Now.Minute:d2}{DateTime.Now.Second:d2}";
                tbLogDirectory.Text = dir + @"\" + tbPrefix.Text + dt + ".log";
            }
        }

        private void BtnSuggest2_Click(object sender, EventArgs e)
        {
            tbLogDirectory.Text = @"C:\Temp";
        }

        private void BtnSuggest1_Click(object sender, EventArgs e)
        {
            tbLogDirectory.Text = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Logs";
        }

        private void ShowExample()
        {
            if (tbLogDirectory.Text.Length > 0)
            {
                string dir = tbLogDirectory.Text;
                string dt = $"{DateTime.Now.Year:d4}{DateTime.Now.Month:d2}{DateTime.Now.Day:d2}_{DateTime.Now.Hour:d2}{DateTime.Now.Minute:d2}{DateTime.Now.Second:d2}";
                lblExample.Text = "Sample: " + dir + @"\" + tbPrefix.Text + dt + ".log";
            }
            else
                lblExample.Text = string.Empty;
        }

        private void TbPrefix_TextChanged(object sender, EventArgs e)
        {
            ShowExample();
        }

        private void TbLogDirectory_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = (tbLogDirectory.Text.Length > 0);
            ShowExample();
        }

        private void BtnEditor_Click(object sender, EventArgs e)
        {

        }

        private void FrmLogOptions_Load(object sender, EventArgs e)
        {
        }
    }
}