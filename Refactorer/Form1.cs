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
        private const int BUFFER_SIZE = 3;
        private List<string> buffer = new List<string>();

        private int selectedRow = 0;
        private int selectedStart = 0;
        private string selectedText = string.Empty;

        private FileManager fileManager;
        private bool isFileSaved;
        private bool isFileOppened;

        private int lineCount = 1;

        public Form1()
        {
            InitializeComponent();
            fileManager = new FileManager();
            richTextBoxNumbers.SelectionAlignment = HorizontalAlignment.Right;
            isFileSaved = false;
            isFileOppened = false;
            fontSizeNumUD.Value = 8;
            richTextBox.ContextMenuStrip= contextMenuStrip1;
            
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
            richTextBox_TextChanged(richTextBox,EventArgs.Empty);
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
        

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                string value = GetLastFromBuffer();
                if (value != null)
                    richTextBox.Text = value;
            }
        }

        private void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = richTextBox.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = richTextBox.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            richTextBoxNumbers.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            richTextBoxNumbers.Text = "";
            /*richTextBoxNumbers.Width = Width;*/
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line+1; i++)
            {
                richTextBoxNumbers.Text += i + 1 + "\n";
            }
        }
        private void richTextBox1_SelectionChanged(object sender, EventArgs e)    
        {    
            Point pt = richTextBox.GetPositionFromCharIndex(richTextBox.SelectionStart);    
            if (pt.X == 1)    
            {    
                AddLineNumbers();    
            }    
        }    
    
        private void richTextBox1_VScroll(object sender, EventArgs e)    
        {
            AddLineNumbers();
        }    


        private void fontSizeNumUD_ValueChanged(object sender, EventArgs e)
        {
            int fontSize = Convert.ToInt32(fontSizeNumUD.Value);
            if (fontSize > 0)
            {
                richTextBox.Font = new Font("Consolas", fontSize);
                richTextBoxNumbers.Font = new Font("Consolas", fontSize);
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            FileIsUpToDate(false);
            richTextBoxNumbers.Text = "";    
            AddLineNumbers();    
     
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Title = "Open .cpp or .h File";
            openFileDialog.Filter = "C++ Files (*.cpp;*.h)|*.cpp;*.h|All Files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                fileNameLabel.Text = selectedFile;
                try
                {
                    richTextBox.Text = fileManager.Read(selectedFile);
                    isFileOppened = true;
                    FileIsUpToDate(true);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileNameLabel.Text.Equals("(none)"))
            {
                fileManager.Save(fileNameLabel.Text, richTextBox.Text);
                FileIsUpToDate(true);
                MessageBox.Show("All changes are saved");
            }
            else
            {
                MessageBox.Show("Any file isn't opened. Try to \"Save as...\" instead.");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = "Create a File";
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Source Code File (*.cpp)|*.cpp|Header File (*.h)|*.h|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    fileManager.SaveAs(filePath, richTextBox.Text);
                    FileIsUpToDate(true);
                    isFileOppened = true;
                    fileNameLabel.Text = filePath;
                    MessageBox.Show("File created successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void FileIsUpToDate(bool v)
        {
            isFileSaved = v;
            if (v)
                isSavedLable.Text = "True";
            else
                isSavedLable.Text = "False";
        }

        private void AddToBuffer(string text)
        {
            buffer.Add(text);
            if (buffer.Count > BUFFER_SIZE)
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
    }
}
