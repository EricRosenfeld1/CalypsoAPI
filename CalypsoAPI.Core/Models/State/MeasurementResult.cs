using System.Collections.Generic;
using System.Data;

namespace CalypsoAPI.Core.Models.State
{
    public class MeasurementResult
    {
        /// <summary>
        /// List containing common information about the measurement 
        /// </summary>
        public List<Measurement> Measurements { get; set; } = new List<Measurement>();

        /// <summary>
        /// Raw chr file contents
        /// </summary>
        public string ChrFile { get; set; } = string.Empty;

        /// <summary>
        /// DataTable containing chr file data
        /// </summary>
        public DataTable ChrTable { get; set; } = new DataTable();
    }
}
