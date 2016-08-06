using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form12 : Form
    {
        private DBConnect dbConnect;
        private List<string>[] Ingedients = new List<string>[4];
        private List<string>[] Pictures = new List<string>[3];
        private string Id;
        private Image CurrentImage;
        public Form12(string Id)
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            Ingedients[0] = new List<string>();
            Ingedients[1] = new List<string>();
            Ingedients[2] = new List<string>();
            Ingedients[3] = new List<string>();
            Pictures[0] = new List<string>();
            Pictures[1] = new List<string>();
            Pictures[2] = new List<string>();
            InitThisSheet(Id);
        }
        private Form1 _f40;
        public Form12(Form1 f40,string Id)
        {
            InitializeComponent();
            _f40 = f40;
            dbConnect = new DBConnect();

            Ingedients[0] = new List<string>();
            Ingedients[1] = new List<string>();
            Ingedients[2] = new List<string>();
            Ingedients[3] = new List<string>();
            Pictures[0] = new List<string>();
            Pictures[1] = new List<string>();
            Pictures[2] = new List<string>();
            InitThisSheet(Id);
        }
        private void InitThisSheet(string Id)
        {

            this.Id = Id;
            string SQLQuery = "Select * from mydb.recipe where (id = "+Id+")";
            List<string> Columns = new List<string>();
            Columns.Add("Id");
            Columns.Add("Name");
            Columns.Add("HowTo");
            Columns.Add("DishCategory");
            List<string>[] Result = dbConnect.ReturnColumns(SQLQuery, Columns);
            ListViewItem listViewItem; 
            label3.Text = Result[1][0];
            SQLQuery = "SELECT * FROM mydb.recipepic where (Recipe = " + Id + ")";
            Columns = new List<string>();
            Columns.Add("Id");
            Columns.Add("Recipe");
            Columns.Add("Path");
            Pictures = dbConnect.ReturnColumns(SQLQuery, Columns);
            SQLQuery = "SELECT * FROM mydb.Ingredients where (Recipe = " + Id + ")";
            Columns = new List<string>();
            Columns.Add("Id");
            Columns.Add("Name");
            Columns.Add("HowMuch");
            Columns.Add("Recipe");
            Ingedients = dbConnect.ReturnColumns(SQLQuery, Columns);
            for(int i = 0; i<Pictures[0].Count;i++){
                imageList1.Images.Add(Pictures[2][i], Image.FromFile(Application.StartupPath + "\\Pictures\\" + Pictures[2][i]));
                listView1.LargeImageList = imageList1;
                listViewItem = listView1.Items.Add("");
                listViewItem.ImageKey = Pictures[2][i];
            }
            textBox1.Text = Result[2][0];
            dataGridView1.Rows.Clear();
            List<string>[] BarcodeList;
            for (int i = 0; i < Ingedients[0].Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = Ingedients[1][i];
                dataGridView1.Rows[i].Cells[1].Value = Ingedients[2][i];
                Columns = new List<string>();
                Columns.Add("Barcode");
                SQLQuery = "SELECT * FROM ProductBase WHERE (Name LIKE \"%" + Ingedients[1][i] + "%\")";
                BarcodeList = dbConnect.ReturnColumns(SQLQuery, Columns);
                bool Exists = false;
                for (int j = 0; (j < BarcodeList[0].Count)&&(!Exists); j++)
                {
                    if (dbConnect.CheckExistance("ProductFridge", "Barcode", BarcodeList[0][j]))
                    {
                        Exists = true;
                    }
                }
                if (Exists)
                {
                    dataGridView1.Rows[i].Cells[2].Value = Image.FromFile("Галочка.png"); 
                }
                else
                    dataGridView1.Rows[i].Cells[2].Value = Image.FromFile("Крестик.png");
            }
            if (listView1.Items.Count > 0)
            {
                CurrentImage = Image.FromFile(Application.StartupPath + "\\Pictures\\" + listView1.Items[0].ImageKey);
                pictureBox1.Image = CurrentImage;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _f40.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            _f40.Show();
            this.Close();
        }

        private void UpdateList()
        {
            
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                CurrentImage = Image.FromFile(Application.StartupPath + "\\Pictures\\" + listView1.SelectedItems[0].ImageKey);
                pictureBox1.Image = CurrentImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string SQLQuery = "DELETE FROM RecipePIC WHERE (Recipe = "+Id+")";
            dbConnect.ExecSQL(SQLQuery);
            SQLQuery = "DELETE FROM Ingredients WHERE (Recipe = " + Id + ")";
            dbConnect.ExecSQL(SQLQuery);
            SQLQuery = "DELETE FROM Recipe WHERE (Id = " + Id + ")";
            dbConnect.ExecSQL(SQLQuery);
            pictureBox1.Image = new Bitmap(1, 1);
            if(!(CurrentImage == null))
                CurrentImage.Dispose();
            string KeyFile;
            for (int i = 0;i<listView1.Items.Count;i++){
                KeyFile = Application.StartupPath + "\\Picture\\" + listView1.Items[i].ImageKey;
                listView1.Items[i].Remove();
                if (System.IO.File.Exists(KeyFile))
                {
                    try
                    {
                        System.IO.File.Delete(KeyFile);

                    }
                    catch (Exception)
                    {

                    }
                    finally { }
                }
            }
            _f40.UpdateRecipeList();
            _f40.Show();
            this.Close();
        }
    }
}