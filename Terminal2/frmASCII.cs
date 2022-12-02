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
    public partial class frmASCII : Form
    {
        public int Xpos = 10;
        public int Ypos = 10;
        public frmASCII()
        {
            InitializeComponent();
            this.Left = Xpos;
            this.Top = Ypos;
        }

        private void ASCII_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 255; i++)
            {
                string str = $"{i:d3} = {i:x2} = {Convert.ToChar(i)}";
                helpASCII.Items.Add(str);
            }
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
