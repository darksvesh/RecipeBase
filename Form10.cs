using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    
    public partial class Form10 : Form
    {
        private DBConnect dbConnect;
        public Form10()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            DateTime saveNow = DateTime.Now;
            //saveNow.ToString("dd.MM.yyyy");
            dateTimePicker1.Value = saveNow;
            dateTimePicker1.MaxDate = saveNow;
        }
        private Form1 _f33;
        public Form10(Form1 f33)
        {
            InitializeComponent();
            _f33 = f33;
            dbConnect = new DBConnect();

            DateTime saveNow = DateTime.Now;
            //saveNow.ToString("dd.MM.yyyy");
            dateTimePicker1.Value = saveNow;
            dateTimePicker1.MaxDate = saveNow;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _f33.Show();
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
           + Name + "\", \"-" + Rub + "\", \"" + DohDate + "\")";
            dbConnect.ExecSQL(SQLQuery);
            textBox4.Clear();
            textBox1.Clear();
            _f33.Show();
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }
    }
}