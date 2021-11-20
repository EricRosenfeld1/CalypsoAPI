using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core.Models
{
    public class ObserverFile
    {
        public string planid { get; set; }
        public string partnbinc { get; set; }
        public string dmeid { get; set; }
        public string dmesn { get; set; }
        public string operid { get; set; }
        public string dmeswv { get; set; }
        public string firmWareRevision { get; set; }    
        public string Date { get; set; }
        public string controllertyp { get; set; }
        public string devicegroup { get; set; }
        public string observerStateCmm { get; set; }
        public string observerStateCal { get; set; }
    }
}
