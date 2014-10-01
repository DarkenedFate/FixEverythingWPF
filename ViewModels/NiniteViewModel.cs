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
            Utils.UpdateDbClickCount("Avast");
        }
        public void Chrome()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Chrome Installer.exe", "chrome.exe");
            Utils.UpdateDbClickCount("Chrome");
        }

        public void Firefox()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Firefox Installer.exe", "firefox.exe");
            Utils.UpdateDbClickCount("Firefox");
        }

        public void ITunes()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite iTunes Installer.exe", "itunes.exe");
            Utils.UpdateDbClickCount("iTunes");
        }

        public void Java()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Java Installer.exe", "java.exe");
            Utils.UpdateDbClickCount("Java");
        }

        public void LibreOffice()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite LibreOffice Installer.exe", "libreoffice.exe");
            Utils.UpdateDbClickCount("LibreOffice");
        }

        public void OpenOffice()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite OpenOffice Installer.exe", "openoffice.exe");
            Utils.UpdateDbClickCount("OpenOffice");
        }

        public void Vlc()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite VLC Installer.exe", "vlc.exe");
            Utils.UpdateDbClickCount("VLC");
        }

        public void Thunderbird()
        {
            Utils.runProgramFromResource(Utils.RESOURCE_DIR + "Ninite Thunderbird Installer.exe", "thunderbird.exe");
            Utils.UpdateDbClickCount("Thunderbird");
        }
    }
}
