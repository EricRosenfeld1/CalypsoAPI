using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalypsoAPI;
using CalypsoAPI.WebApi;
using CalypsoAPI.Interface;
using Microsoft.Toolkit.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using CalypsoAPI.UI.ViewModels;
using System.Diagnostics;

namespace CalypsoAPI.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<MainViewModel>();
        }


    }
}
