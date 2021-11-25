using CalypsoAPI.Core;
using CalypsoAPI.Core.Events;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalypsoAPI.WinForms
{
    public partial class MainForm : Form
    {
        private ApiConfiguration apiConfiguration;
        private Calypso api;
        public MainForm()
        {
            InitializeComponent();
            try
            {
                apiConfiguration = new ApiConfiguration();

                api = new Calypso()
                    .Configure(apiConfiguration)
                    .Initialize();

                api.CmmStateChanged += Api_CmmStateChanged;
                api.MeasurementInfoChanged += Api_MeasurementInfoChanged;
                api.MeasurementPlanInfoChanged += Api_MeasurementPlanInfoChanged;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        private void Api_MeasurementPlanInfoChanged(object sender, MeasurementPlanInfoEventArgs e)
        {
            richTextBox2.Clear();
            richTextBox2.AppendText($"Basesystem Name: {e.MeasurementPlanInfo.BaseSystemName} \r\n");
            richTextBox2.AppendText($"Basesystem Type: {e.MeasurementPlanInfo.BaseSystemType} \r\n");
            richTextBox2.AppendText($"Clear Old Res. : {e.MeasurementPlanInfo.ClearOldResults} \r\n");
            richTextBox2.AppendText($"Compact Prot.  : {e.MeasurementPlanInfo.CompactProtocol} \r\n");
            richTextBox2.AppendText($"Display Plots  : {e.MeasurementPlanInfo.DisplayPlots} \r\n");
            richTextBox2.AppendText($"Featurelist    : {e.MeasurementPlanInfo.FeatureListName} \r\n");
            richTextBox2.AppendText($"Filename       : {e.MeasurementPlanInfo.FileName} \r\n");
            richTextBox2.AppendText($"Motion Planning: {e.MeasurementPlanInfo.MotionPlanning} \r\n");
            richTextBox2.AppendText($"Navi Mode      : {e.MeasurementPlanInfo.NaviMode} \r\n");
            richTextBox2.AppendText($"PDF Export     : {e.MeasurementPlanInfo.PdfExport} \r\n");
            richTextBox2.AppendText($"Pres. Protocol : {e.MeasurementPlanInfo.PresentationProtocol} \r\n");
            richTextBox2.AppendText($"Printer        : {e.MeasurementPlanInfo.Printer} \r\n");
            richTextBox2.AppendText($"Print Plots    : {e.MeasurementPlanInfo.PrintPlots} \r\n");
            richTextBox2.AppendText($"Run Mode       : {e.MeasurementPlanInfo.RunMode} \r\n");
            richTextBox2.AppendText($"Speed          : {e.MeasurementPlanInfo.Speed} \r\n");
        }

        private void Api_MeasurementInfoChanged(object sender, MeasurementInfoEventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText($"Filename: {e.MeasurementInfo.MeasurementPlanFileName} \r\n");
            richTextBox1.AppendText($"Plan ID : {e.MeasurementInfo.MeasurementPlanId} \r\n");
            richTextBox1.AppendText($"Operator: {e.MeasurementInfo.OperatorId} \r\n");
            richTextBox1.AppendText($"Part No : {e.MeasurementInfo.PartNumber} \r\n");
            richTextBox1.AppendText($"HDR Path: {e.MeasurementInfo.HdrFilePath} \r\n");
            richTextBox1.AppendText($"CHR Path: {e.MeasurementInfo.ChrFilePath} \r\n");
            richTextBox1.AppendText($"FET Path: {e.MeasurementInfo.FetFilePath} \r\n");
            richTextBox1.AppendText($"Control.: {e.MeasurementInfo.ControllerType} \r\n");
            richTextBox1.AppendText($"Device  : {e.MeasurementInfo.DeviceGroup} \r\n");
            richTextBox1.AppendText($"Firmware: {e.MeasurementInfo.FirmwareRevision} \r\n");
        }

        private void Api_CmmStateChanged(object sender, CmmStateChangedEventArgs e)
        {
            lblStatus.Text = e.Status.ToString();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select CMMObserver path...";
        }

        private void richTextBox2_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            api.Initialize();
        }
    }
}
