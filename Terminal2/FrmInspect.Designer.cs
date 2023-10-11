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

namespace Terminal
{
    partial class FrmInspect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInspect));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTXTFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToRTFFilewithColoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToTXTFiletextOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchAndReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.inspectAsVerticalListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHorizontalListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNumbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.decodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.inspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inspectAsHorizontalListCtrlHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNumbersCtrlNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.decodeAsABase64StringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.removeAllColoursToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionForegroundColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionBackgroundColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llblMessage = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.btnFilterColours = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnMono = new System.Windows.Forms.PictureBox();
            this.lblSample = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.bNext = new System.Windows.Forms.Button();
            this.bPrev = new System.Windows.Forms.Button();
            this.forePanel = new System.Windows.Forms.Panel();
            this.backPanel = new System.Windows.Forms.Panel();
            this.btnChangeAll = new System.Windows.Forms.Button();
            this.btnChangeSingle = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMono)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToToolStripMenuItem,
            this.editToolStripMenuItem,
            this.selectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1045, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToToolStripMenuItem
            // 
            this.saveToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importTXTFileToolStripMenuItem,
            this.exportToRTFFilewithColoursToolStripMenuItem,
            this.exportToTXTFiletextOnlyToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.saveToToolStripMenuItem.Name = "saveToToolStripMenuItem";
            this.saveToToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.saveToToolStripMenuItem.Text = "&File";
            // 
            // importTXTFileToolStripMenuItem
            // 
            this.importTXTFileToolStripMenuItem.Name = "importTXTFileToolStripMenuItem";
            this.importTXTFileToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.importTXTFileToolStripMenuItem.Text = "Import TXT or LOG file";
            this.importTXTFileToolStripMenuItem.Click += new System.EventHandler(this.importTXTFileToolStripMenuItem_Click);
            // 
            // exportToRTFFilewithColoursToolStripMenuItem
            // 
            this.exportToRTFFilewithColoursToolStripMenuItem.Name = "exportToRTFFilewithColoursToolStripMenuItem";
            this.exportToRTFFilewithColoursToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.exportToRTFFilewithColoursToolStripMenuItem.Text = "Export to RTF file (with colours)";
            this.exportToRTFFilewithColoursToolStripMenuItem.Click += new System.EventHandler(this.exportToRTFFilewithColoursToolStripMenuItem_Click);
            // 
            // exportToTXTFiletextOnlyToolStripMenuItem
            // 
            this.exportToTXTFiletextOnlyToolStripMenuItem.Name = "exportToTXTFiletextOnlyToolStripMenuItem";
            this.exportToTXTFiletextOnlyToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.exportToTXTFiletextOnlyToolStripMenuItem.Text = "Export to TXT file (text only)";
            this.exportToTXTFiletextOnlyToolStripMenuItem.Click += new System.EventHandler(this.exportToTXTFiletextOnlyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(235, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.exitToolStripMenuItem.Text = "&Close";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1,
            this.searchAndReplaceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.ShortcutKeyDisplayString = "";
            this.searchToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(226, 22);
            this.searchToolStripMenuItem1.Text = "&Search";
            this.searchToolStripMenuItem1.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // searchAndReplaceToolStripMenuItem
            // 
            this.searchAndReplaceToolStripMenuItem.Name = "searchAndReplaceToolStripMenuItem";
            this.searchAndReplaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.searchAndReplaceToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.searchAndReplaceToolStripMenuItem.Text = "Search and Replace...";
            this.searchAndReplaceToolStripMenuItem.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.inspectAsVerticalListToolStripMenuItem,
            this.showHorizontalListToolStripMenuItem,
            this.showNumbersToolStripMenuItem,
            this.toolStripMenuItem5,
            this.decodeToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem1_Click);
            // 
            // copyToClipboardToolStripMenuItem1
            // 
            this.copyToClipboardToolStripMenuItem1.Name = "copyToClipboardToolStripMenuItem1";
            this.copyToClipboardToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(250, 22);
            this.copyToClipboardToolStripMenuItem1.Text = "&Copy to Clipboard as text";
            this.copyToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(247, 6);
            // 
            // inspectAsVerticalListToolStripMenuItem
            // 
            this.inspectAsVerticalListToolStripMenuItem.Name = "inspectAsVerticalListToolStripMenuItem";
            this.inspectAsVerticalListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.inspectAsVerticalListToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.inspectAsVerticalListToolStripMenuItem.Text = "Show Vertical list";
            this.inspectAsVerticalListToolStripMenuItem.Click += new System.EventHandler(this.inspectToolStripMenuItem_Click);
            // 
            // showHorizontalListToolStripMenuItem
            // 
            this.showHorizontalListToolStripMenuItem.Name = "showHorizontalListToolStripMenuItem";
            this.showHorizontalListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.showHorizontalListToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.showHorizontalListToolStripMenuItem.Text = "Show Horizontal list";
            this.showHorizontalListToolStripMenuItem.Click += new System.EventHandler(this.inspectAsHorizontalListCtrlHToolStripMenuItem_Click);
            // 
            // showNumbersToolStripMenuItem
            // 
            this.showNumbersToolStripMenuItem.Name = "showNumbersToolStripMenuItem";
            this.showNumbersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.showNumbersToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.showNumbersToolStripMenuItem.Text = "Show Numbers";
            this.showNumbersToolStripMenuItem.Click += new System.EventHandler(this.showNumbersCtrlNToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(247, 6);
            // 
            // decodeToolStripMenuItem
            // 
            this.decodeToolStripMenuItem.Name = "decodeToolStripMenuItem";
            this.decodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.decodeToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.decodeToolStripMenuItem.Text = "Decode as Base64 string";
            this.decodeToolStripMenuItem.Click += new System.EventHandler(this.decodeAsABase64StringToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem1,
            this.copyToClipboardToolStripMenuItem,
            this.toolStripMenuItem2,
            this.inspectToolStripMenuItem,
            this.inspectAsHorizontalListCtrlHToolStripMenuItem,
            this.showNumbersCtrlNToolStripMenuItem,
            this.toolStripMenuItem4,
            this.decodeAsABase64StringToolStripMenuItem,
            this.toolStripMenuItem6,
            this.removeAllColoursToolStripMenuItem1,
            this.selectionForegroundColourToolStripMenuItem,
            this.selectionBackgroundColourToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(236, 220);
            this.contextMenuStrip1.Text = "Decode as a Base64 string";
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(235, 22);
            this.selectAllToolStripMenuItem1.Text = "Select All                        Ctrl+A";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.selectAllToolStripMenuItem1_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard        Ctrl+C";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(232, 6);
            // 
            // inspectToolStripMenuItem
            // 
            this.inspectToolStripMenuItem.Name = "inspectToolStripMenuItem";
            this.inspectToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.inspectToolStripMenuItem.Text = "Show Vertical list        Ctrl+V";
            this.inspectToolStripMenuItem.Click += new System.EventHandler(this.inspectToolStripMenuItem_Click);
            // 
            // inspectAsHorizontalListCtrlHToolStripMenuItem
            // 
            this.inspectAsHorizontalListCtrlHToolStripMenuItem.Name = "inspectAsHorizontalListCtrlHToolStripMenuItem";
            this.inspectAsHorizontalListCtrlHToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.inspectAsHorizontalListCtrlHToolStripMenuItem.Text = "Show Horizontal list   Ctrl+H";
            this.inspectAsHorizontalListCtrlHToolStripMenuItem.Click += new System.EventHandler(this.inspectAsHorizontalListCtrlHToolStripMenuItem_Click);
            // 
            // showNumbersCtrlNToolStripMenuItem
            // 
            this.showNumbersCtrlNToolStripMenuItem.Name = "showNumbersCtrlNToolStripMenuItem";
            this.showNumbersCtrlNToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.showNumbersCtrlNToolStripMenuItem.Text = "Show Numbers           Ctrl+N";
            this.showNumbersCtrlNToolStripMenuItem.Click += new System.EventHandler(this.showNumbersCtrlNToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(232, 6);
            // 
            // decodeAsABase64StringToolStripMenuItem
            // 
            this.decodeAsABase64StringToolStripMenuItem.Name = "decodeAsABase64StringToolStripMenuItem";
            this.decodeAsABase64StringToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.decodeAsABase64StringToolStripMenuItem.Text = "Decode as a Base64 string";
            this.decodeAsABase64StringToolStripMenuItem.Click += new System.EventHandler(this.decodeAsABase64StringToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(232, 6);
            // 
            // removeAllColoursToolStripMenuItem1
            // 
            this.removeAllColoursToolStripMenuItem1.Name = "removeAllColoursToolStripMenuItem1";
            this.removeAllColoursToolStripMenuItem1.Size = new System.Drawing.Size(235, 22);
            this.removeAllColoursToolStripMenuItem1.Text = "Remove all colours";
            this.removeAllColoursToolStripMenuItem1.Click += new System.EventHandler(this.removeAllColoursToolStripMenuItem_Click);
            // 
            // selectionForegroundColourToolStripMenuItem
            // 
            this.selectionForegroundColourToolStripMenuItem.Name = "selectionForegroundColourToolStripMenuItem";
            this.selectionForegroundColourToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.selectionForegroundColourToolStripMenuItem.Text = "Selection Foreground colour...";
            this.selectionForegroundColourToolStripMenuItem.Click += new System.EventHandler(this.setColourToolStripMenuItem_Click);
            // 
            // selectionBackgroundColourToolStripMenuItem
            // 
            this.selectionBackgroundColourToolStripMenuItem.Name = "selectionBackgroundColourToolStripMenuItem";
            this.selectionBackgroundColourToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.selectionBackgroundColourToolStripMenuItem.Text = "Selection Background colour...";
            this.selectionBackgroundColourToolStripMenuItem.Click += new System.EventHandler(this.setSelectionBackgroundToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "rtb";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 27);
            this.panel1.TabIndex = 2;
            // 
            // llblMessage
            // 
            this.llblMessage.AutoSize = true;
            this.llblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblMessage.ForeColor = System.Drawing.Color.Blue;
            this.llblMessage.Location = new System.Drawing.Point(3, 5);
            this.llblMessage.Name = "llblMessage";
            this.llblMessage.Size = new System.Drawing.Size(178, 16);
            this.llblMessage.TabIndex = 0;
            this.llblMessage.Text = "Start by selecting some text...";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "TXT files|*.TXT|LOG files|*.log|All files|*.*";
            this.openFileDialog1.Title = "Import Text File";
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.controlPanel.Controls.Add(this.btnFilterColours);
            this.controlPanel.Controls.Add(this.btnReplace);
            this.controlPanel.Controls.Add(this.btnSearch);
            this.controlPanel.Controls.Add(this.btnMono);
            this.controlPanel.Controls.Add(this.lblSample);
            this.controlPanel.Controls.Add(this.button6);
            this.controlPanel.Controls.Add(this.button5);
            this.controlPanel.Controls.Add(this.button4);
            this.controlPanel.Controls.Add(this.bNext);
            this.controlPanel.Controls.Add(this.bPrev);
            this.controlPanel.Controls.Add(this.forePanel);
            this.controlPanel.Controls.Add(this.backPanel);
            this.controlPanel.Controls.Add(this.btnChangeAll);
            this.controlPanel.Controls.Add(this.btnChangeSingle);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 24);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1045, 48);
            this.controlPanel.TabIndex = 3;
            // 
            // btnFilterColours
            // 
            this.btnFilterColours.BackColor = System.Drawing.Color.White;
            this.btnFilterColours.FlatAppearance.BorderSize = 0;
            this.btnFilterColours.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnFilterColours.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterColours.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilterColours.Location = new System.Drawing.Point(792, 12);
            this.btnFilterColours.Name = "btnFilterColours";
            this.btnFilterColours.Size = new System.Drawing.Size(87, 21);
            this.btnFilterColours.TabIndex = 33;
            this.btnFilterColours.Text = "&Apply filters";
            this.btnFilterColours.UseVisualStyleBackColor = false;
            this.btnFilterColours.Click += new System.EventHandler(this.btnFilterColours_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.BackColor = System.Drawing.Color.White;
            this.btnReplace.FlatAppearance.BorderSize = 0;
            this.btnReplace.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReplace.Location = new System.Drawing.Point(85, 12);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(72, 21);
            this.btnReplace.TabIndex = 32;
            this.btnReplace.Text = "&Replace";
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Location = new System.Drawing.Point(7, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 21);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnMono
            // 
            this.btnMono.Image = ((System.Drawing.Image)(resources.GetObject("btnMono.Image")));
            this.btnMono.Location = new System.Drawing.Point(757, 9);
            this.btnMono.Name = "btnMono";
            this.btnMono.Size = new System.Drawing.Size(29, 28);
            this.btnMono.TabIndex = 30;
            this.btnMono.TabStop = false;
            this.btnMono.Click += new System.EventHandler(this.btnMono_Click);
            // 
            // lblSample
            // 
            this.lblSample.Location = new System.Drawing.Point(532, 11);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(66, 23);
            this.lblSample.TabIndex = 29;
            this.lblSample.Text = "sample";
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.Location = new System.Drawing.Point(419, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(51, 21);
            this.button6.TabIndex = 28;
            this.button6.Text = "123...";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.showNumbersCtrlNToolStripMenuItem_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.Location = new System.Drawing.Point(362, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(51, 21);
            this.button5.TabIndex = 27;
            this.button5.Text = " H";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.inspectAsHorizontalListCtrlHToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.Location = new System.Drawing.Point(331, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(25, 37);
            this.button4.TabIndex = 26;
            this.button4.Text = "V";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.inspectToolStripMenuItem_Click);
            // 
            // bNext
            // 
            this.bNext.BackColor = System.Drawing.Color.White;
            this.bNext.FlatAppearance.BorderSize = 0;
            this.bNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.bNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bNext.Location = new System.Drawing.Point(241, 12);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(72, 21);
            this.bNext.TabIndex = 25;
            this.bNext.Text = "   Next>>";
            this.bNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bNext.UseVisualStyleBackColor = false;
            this.bNext.Click += new System.EventHandler(this.btnNext);
            // 
            // bPrev
            // 
            this.bPrev.BackColor = System.Drawing.Color.White;
            this.bPrev.FlatAppearance.BorderSize = 0;
            this.bPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.bPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPrev.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bPrev.Location = new System.Drawing.Point(163, 12);
            this.bPrev.Name = "bPrev";
            this.bPrev.Size = new System.Drawing.Size(72, 21);
            this.bPrev.TabIndex = 24;
            this.bPrev.Text = "    <<Prev";
            this.bPrev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bPrev.UseVisualStyleBackColor = false;
            this.bPrev.Click += new System.EventHandler(this.btnPrev);
            // 
            // forePanel
            // 
            this.forePanel.BackColor = System.Drawing.Color.Black;
            this.forePanel.Location = new System.Drawing.Point(487, 5);
            this.forePanel.Name = "forePanel";
            this.forePanel.Size = new System.Drawing.Size(25, 24);
            this.forePanel.TabIndex = 23;
            this.forePanel.Click += new System.EventHandler(this.forePanel_Click);
            // 
            // backPanel
            // 
            this.backPanel.BackColor = System.Drawing.Color.White;
            this.backPanel.Location = new System.Drawing.Point(501, 19);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(25, 24);
            this.backPanel.TabIndex = 19;
            this.backPanel.Click += new System.EventHandler(this.backPanel_Click);
            // 
            // btnChangeAll
            // 
            this.btnChangeAll.BackColor = System.Drawing.Color.White;
            this.btnChangeAll.FlatAppearance.BorderSize = 0;
            this.btnChangeAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnChangeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeAll.Image = global::Terminal.Properties.Resources.Gear;
            this.btnChangeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeAll.Location = new System.Drawing.Point(679, 12);
            this.btnChangeAll.Name = "btnChangeAll";
            this.btnChangeAll.Size = new System.Drawing.Size(72, 21);
            this.btnChangeAll.TabIndex = 22;
            this.btnChangeAll.Text = "      &All";
            this.btnChangeAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangeAll.UseVisualStyleBackColor = false;
            this.btnChangeAll.Click += new System.EventHandler(this.btnChangeAll_Click);
            // 
            // btnChangeSingle
            // 
            this.btnChangeSingle.BackColor = System.Drawing.Color.White;
            this.btnChangeSingle.FlatAppearance.BorderSize = 0;
            this.btnChangeSingle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.btnChangeSingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeSingle.Image = global::Terminal.Properties.Resources.Gear;
            this.btnChangeSingle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeSingle.Location = new System.Drawing.Point(601, 12);
            this.btnChangeSingle.Name = "btnChangeSingle";
            this.btnChangeSingle.Size = new System.Drawing.Size(72, 21);
            this.btnChangeSingle.TabIndex = 21;
            this.btnChangeSingle.Text = "   &Single";
            this.btnChangeSingle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangeSingle.UseVisualStyleBackColor = false;
            this.btnChangeSingle.Click += new System.EventHandler(this.btnChangeSingle_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtb);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1045, 399);
            this.panel3.TabIndex = 4;
            // 
            // rtb
            // 
            this.rtb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb.AutoWordSelection = true;
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.ContextMenuStrip = this.contextMenuStrip1;
            this.rtb.Location = new System.Drawing.Point(0, 48);
            this.rtb.Name = "rtb";
            this.rtb.ReadOnly = true;
            this.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtb.ShowSelectionMargin = true;
            this.rtb.Size = new System.Drawing.Size(1045, 351);
            this.rtb.TabIndex = 2;
            this.rtb.Text = "";
            this.rtb.WordWrap = false;
            this.rtb.DoubleClick += new System.EventHandler(this.rtb_DoubleClick);
            // 
            // FrmInspect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 450);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmInspect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInspect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInspect_FormClosing);
            this.Load += new System.EventHandler(this.FrmInspect_Load);
            this.Shown += new System.EventHandler(this.FrmInspect_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnMono)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToRTFFilewithColoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToTXTFiletextOnlyToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label llblMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem decodeAsABase64StringToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem decodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem inspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem inspectAsVerticalListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem inspectAsHorizontalListCtrlHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHorizontalListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNumbersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNumbersCtrlNToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem removeAllColoursToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectionForegroundColourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionBackgroundColourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem searchAndReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTXTFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.Button btnChangeAll;
        private System.Windows.Forms.Button btnChangeSingle;
        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.Button bPrev;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel backPanel;
        private System.Windows.Forms.Panel forePanel;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.PictureBox btnMono;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnFilterColours;
    }
}