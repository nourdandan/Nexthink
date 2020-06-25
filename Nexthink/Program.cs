using Nexthink.BusinessLayer;
using Nexthink.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexthink
{
    class Program
    {
        static void Main(string[] args)
        {
            var folderPath = @"C:\Users\n.dandan\Documents\PowershellSoftwareDeveloper";
            var files = Directory.GetFiles(folderPath);
           
            foreach (var fileName in files)
            {
                DataFilter filter;
                switch (Path.GetExtension(fileName).ToString())
                {
                    case ".xml":
                         filter = new DataFilter(new XmlParser(fileName, true));
                        PrintValues(filter.GetDevicesWhereMonitor(1),"xml");
                        break;
                    case ".csv":
                        filter = new DataFilter(new CsvParser(fileName, true));
                        PrintValues(filter.GetDevicesWhereMonitor(1), "csv");
                        break;
                    default:
                        break;
                }
            }
        }

        static void PrintValues(List<Tuple<string,string>> devices,string fileType)
        {
            foreach (var tuple in devices)
            {
                Console.WriteLine($"FileType : {fileType}, Device name : {tuple.Item1} , Last IP : {tuple.Item2}");
            }
        }
    }
}
