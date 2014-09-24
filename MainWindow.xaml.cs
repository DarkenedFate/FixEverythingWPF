using FixEverything.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



namespace FixEverything
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CURRENT_VERSION = 18;

        public MainWindow()
        {
            InitializeComponent();
            checkProxy();
            DataContext = new AllViewModels()
            {
                Fixits = new FixitsViewModel(),
                MalwareRemoval = new MalwareRemovalViewModel(),
                Office = new OfficeViewModel(),
                Printers = new PrintersViewModel(),
                Antivirus = new AntivirusViewModel(),
                Ninite = new NiniteViewModel(),
                Other = new OtherViewModel()
            };

            // Set all buttons to have the same button handler
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridFixIts))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridMalware))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridOffice))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridPrinters))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridAntivirus))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridNinite))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }
            foreach (Button btn in Utils.FindVisualChildren<Button>(gridOther))
            {
                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(setButtonForegroundColor));
            }

            Messenger.Default.Register<NotificationMessage>(this, ProcessDialogMessage);
        }

        // Process incoming messages from ViewModels
        private void ProcessDialogMessage(NotificationMessage message)
        {
            switch (message.Notification)
            {
                case "Scans":
                    ScansPopup scansPopup = new ScansPopup();
                    scansPopup.Owner = this;
                    scansPopup.ShowDialog();
                    break;

                case "RefreshReset":
                    PasswordForm pwForm = new PasswordForm();
                    pwForm.Owner = this;
                    pwForm.ShowDialog();
                    break;

                default:
                    char[] delimiterChars = { '\t' };
                    string[] details = message.Notification.Split(delimiterChars);

                    DownloadForm dlForm = new DownloadForm(details[0], details[1]);
                    dlForm.Owner = this;
                    dlForm.ShowDialog();
                    break;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("FixEverything.exe.config"))
            {
                Utils.createConfig();
            }

            checkSettings();

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
                        DownloadForm dlForm = new DownloadForm("http://callme.cloudapp.net/download.php?file=updater.exe", "update");
                        dlForm.Owner = this;
                        dlForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!", "Error checking for updates, check your internet connection\nException type: " + ex.GetType().ToString(), MessageBoxButton.OK);
                }
            }
        }

        private void checkProxy()
        {
            if (Utils.isProxyEnabled())
            {
                MessageBoxResult result = MessageBox.Show("Proxy server is enabled on this computer. Would you like to turn it off?",
                    "Proxy Server Enabled", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Utils.disableProxy();
                }
            }
        }

        private void checkSettings()
        {
            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count != 0)
            {
                foreach (Button btn in Utils.FindVisualChildren<Button>(gridFixIts))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }

                foreach (Button btn in Utils.FindVisualChildren<Button>(gridMalware))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }
                foreach (Button btn in Utils.FindVisualChildren<Button>(gridOffice))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }
                foreach (Button btn in Utils.FindVisualChildren<Button>(gridPrinters))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }
                foreach (Button btn in Utils.FindVisualChildren<Button>(gridAntivirus))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }
                foreach (Button btn in Utils.FindVisualChildren<Button>(gridNinite))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }
                foreach (Button btn in Utils.FindVisualChildren<Button>(gridOther))
                {
                    if (appSettings[btn.Name.ToString()] == "true")
                    {
                        btn.Foreground = Brushes.LightGreen;
                        btn.BorderBrush = Brushes.LightGreen;
                    }
                }
            }
        }

        private void setButtonForegroundColor(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                clickedButton.Foreground = Brushes.White;
                clickedButton.BorderBrush = Brushes.White;
                Utils.AddUpdateAppSettings(clickedButton.Name.ToString(), "false");
            }
            else
            {
                clickedButton.Foreground = Brushes.LightGreen;
                clickedButton.BorderBrush = Brushes.LightGreen;
                Utils.AddUpdateAppSettings(clickedButton.Name.ToString(), "true");
            }
        }

        #region Konami Code Listener

        private KonamiSequence sequence = new KonamiSequence();

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (sequence.IsCompletedBy(e.Key))
            {
                btnRefreshReset.Content = "The Carlos Button";
            }
        }

        #endregion
    }
}
