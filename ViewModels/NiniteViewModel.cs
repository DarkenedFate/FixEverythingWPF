using FixEverything.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class NiniteViewModel : ParentViewModel
    {
        public NiniteViewModel()
        {
            NiniteCommand = new NiniteCommand(this);
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

        public ICommand NiniteCommand
        {
            get;
            private set;
        }

        public void Avast()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Avast Installer.exe", "avast.exe");
        }
        public void Chrome()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Chrome Installer.exe", "chrome.exe");
        }

        public void Firefox()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Firefox Installer.exe", "firefox.exe");
        }

        public void ITunes()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite iTunes Installer.exe", "itunes.exe");
        }

        public void Java()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Java Installer.exe", "java.exe");
        }

        public void LibreOffice()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite LibreOffice Installer.exe", "libreoffice.exe");
        }

        public void OpenOffice()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite OpenOffice Installer.exe", "openoffice.exe");
        }

        public void Vlc()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite VLC Installer.exe", "vlc.exe");
        }

        public void Thunderbird()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Thunderbird Installer.exe", "thunderbird.exe");
        }
    }
}
