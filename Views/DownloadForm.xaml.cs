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
using FixEverything.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace FixEverything
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DownloadForm : Window
    {

        private string programName;

        public DownloadForm(string url, string programName)
        {
            InitializeComponent();
            DataContext = new DownloadViewModel(url, programName);
            this.programName = programName;
            lblTitle.Content = "Downloading " + programName;

            Messenger.Default.Register<DownloadProgressMessage>(this, ProcessDialogMessage);
        }

        private void ProcessDialogMessage(DownloadProgressMessage message)
        {
            if (message.Args != null)
            {
                lblProgress.Content = "Downloaded " + message.Args.BytesReceived / 1000 + " kb of " + message.Args.TotalBytesToReceive / 1000 + " kb";
                lblTitle.Content = "Downloading " + programName + "... " + Math.Truncate(message.Percentage).ToString() + "%";
                downloadProgress.Value = int.Parse(Math.Truncate(message.Percentage).ToString());
            }

            if (message.Content != "" && message.Content != "Close")
            {
                lblProgress.Content = message.Content;
            }

            if (message.Content == "Close")
            {
                this.Close();
            }

        }
    }
}
