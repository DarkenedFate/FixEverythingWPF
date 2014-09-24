using FixEverything.Commands;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class OtherViewModel : AllViewModels
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
            Utils.copyResource(Utils.RESOURCE_DIR + "Reset-Ie-Default.ps1", psPath);

            string batPath = Path.GetTempPath() + @"fix_ie.bat";
            Utils.copyResource(Utils.RESOURCE_DIR + "fix_ie.bat", batPath);

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
    }
}
