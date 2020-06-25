using Nexthink.Entities;
using Nexthink.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexthink.BusinessLayer
{
    public class DataFilter
    {
        private List<DeviceData> monitorRecords = new List<DeviceData>();

        public DataFilter(IDataParser parser)
        {
            monitorRecords = parser.GetParsedData();
        }

        public List<DeviceData> GetAll()
        {
            return monitorRecords;
        }

        /// <summary>
        /// get data where number of montitor > numberOfMonitors 
        /// select everything else that is outside of that list
        /// assuming unique monitors are identified via monitor name mapped to name
        /// </summary>
        /// <param name="numberOfMonitors"></param>
        /// <returns Tuple Name,LastIpAddress></returns>
        public List<Tuple<string, string>> GetDevicesWhereMonitor(int numberOfMonitors)
        {
            var filteredData = GroupRecordsByName(numberOfMonitors);

            return monitorRecords
                    .Where(od => filteredData
                    .All(fd => fd != od.Name))
                    .Select(device => new Tuple<string, string>(item1: device.Name, item2: device.LastIpAddress)).ToList();

        }

        /// <summary>
        /// Groups Data by Monitor Name
        /// Where count not larger than passed parameter
        /// </summary>
        /// <param name="numberOfMonitors"></param>
        /// <returns>List of MonitorNames</returns>
        IEnumerable<string> GroupRecordsByName(int numberOfMonitors)
        {
            return monitorRecords
                           .GroupBy(x => x.Name)
                           .Where(x => x.Count() > numberOfMonitors)
                           .Select(g => g.Key)
                           .ToList();
        }
    }
}
