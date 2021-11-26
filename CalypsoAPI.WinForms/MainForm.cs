using CalypsoAPI.Core;
using CalypsoAPI.Core.Events;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalypsoAPI.WinForms
{
    public partial class MainForm : Form
    {
        private CalypsoConfiguration calypsoConfiguration;
        private Calypso calypso;

        public MainForm()
        {
            InitializeComponent();

            calypsoConfiguration = new CalypsoConfiguration();
            calypso = new Calypso(calypsoConfiguration);
            calypso.MeasurementStarted += Calypso_MeasurementStarted;

            lblPath.Text = calypsoConfiguration.CMMObserverFolderPath;

            lblStatus.DataBindings.Add(new Binding("Text", calypso.State, "Status"));
        }

        private void Calypso_MeasurementStarted(object sender, MeasurementStartEventArgs e)
        {
            richTextBox2.Clear();
            richTextBox2.AppendText($"Filename: {e.MeasurementPlan.FileName} \r\n");
            richTextBox2.AppendText($"Basesystem: {e.MeasurementPlan.BaseSystemName} \r\n");
            richTextBox2.AppendText($"Basesystem Type: {e.MeasurementPlan.BaseSystemType} \r\n");
            richTextBox2.AppendText($"Date: {e.MeasurementPlan.Date} \r\n");
            richTextBox2.AppendText($"Time: {e.MeasurementPlan.Time} \r\n");
            richTextBox2.AppendText($"Speed: {e.MeasurementPlan.Speed} \r\n");
            richTextBox2.AppendText($"Operator Id: {e.MeasurementPlan.OperatorId} \r\n");
            richTextBox2.AppendText($"Run Mode: {e.MeasurementPlan.RunMode} \r\n");
            richTextBox2.AppendText($"Feature List: {e.MeasurementPlan.FeatureListName} \r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select the CMMObserver path...";
            var res = dialog.ShowDialog();

            if(res == DialogResult.OK)
            {
                calypsoConfiguration.CMMObserverFolderPath = dialog.SelectedPath;
                lblPath.Text = calypsoConfiguration.CMMObserverFolderPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            calypso?.Stop();
            calypso?.Configure(calypsoConfiguration);
            calypso?.Start();

        }
    }
}
