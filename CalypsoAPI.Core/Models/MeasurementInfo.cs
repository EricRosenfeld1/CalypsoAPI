using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalypsoAPI.Core.Models
{
    public class MeasurementInfo : INotifyPropertyChanged
    {
        private string measurementPlanId = string.Empty;
        public string MeasurementPlanId 
        {
            get => measurementPlanId; 
            set => SetField(ref measurementPlanId, value); 
        }

        private string measurementPlanFileName = string.Empty;
        public string MeasurementPlanFileName 
        { 
            get => measurementPlanFileName; 
            set => SetField(ref measurementPlanFileName, value); 
        }

        public string partNumber = string.Empty;
        public string PartNumber 
        { 
            get => partNumber; 
            set => SetField(ref partNumber, value); 
        }

        private string operatorId = string.Empty;
        public string OperatorId 
        { 
            get => operatorId; 
            set => SetField(ref operatorId, value); 
        }

        private string deviceGroup = string.Empty;
        public string DeviceGroup
        { 
            get => deviceGroup; 
            set => SetField(ref deviceGroup, value); 
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
