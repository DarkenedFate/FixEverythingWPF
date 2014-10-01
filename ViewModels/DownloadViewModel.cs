using FixEverything.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DownloadViewModel : ViewModelBase
    {

        private string url;
        private string programName;
        private WebClient client = new WebClient();

        /// <summary>
        /// Initializes a new instance of the DownloadViewModel class.
        /// </summary>
        public DownloadViewModel(string url, string programName)
        {
            DownloadCommand = new DownloadCommand(this);
            CancelDownloadCommand = new CancelDownloadCommand(this);
            this.url = url;
            this.programName = programName;
        }

        public ICommand DownloadCommand
        {
            get;
            private set;
        }

        public ICommand CancelDownloadCommand
        {
            get;
            private set;
        }

        public void CancelDownload()
        {
            client.CancelAsync();
            Messenger.Default.Send(new DownloadProgressMessage("Close"));
        }

        public void Download()
        {
            startDownload(url);
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

                case "BlueScreenView":
                    client.DownloadFileAsync(new Uri(url), Path.GetTempPath() + programName + ".zip");
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
            Messenger.Default.Send(new DownloadProgressMessage("")
            {
                Args = e,
                Percentage = percentage
            });
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Messenger.Default.Send(new DownloadProgressMessage("Download cancelled"));
            }
            else
            {
                Messenger.Default.Send(new DownloadProgressMessage("Download completed"));
                try
                {
                    switch (programName)
                    {
                        case "Malwarebytes":
                            Messenger.Default.Send(new DownloadProgressMessage("Installing..."));
                            Process.Start(Path.GetTempPath() + programName + ".exe", "/silent")
                                .WaitForExit();
                            Process.Start("C:\\Program Files (x86)\\Malwarebytes Anti-Malware\\mbam.exe");

                            break;

                        case "Malwarebytes 1.75":
                            Messenger.Default.Send(new DownloadProgressMessage("Installing..."));
                            Process.Start(Path.GetTempPath() + programName + ".exe", "/silent")
                                .WaitForExit();

                            Messenger.Default.Send(new DownloadProgressMessage("Updating..."));
                            Process.Start("C:\\Program Files (x86)\\Malwarebytes' Anti-Malware\\mbam.exe", "/update")
                                .WaitForExit();

                            Messenger.Default.Send(new DownloadProgressMessage("Starting quick scan..."));
                            Process.Start("C:\\Program Files (x86)\\Malwarebytes' Anti-Malware\\mbam.exe", "/quickscan");
                            break;

                        case "Sentinel Driver Removal":
                            // haspdinst (Sentinel removal) must be run with a command
                            Process.Start(Path.GetTempPath() + programName + ".exe", "-purge");
                            break;

                        case "UVK Portable":
                            string path = Path.GetTempPath() + @"tuneup.uvksr";
                            Utils.CopyResource("FixEverything.Resources.malware-tuneup.uvksr", path);
                            Utils.CopyResource("FixEverything.Resources.UVKSettings.ini", Path.GetTempPath() + @"UVKSettings.ini");
                            Utils.CopyResource("FixEverything.Resources.key.uvkey", Path.GetTempPath() + @"key.uvkey");
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
                            Messenger.Default.Send(new DownloadProgressMessage("Installing..."));
                            Process.Start(Path.GetTempPath() + programName + ".exe", "/S")
                                .WaitForExit();

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

                        case "BlueScreenView":
                            Process.Start(Path.GetTempPath() + programName + ".zip");
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

            Messenger.Default.Send(new DownloadProgressMessage("Close"));
        }
    }
}