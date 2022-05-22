﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using static System.Resources.ResXFileRef;

namespace Terminal
{
    public partial class FrmDisplayOptions : Form
    {
        public DisplayOptions Options;
        public DialogResult Result = DialogResult.Cancel;

        public readonly CheckBox[] FreezeList;
        public readonly ComboBox[] ModeList;
        public readonly TextBox[] TextList;
        public readonly Panel[] ForePanelList;
        public readonly Panel[] BackPanelList;
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
        }

        private void RefreshScreenFromDatabase()
        {
            TextColorInput.BackColor = Options.inputDefaultForeground;
            BackColorInput.BackColor = Options.inputBackground;

            BackColorOutput.BackColor = Options.outputBackground;

            sampleInput.BackColor = Options.inputBackground;
            sampleInput.Font = Options.inputFont;
            sampleInput.ForeColor = Options.inputDefaultForeground;

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
            }

            tbMaxLines.Text = Options.maxLines.ToString();
            tbXtraLinesToRemove.Text = Options.cutXtraLines.ToString();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            AssemblyInfo info = new AssemblyInfo();
            lblThisVersion.Text = $"v{info.AssemblyVersion}";

            cbTimestampOutputLines.Checked = Options.timestampOutputLines;
            RefreshScreenFromDatabase();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            LimitBufferSizes();
            Options.maxLines = Utils.Int(tbMaxLines.Text);
            Options.cutXtraLines = Utils.Int(tbXtraLinesToRemove.Text);
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
            Options.timestampOutputLines = cbTimestampOutputLines.Checked;
        }

        private void BackColorInput_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Options.inputBackground;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.inputBackground = colorDialog.Color;

                DialogResult yn = MessageBox.Show("Do you want to set all filter backgrounds to this color as well?", "Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    for (int i = 0; i < Options.filter.Length; i++)
                    {
                        if (i != SEARCH_INDEX)
                            Options.filter[i].backColor = colorDialog.Color;
                    }
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

        private void T1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int i = Utils.Int(tb.Tag.ToString());
            Options.filter[i].text = tb.Text;
            ModeList[i].Enabled = (tb.Text.Length > 0);
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
            DialogResult yn = MessageBox.Show("Are you sure?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn != DialogResult.Yes)
                return;

            Options.filter[9].foreColor = Color.LightSalmon;
            Options.filter[8].foreColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            Options.filter[7].foreColor = Color.Green;
            Options.filter[6].foreColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            Options.filter[5].foreColor = Color.Olive;
            Options.filter[4].foreColor = Color.Fuchsia;
            Options.filter[3].foreColor = Color.DarkGreen;
            Options.filter[2].foreColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            Options.filter[1].foreColor = Color.Blue;
            Options.filter[0].foreColor = Color.DarkOrange;

            Options.outputBackground = Color.White;

            Options.inputDefaultForeground = Color.Black;
            Options.inputBackground = Color.White;

            Options.outputBackground = Color.Gainsboro;

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            Options.inputFont = (Font)converter.ConvertFromString(Utils.DefaultInputFont);
            Options.outputFont = (Font)converter.ConvertFromString(Utils.DefaultOutputFont);

            for (int i = 0; i < Options.filter.Length; i++)
            {
                Options.filter[i].mode = 1;
                Options.filter[i].text = string.Empty;
                Options.filter[i].backColor = Color.White;
            }

            // search option
            Options.filter[SEARCH_INDEX].foreColor = Color.Black;
            Options.filter[SEARCH_INDEX].backColor = Color.Lime;

            // suggest error condition
            Options.filter[10].foreColor = Color.Yellow;
            Options.filter[10].backColor = Color.Red;
            Options.filter[10].text = "ERROR";

            RefreshScreenFromDatabase();

            if (Utils.Int(tbMaxLines.Text) != 2000 || Utils.Int(tbXtraLinesToRemove.Text) != 500)
            {
                yn = MessageBox.Show("Do you want to reset the maximum line count as well?", "Reset buffer sizes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                    BtnFTReset_Click(sender, e);
            }
        }

        private void BtnFTReset_Click(object sender, EventArgs e)
        {
            tbMaxLines.Text = "2000";
            tbXtraLinesToRemove.Text = "500";

            Options.maxLines = Utils.Int(tbMaxLines.Text);
            Options.cutXtraLines = Utils.Int(tbXtraLinesToRemove.Text);
        }
        private void LimitBufferSizes()
        {
            int v = Utils.Int(tbMaxLines.Text);
            if (v < 10)
                tbMaxLines.Text = "10";
            else if (v > 10000)
                tbMaxLines.Text = "10000";

            v = Utils.Int(tbXtraLinesToRemove.Text);
            if (v < 0)
                tbXtraLinesToRemove.Text = "0";
            else if (v > 10000)
                tbXtraLinesToRemove.Text = "10000";
        }

        private void TbMaxBufSize_Leave(object sender, EventArgs e)
        {
            LimitBufferSizes();
        }

        private void TbCutSize_Leave(object sender, EventArgs e)
        {
            LimitBufferSizes();
        }

        private void TbFreezeSize_Leave(object sender, EventArgs e)
        {
            LimitBufferSizes();
        }
        private void TextBox2_Leave(object sender, EventArgs e)
        {
            LimitBufferSizes();
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