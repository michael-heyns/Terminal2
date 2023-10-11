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
            txtMaxLogSize.Text = _logOptions.MaxLogSize.ToString();
        }

        private void BtnLogfile_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = _logOptions.Directory;
            DialogResult rc = folderBrowserDialog.ShowDialog();
            if (rc == DialogResult.OK)
                tbLogDirectory.Text = folderBrowserDialog.SelectedPath;
            btnOk.Focus();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(tbLogDirectory.Text);
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("The directory does not exist", "Invalid directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbLogDirectory.Focus();
                return;
            }

            foreach (char ch in txtMaxLogSize.Text)
            {
                if (ch < '0' || ch > '9')
                {
                    MessageBox.Show("The log file limit is not a number", "Invalid number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaxLogSize.Focus();
                    return;
                }
            }

            int len = Utils.Int(txtMaxLogSize.Text);
            if (len > 0 && len < 10000)
            {
                MessageBox.Show("Impractical log file limit.  Specify 0 or a number > 10000", "Logfile limit too small", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaxLogSize.Text = "0";
                txtMaxLogSize.Focus();
                return;
            }

            // screen --> options
            _logOptions.Modified = true;
            _logOptions.Directory = tbLogDirectory.Text;
            _logOptions.Prefix = tbPrefix.Text;
            _logOptions.MaxLogSize = Utils.Int(txtMaxLogSize.Text);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}