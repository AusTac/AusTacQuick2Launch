using AusTacQuick2Launch.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class Arma3Launcher: UserControl
    {

        public ObservableCollection<BoolStringClass> TheList { get; set; }

        
        public Arma3Launcher()
        {
            InitializeComponent();


            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to Arma 3 Launcher");
                
            }


            Loaded += Arma3Launcher_Loaded;

        }

        private async void TheListOutput()
        {

            TheList = new ObservableCollection<BoolStringClass>();

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            await Task.Run(() => Thread.Sleep(50));

            string arma_3_path_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\Arma3Path.txt"))
                {
                    arma_3_path_value = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
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
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 3 Path File");
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
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 3 Mods Path File");
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string path2 = arma_3_path_value;
            string path = arma3_mods_folder_value;


            ///////////////////////////////////////////Start Check for Arma 2
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(path2))
                {

                    //error
                    //logging
                    //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Loading Arma 3 Path");
                    }

                }
                else
                {
                    ///////////////////////////////////////////Start Check for ArmA2OA.exe
                    string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Arma 3 Path Exists");
                        foreach (var item in TheList) { sw.WriteLine(time + " | List Item: " + item.TheText); }
                    }

                    if (!System.IO.File.Exists(path2 + @"\arma3.exe"))
                    {

                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();
                            sw.WriteLine(time + " | Arma3.exe Not Found");
                        }

                    }
                    else
                    {
                        ///////////////////////////////////////////Start Check for Arma 2 Mods Directory
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();
                            sw.WriteLine(time + " | arma3.exe Found");
                        }


                        try
                        {
                            // If the directory doesn't exist, create it.
                            if (!Directory.Exists(path))
                            {
                                //error
                                //logging
                                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Error Loading Arma 3 Mods Path");
                                }

                            }
                            else
                            {
                                //ok
                                //string api_url = FirstFloor.ModernUI.App.Properties.Settings.Default.armasync_base_url;
                                //string api_html_url = "xapi/generate?collection_type=mods&type=html&limit=all";
                                string[] dirs = Directory.GetDirectories(path, "@*");
                                //textbox.Text = "The number of directories starting with @ is {0}.", dirs.Length;
                                foreach (string dir in dirs)
                                {
                                    string FolderName = System.IO.Path.GetFileName(dir);
                                    //string FolderUpdated =  (TimeAgo(DateTime.Parse(Directory.GetLastWriteTime(dir).ToString())));
                                    TheList.Add(new BoolStringClass { IsSelected = false, TheText = FolderName, TheUrl = FolderName });
                                }
                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Arma 3 Mods Path Loaded");
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
                                sw.WriteLine(time + " | Error Loading Arma 3 Mods Path");
                            }
                        }

                        ///////////////////////////////////////////End Check for Arma 2 Mods Directory 

                        ///////////////////////////////////////////Start Check for Arma 2 Directory
                        try
                        {
                            // If the directory doesn't exist, create it.
                            if (!Directory.Exists(path2))
                            {
                                //error
                                //logging
                                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Error Loading Arma 3 Path");
                                }

                            }
                            else
                            {

                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Arma 3 Path Found");
                                }

                                if (!System.IO.File.Exists(path2 + @"\arma3.exe"))
                                {

                                    //error
                                    //logging
                                    //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                    //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                    {
                                        string time = DateTime.Now.ToString();
                                        sw.WriteLine(time + " | arma3.exe Not Found");
                                    }

                                    //Folder is Not Set Correctly
                                    panelhide.Visibility = Visibility.Hidden;
                                    panelnofile.Visibility = Visibility.Visible;
                                    ModsListPanel.Visibility = Visibility.Hidden;

                                }
                                else
                                {

                                    //ok
                                    //string api_url = FirstFloor.ModernUI.App.Properties.Settings.Default.armasync_base_url;
                                    //string api_html_url = "xapi/generate?collection_type=mods&type=html&limit=all";
                                    string[] dirs = Directory.GetDirectories(path2, "@*");
                                    //textbox.Text = "The number of directories starting with @ is {0}.", dirs.Length;
                                    foreach (string dir in dirs)
                                    {
                                        string FolderName = System.IO.Path.GetFileName(dir);
                                        //string FolderUpdated = (TimeAgo(DateTime.Parse(Directory.GetLastWriteTime(dir).ToString())));
                                        TheList.Add(new BoolStringClass { IsSelected = false, TheText = FolderName, TheUrl = FolderName });
                                    }

                                    //TheList.Add(new BoolStringClass { IsSelected = false, TheText = "ACR" });
                                    //TheList.Add(new BoolStringClass { IsSelected = false, TheText = "ACRLite" });

                                }

                            }//END if (!System.IO.File.Exists(path2 + @"\ArmA2OA.exe"));
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
                                sw.WriteLine(time + " | Error Loading Arma 3 Path");
                            }
                        }

                        ///////////////////////////////////////////End Check for Arma 2 Directory

                    }
                }
            }

            catch (Exception)
            {
                // Fail silently
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Loading Arma 3 Path");
                }

                //Folder is Not Set Correctly
                panelhide.Visibility = Visibility.Hidden;
                panelnofile.Visibility = Visibility.Visible;
                ModsListPanel.Visibility = Visibility.Hidden;
            }


            //Provide change-notification for all members
            foreach (var item in TheList)
                item.PropertyChanged += TheList_Item_PropertyChanged;

            this.DataContext = this;



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

        private void Arma3Launcher_Loaded(object sender, RoutedEventArgs e)
        {
            panelhide.Visibility = Visibility.Visible;
            panelnofile.Visibility = Visibility.Hidden;
            //ModsListPanel.Visibility = Visibility.Hidden;
            showProgress();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Arma 3 Launcher Loaded");

            }

        }

        public void showProgress()
        {


            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            timer.Tick += delegate(object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                string arma_3_path = AusTacQuick2Launch.Properties.Settings.Default.Arma3Path;
                string arma3_mods_folder = AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath;
                try
                {
                    // If the directory doesn't exist, create it.
                    if (!Directory.Exists(arma_3_path))
                    {
                        //Folder is Not Set Correctly
                        panelhide.Visibility = Visibility.Hidden;
                        panelnofile.Visibility = Visibility.Visible;
                        //TheListOutput();
                        ModsListPanel.Visibility = Visibility.Hidden;
                        //logging
                        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();

                            sw.WriteLine(time + " | Arma 3 Path(s) Not Set");


                        }
                    }
                    else
                    {
                        //Folder is Set Correctly
                        panelhide.Visibility = Visibility.Hidden;
                        panelnofile.Visibility = Visibility.Hidden;
                        ModsListPanel.Visibility = Visibility.Visible;
                        TheListOutput();
                        //logging
                        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();

                            sw.WriteLine(time + " | Loaded installed Mods from Arma 3 Path(s)");


                        }

                    }
                }
                catch (Exception)
                {
                    // Fail silently
                    //logging
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error in Arma 3 Launcher");

                    }
                }


            };

            timer.Start();

        }

        public static string SteamFolder()
        {
            RegistryKey steamKey = Registry.LocalMachine.OpenSubKey("Software\\Valve\\Steam") ?? Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam");
            return steamKey.GetValue("InstallPath").ToString();
        }

        private async void Launch_Click(object sender, RoutedEventArgs e)
        {
            //Get a List<BoolStringClass> that contains all selected items:
            var res = (
                    from item in TheList
                    where item.IsSelected == true
                    select item
                ).ToList<BoolStringClass>();

            //Convert all items to a concatenated string:
            var res2 =
            from item in TheList
            select item.IsSelected ? item.TheText : "";

            string ip_address_connect = "";
            string port_address_connect = "";
            string password_connect = "";

            if (string.IsNullOrEmpty(ip_address.Text))
            {
                //IP Empty
            }
            else
            {
                if (string.IsNullOrEmpty(port_address.Text))
                {
                    //Port Empty
                }
                else
                {
                    ip_address_connect = "-connect=" + ip_address.Text;
                    port_address_connect = "-port=" + port_address.Text;
                    password_connect = "-password=" + password.Password;
                }
            }

            Process Process = new Process();
            string arma_3_path = AusTacQuick2Launch.Properties.Settings.Default.Arma3Path;
            string arma_3_mods_path = AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath;

            string arma_3_exe = "";
            if (true == allow_battleeye.IsChecked) { arma_3_exe = "arma3battleye.exe"; } else { arma_3_exe = "arma3.exe"; }

            string mods = "-mod=";
            //string Arma3ExtrasOriginal = SteamFolder() + @"\SteamApps\Common\Arma 3;" + "expansion;";
            string Arma3Extras = "";
            //Arma3Extras = Arma3ExtrasOriginal.Replace(@"\", "/");

            StringBuilder sb = new StringBuilder();



            foreach (var item in ModsListBox.Items)
            {
                string seperator = @"\";
                sb.AppendFormat(arma_3_mods_path + seperator + "{0};", item.ToString());
                   
            }


            string app_ouput = sb.ToString();
            //get start up params
            string nosplash = "";
            if (true == launch_setting_nosplash.IsChecked) { nosplash = "-nosplash"; }
            string scripterrors = "";
            if (true == launch_setting_scripterrors.IsChecked) { scripterrors = "-showscripterrors"; }
            string intro = "";
            if (true == launch_setting_intro.IsChecked) { intro = "-skipIntro"; }
            string nopause = "";
            if (true == launch_setting_nopause.IsChecked) { nopause = "-noPause"; }


            Process.StartInfo.Arguments = ip_address_connect + port_address_connect + password_connect + '"' + mods + Arma3Extras + app_ouput + '"' + nosplash + scripterrors + nopause + intro;
            //Process.StartInfo.Arguments = scripterrors;
            string combined = System.IO.Path.Combine(arma_3_path, arma_3_exe);
            Process.StartInfo.FileName = combined;
            Process.Start();
            //string launchparams = "steam://run/33930//" + mods + Arma2Extras + app_ouput + nosplash + scripterrors + nopause + intro;
            //System.Diagnostics.Process.Start(launchparams);
            
            
            /*
            Process process = new Process();
            string combined = System.IO.Path.Combine(arma_2_path, arma_2_exe);
            process.StartInfo.FileName = combined;
            //process.StartInfo.FileName = "arma2oa.exe";
            process.StartInfo.Arguments = "" + nosplash +  "-mod=M:/Steam/SteamApps/common/Arma 2;M:/Steam/SteamApps/common/Arma 2 Operation Arrowhead;expansion;M:/ArmaMods/@ACE;M:/ArmaMods/@ACEX;M:/ArmaMods/@CBA_CO;";
            //process.StartInfo.WorkingDirectory = gameDir + "/Expansion/beta/";
            process.Start();
            */


            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Launching Arma 3 with Params: " + Process.StartInfo.Arguments);
                sw.WriteLine(time + " | Launching Arma 3 from: " + Process.StartInfo.FileName);

            }

            Random rnd = new Random();
            int rand = rnd.Next(999, 9999);
            int rand2 = rnd.Next(999, 9999);

            string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent");
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Recent + "/Arma3/" + rand + rand2 + ".params"))
            {

                sw.WriteLine(Process.StartInfo.Arguments);

            }

            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Recent + "/Arma3/" + rand + rand2 + ".yml"))
            {

                sw.WriteLine("---");
                sw.WriteLine(" ");
                sw.WriteLine("# Name Generated for File");
                sw.WriteLine(":name:" + rand + rand2 + ".yml");
                sw.WriteLine(" ");
                if (string.IsNullOrEmpty(ip_address.Text))
                {
                    //IP Empty
                    sw.WriteLine("# IP Address");
                    sw.WriteLine(":ip:");
                    sw.WriteLine(" ");
                }
                else
                {
                    if (string.IsNullOrEmpty(port_address.Text))
                    {
                        //Port Empty
                        sw.WriteLine("# Port");
                        sw.WriteLine(":port:");
                        sw.WriteLine(" ");
                    }
                    else
                    {

                        sw.WriteLine("# IP Address");
                        sw.WriteLine(":ip:" + ip_address.Text);
                        sw.WriteLine(" ");
                        sw.WriteLine("# Port");
                        sw.WriteLine(":port:" + port_address.Text);
                        sw.WriteLine(" ");

                        sw.WriteLine("# Password");
                        sw.WriteLine(":password:" + password.Password);
                        sw.WriteLine(" ");

                    }
                }

                sw.WriteLine("# Arma Game Version");
                sw.WriteLine(":game: arma3");
                sw.WriteLine(" ");

                sw.WriteLine("# Selected Arma Mods");
                sw.WriteLine(":mods:");
                sw.WriteLine(" ");
                /*
                foreach (var item in TheList)
                {
                    if (item.IsSelected == true)
                    {

                        sw.WriteLine("- " + '"' + item.TheText + '"');

                    }
                }
                */

                foreach (var item in ModsListBox.Items)
                {
                     sw.WriteLine("- " + '"' + item.ToString() + '"');

                }

            }


            //Save to Recent
            if (launch_setting_allowrecent.IsChecked == true)
            {

                
                //logging
                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent");
                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Recent + "/Arma3/" + rand + rand2 + ".recent"))
                {

                    foreach (var item in TheList)
                    {
                        if (item.IsSelected == true)
                        {
                            sw.WriteLine(item.TheText);

                        }
                    }


                }

                

            }

            if (AfterLaunchMinimizeselected.IsChecked == true)
            {

               

            }
            else if (AfterLaunchCloseselected.IsChecked == true)
            {

                //Folder is Not Set Correctly
                panelhide.Visibility = Visibility.Hidden;
                panelnofile.Visibility = Visibility.Hidden;
                panelclosing.Visibility = Visibility.Visible;
                ModsListPanel.Visibility = Visibility.Hidden;
                
                await Task.Run(() => Thread.Sleep(5000)); 
                Application.Current.Shutdown();

            }


        }

        void TheList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Only react for items ADDED to the collection (=> e.NewItems)
            //foreach (BoolStringClass item in e.NewItems) ;
            //UnselectOtherItems(item);	


        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                ModsListBox.Items.Add(cb.Content);
            }

            if (cb.IsChecked == false)
            {
                ModsListBox.Items.Remove(cb.Content);
            }



        }

        void TheList_Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Check whether other item(s) need have their IsSelected-property be set to <false>
            //UnselectOtherItems((BoolStringClass)sender);
            var res = (
                     from item in TheList
                     where item.IsSelected == true
                     select item
                 ).ToList<BoolStringClass>();

            //Convert all items to a concatenated string:
            var res2 =
                    from item in TheList
                    select item.IsSelected ? item.TheText : "";
            //item.TheText + (item.IsSelected ? " is selected." : " is NOT selected.");
            //ModsListBox.Items.Add(string.Join("\r\n", new List<string>(res2).ToArray()));
            //string.Join("\r\n", new List<string>(res2).ToArray());
            foreach (int i in string.Join("\r\n", new List<string>(res2).ToArray()))
            {
                //System.Console.Write("{0} ", i);
                //ModsListBox.Items.Add(i);
            }

            StringBuilder sb = new StringBuilder();

            //foreach (var item in TheList)
            //{
            // if (item.IsSelected == true)
            //{
            //string seperator = @"\";
            //sb.AppendFormat(arma_2_mods_path + seperator + "{0};", item.TheText);
            //ModsListBox.Items.Add(item.TheText);
            //}
            //}


        }

        private async void CheckBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string data = "0";
            
            string urlAddress = "http://sync.austac.net.au/xapi/generate?collection_type=mods&type=xml&limit=all&mod_packagename=" + cb.Tag;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                //var Web_Browser = new WebBrowser {};
                //Web_Browser.write("<html><body style=\"background: #f0f0f0\"></body></html>");
                //Web_Browser.NavigateToString("<html><body style=\"background: #f0f0f0\">" + data + "</body></html>");

                var stackPanel = new StackPanel { Orientation = Orientation.Vertical };

                XDocument itemz = XDocument.Load(urlAddress);

                string name_title = "";
                foreach (var item in itemz.Descendants("item"))
                {
                    string name = item.Element("mod_name").Value;
                    string name_namespace = item.Element("mod_namespace").Value;
                    name_title = item.Element("mod_name").Value;
                    string mod_size = item.Element("mod_size").Value;
                    string updatedat = item.Element("mod_updatedat").Value;
                    string type = item.Element("mod_type").Value;

                    string updatedat_time = "Updated " + (TimeAgo(DateTime.Parse(updatedat)));
                    string createdat_time = "" + (TimeAgo(DateTime.Parse(item.Element("mod_createdat").Value)));

                    BitmapImage map_image = new BitmapImage(new Uri(item.Element("mod_image").Value, UriKind.Absolute));

                    stackPanel.Children.Add(new Image { Source = map_image, Width = 500, Height = 250, Stretch = Stretch.Fill, HorizontalAlignment = System.Windows.HorizontalAlignment.Left });

                    stackPanel.Children.Add(new TextBlock { Text = name + " (" + name_namespace + ")", FontSize = 14, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Margin = new System.Windows.Thickness(0, 15, 0, 0) });
                    stackPanel.Children.Add(new Separator { HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 20, 0, 20), });
                    stackPanel.Children.Add(new TextBlock { Text = updatedat_time, FontSize = 12, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Margin = new System.Windows.Thickness(0, 10, 0, 0), });


                    stackPanel.Children.Add(new TextBlock { FontWeight = FontWeights.Bold, Text = "Mod Info", FontSize = 14, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 5, 0, 0), Margin = new System.Windows.Thickness(0, 5, 0, 0), });
                    stackPanel.Children.Add(new Separator { Opacity = 0.2, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 20, 0, 20), });

                    if (item.Element("mod_dependencies").Value == null)
                    {
                        stackPanel.Children.Add(new TextBlock { Text = "No Mod Dependencies", FontSize = 12, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 0, 0, 5), Margin = new System.Windows.Thickness(0, 5, 0, 0), });
                        stackPanel.Children.Add(new Separator { Opacity = 0.2, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 20, 0, 20), });

                    }
                    else
                    {
                        stackPanel.Children.Add(new TextBlock { Text = "Required Mods: " + item.Element("mod_dependencies").Value, FontSize = 12, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 0, 0, 5), Margin = new System.Windows.Thickness(0, 5, 0, 0), });
                        stackPanel.Children.Add(new Separator { HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 20, 0, 20), });
                        
                    }

                    stackPanel.Children.Add(new TextBlock { Text = "Mod Size | " + mod_size, FontSize = 12, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 0, 0, 5), Margin = new System.Windows.Thickness(0, 5, 0, 0), });
                    stackPanel.Children.Add(new Separator { Opacity = 0.2, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 20, 0, 20), });


                    stackPanel.Children.Add(new TextBlock { Text = "Mod Created | " + createdat_time, FontSize = 12, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 0, 0, 5), Margin = new System.Windows.Thickness(0, 5, 0, 0), });
                    stackPanel.Children.Add(new Separator { Opacity = 0.2, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Width = 500, Padding = new System.Windows.Thickness(0, 20, 0, 20), });


                }

                await Task.Run(() => Thread.Sleep(2000));

                var wnd = new FirstFloor.ModernUI.Windows.Controls.ModernWindow
                {


                    Style = (Style)App.Current.Resources["BlankWindow"],
                    Title = "Viewing " + cb.Tag + " Mod info",
                    Topmost = true,
                    
                    IsTitleVisible = true,
                    Content = stackPanel,
                    Width = 510,
                    Height = 510
                };
                wnd.ResizeMode = ResizeMode.NoResize;
                wnd.Show();

                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();

                    sw.WriteLine(time + " | Success Fetching XML - URL: " + urlAddress);


                }

            }
            else
            {

                var wnd = new FirstFloor.ModernUI.Windows.Controls.ModernWindow
                {
                    Style = (Style)App.Current.Resources["BlankWindow"],
                    Title = "Server Connection Error",
                    IsTitleVisible = true,
                    Content = "Error! Server Down",
                    Width = 280,
                    Height = 280
                };
                wnd.Show();

                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();

                    sw.WriteLine(time + " | Error Fetching XML - Server Down - URL: " + urlAddress);


                }
            }
        }

        private async void Button_Click(object sender2, RoutedEventArgs e)
        {

            panelhide.Visibility = Visibility.Visible;
            panelnofile.Visibility = Visibility.Hidden;
            ModsListPanel.Visibility = Visibility.Hidden;

            await Task.Run(() => Thread.Sleep(2000));

            panelhide.Visibility = Visibility.Hidden;
            panelnofile.Visibility = Visibility.Hidden;


            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                foreach (var item in TheList) { }
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Mod List Refresh Requested");
                foreach (var item in TheList) { sw.WriteLine(time + " | List Item: " + item.TheText); }
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

            string arma3_mods_folder_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\Arma3ModsPath.txt"))
                {
                    arma3_mods_folder_value = AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath;
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

            string path2 = arma_3_path_value;
            string path = arma3_mods_folder_value;


            ///////////////////////////////////////////Start Check for Arma 2
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(path2))
                {

                    //error
                    //logging
                    //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Loading Arma 3 Path");
                    }

                }
                else
                {
                    ///////////////////////////////////////////Start Check for ArmA2OA.exe
                    //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Arma 3 Path Exists");
                    }

                    if (!System.IO.File.Exists(path2 + @"\arma3.exe"))
                    {

                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();
                            sw.WriteLine(time + " | arma3.exe Not Found");
                        }

                    }
                    else
                    {
                        ///////////////////////////////////////////Start Check for Arma 2 Mods Directory
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();
                            sw.WriteLine(time + " | arma3.exe Found");
                        }


                        try
                        {
                            // If the directory doesn't exist, create it.
                            if (!Directory.Exists(path))
                            {
                                //error
                                //logging
                                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Error Loading Arma 3 Mods Path");
                                }

                            }
                            else
                            {
                                //ok
                                //string api_url = FirstFloor.ModernUI.App.Properties.Settings.Default.armasync_base_url;
                                //string api_html_url = "xapi/generate?collection_type=mods&type=html&limit=all";
                                string[] dirs = Directory.GetDirectories(path, "@*");
                                //textbox.Text = "The number of directories starting with @ is {0}.", dirs.Length;
                                foreach (string dir in dirs)
                                {
                                    string FolderName = System.IO.Path.GetFileName(dir);
                                    //string FolderUpdated =  (TimeAgo(DateTime.Parse(Directory.GetLastWriteTime(dir).ToString())));


                                    bool alreadyExists = TheList.Any(x => x.TheText == FolderName);


                                    if (alreadyExists == true)
                                    {

                                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                        {
                                            string time = DateTime.Now.ToString();
                                            sw.WriteLine(time + " | Mod already Exists: " + FolderName);
                                        }

                                    }else{

                                        TheList.Add(new BoolStringClass { IsSelected = false, TheText = FolderName, TheUrl = FolderName });

                                    }
                                    




                                }
                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Arma 3 Mods Path Loaded");
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
                                sw.WriteLine(time + " | Error Loading Arma 3 Mods Path");
                            }
                        }

                        ///////////////////////////////////////////End Check for Arma 2 Mods Directory 

                        ///////////////////////////////////////////Start Check for Arma 2 Directory
                        try
                        {
                            // If the directory doesn't exist, create it.
                            if (!Directory.Exists(path2))
                            {
                                //error
                                //logging
                                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Error Loading Arma 3 Path");
                                }

                            }
                            else
                            {

                                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                {
                                    string time = DateTime.Now.ToString();
                                    sw.WriteLine(time + " | Arma 3 Path Found");
                                }

                                if (!System.IO.File.Exists(path2 + @"\arma3.exe"))
                                {

                                    //error
                                    //logging
                                    //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                    //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                    {
                                        string time = DateTime.Now.ToString();
                                        sw.WriteLine(time + " | arma3.exe Not Found");
                                    }

                                    //Folder is Not Set Correctly
                                    panelhide.Visibility = Visibility.Hidden;
                                    panelnofile.Visibility = Visibility.Visible;
                                    ModsListPanel.Visibility = Visibility.Hidden;

                                }
                                else
                                {

                                    //ok
                                    //string api_url = FirstFloor.ModernUI.App.Properties.Settings.Default.armasync_base_url;
                                    //string api_html_url = "xapi/generate?collection_type=mods&type=html&limit=all";
                                    string[] dirs = Directory.GetDirectories(path2, "@*");
                                    //textbox.Text = "The number of directories starting with @ is {0}.", dirs.Length;
                                    foreach (string dir in dirs)
                                    {
                                        string FolderName = System.IO.Path.GetFileName(dir);
                                        string TheText = FolderName;
                                        //string FolderUpdated = (TimeAgo(DateTime.Parse(Directory.GetLastWriteTime(dir).ToString())));

                                        bool alreadyExists = TheList.Any(x => x.TheText == FolderName);


                                        if (alreadyExists == true)
                                        {
                                            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                            {
                                                string time = DateTime.Now.ToString();
                                                sw.WriteLine(time + " | Mod already Exists: " + FolderName);
                                            }

                                        }
                                        else
                                        {

                                            TheList.Add(new BoolStringClass { IsSelected = false, TheText = FolderName, TheUrl = FolderName });

                                        }
                                        
                                        
                                    }

                                    
                                    bool ACRExists = TheList.Any(x => x.TheText == "ACR");


                                    if (ACRExists == true)
                                    {
                                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                        {
                                            string time = DateTime.Now.ToString();
                                            sw.WriteLine(time + " | Mod already Exists: ACR");
                                        }

                                    }
                                    else
                                    {

                                        TheList.Add(new BoolStringClass { IsSelected = false, TheText = "ACR" });

                                    }



                                    bool ACRLiteExists = TheList.Any(x => x.TheText == "ACRLite");


                                    if (ACRLiteExists == true)
                                    {
                                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                                        {
                                            string time = DateTime.Now.ToString();
                                            sw.WriteLine(time + " | Mod already Exists: ACRLite");
                                        }

                                    }
                                    else
                                    {

                                        TheList.Add(new BoolStringClass { IsSelected = false, TheText = "ACRLite" });

                                    }

                                }

                            }//END if (!System.IO.File.Exists(path2 + @"\ArmA2OA.exe"));
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
                                sw.WriteLine(time + " | Error Loading Arma 3 Path");
                            }
                        }

                        ///////////////////////////////////////////End Check for Arma 2 Directory

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
                    sw.WriteLine(time + " | Error Loading Arma 3 Path");
                }

                //Folder is Not Set Correctly
                panelhide.Visibility = Visibility.Hidden;
                panelnofile.Visibility = Visibility.Visible;
                ModsListPanel.Visibility = Visibility.Hidden;
            }


            ModsListPanel.Visibility = Visibility.Visible;
           
        }

        private async void pw6_Click(object sender, RoutedEventArgs e)
        {

            panelhide.Visibility = Visibility.Visible;
            panelnofile.Visibility = Visibility.Hidden;
            ModsListPanel.Visibility = Visibility.Hidden;
            pw6.Visibility = Visibility.Hidden;
            pw6_loading.Visibility = Visibility.Visible;


            await Task.Run(() => Thread.Sleep(8000));


            StringBuilder sb = new StringBuilder();

            foreach (var item in TheList)
            {
            if (item.IsSelected == true)
             {
                sb.AppendFormat(item.TheText + " "); }
             }
             string app_ouput = sb.ToString();

             Random rnd = new Random();
             int rand = rnd.Next(999, 9999);
             int rand2 = rnd.Next(999, 9999);

             string set_user = "user_id=" + rand + rand2;
             string set_game = "&game_id=arma3";
             string set_mods = "&mods_array=" + app_ouput;
             string set_name = "&collection_name=AusTac Quick2Launch Mods Check";
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://sync.austac.net.au/wpf/uploads/quick2launch/index.php?" + set_user + set_game + set_name + set_mods);
             HttpWebResponse response = (HttpWebResponse)request.GetResponse();
             if (response.StatusCode == HttpStatusCode.OK)
             {


             string pw6path = "sync.austac.net.au/wpf/uploads/check_" +rand + rand2 + "/default.yml";

             System.Diagnostics.Process.Start(@"pws://" + pw6path);
             string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
             string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
             using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
             {
                 string time = DateTime.Now.ToString();
                 sw.WriteLine(time + " | Play withSix Path: " + pw6path);
             }

             }


              panelhide.Visibility = Visibility.Hidden;
              panelnofile.Visibility = Visibility.Hidden;
              ModsListPanel.Visibility = Visibility.Visible;
              pw6.Visibility = Visibility.Visible;
              pw6_loading.Visibility = Visibility.Hidden;


        }

        private void ClearSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in TheList)
            {
                if (item.IsSelected == true)
                {
                    item.IsSelected = false;
                }
            }

            //ModsLister.Items.Clear();
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();

                sw.WriteLine(time + " | Arma 3 Launcher Selected Mods Cleared");


            }
        }


        
    }
}
