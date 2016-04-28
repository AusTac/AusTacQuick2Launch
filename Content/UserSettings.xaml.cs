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
    public partial class UserSettings : UserControl
    {
        public UserSettings()
        {
            InitializeComponent();
            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to UserSettings XAML");
            }
            
            //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string user_name_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\UserName.txt"))
                {
                    user_name_value = AusTacQuick2Launch.Properties.Settings.Default.UserName;
                }
                else
                {
                    user_name_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\UserName.txt").First();
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
                    sw.WriteLine(time + " | Error User Name File");
                }
            }
            user_name.Text = user_name_value;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string hashkey_value = "";
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\HashKey.txt"))
                {
                    hashkey_value = AusTacQuick2Launch.Properties.Settings.Default.HashKey;
                }
                else
                {
                    hashkey_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\HashKey.txt").First();
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
                    sw.WriteLine(time + " | Error HashKey File");
                }
            }
            hashkey.Password = hashkey_value;

            
            
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

            
            AusTacQuick2Launch.Properties.Settings.Default.UserName = user_name.Text;
            AusTacQuick2Launch.Properties.Settings.Default.HashKey = hashkey.Password;
            

            //Create Writes to Files
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/UserName.txt"))
            { sw.WriteLine(user_name.Text); }

            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/HashKey.txt"))
            { sw.WriteLine(hashkey.Password); }

            using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/restart"))
            { sw.WriteLine("true"); }


            SaveButton.Visibility = Visibility.Hidden;
            onclick_progress.Visibility = Visibility.Visible;

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

    }
}
