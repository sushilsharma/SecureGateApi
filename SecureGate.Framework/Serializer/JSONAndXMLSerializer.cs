using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SecureGate.Framework.Serializer
{
    public static class JSONAndXMLSerializer
    {

        public static string JSONtoXML(string jsonText)
        {
            string xmlNode = JsonConvert.DeserializeXmlNode(jsonText).OuterXml;
            return xmlNode;
        }


        public static string XMLtoJSON(string xmlText)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlText);

            string jsonText = JsonConvert.SerializeXmlNode(doc);

            return jsonText;
        }

    }
}
