using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FixEverything
{
    class DownloadProgressMessage
    {
        public DownloadProgressMessage(String message)
        {
            Content = message;
        }
        public DownloadProgressChangedEventArgs Args { get; set; }
        public Double Percentage { get; set; }
        public String Content { get; private set; }
    }
}
