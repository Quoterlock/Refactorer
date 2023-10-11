using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.ContextMenuStrip = contextMenuStrip1;
            this.textBox.ContextMenuStrip= contextMenuStrip1;
            string inputText = @"
                void someFunc()
                {
                    int func = 1;
                    func();
                }

                void func() 
                {
                }";
            textBox.Text = inputText;
        }

        private void renameMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rename Method");
        }

        private void extractConstantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Extract Constant");
        }

        private void deleteParamMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete unused params");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
