using CalypsoAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CalypsoAPI.WebApi
{
    public static class Extensions
    {
        /// <summary>
        /// Add a web-api with default settings - listen on http://localhost:80
        /// </summary>
        /// <param name="calypso"></param>
        /// <returns></returns>
        public static CalypsoBuilder AddWebApi(this CalypsoBuilder builder)
        {
            builder.Calypso.Services.Add(new WebApi(builder.Calypso));
            return builder;
        }

        /// <summary>
        /// Add a web-api with custom host configuration
        /// </summary>
        /// <param name="calypso"></param>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static CalypsoBuilder AddWebApi(this CalypsoBuilder builder, Action<IWebHostBuilder> webHostBuilder)
        {
            builder.Calypso.Services.Add(new WebApi(builder.Calypso, webHostBuilder));
            return builder;
        }
    }
}
