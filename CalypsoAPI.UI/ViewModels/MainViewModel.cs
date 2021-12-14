using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CalypsoAPI.Interface;
using CalypsoAPI.Models;
using CalypsoAPI.WebApi;
using Microsoft.AspNetCore.Hosting;
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


        public MainViewModel()
        {
            Calypso = new CalypsoBuilder()
                .ConfigureDefault()
                .AddWebApi(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:5000");
                })
                .Build();

            Calypso.CalypsoException += Calypso_CalypsoException;
            Calypso.MeasurementFinished += Calypso_MeasurementFinished;

            StartCalypsoAsyncCommand = new AsyncRelayCommand(Calypso.StartAsync);
            StopCalypsoAsyncCommand = new AsyncRelayCommand(Calypso.StopAsync);
        }

        private void Calypso_MeasurementFinished(object sender, Events.MeasurementFinishEventArgs e)
        {
        
        }

        private void Calypso_CalypsoException(object sender, Events.CalypsoExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

    }
}
