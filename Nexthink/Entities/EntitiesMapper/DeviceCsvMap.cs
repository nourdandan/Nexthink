using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexthink.Entities.EntitiesMapper
{
    public sealed class DeviceCsvMap : ClassMap<DeviceData>
    {
        /// <summary>
        /// Maps values of csv data into our entity
        /// This plays the role of data layer 
        /// </summary>
        public DeviceCsvMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.LastIpAddress).Index(1);
            Map(m => m.MonitorName).Index(2);
            Map(m => m.MonitorSerialNumber).Index(3);
            Map(m => m.MonitorVendor).Index(4);
            Map(m => m.MonitorDiagonal).Index(5).TypeConverterOption.NumberStyles(System.Globalization.NumberStyles.AllowDecimalPoint);
            Map(m => m.MonitorMaxHorizontal).Index(6).TypeConverterOption.NumberStyles(System.Globalization.NumberStyles.AllowDecimalPoint);
            Map(m => m.MonitorMaxVertical).Index(7).TypeConverterOption.NumberStyles(System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }
}
