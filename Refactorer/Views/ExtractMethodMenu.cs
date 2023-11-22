using CSharpProjectPractFirst.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactorer.Views
{
    public partial class ExtractMethodMenu : Form
    {
        private string _code; 
        private string _funcBody;
        public string Result { get; set; }
        public ExtractMethodMenu(string code, string funcBody)
        {
            InitializeComponent();
            _code = code;
            _funcBody = funcBody;
            textBoxMethodBody.Text = _funcBody;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInput();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string parameters = string.Empty;
            if (textBoxMethodParams.Text.Length == 0)
            {
                parameters = "void void";
            }
            else
            {
                parameters = textBoxMethodParams.Text;
            }
            string[] splitParams = parameters.Split(',');
            try
            {
                Result = Refactor.GetMethod(_code, textBoxMethodType.Text, textBoxMethodName.Text, new List<string>(splitParams), _funcBody);
            } catch(Exception ex)
            {
                MessageBox.Show("Exception occured. Please enter correct params or check input value.");
                return;
            }
            this.Close();
        }

        private void CheckInput()
        {
            if (Parser.IsReservedWord(textBoxMethodName.Text) || Parser.ContainsSeparators(textBoxMethodName.Text))
            {
                throw new Exception("Enter correct method name, PLEASE!");
            }

            string[] types = new string[] { "void", "int", "double", "bool", "float", "string", "byte", "long" };
            if(Parser.IsReservedWord(textBoxMethodType.Text))
            {
                foreach (var type in types)
                {
                    if (textBoxMethodType.Text.Equals(type))
                        return;
                }
                throw new Exception("Enter correct return value, PLEASE!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = _code;
            this.Close();
        }
    }
}
