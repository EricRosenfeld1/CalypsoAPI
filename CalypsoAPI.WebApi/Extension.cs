using CalypsoAPI.Core;
using Microsoft.Extensions.Hosting;
using System;

namespace CalypsoAPI.WebApi
{
    public static class Extension
    {
        /// <summary>
        /// Add a web-api with default settings - listen on http://localhost:80
        /// </summary>
        /// <param name="calypso"></param>
        /// <returns></returns>
        public static Calypso AddWebApi(this Calypso calypso)
        {
            calypso.Services.Add(new WebApi(calypso));
            return calypso;
        }

        /// <summary>
        /// Add a web-api with custom host configuration
        /// </summary>
        /// <param name="calypso"></param>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static Calypso AddWebApi(this Calypso calypso, Action<IHostBuilder> hostBuilder)
        {
            calypso.Services.Add(new WebApi(calypso, hostBuilder));
            return calypso;
        }
    }
}
