using System;
using System.Collections.Generic;
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
    /// Interaction logic for AboutAusTac.xaml
    /// </summary>
    public partial class AboutAusTac : UserControl
    {
        public AboutAusTac()
        {
            InitializeComponent();

            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to About AusTac XAML");

            }

            Loaded += AboutAusTac_Loaded;
        }


        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupHeadline { get; set; }
        public string GroupSummary { get; set; }
        public string GroupAvatar { get; set; }
        public string GroupMemberCount { get; set; }
        public string GroupMembersInChat { get; set; }
        public string GroupMembersInGame { get; set; }
        public string GroupMembersOnline { get; set; }

        private void AboutAusTac_Loaded(object sender, RoutedEventArgs e)
        {
            //logging
            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Loaded Steam Landing XAML ");
            }
            panelhide.Visibility = Visibility.Visible;
            GridA.Visibility = Visibility.Hidden;
            showProgress();
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();


        }

        public void showProgress()
        {


            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(5)
            };

            timer.Tick += delegate(object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                panelhide.Visibility = Visibility.Hidden;
                panelLoader.Visibility = Visibility.Hidden;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://steamcommunity.com/groups/AustralianTacticalCombatLeague/memberslistxml/?xml=1");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        XDocument itemz = XDocument.Load("http://steamcommunity.com/groups/AustralianTacticalCombatLeague/memberslistxml/?xml=1");

                        foreach (var item in itemz.Descendants("groupDetails"))
                        {
                            string group_headline = item.Element("headline").Value;
                            SteamGroupName.Text = item.Element("headline").Value;



                            string group_summary = item.Element("summary").Value;
                            //SteamGroupSummary.DocumentText(group_summary);
                            GroupSummary = group_summary;
                            SteamGroupSummary.NavigateToString(item.Element("summary").Value);


                            string group_avatar = item.Element("avatarFull").Value;
                            GroupAvatar = group_avatar;
                            Uri uri = new Uri(item.Element("avatarFull").Value);
                            BitmapImage imgSource = new BitmapImage(uri);
                            SteamGroupImage.Source = imgSource;



                            string group_member_count = item.Element("memberCount").Value;
                            GroupMemberCount = group_member_count;
                            string group_member_in_chat = item.Element("membersInChat").Value;
                            GroupMembersInChat = group_member_in_chat;
                            string group_member_online = item.Element("membersInGame").Value;
                            GroupMembersOnline = group_member_online;
                            string group_in_game = item.Element("membersOnline").Value;
                            GroupMembersInGame = group_in_game;
                            SteamGroupMembers.Text = "Members - " + item.Element("memberCount").Value;
                            SteamGroupDetails.Text = "Players Online - " + item.Element("membersOnline").Value + " | " + "Players InGame - " + item.Element("membersInGame").Value;

                            panelLoader.Visibility = Visibility.Hidden;
                            GridA.Visibility = Visibility.Visible;

                        }
                    }
                    catch (Exception)
                    {
                        panelLoader.Visibility = Visibility.Hidden;
                        GridA.Visibility = Visibility.Hidden;

                        //logging
                        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();
                            sw.WriteLine(time + " | Error in Steam XML Request via AusTac About XAML");
                        }

                    }
                }

            };

            timer.Start();

        }


    }
}
