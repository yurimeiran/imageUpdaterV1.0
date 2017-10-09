using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.IO;

namespace imageUpdaterV1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Main();
        }

        //give full access to directory
         [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

        private static void Main()
        {
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


        }//end of Main method



    }
}
