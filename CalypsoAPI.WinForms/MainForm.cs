using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalypsoAPI.Core;
using CalypsoAPI.Core.Events;

namespace CalypsoAPI.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var api = new Calypso(new ApiConfiguration(@"C:\Users\Public\Documents\Zeiss\CMMObserver"));
            api.Initialize();

            api.CMMStateChanged += Api_CMMStateChanged;
            api.MeasurementInfoChanged += Api_MeasurementInfoChanged;
        }

        private void Api_MeasurementInfoChanged(object sender, MeasurementInfoEventargs e)
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

        private void Api_CMMStateChanged(object sender, CMMStateChangedEventArgs e)
        {
            lblStatus.Text = e.Status.ToString();
        }
    }
}
