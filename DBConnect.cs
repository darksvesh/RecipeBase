using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
//Add MySql Library
using MySql.Data.MySqlClient;

namespace ConnectCsharpToMysql
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        //Constructor
        public DBConnect()
        {
            Initialize();
        }
        //Initialize values
        private void Initialize()
        {
            server = "127.0.0.1";
            database = "mydb";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }
        public List<string>[] ReturnColumns(string SQLQuery, List<string> Columns)
        {
            List<string>[] list = new List<string>[Columns.Count];
            for (int i = 0; i < Columns.Count; i++)
            {
                list[i] = new List<string>();
            }
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(SQLQuery, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                while (dataReader.Read())
                {
                    for (int i = 0; i < Columns.Count; i++)
                    {
                        list[i].Add(dataReader[Columns[i]] + "");
                    }
                }
                dataReader.Close();
                this.CloseConnection();
                return list;
            }
            else
            {
                return list;
            }
            
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool CheckExistance(string Table, string Column, string Value)
        {
            bool result = false;
            if (this.OpenConnection() == true)
            {
            string SQLQuery = "SELECT * FROM "+ Table + " WHERE (" + Column + " = \"" + Value + "\")";
            MySqlCommand cmd = new MySqlCommand(SQLQuery, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            result = dataReader.HasRows;
            dataReader.Close();
            this.CloseConnection();
            }
            return result;                
        }
        //123
        public void ExecSQL(string SqlQuery)
        {
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(SqlQuery, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
    }
}
