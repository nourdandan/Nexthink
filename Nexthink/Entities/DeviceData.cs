using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexthink.Entities
{
   public class DeviceData
    {
        public string Name { get; set; }
        public string LastIpAddress { get; set; }
        public string MonitorName { get; set; }
        public string MonitorSerialNumber { get; set; }
        public string MonitorVendor { get; set; }
        public double MonitorDiagonal  { get; set; }
        public int MonitorMaxHorizontal  { get; set; }
        public int MonitorMaxVertical  { get; set; }
    }
}
