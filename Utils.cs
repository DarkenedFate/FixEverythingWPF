using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using WindowsInput;

namespace FixEverything
{
    class Utils
    {
        internal const string RESOURCE_DIR = "FixEverything.Resources.";
        private static MySqlConnection conn = new MySqlConnection("Server=191.238.32.68;Database=my_wiki;Uid=JT;Pwd=Mohnjiles1!;");

        internal static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        internal static void CopyResource(string resourceName, string file)
        {
            using (Stream resource = Assembly.GetExecutingAssembly()
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

        public static List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
            return logicalCollection;
        }
        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        internal static void downloadProgram(String url, String programName)
        {
            Messenger.Default.Send(new NotificationMessage(url + "\t" + programName));
        }

        internal static void runProgramFromResource(String resourceLocation, String fileName)
        {
            string path = Path.GetTempPath() + fileName;
            CopyResource(resourceLocation, path);
            Process.Start(path);
        }

        internal static bool isProxyEnabled()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
            int proxyStatus = (int)key.GetValue("ProxyEnable");

            return proxyStatus == 1 ? true : false;
        }

        internal static void disableProxy()
        {
            Process p = new Process();
            p.StartInfo.FileName = @"C:\Windows\system32\RunDll32.exe";
            p.StartInfo.Arguments = @"shell32.dll,Control_RunDLL inetcpl.cpl,Internet,4";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();

            Thread.Sleep(500);
            InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_L);
            Thread.Sleep(100);
            InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_X);
            Thread.Sleep(100);
            InputSimulator.SimulateKeyDown(VirtualKeyCode.RETURN);
            Thread.Sleep(100);
            InputSimulator.SimulateKeyDown(VirtualKeyCode.ESCAPE);
        }

        internal static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            String fileName = e.Argument as String;

            MySqlDataReader reader;
            MySqlCommand cmd = new MySqlCommand();

            String countQuery = String.Format("SELECT COUNT(*) AS countfile FROM download WHERE filename='{0}'", fileName);
            String insertOrUpdateQuery = "";

            cmd.CommandText = countQuery;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            conn.OpenAsync();

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        insertOrUpdateQuery = String.Format("UPDATE download SET stats = stats + 1 WHERE filename='{0}'", fileName);
                    }
                    else
                    {
                        insertOrUpdateQuery = String.Format("INSERT INTO download (filename, stats) VALUES('{0}', 1)", fileName);
                    }
                }
            }

            reader.Close();

            cmd.CommandText = insertOrUpdateQuery;
            cmd.ExecuteNonQueryAsync();

        }

        internal static void UpdateDbClickCount(String fileName)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync(fileName);
            }
            else
            {
                BackgroundWorker bw2 = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                if (!bw2.IsBusy)
                {
                    bw2.RunWorkerAsync(fileName);
                }
            }
        }
    }
}
