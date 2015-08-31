using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for DebugDisplay.xaml
    /// </summary>
    public partial class DebugDisplay : UserControl
    {
        public DebugDisplay()
        {
            InitializeComponent();
            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to Debug Display XAML ");
            }
            //Get Version Value
            XmlDocument xmlDoc = new XmlDocument();
            Assembly asmCurrent = System.Reflection.Assembly.GetExecutingAssembly();
            string executePath = new Uri(asmCurrent.GetName().CodeBase).LocalPath;

            xmlDoc.Load(executePath + ".manifest");
            string retval = string.Empty;
            if (xmlDoc.HasChildNodes)
            {
                retval = xmlDoc.ChildNodes[1].ChildNodes[0].Attributes.GetNamedItem("version").Value.ToString();
            }
            Application_Version.Text = retval;


            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\runonce"))
                {
                    Application_Run.Text = AusTacQuick2Launch.Properties.Settings.Default.firstrun.ToString();
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Checking RunOnce File");

                    }
                }
                else
                {
                    string getrunoncevaluebool = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\runonce").First();
                    Application_Run.Text = getrunoncevaluebool;
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Success Checking RunOnce File");

                    }
                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking RunOnce File");
                    Application_Run.Text = AusTacQuick2Launch.Properties.Settings.Default.firstrun.ToString();
                }
            }






            Application_Dir.Text = AusTacQuick2Launch.Properties.Settings.Default.BaseDir.ToString();

            //string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string arma_2_path_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\Arma2Path.txt"))
                {
                    arma_2_path_value = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
                }
                else
                {
                    arma_2_path_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\Arma2Path.txt").First();
                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 2 Path File");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string arma_3_path_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\Arma3Path.txt"))
                {
                    arma_3_path_value = AusTacQuick2Launch.Properties.Settings.Default.Arma3Path;
                }
                else
                {
                    arma_3_path_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\Arma3Path.txt").First();
                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 3 Path File");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string arma2_mods_folder_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\Arma2ModsPath.txt"))
                {
                    arma2_mods_folder_value = AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath;
                }
                else
                {
                    arma2_mods_folder_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\Arma2ModsPath.txt").First();
                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 2 Mods Path File");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string arma3_mods_folder_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\Arma3ModsPath.txt"))
                {
                    arma3_mods_folder_value = AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath;
                }
                else
                {
                    arma3_mods_folder_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\Arma3ModsPath.txt").First();
                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 3 Mods Path File");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Arma 3 Check Path
            string a3_path = arma_3_path_value;
            try
            {
                // If the directory doesn't exist, create it.
                if (!System.IO.Directory.Exists(a3_path))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_path_ok.Source = imgSource;
                    arma3_path_ok.ToolTip = @"Path:: " + arma_3_path_value;
                    //File.GetLastWriteTime(a3_mods_xml_path).ToString();
                    Arma3Path.Text = arma_3_path_value;
                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_path_ok.Source = imgSource;
                    arma3_path_ok.ToolTip = @"Path:: " + arma_3_path_value;
                    Arma3Path.Text = arma_3_path_value;

                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 3 Path");
                }
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Arma 3 Mods Check Path
            string a3_mods_path = arma3_mods_folder_value;
            try
            {
                // If the directory doesn't exist, create it.
                if (!System.IO.Directory.Exists(a3_mods_path))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_mods_ok.Source = imgSource;
                    arma3_mods_ok.ToolTip = @"Path:: " + arma3_mods_folder_value;
                    //File.GetLastWriteTime(a3_mods_xml_path).ToString();
                    Arma3ModsPath.Text = arma3_mods_folder_value;
                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_mods_ok.Source = imgSource;
                    arma3_mods_ok.ToolTip = @"Path:: " + arma3_mods_folder_value;
                    Arma3ModsPath.Text = arma3_mods_folder_value;

                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 3 Mods Path");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Arma 2 Mods XML Check Path
            string a2_mods_xml_path = arma2_mods_folder_value;
            try
            {
                // If the directory doesn't exist, create it.
                if (!System.IO.Directory.Exists(a2_mods_xml_path))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2_mods_ok.Source = imgSource;
                    arma2_mods_ok.ToolTip = @"Path:: " + arma2_mods_folder_value;
                    Arma2ModsPath.Text = arma2_mods_folder_value;
                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2_mods_ok.Source = imgSource;
                    arma2_mods_ok.ToolTip = @"Path:: " + arma2_mods_folder_value;
                    Arma2ModsPath.Text = arma2_mods_folder_value;

                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 2 Mods Path");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Arma 3 Check Path
            string a2_path = arma_2_path_value;
            try
            {
                // If the directory doesn't exist, create it.
                if (!System.IO.Directory.Exists(a2_path))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2_path_ok.Source = imgSource;
                    arma2_path_ok.ToolTip = @"Path:: " + arma_2_path_value;
                    //File.GetLastWriteTime(a3_mods_xml_path).ToString();
                    Arma2Path.Text = arma_2_path_value;
                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2_path_ok.Source = imgSource;
                    arma2_path_ok.ToolTip = @"Path:: " + arma_2_path_value;
                    Arma2Path.Text = arma_2_path_value;

                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }

        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }


    }
}
