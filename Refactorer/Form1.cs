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

namespace Refactorer
{
    public partial class Form1 : Form
    {
        private const int BUFFER_SIZE = 10;
        int selectedRow = 0;
        int selectedStart = 0;
        string selectedText = string.Empty;
        List<string> buffer = new List<string>();
        
        public Form1()
        {
            InitializeComponent();
            
            fontSizeNumUD.Value = 10;
            this.richTextBox.ContextMenuStrip= contextMenuStrip1;
            
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
            richTextBox.Text = inputText;
        }

        private void renameMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInput();
            var menu = new RenameMethodMenu(richTextBox.Text, selectedText);
            menu.ShowDialog();

            AddToBuffer(richTextBox.Text.ToString());
            richTextBox.Text = menu.ResultText;
        }

        private void extractConstantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInput();
            var menu = new ExtractConstantMenu(richTextBox.Text, selectedRow, selectedText);
            menu.ShowDialog();

            AddToBuffer(richTextBox.Text.ToString());
            richTextBox.Text = menu.ResultText;
        }

        private void deleteParamMenuItem_Click(object sender, EventArgs e)
        {
            AddToBuffer(richTextBox.Text.ToString());
            richTextBox.Text = Refactorer2810.RemoveUnusedParameters(richTextBox.Text);
        }

        private void fontSizeNumUD_ValueChanged(object sender, EventArgs e)
        {
            int fontSize = Convert.ToInt32(fontSizeNumUD.Value);
            if (fontSize > 0)
                richTextBox.Font = new Font("Consolas", fontSize);
        }

        private void AddToBuffer(string text)
        {
            buffer.Add(text);
            if(buffer.Count > BUFFER_SIZE)
                buffer.RemoveAt(0);
        }
        private string GetLastFromBuffer()
        {
            string last;
            if (buffer.Count > 0)
            {
                last = buffer.Last();
                buffer.RemoveAt(buffer.Count - 1);
            }
            else last = null;
            return last;
        }

        private void GetInput()
        {
            selectedText = richTextBox.SelectedText;
            selectedStart = richTextBox.SelectionStart;
            selectedRow = richTextBox.GetLineFromCharIndex(selectedStart);
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                string value = GetLastFromBuffer();
                if (value != null)
                    richTextBox.Text = value;
            }
        }
    }
}
