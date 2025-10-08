/* 
 * Terminal2
 *
 * Copyright © 2022 - 23 Michael Heyns
 * 
 * This file is part of Terminal2.
 * 
 * Terminal2 is free software: you can redistribute it and/or  modify it 
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Terminal
{
    public partial class FrmInspect : Form
    {
        private List<Form> forms = new List<Form>();

        private string selectedText = null;
        private bool selectedTextCase = true;
        private Color startForeColor;
        private Color startBackColor;
        private Profile _activeProfile;
        private Brush[] _filterFrontBrush;
        private Brush[] _filterBackBrush;

        public FrmInspect(Brush[] filterFrontBrush, Brush[] filterBackBrush, Profile activeProfile)
        {
            _activeProfile = activeProfile;
            _filterFrontBrush = filterFrontBrush;
            _filterBackBrush = filterBackBrush;
            InitializeComponent();
        }

        private void exportToRTFFilewithColoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.DefaultExt = "rtb";
                saveFileDialog1.Filter = "RTB files|*.rtb";
                saveFileDialog1.Title = "Export to RTF file";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtb.SaveFile(saveFileDialog1.FileName);
                }
            }
            catch
            { 
                // nothing
            }
        }

    private void exportToTXTFiletextOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "TXT files|*.txt|LOG files|*.log|All files|*.*";
                saveFileDialog1.Title = "Export to file";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveFileDialog1.FileName, rtb.Text.ToString());
                }
            }
            catch
            { 
                // nothing
            }
        }

        private void SetForeColor(string txt, bool ignorecase, Color color)
        {
            if (txt.Length == 0) return;
            int start = 0;

            RichTextBoxFinds compCase = RichTextBoxFinds.MatchCase;
            if (ignorecase)
                compCase = RichTextBoxFinds.None;

            while (rtb.Find(txt, start, compCase) >= 0)
            {
                rtb.SelectionColor = color;
                start = rtb.SelectionStart + txt.Length;
            }
        }
        private void SetBackColor(string txt, bool ignorecase, Color color)
        {
            if (txt.Length == 0) return;
            int start = 0;

            RichTextBoxFinds compCase = RichTextBoxFinds.MatchCase;
            if (ignorecase)
                compCase = RichTextBoxFinds.None;

            while (rtb.Find(txt, start, compCase) >= 0)
            {
                rtb.SelectionBackColor = color;
                start = rtb.SelectionStart + txt.Length;
            }
        }
        private string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch 
            { 
                // fails
            }
            return null;
        }

        private void toolStripMenuItemPrev_Click(object sender, EventArgs e)
        {
            int index;
            int start = 0;

            int stopIndex = rtb.SelectionStart;

            StringComparison comparison = StringComparison.Ordinal;
            if (selectedTextCase)
                comparison = StringComparison.OrdinalIgnoreCase;

            int candidate = -1;
            try
            {
                while ((index = rtb.Text.IndexOf(selectedText, start, comparison)) != -1)
                {
                    if (index >= 0 && index < stopIndex)
                        candidate = index;
                    start = index + 1;
                }

                if (candidate >= 0)
                {
                    rtb.Select(candidate, selectedText.Length);
                }
            }
            catch 
            { 
                // nothing
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;

            try
            {
                Clipboard.SetData(DataFormats.Text, rtb.SelectedText);
                rtb.SelectionStart += rtb.SelectionLength;
                rtb.Select(rtb.SelectionStart, 0);
            }
            catch
            {
                // nothing
            }
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rtb.Select(0, rtb.Text.Length);
        }

        private void inspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;
            FrmVertical vert = new FrmVertical();
            for (int i = 0; i < rtb.SelectedText.Length; i++)
            {
                char ch = rtb.SelectedText[i];
                int decvalue = (int)ch & 0xff;
                string dectohex = decvalue.ToString("X");

                DataGridViewRow row = (DataGridViewRow)vert.grid.Rows[0].Clone();
                row.Cells[0].Value = "[ " + i.ToString() + " ]";
                if (decvalue > 32 && decvalue < 128)
                    row.Cells[1].Value = ch.ToString();
                else
                    row.Cells[1].Value = "";
                row.Cells[2].Value = decvalue.ToString();
                row.Cells[3].Value = dectohex;
                vert.grid.Rows.Add(row);    
            }
            vert.SetTitle($"Length={rtb.SelectedText.Length}");
            vert.Show();
            forms.Add(vert);
        }

        private void ShowInHorizontalWindow(string str)
        {
            FrmHorizontal hor = new FrmHorizontal();

            int tlen = Math.Min(str.Length, 653);
            hor.grid.ColumnCount = 1 + tlen;

            DataGridViewRow row1 = hor.grid.Rows[0];
            row1.Height = 18;
            row1.Cells[0].Value = "i";
            for (int i = 0; i < tlen; i++)
                row1.Cells[i + 1].Value = "[ " + i.ToString() + " ]";

            DataGridViewRow row2 = (DataGridViewRow)hor.grid.Rows[0].Clone();
            row2.Height = 18;
            row2.Cells[0].Value = "Char";
            for (int i = 0; i < tlen; i++)
            {
                char ch = str[i];
                int decvalue = (int)ch & 0xff;
                if (decvalue > 32 && decvalue < 128)
                    row2.Cells[i + 1].Value = ch.ToString();
                else
                    row2.Cells[i + 1].Value = "";
            }

            DataGridViewRow row3 = (DataGridViewRow)hor.grid.Rows[0].Clone();
            row3.Height = 18;
            row3.Cells[0].Value = "Dec";
            for (int i = 0; i < tlen; i++)
            {
                char ch = str[i];
                int decvalue = (int)ch & 0xff;
                row3.Cells[i + 1].Value = decvalue.ToString();
            }

            DataGridViewRow row4 = (DataGridViewRow)hor.grid.Rows[0].Clone();
            row4.Height = 18;
            row4.Cells[0].Value = "Hex";
            for (int i = 0; i < tlen; i++)
            {
                char ch = str[i];
                int decvalue = (int)ch & 0xff;
                string dectohex = decvalue.ToString("X");
                row4.Cells[i + 1].Value = dectohex;
            }

            hor.grid.Rows.Add(row2);
            hor.grid.Rows.Add(row3);
            hor.grid.Rows.Add(row4);

            for (int i = 0; i < hor.grid.ColumnCount; i++)
                hor.grid.Columns[i].Width = 45;

            hor.Configure($"Length={str.Length}", str);
            hor.Show();
            forms.Add(hor);
        }
        private void inspectAsHorizontalListCtrlHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;
            ShowInHorizontalWindow(rtb.SelectedText);
        }

        private void decodeAsABase64StringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = rtb.SelectedText;
            if (txt.Length == 0) return;

            bool pad1 = false;
            bool pad2 = false;
            int index = rtb.SelectionStart;
            int pad1index = index + txt.Length;
            int pad2index = index + txt.Length + 1;

            if (pad1index < rtb.Text.Length)
            {
                string p1 = rtb.Text.Substring(pad1index, 1);
                if (p1.StartsWith("="))
                    pad1 = true;
            }

            if (pad2index < rtb.Text.Length)
            {
                string p2 = rtb.Text.Substring(pad1index + 1, 1);
                if (p2.StartsWith("="))
                    pad2 = true;
            }

            string todecode = txt.Trim();
            if (pad1)
                todecode += "=";
            if (pad2)
                todecode += "=";

            string decoded = Base64Decode(todecode);
            if (decoded != null)
            {
                ShowInHorizontalWindow(decoded);
            }
            else
            {
                MessageBox.Show("Cannot convert selected string - Verify selection", "Conversion error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void showNumbersCtrlNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = rtb.SelectedText;
            if (txt.Length == 0) return;

            if (txt.Length > 16)
            {
                MessageBox.Show("String too long (length must be <= 15)", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FrmNumber num = new FrmNumber();

            num.Original.Text = $"{txt}";
            num.Original2.Text = $"{txt}";
            num.Original3.Text = $"{txt}";
            try
            {
                Int64 decvalue = Int64.Parse(txt.Trim());
                string txttohex = decvalue.ToString("X");
                num.HexValue.Text = $"= 0x{txttohex}";
            }
            catch
            {
                num.HexValue.Text = "= Not a DEC number";
            }

            try
            {
                long txttodec = Convert.ToInt64(txt.Trim(), 16);
                num.DecValue.Text = $"= {txttodec}";
            }
            catch
            {
                num.DecValue.Text = "= Not a HEX number";
            }

            try
            {
                Int64 decvalue = Int64.Parse(txt.Trim());
                if (decvalue > ' ' && decvalue < 128)
                {
                    char ch = (char)decvalue;
                    num.ASCIIcharacter.Text = $"= '{ch.ToString()}'";
                }
                else
                {
                    num.ASCIIcharacter.Text = $"= Not printable";
                }
            }
            catch
            {
                num.ASCIIcharacter.Text = "= Not an ASCII character";
            }
            num.Show();
            forms.Add(num);
        }

        private void removeAllColoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.Text.Length;
            rtb.SelectionColor = startForeColor;
            rtb.SelectionBackColor = startBackColor;
            rtb.SelectionStart = rtb.Text.Length;
            selectedText = null;
        }

        private void FrmInspect_Shown(object sender, EventArgs e)
        {
            startForeColor = rtb.ForeColor;
            startBackColor = rtb.BackColor;

            lblSample.ForeColor = Color.Yellow;// DarkSlateGray;// rtb.ForeColor;
            lblSample.BackColor = Color.Gray;// Yellow;// rtb.ForeColor;

            forePanel.BackColor = lblSample.ForeColor;
            backPanel.BackColor = lblSample.BackColor;
            this.BringToFront();
        }

        private void setColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            SetForeColor(rtb.SelectedText, false, colorDialog1.Color);
        }

        private void setSelectionBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            SetBackColor(rtb.SelectedText, false, colorDialog1.Color);
        }

        private void FrmInspect_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in forms)
            {
                try
                {
                    frm.Close();
                }
                catch 
                { 
                    // nothing
                }
            }
        }

        private void importTXTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    rtb.Clear();
                    string alltext = System.IO.File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
                    rtb.Text = alltext;
                }
            }
            catch
            {
                // nothing
            }
        }

        private void forePanel_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            forePanel.BackColor = colorDialog1.Color;
            lblSample.ForeColor = colorDialog1.Color;
        }

        private void backPanel_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backPanel.BackColor = colorDialog1.Color;
            lblSample.BackColor = colorDialog1.Color;
        }

        private void btnChangeSingle_Click(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;
            rtb.SelectionColor = forePanel.BackColor;
            rtb.SelectionBackColor = backPanel.BackColor;
            rtb.Select(rtb.SelectionStart, 0);
            rtb.Focus();
        }

        private void btnChangeAll_Click(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;

            int selStart = rtb.SelectionStart;
            int start = 0;
            while (rtb.Find(rtb.SelectedText, start, RichTextBoxFinds.MatchCase) >= 0)
            {
                rtb.SelectionColor = forePanel.BackColor;
                rtb.SelectionBackColor = backPanel.BackColor;
                start = rtb.SelectionStart + rtb.SelectedText.Length;
            }
            rtb.SelectionStart = selStart;
            rtb.Select(rtb.SelectionStart, 0);
            rtb.Focus();
        }

        private void btnNext(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;

            int start = rtb.SelectionStart + rtb.SelectedText.Length;
            rtb.Find(rtb.SelectedText, start, RichTextBoxFinds.MatchCase);
            rtb.Focus();
        }

        private void btnPrev(object sender, EventArgs e)
        {
            if (rtb.SelectedText.Length == 0) return;

            int endPoint = rtb.SelectionStart;
            int previousPoint = -1;
            int start = 0;
            while (rtb.Find(rtb.SelectedText, start, RichTextBoxFinds.MatchCase) >= 0)
            {
                if (rtb.SelectionStart >= endPoint)
                {
                    if (previousPoint >= 0)
                    {
                        rtb.Select(previousPoint, rtb.SelectedText.Length);
                    }
                    rtb.Select(rtb.SelectionStart, rtb.SelectedText.Length);
                    rtb.Focus();
                    return;
                }
                previousPoint = rtb.SelectionStart;
                start = rtb.SelectionStart + rtb.SelectedText.Length;
            }
            rtb.Select(rtb.SelectionStart, rtb.SelectedText.Length);
            rtb.Focus();
        }

        private void btnMono_Click(object sender, EventArgs e)
        {
            int startSelText = rtb.SelectionStart;
            int startSelTextLen = rtb.SelectedText.Length;

            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.Text.Length;
            rtb.SelectionColor = startForeColor;
            rtb.SelectionBackColor = startBackColor;

            if (startSelText >= 0)
            {
                rtb.Select(startSelText, startSelTextLen);
            }
            rtb.Focus();
        }

        private int searchStart = 0;
        private string lastSearch = string.Empty;
        private RichTextBoxFinds lastCase;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (rtb.Text.Length == 0) return;

            FrmSearch search = new FrmSearch();
            search.SearchText.Text = lastSearch;
            DialogResult rc = search.ShowDialog();
            if (rc == DialogResult.OK)
            {
                RichTextBoxFinds compCase = RichTextBoxFinds.MatchCase;
                if (search.IgnoreCase.Checked)
                    compCase = RichTextBoxFinds.None;

                if (!search.SearchText.Text.Equals(lastSearch))
                    searchStart = 0;

                lastCase = compCase;
                rtb.Find(search.SearchText.Text, searchStart, compCase);
                searchStart = rtb.SelectionStart + 1;

                lastSearch = search.SearchText.Text;
            }
            rtb.Focus();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            FrmReplace replace = new FrmReplace();
            replace.SearchText.Text = rtb.SelectedText;
            DialogResult rc = replace.ShowDialog();
            int selStart = rtb.SelectionStart;
            if (rc == DialogResult.OK)
            {
                RichTextBoxFinds compCase = RichTextBoxFinds.MatchCase;
                if (replace.IgnoreCase.Checked)
                    compCase = RichTextBoxFinds.None;

                int start = 0;
                while (rtb.Find(replace.SearchText.Text, start, compCase) >= 0)
                {
                    rtb.SelectedText = replace.ReplaceText.Text;
                    start = rtb.SelectionStart + 1;
                }
            }
            rtb.SelectionStart = selStart;
            rtb.Focus();
        }

        private void rtb_DoubleClick(object sender, EventArgs e)
        {
            rtb.Enabled = false;

            if (rtb.SelectedText.Length > 0)
            {
                int selStart = rtb.SelectionStart;
                int start = 0;
                while (rtb.Find(rtb.SelectedText, start, RichTextBoxFinds.MatchCase) >= 0)
                {
                    rtb.SelectionColor = forePanel.BackColor;
                    rtb.SelectionBackColor = backPanel.BackColor;
                    start = rtb.SelectionStart + rtb.SelectedText.Length;
                }
                rtb.SelectionStart = selStart;
                rtb.Focus();
            }
            rtb.Enabled = true;
        }

        private void btnFilterColours_Click(object sender, EventArgs e)
        {
            controlPanel.Enabled = false;
            rtb.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            int selStart = rtb.SelectionStart;
            Filters filters = new Filters();
            for (int i = 0; i < rtb.Lines.Count(); i++)
            {
                string line = rtb.Lines[i].ToString();

                int offset = 0;
                if (Utils.HasTimestamp(line))
                    offset += Utils.TimestampLength();

                int filter = filters.FindApplicableFilter(_activeProfile, line, offset);
                if (filter >= 0)
                {
                    int start = rtb.GetFirstCharIndexFromLine(i);
                    int end = rtb.GetFirstCharIndexFromLine(i + 1);
                    if (start >= 0 && end > start)
                    {
                        rtb.Select(start, end - start);
                        rtb.SelectionColor = ((SolidBrush)_filterFrontBrush[filter]).Color;
                        rtb.SelectionBackColor = ((SolidBrush)_filterBackBrush[filter]).Color;
                    }
                }
            }
            rtb.Select(selStart, 0);
            rtb.Enabled = true;
            this.Cursor = Cursors.Default;
            controlPanel.Enabled = true;
        }

        private void FrmInspect_Load(object sender, EventArgs e)
        {
            btnFilterColours_Click(sender, e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int startSelText = rtb.SelectionStart;
            int startSelTextLen = rtb.SelectedText.Length;

            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.Text.Length;
            rtb.SelectionColor = startForeColor;
            rtb.SelectionBackColor = startBackColor;

            if (startSelText >= 0)
            {
                rtb.Select(startSelText, startSelTextLen);
            }
            rtb.Focus();
        }

        private void resetColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
        }

        private void coloiseTheSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnChangeSingle_Click(sender, e);
        }

        private void coloriseAllSimilarItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnChangeAll_Click(sender, e);
        }

        private void coloriseUsingEventDetectionFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFilterColours_Click(sender, e);
        }

        private void asBase64StringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decodeAsABase64StringToolStripMenuItem_Click(sender, e);
        }

        private void btnBase64_Click(object sender, EventArgs e)
        {
            decodeAsABase64StringToolStripMenuItem_Click(sender, e);
        }
    }
}
