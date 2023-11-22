using CSharpProjectPractFirst.Model;
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
    public partial class EmbedFuncMenu : Form
    {
        string _code;
        public string Result { get; set; }
        public EmbedFuncMenu(string code)
        {
            InitializeComponent();
            _code = code;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Result = Refactor.EmbedMethod(_code, textBoxMethodName.Text);
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Result = _code;
            this.Close();
        }
    }
}
