using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalypsoAPI;
using CalypsoAPI.Interface;
using CalypsoAPI.WebApi;

namespace CalypsoAPI.UI
{
    public class CalypsoService
    {
        public ICalypso Calypso { get; private set; }

        public CalypsoService()
        {
            Calypso = new CalypsoBuilder()
                .ConfigureDefault()
                .AddWebApi()
                .Build();
        }
    }
}
