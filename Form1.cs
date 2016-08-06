using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
namespace ConnectCsharpToMysql
{
    public partial class Form1 : Form
    {
        private List<string>[] ProductBase = new List<string>[8];
        private List<string>[] ProductList = new List<string>[7];
        private List<string>[] ShopList = new List<string>[2];
        private List<string>[] CathegoryList = new List<string>[2];
        private List<string>[] BudjetList = new List<string>[4];
        private List<string>[] RecipeList = new List<string>[3];
        private List<string> SortedRecipeIdList = new List<string>();
        private DBConnect dbConnect;

        public Form1()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
            //tabPage2.BackgroundImage = Image.FromFile("База.jpg");
            tabPage3.BackgroundImage = Image.FromFile("Рецепт.jpg");
            tabPage5.BackgroundImage = Image.FromFile("Покупки.jpg");
            for(int i = 0; i<7; i++)
                ProductList[i] = new List<string>();
            /*for (int i = 0; i < 2; i++)
            {
                CathegoryList[i] = new List<string>();
                ShopList[i] = new List<string>();
            }*/
            for (int i = 0; i < 4; i++) 
            {
                BudjetList[i] = new List<string>();
            }
            for (int i = 0; i < 2; i++) 
            {
                RecipeList[i] = new List<string>();
            }
                UpdateBudjet();
            UpdateProductList();
            DateTime saveNow = DateTime.Now;
            dateTimePicker1.Value = saveNow;
            dateTimePicker2.Value = saveNow;
            dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-1);
            dateTimePicker1.MaxDate = dateTimePicker2.Value.AddDays(-1);
            dateTimePicker2.MaxDate = saveNow.AddDays(1);
            listBox1.Items.Clear();
            UpdateRecipeList();
            ProductTimer = new List<string>[3];
            ProductTimer[0] = new List<string>();
            ProductTimer[1] = new List<string>();
            ProductTimer[2] = new List<string>();
            timer1.Enabled = true;
            timer1_tick();
        }
        public void UpdateRecipeList()
        {
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            Columns.Add("DishCategory");
            string SQLQuery = "SELECT * FROM mydb.Recipe";
            RecipeList = dbConnect.ReturnColumns(SQLQuery, Columns);
            listBox1.Items.Clear();
            SortedRecipeIdList.Clear();
            for (int i = 0; i < RecipeList[0].Count; i++)
            {
                if (!textBox5.Text.Equals(""))
                {
                    if (checkedListBox1.CheckedIndices.Contains(int.Parse(RecipeList[2][i])))
                        if (RecipeList[1][i].Contains(textBox5.Text.ToString()))
                        {
                            listBox1.Items.Add(RecipeList[1][i]);
                            SortedRecipeIdList.Add(RecipeList[0][i]);
                        }
                }
                else
                {

                    if (checkedListBox1.CheckedIndices.Contains(int.Parse(RecipeList[2][i])))
                    {
                        listBox1.Items.Add(RecipeList[1][i]);
                        SortedRecipeIdList.Add(RecipeList[0][i]);
                    }
                    
                }
            }
        }
        private void UpdateProductBase()
        {
            List<string> Columns = new List<string>();
            Columns.Add("id");//0
            Columns.Add("Barcode");//1
            Columns.Add("Name");//2
          //  Columns.Add("Measure");
            Columns.Add("ShelfLife");//3
          //  Columns.Add("Buy");
            Columns.Add("KCal");
            string SQLQuery = "SELECT * FROM mydb.ProductBase";
            ProductBase = dbConnect.ReturnColumns(SQLQuery, Columns);
            UpdateBuyList();
        }
        private void UpdateShopList()
        {
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            string SQLQuery = "SELECT * FROM mydb.ShopList";
            ShopList = dbConnect.ReturnColumns(SQLQuery, Columns);
        }
      /*  private void UpdateCathegoryList()
        {
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            string SQLQuery = "SELECT * FROM mydb.CathegoryList";
            CathegoryList = dbConnect.ReturnColumns(SQLQuery, Columns);
        }*/

        public void UpdateProductList()
        {
            //UpdateCathegoryList();
            UpdateShopList();
            UpdateProductBase();
            dataGridView1.Rows.Clear(); 
            List<string> Columns = new List<string>();
            Columns.Add("id");//0
            Columns.Add("Barcode");//1
            Columns.Add("BuyDate");//2
            //Columns.Add("Count");//3
            Columns.Add("ShopId");//3
            Columns.Add("Price");//4
            Columns.Add("CreateDate");//5
            int ShopListIndex = 0;
            int ProductBaseIndex = 0;
            //int CathegoryListIndex = 0;
            string SQLQuery = "SELECT * FROM mydb.ProductFridge";
            ProductList = dbConnect.ReturnColumns(SQLQuery, Columns);
            for (int i = 0; i < ProductList[0].Count; i++)
            {
                ProductBaseIndex = FindIndex(ProductList[1][i], ProductBase[1].Count, ProductBase[1]);
                ShopListIndex = FindIndex(ProductList[3][i], ShopList[0].Count ,ShopList[0]);
                //CathegoryListIndex = FindIndex(ProductBase[4][ProductBaseIndex], CathegoryList[0].Count, CathegoryList[0]);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = ProductList[0][i];
                dataGridView1.Rows[i].Cells[1].Value = ProductList[2][i];
                dataGridView1.Rows[i].Cells[2].Value = ProductList[1][i];
                dataGridView1.Rows[i].Cells[3].Value = ProductBase[2][ ProductBaseIndex ];  
                dataGridView1.Rows[i].Cells[4].Value = ProductList[4][i];
                dataGridView1.Rows[i].Cells[5].Value = ProductBase[4][ProductBaseIndex];
                dataGridView1.Rows[i].Cells[6].Value = ShopList[1][ShopListIndex];
                //dataGridView1.Rows[i].Cells[7].Value = CathegoryList[1][CathegoryListIndex];
                dataGridView1.Rows[i].Cells[7].Value = ProductList[5][i];
                dataGridView1.Rows[i].Cells[8].Value = ProductBase[3][ProductBaseIndex];
            }
        }
        private int FindIndex(string s, int max, List<string> Arr){
            int result = 0;
            for (int i = 0; i < max; i++)
            {
                if (s == Arr[i]) result = i;
            }
            return result;
        }
        private void UpdateBuyList()
        {

            dataGridView4.Rows.Clear();
            //List<string> Barcode = new List<string>();
            //Barcode = ProductBase[1];
         /*   


            dataGridView4.Columns.RemoveAt(2);
            DataGridViewComboBoxColumn BarcodeColumn = new DataGridViewComboBoxColumn();
            BarcodeColumn.DataSource = ProductBase[1];
            BarcodeColumn.HeaderText = "Штрихкод";
            BarcodeColumn.DataPropertyName = "Barcode";
            dataGridView4.Columns.Insert(2,BarcodeColumn);

            dataGridView4.Columns.RemoveAt(3);
            DataGridViewComboBoxColumn ShopColumn = new DataGridViewComboBoxColumn();
            ShopColumn.DataSource = ShopList[1];
            ShopColumn.HeaderText = "Магазин";
            ShopColumn.DataPropertyName = "Shop";
            dataGridView4.Columns.Insert(3, ShopColumn);

*/
            (dataGridView4.Columns[7] as DataGridViewTextBoxColumn).DefaultCellStyle.NullValue = DateTime.Now.ToString("dd.MM.yyyy");
            (dataGridView4.Columns[8] as DataGridViewTextBoxColumn).DefaultCellStyle.NullValue = DateTime.Now.ToString("dd.MM.yyyy");

            (dataGridView4.Columns[2] as DataGridViewComboBoxColumn).Items.Clear();
            (dataGridView4.Columns[3] as DataGridViewComboBoxColumn).Items.Clear();
            foreach (string Value in ShopList[1])
                (dataGridView4.Columns[3] as DataGridViewComboBoxColumn).Items.Add(Value);
            foreach (string Value in ProductBase[1])
                (dataGridView4.Columns[2] as DataGridViewComboBoxColumn).Items.Add(Value);
            dataGridView4.Columns[2].Visible = false;
            dataGridView4.Columns[3].Visible = false;
            dataGridView4.Columns[4].Visible = false;
            dataGridView4.Columns[5].Visible = false;
            dataGridView4.Columns[6].Visible = false;
            dataGridView4.Columns[7].Visible = false;
            dataGridView4.Columns[8].Visible = false;
            dataGridView4.Columns[9].Visible = false;
            
            dataGridView4.Width = 250;
            string SQLQuery = "SELECT * FROM NEED";
            List<string> Columns = new List<string>();
            Columns.Add("Id");
            Columns.Add("Name");
            List<string>[] Needs = dbConnect.ReturnColumns(SQLQuery, Columns);
            int j = 0;
            for (int i = 0; i < Needs[0].Count; i++)
            {
                    dataGridView4.Rows.Add();
                    dataGridView4.Rows[j].Cells[1].Value = Needs[1][i];
                    dataGridView4.Rows[j].Cells[9].Value = "Добавить";
                    //dataGridView4.Rows[j].Cells[1].Value = CathegoryList[1][FindIndex(ProductBase[4][i],CathegoryList[0].Count,CathegoryList[0])];
                    j++;
            }
        }

     

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            f2.Show();
        }

        private void изменитьКатегорииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form4 f7 = new Form4(this);
           // f7.Show();

        }

        private void изменитьСписокМагазиновToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f9 = new Form5(this);
            f9.Show();
        }

        private void изменитьЕдиницыИзмеренияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f11 = new Form6(this);
            f11.Show();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Form3 s1 = new Form3(this);
            s1.Show();

        }

        private void добавитьПродуктВХолодильникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 f20 = new Form7(this);
            f20.Show();
        }


        private void GridStockItemEntry_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            DataGridViewRow row = dataGridView4.CurrentRow;
            DataGridViewCell cell = dataGridView4.CurrentCell;
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                if (cell == row.Cells[2])// && Convert.ToString(row.Cells["Type"].Value) == "Raw Material")
                {
                    DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                    cbo.DropDownStyle = ComboBoxStyle.DropDown;
                    cbo.Validating += new CancelEventHandler(cbo_Validating);
                }
            }
        }

        void cbo_Validating(object sender, CancelEventArgs e)
        {
            DataGridViewComboBoxEditingControl cbo = sender as DataGridViewComboBoxEditingControl;
            DataGridView grid = cbo.EditingControlDataGridView;
            object value = cbo.Text;
            if (cbo.Items.IndexOf(value) == -1)
            {
                DataGridViewComboBoxCell cboCol = (DataGridViewComboBoxCell)grid.CurrentCell;
                cbo.Items.Add(value);
                cboCol.Items.Add(value);
                grid.CurrentCell.Value = value;

            }
        }


        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         if(e.RowIndex>=0)
             if (dataGridView4[0, e.RowIndex].Value != null && dataGridView4[1, e.RowIndex].Value != null &&
                     dataGridView4[2, e.RowIndex].Value != null && dataGridView4[3, e.RowIndex].Value != null &&
                     dataGridView4[4, e.RowIndex].Value != null && dataGridView4[5, e.RowIndex].Value != null &&
                     dataGridView4[6, e.RowIndex].Value != null)
             {
                 DataGridView senderGrid = (DataGridView)sender;
                 string ErrorMessage = "";
                 DateTime CreateDate = DateTime.Now;
                 DateTime BuyDate = DateTime.Now;
                 bool Error = false;
                 string SQLQuery2;
                 if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                     e.RowIndex >= 0)
                 {

                     try
                     {
                         if (!(dataGridView4.Rows[e.RowIndex].Cells[7].Value == null))
                             CreateDate = DateTime.Parse(dataGridView4.Rows[e.RowIndex].Cells[7].Value.ToString());
                         if (!(dataGridView4.Rows[e.RowIndex].Cells[8].Value == null))

                             BuyDate = DateTime.Parse(dataGridView4.Rows[e.RowIndex].Cells[8].Value.ToString());

                     }
                     catch (Exception)
                     {
                         Error = true;
                         ErrorMessage = "Не могу распознать дату";
                     }
                     finally { }
                     //обработка кнопки добавить   
                     if (!Error)
                     {
                         string SQLQuery = "INSERT INTO mydb.budjet (Name, Price, Data) values(\"" + dataGridView4.Rows[e.RowIndex].Cells[1].Value + " в " +
                         ShopList[1][FindIndex(dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString(), ShopList[0].Count, ShopList[1])] + "\", -" + dataGridView4.Rows[e.RowIndex].Cells[5].Value + ", \"" +
                          BuyDate.ToString(("yyyy-MM-dd")) + "\")";
                         dbConnect.ExecSQL(SQLQuery);
                         SQLQuery = "INSERT INTO mydb.productfridge (Barcode, BuyDate, ShopId, Price, CreateDate)values";

                         int index = FindIndex(dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString(), ShopList[0].Count, ShopList[1]);

                         // "INSERT INTO mydb.productfridge (Barcode, BuyDate, ShopId, Price, CreateDate)values(\"111q\",\"DataGridViewTextBoxColumn { Name=BuyDate, Index=8 }\",11qwe,12,\"DataGridViewTextBoxColumn { Name=CreateDate, Index=7 }\")"
                         SQLQuery += "(\"" + dataGridView4.Rows[e.RowIndex].Cells[2].Value + "\",\"" + BuyDate.ToString(("yyyy-MM-dd")) + "\","
                             + ShopList[0][index] + "," + dataGridView4.Rows[e.RowIndex].Cells[5].Value + ",\"" + CreateDate.ToString(("yyyy-MM-dd")) + "\")";
                         if (!dbConnect.CheckExistance("ProductBase", "Barcode", dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString()))
                         {
                             //Barcode, Name, ShelfLife, Buy, KCal
                             SQLQuery2 = "INSERT INTO ProductBase (Barcode, Name, ShelfLife,  KCal) values (\"" + dataGridView4.Rows[e.RowIndex].Cells[2].Value
                                 + "\",\"" + dataGridView4.Rows[e.RowIndex].Cells[1].Value + "\"," + dataGridView4.Rows[e.RowIndex].Cells[6].Value + ", " + dataGridView4.Rows[e.RowIndex].Cells[4].Value + ")";
                             try
                             {
                                 dbConnect.ExecSQL(SQLQuery2);
                             }
                             catch (DivideByZeroException)
                             {
                                 ErrorMessage = "Неизвестная ошибка, проверьте правильность данных!";
                                 Error = true;
                             }
                             finally { }
                         }

                         if (!Error)
                         {
                             try
                             {
                                 dbConnect.ExecSQL(SQLQuery);
                                 SQLQuery = "Delete from Need where (Name = \"" + dataGridView4.Rows[e.RowIndex].Cells[1].Value + "\")";
                                 dbConnect.ExecSQL(SQLQuery);
                             }
                             catch (DivideByZeroException)
                             {
                                 Error = true;
                                 ErrorMessage = "Неизвестная ошибка, проверьте правильность данных!";
                             }
                             finally { }

                             UpdateBuyList();
                             UpdateBudjet();
                             UpdateProductList();
                         }

                     }
                     if (Error)
                     {
                         MessageBox.Show(ErrorMessage);

                     }
                 }

             }

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            UpdateBudjet();
            UpdateProductList();
            UpdateBuyList();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string SelectedElement = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string SQLQuery = "DELETE FROM Mydb.productfridge WHERE (id = " + SelectedElement + ")";
            dbConnect.ExecSQL(SQLQuery);
            UpdateProductList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SQLQuery = "delete from mydb.need WHERE (1 = 1)";
            dbConnect.ExecSQL(SQLQuery);
            dataGridView4.Rows.Clear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int ShopListIndex = 0;
            int ProductBaseIndex = 0;
          //  int CathegoryListIndex = 0;
            if (!textBox4.Text.ToString().Equals("") && !textBox4.Text.ToString().Equals(" "))
            {
                int j = 0;
                for (int i = 0; i < ProductList[0].Count; i++)
                {
                    if (ProductBase[1][i].Contains(textBox4.Text.ToString()) || ProductBase[2][i].Contains(textBox4.Text.ToString()))
                    {
                        ProductBaseIndex = FindIndex(ProductList[1][i], ProductBase[1].Count, ProductBase[1]);
                        ShopListIndex = FindIndex(ProductList[4][i], ShopList[0].Count, ShopList[0]);
                     //   CathegoryListIndex = FindIndex(ProductBase[4][ProductBaseIndex], CathegoryList[0].Count, CathegoryList[0]);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[j].Cells[0].Value = ProductList[0][i];
                        dataGridView1.Rows[j].Cells[1].Value = ProductList[2][i];
                        dataGridView1.Rows[j].Cells[2].Value = ProductList[1][i];
                        dataGridView1.Rows[j].Cells[3].Value = ProductBase[2][ProductBaseIndex];
                        dataGridView1.Rows[j].Cells[4].Value = ProductList[4][i];
                        dataGridView1.Rows[j].Cells[5].Value = ProductBase[4][ProductBaseIndex];
                        dataGridView1.Rows[j].Cells[6].Value = ShopList[1][ShopListIndex];
                       // dataGridView1.Rows[j].Cells[7].Value = CathegoryList[1][CathegoryListIndex];
                        dataGridView1.Rows[j].Cells[7].Value = ProductList[5][i];
                        dataGridView1.Rows[j].Cells[8].Value = ProductBase[3][ProductBaseIndex];
                        j++;
                    }
                }
            }
            else
            {

                for (int i = 0; i < ProductList[0].Count; i++)
                {
                    ProductBaseIndex = FindIndex(ProductList[1][i], ProductBase[1].Count, ProductBase[1]);
                    ShopListIndex = FindIndex(ProductList[4][i], ShopList[0].Count, ShopList[0]);
                //    CathegoryListIndex = FindIndex(ProductBase[4][ProductBaseIndex], CathegoryList[0].Count, CathegoryList[0]);
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = ProductList[0][i];
                    dataGridView1.Rows[i].Cells[1].Value = ProductList[2][i];
                    dataGridView1.Rows[i].Cells[2].Value = ProductList[1][i];
                    dataGridView1.Rows[i].Cells[3].Value = ProductBase[2][ProductBaseIndex];
                    dataGridView1.Rows[i].Cells[4].Value = ProductList[4][i];
                    dataGridView1.Rows[i].Cells[5].Value = ProductBase[4][ProductBaseIndex];
                    dataGridView1.Rows[i].Cells[6].Value = ShopList[1][ShopListIndex];
                  //  dataGridView1.Rows[i].Cells[7].Value = CathegoryList[1][CathegoryListIndex];
                    dataGridView1.Rows[i].Cells[7].Value = ProductList[5][i];
                    dataGridView1.Rows[i].Cells[8].Value = ProductBase[3][ProductBaseIndex];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 f31 = new Form9(this);
            f31.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form10 f34 = new Form10(this);
            f34.Show();
        }
        private void UpdateBudjet()
        {
            List<string> Columns = new List<string>();
            Columns.Add("id");
            Columns.Add("Name");
            Columns.Add("Price");
            Columns.Add("Data");
            string SQLQuery = "SELECT * FROM mydb.Budjet";
            BudjetList = dbConnect.ReturnColumns(SQLQuery, Columns);
            DateTime Date = new DateTime();
            CultureInfo provider = new CultureInfo("ru-RU");
           // string SQLDateFormat = "DD.MM.YYYY H:MM:SS";
            DateTime MaxDate = dateTimePicker2.Value;
            MaxDate.AddHours((double)23.9);
            DateTime MinDate = dateTimePicker1.Value;
            dataGridView3.Rows.Clear();
            dataGridView2.Rows.Clear();
            int j = 0;
            int k = 0;
            int Earn = 0;
            int Cost = 0;
            int Sum = 0;

            for (int i = 0; i < BudjetList[0].Count; i++ )
            {
                Date = DateTime.Parse(BudjetList[3][i],  provider);
                if ((Date > MinDate) && (Date < MaxDate))
                {
                    if(int.Parse(BudjetList[2][i])<0)
                    {
                        dataGridView3.Rows.Add();
                        dataGridView3.Rows[j].Cells[0].Value = BudjetList[0][i];
                        dataGridView3.Rows[j].Cells[1].Value = BudjetList[3][i];
                        dataGridView3.Rows[j].Cells[2].Value = BudjetList[1][i];
                        dataGridView3.Rows[j].Cells[3].Value = BudjetList[2][i].Remove(0,1);
                        j++;
                        Cost += int.Parse(BudjetList[2][i]);
                        
                    }
                    else
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[k].Cells[0].Value = BudjetList[0][i];
                        dataGridView2.Rows[k].Cells[1].Value = BudjetList[3][i];
                        dataGridView2.Rows[k].Cells[2].Value = BudjetList[1][i];
                        dataGridView2.Rows[k].Cells[3].Value = BudjetList[2][i];
                        k++;
                        Earn += int.Parse(BudjetList[2][i]);
                    }
                }
            }
            textBox2.Text = Cost.ToString();
            textBox1.Text = Earn.ToString();
            Sum = Earn + Cost;
            textBox3.Text = Sum.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBudjet();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = dateTimePicker2.Value.AddDays(-1);
            UpdateBudjet();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateBudjet();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            string SelectedElement = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            string SQLQuery = "DELETE FROM Mydb.Budjet WHERE (id = " + SelectedElement + ")";
            dbConnect.ExecSQL(SQLQuery);
            UpdateProductList();
            UpdateBudjet();

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string SelectedElement = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            string SQLQuery = "DELETE FROM Mydb.Budjet WHERE (id = " + SelectedElement + ")";
            dbConnect.ExecSQL(SQLQuery);
            UpdateProductList();
            UpdateBudjet();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form11 f39 = new Form11(this);
            f39.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string ProductName =
                dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                string SQLQuery = "INSERT INTO NEED (Name)values(\"" + ProductName + "\")";
                dbConnect.ExecSQL(SQLQuery);
                UpdateBuyList();
            }
        }



        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    if (dataGridView4.Rows.Count > 1 && e.RowIndex >= 0)
                        if (!(dataGridView4[2, e.RowIndex].Value == null))
                            if (dbConnect.CheckExistance("ProductBase", "Barcode", dataGridView4[2, e.RowIndex].Value.ToString()))
                            {
                               // dataGridView4[1, e.RowIndex].Value = ProductBase[2][FindIndex(dataGridView4[e.ColumnIndex, e.RowIndex].Value.ToString(), ProductBase[0].Count, ProductBase[1])];
                                dataGridView4[4, e.RowIndex].Value = ProductBase[4][FindIndex(dataGridView4[e.ColumnIndex, e.RowIndex].Value.ToString(), ProductBase[0].Count, ProductBase[1])];
                                dataGridView4[6, e.RowIndex].Value = ProductBase[3][FindIndex(dataGridView4[e.ColumnIndex, e.RowIndex].Value.ToString(), ProductBase[0].Count, ProductBase[1])];
                            }
                }

                if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
                {
                    if (e.ColumnIndex == 1)
                        (dataGridView4[2,e.RowIndex] as DataGridViewComboBoxCell).Items.Clear();
                        for (int i = 0; i < ProductBase[0].Count; i++)
                            if(ProductBase[2][i].Contains(dataGridView4[e.ColumnIndex,e.RowIndex].Value.ToString()))
                                (dataGridView4[2,e.RowIndex] as DataGridViewComboBoxCell).Items.Add(ProductBase[1][i]);
                    if (!(dataGridView4.Rows[e.RowIndex].Cells[0].Value == null))
                        if (dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("True"))
                            if (!(dataGridView4.Rows[e.RowIndex].Cells[1].Value == null))
                                if (!dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString().Equals(""))
                                {
                                    dataGridView4.Columns[2].Visible = true;
                                    dataGridView4.Columns[3].Visible = true;
                                    dataGridView4.Columns[4].Visible = true;
                                    dataGridView4.Columns[5].Visible = true;
                                    dataGridView4.Columns[6].Visible = true;
                                    dataGridView4.Columns[7].Visible = true;
                                    dataGridView4.Columns[8].Visible = true;
                                    dataGridView4.Columns[9].Visible = true;
                                    dataGridView4.Width = 932;
                                }
                    bool NeedsToCLose = true;
                    for (int i = 0; i < dataGridView4.Rows.Count; i++)

                        if (!(dataGridView4.Rows[i].Cells[0].Value == null))
                            if (dataGridView4.Rows[i].Cells[0].Value.ToString().Equals("True"))
                                if (!(dataGridView4.Rows[i].Cells[1].Value == null))
                                    if (!dataGridView4.Rows[i].Cells[1].Value.ToString().Equals(""))
                                        NeedsToCLose = false;
                    if (NeedsToCLose)
                    {
                        dataGridView4.Width = 250;
                        dataGridView4.Columns[2].Visible = false;
                        dataGridView4.Columns[3].Visible = false;
                        dataGridView4.Columns[4].Visible = false;
                        dataGridView4.Columns[5].Visible = false;
                        dataGridView4.Columns[6].Visible = false;
                        dataGridView4.Columns[7].Visible = false;
                        dataGridView4.Columns[8].Visible = false;
                        dataGridView4.Columns[9].Visible = false;
                    }
                }

            }
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(!(listBox1.SelectedIndex==null))
            {
                if (listBox1.SelectedIndex>=0)
                if (listBox1.Items[listBox1.SelectedIndex] != null)
                {
                    Form12 f1 = new Form12(this, SortedRecipeIdList[listBox1.SelectedIndex]);
                    f1.Show();
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            UpdateRecipeList();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void checkedListBox1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateRecipeList();
        }

        private void dataGridView4_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
          //  (e.Row.Cells[9] as DataGridViewButtonCell).Value = "Добавить";
        }
        private List<string>[] ProductTimer;
        private void timer1_tick()
        {
            DateTime Now = DateTime.Now;
            Now = Now.AddDays(1);
            TimeSpan ElapsedDays;
            if (ProductList[0].Count > 0)
            {
                ProductTimer[0].Clear();
                ProductTimer[1].Clear();
                ProductTimer[2].Clear();
                int j = 0;
                bool DoNotShow = true;
                for (int i = 0; i < ProductList[0].Count; i++)
                {

                    int ProductBaseIndex = FindIndex(ProductList[1][i], ProductBase[0].Count, ProductBase[1]);
                    DateTime CreateDate = DateTime.Parse(ProductList[5][i]);
                    CreateDate = CreateDate.AddDays(int.Parse(ProductBase[3][ProductBaseIndex]));
                    if (CreateDate <= Now)
                    {
                        ElapsedDays = DateTime.Now -CreateDate;
                        ProductTimer[0].Add(ProductList[0][i]);
                        ProductTimer[1].Add(ProductBase[2][ProductBaseIndex]);
                        ProductTimer[2].Add(ElapsedDays.ToString());
                        j++;
                        DoNotShow = false;
                    }
                }
                if (!DoNotShow)
                {
                    Form4 t = new Form4(this);
                    t.SetProductTimer(ProductTimer);
                    t.Show();
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1_tick();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if(dataGridView4.SelectedCells.Count>0){
                if (dataGridView4[1, dataGridView4.SelectedCells[0].RowIndex].Value != null)
                {
                    if (dbConnect.CheckExistance("Need", "Name", dataGridView4[1, dataGridView4.SelectedCells[0].RowIndex].Value.ToString()))
                    {
                        dbConnect.ExecSQL("Delete from mydb.need where (Name = \"" + dataGridView4[1, dataGridView4.SelectedCells[0].RowIndex].Value.ToString() + "\")");
                    }
                }
                dataGridView4.Rows.RemoveAt(dataGridView4.SelectedCells[0].RowIndex);
            }
        }

        

       

    }
}