namespace Refactorer.Views
{
    partial class RenameMethodMenu
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
            this.oldNameTextBox = new System.Windows.Forms.TextBox();
            this.newNameTextBox = new System.Windows.Forms.TextBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.executeBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // oldNameTextBox
            // 
            this.oldNameTextBox.Location = new System.Drawing.Point(12, 30);
            this.oldNameTextBox.Name = "oldNameTextBox";
            this.oldNameTextBox.Size = new System.Drawing.Size(156, 22);
            this.oldNameTextBox.TabIndex = 0;
            // 
            // newNameTextBox
            // 
            this.newNameTextBox.Location = new System.Drawing.Point(12, 74);
            this.newNameTextBox.Name = "newNameTextBox";
            this.newNameTextBox.Size = new System.Drawing.Size(156, 22);
            this.newNameTextBox.TabIndex = 1;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(12, 108);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // executeBtn
            // 
            this.executeBtn.Location = new System.Drawing.Point(93, 108);
            this.executeBtn.Name = "executeBtn";
            this.executeBtn.Size = new System.Drawing.Size(75, 23);
            this.executeBtn.TabIndex = 3;
            this.executeBtn.Text = "Rename";
            this.executeBtn.UseVisualStyleBackColor = true;
            this.executeBtn.Click += new System.EventHandler(this.executeBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Function name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "New name";
            // 
            // RenameMethodMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 144);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.executeBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.newNameTextBox);
            this.Controls.Add(this.oldNameTextBox);
            this.Name = "RenameMethodMenu";
            this.Text = "Rename";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox oldNameTextBox;
        private System.Windows.Forms.TextBox newNameTextBox;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button executeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}