using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Threading;

namespace FixEverything
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DownloadForm : Window
    {
        private string url;
        private string programName;
        private WebClient client = new WebClient();


        public DownloadForm(string url, string programName)
        {
            InitializeComponent();
            this.url = url;
            this.programName = programName;
            lblTitle.Content = "Downloading " + programName;

        }

        private void startDownload(string url)
        {
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            switch (programName)
            {
                case "Office 2013 Removal Tool":
                    client.DownloadFileAsync(new Uri(url), Path.GetTempPath() + programName + ".diagcab");
                    break;

                case "Office 2010 Removal Tool":
                    client.DownloadFileAsync(new Uri(url), Path.GetTempPath() + programName + ".msi");
                    break;

                case "DVD Drive Fix":
                    client.DownloadFileAsync(new Uri(url), Path.GetTempPath() + programName + ".diagcab");
                    break;
                case "Office 2013":
                    client.DownloadFileAsync(new Uri(url), Environment.GetEnvironmentVariable("USERPROFILE") + @"\Desktop\HomeStudentRetail.img");
                    break;
                default:
                    client.DownloadFileAsync(new Uri(url), Path.GetTempPath() + programName + ".exe");
                    break;
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            lblProgress.Content = "Downloaded " + e.BytesReceived / 1000 + " kb of " + e.TotalBytesToReceive / 1000 + " kb";
            lblTitle.Content = "Downloading " + programName + "... " + Math.Truncate(percentage).ToString() + "%";
            downloadProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblProgress.Content = "Download cancelled";
            }
            else
            {

                lblProgress.Content = "Download completed";
                try
                {
                    switch (programName)
                    {
                        case "Malwarebytes":
                            Process mbam;
                            mbam = Process.Start(Path.GetTempPath() + programName + ".exe", "/silent");
                            while (!mbam.HasExited)
                            {
                                lblProgress.Content = "Installing...";
                            }
                            Process.Start("C:\\Program Files (x86)\\Malwarebytes Anti-Malware\\mbam.exe");
                            break;

                        case "Malwarebytes 1.75":
                            Process p;
                            p = Process.Start(Path.GetTempPath() + programName + ".exe", "/silent");
                            while (!p.HasExited)
                            {
                                lblProgress.Content = "Installing...";
                            }
                            p = Process.Start("C:\\Program Files (x86)\\Malwarebytes' Anti-Malware\\mbam.exe", "/update");
                            while (!p.HasExited)
                            {
                                lblProgress.Content = "Updating...";
                            }
                            lblProgress.Content = "Starting quick scan...";
                            p = Process.Start("C:\\Program Files (x86)\\Malwarebytes' Anti-Malware\\mbam.exe", "/quickscan");
                            break;

                        case "Sentinel Driver Removal":
                            // haspdinst (Sentinel removal) must be run with a command
                            Process.Start(Path.GetTempPath() + programName + ".exe", "-purge");
                            break;

                        case "UVK Portable":
                            string path = Path.GetTempPath() + @"tuneup.uvksr";
                            CopyResource("FixEverything.Resources.malware-tuneup.uvksr", path);
                            CopyResource("FixEverything.Resources.UVKSettings.ini", Path.GetTempPath() + @"UVKSettings.ini");
                            CopyResource("FixEverything.Resources.key.uvkey", Path.GetTempPath() + @"key.uvkey");
                            Process.Start(Path.GetTempPath() + programName + ".exe", "-ImportSr \"" + path + "\"");
                            break;

                        case "HitmanPro":
                            Process.Start(Path.GetTempPath() + programName + ".exe", "/scan");
                            break;

                        case "update":
                            Process.Start(Path.GetTempPath() + programName + ".exe");
                            Application.Current.Shutdown();
                            break;

                        case "Office 2013 Removal Tool":
                            Process.Start(Path.GetTempPath() + programName + ".diagcab");
                            break;

                        case "Office 2010 Removal Tool":
                            Process.Start(Path.GetTempPath() + programName + ".msi");
                            break;
                        case "DVD Drive Fix":
                            Process.Start(Path.GetTempPath() + programName + ".diagcab");
                            break;

                        case "Office 2013":
                            break;

                        case "CCleaner":
                            Process cCleaner = Process.Start(Path.GetTempPath() + programName + ".exe", "/S");

                            while (!cCleaner.HasExited)
                            {
                                lblProgress.Content = "Installing...";
                            }

                            string cCleaner64Dir = @"C:\Program Files\CCleaner\CCleaner.exe";
                            if (File.Exists(cCleaner64Dir))
                            {
                                Process.Start(cCleaner64Dir);
                            }
                            else
                            {
                                Process.Start(@"C:\Program Files (x86)\CCleaner\CCleaner.exe");
                            }
                            break;

                        default:
                            Process.Start(Path.GetTempPath() + programName + ".exe");
                            break;
                    }
                }
                catch (Win32Exception ex)
                {
                    if (!e.Cancelled)
                    {
                        MessageBox.Show("Unable to download file, check internet connection and/or proxy server", "Error", MessageBoxButton.OK);
                    }
                }
            }

            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startDownload(url);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            client.CancelAsync();
            lblProgress.Content = @"Cancelling...";
            this.Close();
        }

        private void CopyResource(string resourceName, string file)
        {
            using (Stream resource = GetType().Assembly
                                              .GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                {
                    throw new ArgumentException("No such resource", "resourceName");
                }
                using (Stream output = File.OpenWrite(file))
                {
                    resource.CopyTo(output);
                }
            }
        }
    }
}
