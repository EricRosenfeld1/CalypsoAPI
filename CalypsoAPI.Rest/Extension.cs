using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalypsoAPI.Core;

namespace CalypsoAPI.Rest
{
    public static class Extension
    {
        public static Calypso AddRestApi(this Calypso calypso)
        {
            calypso.Services.Add(new RestApi(calypso));
            return calypso;
        }
    }
}
