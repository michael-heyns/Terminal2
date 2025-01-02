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
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

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
            tbDelayBetweenLinesMs.Text = "200";
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

        private void btnMoveMacro_Click(object sender, EventArgs e)
        {
            FrmMacroList macroList = new FrmMacroList();

            macroList.listBox.Items.Clear();
            byte group = (byte)'A';
            int groupCount = 0;
            int column = 1;
            int row = 1;
            string rowText = "Plain";
            for (int m = 0; m < _activeProfile.macros.Length; m++)
            {
                Macro mac = _activeProfile.macros[m];
                if (mac != null)
                {
                    if (mac.title.Length > 0)
                    {
                        string entry = $"{(m + 1),3}  {(char)group}: {rowText,-7} F{column,-2} : {mac.title}";
                        macroList.listBox.Items.Add(entry);
                    }
                    else
                    {
                        string entry = $"{(m + 1),3}  {(char)group}: {rowText,-7} F{column,-2} :";
                        macroList.listBox.Items.Add(entry);
                    }
                }
                else
                {
                    string entry = $"{(m + 1),3}  {(char)group}: {rowText,-7} F{column,-2} :";
                    macroList.listBox.Items.Add(entry);
                }

                // group
                groupCount++;
                if (groupCount == 48)
                {
                    groupCount = 0;
                    group += 1;
                }

                // column
                column++;
                if (column == 13)
                {
                    column = 1;

                    // row
                    row++;
                    if (row == 5)
                    {
                        row = 1;
                    }

                    // row text
                    if (row == 1)
                        rowText = "Plain";
                    else if (row == 2)
                        rowText = "Shift +";
                    else if (row == 3)
                        rowText = "Ctrl +";
                    else if (row == 4)
                        rowText = "Alt +";
                }
            }
            macroList.ShowDialog();
            if (macroList.PressedOK)
            {
                if (macroList.SelectedSlot >= 0)
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

                    int destination = macroList.SelectedSlot;
                    Macro src = _activeProfile.macros[_id];
                    _activeProfile.macros[destination] = src.Clone();
                    _activeProfile.macros[_id] = null;
                    Close();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FrmMacroExport macroList = new FrmMacroExport();

            macroList.listBox.Items.Clear();
            byte group = (byte)'A';
            int groupCount = 0;
            int column = 1;
            int row = 1;
            string rowText = "Plain";
            for (int m = 0; m < _activeProfile.macros.Length; m++)
            {
                Macro mac = _activeProfile.macros[m];
                if (mac != null)
                {
                    if (mac.title.Length > 0)
                    {
                        string entry = $"{(m + 1),3}  {(char)group}: {rowText,-7} F{column,-2} : {mac.title}";
                        macroList.listBox.Items.Add(entry);
                    }
                    //else
                    //{
                    //    string entry = $"{(m + 1),3}  {(char)group}: {rowText,-7} F{column,-2} :";
                    //    macroList.listBox.Items.Add(entry);
                    //}
                }
                //else
                //{
                //    string entry = $"{(m + 1),3}  {(char)group}: {rowText,-7} F{column,-2} :";
                //    macroList.listBox.Items.Add(entry);
                //}

                // group
                groupCount++;
                if (groupCount == 48)
                {
                    groupCount = 0;
                    group += 1;
                }

                // column
                column++;
                if (column == 13)
                {
                    column = 1;

                    // row
                    row++;
                    if (row == 5)
                    {
                        row = 1;
                    }

                    // row text
                    if (row == 1)
                        rowText = "Plain";
                    else if (row == 2)
                        rowText = "Shift +";
                    else if (row == 3)
                        rowText = "Ctrl +";
                    else if (row == 4)
                        rowText = "Alt +";
                }
            }
            macroList.ShowDialog();
            if (macroList.PressedOK && macroList.listBox.SelectedItems.Count > 0)
            {
                try
                {
                    DialogResult rc = saveFileDialog1.ShowDialog();
                    if (rc == DialogResult.OK)
                    {
                        string data = string.Empty;

                        data += "# -----------------------------------------------------\n";
                        data += "# Terminal2 - Copyright ©2022-2024 Michael Heyns\n";
                        data += "# Exported macros\n";
                        data += "# -----------------------------------------------------\n";
                        data += "\n";

                        for (int i = 0; i < macroList.listBox.SelectedIndices.Count; i++)
                        {
                            int index = macroList.listBox.SelectedIndices[i];
                            string line = macroList.listBox.Items[index].ToString();
                            char[] delim = { ':' };
                            string[] str = line.ToString().Split(delim);
                            if (str[1].Length > 0)
                            {
                                char[] spaces = { ' ' };
                                string[] tok = line.Trim().Split(spaces);
                                int m = Utils.Int(tok[0]) - 1;

                                Macro mac = _activeProfile.macros[m];
                                if (mac != null && mac.title.Length > 0)
                                {
                                    data += $"[Macro]\n";
                                    data += $"Title={mac.title}\n";
                                    data += $"ICD={mac.delayBetweenChars}\n";
                                    data += $"ILD={mac.delayBetweenLinesMs}\n";
                                    data += $"RepeatON={mac.repeatEnabled}\n";
                                    data += $"Delta={mac.resendEveryMs}\n";
                                    data += $"Repeats={mac.stopAfterRepeats}\n";
                                    data += $"Text={mac.macro}\n";
                                    data += $"AddCR={mac.addCR}\n";
                                    data += $"AddLF={mac.addLF}\n";
                                    data += $"\n";
                                }
                            }
                        }
                        File.WriteAllText(saveFileDialog1.FileName, data);
                        MessageBox.Show($"The macros were exported to {saveFileDialog1.FileName}", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch 
                {
                    MessageBox.Show("An error occurred and the macros did not export correctly", "Please check the filename", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rc = openFileDialog1.ShowDialog();
                if ((rc == DialogResult.OK))
                {
                    string data = File.ReadAllText(openFileDialog1.FileName);
                    string[] lines = data.Split(new char[] { '\r', '\n' });

                    Macro mac = null;

                    foreach (string line in lines)
                    {
                        if (line.Equals("[Macro]"))
                        {
                            // find a new slot inside our profile
                            for (int i = 0; i < _activeProfile.macros.Length; i++)
                            {
                                if (_activeProfile.macros[i] == null || _activeProfile.macros[i].title == null || _activeProfile.macros[i].title.Length == 0)
                                {
                                    _activeProfile.macros[i] = null;
                                    _activeProfile.macros[i] = new Macro();
                                    mac = _activeProfile.macros[i];
                                    break;
                                }
                            }
                        }
                        else if (line.StartsWith("["))
                        {
                            mac = null;
                        }
                        else if (mac != null)
                        {
                            if (line.StartsWith("Title="))
                            {
                                mac.title = line.Substring(6);
                            }
                            else if (line.StartsWith("ICD="))
                            {
                                mac.delayBetweenChars = Utils.Int(line.Substring(4));
                            }
                            else if (line.StartsWith("ILD="))
                            {
                                mac.delayBetweenLinesMs = Utils.Int(line.Substring(4));
                            }
                            else if (line.StartsWith("RepeatON="))
                            {
                                mac.repeatEnabled = line.Contains("True");
                            }
                            else if (line.StartsWith("Delta="))
                            {
                                mac.resendEveryMs = Utils.Int(line.Substring(6));
                            }
                            else if (line.StartsWith("Repeats="))
                            {
                                mac.stopAfterRepeats = Utils.Int(line.Substring(8));
                            }
                            else if (line.StartsWith("Text="))
                            {
                                mac.macro = line.Substring(5);
                            }
                            else if (line.StartsWith("AddCR="))
                            {
                                mac.addCR = line.Contains("True");
                            }
                            else if (line.StartsWith("AddLF="))
                            {
                                mac.addLF = line.Contains("True");
                            }
                        }
                    }
                    MessageBox.Show($"The macros were imported successfully to open slots.  You may want to move them around now.", "Imported successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("An error occurred and the macros did not import correctly", "Please check the filename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}