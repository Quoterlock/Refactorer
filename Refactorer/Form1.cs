using Refactorer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            string selectedText = textBox.SelectedText;
            int selectedStart = textBox.SelectionStart;
            int selectedRow = textBox.GetLineFromCharIndex(selectedStart);

            var menu = new RenameMethodMenu(textBox.Text, selectedText);
            menu.ShowDialog();
            textBox.Text = menu.ResultText;
        }

        private void extractConstantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedText = textBox.SelectedText;
            int selectedStart = textBox.SelectionStart;
            int selectedRow = textBox.GetLineFromCharIndex(selectedStart);

            var menu = new ExtractConstantMenu(textBox.Text, selectedRow, selectedText);
            menu.ShowDialog();
            textBox.Text = menu.ResultText;
        }

        private void deleteParamMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete unused params");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
