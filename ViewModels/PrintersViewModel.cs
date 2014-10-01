using FixEverything.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class PrintersViewModel : ParentViewModel
    {
        public PrintersViewModel()
        {
            PrintersCommand = new PrintersCommand(this);
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

        public ICommand PrintersCommand
        {
            get;
            private set;
        }

        public void HpPrinterInstaller()
        {
            Utils.downloadProgram("http://ftp.hp.com/pub/softlib/software12/COL50403/mp-122330-1/hppiw.exe", "HP Printer Install Wizard");
            Utils.UpdateDbClickCount("HP Printer Install Wizard");
        }

        public void HpPrintScanDoc()
        {
            Utils.downloadProgram("http://ftp.hp.com/pub/softlib/software12/COL50849/mp-135113-1/HPPSdr.exe", "HP Print and Scan Doctor");
            Utils.UpdateDbClickCount("HP Print and Scan Doctor");
        }

        public void KodakPrinterInstaller()
        {
            Utils.downloadProgram("http://download.kodak.com/digital/software/inkjet/v7_8/Bits/webdownload/aio_install.exe", "Kodak Printer Installer");
            Utils.UpdateDbClickCount("Kodak Printer Installer");
        }
    }
}
