using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactorer.Views
{
    public partial class ExtractConstantMenu : Form
    {
        private string _text;
        private int _row;
        private string _selectedValue;
        public string ResultText { get; set; }
        public ExtractConstantMenu(string text, int row, string selectedValue)
        {
            InitializeComponent();
            _text = text;
            _row = row;
            _selectedValue = selectedValue;

            selectedRowLabel.Text = _row.ToString();
            constValueTextBox.Text = _selectedValue;
        }

        private void selectedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(selectedOnlyCheckBox.Checked)
                allConstsCheckBox.Checked = false;
            else allConstsCheckBox.Checked = true;
        }

        private void allConstsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(allConstsCheckBox.Checked)
                selectedOnlyCheckBox.Checked = false;
            else selectedOnlyCheckBox.Checked = true;
        }

        private void refactorBtn_Click(object sender, EventArgs e)
        {
                try
                {
                    CheckInput();
                    ResultText = Refactorer2810.ExtractConstant(
                        constValueTextBox.Text,
                        constNameTextBox.Text,
                        _row,
                        _text,
                        allConstsCheckBox.Checked);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private bool CheckInput()
        {
            if (_row >= 0 
                && _text != null 
                && constNameTextBox.Text != null 
                && constNameTextBox.Text != string.Empty 
                && constValueTextBox.Text != null
                && constValueTextBox.Text != string.Empty)
            {
                string name = constNameTextBox.Text;
                if (Char.IsNumber(name[0]) || Parser.ContainsSeparators(name))
                    throw new Exception("Constant name is unacceptable!");
                return true;
            }
            throw new Exception("Fill all text fields!");
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            ResultText = _text;
            this.Close();
        }
    }
}
