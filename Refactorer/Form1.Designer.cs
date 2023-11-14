namespace Refactorer
{
    partial class Form1
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractConstantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteParamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontSizeNumUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.richTextBoxNumbers = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.isSavedLable = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeNumUD)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.renameMethodToolStripMenuItem, this.extractConstantToolStripMenuItem, this.deleteParamMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(331, 112);
            // 
            // renameMethodToolStripMenuItem
            // 
            this.renameMethodToolStripMenuItem.Name = "renameMethodToolStripMenuItem";
            this.renameMethodToolStripMenuItem.Size = new System.Drawing.Size(330, 36);
            this.renameMethodToolStripMenuItem.Text = "Rename method";
            this.renameMethodToolStripMenuItem.Click += new System.EventHandler(this.renameMethodToolStripMenuItem_Click);
            // 
            // extractConstantToolStripMenuItem
            // 
            this.extractConstantToolStripMenuItem.Name = "extractConstantToolStripMenuItem";
            this.extractConstantToolStripMenuItem.Size = new System.Drawing.Size(330, 36);
            this.extractConstantToolStripMenuItem.Text = "Extract constant";
            this.extractConstantToolStripMenuItem.Click += new System.EventHandler(this.extractConstantToolStripMenuItem_Click);
            // 
            // deleteParamMenuItem
            // 
            this.deleteParamMenuItem.Name = "deleteParamMenuItem";
            this.deleteParamMenuItem.Size = new System.Drawing.Size(330, 36);
            this.deleteParamMenuItem.Text = "Delete unused params";
            this.deleteParamMenuItem.Click += new System.EventHandler(this.deleteParamMenuItem_Click);
            // 
            // fontSizeNumUD
            // 
            this.fontSizeNumUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fontSizeNumUD.Location = new System.Drawing.Point(232, 1280);
            this.fontSizeNumUD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fontSizeNumUD.Name = "fontSizeNumUD";
            this.fontSizeNumUD.Size = new System.Drawing.Size(68, 31);
            this.fontSizeNumUD.TabIndex = 1;
            this.fontSizeNumUD.ValueChanged += new System.EventHandler(this.fontSizeNumUD_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 1145);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Font Size";
            // 
            // richTextBox
            // 
            this.richTextBox.AcceptsTab = true;
            this.richTextBox.AutoWordSelection = true;
            this.richTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.Location = new System.Drawing.Point(80, 52);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox.Size = new System.Drawing.Size(986, 1071);
            this.richTextBox.TabIndex = 3;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = false;
            this.richTextBox.VScroll += new System.EventHandler(this.richTextBox1_VScroll);
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            this.richTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
            this.richTextBox.StyleChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 5, 0, 5);
            this.menuStrip1.Size = new System.Drawing.Size(1083, 46);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.openFileToolStripMenuItem, this.saveAsToolStripMenuItem, this.saveToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(221, 36);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(221, 36);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(221, 36);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 1145);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Opened file: ";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(447, 1145);
            this.fileNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(74, 25);
            this.fileNameLabel.TabIndex = 6;
            this.fileNameLabel.Text = "(none)";
            // 
            // richTextBoxNumbers
            // 
            this.richTextBoxNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxNumbers.Location = new System.Drawing.Point(16, 52);
            this.richTextBoxNumbers.Name = "richTextBoxNumbers";
            this.richTextBoxNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBoxNumbers.Size = new System.Drawing.Size(56, 1070);
            this.richTextBoxNumbers.TabIndex = 7;
            this.richTextBoxNumbers.Text = "";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(862, 1145);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Saved:";
            // 
            // isSavedLable
            // 
            this.isSavedLable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.isSavedLable.AutoSize = true;
            this.isSavedLable.Location = new System.Drawing.Point(936, 1145);
            this.isSavedLable.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.isSavedLable.Name = "isSavedLable";
            this.isSavedLable.Size = new System.Drawing.Size(65, 25);
            this.isSavedLable.TabIndex = 9;
            this.isSavedLable.Text = "False";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1083, 1192);
            this.Controls.Add(this.isSavedLable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBoxNumbers);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fontSizeNumUD);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeNumUD)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.RichTextBox richTextBoxNumbers;

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renameMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractConstantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteParamMenuItem;
        private System.Windows.Forms.NumericUpDown fontSizeNumUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label isSavedLable;
    }
}

