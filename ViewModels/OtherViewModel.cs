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

            Utils.UpdateDbClickCount("Reset IE");
        }

        public void ResetChrome()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "fix_chrome.bat", "fix_chrome.bat");
            Utils.UpdateDbClickCount("Reset Chrome");
        }

        public void ResetFirefox()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "fix_firefox.bat", "fix_firefox.bat");
            Utils.UpdateDbClickCount("Reset Firefox");
        }

        public void RefreshReset()
        {
            Messenger.Default.Send(new NotificationMessage("RefreshReset"));
            Utils.UpdateDbClickCount("Refresh / Reset");
        }

        public void Autoruns()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "autoruns.exe", "autoruns.exe");
            Utils.UpdateDbClickCount("Autoruns");
        }

        public void AmdCompatChecker()
        {
            Utils.downloadProgram("http://www2.ati.com/drivers/auto/amddriverdownloader.exe", "AMD Compatibility Checker");
            Utils.UpdateDbClickCount("AMD Compatibility Checker");
        }

        public void BlueScreenView()
        {
            Utils.downloadProgram("http://www.nirsoft.net/utils/bluescreenview.zip", "BlueScreenView");
            Utils.UpdateDbClickCount("BlueScreenView");
        }

        public void UninstallScanners()
        {
            List<String> scannerUninstallPaths = new List<String>();
            int timesNotFound = 0;

            scannerUninstallPaths.Add(@"C:\Program Files (x86)\Malwarebytes Anti-Malware\unins000.exe");
            scannerUninstallPaths.Add(@"C:\Program Files (x86)\Malwarebytes' Anti-Malware\unins000.exe");
            scannerUninstallPaths.Add(@"C:\Program Files\SUPERAntiSpyware\Uninstall.exe");
            scannerUninstallPaths.Add(@"C:\Program Files (x86)\IObit\IObit Uninstaller\UninstallDisplay.exe");
            scannerUninstallPaths.Add(@"C:\Program Files\CCleaner\uninst.exe");
            scannerUninstallPaths.Add(@"C:\Program Files (x86)\CCleaner\uninst.exe");
            scannerUninstallPaths.Add(@"C:\Program Files\VS Revo Group\Revo Uninstaller Pro\unins000.exe");

            foreach (String path in scannerUninstallPaths)
            {
                if (File.Exists(path))
                {
                    if (path.Contains("IObit"))
                    {
                        Process.Start(path, "uninstall_start").WaitForExit();
                    }
                    else
                    {
                        Process.Start(path).WaitForExit();
                    }
                }
                else
                {
                    timesNotFound++;
                }
            }

            if (timesNotFound == 7)
            {
                MessageBox.Show("No malware scanners or cleanup tools were found.", "There's nothing here!", MessageBoxButton.OK);
            }

            Utils.UpdateDbClickCount("Uninstall Scanners");
        }
    }
}
