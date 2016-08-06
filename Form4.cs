using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form4 : Form
    {
        private DBConnect dbConnect;
        private Form1 _f50;
        public Form4(Form1 f50)
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            _f50 = f50;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            _f50.Show();
            _f50.UpdateProductList();
        
            this.Close();
         }
       
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                string Id = Row.Cells[0].Value.ToString();
                string SQLQuery = "DELETE FROM ProductFridge WHERE (Id = " + Id + ")";
                dbConnect.ExecSQL(SQLQuery);
                dataGridView1.Rows.RemoveAt(Row.Index);
            }
            _f50.Show();
            _f50.UpdateProductList();
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
        public void SetProductTimer(List<string>[] ProductTimer)
        {
            
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ProductTimer[0].Count;i++ )
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = ProductTimer[0][i];
                dataGridView1.Rows[i].Cells[1].Value = ProductTimer[1][i];
                dataGridView1.Rows[i].Cells[2].Value = ProductTimer[2][i].Substring(0, ProductTimer[2][i].IndexOf('.'));

            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                string Id = Row.Cells[0].Value.ToString();
                string SQLQuery = "DELETE FROM ProductFridge WHERE (Id = "+ Id + ")";
                dbConnect.ExecSQL(SQLQuery);
                dataGridView1.Rows.RemoveAt(Row.Index);
            }
        }

        private void пустьПолежитToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(Row.Index);
            }
        }
    }
}