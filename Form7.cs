using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form7 : Form
    {
        private DBConnect dbConnect;
        private List<string>[] ProductBase = new List<string>[8];
       // private List<string>[] CathegoryList = new List<string>[2];

        public Form7()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
           /* for (int i = 0; i < 2; i++)
            {
                CathegoryList[i] = new List<string>();
            }*/
            UpdateProductBase();
        }
         private Form1 _f21;
        public Form7(Form1 f21)
        {
            InitializeComponent();
            _f21 = f21;
            dbConnect = new DBConnect();
           /* for (int i = 0; i < 2; i++)
            {
                CathegoryList[i] = new List<string>();
            }*/
            UpdateProductBase();
        }
      /*  private void UpdateCathegoryList()
        {
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            string SQLQuery = "SELECT * FROM mydb.CathegoryList";
            CathegoryList = dbConnect.ReturnColumns(SQLQuery, Columns);
        }*/
        private void UpdateProductBase()
        {
           // UpdateCathegoryList();
            List<string> Columns = new List<string>();
            dataGridView1.Rows.Clear();
            Columns.Add("id");
            Columns.Add("Barcode");
            Columns.Add("Name");
          //  Columns.Add("Measure");
          //  Columns.Add("Cathegory");
            Columns.Add("ShelfLife");
           // Columns.Add("Buy");
            Columns.Add("KCal");
            string SQLQuery = "SELECT * FROM mydb.ProductBase";
            ProductBase = dbConnect.ReturnColumns(SQLQuery, Columns);
            for (int i = 0; i < ProductBase[0].Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = ProductBase[1][i];
                dataGridView1.Rows[i].Cells[1].Value = ProductBase[2][i];
                dataGridView1.Rows[i].Cells[2].Value = ProductBase[4][i];
               // dataGridView1.Rows[i].Cells[3].Value = CathegoryList[1][FindIndex(ProductBase[4][i], CathegoryList[0])];
                dataGridView1.Rows[i].Cells[3].Value = ProductBase[3][i];
            }
        }
        private int FindIndex(string s, List<string> Arr)
        {
            int result = 0;
            for (int i = 0; i < Arr.Count; i++)
            {
                if (s == Arr[i]) result = i;
            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _f21.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string SelectedElement = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Form8 f23 = new Form8(this);
            f23.SetBarcode(SelectedElement);
            f23.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (!textBox4.Text.ToString().Equals("")&&!textBox4.Text.ToString().Equals(" "))
            {
                int j = 0;
                for (int i = 0; i < ProductBase[0].Count; i++)
                {
                    if (ProductBase[1][i].Contains(textBox4.Text.ToString()) || ProductBase[2][i].Contains(textBox4.Text.ToString()))
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[j].Cells[0].Value = ProductBase[1][i];
                        dataGridView1.Rows[j].Cells[1].Value = ProductBase[2][i];
                        dataGridView1.Rows[j].Cells[2].Value = ProductBase[4][i];
                      //  dataGridView1.Rows[j].Cells[3].Value = CathegoryList[1][FindIndex(ProductBase[4][i], CathegoryList[0])];
                        dataGridView1.Rows[j].Cells[3].Value = ProductBase[3][i];
                        j++;
                    }
                }
            }else {
                            
                for (int i = 0; i < ProductBase[0].Count; i++)
                {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = ProductBase[1][i];
                dataGridView1.Rows[i].Cells[1].Value = ProductBase[2][i];
                dataGridView1.Rows[i].Cells[2].Value = ProductBase[4][i];
               // dataGridView1.Rows[i].Cells[3].Value = CathegoryList[1][FindIndex(ProductBase[4][i], CathegoryList[0])];
                dataGridView1.Rows[i].Cells[3].Value = ProductBase[3][i];
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}