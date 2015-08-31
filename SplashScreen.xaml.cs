using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace SplashDemo
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Folder = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch");
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            string AusTacQuick2Launch_Settings = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Settings");
            string AusTacQuick2Launch_PlaywithSix = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/PlaywithSix");
            string AusTacQuick2Launch_Recent = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent");
            string AusTacQuick2Launch_RecentArma2 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma2");
            string AusTacQuick2Launch_RecentArma3 = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Recent/Arma3");
            if (!Directory.Exists(AusTacQuick2Launch_Folder)) Directory.CreateDirectory(AusTacQuick2Launch_Folder);
            if (!Directory.Exists(AusTacQuick2Launch_Log)) Directory.CreateDirectory(AusTacQuick2Launch_Log);
            if (!Directory.Exists(AusTacQuick2Launch_Settings)) Directory.CreateDirectory(AusTacQuick2Launch_Settings);
            if (!Directory.Exists(AusTacQuick2Launch_RecentArma2)) Directory.CreateDirectory(AusTacQuick2Launch_RecentArma2);
            if (!Directory.Exists(AusTacQuick2Launch_RecentArma3)) Directory.CreateDirectory(AusTacQuick2Launch_RecentArma3);
            if (!Directory.Exists(AusTacQuick2Launch_PlaywithSix)) Directory.CreateDirectory(AusTacQuick2Launch_PlaywithSix);
        }

    }

}