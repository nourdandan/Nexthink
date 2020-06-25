using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Xml;
using Nexthink.Interfaces;
using Nexthink.Entities;
using System.Xml.Linq;
using Nexthink.Entities.EntitiesMapper;
using System.IO;

namespace Nexthink.Parsers
{
    public class XmlParser : IDataParser
    {

        private string filePath;
        private bool hasHeader;

        public XmlParser(string filePath, bool hasHeader)
        {
            this.filePath = new Uri(Path.GetFullPath(filePath)).ToString();
            this.hasHeader = hasHeader;
        }

        /// <summary>
        /// load xml file
        /// get corresponding nodes and iterate through them
        /// returns all data
        /// </summary>
        /// <returns></returns>
        public List<DeviceData> GetParsedData()
        {
            XDocument doc = XDocument.Load(filePath);
            var root = doc.Root;
            var ns = root.GetDefaultNamespace();
          var body =  doc.Root.Element(ns + "body").Elements(ns+"r");
            if (root.IsEmpty)
                throw new Exception();
            return LoadData(body,ns);
        }

        /// <summary>
        /// Loads data using the corresponding file type value mapper
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        public static List<DeviceData> LoadData(IEnumerable<XElement> elements,XNamespace ns)
        {
            var devices = new List<DeviceData>();
            foreach (var innerBodyTag in elements)
            {
                devices.AddRange(
                    new DeviceXmlMap(innerBodyTag,ns)
                    .ListOfDevices
                    );
            }
            return devices;
        }
    }
}
