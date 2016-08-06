using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
         private Form1 _f12;
        public Form6(Form1 f12)
        {
            InitializeComponent();
            _f12 = f12;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _f12.Show();
            this.Close();
        }
    }
}