using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form9 : Form
    {
        private DBConnect dbConnect;
        public Form9()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            DateTime saveNow = DateTime.Now;
            dateTimePicker1.Value = saveNow;
            dateTimePicker1.MaxDate = saveNow;

        }
        private Form1 _f30;
        public Form9(Form1 f30)
        {
            InitializeComponent();
            _f30 = f30;
            dbConnect = new DBConnect();

            DateTime saveNow = DateTime.Now;
            dateTimePicker1.Value = saveNow;
            dateTimePicker1.MaxDate = saveNow;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _f30.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Name = textBox4.Text.ToString();
            string Rub = textBox1.Text.ToString();
            string DohDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string SQLQuery = "INSERT INTO mydb.Budjet (Name, Price, Data) values (\""
           + Name + "\", \"" + Rub + "\", \"" + DohDate + "\")";
            dbConnect.ExecSQL(SQLQuery);
            textBox4.Clear();
            textBox1.Clear();
            _f30.Show();
            this.Close();
        }

        private void UpdateList()
        {
            
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }
    }
}