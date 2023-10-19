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

            if(CheckInput())
            {
                try
                {
                    ResultText = Refactorer2810.RenameMethod(
                        oldNameTextBox.Text, 
                        newNameTextBox.Text, 
                        string.Empty, 
                        _text);
                    this.Close();
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Fill all text fields!");
        }

        private bool CheckInput()
        {
            return true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
