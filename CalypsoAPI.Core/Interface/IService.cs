using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoAPI.Core.Interface
{
    public interface IService
    {
        Task StartAsync();
        Task StopAsync();
    }
}
