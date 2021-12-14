using CalypsoAPI.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CalypsoAPI.WebApi
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

        public WebApi(ICalypso calypso, Action<IWebHostBuilder> webBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();

            _calypso = calypso;
            _host = CreateHostBuilder(null)
                .ConfigureWebHostDefaults(webBuilder)
                .Build();
        }

        private IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
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
            await _host.StartAsync();
        }

        public async Task StopAsync()
        {
            await _host.StopAsync();
        }
    }
}
