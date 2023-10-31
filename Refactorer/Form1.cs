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
        int selectedRow = 0;
        int selectedStart = 0;
        string selectedText = string.Empty; 
        public Form1()
        {
            InitializeComponent();
            fontSizeNumUD.Value = 10;
            //this.ContextMenuStrip = contextMenuStrip1;
            this.textBox.ContextMenuStrip= contextMenuStrip1;
            string inputText = @"
                void someFunc()
                {
                    int func = 10;
                    func();
                }

                void func(int param) 
                {
                    int result = 10 + 4; // param
                    /* param /*

                    /* 
                        param
                    */
                }";
            textBox.Text = inputText;
        }

        private void renameMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInput();
            var menu = new RenameMethodMenu(textBox.Text, selectedText);
            menu.ShowDialog();
            textBox.Text = menu.ResultText;
        }

        private void extractConstantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInput();
            var menu = new ExtractConstantMenu(textBox.Text, selectedRow, selectedText);
            menu.ShowDialog();
            textBox.Text = menu.ResultText;
        }

        private void deleteParamMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = Refactorer2810.RemoveUnusedParameters(textBox.Text);
        }

        private void GetInput()
        {
            selectedText = textBox.SelectedText;
            selectedStart = textBox.SelectionStart;
            selectedRow = textBox.GetLineFromCharIndex(selectedStart);
        }

        private void fontSizeNumUD_ValueChanged(object sender, EventArgs e)
        {
            int fontSize = Convert.ToInt32(fontSizeNumUD.Value);
            if (fontSize > 0)
                textBox.Font = new Font("Consolas", fontSize);
        }
    }
}
