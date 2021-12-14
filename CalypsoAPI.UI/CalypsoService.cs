using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using CalypsoAPI;
using CalypsoAPI.Interface;
using CalypsoAPI.WebApi;
using Microsoft.AspNetCore.Hosting;
using System.Windows.Controls;

namespace CalypsoAPI.UI
{
    public class CalypsoService
    {
        public ICalypso Calypso { get; private set; }

        public CalypsoService(RichTextBox textBox)
        {
            Calypso = new CalypsoBuilder()
                .ConfigureDefault()
                .AddWebApi(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:5000");
                })
                .Build();
        }
    }
}
