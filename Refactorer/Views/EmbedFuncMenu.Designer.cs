namespace Refactorer.Views
{
    partial class EmbedFuncMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMethodName = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Method name";
            // 
            // textBoxMethodName
            // 
            this.textBoxMethodName.Location = new System.Drawing.Point(12, 29);
            this.textBoxMethodName.Name = "textBoxMethodName";
            this.textBoxMethodName.Size = new System.Drawing.Size(152, 20);
            this.textBoxMethodName.TabIndex = 11;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(91, 55);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(73, 23);
            this.buttonStart.TabIndex = 10;
            this.buttonStart.Text = "Embed";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(12, 55);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(73, 23);
            this.cancelBtn.TabIndex = 13;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // EmbedFuncMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMethodName);
            this.Controls.Add(this.buttonStart);
            this.Name = "EmbedFuncMenu";
            this.Text = "EmbedFuncMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMethodName;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button cancelBtn;
    }
}