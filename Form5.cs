using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form5 : Form
    {
        private DBConnect dbConnect;
        private List<string>[] ShopList = new List<string>[2];
        public Form5()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            ShopList[0] = new List<string>();
            ShopList[1] = new List<string>();
            UpdateList();

        }
        private Form1 _f10;
        public Form5(Form1 f10)
        {
            InitializeComponent();
            _f10 = f10;
            dbConnect = new DBConnect();
            ShopList[0] = new List<string>();
            ShopList[1] = new List<string>();
            UpdateList();
        }

        private void UpdateList()
        {
            comboBox1.Items.Clear();
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            string SQLQuery = "SELECT * FROM mydb.ShopList";
            ShopList = dbConnect.ReturnColumns(SQLQuery, Columns);
            for(int i = 0;i<ShopList[0].Count;i++)
                this.comboBox1.Items.Add(ShopList[1][i]);
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _f10.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool AlreadyExist = false;
            for (int i = 0; i < comboBox1.Items.Count; i++)
                if (comboBox1.Items[i].Equals(textBox4.Text))
                    AlreadyExist = true;
            if (!AlreadyExist)
            {
                string sqlquery = "INSERT INTO mydb.ShopList (Name) values (\"" + textBox4.Text.ToString() + "\")";
                dbConnect.ExecSQL(sqlquery);
                UpdateList();
            }
            else
            {
                MessageBox.Show("Такое название уже существует");
            }
            textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SQLQuery = "DELETE FROM mydb.ShopList WHERE (id = " + ShopList[0][comboBox1.SelectedIndex] + ")";
            dbConnect.ExecSQL(SQLQuery);
            UpdateList();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}