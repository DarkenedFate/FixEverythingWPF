using FixEverything.Commands;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FixEverything.ViewModels
{
    internal class FixitsViewModel : ParentViewModel
    {

        public FixitsViewModel()
        {
            FixitsCommand = new FixitsCommand(this);
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
        
        public ICommand FixitsCommand
        {
            get;
            private set;
        }

        public void WinUpdateFix()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "WinUpdateFix-06.11.14.bat", "winupdate.bat");
        }

        public void ScansPopup()
        {
            Messenger.Default.Send(new NotificationMessage("Scans"));
        }

        public void DvdFix()
        {
            Utils.downloadProgram("http://callme.cloudapp.net/download.php?file=CDDVDWin8.meta.diagcab", "DVD Drive Fix");
        }

        public void ClearPrintQueue()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Printer fix.bat", "printerfix.bat");
        }

        public void SoundFix()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "soundfix.bat", "soundfix.bat");
        }

        public void AdminFix()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "win81adminfix.bat", "win81adminfix.bat");
        }

        public void AppsFix()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "apps.diagcab", "apps.diagcab");
        }

        public void RemoveSentinelDrivers()
        {
            Utils.downloadProgram("http://callme.cloudapp.net/download.php?file=haspdinst.exe", "Sentinel Driver Removal");
        }

        public void FixPcSettings()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "fix_pcsettings.bat", "fix_pcsettings.bat");
        }

    }
}
