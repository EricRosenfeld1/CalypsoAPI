using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CalypsoAPI.Interface;
using Microsoft.Toolkit.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CalypsoAPI.UI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ICalypso Calypso { get; set; }

        public IAsyncRelayCommand StartCalypsoAsyncCommand { get; }
        public IAsyncRelayCommand StopCalypsoAsyncCommand { get; }

        public string Status { get; set; }

        public MainViewModel(CalypsoService calypsoService)
        {
            Calypso = calypsoService.Calypso;
            Calypso.CalypsoException += Calypso_CalypsoException;

            StartCalypsoAsyncCommand = new AsyncRelayCommand(Calypso.StartAsync);
            StopCalypsoAsyncCommand = new AsyncRelayCommand(Calypso.StopAsync);
        }

        private void Calypso_CalypsoException(object sender, Events.CalypsoExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

    }
}
