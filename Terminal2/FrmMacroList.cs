using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terminal
{
    public partial class FrmMacroList : Form
    {

        public bool PressedOK = false;
        public int SelectedSlot = -1;
        public FrmMacroList()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                PressedOK = false;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PressedOK = false;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PressedOK = true;
            Close();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = listBox.SelectedIndex;
                if (index >= 0)
                {
                    string slot = listBox.Items[index].ToString();
                    char[] delim = { ':' };
                    string[] str = slot.Split(delim);
                    if (str[2].Length == 0)
                    {
                        SelectedSlot = index;
                        btnOk.Enabled = true;
                        return;
                    }
                    else
                        btnOk.Enabled = false;
                }
                else
                    btnOk.Enabled = false;
            }
            catch { }
            SelectedSlot = -1;
        }
    }
}
