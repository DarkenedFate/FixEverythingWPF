using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FixEverything
{
    /// <summary>
    /// Interaction logic for ScansPopup.xaml
    /// </summary>
    public partial class ScansPopup : Window
    {
        public ScansPopup()
        {
            InitializeComponent();
        }

        private void btnSfc_Click(object sender, EventArgs e)
        {
            const string sfcCommands = "/c sfc /scannow&pause";
            Process.Start("cmd.exe", sfcCommands);
        }

        private void btnChkdsk_Click(object sender, EventArgs e)
        {
            const string chkdskCommands = "/c chkdsk&pause";
            Process.Start("cmd.exe", chkdskCommands);
        }

        private void btnDism_Click(object sender, EventArgs e)
        {
            
            string dismCommands = @"/c Dism /online /cleanup-image /scanhealth" +
                @"&Dism /online /cleanup-image /startcomponentcleanup" +
                @"&Dism /online /cleanup-image /restorehealth&pause";
            Process.Start(@"cmd.exe", dismCommands);
        }
    }
}
