using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalypsoAPI.Core;
using CalypsoAPI.Rest;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var calypso = new Calypso()
                .Configure(configuration =>
                {
                    configuration.CMMObserverFolderPath = @"C:\Users\Public\Documents\Zeiss\CMMObserver";
                    configuration.CopyChrFileAfterReading = false;
                })
                .AddRestApi()
                .Run();
        }

    }
}
