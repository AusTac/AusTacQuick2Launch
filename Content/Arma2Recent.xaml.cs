using AusTacQuick2Launch.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
    /// Interaction logic for Arma2Recent.xaml
    /// </summary>
    public partial class Arma2Recent : UserControl
    {

        public ObservableCollection<BoolStringClass> Arma2RecentList { get; set; }
        
        public Arma2Recent()
        {
            InitializeComponent();
            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to Arma 2 Recent XAML");

            }
            Loaded += Arma2Recent_Loaded;
        }

        private void Arma2Recent_Loaded(object sender, RoutedEventArgs e)
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
                sw.WriteLine(time + " | Arma 2 Recent Loaded");

            }

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

        public void showProgress()
        {


            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            timer.Tick += delegate(object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                
                try
                {
                    string arma2recentdir = "";
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma2");
                    if (System.IO.Directory.GetFiles(AusTacQuick2Launch_Recent).Length == 0)
                    {
                        // folder has no files..
                        arma2recentdir = "false";
                    }
                    else
                    {
                        // folder has files..
                        arma2recentdir = "true";
                    }


                    // If the directory doesn't exist, create it.
                    if (arma2recentdir == "false")
                    {
                        //Arma 2 Recent is Empty
                        panelhide.Visibility = Visibility.Hidden;
                        panelnofile.Visibility = Visibility.Visible;
                        //TheListOutput();
                        ModsListPanel.Visibility = Visibility.Hidden;
                        //logging
                        //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();

                            sw.WriteLine(time + " | Arma 2 Recent is Empty");


                        }
                    }
                    else
                    {
                        //Arma 2 Recent is present
                        panelhide.Visibility = Visibility.Hidden;
                        panelnofile.Visibility = Visibility.Hidden;
                        ModsListPanel.Visibility = Visibility.Visible;
                        TheListOutput();
                        //logging
                        //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();

                            sw.WriteLine(time + " | Loaded items from Arma 2 Recent folder");


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
                        sw.WriteLine(time + " | Error in Arma 2 Recent List");

                    }
                }


            };

            timer.Start();

        }


        private async void TheListOutput()
        {



            Arma2RecentList = new ObservableCollection<BoolStringClass>();

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma2");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
                DirectoryInfo d = new DirectoryInfo(AusTacQuick2Launch_Recent);
                foreach (var file in d.GetFiles("*.recent"))
                {
                    //Directory.Move(file.FullName, filepath + "\\TextFiles\\" + file.Name);

                    string file_path = AusTacQuick2Launch_Recent + "/" + file.Name + "".ToString();
                    string[] all_mods = System.IO.File.ReadAllLines(file_path);

                    StringBuilder sb = new StringBuilder();
                    foreach (string mods in all_mods)
                    {
                        sb.AppendFormat(mods + ",");
                    }
                    string app_ouput = sb.ToString();
                    string time = (TimeAgo(DateTime.Parse(File.GetLastWriteTime(file_path).ToString())));
                    Arma2RecentList.Add(new BoolStringClass { IsSelected = false, TheText = time + " | " + app_ouput, TheUrl = file.Name + all_mods });
                }

                foreach (var item in Arma2RecentList)
                {
                    Console.WriteLine("List Item");
                }

             string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
             using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
             {
             string time = DateTime.Now.ToString();
             sw.WriteLine(time + " | Arma 2 Recent List Loaded");
             }

             await Task.Run(() => Thread.Sleep(100));         
                //Folder is Not Set Correctly
                panelhide.Visibility = Visibility.Hidden;
                panelnofile.Visibility = Visibility.Hidden;
                ModsListPanel.Visibility = Visibility.Visible;


                //Provide change-notification for all members
                foreach (var item in Arma2RecentList)
                    item.PropertyChanged += Arma2RecentList_Item_PropertyChanged;

                this.DataContext = this;

            }

        void Arma2RecentList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            	


        }

        void Arma2RecentList_Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Check whether other item(s) need have their IsSelected-property be set to <false>
            //UnselectOtherItems((BoolStringClass)sender);
            var res = (
                     from item in Arma2RecentList
                     where item.IsSelected == true
                     select item
                 ).ToList<BoolStringClass>();

            //Convert all items to a concatenated string:
            var res2 =
                    from item in Arma2RecentList
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

    }
}
