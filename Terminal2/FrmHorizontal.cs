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
    public partial class FrmHorizontal : Form
    {
        private string _theText;
        private int _fixedHeight = 0;
        public FrmHorizontal()
        {
            InitializeComponent();
        }

        private void _ShowSplitLine(int index)
        {
            if (index < 0)
                index = 0;
            else if (index > _theText.Length)
                index = _theText.Length;

            int countLeft = index;
            if (countLeft > 20)
            {
                tbLeft.Text = "..." + _theText.Substring(countLeft - 20, 20);
            }
            else
            {
                tbLeft.Text = _theText.Substring(0, countLeft);
            }
            tbRight.Text = _theText.Substring(index);
        }
        public void Configure(string str, string theText)
        {
            this.Text = str;
            _theText = theText;
            _ShowSplitLine(0);
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

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _ShowSplitLine(e.ColumnIndex - 1);
        }

        private void FrmHorizontal_Shown(object sender, EventArgs e)
        {
            _fixedHeight = this.Height;
        }

        private void FrmHorizontal_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Height != _fixedHeight)
                this.Height = _fixedHeight;
        }
    }
}
