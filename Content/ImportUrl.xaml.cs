using System;
using System.Collections.Generic;
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

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for ImportUrl.xaml
    /// </summary>
    public partial class ImportUrl : UserControl
    {
        public ImportUrl()
        {
            InitializeComponent();
            Loaded += ImportUrl_Loaded;
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();

                sw.WriteLine(time + " | Navigated to Import URL XAML");


            }
        }

        private void ImportUrl_Loaded(object sender, RoutedEventArgs e)
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
                sw.WriteLine(time + " | Import Url Array Loaded");

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

        private void Validate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LaunchWithArray_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
