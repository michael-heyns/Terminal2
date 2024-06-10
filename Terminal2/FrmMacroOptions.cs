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
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Terminal
{
    public partial class FrmMacroOptions : Form
    {
        public bool Modified = false;
        public char macroLabel = 'A';
        public int macroGroupStart = 0;

        private readonly Profile _activeProfile;
        private int _id;

        public FrmMacroOptions(Profile profile, int id)
        {
            InitializeComponent();
            _activeProfile = profile;

            if (id < 0 || id >= (48 * 4))
                id = 0;
            _id = id;

            if (_activeProfile.macros[id] == null) 
                _activeProfile.macros[id] = new Macro();
            tbTitle.Text = _activeProfile.macros[id].title;
            tbDelayBetweenChars.Text = _activeProfile.macros[id].delayBetweenChars.ToString();

            int dbl = _activeProfile.macros[id].delayBetweenLinesMs; // in ms
            tbDelayBetweenLinesSec.Text = (dbl / 1000).ToString();
            tbDelayBetweenLinesMs.Text = (dbl % 1000).ToString();

            cbAddCR.Checked = _activeProfile.macros[id].addCR;
            cbAddLF.Checked = _activeProfile.macros[id].addLF;

            cbRepeat.Checked = _activeProfile.macros[id].repeatEnabled;

            int res = _activeProfile.macros[id].resendEveryMs; // in ms
            tbResendEverySec.Text = (res / 1000).ToString();
            tbResendEveryMs.Text = (res % 1000).ToString();

            tbStopAfterRepeats.Text = _activeProfile.macros[id].stopAfterRepeats.ToString();

            string s1 = _activeProfile.macros[id].macro.Replace("{0D}", "\r");
            string s2 = s1.Replace("{0A}", "\n");
            tbMacroText.Text = s2;
        }

        private int ApplyNumbers(TextBox tb, int min, int max)
        {
            int v = min;
            try
            {
                if (tb == null)
                    return min;
                if (tb.MaxLength == 0)
                    return min;

                v = int.Parse(tb.Text);
                if (v < min)
                    v = min;
                else if (v > max)
                    v = max;
                tb.Text = v.ToString();
            }
            catch { }
            return v;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (Modified)
            {
                if (tbTitle.Text.Length == 0 && tbMacroText.Text.Length > 0)
                {
                    MessageBox.Show("This macro has no name", "Name required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SaveProfile();
            }
            Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            Modified = true;
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            DialogResult yn = MessageBox.Show("All the macros for this profile will be cleared.  Are you sure?", "Clear all macros", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn != DialogResult.Yes)
                return;

            yn = MessageBox.Show("Are you really, really sure?", "Clear all macros", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn == DialogResult.Yes)
            {
                for (int m = 0; m < _activeProfile.macros.Length; m++)
                    _activeProfile.macros[m] = null;
                ClearScreen();
                SaveProfile();
                Close();
            }
        }

        private void ClearScreen()
        {
            tbMacroText.Text = string.Empty;
            tbTitle.Text = string.Empty;
            tbDelayBetweenChars.Text = "0";
            tbDelayBetweenLinesMs.Text = "0";
            tbDelayBetweenLinesSec.Text = "0";
            tbResendEveryMs.Text = "0";
            tbResendEverySec.Text = "0";
            tbStopAfterRepeats.Text = "0";

            cbAddCR.Checked = true;
            cbAddLF.Checked = false;
            Modified = true;
        }

        private void FrmMacroOptions_Load(object sender, EventArgs e)
        {
            Modified = false;
            btnClearGroup.Text = $"Clear group '{macroLabel}' macros";
        }

        private void MacroText_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void SaveProfile()
        {
            if (tbTitle.Text.Length == 0 && tbMacroText.Text.Length > 0)
                return;

            if (_activeProfile.macros[_id] == null)
                _activeProfile.macros[_id] = new Macro();

            _activeProfile.macros[_id].title = tbTitle.Text;

            string s1 = tbMacroText.Text.Replace("\r", "{0D}");
            string s2 = s1.Replace("\n", "{0A}");
            _activeProfile.macros[_id].macro = s2;

            int ms = ApplyNumbers(tbResendEveryMs, 0, 999);
            int sec = ApplyNumbers(tbResendEverySec, 0, 999999);
            _activeProfile.macros[_id].resendEveryMs = (sec * 1000) + ms;

            _activeProfile.macros[_id].stopAfterRepeats = ApplyNumbers(tbStopAfterRepeats, 0, 60000);
            _activeProfile.macros[_id].addCR = cbAddCR.Checked;
            _activeProfile.macros[_id].addLF = cbAddLF.Checked;

            _activeProfile.macros[_id].repeatEnabled = cbRepeat.Checked;
            _activeProfile.macros[_id].delayBetweenChars = ApplyNumbers(tbDelayBetweenChars, 0, 60000);

            ms = ApplyNumbers(tbDelayBetweenLinesMs, 0, 999);
            sec = ApplyNumbers(tbDelayBetweenLinesSec, 0, 999999);
            _activeProfile.macros[_id].delayBetweenLinesMs = (sec * 1000) + ms;

            int column = (_id % 12) + 1;
            int row = (_id / 12) + 1;
            Modified = false;
        }

        private void CbAddCR_CheckedChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void FrmMacroOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
            {
                DialogResult yn = MessageBox.Show("Do you want to save the changes?", "Save changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    if (tbTitle.Text.Length == 0 && tbMacroText.Text.Length > 0)
                        MessageBox.Show("This macro has no name.  It will not be saved", "Name required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        SaveProfile();
                }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbResendEveryMs.Enabled = cbRepeat.Checked;
            tbResendEverySec.Enabled = cbRepeat.Checked;
            tbStopAfterRepeats.Enabled = cbRepeat.Checked;
            if (cbRepeat.Checked)
                cbRepeat.ForeColor = Color.Red;
            else
                cbRepeat.ForeColor = Color.Black;
            Modified = true;
        }
        private void Common_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && !Modified)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            localHelp.Visible = true;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            localHelp.Visible = false;
        }

        private void btnClearGroup_Click(object sender, EventArgs e)
        {
            DialogResult yn = MessageBox.Show($"Macros {macroGroupStart + 1} - {macroGroupStart + 48} for this profile will be cleared.  Are you sure?", "Clear all macros", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn != DialogResult.Yes)
                return;

            yn = MessageBox.Show("Are you really, really sure?", "Clear all macros", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn == DialogResult.Yes)
            {
                for (int m = macroGroupStart; m < macroGroupStart + 48; m++)
                    _activeProfile.macros[m] = null;
                ClearScreen();
                SaveProfile();
                Close();
            }
        }
    }
}