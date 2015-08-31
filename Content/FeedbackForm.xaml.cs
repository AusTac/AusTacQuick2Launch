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

namespace AusTacQuick2Launch.Content
{
    /// <summary>
    /// Interaction logic for FeedbackForm.xaml
    /// </summary>
    public partial class FeedbackForm : UserControl
    {
        public FeedbackForm()
        {
            InitializeComponent();
            Random rnd = new Random();
            int rand = rnd.Next(999, 9999);
            int rand2 = rnd.Next(999, 9999);
            id.Text = (rand + rand2).ToString();
            panelhide.Visibility = Visibility.Hidden;
            submitted.Visibility = Visibility.Hidden;
            Form.Visibility = Visibility.Visible;

            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " | Navigated to Feedback XAML");
            }
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {

            panelhide.Visibility = Visibility.Visible;
            Form.Visibility = Visibility.Hidden;
            submitted.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Hidden;
            Reset.Visibility = Visibility.Hidden;
            MessageBorder.Visibility = Visibility.Hidden;

            if (string.IsNullOrEmpty(id.Text))
            {
                //ID Empty
                await Task.Run(() => Thread.Sleep(1000));
                panelhide.Visibility = Visibility.Hidden;
                Form.Visibility = Visibility.Visible;
                submitted.Visibility = Visibility.Hidden;
                Send.Visibility = Visibility.Visible;
                Reset.Visibility = Visibility.Visible;
                MessageBorder.Visibility = Visibility.Visible;
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                {
                    string time = DateTime.Now.ToString();
                    sw.WriteLine(time + " | Feedback Path: ID Field is Empty");
                }

            }
            else
            {
                if (string.IsNullOrEmpty(name.Text))
                {
                    //Name Empty
                    await Task.Run(() => Thread.Sleep(1000));
                    panelhide.Visibility = Visibility.Hidden;
                    Form.Visibility = Visibility.Visible;
                    submitted.Visibility = Visibility.Hidden;
                    Send.Visibility = Visibility.Visible;
                    Reset.Visibility = Visibility.Visible;
                    MessageBorder.Visibility = Visibility.Visible;
                    string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                    using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                    {
                        string time = DateTime.Now.ToString();
                        sw.WriteLine(time + " | Feedback Path: Name Field is Empty");
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(description.Text))
                    {
                        //Description Empty
                        await Task.Run(() => Thread.Sleep(1000));
                        panelhide.Visibility = Visibility.Hidden;
                        Form.Visibility = Visibility.Visible;
                        submitted.Visibility = Visibility.Hidden;
                        Send.Visibility = Visibility.Visible;
                        Reset.Visibility = Visibility.Visible;
                        MessageBorder.Visibility = Visibility.Visible;
                        string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                        using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                        {
                            string time = DateTime.Now.ToString();
                            sw.WriteLine(time + " | Feedback Path: Feedback Field is Empty");
                        }

                    }
                    else
                    {

                        //All fields OK!

                        string set_user = "user_id=" + id.Text;
                        string set_name = "&name="+ name.Text;
                        string set_email = "&email=" + email.Text;
                        string set_feedback = "&feedback=" + description.Text;
                        string set_time = "&user_time=" + DateTime.Now.ToString();
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://sync.austac.net.au/wpf/uploads/quick2launch/feedback.php?" + set_user + set_name + set_email + set_time + set_feedback);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {

                            await Task.Run(() => Thread.Sleep(3000));
                            string feedbackpath = "sync.austac.net.au/wpf/uploads/feedback_" + id.Text + "/" + id.Text + ".feedback";

                            System.Diagnostics.Process.Start(@"http://" + feedbackpath);
                            string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                            string AusTacQuick2Launch_Log = System.IO.Path.Combine(appfolder, "AusTacQuick2Launch/Log");
                            using (StreamWriter sw = File.AppendText(AusTacQuick2Launch_Log + "/log.txt"))
                            {
                                string time = DateTime.Now.ToString();
                                sw.WriteLine(time + " | Feedback Path: " + feedbackpath);
                            }

                        }

                        await Task.Run(() => Thread.Sleep(5000));

                        panelhide.Visibility = Visibility.Hidden;
                        Form.Visibility = Visibility.Hidden;

                        Send.Visibility = Visibility.Hidden;
                        Reset.Visibility = Visibility.Hidden;

                        submitted.Visibility = Visibility.Visible;

                        await Task.Run(() => Thread.Sleep(3000));


                        Random rnd = new Random();
                        int rand = rnd.Next(999, 9999);
                        int rand2 = rnd.Next(999, 9999);
                        id.Text = (rand + rand2).ToString();

                        name.Text = "";
                        email.Text = "";
                        description.Text = "";

                        panelhide.Visibility = Visibility.Hidden;
                        Form.Visibility = Visibility.Visible;
                        submitted.Visibility = Visibility.Hidden;
                        Send.Visibility = Visibility.Visible;
                        Reset.Visibility = Visibility.Visible;



                    }
                }
            }

        }

        private async  void Reset_Click(object sender, RoutedEventArgs e)
        {


            panelhide.Visibility = Visibility.Visible;
            Form.Visibility = Visibility.Hidden;
            submitted.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Hidden;
            Reset.Visibility = Visibility.Hidden;

            await Task.Run(() => Thread.Sleep(1000));


            Random rnd = new Random();
            int rand = rnd.Next(999, 9999);
            int rand2 = rnd.Next(999, 9999);
            id.Text = (rand + rand2).ToString();

            name.Text = "";
            email.Text = "";
            description.Text = "";

            panelhide.Visibility = Visibility.Hidden;
            Form.Visibility = Visibility.Visible;
            submitted.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Visible;
            Reset.Visibility = Visibility.Visible;
        }
    }
}
