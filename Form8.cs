using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form8 : Form
    {
        private List<string>[] ShopList = new List<string>[2];
        private DBConnect dbConnect;
        private string BarCode;
        public Form8()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            ShopList[0] = new List<string>();
            ShopList[1] = new List<string>();
            UpdateList();
            DateTime saveNow = DateTime.Now;
            //saveNow.ToString("dd.MM.yyyy");

            dateTimePicker1.MaxDate = saveNow;
            dateTimePicker2.MaxDate = saveNow;
            dateTimePicker1.Value = saveNow;
            dateTimePicker2.Value = saveNow;

        }
         private Form7 _f24;
        public Form8(Form7 f24)
        {
            InitializeComponent();
            _f24 = f24;
            dbConnect = new DBConnect();
            ShopList[0] = new List<string>();
            ShopList[1] = new List<string>();
            UpdateList();
            DateTime saveNow = DateTime.Now;
            //saveNow.ToString("dd.MM.yyyy");

            dateTimePicker1.MaxDate = saveNow;
            dateTimePicker2.MaxDate = saveNow;
            dateTimePicker1.Value = saveNow;
            dateTimePicker2.Value = saveNow;
            

        }

        public void SetBarcode(string BarCode){
            this.BarCode = BarCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _f24.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string BuyDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string CreateDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string PriceNow = textBox2.Text.ToString();
            string SQLQuery = "INSERT INTO mydb.ProductFridge (Barcode, BuyDate, Shopid, Price, CreateDate) values (\""
            + BarCode + "\", \"" + BuyDate + "\", \"" + ShopList[0][comboBox2.SelectedIndex] +"\", \"" + PriceNow + "\", \"" + CreateDate +"\")";
            dbConnect.ExecSQL(SQLQuery);
            //dateTimePicker1. Clear();
            //dateTimePicker2.Clear();
            textBox2.Clear();
            
            this.Close();
            _f24.Close();
        }
        private void UpdateList()
        {
            comboBox2.Items.Clear();
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            string SQLQuery = "SELECT * FROM mydb.ShopList";
            ShopList = dbConnect.ReturnColumns(SQLQuery, Columns);
            for (int i = 0; i < ShopList[0].Count; i++)
                this.comboBox2.Items.Add(ShopList[1][i]);
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedItem = comboBox2.Items[0];
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MaxDate = dateTimePicker1.Value;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }
}