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
    public partial class FrmMacroExport : Form
    {
        public bool PressedOK = false;

        public FrmMacroExport()
        {
            InitializeComponent();
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
                int count = listBox.SelectedItems.Count;
                if (count > 0)
                    btnOk.Enabled = true;
                else
                    btnOk.Enabled = false;
            }
            catch { }
        }
    }
}
