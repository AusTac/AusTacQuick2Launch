using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using System.Windows.Threading;
using System.Windows.Forms;

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to Settings XAML");
            }
            //RestartButton.Visibility = Visibility.Hidden;
            //Settings
            /*
            //Removed as now saving settings to file
            arma_2_path.Text = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
            arma_3_path.Text = AusTacQuick2Launch.Properties.Settings.Default.Arma3Path;
            arma2_mods_folder.Text = AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath;
            arma3_mods_folder.Text = AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath;
            */
            //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
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
            arma_2_path.Text = arma_2_path_value;
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
            arma_3_path.Text = arma_3_path_value;
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
            arma2_mods_folder.Text = arma2_mods_folder_value;
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
            arma3_mods_folder.Text = arma3_mods_folder_value;

            
            //Get Debug Value from file if exists
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\debug"))
                {
                    allow_debugmode.IsChecked = AusTacQuick2Launch.Properties.Settings.Default.debug;
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Checking Debug File");
                        
                    }
                }
                else
                {
                    string getdebugvaluebool = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\debug").First();
                    allow_debugmode.IsChecked = Convert.ToBoolean(getdebugvaluebool);
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
                    sw.WriteLine(time + " | Error Checking Debug File");
                    allow_debugmode.IsChecked = AusTacQuick2Launch.Properties.Settings.Default.debug;
                }
            }

            //Get Splash Value from file if exists
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\splash"))
                {
                    allow_splash.IsChecked = AusTacQuick2Launch.Properties.Settings.Default.splash;
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Checking Splash File");

                    }
                }
                else
                {
                    string getsplashvaluebool = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\splash").First();
                    allow_splash.IsChecked = Convert.ToBoolean(getsplashvaluebool);
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
                    sw.WriteLine(time + " | Error Checking Splash File");
                    allow_splash.IsChecked = AusTacQuick2Launch.Properties.Settings.Default.splash;
                }
            }

            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma_2_path.Text))
                {
                    //Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    //BitmapImage imgSource = new BitmapImage(uri);
                    //arma_2_path_ok.Source = imgSource;

                }
                else
                {
                    if (!System.IO.File.Exists(arma_2_path.Text + @"\ArmA2OA.exe"))
                    {

                        //Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                        //BitmapImage imgSource = new BitmapImage(uri);
                        //arma_2_path_ok.Source = imgSource;

                    }
                    else
                    {

                        //Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                        //BitmapImage imgSource = new BitmapImage(uri);
                        //arma_2_path_ok.Source = imgSource;

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
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }

            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma_3_path.Text))
                {
                    //Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    //BitmapImage imgSource = new BitmapImage(uri);
                    //arma_3_path_ok.Source = imgSource;

                }
                else
                {
                    if (!System.IO.File.Exists(arma_3_path.Text + @"\arma3.exe"))
                    {

                        //Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                        //BitmapImage imgSource = new BitmapImage(uri);
                        //arma_3_path_ok.Source = imgSource;

                    }
                    else
                    {

                        //Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                        //BitmapImage imgSource = new BitmapImage(uri);
                        //arma_3_path_ok.Source = imgSource;

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
                    sw.WriteLine(time + " | Error Checking Arma 3 Path");
                }
            }

            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma3_mods_folder.Text))
                {
                    //Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    //BitmapImage imgSource = new BitmapImage(uri);
                    //arma3_mods_folder_ok.Source = imgSource;

                }
                else
                {

                    //Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    //BitmapImage imgSource = new BitmapImage(uri);
                    //arma3_mods_folder_ok.Source = imgSource;
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
                    sw.WriteLine(time + " | Error Checking Arma 3 Mods Path");
                }
            }

            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma2_mods_folder.Text))
                {
                    //Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    //BitmapImage imgSource = new BitmapImage(uri);
                    //arma2oa_mods_folder_ok.Source = imgSource;

                }
                else
                {

                    //Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    //BitmapImage imgSource = new BitmapImage(uri);
                    //arma2oa_mods_folder_ok.Source = imgSource;
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
                    sw.WriteLine(time + " | Error Checking Arma 2 Mods Path");
                }
            }
            
        }


        //allow_debugmode
        private void allow_debugmode_Checked(object sender, RoutedEventArgs e)
        {
            //FirstFloor.ModernUI.App.Properties.Settings.Default.launch_setting_nosplash = launch_setting_nosplash.IsChecked == false;
            AusTacQuick2Launch.Properties.Settings.Default.debug = allow_debugmode.IsChecked.HasValue ? allow_debugmode.IsChecked.Value : false;
            Properties.Settings.Default.Save();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Debug Setting Saved - Debug Enabled");

            }

            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/debug"))
            { sw.WriteLine("true"); }
        }

        private void allow_debugmode_Unchecked(object sender, RoutedEventArgs e)
        {
            //FirstFloor.ModernUI.App.Properties.Settings.Default.launch_setting_nosplash = launch_setting_nosplash.IsChecked == true;
            AusTacQuick2Launch.Properties.Settings.Default.debug = allow_debugmode.IsChecked.HasValue ? allow_debugmode.IsChecked.Value : false;
            Properties.Settings.Default.Save();


            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Debug Setting Saved - Debug Disabled");

            }

            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/debug"))
            { sw.WriteLine("false"); }
        }

        //allow_debugmode
        private void allow_splash_Checked(object sender, RoutedEventArgs e)
        {
            //FirstFloor.ModernUI.App.Properties.Settings.Default.launch_setting_nosplash = launch_setting_nosplash.IsChecked == false;
            AusTacQuick2Launch.Properties.Settings.Default.splash = allow_splash.IsChecked.HasValue ? allow_splash.IsChecked.Value : false;
            Properties.Settings.Default.Save();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Splash Setting Saved - Splash Disabled");

            }

            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/splash"))
            { sw.WriteLine("true"); }

        }

        private void allow_splash_Unchecked(object sender, RoutedEventArgs e)
        {
            //FirstFloor.ModernUI.App.Properties.Settings.Default.launch_setting_nosplash = launch_setting_nosplash.IsChecked == true;
            AusTacQuick2Launch.Properties.Settings.Default.splash = allow_splash.IsChecked.HasValue ? allow_splash.IsChecked.Value : false;
            Properties.Settings.Default.Save();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Splash Setting Saved - Splash Enabled");

            }

            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/splash"))
            { sw.WriteLine("false"); }
        }

        public void showProgress()
        {


            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3)
            };

            timer.Tick += delegate(object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                SaveButton.Visibility = Visibility.Visible;
                onclick_progress.Visibility = Visibility.Hidden;
                RestartButton.Visibility = Visibility.Visible;
            };

            timer.Start();

        }


        private void Restart()
        {

            //Application.Current.Shutdown();
            //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            
            // from System.Windows.Forms.dll
            //System.Windows.Forms.Application.Restart();
            //Application.Current.Shutdown();

            //String ApplicationEntryPoint = ApplicationDeployment.CurrentDeployment.UpdatedApplicationFullName;
            //Process.Start(ApplicationEntryPoint);

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Process.GetCurrentProcess().Kill();
        }

        
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
           
            Restart();
            
        }



        private void Save_Click(object sender, RoutedEventArgs e)
        {
           
            //Needed as fallback

            
            AusTacQuick2Launch.Properties.Settings.Default.Arma2Path = arma_2_path.Text;
            AusTacQuick2Launch.Properties.Settings.Default.Arma3Path = arma_3_path.Text;
            AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath = arma2_mods_folder.Text;
            AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath = arma3_mods_folder.Text;
            

            //Create Writes to Files
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            //Save Arma 2 Path
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma2Path.txt"))
            { sw.WriteLine(arma_2_path.Text); }
            //Save Arma 2 Mods Path
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma2ModsPath.txt"))
            { sw.WriteLine(arma2_mods_folder.Text); }
            //Save Arma 3 Path
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma3Path.txt"))
            { sw.WriteLine(arma_3_path.Text); }
            //Save Arma 3 Mods Path
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma3ModsPath.txt"))
            { sw.WriteLine(arma3_mods_folder.Text); }

            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/restart"))
            { sw.WriteLine("true"); }

            SaveButton.Visibility = Visibility.Hidden;
            onclick_progress.Visibility = Visibility.Visible;
            RestartButton.Visibility = Visibility.Hidden;

            showProgress();
            AusTacQuick2Launch.Properties.Settings.Default.Save();
            //logging
            //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Saved Settings");
            }
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma_2_path.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma_2_path_ok.Source = imgSource;

                }
                else
                {

                    if (!System.IO.File.Exists(arma_2_path.Text + @"\ArmA2OA.exe"))
                    {

                        Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_2_path_ok.Source = imgSource;

                    }
                    else
                    {

                        Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_2_path_ok.Source = imgSource;

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
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma_3_path.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma_3_path_ok.Source = imgSource;

                }
                else
                {

                    if (!System.IO.File.Exists(arma_3_path.Text + @"\arma3.exe"))
                    {

                        Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_3_path_ok.Source = imgSource;

                    }
                    else
                    {

                        Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_3_path_ok.Source = imgSource;

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
                    sw.WriteLine(time + " | Error Checking Arma 3 Path");
                }
            }

            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma2_mods_folder.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2oa_mods_folder_ok.Source = imgSource;

                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2oa_mods_folder_ok.Source = imgSource;
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
                    sw.WriteLine(time + " | Error Checking Arma 2 Mods Path");
                }
            }
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma3_mods_folder.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_mods_folder_ok.Source = imgSource;

                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_mods_folder_ok.Source = imgSource;
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
                    sw.WriteLine(time + " | Error Checking Arma 3 Mods Path");
                }
            }
        }



        private void OnExit(object sender, ExitEventArgs e)
        {
            AusTacQuick2Launch.Properties.Settings.Default.Save();

        }

        /*
        //Removed as now saving settings to file
         * 
        private void arma2_path_TextChanged(object sender, TextChangedEventArgs e)
        {
            AusTacQuick2Launch.Properties.Settings.Default.Save();
            SaveButton.Visibility = Visibility.Hidden;
            //RestartButton.Visibility = Visibility.Visible;
            onclick_progress.Visibility = Visibility.Visible;
            showProgress();
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma_2_path.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma_2_path_ok.Source = imgSource;

                }
                else
                {

                    if (!System.IO.File.Exists(arma_2_path.Text + @"\ArmA2OA.exe"))
                    {

                        Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_2_path_ok.Source = imgSource;

                    }
                    else
                    {

                        Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_2_path_ok.Source = imgSource;

                    }
                }
            }
            catch (Exception)
            {
                // Fail silently
                //logging
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacSyncFolder_Log = System.IO.Path.Combine(appfolder, "AusTacSync/Log");
                using (StreamWriter sw = File.AppendText(AusTacSyncFolder_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }

        }
        private void arma_3_path_TextChanged(object sender, TextChangedEventArgs e)
        {
            AusTacQuick2Launch.Properties.Settings.Default.Save();
            SaveButton.Visibility = Visibility.Hidden;
            //RestartButton.Visibility = Visibility.Visible;
            onclick_progress.Visibility = Visibility.Visible;
            showProgress();
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma_3_path.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma_3_path_ok.Source = imgSource;

                }
                else
                {

                    if (!System.IO.File.Exists(arma_3_path.Text + @"\arma3.exe"))
                    {

                        Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_3_path_ok.Source = imgSource;

                    }
                    else
                    {

                        Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                        BitmapImage imgSource = new BitmapImage(uri);
                        arma_3_path_ok.Source = imgSource;

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
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }
        }
        private void arma2_mods_folder_TextChanged(object sender, TextChangedEventArgs e)
        {
            AusTacQuick2Launch.Properties.Settings.Default.Save();
            SaveButton.Visibility = Visibility.Hidden;
            //RestartButton.Visibility = Visibility.Visible;
            onclick_progress.Visibility = Visibility.Visible;
            showProgress();
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma2_mods_folder.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2oa_mods_folder_ok.Source = imgSource;

                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma2oa_mods_folder_ok.Source = imgSource;
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
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }


        }
        private void arma3_mods_folder_TextChanged(object sender, TextChangedEventArgs e)
        {
            AusTacQuick2Launch.Properties.Settings.Default.Save();
            SaveButton.Visibility = Visibility.Hidden;
            //RestartButton.Visibility = Visibility.Visible;
            onclick_progress.Visibility = Visibility.Visible;
            showProgress();
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(arma3_mods_folder.Text))
                {
                    Uri uri = new Uri("/Assets/error 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_mods_folder_ok.Source = imgSource;

                }
                else
                {

                    Uri uri = new Uri("/Assets/tick 1.png", UriKind.Relative);
                    BitmapImage imgSource = new BitmapImage(uri);
                    arma3_mods_folder_ok.Source = imgSource;
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
                    sw.WriteLine(time + " | Error Checking Arma 2 Path");
                }
            }
        }
        */

        private void buttonA2OA_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string path = dlg.SelectedPath;
                arma_2_path.Text = path;
                //logging
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Opened Folder " + dlg.SelectedPath);
                }
            }
        }

        private void buttonA3_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string path = dlg.SelectedPath;
                arma_3_path.Text = path;
                //logging
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Opened Folder " + dlg.SelectedPath);
                }
            }
        }

        private void buttonA2OAMods_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string path = dlg.SelectedPath;
                arma2_mods_folder.Text = path;
                //logging
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Opened Folder " + dlg.SelectedPath);
                }
            }
        }

        private void buttonA3Mods_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string path = dlg.SelectedPath;
                arma3_mods_folder.Text = path;
                //logging
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Opened Folder " + dlg.SelectedPath);
                }
            }
        }


    }
}
