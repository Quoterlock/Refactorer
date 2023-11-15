using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactorer.Views
{
    public partial class RenameMethodMenu : Form
    {
        private string _text;
        
        public string ResultText { get; set; }
        public RenameMethodMenu(string text, string name)
        {
            InitializeComponent();
            _text = text;
            oldNameTextBox.Text = name;
            
        }

        private void executeBtn_Click(object sender, EventArgs e)
        {
                try
                {
                    CheckInput();
                    ResultText = Refactorer2810.RenameMethod(
                        oldNameTextBox.Text, 
                        newNameTextBox.Text, 
                        string.Empty, 
                        _text);
                    this.Close();
                } catch (Exception ex)
                {
                    ResultText = _text;
                    MessageBox.Show(ex.Message);
                }
        }

        private bool CheckInput()
        {
            if (!oldNameTextBox.Text.Equals(string.Empty)
                && !newNameTextBox.Text.Equals(string.Empty)
                && !_text.Equals(string.Empty))
            {
                string name = oldNameTextBox.Text;
                if (Char.IsNumber(name[0]) || Parser.ContainsSeparators(name))
                    throw new Exception("Choose correct name!");
                name = newNameTextBox.Text;
                if (Char.IsNumber(name[0]) || Parser.ContainsSeparators(name))
                    throw new Exception("New method name is unacceptable!");
                return true;
            }
            return false;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            ResultText = _text;
            this.Close();
        }
    }
}
