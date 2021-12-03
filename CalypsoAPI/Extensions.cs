using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI
{
    public static class Extensions
    {
        public static Calypso Configure(this Calypso calypso, Action<CalypsoConfiguration> configuration)
        {
            CalypsoConfiguration config = new CalypsoConfiguration();
            configuration(config);
            calypso.Configuration = config;
            return calypso;
        }

        public static async Task<Calypso> Run(this Calypso calypso)
        {
            await calypso.StartAsync();
            return calypso;
        }
    }
}
