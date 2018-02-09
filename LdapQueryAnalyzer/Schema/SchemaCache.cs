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
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SchemaCache : AttributeSyntax
    {
        #region fields

        private const string CacheVersion = "3";

        //see ADFields
        #endregion

        #region constructor

        public SchemaCache()
            : base()
        { }

        #endregion

        #region methods

        public void LoadCache(string forestName, DateTime modifyTimeStamp)
        {
            base.MustLoadSchemaFromAD = false;


        }

        public void LoadCacheFile(string forestName)
        {

        }

        public void LoadCacheFromXML(string forestName, Int32 updateVer)
        {
            base.MustLoadSchemaFromAD = false;

            if (GetCache(forestName, updateVer))
            {
                XElement version = ForestBase.XMLCache.Root.Elements().Where(e => e.Name == "CacheVersion").FirstOrDefault();

                if (version != null)
                {
                    if (version.Value != CacheVersion)
                    {
                        // clearcache file and forestbase cache
                        base.MustLoadSchemaFromAD = true;
                    }
                }

                else
                { base.MustLoadSchemaFromAD = true; }

                if (base.MustLoadSchemaFromAD == false)
                {
                    XAttribute xuver = ForestBase.CurrentForestNode.Attributes().Where(a => a.Name == "UpdateVersion").FirstOrDefault();

                    if (xuver != null)
                    {
                        Int32 uver = 0;

                        Int32.TryParse(xuver.Value, out uver);

                        ForestBase.CurrentSchemaUpdateVersion = uver;

                        if (ForestBase.CurrentSchemaUpdateVersion < updateVer)
                        {
                            base.MustLoadSchemaFromAD = true;

                            ForestBase.CurrentClassesNode.Descendants().Remove();
                            ForestBase.CurrentClassesNode.Add(new XElement("All", new XAttribute("value", "*")));
                            ForestBase.CurrentAttributesNode.Descendants().Remove();
                            ForestBase.CurrentExtendedRightsNode.Elements().Remove();
                        }
                    }

                    else
                    { base.MustLoadSchemaFromAD = true; }
                }
            }

            else
            { base.MustLoadSchemaFromAD = true; }

            ForestBase.SchemaCacheIsDirty = base.MustLoadSchemaFromAD;
        }


        public void LoadCacheFromXMLOld(string forestName, DateTime modifyTimeStamp)
        {
            base.MustLoadSchemaFromAD = false;

            if (GetCacheOld(forestName, modifyTimeStamp))
            {
                if (DateTime.TryParse(ForestBase.CurrentForestNode.Attribute("modifyTimeStamp").Value, out ForestBase.CurrentSchemaTimeStamp))
                {
                    XElement version = ForestBase.XMLCache.Root.Elements().Where(e => e.Name == "CacheVersion").FirstOrDefault();

                    if (version != null)
                    {
                        if (version.Value != CacheVersion)
                        {
                            // clearcache file and forestbase cache
                            base.MustLoadSchemaFromAD = true;
                        }
                    }

                    else
                    { base.MustLoadSchemaFromAD = true; }

                    if (!base.MustLoadSchemaFromAD)
                    {
                        if (ForestBase.CurrentSchemaTimeStamp < modifyTimeStamp)
                        {
                            base.MustLoadSchemaFromAD = true;

                            ForestBase.CurrentClassesNode.Descendants().Remove();
                            ForestBase.CurrentClassesNode.Add(new XElement("All", new XAttribute("value", "*")));
                            ForestBase.CurrentAttributesNode.Descendants().Remove();
                            ForestBase.CurrentExtendedRightsNode.Elements().Remove();
                        }
                    }
                }

                else
                { base.MustLoadSchemaFromAD = true; }
            }

            else
            { base.MustLoadSchemaFromAD = true; }

            ForestBase.SchemaCacheIsDirty = base.MustLoadSchemaFromAD;
        }

        #region load from ad

        public void DownloadSchemaCache(List<SearchResultEntry> attributeSchema, 
                                        List<SearchResultEntry> classSchema, 
                                        List<SearchResultEntry> extendedRights, 
                                        Int32 updateVer)
        {
            Reset();

            ForestBase.CurrentForestNode.Attributes().Remove();

            ForestBase.CurrentForestNode.Add(new XAttribute("UpdateVersion", updateVer.ToString()));

            StoreAttributes(attributeSchema);

            StoreClasses(classSchema);

            StoreExtendedRights(extendedRights);

            SaveCache();
        }

        public void DownloadSchemaCacheOld(List<SearchResultEntry> attributeSchema, List<SearchResultEntry> classSchema, List<SearchResultEntry> extendedRights, DateTime modifyTimeStamp)
        {
            Reset();

            ForestBase.CurrentForestNode.Attributes().Remove();

            ForestBase.CurrentForestNode.Add(new XAttribute("modifyTimeStamp", modifyTimeStamp.ToString()));

            StoreAttributes(attributeSchema);

            StoreClasses(classSchema);

            StoreExtendedRights(extendedRights);

            SaveCache();
        }

        private void StoreAttributes(List<SearchResultEntry> attributeSchema)
        {
            foreach (SearchResultEntry entry in attributeSchema)
            {
                bool isdefunct = false;

                ActiveDirectorySyntax syntax = ActiveDirectorySyntax.CaseIgnoreString;

                if (entry.Attributes["isDefunct"] != null)
                {
                    string sdef = entry.Attributes["isDefunct"].GetValues(typeof(string))[0].ToString();

                    isdefunct = Boolean.Parse(sdef);
                }

                if (!isdefunct)
                {
                    #region name + syntax

                    string name = entry.Attributes["lDAPDisplayName"][0].ToString();

                    string asyntax = entry.Attributes["attributeSyntax"][0].ToString();

                    int omsyntax = 0;

                    if (Int32.TryParse(entry.Attributes["oMSyntax"][0].ToString(), out omsyntax))
                    { syntax = DecodeLDAPAttributeSyntax(asyntax, omsyntax); }

                    long systemflags = 0;

                    if (entry.Attributes.Contains("systemFlags"))
                    { long.TryParse(entry.Attributes["systemFlags"][0].ToString(), out systemflags); }

                    long linkid = 0;

                    if (entry.Attributes.Contains("linkID"))
                    { long.TryParse(entry.Attributes["linkID"][0].ToString(), out linkid); }



                    bool flags = false;
                    if ((systemflags & 4) == 4) flags = true;

                    ForestBase.AttributeCache.Add(name, new AttributeSchema(name, syntax, flags, linkid));

                    ForestBase.CurrentAttributesNode.Add(new XElement(name, syntax.ToString(), new XAttribute("checked", "0"), new XAttribute("constructed", Convert.ToInt32(flags)), new XAttribute("linked", Convert.ToInt32(linkid))));

                    ForestBase.CurrentClassesNode.Elements("All").FirstOrDefault().Add(new XElement("Attribute", name));

                    #endregion

                    #region guids

                    string oid = String.Empty;

                    foreach (byte[] value in entry.Attributes["schemaIDGUID"].GetValues(typeof(byte[])))
                    {
                        oid = new Guid(value).ToString();

                        if (ForestBase.RightsGuids.AddSafe<string, string>(oid, name))
                        { ForestBase.CurrentExtendedRightsNode.Add(new XElement("GUID" + oid.ToUpperInvariant(), name)); }
                    }

                    if (entry.Attributes.Contains("attributeSecurityGUID"))
                    {
                        foreach (byte[] value in entry.Attributes["attributeSecurityGUID"].GetValues(typeof(byte[])))
                        {
                            oid = new Guid(value).ToString();

                            if (ForestBase.RightsGuids.AddSafe<string, string>(oid, name))
                            { ForestBase.CurrentExtendedRightsNode.Add(new XElement("GUID" + oid.ToUpperInvariant(), name)); }
                        }
                    }

                    #endregion
                }
            }
        }

        private void StoreClasses(List<SearchResultEntry> classSchema)
        {
            if (ForestBase.CurrentClassesNode.Elements("All").FirstOrDefault() == null)
            { ForestBase.CurrentClassesNode.Add(new XElement("All", new XAttribute("value", "*"))); }

            ForestBase.ClassCache.Add("All", "*");

            foreach (SearchResultEntry entry in classSchema)
            {
                bool isdefunct = false;

                if (entry.Attributes["isDefunct"] != null)
                {
                    string sdef = entry.Attributes["isDefunct"].GetValues(typeof(string))[0].ToString();

                    isdefunct = Boolean.Parse(sdef);
                }

                if (!isdefunct)
                {
                    string name = entry.Attributes["lDAPDisplayName"][0].ToString();

                    string oid = null;

                    foreach (byte[] value in entry.Attributes["schemaIDGUID"].GetValues(typeof(byte[])))
                    {
                        oid = new Guid(value).ToString();

                        break;
                    }

                    List<string> mustmay = new List<string>();

                    if (entry.Attributes["systemMustContain"] != null)
                    { mustmay.AddRange(((string[])entry.Attributes["systemMustContain"].GetValues(typeof(string))).ToList()); }

                    if (entry.Attributes["systemMayContain"] != null)
                    { mustmay.AddRange(((string[])entry.Attributes["systemMayContain"].GetValues(typeof(string))).ToList()); }

                    if (entry.Attributes["mayContain"] != null)
                    { mustmay.AddRange(((string[])entry.Attributes["mayContain"].GetValues(typeof(string))).ToList()); }

                    if (entry.Attributes["mustContain"] != null)
                    { mustmay.AddRange(((string[])entry.Attributes["mustContain"].GetValues(typeof(string))).ToList()); }

                    List<string> aux = new List<string>();

                    if (entry.Attributes["systemAuxiliaryClass"] != null)
                    { aux.AddRange(((string[])entry.Attributes["systemAuxiliaryClass"].GetValues(typeof(string))).ToList()); }

                    if (entry.Attributes["auxiliaryClass"] != null)
                    { aux.AddRange(((string[])entry.Attributes["auxiliaryClass"].GetValues(typeof(string))).ToList()); }

                    if (entry.Attributes["subClassOf"] != null)
                    {
                        string superior = entry.Attributes["subClassOf"][0].ToString();

                        AddSubClass(name, superior, true);
                    }

                    foreach (string relative in aux)
                    { AddSubClass(name, relative); }

                    if (ForestBase.RightsGuids.AddSafe<string, string>(oid, name))
                    { ForestBase.CurrentExtendedRightsNode.Add(new XElement("GUID" + oid.ToUpperInvariant(), name)); }

                    foreach (string attributename in mustmay)
                    { AssociateAttributesToClass(attributename, name); }
                }
            }

            WalkClasses();

        }

        private void StoreExtendedRights(List<SearchResultEntry> extendedRights)
        {
            foreach (SearchResultEntry entry in extendedRights)
            {
                if (ForestBase.RightsGuids.AddSafe<string, string>(entry.Attributes["rightsGuid"][0].ToString().ToUpperInvariant(), entry.Attributes["displayName"][0].ToString()))
                {
                    ForestBase.CurrentExtendedRightsNode.Add(new XElement("GUID" + entry.Attributes["rightsGuid"][0].ToString().ToUpperInvariant(),
                                                             entry.Attributes["displayName"][0].ToString()));
                }

                else
                {
                    ForestBase.RightsGuids[entry.Attributes["rightsGuid"][0].ToString()] = ForestBase.RightsGuids[entry.Attributes["rightsGuid"][0].ToString()] + "/" + entry.Attributes["displayName"][0].ToString();

                    ForestBase.CurrentExtendedRightsNode.Descendants("GUID" + entry.Attributes["rightsGuid"][0].ToString().ToUpperInvariant()).FirstOrDefault().Value = ForestBase.RightsGuids[entry.Attributes["rightsGuid"][0].ToString()];
                }
            }
        }

        private void HandleMustMay(string value, string className)
        {
            if (value.Contains("MUST ("))
            {
                string temp = value.Substring(value.IndexOf("MUST (") + 6);
                temp = temp.Substring(0, temp.IndexOf(")") - 1);

                foreach (string attributename in temp.Split(new string[] { " $ " }, StringSplitOptions.RemoveEmptyEntries))
                { AssociateAttributesToClass(attributename, className); }
            }

            if (value.Contains("MAY ("))
            {
                string temp = value.Substring(value.IndexOf("MAY (") + 5);
                temp = temp.Substring(0, temp.IndexOf(")") - 1);

                foreach (string attributename in temp.Split(new string[] { " $ " }, StringSplitOptions.RemoveEmptyEntries))
                { AssociateAttributesToClass(attributename, className); }
            }
        }

        private void AssociateAttributesToClass(string attributeName, string className)
        { ForestBase.CurrentClassesNode.Descendants(className).FirstOrDefault().Add(new XElement("Attribute", attributeName)); }

        private void AddClass(string name)
        {
            // to be repaired in WS lab - remove NOT operator!
            // #####################################

            if (ForestBase.ClassCache.AddSafe<string, string>(name, name))
            { ForestBase.CurrentClassesNode.Add(new XElement(name, new XAttribute("value", name))); }
        }

        private void AddSubClass(string name, string relativeName, bool isSuperior = false)
        {
            AddClass(name);
            AddClass(relativeName);

            if (isSuperior)
            { ForestBase.CurrentClassesNode.Descendants(name).FirstOrDefault().Add(new XElement("Superior", relativeName)); }

            else
            { ForestBase.CurrentClassesNode.Descendants(name).FirstOrDefault().Add(new XElement("SubClass", relativeName)); }
        }

        private void WalkClasses()
        {
            List<string> attribs;
            List<string> temp;

            foreach (XElement classnode in ForestBase.CurrentClassesNode.Elements())
            {
                attribs = new List<string>();

                attribs.AddRange(classnode.Elements("Attribute").Select(a => a.Value).ToList());

                XElement supnode = classnode.Elements("Superior").FirstOrDefault();

                if (supnode != null)
                {
                    temp = GetSuperiorAttributes(supnode.Value);

                    foreach (string attrib in temp)
                    {
                        if (!attribs.Contains(attrib))
                        {
                            attribs.Add(attrib);

                            classnode.Add(new XElement("Attribute", attrib));
                        }
                    }
                }

                attribs.Sort();

                foreach (XElement snode in classnode.Elements("SubClass"))
                {
                    temp = GetSubClassAttributes(snode.Value);

                    foreach (string attrib in temp)
                    {
                        if (!attribs.Contains(attrib))
                        {
                            attribs.Add(attrib);

                            classnode.Add(new XElement("Attribute", attrib));
                        }
                    }
                }
            }
        }

        private List<string> GetSuperiorAttributes(string supName)
        {
            List<string> ret = new List<string>();

            XElement supnode = ForestBase.CurrentClassesNode.Elements(supName).FirstOrDefault();

            ret.AddRange(supnode.Elements("Attribute").Select(a => a.Value).ToList());

            XElement supsupnode = supnode.Elements("Superior").FirstOrDefault();

            if (supsupnode != null)
            {
                if (supName != supsupnode.Value)
                { ret.AddRange(GetSuperiorAttributes(supsupnode.Value)); }
            }

            return ret;
        }

        private List<string> GetSubClassAttributes(string subName)
        {
            List<string> ret = new List<string>();

            XElement supnode = ForestBase.CurrentClassesNode.Elements(subName).FirstOrDefault();

            ret.AddRange(supnode.Elements("Attribute").Select(a => a.Value).ToList());

            foreach (XElement snode in supnode.Elements("SubClass"))
            { ret.AddRange(GetSubClassAttributes(snode.Value)); }

            return ret;
        }

        private void BuildSyntaxDecoder()
        {
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.7", ActiveDirectorySyntax.Bool);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.27", ActiveDirectorySyntax.Int);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.906", ActiveDirectorySyntax.Int64);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.2", ActiveDirectorySyntax.AccessPointDN);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.903", ActiveDirectorySyntax.DNWithBinary);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.904", ActiveDirectorySyntax.DNWithString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.12", ActiveDirectorySyntax.DN);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.1221", ActiveDirectorySyntax.ORName);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.43", ActiveDirectorySyntax.PresentationAddress);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.1362", ActiveDirectorySyntax.CaseExactString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.24", ActiveDirectorySyntax.GeneralizedTime);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.26", ActiveDirectorySyntax.IA5String);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.907", ActiveDirectorySyntax.SecurityDescriptor);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.36", ActiveDirectorySyntax.NumericString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.38", ActiveDirectorySyntax.Oid);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.40", ActiveDirectorySyntax.OctetString);
            ForestBase.AggregateSyntaxes.AddSafe("OctetString", ActiveDirectorySyntax.OctetString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.44", ActiveDirectorySyntax.PrintableString);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.905", ActiveDirectorySyntax.CaseIgnoreString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.15", ActiveDirectorySyntax.DirectoryString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.53", ActiveDirectorySyntax.UtcTime);

            #region "decoding info"
            /*
                Boolean				 			1.3.6.1.4.1.1466.115.121.1.7
                Enumeration			 			1.3.6.1.4.1.1466.115.121.1.27
                Integer				 			1.3.6.1.4.1.1466.115.121.1.27
                LargeInteger		 			1.2.840.113556.1.4.906
                Object(Access-Point) 			1.3.6.1.4.1.1466.115.121.1.2
                Object(DN-Binary)				1.2.840.113556.1.4.903
                Object(DN-String)				1.2.840.113556.1.4.904
                Object(DS-DN)					1.3.6.1.4.1.1466.115.121.1.12
                Object(OR-Name)					1.2.840.113556.1.4.1221
                Object(Presentation-Address)	1.3.6.1.4.1.1466.115.121.1.43
                Object(Replica-Link)			OctetString
                String(Case)					1.2.840.113556.1.4.1362
                String(Generalized-Time)		1.3.6.1.4.1.1466.115.121.1.24
                String(IA5)						1.3.6.1.4.1.1466.115.121.1.26
                String(NT-Sec-Desc)				1.2.840.113556.1.4.907
                String(Numeric)					1.3.6.1.4.1.1466.115.121.1.36
                String(Object-Identifier)		1.3.6.1.4.1.1466.115.121.1.38
                String(Octet)					1.3.6.1.4.1.1466.115.121.1.40
                String(Printable)				1.3.6.1.4.1.1466.115.121.1.44
                String(Sid)						1.3.6.1.4.1.1466.115.121.1.40
                String(Teletex)					1.2.840.113556.1.4.905
                String(Unicode)					1.3.6.1.4.1.1466.115.121.1.15
                String(UTC-Time)				1.3.6.1.4.1.1466.115.121.1.53
             */
            #endregion
        }

        #endregion

        #region load from cache

        private bool GetCache(string forestName, Int32 updateVer)
        {
            bool ret = false;

            if (ForestBase.XMLCache != null)
            { ret = ReadCache(forestName, updateVer); }

            else
            {
                if (!File.Exists(ForestBase.XMLName))
                { CreateCache(forestName, true, updateVer); }

                else
                {
                    if (LoadCache(forestName))
                    { ret = ReadCache(forestName, updateVer); }

                    else
                    { CreateCache(forestName, (ForestBase.XMLCache == null), updateVer); }
                }
            }

            return ret;
        }

        private bool GetCacheOld(string forestName, DateTime modifyTimeStamp)
        {
            bool ret = false;

            if (ForestBase.XMLCache != null)
            { ret = ReadCacheOld(forestName, modifyTimeStamp); }

            else
            {
                if (!File.Exists(ForestBase.XMLName))
                { CreateCacheOld(forestName, true, modifyTimeStamp); }

                else
                {
                    if (LoadCache(forestName))
                    { ret = ReadCacheOld(forestName, modifyTimeStamp); }

                    else
                    { CreateCacheOld(forestName, (ForestBase.XMLCache == null), modifyTimeStamp); }
                }
            }

            return ret;
        }

        private bool LoadCache(string forestName)
        {
            bool ret = true;

            bool delete = false;

            try
            {
                using (StreamReader reader = File.OpenText(ForestBase.XMLName))
                {
                    XDocument cache = XDocument.Load(reader);

                    XElement version = cache.Root.Elements().Where(e => e.Name == "CacheVersion").FirstOrDefault();

                    if (version != null)
                    {
                        if (version.Value != CacheVersion)
                        {
                            delete = true;

                            ret = false;
                        }
                    }

                    if (ret)
                    { ForestBase.XMLCache = cache; }
                }
            }

            catch { }

            try
            {
                if (delete)
                { File.Delete(ForestBase.XMLName); }
            }

            catch { };

            return ret;
        }

        private bool ReadCache(string forestName, Int32 updateVer)
        {
            bool ret = false;

            if (ForestBase.CurrentForestName == forestName.ToLowerInvariant())
            { return true; }

            if (ForestBase.LoadedForests.ContainsKey(forestName))
            {
                ForestBase.CurrentForestNode = ForestBase.LoadedForests[forestName];

                NewForest(forestName);

                return true;
            }

            else
            {
                ForestBase.CurrentForestNode = ForestBase.XMLCache.Root.Descendants(forestName.ToLowerInvariant()).FirstOrDefault();

                if (ForestBase.CurrentForestNode != null)
                {
                    NewForest(forestName);

                    return true;
                }
            }

            CreateCache(forestName, false, updateVer);

            NewForest(forestName);

            return ret;
        }
        
        private bool ReadCacheOld(string forestName, DateTime modifyTimeStamp)
        {
            bool ret = false;

            if (ForestBase.CurrentForestName == forestName.ToLowerInvariant())
            { return true; }

            if (ForestBase.LoadedForests.ContainsKey(forestName))
            {
                ForestBase.CurrentForestNode = ForestBase.LoadedForests[forestName];

                NewForest(forestName);

                return true;
            }

            else
            {
                ForestBase.CurrentForestNode = ForestBase.XMLCache.Root.Descendants(forestName.ToLowerInvariant()).FirstOrDefault();

                if (ForestBase.CurrentForestNode != null)
                {
                    NewForest(forestName);

                    return true;
                }
            }

            CreateCacheOld(forestName, false, modifyTimeStamp);

            NewForest(forestName);

            return ret;
        }

        private void CreateCache(string forestName, bool createFile, Int32 updateVer)
        {
            if (createFile)
            { ForestBase.XMLCache = XDocument.Parse(String.Format(@"<{0}></{0}>", ForestBase.RootName)); }

            ForestBase.XMLCache.Root.Add(new XElement("CacheVersion", CacheVersion));

            ForestBase.CurrentForestNode = new XElement(forestName.ToLowerInvariant(), new XAttribute("UpdateVersion", updateVer.ToString()));

            ForestBase.CurrentClassesNode = new XElement("Classes", new XElement("All", new XAttribute("value", "*")));

            ForestBase.CurrentForestNode.Add(ForestBase.CurrentClassesNode);

            ForestBase.CurrentAttributesNode = new XElement("Attributes");

            ForestBase.CurrentForestNode.Add(ForestBase.CurrentAttributesNode);

            ForestBase.CurrentExtendedRightsNode = new XElement("ExtendedRights");

            ForestBase.CurrentForestNode.Add(ForestBase.CurrentExtendedRightsNode);

            ForestBase.XMLCache.Root.Add(ForestBase.CurrentForestNode);

            ForestBase.LoadedForests.Add(forestName, ForestBase.CurrentForestNode);

            NewForest(forestName);

            ForestBase.SchemaCacheIsDirty = true;
        }


        private void CreateCacheOld(string forestName, bool createFile, DateTime modifyTimeStamp)
        {
            if (createFile)
            { ForestBase.XMLCache = XDocument.Parse(String.Format(@"<{0}></{0}>", ForestBase.RootName)); }

            ForestBase.XMLCache.Root.Add(new XElement("CacheVersion", CacheVersion));

            ForestBase.CurrentForestNode = new XElement(forestName.ToLowerInvariant(), new XAttribute("modifyTimeStamp", modifyTimeStamp.ToString()));

            ForestBase.CurrentClassesNode = new XElement("Classes", new XElement("All", new XAttribute("value", "*")));

            ForestBase.CurrentForestNode.Add(ForestBase.CurrentClassesNode);

            ForestBase.CurrentAttributesNode = new XElement("Attributes");

            ForestBase.CurrentForestNode.Add(ForestBase.CurrentAttributesNode);

            ForestBase.CurrentExtendedRightsNode = new XElement("ExtendedRights");

            ForestBase.CurrentForestNode.Add(ForestBase.CurrentExtendedRightsNode);

            ForestBase.XMLCache.Root.Add(ForestBase.CurrentForestNode);

            ForestBase.LoadedForests.Add(forestName, ForestBase.CurrentForestNode);

            NewForest(forestName);

            ForestBase.SchemaCacheIsDirty = true;
        }

        private void NewForest(string forestName)
        {
            ForestBase.CurrentForestName = forestName.ToLowerInvariant();

            ForestBase.CurrentClassesNode = ForestBase.CurrentForestNode.Descendants("Classes").FirstOrDefault();

            ForestBase.CurrentAttributesNode = ForestBase.CurrentForestNode.Descendants("Attributes").FirstOrDefault();

            ForestBase.CurrentExtendedRightsNode = ForestBase.CurrentForestNode.Descendants("ExtendedRights").FirstOrDefault();

            ForestBase.LoadedAttributes.Clear();
        }

        public void SaveCache()
        {
            if (!ForestBase.SchemaCacheIsDirty) return;

            try
            {
                if (!Directory.Exists(new FileInfo(ForestBase.XMLName).DirectoryName))
                { Directory.CreateDirectory(new FileInfo(ForestBase.XMLName).DirectoryName); }

                ForestBase.XMLCache.Save(ForestBase.XMLName);
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        internal void LoadSchemaFomCache()
        {
            LoadAttributesFromCache();

            LoadRightsGuidsFromCache();

            LoadClassesFromCache();

            //SchemaStore sch = new SchemaStore();



            //sch.AttributeCache = new CustomDictionary<string, AttributeSchema>(ForestBase.AttributeCache);
            //sch.ClassCache = new CustomDictionary<string,string>(ForestBase.ClassCache);
            //sch.RightsGuids = new CustomDictionary<string,string>(ForestBase.RightsGuids);

            //sch.SerializeThis(SchemaBase.XMLName);


        }

        private void LoadAttributesFromCache()
        {
            ForestBase.AttributeCache = new Dictionary<string, AttributeSchema>(StringComparer.InvariantCultureIgnoreCase) { };

            ActiveDirectorySyntax adsyntax;

            string check;

            foreach (XElement attribnode in ForestBase.CurrentAttributesNode.Elements())
            {
                string name = attribnode.Name.LocalName;

                adsyntax = ActiveDirectorySyntax.OctetString;

                string syntax = attribnode.Value;

                check = "0";

                XAttribute acheck = attribnode.Attributes("checked").FirstOrDefault();

                if (acheck != null)
                { check = acheck.Value; }

                bool ischecked = check.ToBool();

                check = "0";

                XAttribute aconstructed = attribnode.Attributes("constructed").FirstOrDefault();

                if (aconstructed != null)
                { check = aconstructed.Value; }

                bool isconstructed = check.ToBool();

                check = "0";

                XAttribute alinked = attribnode.Attributes("linked").FirstOrDefault();

                if (alinked != null)
                { check = alinked.Value; }

                long linkid = 0;

                long.TryParse(check, out linkid);

                bool isbad = false;

                if (syntax == "Unknown")
                { isbad = true; }

                else
                { adsyntax = StringSyntaxToADSynatx(syntax); }

                AttributeSchema aschema = new AttributeSchema(name, adsyntax, isconstructed, linkid) { Checked = ischecked, UnkownSyntax = isbad };

                ForestBase.AttributeCache.AddSafe(name, aschema);
            }
        }

        private void LoadRightsGuidsFromCache()
        {
            ForestBase.RightsGuids = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

            foreach (XElement guidnode in ForestBase.CurrentExtendedRightsNode.Elements())
            {
                string sguid = guidnode.Name.LocalName;

                sguid = sguid.Replace("GUID", null);

                ForestBase.RightsGuids.AddSafe(sguid, guidnode.Value);
            }
        }

        private void LoadClassesFromCache()
        {
            ForestBase.ClassCache = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

            foreach (XElement classnode in ForestBase.CurrentClassesNode.Elements())
            { ForestBase.ClassCache.AddSafe(classnode.Name.LocalName, classnode.Attributes("value").FirstOrDefault().Value); }
        }

        public static List<string> LoadClassInfoFromCache(string className)
        {
            List<string> ret = new List<string>();

            XElement classnode = ForestBase.CurrentClassesNode.Elements(className).FirstOrDefault();

            if (classnode != null)
            { ret.AddRange(classnode.Elements("Attribute").Select(a => a.Value).ToList()); }

            return ret;
        }

        //tbchanged
        public bool AttributeIsInCache(string forestName, string attributeName, out ActiveDirectorySyntax adSyntax, out bool isBad)
        {
            bool ret = false;

            adSyntax = ActiveDirectorySyntax.CaseIgnoreString;
            isBad = false;

            if (GetCache(forestName, 0))
            {
                if (ForestBase.LoadedAttributes.ContainsKey(attributeName))
                {
                    string syntax = ForestBase.LoadedAttributes[attributeName];

                    if (syntax == "UnKnown")
                    {
                        isBad = true;

                        return true;
                    }

                    adSyntax = StringSyntaxToADSynatx(attributeName);

                    return true;
                }

                else
                { ret = ReadSyntaxFromCache(attributeName, out adSyntax, out isBad); }
            }

            return ret;
        }

        public bool AttributeIsInCacheOld(string forestName, string attributeName, out ActiveDirectorySyntax adSyntax, out bool isBad)
        {
            bool ret = false;

            adSyntax = ActiveDirectorySyntax.CaseIgnoreString;
            isBad = false;

            if (GetCacheOld(forestName, new DateTime(DateTime.MinValue.Ticks)))
            {
                if (ForestBase.LoadedAttributes.ContainsKey(attributeName))
                {
                    string syntax = ForestBase.LoadedAttributes[attributeName];

                    if (syntax == "UnKnown")
                    {
                        isBad = true;

                        return true;
                    }

                    adSyntax = StringSyntaxToADSynatx(attributeName);

                    return true;
                }

                else
                { ret = ReadSyntaxFromCache(attributeName, out adSyntax, out isBad); }
            }

            return ret;
        }

        public void AddAttributeToCache(string forestName, string attributeName, ActiveDirectorySyntax adSyntax, bool isBad)
        {
            if (ForestBase.LoadedAttributes.ContainsKey(attributeName))
            { return; }

            return;


            //GetCache(forestName, new DateTime(DateTime.MinValue.Ticks));

            //XElement attribnode = CurrentForestNode.Descendants(attributeName.ToLowerInvariant()).FirstOrDefault();

            //if (attribnode == null)
            //{
            //    attribnode = new XElement(attributeName.ToLowerInvariant());

            //    attribnode.Value = isBad ? "UnKnown" : adSyntax.ToString();

            //    CurrentForestNode.Add(attribnode);

            //    LoadedAttributes.Add(attributeName, isBad ? "UnKnown" : adSyntax.ToString());

            //    SchemaCacheIsDirty = true;
            //}
        }

        public void MarkAttributeAsBad(string forestName, string attributeName)
        {
            return;

            //GetCache(forestName, new DateTime(DateTime.MinValue.Ticks));

            //XElement attribnode = CurrentAttributesNode.Descendants(attributeName.ToLowerInvariant()).FirstOrDefault();

            //if (attribnode != null)
            //{
            //    attribnode.Value = "UnKnown";

            //    if (LoadedAttributes.ContainsKey(attributeName))
            //    { LoadedAttributes[attributeName] = "UnKnown"; }

            //    else
            //    { LoadedAttributes.Add(attributeName, "UnKnown"); }

            //    SchemaCacheIsDirty = true;
            //}

            //else
            //{ AddAttributeToCache(forestName, attributeName, ActiveDirectorySyntax.CaseIgnoreString, true); }
        }

        private bool ReadSyntaxFromCache(string attributeName, out ActiveDirectorySyntax adSyntax, out bool isBad)
        {
            bool ret = false;

            adSyntax = ActiveDirectorySyntax.CaseIgnoreString;
            isBad = false;

            XElement attribnode = ForestBase.CurrentAttributesNode.Descendants(attributeName.ToLowerInvariant()).FirstOrDefault();

            if (attribnode != null)
            {
                string syntax = attribnode.Value;

                if (syntax == "UnKnown")
                {
                    isBad = true;

                    return true;
                }

                try
                {
                    ActiveDirectorySyntax temp = StringSyntaxToADSynatx(syntax);

                    adSyntax = temp;

                    ForestBase.LoadedAttributes.Add(attributeName, syntax);

                    return true;
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            return ret;
        }

        #endregion

        private ActiveDirectorySyntax StringSyntaxToADSynatx(string syntax)
        {
            ActiveDirectorySyntax ret = ActiveDirectorySyntax.CaseIgnoreString; ;

            try
            { ret = (ActiveDirectorySyntax)Enum.Parse(typeof(ActiveDirectorySyntax), syntax, true); }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        private void Reset()
        {
            ForestBase.AttributeCache.InvokeSafe("Clear");

            ForestBase.RightsGuids.InvokeSafe("Clear");

            ForestBase.ClassCache.InvokeSafe("Clear");
        }

        #endregion
    }
}
