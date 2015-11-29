using Microsoft.Win32;
using SplashDemo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AusTacQuick2Launch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


                /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Splasher.Splash = new SplashDemo.SplashScreen();
            Splasher.ShowSplash();

            //Get FirstRun Value from file if exists
            bool firstrunvalue = Convert.ToBoolean("false");
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            try
            {
                if (!System.IO.File.Exists(AusTacQuick2Launch_Settings + @"\runonce"))
                {
                    firstrunvalue = AusTacQuick2Launch.Properties.Settings.Default.firstrun;
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Error Checking RunOnce File");

                    }
                }
                else
                {
                    string getrunoncevaluebool = System.IO.File.ReadLines(AusTacQuick2Launch_Settings + @"\runonce").First();
                    firstrunvalue = Convert.ToBoolean(getrunoncevaluebool);
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
                    firstrunvalue = AusTacQuick2Launch.Properties.Settings.Default.firstrun;
                }
            }

            if (firstrunvalue == false)
            {

                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                AusTacQuick2Launch.Properties.Settings.Default.debug = false;
                AusTacQuick2Launch.Properties.Settings.Default.firstrun = true;
                AusTacQuick2Launch.Properties.Settings.Default.splash = false;
                AusTacQuick2Launch.Properties.Settings.Default.BaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
                AusTacQuick2Launch.Properties.Settings.Default.AppDir = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch");

                //Get Steam User Install location
                RegistryKey steamKey = Registry.LocalMachine.OpenSubKey("Software\\Valve\\Steam") ?? Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam");


                AusTacQuick2Launch.Properties.Settings.Default.Arma2Path = steamKey.GetValue("InstallPath").ToString() + @"\SteamApps\common\arma 2 operation arrowhead";
                AusTacQuick2Launch.Properties.Settings.Default.Arma3Path = steamKey.GetValue("InstallPath").ToString() + @"\SteamApps\common\Arma 3";
                AusTacQuick2Launch.Properties.Settings.Default.Arma2ModsPath = "";
                AusTacQuick2Launch.Properties.Settings.Default.Arma3ModsPath = "";

                

                AusTacQuick2Launch.Properties.Settings.Default.Save();

                string AusTacQuick2Launch_Folder = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch");
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                //string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
                string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent");
                string AusTacQuick2Launch_PlaywithSix = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/PlaywithSix");
                string AusTacQuick2Launch_RecentArma2 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma2");
                string AusTacQuick2Launch_RecentArma3 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma3");
                if (!Directory.Exists(AusTacQuick2Launch_Folder)) Directory.CreateDirectory(AusTacQuick2Launch_Folder);
                if (!Directory.Exists(AusTacQuick2Launch_Log)) Directory.CreateDirectory(AusTacQuick2Launch_Log);
                if (!Directory.Exists(AusTacQuick2Launch_Settings)) Directory.CreateDirectory(AusTacQuick2Launch_Settings);
                if (!Directory.Exists(AusTacQuick2Launch_RecentArma2)) Directory.CreateDirectory(AusTacQuick2Launch_RecentArma2);
                if (!Directory.Exists(AusTacQuick2Launch_RecentArma3)) Directory.CreateDirectory(AusTacQuick2Launch_RecentArma3);
                if (!Directory.Exists(AusTacQuick2Launch_PlaywithSix)) Directory.CreateDirectory(AusTacQuick2Launch_PlaywithSix);


                //Create Writes to Files
                //Save Arma 2 Path
                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma2Path.txt"))
                { sw.WriteLine(steamKey.GetValue("InstallPath").ToString() + @"\SteamApps\common\arma 2 operation arrowhead"); }
                //Save Arma 2 Mods Path
                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma2ModsPath.txt"))
                {sw.WriteLine("");}
                //Save Arma 3 Path
                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma3Path.txt"))
                { sw.WriteLine(steamKey.GetValue("InstallPath").ToString() + @"\SteamApps\common\Arma 3"); }
                //Save Arma 3 Mods Path
                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/Arma3ModsPath.txt"))
                { sw.WriteLine(""); }

                //Save First Launch
                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/runonce"))
                { sw.WriteLine("true"); }

                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/debug"))
                { sw.WriteLine("false"); }

                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/splash"))
                { sw.WriteLine("false"); }

                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/restart"))
                { sw.WriteLine("false"); }

            }else{

                //string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Folder = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch");
                //string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                //string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
                string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent");
                string AusTacQuick2Launch_PlaywithSix = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/PlaywithSix");
                string AusTacQuick2Launch_RecentArma2 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma2");
                string AusTacQuick2Launch_RecentArma3 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma3");
                if (!Directory.Exists(AusTacQuick2Launch_Folder)) Directory.CreateDirectory(AusTacQuick2Launch_Folder);
                if (!Directory.Exists(AusTacQuick2Launch_Log)) Directory.CreateDirectory(AusTacQuick2Launch_Log);
                if (!Directory.Exists(AusTacQuick2Launch_Settings)) Directory.CreateDirectory(AusTacQuick2Launch_Settings);
                if (!Directory.Exists(AusTacQuick2Launch_RecentArma2)) Directory.CreateDirectory(AusTacQuick2Launch_RecentArma2);
                if (!Directory.Exists(AusTacQuick2Launch_RecentArma3)) Directory.CreateDirectory(AusTacQuick2Launch_RecentArma3);
                if (!Directory.Exists(AusTacQuick2Launch_PlaywithSix)) Directory.CreateDirectory(AusTacQuick2Launch_PlaywithSix);

                using (StreamWriter sw = File.CreateText(AusTacQuick2Launch_Settings + "/restart"))
                { sw.WriteLine("false"); }
            
            }


            if (AusTacQuick2Launch.Properties.Settings.Default.splash == true)
            {
                //Splash Disabled
                //run for a 1 ms
                for (int i = 0; i < 1; i++)
                {
                    if (AusTacQuick2Launch.Properties.Settings.Default.debug == true)
                    {
                        MessageListener.Instance.ReceiveMessage(string.Format("[Debug] {0} of " + 1, i));
                        Thread.Sleep(1);
                    }
                    else
                    {
                        MessageListener.Instance.ReceiveMessage(string.Format("[Debug] {0} of " + 1, i));
                        Thread.Sleep(1);
                    }

                }

            }
            else
            {
                //Splash Enabled
                for (int i = 0; i < 1200; i++)
                {
                    if (AusTacQuick2Launch.Properties.Settings.Default.debug == true)
                    {
                        MessageListener.Instance.ReceiveMessage(string.Format("[Debug] {0} of " + 1200, i));
                        Thread.Sleep(1);
                    }
                    else
                    {
                        MessageListener.Instance.ReceiveMessage(string.Format("[Debug] {0} of " + 1200, i));
                        Thread.Sleep(1);
                    }

                }

            }

        }


    }
}
