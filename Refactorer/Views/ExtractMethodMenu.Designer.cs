namespace Refactorer.Views
{
    partial class ExtractMethodMenu
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMethodBody = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMethodParams = new System.Windows.Forms.TextBox();
            this.textBoxMethodType = new System.Windows.Forms.TextBox();
            this.textBoxMethodName = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Method body";
            // 
            // textBoxMethodBody
            // 
            this.textBoxMethodBody.Enabled = false;
            this.textBoxMethodBody.Location = new System.Drawing.Point(12, 112);
            this.textBoxMethodBody.Multiline = true;
            this.textBoxMethodBody.Name = "textBoxMethodBody";
            this.textBoxMethodBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMethodBody.Size = new System.Drawing.Size(213, 59);
            this.textBoxMethodBody.TabIndex = 21;
            this.textBoxMethodBody.TextChanged += new System.EventHandler(this.textBoxMethodBody_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Method params";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Method type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Method name";
            // 
            // textBoxMethodParams
            // 
            this.textBoxMethodParams.Location = new System.Drawing.Point(12, 64);
            this.textBoxMethodParams.Name = "textBoxMethodParams";
            this.textBoxMethodParams.Size = new System.Drawing.Size(110, 20);
            this.textBoxMethodParams.TabIndex = 17;
            // 
            // textBoxMethodType
            // 
            this.textBoxMethodType.Location = new System.Drawing.Point(12, 38);
            this.textBoxMethodType.Name = "textBoxMethodType";
            this.textBoxMethodType.Size = new System.Drawing.Size(110, 20);
            this.textBoxMethodType.TabIndex = 16;
            // 
            // textBoxMethodName
            // 
            this.textBoxMethodName.Location = new System.Drawing.Point(12, 12);
            this.textBoxMethodName.Name = "textBoxMethodName";
            this.textBoxMethodName.Size = new System.Drawing.Size(110, 20);
            this.textBoxMethodName.TabIndex = 15;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(121, 177);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(104, 23);
            this.buttonStart.TabIndex = 14;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExtractMethodMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 221);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMethodBody);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMethodParams);
            this.Controls.Add(this.textBoxMethodType);
            this.Controls.Add(this.textBoxMethodName);
            this.Controls.Add(this.buttonStart);
            this.Name = "ExtractMethodMenu";
            this.Text = "ExtractMethodMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMethodBody;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMethodParams;
        private System.Windows.Forms.TextBox textBoxMethodType;
        private System.Windows.Forms.TextBox textBoxMethodName;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button button1;
    }
}