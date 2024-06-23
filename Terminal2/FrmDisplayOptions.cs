/* 
 * Terminal2
 *
 * Copyright © 2021-2024 Michael Heyns
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using static System.Resources.ResXFileRef;

namespace Terminal
{
    public partial class FrmDisplayOptions : Form
    {
        public DisplayOptions Options;
        public DialogResult Result = DialogResult.Cancel;
        public List<string> MacroNames = new List<string>();

        public readonly ComboBox[] ModeList;
        public readonly TextBox[] TextList;
        public readonly Panel[] ForePanelList;
        public readonly Panel[] BackPanelList;
        public readonly ComboBox[] MacroList;
        public readonly Label[] SampleList;

        private const int SEARCH_INDEX = 11;

        public FrmDisplayOptions()
        {
            InitializeComponent();
            ModeList = new ComboBox[] { m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12 };
            TextList = new TextBox[] { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12 };
            ForePanelList = new Panel[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12 };
            BackPanelList = new Panel[] { b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12 };
            SampleList = new Label[] { sample1, sample2, sample3, sample4, sample5, sample6, sample7, sample8, sample9, sample10, sample11, sample12 };

            ComboBox dummy = new ComboBox();
            MacroList = new ComboBox[] { mac1, mac2, mac3, mac4, mac5, mac6, mac7, mac8, mac9, mac10, mac11, dummy };
        }

        private void RefreshScreenFromDatabase()
        {
            TextColorInput.BackColor = Options.inputDefaultForeground;
            BackColorInput.BackColor = Options.inputBackground;

            BackColorOutput.BackColor = Options.outputBackground;

            sampleInput.BackColor = Options.inputBackground;
            sampleInput.Font = Options.inputFont;
            sampleInput.ForeColor = Options.inputDefaultForeground;

            cbFilterCase.Checked = Options.IgnoreCase;

            for (int i = 0; i < Options.filter.Length; i++)
            {
                ModeList[i].SelectedIndex = Options.filter[i].mode;
                ModeList[i].Enabled = (Options.filter[i].text.Length > 0);

                TextList[i].Text = Options.filter[i].text;

                ForePanelList[i].BackColor = Options.filter[i].foreColor;
                BackPanelList[i].BackColor = Options.filter[i].backColor;

                SampleList[i].Font = Options.inputFont;
                SampleList[i].ForeColor = Options.filter[i].foreColor;
                SampleList[i].BackColor = Options.filter[i].backColor;

                MacroList[i].Text = Options.filter[i].macro;
            }
            Application.DoEvents();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
                Options.filter[i].macro = MacroList[i].Text;
            Result = DialogResult.OK;
            Close();
        }
        private void BtnSelectFontInput_Click(object sender, EventArgs e)
        {
            fontDialog.Font = Options.inputFont;
            DialogResult ok = fontDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.inputFont = fontDialog.Font;
                RefreshScreenFromDatabase();
            }
        }

        private void BtnSelectFontOutput_Click(object sender, EventArgs e)
        {
            fontDialog.Font = Options.outputFont;
            DialogResult ok = fontDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.outputFont = fontDialog.Font;
                RefreshScreenFromDatabase();
            }
        }

        private void CbTimestampOutputLines_CheckedChanged(object sender, EventArgs e)
        {
            Options.ShowOutputTimestamp = cbTimestampOutputLines.Checked;
        }

        private void BackColorInput_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Options.inputBackground;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.inputBackground = colorDialog.Color;
                for (int i = 0; i <= 10; i++)
                {
                    if (Options.filter[i].text.Length == 0)
                        Options.filter[i].backColor = colorDialog.Color;
                }
                RefreshScreenFromDatabase();
            }
        }

        private void BackColorOutput_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Options.outputBackground;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.outputBackground = colorDialog.Color;
                RefreshScreenFromDatabase();
            }
        }

        private void ColorFilter_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            int i = Utils.Int(p.Tag.ToString());
            colorDialog.Color = Options.filter[i].foreColor;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                ForePanelList[i].BackColor = colorDialog.Color;
                Options.filter[i].foreColor = colorDialog.Color;
                RefreshScreenFromDatabase();
            }
        }
        private void BackColorClick(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            int i = Utils.Int(p.Tag.ToString());
            colorDialog.Color = Options.filter[i].backColor;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                BackPanelList[i].BackColor = colorDialog.Color;
                Options.filter[i].backColor = colorDialog.Color;
                RefreshScreenFromDatabase();
            }
        }

        private void M1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            int i = Utils.Int(cbox.Tag.ToString());
            Options.filter[i].mode = cbox.SelectedIndex;
        }

        private void ResetRow(int row)
        {
            switch (row)
            {
                case 0:
                    Options.filter[row].foreColor = Color.DarkOrange;
                    break;
                case 1:
                    Options.filter[row].foreColor = Color.Blue;
                    break;
                case 2:
                    Options.filter[row].foreColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                    break;
                case 3:
                    Options.filter[row].foreColor = Color.DarkGreen;
                    break;
                case 4:
                    Options.filter[row].foreColor = Color.Fuchsia;
                    break;
                case 5:
                    Options.filter[row].foreColor = Color.Olive;
                    break;
                case 6:
                    Options.filter[row].foreColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    break;
                case 7:
                    Options.filter[row].foreColor = Color.Green;
                    break;
                case 8:
                    Options.filter[row].foreColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                    break;
                case 9:
                    Options.filter[row].foreColor = Color.LightSalmon;
                    break;
                case 10:
                    Options.filter[row].foreColor = Color.Yellow;
                    break;
                default:
                    return;
            }

            Options.filter[row].backColor = BackColorInput.BackColor;
        }

        private void T1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int row = Utils.Int(tb.Tag.ToString());
            Options.filter[row].text = tb.Text;
            ModeList[row].Enabled = (tb.Text.Length > 0);

            if (!ModeList[row].Enabled)
            {
                ResetRow(row);
                RefreshScreenFromDatabase();
            }
        }

        private void TextColorInput_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Options.inputDefaultForeground;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.inputDefaultForeground = colorDialog.Color;
                RefreshScreenFromDatabase();
            }
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            for (int row = 0; row <= 10; row++)
                ResetRow(row);
            Options.filter[SEARCH_INDEX].foreColor = Color.Black;
            Options.filter[SEARCH_INDEX].backColor = Color.Lime;
            RefreshScreenFromDatabase();
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

        private void cbFilterCase_CheckedChanged(object sender, EventArgs e)
        {
            Options.IgnoreCase = cbFilterCase.Checked;
            RefreshScreenFromDatabase();
        }
        private void FrmDisplayOptions_Shown(object sender, EventArgs e)
        {
            RefreshScreenFromDatabase();
            Application.DoEvents();
            foreach (var mac in MacroList)
            {
                mac.Items.Clear();
                foreach (string s in MacroNames)
                    mac.Items.Add(s);
            }
            cbTimestampOutputLines.Checked = Options.ShowOutputTimestamp;
            RefreshScreenFromDatabase();
        }

        private void btnClearMacros_Click(object sender, EventArgs e)
        {
            for (int row = 0; row <= 10; row++)
            {
                Options.filter[row].macro = string.Empty;
            }
            RefreshScreenFromDatabase();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row <= 10; row++)
            {
                Options.filter[row].mode = 1;
                Options.filter[row].text = string.Empty;
                Options.filter[row].macro = string.Empty;
            }
            RefreshScreenFromDatabase();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }
    }
}