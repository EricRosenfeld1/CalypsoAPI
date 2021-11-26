namespace CalypsoAPI.Core
{
    public class CalypsoConfiguration
    {
        public CalypsoConfiguration(string cmmObserverFilePath = @"C:\Users\Public\Documents\Zeiss\CMMObserver")
        {
            CMMObserverFolderPath = cmmObserverFilePath;
        }

        /// <summary>
        /// Observer directory. Usally located under C:/Users/Public/Public Documents.
        /// </summary>
        public string CMMObserverFolderPath { get; set; }

        /// <summary>
        /// Copy results files (chr/hdr/fet) after processing. Can be used if files are needed for QC-Calc etc.
        /// </summary>
        public bool CopyChrFileAfterReading { get; set; }

        /// <summary>
        /// Destination directory if <see cref="CopyChrFileAfterReading"/> is <see langword="true"/>
        /// </summary>
        public string ChrDestinationFolderPath { get; set; }
    }
}
