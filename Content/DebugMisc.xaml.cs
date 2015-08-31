using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class DebugMisc : UserControl
    {
        public DebugMisc()
        {
            InitializeComponent();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to DebugMisc XAML");

            }


            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");

            if (regKey != null)
            {
                //Get Steam Install location
                RegistryKey steamKey = Registry.LocalMachine.OpenSubKey("Software\\Valve\\Steam") ?? Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam");
                steamdir.Text = "Steam Directory: " + steamKey.GetValue("InstallPath").ToString();

                //Get Arma 2 Steam Install location
                ArmA2OAValue.Text = "Arma 2 Directory: " + Arma2OAFolder();

                //Get Arma 3 Steam Install location
                ArmA3Value.Text = "Arma 3 Directory: " + Arma3Folder();
            }


        }

        public static string SteamFolder()
        {
            RegistryKey steamKey = Registry.LocalMachine.OpenSubKey("Software\\Valve\\Steam") ?? Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam");
            return steamKey.GetValue("InstallPath").ToString();
        }


        public static List<string> LibraryFolders()
        {
            List<string> folders = new List<string>();

            string steamFolder = SteamFolder();
            folders.Add(steamFolder);

            string configFile = steamFolder + "\\config\\config.vdf";

            Regex regex = new Regex("BaseInstallFolder[^\"]*\"\\s*\"([^\"]*)\"");
            using (StreamReader reader = new StreamReader(configFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        folders.Add(Regex.Unescape(match.Groups[1].Value));
                    }
                }
            }

            return folders;
        }


        public static string Arma2OAFolder()
        {
            var appFolders = LibraryFolders().Select(x => x + "\\SteamApps\\common");
            foreach (var folder in appFolders)
            {
                try
                {
                    var matches = Directory.GetDirectories(folder, "arma 2 operation arrowhead");
                    if (matches.Length >= 1)
                    {
                        return matches[0];
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    //continue;
                    //logging
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Arma 2 Folder Not Found via Registry Path");

                    }
                }

            }

            // Couldn't find folder, attempt another method
            return null;
            
        }


        public static string Arma3Folder()
        {
            var appFolders = LibraryFolders().Select(x => x + "\\SteamApps\\common");
            foreach (var folder in appFolders)
            {
                try
                {
                    var matches = Directory.GetDirectories(folder, "arma 3");
                    if (matches.Length >= 1)
                    {
                        return matches[0];
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    //continue;
                    //logging
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Arma 3 Folder Not Found via Registry Path");

                    }

                }

            }

            // Couldn't find folder, attempt another method
            return null;
            
        }

    }
}
