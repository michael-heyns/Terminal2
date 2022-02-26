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
        public FrmProfileName(string srcname, string title)
        {
            InitializeComponent();
            lblTitle.Text = title;
            lblSourceName.Text = srcname;
            _name = srcname;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            _name = tbSessionName.Text;
            Close();
        }

        private void TbSessionName_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = (tbSessionName.Text.Length > 0);
        }

        private void TbSessionName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnApply.Enabled)
                BtnApply_Click(sender, e);
        }
    }
}
