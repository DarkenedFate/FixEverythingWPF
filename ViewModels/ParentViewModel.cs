using FixEverything.Commands;
using GalaSoft.MvvmLight.Messaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class ParentViewModel
    {
        public FixitsViewModel Fixits { get; set; }
        public MalwareRemovalViewModel MalwareRemoval { get; set; }
        public OfficeViewModel Office { get; set; }
        public PrintersViewModel Printers { get; set; }
        public AntivirusViewModel Antivirus { get; set; }
        public NiniteViewModel Ninite { get; set; }
        public OtherViewModel Other { get; set; }

        private const int CURRENT_VERSION = 22;

        public ParentViewModel()
        {
            WindowLoadedCommand = new WindowLoadedCommand(this);
        }

        public ICommand WindowLoadedCommand
        {
            get;
            private set;
        }

        public void OnWindowLoaded()
        {
            if (!File.Exists("FixEverything.exe.config"))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sb.AppendLine("<configuration>");
                sb.AppendLine("</configuration>");

                string loc = Assembly.GetEntryAssembly().Location;
                System.IO.File.WriteAllText(String.Concat(loc, ".config"), sb.ToString());
            }

            Messenger.Default.Send(new NotificationMessage("CheckSettings"));

            var appSettings = ConfigurationManager.AppSettings;

            // Check for updated version
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://callme.cloudapp.net/version.txt");
            StreamReader reader = new StreamReader(stream);
            string version = reader.ReadToEnd();
            int serverVersion = Convert.ToInt16(version);
            if (CURRENT_VERSION < serverVersion)
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("New version of Fix Everything is available. Download now?", "New version available", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Messenger.Default.Send(new NotificationMessage("http://callme.cloudapp.net/download.php?file=updater.exe\tupdate"));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!", "Error checking for updates, check your internet connection\nException type: " + ex.GetType().ToString(), MessageBoxButton.OK);
                }
            }
        }
    }
}
