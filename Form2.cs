using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form2 : Form
    {
      //  private List<string>[] CategoryList = new List<string>[2];
        private DBConnect dbConnect;
        public Form2()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
           // CategoryList[0] = new List<string>();
           // CategoryList[1] = new List<string>();
           // UpdateList();
        }
        private Form1 _f4;
        public Form2(Form1 f4)
        {
            InitializeComponent();
            _f4 = f4;

            dbConnect = new DBConnect();
            //CategoryList[0] = new List<string>();
           // CategoryList[1] = new List<string>();
           // UpdateList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length == 13) || (textBox1.Text.Length == 8))
            {
                string ProductName = textBox4.Text.ToString();
                string BarCode = textBox1.Text.ToString();
                if (!dbConnect.CheckExistance("mydb.ProductBase", "Barcode", BarCode))
                {
                    string ShelfLife = textBox5.Text.ToString();
                    string Ccal = textBox2.Text.ToString();
                    string SQLQuery = "INSERT INTO mydb.ProductBase (Barcode, Name, ShelfLife, KCal) values ("
                        + BarCode + ", \"" + ProductName + "\", " + ShelfLife + ", " + Ccal + ")";
                    dbConnect.ExecSQL(SQLQuery);
                    SQLQuery = "INSERT INTO NEED (Name)values(\""+ProductName+"\")";
                    dbConnect.ExecSQL(SQLQuery);
                }
                else MessageBox.Show("Такой штрихкод уже занесен в базу!");
                textBox1.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox2.Clear();
                checkBox1.Checked = false;
            }
            else MessageBox.Show("Некорректный штрихкод!");
        }
       /* private void UpdateList()
        {
            comboBox2.Items.Clear();
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            string SQLQuery = "SELECT * FROM mydb.CathegoryList";
            CategoryList = dbConnect.ReturnColumns(SQLQuery, Columns);
            for (int i = 0; i < CategoryList[0].Count; i++)
                this.comboBox2.Items.Add(CategoryList[1][i]);
            if (comboBox2.Items.Count > 0)
            comboBox2.SelectedItem = comboBox2.Items[0];
        }*/
        private void button1_Click(object sender, EventArgs e)
        {
            _f4.Show();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}