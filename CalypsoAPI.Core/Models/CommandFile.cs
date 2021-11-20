using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core.Models
{
    public class CommandFile
    {
        public string state { get; set; }
        public string toleranceState { get; set; }
        public string hdrPath { get; set; }
        public string chrPath { get; set; }
        public string fetPath { get; set; }
        public string planPath { get; set; }
    }
}
