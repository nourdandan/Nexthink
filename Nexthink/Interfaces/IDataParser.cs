using Nexthink.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexthink.Interfaces
{
    public interface IDataParser
    {
        List<DeviceData> GetParsedData();
    }
}
