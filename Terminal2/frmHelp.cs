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
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
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

        private void FrmHelp_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 255; i++)
            {
                string str = $"{i:d3} = {i:x2} = {Convert.ToChar(i)}";
                helpASCII.Items.Add(str);
            }
        }
    }
}
