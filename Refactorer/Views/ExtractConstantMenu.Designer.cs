namespace Refactorer.Views
{
    partial class ExtractConstantMenu
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
            this.constValueTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.constNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selectedRowLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.allConstsCheckBox = new System.Windows.Forms.CheckBox();
            this.selectedOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.refactorBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // constValueTextBox
            // 
            this.constValueTextBox.Location = new System.Drawing.Point(12, 42);
            this.constValueTextBox.Name = "constValueTextBox";
            this.constValueTextBox.Size = new System.Drawing.Size(154, 22);
            this.constValueTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Const value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Const name";
            // 
            // constNameTextBox
            // 
            this.constNameTextBox.Location = new System.Drawing.Point(15, 94);
            this.constNameTextBox.Name = "constNameTextBox";
            this.constNameTextBox.Size = new System.Drawing.Size(154, 22);
            this.constNameTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Selected row:";
            // 
            // selectedRowLabel
            // 
            this.selectedRowLabel.AutoSize = true;
            this.selectedRowLabel.Location = new System.Drawing.Point(106, 128);
            this.selectedRowLabel.Name = "selectedRowLabel";
            this.selectedRowLabel.Size = new System.Drawing.Size(14, 16);
            this.selectedRowLabel.TabIndex = 7;
            this.selectedRowLabel.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.allConstsCheckBox);
            this.groupBox1.Controls.Add(this.selectedOnlyCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(202, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 134);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extract mode";
            // 
            // allConstsCheckBox
            // 
            this.allConstsCheckBox.AutoSize = true;
            this.allConstsCheckBox.Location = new System.Drawing.Point(6, 47);
            this.allConstsCheckBox.Name = "allConstsCheckBox";
            this.allConstsCheckBox.Size = new System.Drawing.Size(158, 20);
            this.allConstsCheckBox.TabIndex = 8;
            this.allConstsCheckBox.Text = "All constants in scope";
            this.allConstsCheckBox.UseVisualStyleBackColor = true;
            this.allConstsCheckBox.CheckedChanged += new System.EventHandler(this.allConstsCheckBox_CheckedChanged);
            // 
            // selectedOnlyCheckBox
            // 
            this.selectedOnlyCheckBox.AutoSize = true;
            this.selectedOnlyCheckBox.Checked = true;
            this.selectedOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectedOnlyCheckBox.Location = new System.Drawing.Point(6, 21);
            this.selectedOnlyCheckBox.Name = "selectedOnlyCheckBox";
            this.selectedOnlyCheckBox.Size = new System.Drawing.Size(111, 20);
            this.selectedOnlyCheckBox.TabIndex = 7;
            this.selectedOnlyCheckBox.Text = "Selected only";
            this.selectedOnlyCheckBox.UseVisualStyleBackColor = true;
            this.selectedOnlyCheckBox.CheckedChanged += new System.EventHandler(this.selectedOnlyCheckBox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.constNameTextBox);
            this.groupBox2.Controls.Add(this.constValueTextBox);
            this.groupBox2.Controls.Add(this.selectedRowLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 163);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input values";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(202, 152);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(81, 23);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // refactorBtn
            // 
            this.refactorBtn.Location = new System.Drawing.Point(289, 152);
            this.refactorBtn.Name = "refactorBtn";
            this.refactorBtn.Size = new System.Drawing.Size(84, 23);
            this.refactorBtn.TabIndex = 10;
            this.refactorBtn.Text = "Refactor";
            this.refactorBtn.UseVisualStyleBackColor = true;
            this.refactorBtn.Click += new System.EventHandler(this.refactorBtn_Click);
            // 
            // ExtractConstantMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 188);
            this.Controls.Add(this.refactorBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExtractConstantMenu";
            this.Text = "ExtractConstantMenu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox constValueTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox constNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label selectedRowLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button refactorBtn;
        private System.Windows.Forms.CheckBox allConstsCheckBox;
        private System.Windows.Forms.CheckBox selectedOnlyCheckBox;
    }
}