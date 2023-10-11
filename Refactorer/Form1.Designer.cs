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
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 29);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(776, 409);
            this.textBox.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameMethodToolStripMenuItem,
            this.extractConstantToolStripMenuItem,
            this.deleteParamMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // renameMethodToolStripMenuItem
            // 
            this.renameMethodToolStripMenuItem.Name = "renameMethodToolStripMenuItem";
            this.renameMethodToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameMethodToolStripMenuItem.Text = "Rename Method";
            this.renameMethodToolStripMenuItem.Click += new System.EventHandler(this.renameMethodToolStripMenuItem_Click);
            // 
            // extractConstantToolStripMenuItem
            // 
            this.extractConstantToolStripMenuItem.Name = "extractConstantToolStripMenuItem";
            this.extractConstantToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extractConstantToolStripMenuItem.Text = "Extract Constant";
            this.extractConstantToolStripMenuItem.Click += new System.EventHandler(this.extractConstantToolStripMenuItem_Click);
            // 
            // deleteParamMenuItem
            // 
            this.deleteParamMenuItem.Name = "deleteParamMenuItem";
            this.deleteParamMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteParamMenuItem.Text = "Delete Param";
            this.deleteParamMenuItem.Click += new System.EventHandler(this.deleteParamMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
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
    }
}

