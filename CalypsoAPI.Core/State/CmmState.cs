using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core.Models.State
{
    public class CmmState : INotifyPropertyChanged
    {
        private Status status = Status.Stopped;
        /// <summary>
        /// Current state of the measuring machine
        /// </summary>
        public Status Status
        {
            get => status;
            set => SetField(ref status, value); 
        }


        private MeasurementPlanInfo measurementPlanInfo = new MeasurementPlanInfo();
        /// <summary>
        /// Current measurement plan details
        /// </summary>
        public MeasurementPlanInfo MeasurementPlan 
        { 
            get => measurementPlanInfo; 
            set => SetField(ref measurementPlanInfo, value); 
        }

        private MeasurementResult latestMeasurementResults = new MeasurementResult();
        /// <summary>
        /// Latest available measurement results
        /// </summary>
        public MeasurementResult LatestMeasurementResults
        {
            get => latestMeasurementResults;
            set => SetField(ref latestMeasurementResults, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
