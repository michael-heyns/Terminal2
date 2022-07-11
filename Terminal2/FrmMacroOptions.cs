/* 
 * Terminal2
 *
 * Copyright © 2022 Michael Heyns
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

        private readonly Profile _activeProfile;
        private readonly DataGridView _macroTable;
        private int _Base = -1;
        private int _Offset = -1;
        public FrmMacroOptions(Profile profile, int id, DataGridView table)
        {
            InitializeComponent();
            _activeProfile = profile;
            _macroTable = table;

            if (id < 0 || id >= 48)
                id = 0;

            Edit(id);
        }

        private int ApplyNumbers(TextBox tb, int min, int max)
        {
            int v = int.Parse(tb.Text);
            if (v < min)
                v = min;
            else if (v > max)
                v = max;
            tb.Text = v.ToString();
            return v;
        }

        private void Base_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton cb = (RadioButton)sender;
            if (cb.Checked)
            {
                SaveThisMacro();
                bool applyState = btnApply.Enabled;
                _Base = int.Parse(cb.Tag.ToString());
                Edit();
                btnApply.Enabled = applyState;
                grpKeys.Enabled = !applyState;
                grpShift.Enabled = !applyState;
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (_Base < 0 || _Offset < 0)
                return;
            if (tbTitle.Text.Length == 0 && tbMacroText.Text.Length > 0)
            {
                MessageBox.Show("This macro has no name", "Name required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveThisMacro();
            btnApply.Enabled = false;
            grpKeys.Enabled = true;
            grpShift.Enabled = true;
            Modified = true;
            Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text.Length > 0 || tbMacroText.Text.Length > 0)
            {
                DialogResult yn = MessageBox.Show("This macro will be cleared.  Are you sure?", "Clear this macro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn != DialogResult.Yes)
                    return;
            }

            ClearScreen();
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            ClearAllMacros();
        }

        private bool ClearAllMacros()
        {
            DialogResult yn = MessageBox.Show("All the macros for this profile will be cleared.  Are you sure?", "Clear all macros", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn != DialogResult.Yes)
                return false;

            yn = MessageBox.Show("Are you really, really sure?", "Clear all macros", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn == DialogResult.Yes)
            {
                ClearScreen();
                for (int m = 0; m < _activeProfile.macros.Length; m++)
                    _activeProfile.macros[m] = null;
                for (int r = 1; r <= 4; r++)
                {
                    for (int f = 1; f <= 12; f++)
                        _macroTable.Rows[r].Cells[f].Value = string.Empty;
                }
                btnApply.Enabled = false;
                grpKeys.Enabled = true;
                grpShift.Enabled = true;
                return true;
            }
            return false;
        }

        private void ClearScreen()
        {
            tbMacroText.Text = string.Empty;
            tbTitle.Text = string.Empty;
            tbDelayBetweenChars.Text = "0";
            tbDelayBetweenLines.Text = "0";
            tbResendEveryMs.Text = "0";
            tbStopAfterRepeats.Text = "0";
            btnApply.Enabled = true;
            grpKeys.Enabled = false;
            grpShift.Enabled = false;
            btnClear.Enabled = false;
            cbAddCR.Checked = true;
            cbAddLF.Checked = false;
        }

        private void Edit(int id)
        {
            RadioButton[] BaseRB = new RadioButton[] { rbNormal, rbShiftPlus, rbControlPlus, rbAltPlus };
            RadioButton[] OffsetRB = new RadioButton[] { F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12 };

            int bs = id / 12;       // 0..3
            int offset = id % 12;   // 0..11

            BaseRB[bs].Checked = true;
            OffsetRB[offset].Checked = true;

            // this triggers "Edit()" indirectly...
        }

        private void Edit()
        {
            if (_Base < 0 || _Offset < 0)
                return;
            int id = _Base + _Offset;

            if (_activeProfile.macros[id] == null)
                _activeProfile.macros[id] = new Macro();
            tbTitle.Text = _activeProfile.macros[id].title;
            tbDelayBetweenChars.Text = _activeProfile.macros[id].delayBetweenChars.ToString();
            tbDelayBetweenLines.Text = _activeProfile.macros[id].delayBetweenLines.ToString();
            cbAddCR.Checked = _activeProfile.macros[id].addCR;
            cbAddLF.Checked = _activeProfile.macros[id].addLF;

            cbRepeat.Checked = _activeProfile.macros[id].repeatEnabled;
            tbResendEveryMs.Text = _activeProfile.macros[id].resendEveryMs.ToString();
            tbStopAfterRepeats.Text = _activeProfile.macros[id].stopAfterRepeats.ToString();

            string s1 = _activeProfile.macros[id].macro.Replace("{0D}", "\r");
            string s2 = s1.Replace("{0A}", "\n");
            tbMacroText.Text = s2;
        }
        private void FrmMacroOptions_Load(object sender, EventArgs e)
        {
            Modified = false;
            btnApply.Enabled = false;
            grpKeys.Enabled = true;
            grpShift.Enabled = true;
        }

        private void MacroText_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            grpKeys.Enabled = false;
            grpShift.Enabled = false;
            btnClear.Enabled = true;
        }

        private void Offset_CheckChanged(object sender, EventArgs e)
        {
            RadioButton cb = (RadioButton)sender;
            if (cb.Checked)
            {
                SaveThisMacro();
                bool applyState = btnApply.Enabled;
                _Offset = int.Parse(cb.Tag.ToString());
                Edit();
                btnApply.Enabled = applyState;
                grpKeys.Enabled = !applyState;
                grpShift.Enabled = !applyState;
            }
        }

        private void SaveThisMacro()
        {
            if (_Base < 0 || _Offset < 0)
                return;

            if (tbTitle.Text.Length == 0 && tbMacroText.Text.Length > 0)
                return;

            int id = _Base + _Offset;

            if (_activeProfile.macros[id] == null)
                _activeProfile.macros[id] = new Macro();

            _activeProfile.macros[id].title = tbTitle.Text;

            string s1 = tbMacroText.Text.Replace("\r", "{0D}");
            string s2 = s1.Replace("\n", "{0A}");
            _activeProfile.macros[id].macro = s2;

            _activeProfile.macros[id].resendEveryMs = ApplyNumbers(tbResendEveryMs, 0, 60000);
            _activeProfile.macros[id].stopAfterRepeats = ApplyNumbers(tbStopAfterRepeats, 0, 60000);
            _activeProfile.macros[id].addCR = cbAddCR.Checked;
            _activeProfile.macros[id].addLF = cbAddLF.Checked;

            _activeProfile.macros[id].repeatEnabled = cbRepeat.Checked;
            _activeProfile.macros[id].delayBetweenChars = ApplyNumbers(tbDelayBetweenChars, 0, 60000);
            _activeProfile.macros[id].delayBetweenLines = ApplyNumbers(tbDelayBetweenLines, 0, 60000);

            int row = (_Base / 10) + 1;
            int column = (_Offset + 1);
            _macroTable.Rows[row].Cells[column].Value = tbTitle.Text;
        }

        private void Title_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            grpKeys.Enabled = false;
            grpShift.Enabled = false;
            btnClear.Enabled = true;
        }

        private void CbAddCR_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            grpKeys.Enabled = false;
            grpShift.Enabled = false;
            btnClear.Enabled = true;
        }

        private void FrmMacroOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnApply.Enabled)
            {
                DialogResult yn = MessageBox.Show("Do you want to save the changes?", "Save changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    if (tbTitle.Text.Length == 0 && tbMacroText.Text.Length > 0)
                        MessageBox.Show("This macro has no name.  It will not be saved", "Name required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        SaveThisMacro();
                }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbResendEveryMs.Enabled = cbRepeat.Checked;
            tbStopAfterRepeats.Enabled = cbRepeat.Checked;
            if (cbRepeat.Checked)
                cbRepeat.ForeColor = Color.Red;
            else
                cbRepeat.ForeColor = Color.Black;

            btnApply.Enabled = true;
            grpKeys.Enabled = false;
            grpShift.Enabled = false;
            btnClear.Enabled = true;
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