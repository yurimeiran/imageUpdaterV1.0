using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GriffithElder.Database.MySql;
using MySql.Data.MySqlClient;

namespace imageUpdaterV1._0
{
    class db
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
       
        //Constructor
        public db()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "anpr";
            uid = "root";
            password = "GElderMySQL";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("connected");
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
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
         }//end of OpenConnection

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
                Console.WriteLine(ex.Message);
                return false;
            }
        } //end of CloseConnection

        //Insert statement
        public void Insert()
        {
            string query = "insert into anpr.images values (null, current_timestamp(), 'ww04sks', 'F:\\dev\\imgUpdater\\imgUpdater\\bin\\Debug\\121212.jpg', 1, 'test data');";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

    
    
    } ///end of db class
      ///

}///end of namespace imageUpdaterV1._0
