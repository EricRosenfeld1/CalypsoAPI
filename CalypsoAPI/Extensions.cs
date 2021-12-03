using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI
{
    public static class Extensions
    {
        public static CalypsoBuilder Configure(this CalypsoBuilder builder, Action<CalypsoConfiguration> configuration)
        {
            CalypsoConfiguration config = new CalypsoConfiguration();
            configuration(config);
            builder.Calypso.Configuration = config;
            return builder;
        }

        public static CalypsoBuilder ConfigureDefault(this CalypsoBuilder builder)
        {
            builder.Calypso.Configuration = new CalypsoConfiguration();
            return builder;
        }

    }

    public class CalypsoBuilder
    {
        private Calypso _calypso = new Calypso();
        public Calypso Calypso => _calypso;

        public Calypso Build() => _calypso;
    }
}
