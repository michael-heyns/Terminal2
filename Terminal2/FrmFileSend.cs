/* 
 * Terminal2
 *
 * Copyright © 2022-23-23 Michael Heyns
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terminal
{
    public partial class FrmFileSend : Form
    {
        public DialogResult Result = DialogResult.Cancel;
        public string Filename = string.Empty;
        public int InterLineDelay = 0;
        public bool SendCR = true;
        public bool SendLF = true;
        public int MinimumDelay = 0;
        public int MaximumDelay = 10000;
        public bool XModem = false;
        public FrmFileSend()
        {
            InitializeComponent();
            Result = DialogResult.Cancel;
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            int ivalue = Utils.Int(msDelay.Text);
            if (ivalue < MinimumDelay || ivalue > MaximumDelay)
            {
                MessageBox.Show($"The inter-line-delay value is out of range. Please specify a value between {MinimumDelay} and {MaximumDelay} inclusive", "Value out of range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InterLineDelay = ivalue;
            SendCR = cbSendCR.Checked;
            SendLF = cbSendLF.Checked;
            Filename = tbFilename.Text;
            Result = DialogResult.OK;
            Close();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = Filename;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                tbFilename.Text = openFileDialog.FileName;
        }

        private void FileSend_Shown(object sender, EventArgs e)
        {
            tbFilename.Text = Filename;
            msDelay.Text = InterLineDelay.ToString();
            cbSendCR.Checked = SendCR;
            cbSendLF.Checked = SendLF;
        }

        private void TbFilename_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = (tbFilename.Text.Length > 0 && msDelay.Text.Length > 0);
        }

        private void MsDelay_TextChanged(object sender, EventArgs e)
        {
            if (msDelay.Text.Length > 0)
            {
                int ivalue = Utils.Int(msDelay.Text);
                if (!msDelay.Text.Equals(ivalue.ToString()))
                    msDelay.Text = string.Empty;
            }
            btnSend.Enabled = (tbFilename.Text.Length > 0 && msDelay.Text.Length > 0);
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

        private void cbText_CheckedChanged(object sender, EventArgs e)
        {
            grpText.Enabled = cbText.Checked;
            XModem = cbXmodem.Checked;
        }

        private void cbXmodem_CheckedChanged(object sender, EventArgs e)
        {
            grpText.Enabled = cbText.Checked;
            XModem = cbXmodem.Checked;
        }
    }
}
