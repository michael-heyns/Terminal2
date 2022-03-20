using System;
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
        public readonly Panel[] PanelList;
        public readonly Label[] SampleList;
        public FrmDisplayOptions()
        {
            InitializeComponent();
            ModeList = new ComboBox[] { m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12 };
            TextList = new TextBox[] { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12 };
            PanelList = new Panel[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12 };
            SampleList = new Label[] { sample1, sample2, sample3, sample4, sample5, sample6, sample7, sample8, sample9, sample10, sample11, sample12 };
            FreezeList = new CheckBox[] { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12 };
        }

        private void ShowDisplayOptions()
        {
            cbColourFilters.Checked = Options.colorFiltersEnabled;
            grpColourFilters.Enabled = Options.colorFiltersEnabled;
            BackColorOutput.BackColor = Options.outputBackground;

            sampleInput.BackColor = Options.inputBackground;
            sampleInput.Font = Options.inputFont;
            sampleInput.ForeColor = Options.inputText;
            TextColorInput.BackColor = Options.inputText;
            BackColorInput.BackColor = Options.inputBackground;
            foreach (Label lbl in SampleList)
                lbl.BackColor = Options.inputBackground;

            for (int i = 0; i < Options.lines.Length; i++)
            {
                ModeList[i].SelectedIndex = Options.lines[i].mode;
                PanelList[i].BackColor = Options.lines[i].color;
                SampleList[i].Font = Options.inputFont;
                SampleList[i].ForeColor = Options.lines[i].color;
                TextList[i].Text = Options.lines[i].text;
                ModeList[i].Enabled = (Options.lines[i].text.Length > 0);
                FreezeList[i].Checked = Options.lines[i].freeze;
            }

            tbMaxBufSize.Text = Options.maxBufferSizeMB.ToString();
            tbCutSize.Text = Options.cutPercent.ToString();
            tbFreezeSize.Text = Options.freezeSizeKB.ToString();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            AssemblyInfo info = new AssemblyInfo();
            lblThisVersion.Text = $"v{info.AssemblyVersion}";

            cbTimestampOutputLines.Checked = Options.timestampOutputLines;
            ShowDisplayOptions();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            LimitBufferSizes();
            Options.maxBufferSizeMB = Utils.Int(tbMaxBufSize.Text);
            Options.cutPercent = Utils.Int(tbCutSize.Text);
            Options.freezeSizeKB = Utils.Int(tbFreezeSize.Text);
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
                ShowDisplayOptions();
            }
        }

        private void BtnSelectFontOutput_Click(object sender, EventArgs e)
        {
            fontDialog.Font = Options.outputFont;
            DialogResult ok = fontDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.outputFont = fontDialog.Font;
                ShowDisplayOptions();
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
                ShowDisplayOptions();
            }
        }

        private void BackColorOutput_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Options.outputBackground;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.outputBackground = colorDialog.Color;
                ShowDisplayOptions();
            }
        }

        private void ColorFilter_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            int i = Utils.Int(p.Tag.ToString());
            colorDialog.Color = Options.lines[i].color;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                PanelList[i].BackColor = colorDialog.Color;
                Options.lines[i].color = colorDialog.Color;
                ShowDisplayOptions();
            }
        }

        private void M1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            int i = Utils.Int(cbox.Tag.ToString());
            Options.lines[i].mode = cbox.SelectedIndex;
        }

        private void T1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int i = Utils.Int(tb.Tag.ToString());
            Options.lines[i].text = tb.Text;
            ModeList[i].Enabled = (tb.Text.Length > 0);
        }

        private void TextColorInput_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Options.inputText;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.inputText = colorDialog.Color;
                ShowDisplayOptions();
            }
        }

        private void CbColourFilters_CheckedChanged(object sender, EventArgs e)
        {
            grpColourFilters.Enabled = cbColourFilters.Checked;
            Options.colorFiltersEnabled = cbColourFilters.Checked;
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            DialogResult yn = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn != DialogResult.Yes)
                return;

            Options.lines[11].color = Color.BlueViolet;
            Options.lines[10].color = Color.SeaGreen;
            Options.lines[9].color = Color.LightSalmon;
            Options.lines[8].color = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            Options.lines[7].color = Color.Green;
            Options.lines[6].color = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            Options.lines[5].color = Color.Olive;
            Options.lines[4].color = Color.Fuchsia;
            Options.lines[3].color = Color.Yellow;
            Options.lines[2].color = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            Options.lines[1].color = Color.Blue;
            Options.lines[0].color = Color.Red;

            Options.colorFiltersEnabled = false;
            Options.outputBackground = Color.White;

            Options.inputText = Color.Black;
            Options.inputBackground = Color.White;

            Options.outputBackground = Color.Gainsboro;

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            Options.inputFont = (Font)converter.ConvertFromString(Utils.DefaultInputFont);
            Options.outputFont = (Font)converter.ConvertFromString(Utils.DefaultOutputFont);

            for (int i = 0; i < Options.lines.Length; i++)
            {
                Options.lines[i].mode = 1;
                Options.lines[i].text = string.Empty;
                Options.lines[i].freeze = false;
            }
            ShowDisplayOptions();
        }

        private void BtnFTReset_Click(object sender, EventArgs e)
        {
            tbMaxBufSize.Text = "10";
            tbCutSize.Text = "10";
            tbFreezeSize.Text = "50";
        }
        private void LimitBufferSizes()
        {
            int v = Utils.Int(tbMaxBufSize.Text);
            if (v < 1)
                tbMaxBufSize.Text = "1";

            v = Utils.Int(tbCutSize.Text);
            if (v < 1)
                tbCutSize.Text = "1";
            else if (v > 50)
                tbCutSize.Text = "50";

            v = Utils.Int(tbFreezeSize.Text);
            if (v < 1)
                tbFreezeSize.Text = "1";
            else if (v > 1000)
                tbFreezeSize.Text = "1000";
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FreezeChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int i = Utils.Int(cb.Tag.ToString());
            Options.lines[i].freeze = cb.Checked;
        }
    }
}