using CalypsoAPI.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CalypsoAPI.Rest
{
    public class WebApi : IService
    {
        private IHost _host;
        private ICalypso _calypso;

        public WebApi(ICalypso calypso)
        {
            _calypso = calypso;
            _host = CreateHostBuilder(null)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:80");
                })           
                .Build();
        }

        public WebApi(ICalypso calypso, Action<IHostBuilder> webBuilder)
        {
            _calypso = calypso;
            var builder = CreateHostBuilder(null);

            webBuilder(builder);

            _host = builder.Build();
        }

        private IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices(services =>
                {
                    services.AddSingleton(_calypso);
                });
                
        public async Task StartAsync()
        {
           await _host.RunAsync();
        }

        public async Task StopAsync()
        {
            await _host.StopAsync();
        }
    }
}
