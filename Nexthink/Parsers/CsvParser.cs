using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Nexthink.Entities;
using Nexthink.Entities.EntitiesMapper;
using Nexthink.Interfaces;

namespace Nexthink.Parsers
{
   public class CsvParser: IDataParser
    {
        private string filePath;
        private bool hasHeader;

        public CsvParser(string filePath,bool hasHeader)
        {
            this.filePath = filePath;
            this.hasHeader = hasHeader;
        }

        public List<DeviceData> GetParsedData()
        {
            return ReadData();
        }

        List<DeviceData> ReadData()
        {
            var monitorDataRecords = new List<DeviceData>();
            using (var reader= new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<DeviceCsvMap>();
                    while (csv.Read())
                    {
                        if(hasHeader)
                        {
                            csv.ReadHeader();
                            hasHeader = false;
                            continue;
                        }
                        monitorDataRecords.Add(csv.GetRecord<DeviceData>());
                    }
                }
            }
            return monitorDataRecords;
        }
    }
}
