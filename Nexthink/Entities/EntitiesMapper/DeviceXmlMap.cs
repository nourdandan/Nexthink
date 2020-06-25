using Nexthink.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Nexthink.Entities.EntitiesMapper
{
    public sealed class DeviceXmlMap
    {
        public static string MainNodeName = "investigation";
        public static string BodyNodeName = "body";
        public List<DeviceData> ListOfDevices = new List<DeviceData>();

        public DeviceXmlMap(XElement element, XNamespace nameSpace)
        {
            var monitors = ValueHelper.GetNodeValueInParent(element, nameSpace+"c1", nameSpace+"monitor");
            foreach (var monitor in monitors)
            {
                var deviceData = new DeviceData();
                deviceData.Name = ValueHelper.GetNodeValueAsStringOrNull(element, nameSpace + "c0");
                deviceData.MonitorName = ValueHelper.GetNodeValueAsStringOrNull(monitor, nameSpace + "name");
                deviceData.MonitorSerialNumber = ValueHelper.GetNodeValueAsStringOrNull(monitor, nameSpace + "serial_number");
                deviceData.MonitorVendor = ValueHelper.GetNodeValueAsStringOrNull(monitor, nameSpace + "vendor");
                deviceData.MonitorDiagonal = Convert.ToDouble(ValueHelper.GetNodeDecimalValueWithValidation(monitor, nameSpace + "diagonal"));
                deviceData.MonitorMaxHorizontal = Convert.ToInt32(
                    ValueHelper.GetNodeIntValueWithValidation(
                        monitor.Element(nameSpace+"max_resolution"), nameSpace + "horizontal"
                        ));
                deviceData.MonitorMaxVertical = Convert.ToInt32(
                    ValueHelper.GetNodeIntValueWithValidation(
                        monitor.Element(nameSpace + "max_resolution"), nameSpace + "vertical"
                        ));
                deviceData.LastIpAddress = ValueHelper.GetNodeValueAsStringOrNull(element, nameSpace + "c2");
                ListOfDevices.Add(deviceData);
            }
        }

    }
}

