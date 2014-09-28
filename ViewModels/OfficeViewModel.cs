using FixEverything.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class OfficeViewModel : ParentViewModel
    {
        public OfficeViewModel()
        {
            OfficeCommand = new OfficeCommand(this);
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

        public ICommand OfficeCommand
        {
            get;
            private set;
        }

        public void Office2013Direct() 
        {
            Utils.downloadProgram("http://officecdn.microsoft.com/pr/39168D7E-077B-48E7-872C-B232C3E72675/media/en-US/HomeStudentRetail.img", "Office 2013");
        }

        public void Office2010Direct()
        {
            Utils.downloadProgram("https://drcdn.blob.core.windows.net/office2010/X17-75058.exe", "Office 2010");
        }

        public void Office2013Website()
        {
            Process.Start("https://downloadoffice.getmicrosoftkey.com/");
        }

        public void Office2010Website()
        {
            Process.Start("https://www2.downloadoffice2010.microsoft.com/usa/registerkey.aspx?culture=EN-US&ref=backup&country_id=US");
        }

        public void RemoveOffice2013()
        {
            Utils.downloadProgram("http://go.microsoft.com/?linkid=9815935", "Office 2013 Removal Tool");
        }

        public void RemoveOffice2010()
        {
            Utils.downloadProgram("http://191.238.32.68/download.php?file=Office_2010_Remove.msi", "Office 2010 Removal Tool");
        }
    }
}
