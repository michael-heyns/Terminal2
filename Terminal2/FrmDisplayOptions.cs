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

        private void RefreshScreenFromDatabase()
        {
            BackColorOutput.BackColor = Options.outputBackground;

            sampleInput.BackColor = Options.inputBackground;
            sampleInput.Font = Options.inputFont;
            sampleInput.ForeColor = Options.inputText;
            TextColorInput.BackColor = Options.inputText;
            BackColorInput.BackColor = Options.inputBackground;
            foreach (Label lbl in SampleList)
                lbl.BackColor = Options.inputBackground;

            for (int i = 0; i < Options.filter.Length; i++)
            {
                ModeList[i].SelectedIndex = Options.filter[i].mode;
                PanelList[i].BackColor = Options.filter[i].color;
                SampleList[i].Font = Options.inputFont;
                SampleList[i].ForeColor = Options.filter[i].color;
                TextList[i].Text = Options.filter[i].text;
                ModeList[i].Enabled = (Options.filter[i].text.Length > 0);
                FreezeList[i].Checked = Options.filter[i].freeze;
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
            colorDialog.Color = Options.filter[i].color;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                PanelList[i].BackColor = colorDialog.Color;
                Options.filter[i].color = colorDialog.Color;
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
            colorDialog.Color = Options.inputText;
            DialogResult ok = colorDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                Options.inputText = colorDialog.Color;
                RefreshScreenFromDatabase();
            }
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            DialogResult yn = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (yn != DialogResult.Yes)
                return;

            Options.filter[11].color = Color.BlueViolet;
            Options.filter[10].color = Color.SeaGreen;
            Options.filter[9].color = Color.LightSalmon;
            Options.filter[8].color = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            Options.filter[7].color = Color.Green;
            Options.filter[6].color = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            Options.filter[5].color = Color.Olive;
            Options.filter[4].color = Color.Fuchsia;
            Options.filter[3].color = Color.Yellow;
            Options.filter[2].color = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            Options.filter[1].color = Color.Blue;
            Options.filter[0].color = Color.Red;

            Options.outputBackground = Color.White;

            Options.inputText = Color.Black;
            Options.inputBackground = Color.White;

            Options.outputBackground = Color.Gainsboro;

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            Options.inputFont = (Font)converter.ConvertFromString(Utils.DefaultInputFont);
            Options.outputFont = (Font)converter.ConvertFromString(Utils.DefaultOutputFont);

            for (int i = 0; i < Options.filter.Length; i++)
            {
                Options.filter[i].mode = 1;
                Options.filter[i].text = string.Empty;
                Options.filter[i].freeze = false;
            }

            RefreshScreenFromDatabase();

            if (Utils.Int(tbMaxLines.Text) != 1500 || Utils.Int(tbXtraLinesToRemove.Text) != 500)
            {
                yn = MessageBox.Show("Do you want to reset the maximum line count as well?", "Reset buffer sizes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                    BtnFTReset_Click(sender, e);
            }
        }

        private void BtnFTReset_Click(object sender, EventArgs e)
        {
            tbMaxLines.Text = "1500";
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
        private void E1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int i = Utils.Int(cb.Tag.ToString());
            Options.filter[i].freeze = cb.Checked;
        }
    }
}