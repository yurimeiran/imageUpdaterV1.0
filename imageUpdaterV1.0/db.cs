using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GriffithElder.Database.MySql;
using GriffithElder.Utils;
using MySql.Data.MySqlClient;


namespace imageUpdaterV1._0
{
    class db
    {
        private MySqlConnection _connection;
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
          /* server name; either hostname or IP address */
          //server = "192.168.16.1"; 
          server = "192.168.16.122";
          //server = "localhost"; 
          /********************************************/

           /****** database name ***********************/
          //database = "cameras";
            database = "anpr";
           /*******************************************/

           /******** database username ****************/
          //uid = "web";
            uid = "yuri_m"; //allows remote connection
          //uid = "root" //only allows connections from local machine

           /************ database password ***************/
          //password = "//GE1981//Oakland5";
            password = "GElderMySQL";
           /*********************************************/
            
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            _connection = new MySqlConnection(connectionString);
        }


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
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
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        } //end of CloseConnection

        //Insert statement
        public void Insert(string query)
        {
            //string query = "INSERT into images VALUES (null, current_timestamp(), 'cv55qok', 'image.jpg', 1, 'test data');";

            //open connection
            //if (this.OpenConnection() == true)
            try
            {
                _connection.Open();
            
                //create command and assign the query and connection from the constructor
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Executed");
                }

            }  

             catch (MySqlException ex)
            {
                Log.WriteLog(LogType.Database, ex);
                throw ex;
            }

            finally
            {
                //close connection
                this.CloseConnection();
            }
           
            
        }//end of insert method

        //Update statement
        public void Update(string query)
        {
            //string query = "UPDATE images SET `haul_name` = 'some companys vehicle' WHERE `reg_no` = 'ap16dfw' ;";
           

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = _connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }// end of update method

    
    } ///end of db class
      ///

}///end of namespace imageUpdaterV1._0
