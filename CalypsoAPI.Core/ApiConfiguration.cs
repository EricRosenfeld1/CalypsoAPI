using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core
{
    public class ApiConfiguration
    {
        public ApiConfiguration(string cmmObserverFilePath)
        {
            CMMObserverFolderPath = cmmObserverFilePath;
        }
        public string CMMObserverFolderPath { get; set; }
        public bool CopyChrFileAfterReading { get; set; }
        public string ChrDestinationFolderPath { get; set; }
    }
}
