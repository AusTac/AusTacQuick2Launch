using FirstFloor.ModernUI.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using SplashDemo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
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
using System.Xml.Linq;

namespace AusTacQuick2Launch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        
        public TaskbarIcon MyTasksBar;
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public MainWindow()
        {
            InitializeComponent();
            Splasher.CloseSplash();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | App Launched");
                sw.WriteLine(time + " | Main Window Loaded");

            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            watcher.Path = AusTacQuick2Launch_Settings;


            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "restart";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;


            //ShowCustomBalloon();
            MyTasksBar = (TaskbarIcon)FindResource("MyNotifyIcon");

            //MyTasksBar.ContextMenu.Items.Add(new MenuItem { Header = "Item" });
            MyTasksBar.ContextMenu.Items.Add(CreateA3SubMenu("Launch Arma 3 OA"));
            MyTasksBar.ContextMenu.Items.Add(CreateA2SubMenu("Launch Arma 2 OA"));
            MyTasksBar.ContextMenu.Items.Add(new Separator());
            //MyTasksBar.ContextMenu.Items.Add(new MenuItem { Header = "Open", Name="TaskbarOpen" });
            //MyTasksBar.ContextMenu.Items.Add(new MenuItem { Header = "Exit", Name = "TaskbarExit" });


            MenuItem TaskbarOpenItem = (new MenuItem { Header = "Open", Name = "TaskbarOpen" });
            TaskbarOpenItem.Click += TaskbarOpenItem_Click;
            MyTasksBar.ContextMenu.Items.Add(TaskbarOpenItem);


            MenuItem TaskbarExitItem = (new MenuItem { Header = "Exit", Name = "TaskbarExit" });
            TaskbarExitItem.Click += TaskbarExitItem_Click;
            MyTasksBar.ContextMenu.Items.Add(TaskbarExitItem);
            Loaded += MainWindow_Loaded;

        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dueTime = TimeSpan.FromSeconds(5);
            var interval = TimeSpan.FromSeconds(5);

            // TODO: Add a CancellationTokenSource and supply the token here instead of None.
            DoPeriodicWorkAsync(dueTime, interval, CancellationToken.None);

        }

        // Define the event handlers. 
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //ChangeRestartMessage();
            
        }

        private async Task DoPeriodicWorkAsync(TimeSpan dueTime,
                                       TimeSpan interval,
                                       CancellationToken token)
        {
            // Initial wait time before we begin the periodic loop.
            if (dueTime > TimeSpan.Zero)
                await Task.Delay(dueTime, token);

            // Repeat this loop until cancelled.
            while (!token.IsCancellationRequested)
            {
                // TODO: Do some kind of work here. 
                Console.WriteLine("Restart is Polling..");
                try
                {
                    Console.WriteLine("Restart is Polling..TRY");
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
                    if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + "/restart"))
                    {
                        Console.WriteLine("Restart is Polling..File Check Not Found");
 
                      }else{

                          
                          Console.WriteLine("Restart is Polling..File Check Found");
                          string restart_value = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\restart").First();

                          if (restart_value == "true")
                          {

                              RestartAction.Visibility = Visibility.Visible;
                              Console.WriteLine("Restart Set to True");

                          }
                          else
                          {

                              Console.WriteLine("Restart Set to False");

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
                                sw.WriteLine(time + " | Error Checking Restart File");
                            }

                            Console.WriteLine("Restart is Polling..EXECEPTION");
                        }
                

                // Wait to repeat again.
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }

        public void ChangeRestartMessage() 
        {

            var dueTime = TimeSpan.FromSeconds(5);
            var interval = TimeSpan.FromSeconds(5);

            // TODO: Add a CancellationTokenSource and supply the token here instead of None.
            DoPeriodicWorkAsync(dueTime, interval, CancellationToken.None);

        }


        private MenuItem CreateA3SubMenu(string header)
        {
            var item = new MenuItem { Header = header };

            //add foreach here
            item.Items.Add(new MenuItem { Header = "Launch in Vanilla Mode" });


            item.Items.Add(new Separator());
            item.Items.Add("Launch in Vanilla Mode");
            return item;
        }

        private MenuItem CreateA2SubMenu(string header)
        {
            var item = new MenuItem { Header = header };

            //add foreach here
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacSyncFolderA2 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma2/");

            DirectoryInfo d = new DirectoryInfo(AusTacSyncFolderA2);//Assuming Test is your Folder
            if (!System.IO.File.Exists(AusTacSyncFolderA2))
            {
                FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files
                foreach (FileInfo file in Files)
                {
                    //string str = str + ", " + file.Name;
                    string FolderName = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileName(file.Name));
                    //BoxA.Items.Add(FolderName);
                    MenuItem A2FolderNameMode = (new MenuItem { Header = FolderName });
                    A2FolderNameMode.Click += A2FolderNameMode_Click;
                    item.Items.Add(A2FolderNameMode);
                    //item.Items.Add(new MenuItem { Header = FolderName, Tag = FolderName,  });
                    //FolderNameMode.Click += FolderNameMode_Click;
                }

                item.Items.Add(new Separator());

                MenuItem A2LaunchVanillaMode = (new MenuItem { Header = "Launch in Vanilla Mode", Name = "A2LaunchVanillaMode" });
                A2LaunchVanillaMode.Click += A2LaunchVanillaMode_Click;
                item.Items.Add(A2LaunchVanillaMode);
            }
                //item.Items.Add("Launch in Vanilla Mode");
                return item;

            }
        
        void A2FolderNameMode_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            //MessageBox.Show("You clicked on " + menuItem.Header);
            string arma_2_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
            string arma_2_exe = "ArmA2OA.exe";
            string arma_2_mods_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath;
            string mods = "-mod=";
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AusTacQuick2Launch/Recent/Arma2/");
            string mod_params_path = path + menuItem.Header + ".xml";
            XDocument items = XDocument.Load(mod_params_path);
            StringBuilder sb = new StringBuilder();
            foreach (var item in items.Descendants("data"))
            {
            //mod_params = item.Element("mod_params").Value;
            if (arma_2_mods_path == null) {
                //string seperator = @"\";
                sb.AppendFormat("{0};", item.Element("value").Value);
            } else {
                string seperator = @"\";
                sb.AppendFormat(arma_2_mods_path + seperator + "{0};", item.Element("value").Value);
            }
            
            }
            string app_ouput = sb.ToString();
            Process Process = new Process();

            Process.StartInfo.Arguments = mods + app_ouput;
            //Process.StartInfo.Arguments = scripterrors;
            string combined = System.IO.Path.Combine(arma_2_path, arma_2_exe);
            Process.StartInfo.FileName = combined;

            Process.Start();
            this.WindowState = WindowState.Minimized;
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Launching Arma 2 with Params: " + Process.StartInfo.Arguments);
                sw.WriteLine(time + " | Launching Arma 2 from: " + Process.StartInfo.FileName);

            }
        }

        void A2ModsList_Click(object sender, EventArgs e)
        {
            //NavigationService navService = NavigationService .GetNavigationService(this);
            //navService.Navigate(new System.Uri("Window2.xaml", UriKind.Relative));
        }

        void A3ModsList_Click(object sender, EventArgs e)
        {

        }

        void A2ServersList_Click(object sender, EventArgs e)
        {

        }

        void A3ServersList_Click(object sender, EventArgs e)
        {

        }


        void A2LaunchVanillaMode_Click(object sender, EventArgs e)
        {

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Launch Arma 2 Vanilla Rquested");

            }
            Process Process = new Process();
            string arma_2_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
            string arma_2_exe = "ArmA2OA.exe";
            Process.StartInfo.Arguments = "-mod=;";
            //Process.StartInfo.Arguments = scripterrors;
            string combined = System.IO.Path.Combine(arma_2_path, arma_2_exe);
            Process.StartInfo.FileName = combined;

            Process.Start();
        }

        private void LaunchA2_Vanilla_MenuItem_Click(object sender, RoutedEventArgs e)
        {

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Launch Arma 2 Vanilla Rquested");

            }
            Process Process = new Process();
            string arma_2_path = AusTacQuick2Launch.Properties.Settings.Default.Arma2Path;
            string arma_2_exe = "ArmA2OA.exe";

            Process.StartInfo.Arguments = "-mod=;";
            //Process.StartInfo.Arguments = scripterrors;
            string combined = System.IO.Path.Combine(arma_2_path, arma_2_exe);
            Process.StartInfo.FileName = combined;

            Process.Start();
        }

        private void LaunchA3_Vanilla_MenuItem_Click(object sender, RoutedEventArgs e)
        {

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Launch Arma 3 Vanilla Rquested");

            }
            Process Process = new Process();
            string arma_3_path = AusTacQuick2Launch.Properties.Settings.Default.Arma3Path;
            string arma_3_exe = "arma3.exe";

            Process.StartInfo.Arguments = "-mod=;";
            //Process.StartInfo.Arguments = scripterrors;
            string combined = System.IO.Path.Combine(arma_3_path, arma_3_exe);
            Process.StartInfo.FileName = combined;

            Process.Start();
        }


        void TaskbarExitItem_Click(object sender, EventArgs e)
        {
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Taskbar Shutdown Requested");

            }
            Application.Current.Shutdown();
        }


        void TaskbarOpenItem_Click(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Window Maximized");

            }
        }


        private void RestartActionRequest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }


    }
}
