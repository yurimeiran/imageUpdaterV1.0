using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.IO;

namespace imageUpdaterV1._0
{
    public partial class watcher
    {
 
        private static db database;
        
       
        //give full access to directory
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        
        public static void Main()
        {

            database = new db();

            //using GetCurrentDirectory() get directory where executable is located
            string path = System.IO.Directory.GetCurrentDirectory();

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch jpeg files.
            watcher.Filter = "*.jpg";// "*.jpeg";

            // Add event handlers.
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
           
            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit.");
            while (Console.Read() != 'q') ;

        }//end of Main method

        
        
        public static void OnChanged(object source, FileSystemEventArgs e)
        {




            string filename = e.Name.ToString();//turn e.Name to string
                filename = filename.Remove(filename.LastIndexOf(".")); //remove the file extention

            
                // Specify what is done when a file is changed, created, or deleted.
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType + " " + filename);

                //just for the test purpose write the name without extension again
                Console.WriteLine(filename);

           
           

           
                //06-10-2017 when file changed , created, or deleted create text file and make record
                using (StreamWriter write = new StreamWriter("onChange.txt", true))
                {
                   

                    write.WriteLine("File: " + e.FullPath + " " + e.ChangeType + " " + filename + " " + DateTime.Now);
                    //just for the test purpose write the name without extension again
                    write.WriteLine(filename);
                    write.Close();


                }
             string query = "INSERT into `images` VALUES (null, current_timestamp(), '" + filename + "','" + e.Name + "', 1 , '" + e.FullPath + "');";
            //string query = "insert into `images` values (null, current_timestamp(), 'cv55qok', 'image.jpg', 1, 'test data');"; 
            database.Insert(query);




        }//end of onChange 

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);

            //06-10-2017 when file is renamed create 
            using (StreamWriter write = new StreamWriter("onChange.txt", true))
            {
                write.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath + " " + DateTime.Now);
                write.Close();

            }

            string query = string.Format("INSERT into `images` VALUES (null, {0}, {1}, {2});", DateTime.Now, e.Name, e.FullPath);
            database.Insert(query);
            

        }
    }
}
