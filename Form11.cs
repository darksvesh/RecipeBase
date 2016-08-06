using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ConnectCsharpToMysql
{
    
    public partial class Form11 : Form
    {
        private Image CurrentImage;
        private DBConnect dbConnect;
        public Form11()
        {
            InitializeComponent();
            dbConnect = new DBConnect();

        }
        private Form1 _f38;
        public Form11(Form1 f38)
        {
            InitializeComponent();
            _f38 = f38;
            dbConnect = new DBConnect();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            _f38.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RecipeName = textBox4.Text.ToString();
            
            if (!dbConnect.CheckExistance("mydb.Recipe", "Name", RecipeName))
            {
                string HowTo = richTextBox2.Text.ToString();
                string SQLQuery = "INSERT INTO mydb.Recipe (Name, HowTo, DishCategory) values ( \""
                    + RecipeName +  "\", \"" + HowTo + "\", " + comboBox2.SelectedIndex + ")";
                dbConnect.ExecSQL(SQLQuery);
                SQLQuery = "SELECT * FROM  Recipe WHERE (Name = \""+RecipeName+"\")";
                List<string> Columns = new List<string>();
                Columns.Add("Id");
                List<string>[] RecipeId = dbConnect.ReturnColumns(SQLQuery, Columns);
                string CurrentRecipeId = RecipeId[0][0];
                for (int k=0; k < dataGridView1.RowCount-1; k++) 
                {
                    SQLQuery = "INSERT INTO ingredients (Name, HowMuch, Recipe) values (\""+dataGridView1.Rows[k].Cells[0].Value+
                        "\",\""+dataGridView1.Rows[k].Cells[1].Value+"\","+CurrentRecipeId+")";
                    dbConnect.ExecSQL(SQLQuery);
                }
                for (int k = 0; k < comboBox3.Items.Count; k++)
                {
                    SQLQuery = "INSERT INTO RecipePic (Recipe, Path) values (\""+CurrentRecipeId+"\",\""+comboBox3.Items[k].ToString()+"\")";
                    dbConnect.ExecSQL(SQLQuery);
                }

                this.Close();
            }
            else MessageBox.Show("Рецепт с таким названием уже занесен в базу");

            textBox4.Clear();
            richTextBox2.Clear();
        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if(openFileDialog1.FileName.Contains(".")){
            string Filetype = openFileDialog1.FileName;
            while (Filetype.Contains("."))
            {
                Filetype = Filetype.Remove(0, 1);
            }

            DateTime saveNow = DateTime.Now;
            string NewName = saveNow.ToString("yyyy-MM-dd-HH-mm-ss") + "." + Filetype;
            if(!System.IO.Directory.Exists(Application.StartupPath + "\\Pictures\\")){
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Pictures\\");
            }
            System.IO.File.Copy(openFileDialog1.FileName,Application.StartupPath + "\\Pictures\\" + NewName);
            CurrentImage = Image.FromFile(Application.StartupPath + "\\Pictures\\" + NewName);
            pictureBox1.Image = CurrentImage;
            comboBox3.Items.Add(NewName);
            comboBox3.SelectedIndex = 0;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!(comboBox3.SelectedIndex == -1)){
                pictureBox1.Image = new Bitmap(1,1);
                CurrentImage.Dispose();
            if (System.IO.File.Exists(comboBox3.Items[comboBox3.SelectedIndex].ToString()))
            {
                
                try
                {
                    System.IO.File.Delete(comboBox3.Items[comboBox3.SelectedIndex].ToString());
                }
                catch (IOException) { }
                finally { }
            }
            comboBox3.Items.RemoveAt(comboBox3.SelectedIndex);
            comboBox3.SelectedText = "";
            comboBox3.Text = "";
            if (comboBox3.Items.Count > 0)
                comboBox3.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;

        }
    }
}