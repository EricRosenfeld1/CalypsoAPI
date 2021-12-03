using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Models
{
    /// <summary>
    /// Common measurement model
    /// </summary>
    public class Measurement
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Actual { get; set; }
        public string Nominal { get; set; }
        public string LowerTolerance { get; set; }
        public string UpperTolerance { get; set; }
        public string Deviation { get; set; }
        public string Exceed { get; set; }
        public string FeatureId { get; set; }
        public string GroupName { get; set; }        
    }
}
