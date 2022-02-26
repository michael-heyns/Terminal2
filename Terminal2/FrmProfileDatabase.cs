﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace Terminal
{
    public partial class FrmProfileDatabase : Form
    {
        private enum FSection { None, Profile, Connections, Macros };
        private string _selectedProfileName = string.Empty;
        public string SelectedProfile { get { return _selectedProfileName; } }
        public FrmProfileDatabase(string name)
        {
            InitializeComponent();
            Database.GetAllNames(lbProfileList);
            _selectedProfileName = name;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult yn = MessageBox.Show("Are you sure?", "Delete this profile", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (yn == DialogResult.Yes)
            {
                string name = lbProfileList.SelectedItem.ToString();
                Database.Remove(name);
                if (_selectedProfileName.Equals(name))
                    _selectedProfileName = "Default";
                Database.GetAllNames(lbProfileList);
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (lbProfileList.SelectedIndex >= 0)
            {
                _selectedProfileName = lbProfileList.SelectedItem.ToString();
                Close();
            }
        }

        private void LbProfileList_DoubleClick(object sender, EventArgs e)
        {
            BtnSelect_Click(sender, e);
        }

        private void ProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbProfileList.Items.Count == 0 || lbProfileList.SelectedIndex < 0)
            {
                btnSelect.Enabled = false;
                btnDelete.Enabled = false;
                btnExport.Enabled = false;
                btnCopy.Enabled = false;
                btnRename.Enabled = false;
                btnStart.Enabled = false;
            }
            else
            {
                string selected = lbProfileList.SelectedItem.ToString();
                if (selected.Equals("Default"))
                {
                    btnDelete.Enabled = false;
                    btnRename.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnRename.Enabled = true;
                }
                btnSelect.Enabled = true;
                btnExport.Enabled = true;
                btnCopy.Enabled = true;
                btnStart.Enabled = true;
            }
        }
        private void BtnExport_Click(object sender, EventArgs e)
        {
            string name = lbProfileList.SelectedItem.ToString();
            saveFileDialog.FileName = name;
            DialogResult ok = saveFileDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                if (Database.Export(name, saveFileDialog.FileName))
                    MessageBox.Show($"Profile {name} exported to {saveFileDialog.FileName}", "Profile exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Export failure - Ensure the filename is correct and try again", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            DialogResult ok = openFileDialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                if (Database.Import(openFileDialog.FileName))
                    Database.GetAllNames(lbProfileList);
            }
        }

        private void BtnRename_Click(object sender, EventArgs e)
        {
            string name = lbProfileList.SelectedItem.ToString();
            FrmProfileName askname = new FrmProfileName(name, "New name for:");
            askname.ShowDialog();
            if (!askname.NewName.Equals(name))
            {
                if (Database.Find(askname.NewName))
                {
                    MessageBox.Show("That name is already in use", "Duplicate name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Database.Rename(name, askname.NewName))
                {
                    Database.GetAllNames(lbProfileList);
                    if (_selectedProfileName.Equals(name))
                        _selectedProfileName = askname.NewName;
                }
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            string name = lbProfileList.SelectedItem.ToString();
            FrmProfileName askname = new FrmProfileName(name, "Name for the copy of:");
            askname.ShowDialog();
            if (!askname.NewName.Equals(name))
            {
                if (Database.Find(askname.NewName))
                {
                    MessageBox.Show("That name is already in use", "Duplicate name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Database.Copy(name, askname.NewName);
                Database.GetAllNames(lbProfileList);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedProfile = lbProfileList.SelectedItem.ToString();
                string currentExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                System.Diagnostics.Process.Start(currentExecutable, "\"" + selectedProfile + "\"");
            }
            catch { }
        }
    }
}