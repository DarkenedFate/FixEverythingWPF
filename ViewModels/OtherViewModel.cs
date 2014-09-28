using FixEverything.Commands;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class OtherViewModel : ParentViewModel
    {
        public OtherViewModel()
        {
            OtherCommand = new OtherCommand(this);
        }
        public bool CanExecute
        {
            get
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    return false;
                }
                return true;
            }
        }

        public ICommand OtherCommand
        {
            get;
            private set;
        }

        public void ResetIe()
        {
            string psPath = Path.GetTempPath() + @"reset-ie.ps1";
            Utils.CopyResource(Utils.RESOURCE_DIR + "Reset-Ie-Default.ps1", psPath);

            string batPath = Path.GetTempPath() + @"fix_ie.bat";
            Utils.CopyResource(Utils.RESOURCE_DIR + "fix_ie.bat", batPath);

            Process.Start(batPath);
        }

        public void ResetChrome()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "fix_chrome.bat", "fix_chrome.bat");
        }

        public void ResetFirefox()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "fix_firefox.bat", "fix_firefox.bat");
        }

        public void RefreshReset()
        {
            Messenger.Default.Send(new NotificationMessage("RefreshReset"));
        }

        public void Autoruns()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "autoruns.exe", "autoruns.exe");
        }

        public void AmdCompatChecker()
        {
            Utils.downloadProgram("http://www2.ati.com/drivers/auto/amddriverdownloader.exe", "AMD Compatibility Checker");
        }

        public void BlueScreenView()
        {
            Utils.downloadProgram("http://www.nirsoft.net/utils/bluescreenview.zip", "BlueScreenView");
        }

        public void UninstallScanners()
        {
            var appSettings = ConfigurationManager.AppSettings;

            // Should actually be called something like allSettingKeysThatAreTrue
            var installedPrograms = new List<String>();
            var wantedPrograms = new List<String>();

            wantedPrograms.Add("btnMalwarebytes");
            wantedPrograms.Add("btnMalwarebytesOld");
            wantedPrograms.Add("btnIobit");
            wantedPrograms.Add("btnSuper");
            wantedPrograms.Add("btnCcleaner");
            wantedPrograms.Add("btnRevo");

            if (appSettings.Count != 0)
            {
                foreach (var key in appSettings.AllKeys)
                {
                    if (appSettings[key] == "true")
                    {
                        installedPrograms.Add(key);
                    }
                }

                // To make sure we're only counting malware scanners & cleanup tools
                var unwantedPrograms = installedPrograms.Except(wantedPrograms).ToList();
                installedPrograms = installedPrograms.Except(unwantedPrograms).ToList();

                if (installedPrograms.Count < 1)
                {   
                    MessageBox.Show("No malware scanners or cleanup tools were found.", "There's nothing here!", MessageBoxButton.OK);
                    return;
                }

                foreach (var key in installedPrograms)
                {
                    switch (key)
                    {
                        case "btnMalwarebytes":

                            String mbamPath = @"C:\Program Files (x86)\Malwarebytes Anti-Malware\unins000.exe";
                            if (!File.Exists(mbamPath))
                            {
                                Utils.AddUpdateAppSettings(key, "false");
                                continue;
                            }

                            Process.Start(mbamPath).WaitForExit();
                            Utils.AddUpdateAppSettings(key, "false");
                            continue;

                        case "btnMalwarebytesOld":

                            String mbamOldPath = @"C:\Program Files (x86)\Malwarebytes' Anti-Malware\unins000.exe";
                            if (!File.Exists(mbamOldPath))
                            {
                                Utils.AddUpdateAppSettings(key, "false");
                                continue;
                            }

                            Process.Start(mbamOldPath).WaitForExit();
                            Utils.AddUpdateAppSettings(key, "false");
                            continue;

                        case "btnSuper":

                            String superPath = @"C:\Program Files\SUPERAntiSpyware\Uninstall.exe";
                            if (!File.Exists(superPath))
                            {
                                Utils.AddUpdateAppSettings(key, "false");
                                continue;
                            }

                            Process.Start(superPath).WaitForExit();
                            Utils.AddUpdateAppSettings(key, "false");
                            continue;

                        case "btnIobit":

                            String iobitPath = @"C:\Program Files (x86)\IObit\IObit Uninstaller\UninstallDisplay.exe";
                            if (!File.Exists(iobitPath))
                            {
                                Utils.AddUpdateAppSettings(key, "false");
                                continue;
                            }

                            Process.Start(iobitPath, "uninstall_start").WaitForExit();
                            Utils.AddUpdateAppSettings(key, "false");
                            continue;

                        case "btnCcleaner":

                            String ccleanerPath = @"C:\Program Files\CCleaner\uninst.exe";
                            if (!File.Exists(ccleanerPath))
                            {
                                ccleanerPath = @"C:\Program Files (x86)\CCleaner\uninst.exe";
                                if (!File.Exists(ccleanerPath)) continue;
                            }

                            Process.Start(ccleanerPath).WaitForExit();
                            Utils.AddUpdateAppSettings(key, "false");
                            continue;

                        case "btnRevo":
                            String revoPath = @"C:\Program Files\VS Revo Group\Revo Uninstaller Pro\unins000.exe";
                            if (!File.Exists(revoPath))
                            {
                                Utils.AddUpdateAppSettings(key, "false");
                                continue;
                            }

                            Process.Start(revoPath).WaitForExit();
                            Utils.AddUpdateAppSettings(key, "false");
                            continue;

                        default: break;
                    }
                }
            }
        }
    }
}
