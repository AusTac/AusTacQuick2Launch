using AusTacQuick2Launch.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log : UserControl
    {


        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public Log()
        {
            InitializeComponent();
            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to Log XAML");
            }

            DirectoryInfo di = new DirectoryInfo(AusTacQuick2Launch_Log);
            // Get a reference to each file in that directory.
            FileInfo[] fiArr = di.GetFiles();
            // Display the names and sizes of the files.
            //Console.WriteLine("The directory {0} contains the following files:", di.Name);
            foreach (FileInfo f in fiArr) {


                string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
                int s = 0;
                long size = f.ToString().Length;

                while (size >= 1024)
                {
                    s++;
                    size /= 1024;
                }

                string humanReadable = String.Format("{0} {1}", size, suffixes[s]);
                LogSize.Text = humanReadable;
            
            }


            if (System.IO.File.Exists(AusTacQuick2Launch_Log + "/log.txt"))
            {
                
                
                // Use a try block to catch IOExceptions, to 
                // handle the case of the file already being 
                // opened by another process. 
                try
                {
                    var lines = File.ReadAllLines(AusTacQuick2Launch_Log + "/log.txt").Reverse();

                    foreach (string line in lines)
                    {
                        // This will step through each line of the file, from the bottom to the top
                        loglistbox.Items.Add(line);
                    }

                }

                catch (System.IO.IOException error)
                {
                    Console.WriteLine(error.Message);
                    //logging
                    //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    //string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Reading Log: " + error.Message);
                    }
                }

            }
            
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //loglistbox.DataSource;
        }

        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            // Delete a file by using File class static method... 
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");


            if (System.IO.File.Exists(AusTacQuick2Launch_Log + "/log.txt"))
            {
                // Use a try block to catch IOExceptions, to 
                // handle the case of the file already being 
                // opened by another process. 
                try
                {
                    System.IO.File.Delete(AusTacQuick2Launch_Log + "/log.txt");
                }
                catch (System.IO.IOException error)
                {
                    Console.WriteLine(error.Message);
                    //logging
                    //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    //string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Clearing Log: " + error.Message);
                    }
                }
            }
        }

    }
}
