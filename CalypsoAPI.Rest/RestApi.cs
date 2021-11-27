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

namespace CalypsoAPI.Rest
{
    public class RestApi : IService
    {
        private IHost host;

        public RestApi(Calypso calypso)
        {
            host = CreateHostBuilder(null)
                .ConfigureServices(services => 
                {
                    services.AddSingleton(calypso);
                })
                .Build();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public async Task StartAsync()
        {
            await host.StartAsync();
        }

        public async Task StopAsync()
        {
            await host.StopAsync();
        }
    }
}
