using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixEverything.ViewModels
{
    internal class AllViewModels
    {
        public FixitsViewModel Fixits { get; set; }
        public MalwareRemovalViewModel MalwareRemoval { get; set; }
        public OfficeViewModel Office { get; set; }
        public PrintersViewModel Printers { get; set; }
        public AntivirusViewModel Antivirus { get; set; }
        public NiniteViewModel Ninite { get; set; }
        public OtherViewModel Other { get; set; }
    }
}
