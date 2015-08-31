using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for ImportArray.xaml
    /// </summary>
    public partial class ImportArray : UserControl
    {
        public ImportArray()
        {
            InitializeComponent();
            Loaded += ImportArray_Loaded;
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();

                sw.WriteLine(time + " | Navigated to Import Array XAML");


            }
        }


        private void ImportArray_Loaded(object sender, RoutedEventArgs e)
        {
            panelhide.Visibility = Visibility.Visible;
            panelnofile.Visibility = Visibility.Hidden;
            PageOutput.Visibility = Visibility.Hidden;
            showProgress();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Import Array Loaded");

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
                string arma_2_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
                string arma2oa_mods_folder = AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath;
                try
                {
                    // If the directory doesn't exist, create it.
                    if (!Directory.Exists(arma_2_path))
                    {
                        //Folder is Not Set Correctly
                        panelhide.Visibility = Visibility.Hidden;
                        panelnofile.Visibility = Visibility.Visible;
                        //TheListOutput();
                        PageOutput.Visibility = Visibility.Hidden;
                        //logging
                        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();

                            sw.WriteLine(time + " | Arma Path(s) Not Set");


                        }
                    }
                    else
                    {
                        //Folder is Set Correctly
                        panelhide.Visibility = Visibility.Hidden;
                        panelnofile.Visibility = Visibility.Hidden;
                        PageOutput.Visibility = Visibility.Visible;
                        //logging
                        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();

                            sw.WriteLine(time + " | Loaded Import via Array");


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
                        sw.WriteLine(time + " | Error in Import via Array");

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

        private void LaunchWithArray_Click(object sender, RoutedEventArgs e)
        {

            panelhide.Visibility = Visibility.Visible;
            panelnofile.Visibility = Visibility.Hidden;
            PageOutput.Visibility = Visibility.Hidden;
            showProgress();
            MessageBorder.Visibility = Visibility.Hidden;

            string armagame = "";
            if (arma2selected.IsChecked == true)
            {
                armagame = "Arma2";
            }
            else if (arma3selected.IsChecked == true)
            {
                armagame = "Arma3";
            }

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


            string arma_path = "";
            string arma_mods_path = "";
            string arma_exe = "";
            string mods = "";
            string ArmaExtrasOriginal = "";
            string ArmaExtras = "";

            if (armagame == "Arma2")
            {
                arma_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
                arma_mods_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath;
                arma_exe = "ArmA2OA.exe";
                mods = "-mod=";
                ArmaExtrasOriginal = SteamFolder() + @"\SteamApps\Common\Arma 2;" + SteamFolder() + @"\SteamApps\Common\Arma 2 operation arrowhead;" + "expansion;";
                ArmaExtras = "";
                ArmaExtras = ArmaExtrasOriginal.Replace(@"\", "/");

            }
            else if (armagame == "Arma3")
            {
                arma_path = AusTacQuick2Launch.Properties.Settings.Default.Arma3Path;
                arma_mods_path = AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath;
                arma_exe = "arma3.exe";
                mods = "-mod=";

            }




            StringBuilder sb = new StringBuilder();
            if (mods_array.Text.Length > 0)
            {
                //label1.Text = string.Empty;
                string[] arr = mods_array.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arr.Length; i++)
                {
                    //sb.AppendFormat(arr[i] + ";");

                    if (arr[i] == "ACR")
                    {
                        string seperator = @"\";
                        sb.AppendFormat(arma_path + seperator + "{0};", arr[i]);

                    }
                    else
                    {
                        if (arr[i] == "ACRLite")
                        {
                            string seperator = @"\";
                            sb.AppendFormat(arma_path + seperator + "{0};", arr[i]);

                        }
                        else
                        {

                            string seperator = @"\";
                            sb.AppendFormat(arma_mods_path + seperator + "{0};", arr[i]);
                        }

                    }




                    //Console.WriteLine(arr[i]);
                }
                string app_ouput = sb.ToString();

                Process.StartInfo.Arguments = ip_address_connect + port_address_connect + password_connect + '"' + mods + ArmaExtras + app_ouput + '"';
                //Process.StartInfo.Arguments = scripterrors;
                string combined = System.IO.Path.Combine(arma_path, arma_exe);
                Process.StartInfo.FileName = combined;
                Process.Start();

                //Save to Recent
                if (launch_setting_allowrecent.IsChecked == true)
                {

                    Random rnd = new Random();
                    int rand = rnd.Next(999, 9999);
                    int rand2 = rnd.Next(999, 9999);
                    //logging
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent");
                    using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Recent + "/" + armagame + "/" + rand + rand2 + ".recent"))
                    {
                        string[] allowrecent = app_ouput.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < arr.Length; i++)
                        {
                            sw.WriteLine(allowrecent[i]);
                        }



                    }



                }

            }
            else
            {

                /*
                var wnd = new FirstFloor.ModernUI.Windows.Controls.ModernWindow
                {
                    Style = (Style)App.Current.Resources["BlankWindow"],
                    Title = "Mods Array Error",
                    IsTitleVisible = true,
                    Content = "Cant import & launch without any mods in the array!",
                    Width = 280,
                    Height = 280
                };
                wnd.Show();
                */

                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                MessageBorder.Visibility = Visibility.Visible;


                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();

                    sw.WriteLine(time + " | No Mods in Array");


                }

            }


        }


        private async void pw6_Click(object sender, RoutedEventArgs e)
        {

            panelhide.Visibility = Visibility.Visible;
            panelnofile.Visibility = Visibility.Hidden;
            PageOutput.Visibility = Visibility.Hidden;
            pw6.Visibility = Visibility.Hidden;
            pw6_loading.Visibility = Visibility.Visible;


            await Task.Run(() => Thread.Sleep(8000));

            string armagame = "";
            if (arma2selected.IsChecked == true)
            {
                armagame = "arma2co";
            }
            else if (arma3selected.IsChecked == true)
            {
                armagame = "arma3";
            }




            StringBuilder sb = new StringBuilder();
            if (mods_array.Text.Length > 0)
            {
                //label1.Text = string.Empty;
                string[] arr = mods_array.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arr.Length; i++)
                {

                    sb.AppendFormat(arr[i]);

                }
                string app_ouput = sb.ToString();

                Random rnd = new Random();
                int rand = rnd.Next(999, 9999);
                int rand2 = rnd.Next(999, 9999);

                string set_user = "user_id=" + rand + rand2;
                string set_game = "&game_id=" + armagame;
                string set_mods = "&mods_array=" + app_ouput;
                string set_name = "&collection_name=AusTac Quick2Launch Mods Check (Via Array)";
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
                PageOutput.Visibility = Visibility.Visible;
                pw6.Visibility = Visibility.Visible;
                pw6_loading.Visibility = Visibility.Hidden;


            }

        }
    }
}


