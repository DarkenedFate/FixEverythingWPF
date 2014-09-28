using FixEverything.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.ViewModels
{
    internal class AntivirusViewModel : ParentViewModel
    {
        public AntivirusViewModel()
        {
            AntivirusCommand = new AntivirusCommand(this);
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

        public ICommand AntivirusCommand
        {
            get;
            private set;
        }

        public void TrendMicroDownloader()
        {
            Utils.downloadProgram("http://files.trendmicro.com/products/Titanium/TTi_7.0_EL_64bit.exe", "Trend Micro");
        }

        public void RemoveTrendMicro()
        {
            Utils.downloadProgram("http://solutionfile.trendmicro.com/solutionfile/Titanium-2014/" +
                    "Ti_70_win_global_en_Uninstall_hfb0001.exe", "Trend Micro Removal Tool");
        }

        public void McAfeeDownloader()
        {
            Utils.downloadProgram("http://191.238.32.68/download.php?file=MCAfeesetup.exe", "McAfee");
        }

        public void RemoveMcAfee()
        {
            Utils.downloadProgram("http://download.mcafee.com/products/licensed/cust_support_patches/MCPR.exe", "McAfee Removal Tool");
        }

        public void RemoveNorton()
        {
            Utils.downloadProgram("ftp://ftp.symantec.com/public/english_us_canada/removal_tools/Norton_Removal_Tool.exe", "Norton Removal Tool");
        }
    }
}
