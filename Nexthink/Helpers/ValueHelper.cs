using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nexthink.Helpers
{
    class ValueHelper
    {

        public static string GetNodeIntValueWithValidation(XElement parentNode, XName childNodeName)
        {
            return ValueHandler<int>(parentNode, childNodeName, int.TryParse)?
                .ToString();
        }

        public static string GetNodeDecimalValueWithValidation(XElement parentNode, XName childNodeName, string format = "0.0")
        {
            return ValueHandler<double>(parentNode, childNodeName, double.TryParse)?
                .ToString(format);
        }


        private static string CheckNodeExists(XElement parentNode, XName childNodeName)
        {
            if (!parentNode.Descendants(childNodeName).Any())
                throw new Exception($"missing element or invalid element name: {childNodeName}");
            return parentNode.Descendants(childNodeName).FirstOrDefault().Value;
        }

        public static string GetNodeValueAsStringOrNull(XElement parentNode, XName childNodeName, bool isMandatory = false)
        {
            var value = CheckNodeExists(parentNode, childNodeName);
            return ((isMandatory && string.IsNullOrEmpty(value))
                ? throw new InvalidCastException($"Mandatory data missing: {childNodeName}")
                : string.IsNullOrEmpty(value)
                ? null
                : value);
        }

        public static IEnumerable<XElement> GetNodeValueInParent(XElement parentNode, XName childNodeName, XName innerChild)
        {
            if (!parentNode.Elements(childNodeName).Any())
                return null;
            if (!parentNode.Element(childNodeName).HasElements)
                throw new Exception($"missing element or invalid element name: {childNodeName}");
            var monitorsNode = parentNode.Element(childNodeName);
            return monitorsNode.Elements(innerChild);
        }

        private static T? ValueHandler<T>(XElement parentNode, XName childNodeName, TryParseHandler<T> handler) where T : struct
        {
            var value = CheckNodeExists(parentNode, childNodeName);
            if (string.IsNullOrEmpty(value))
                throw new InvalidCastException($"Mandatory field {typeof(T)} type value for node: {childNodeName}");
            if (string.IsNullOrEmpty(value))
                return null;
            T obj;
            if (!handler(value, out obj))
                throw new InvalidCastException($"Invalid {typeof(T)} type value for node: {childNodeName}");
            return obj;
        }



        private delegate bool TryParseHandler<T>(string value, out T result);
    }
}
