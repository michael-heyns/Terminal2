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
    public partial class FrmProfileName : Form
    {
        private string _name;

        public string NewName { get { return _name; } }
        public FrmProfileName(string str, string title)
        {
            InitializeComponent();
            lblBigHeading.Text = title;
            lblSmallHeading.Text = str;
            _name = str;
            tbEditString.Text = str;
        }

        public FrmProfileName(string str, string title, string heading)
        {
            InitializeComponent();
            lblBigHeading.Text = title;
            lblSmallHeading.Text = heading;
            _name = str;
            tbEditString.Text = str;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            _name = tbEditString.Text;
            Close();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = (tbEditString.Text.Length > 0);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnApply.Enabled)
                BtnApply_Click(sender, e);
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
