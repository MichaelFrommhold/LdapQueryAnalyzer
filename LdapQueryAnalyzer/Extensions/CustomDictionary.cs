// =========================================================================
// THIS CODE-SAMPLE IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

// This sample is not supported under any Microsoft standard support program 
// or service. The code sample is provided AS IS without warranty of any kind. 
// Microsoft further disclaims all implied warranties including, without 
// limitation, any implied warranties of merchantability or of fitness for a 
// particular purpose. The entire risk arising out of the use or performance
// of the sample and documentation remains with you. In no event shall 
// Microsoft, its authors, or anyone else involved in the creation, 
// production, or delivery of the script be liable for any damages whatsoever 
// (including, without limitation, damages for loss of business profits, 
// business interruption, loss of business information, or other pecuniary 
// loss) arising out of  the use of or inability to use the sample or 
// documentation, even if Microsoft has been advised of the possibility of 
// such damages.
//========================================================================= 

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    [XmlRoot("dictionary"), Serializable]
    public class CustomDictionary<TKey, TVal> : Dictionary<TKey, TVal>, IXmlSerializable
    {

        public CustomDictionary(IComparer<TKey> comparer)
            : base()
        { }

        public CustomDictionary()
            : base()
        { }

        public CustomDictionary(Dictionary<TKey, TVal> dictionary)
            : base(dictionary)
        { }

        public XmlSchema GetSchema()
        { return null; }

        public void ReadXml(XmlReader xmlIn)
        {
            XmlSerializer xmlkey = new XmlSerializer(typeof(TKey));
            XmlSerializer xmlvalue = new XmlSerializer(typeof(TVal));

            bool empty = xmlIn.IsEmptyElement;

            xmlIn.Read();

            if ((xmlIn == null) || (xmlIn.IsEmptyElement))
            { return; }

            while (xmlIn.NodeType != XmlNodeType.EndElement)
            {
                xmlIn.ReadStartElement("item");

                xmlIn.ReadStartElement("key");
                TKey key = (TKey)xmlkey.Deserialize(xmlIn);
                xmlIn.ReadEndElement();

                xmlIn.ReadStartElement("value");
                TVal value = (TVal)xmlvalue.Deserialize(xmlIn);
                xmlIn.ReadEndElement();

                this.Add(key, value);

                xmlIn.ReadEndElement();
                xmlIn.MoveToContent();
            }
            xmlIn.ReadEndElement();
        }

        public void WriteXml(XmlWriter xmlOut)
        {
            XmlSerializer xmlkey = new XmlSerializer(typeof(TKey));
            XmlSerializer xmlvalue = new XmlSerializer(typeof(TVal));

            foreach (TKey key in this.Keys)
            {
                xmlOut.WriteStartElement("item");

                xmlOut.WriteStartElement("key");
                xmlkey.Serialize(xmlOut, key);
                xmlOut.WriteEndElement();

                xmlOut.WriteStartElement("value");
                TVal value = this[key];
                xmlvalue.Serialize(xmlOut, value);
                xmlOut.WriteEndElement();

                xmlOut.WriteEndElement();
            }
        }
    }
}
