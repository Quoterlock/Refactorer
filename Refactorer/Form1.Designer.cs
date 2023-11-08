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
            this.textBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractConstantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteParamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontSizeNumUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeNumUD)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(10, 12);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(782, 408);
            this.textBox.TabIndex = 0;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameMethodToolStripMenuItem,
            this.extractConstantToolStripMenuItem,
            this.deleteParamMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(192, 70);
            // 
            // renameMethodToolStripMenuItem
            // 
            this.renameMethodToolStripMenuItem.Name = "renameMethodToolStripMenuItem";
            this.renameMethodToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.renameMethodToolStripMenuItem.Text = "Rename method";
            this.renameMethodToolStripMenuItem.Click += new System.EventHandler(this.renameMethodToolStripMenuItem_Click);
            // 
            // extractConstantToolStripMenuItem
            // 
            this.extractConstantToolStripMenuItem.Name = "extractConstantToolStripMenuItem";
            this.extractConstantToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.extractConstantToolStripMenuItem.Text = "Extract constant";
            this.extractConstantToolStripMenuItem.Click += new System.EventHandler(this.extractConstantToolStripMenuItem_Click);
            // 
            // deleteParamMenuItem
            // 
            this.deleteParamMenuItem.Name = "deleteParamMenuItem";
            this.deleteParamMenuItem.Size = new System.Drawing.Size(191, 22);
            this.deleteParamMenuItem.Text = "Delete unused params";
            this.deleteParamMenuItem.Click += new System.EventHandler(this.deleteParamMenuItem_Click);
            // 
            // fontSizeNumUD
            // 
            this.fontSizeNumUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fontSizeNumUD.Location = new System.Drawing.Point(60, 424);
            this.fontSizeNumUD.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fontSizeNumUD.Name = "fontSizeNumUD";
            this.fontSizeNumUD.Size = new System.Drawing.Size(34, 20);
            this.fontSizeNumUD.TabIndex = 1;
            this.fontSizeNumUD.ValueChanged += new System.EventHandler(this.fontSizeNumUD_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 426);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Font Size";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fontSizeNumUD);
            this.Controls.Add(this.textBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeNumUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renameMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractConstantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteParamMenuItem;
        private System.Windows.Forms.NumericUpDown fontSizeNumUD;
        private System.Windows.Forms.Label label1;
    }
}

