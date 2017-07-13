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
using System.Linq;

using System.IO;
using System.Reflection.Emit;
using System.Reflection;
using System.Xml.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DynamicTypeLoader
    {
        #region constants

        private const string MODULE_ID = "DynamicTypeLibrary";
        private const string ROOT_ID = "AttributeTypes";
        private const string ASSOCIATOR_ID = "assoc";
        private const string ENUM_ID = "enum";
        private const string DECODE_ID = "berdecode";
        private const string DICT_ID = "dict";
        private const string NAME_ID = "name";
        private const string VALUE_ID = "value";
        private const string FLAGS_ID = "flags";
        private const string FIELD_ID = "field";
        private const string TAGS_ID = "tags";

        private const string SYSTEMTYPE_ID = "systemtype";
        private const string METHOD_ID = "invokemethod";
        private const string RCTYPE_ID = "invokereturntype";
        private const string ARGUMENTTYPE_ID = "type";
        private const string ARGUMENTREFTYPE_ID = "makereftype";
        private const string CASTMETHOD_ID = "castmethod";
        
        

        #endregion
        
        #region fields

        public Dictionary<string, Type> EnumList = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        public Dictionary<string, DynamicEnum> DynamicEnumList = new Dictionary<string, DynamicEnum>(StringComparer.InvariantCultureIgnoreCase);

        public Dictionary<string, DynamicBerConverter> BerConverterList = new Dictionary<string, DynamicBerConverter>(StringComparer.InvariantCultureIgnoreCase);

        public Dictionary<string, DynamicAttributeAssociator> AssociatorList = new Dictionary<string, DynamicAttributeAssociator>(StringComparer.InvariantCultureIgnoreCase);

        public Dictionary<string, Dictionary<string, string>> DictionaryList = new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

        public bool HasTypes { get { return ((EnumList.Count + BerConverterList.Count) > 0); } }

        private AssemblyBuilder AsmBuilder = null;
        private ModuleBuilder ModBuilder = null;

        public string CachePath ="Cache\\DynamicTypes";

        private string FilePath = ROOT_ID + ".xml";

        #endregion

        #region constructor

        internal DynamicTypeLoader(bool generateTypes = true, bool loadDefault = false)
        { LoadInternal(generateTypes, loadDefault); }

        #endregion

        #region methods

        private void LoadInternal(bool generateTypes, bool loadDefault)
        {
            XDocument dxml = null;

            bool success = false;

            CachePath = GlobalHelper.PathInCurrentDirectory(CachePath, out success);

            FilePath = Path.Combine(CachePath, FilePath);

            if (!loadDefault)
            {    
                if (File.Exists(FilePath))
                { dxml = XDocument.Load(FilePath); }

                else
                {
                    dxml = XDocument.Parse(GetDefaultXML());

                    dxml.Save(FilePath);
                }
            }

            else
            {
                dxml = XDocument.Parse(GetDefaultXML());

                dxml.Save(FilePath);
            }

            XElement xmlroot = dxml.Elements(ROOT_ID).FirstOrDefault();

            foreach (XElement xmlassoc in xmlroot.Elements(ASSOCIATOR_ID))
            { GetAttributeAssociatorFromXML(xmlassoc); }

            foreach (XElement xmlenum in xmlroot.Elements(ENUM_ID))
            {
                DynamicEnum dynenum = GetEnumFromXML(xmlenum);

                if (generateTypes)
                { GenerateEnum(dynenum); }
            }

            foreach (XElement xmlber in xmlroot.Elements(DECODE_ID))
            { GetBerConverterFromXML(xmlber); }

            foreach (XElement xmldict in xmlroot.Elements(DICT_ID))
            { GetDictionaryFromXML(xmldict); }          
        }

        public void GenerateEnum(DynamicEnum dynEnum)
        {
            if (dynEnum.Name.Length == 0) return;

            if (!dynEnum.HasValues) return;

            DynamicEnumList.RemoveSafe<string, DynamicEnum>(dynEnum.Name);

            EnumList.RemoveSafe<string, Type>(dynEnum.Name);

            try
            {
                CheckReflectionTypes();

                EnumBuilder enb = ModBuilder.DefineEnum(dynEnum.Name, TypeAttributes.Public, dynEnum.InheritedType);

                if (dynEnum.SetAsFlags)
                { enb.SetCustomAttribute(new CustomAttributeBuilder(typeof(FlagsAttribute).GetConstructor(Type.EmptyTypes), new object[] { })); }

                foreach (KeyValuePair<string, object> field in dynEnum.Values)
                { enb.DefineLiteral(field.Key, field.Value); }

                Type entype = enb.CreateType();

                dynEnum.IsDirty = false;

                DynamicEnumList.Add(dynEnum.Name, dynEnum);

                EnumList.Add(dynEnum.Name, entype);
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public bool Save()
        {
            bool ret = false;

            XDocument dxml = new XDocument(new XElement(ROOT_ID));

            XElement xmlroot = dxml.Elements(ROOT_ID).FirstOrDefault();

            xmlroot.Add(new XComment("Associators"));

            foreach (DynamicAttributeAssociator assoc in AssociatorList.Values)
            { AddAssociatorToXml(assoc, ref xmlroot); }

            xmlroot.Add(new XComment("BerDecoder"));

            foreach (DynamicBerConverter ber in BerConverterList.Values)
            { AddBerConverterToXML(ber, ref xmlroot); }

            xmlroot.Add(new XComment("Enums"));

            foreach (DynamicEnum dynenum in DynamicEnumList.Values)
            { AddEnumToXML(dynenum, ref xmlroot); }

            xmlroot.Add(new XComment("Dictionaries"));

            foreach (KeyValuePair<string, Dictionary<string, string>> dyndict in DictionaryList)
            { AddDictionaryToXML(dyndict.Value, dyndict.Key, ref xmlroot); }

            HandleBackup();

            dxml.Save(FilePath);            
            
            return ret;
        }

        public Type GetEnumFromDynamicAssociator(string attribName)
        {
            Type ret = null;

            if (AssociatorList.ContainsKey(attribName))
            {
                DynamicAttributeAssociator assoc = AssociatorList[attribName];

                if (assoc.AssociateFrom == ASSOCIATE_FROM.ENUM)
                {  ret = EnumList.GetValueSafe<string, Type>(assoc.EnumName); }
            }

            return ret;
        }

        private void GetAttributeAssociatorFromXML(XElement xmlAssoc)
        {
            DynamicAttributeAssociator dynassoc = new DynamicAttributeAssociator();

            dynassoc.Name = xmlAssoc.Attributes(NAME_ID).FirstOrDefault().Value;

            if (xmlAssoc.Attributes(ENUM_ID).Count() != 0)
            {
                dynassoc.EnumName = xmlAssoc.Attributes(ENUM_ID).FirstOrDefault().Value;

                dynassoc.AssociateFrom = ASSOCIATE_FROM.ENUM;
            }

            else if (xmlAssoc.Attributes(DECODE_ID).Count() != 0)
            {
                dynassoc.BerConverterName = xmlAssoc.Attributes(DECODE_ID).FirstOrDefault().Value;

                dynassoc.AssociateFrom = ASSOCIATE_FROM.BERCONVERTER;
            }

            else if (xmlAssoc.Attributes(DICT_ID).Count() != 0)
            {
                dynassoc.DictionaryName = xmlAssoc.Attributes(DICT_ID).FirstOrDefault().Value;

                dynassoc.AssociateFrom = ASSOCIATE_FROM.DICTIONARY;
            }

            if (dynassoc.Name.Length != 0)
            { AssociatorList.Add(dynassoc.Name, dynassoc); }
        }

        private DynamicEnum GetEnumFromXML(XElement xmlEnum)
        {
            DynamicEnum ret = new DynamicEnum();

            if (xmlEnum.Attributes(NAME_ID).FirstOrDefault() == null) return ret;

            ret.Name = xmlEnum.Attributes(NAME_ID).FirstOrDefault().Value;

            string flags = "false";

            if (xmlEnum.Attributes(FLAGS_ID).FirstOrDefault() != null)
            { flags = xmlEnum.Attributes(FLAGS_ID).FirstOrDefault().Value; }

            bool setasflags = false;

            Boolean.TryParse(flags, out setasflags);

            ret.SetAsFlags = setasflags;

            foreach (XElement xmlfield in xmlEnum.Elements(FIELD_ID))
            {
                if (xmlfield.Attributes(NAME_ID).FirstOrDefault() != null)
                {
                    string name = xmlfield.Attributes(NAME_ID).FirstOrDefault().Value;

                    if (xmlfield.Attributes(VALUE_ID).FirstOrDefault() != null)
                    {
                        string val = xmlfield.Attributes(VALUE_ID).FirstOrDefault().Value;

                        ret.AddEnumField(name, val);
                    }
                }
            }

            return ret;
        }

        private void GetBerConverterFromXML(XElement xmlBer)
        {
            DynamicBerConverter dynber = new DynamicBerConverter();

            if (xmlBer.Attributes(NAME_ID).FirstOrDefault() == null) return;

            dynber.Name = xmlBer.Attributes(NAME_ID).FirstOrDefault().Value;

            string tags = "false"; ;

            if (xmlBer.Attributes(TAGS_ID).FirstOrDefault() != null)
            { tags = xmlBer.Attributes(TAGS_ID).FirstOrDefault().Value; }

            bool incltags = false;

            Boolean.TryParse(tags, out incltags);
            
            dynber.IncludeTags = incltags; 

            if (BerConverterList.ContainsKey(dynber.Name))
            { dynber = BerConverterList[dynber.Name]; }

            dynber.SetConversionRule(xmlBer.Attributes(VALUE_ID).FirstOrDefault().Value);

            foreach (XElement xmlfield in xmlBer.Elements(FIELD_ID))
            {
                if (xmlfield.Attributes(NAME_ID).FirstOrDefault() != null)
                {
                    string name = xmlfield.Attributes(NAME_ID).FirstOrDefault().Value;

                    dynber.AddFieldName(name);
                }
            }

            BerConverterList.AddSafe<string, DynamicBerConverter>(dynber.Name, dynber);
        }

        private void GetDictionaryFromXML(XElement xmlDict)
        {
            if (xmlDict.Attributes(NAME_ID).FirstOrDefault() == null) return;

            Dictionary<string, string> dyndict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            foreach (XElement xmlfield in xmlDict.Elements(FIELD_ID))
            {
                if (xmlfield.Attributes(NAME_ID).FirstOrDefault() != null)
                {
                    string name = xmlfield.Attributes(NAME_ID).FirstOrDefault().Value;

                    string val = xmlfield.Attributes(VALUE_ID).FirstOrDefault().Value;

                    dyndict.Add(name, val);
                }
            }

            DictionaryList.Add(xmlDict.Attributes(NAME_ID).FirstOrDefault().Value, dyndict);

            dyndict = new Dictionary<string, string>(); dyndict = null;
        }

        private void CheckReflectionTypes()
        {
            if (AsmBuilder == null)
            { AsmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(MODULE_ID), AssemblyBuilderAccess.RunAndSave); }

            if (ModBuilder == null)
            { ModBuilder = AsmBuilder.DefineDynamicModule(MODULE_ID, String.Format("{0}.dll", MODULE_ID)); }
        }

        private void AddAssociatorToXml(DynamicAttributeAssociator assoc, ref XElement xmlRoot)
        {
            bool goon = true;

            XAttribute xmltype = null;

            switch (assoc.AssociateFrom)
            {
                case ASSOCIATE_FROM.BERCONVERTER:

                    xmltype = new XAttribute(DECODE_ID, assoc.BerConverterName);
                    break;

                case ASSOCIATE_FROM.ENUM:

                    xmltype = new XAttribute(ENUM_ID, assoc.EnumName);
                    break;

                case ASSOCIATE_FROM.DICTIONARY:

                    xmltype = new XAttribute(DICT_ID, assoc.DictionaryName);
                    break;

                //case ASSOCIATE_FROM.TYPE:

                //    xmltype = new XAttribute(SYSTEMTYPE_ID, assoc.TypeName);
                //    break;

                default:

                    goon = false;
                    break;
            }

            if (goon)
            { 
                XElement xmlassoc = new XElement(ASSOCIATOR_ID, new object[] { new XAttribute(NAME_ID, assoc.Name), xmltype });

                xmlRoot.Add(xmlassoc);
            }
        }

        private void AddBerConverterToXML(DynamicBerConverter ber, ref XElement xmlRoot)
        {
            foreach (int pos in ber.ConversionRules.Keys)
            {
                XElement xmlber = new XElement(DECODE_ID);

                xmlber.Add(new XAttribute(NAME_ID, ber.Name));

                xmlber.Add(new XAttribute(TAGS_ID, ber.IncludeTags));

                string rule = ber.ConversionRules[pos];
                
                rule = rule.Replace("{", String.Empty);
                rule = rule.Replace("}", String.Empty);

                xmlber.Add(new XAttribute(VALUE_ID, rule));

                foreach (string field in ber.FieldNames[pos].Values)
                { xmlber.Add(new XElement(FIELD_ID, new XAttribute(NAME_ID, field))); }

                xmlRoot.Add(xmlber);
            }            
        }

        private void AddEnumToXML(DynamicEnum dynEnum, ref XElement xmlRoot)
        {
            if (dynEnum.IsDirty)
            { GenerateEnum(dynEnum); }

            XElement xmlenum = new XElement(ENUM_ID);

            xmlenum.Add(new XAttribute(NAME_ID, dynEnum.Name));

            xmlenum.Add(new XAttribute(FLAGS_ID, dynEnum.SetAsFlags));

            foreach (KeyValuePair<string, string> field in dynEnum.StringValues)
            {
                XElement xmlfield = new XElement(FIELD_ID);

                xmlfield.Add(new XAttribute(NAME_ID, field.Key));

                xmlfield.Add(new XAttribute(VALUE_ID, field.Value));

                xmlenum.Add(xmlfield);
            }

            xmlRoot.Add(xmlenum);
        }

        private void AddDictionaryToXML(Dictionary<string, string> dynDict, string dictName, ref XElement xmlRoot)
        {
            XElement xmldict = new XElement(DICT_ID);

            xmldict.Add(new XAttribute(NAME_ID, dictName));

            foreach (KeyValuePair<string, string> field in dynDict)
            {
                XElement xmlfield = new XElement(FIELD_ID);

                xmlfield.Add(new XAttribute(NAME_ID, field.Key));

                xmlfield.Add(new XAttribute(VALUE_ID, field.Value));

                xmldict.Add(xmlfield);
            }

            xmlRoot.Add(xmldict);
        }

        private void HandleBackup()
        {
            if (!Directory.Exists(CachePath))
            { Directory.CreateDirectory(CachePath); }

            int prev = 1;

            for (int cnt = 2; cnt >= 0; cnt--)
            {
                if (File.Exists(FilePath + ".bkp" + cnt))
                { File.Delete(FilePath + ".bkp"+ cnt); }

                if (File.Exists(FilePath + ".bkp" + prev))
                { File.Move(FilePath + ".bkp" + prev, FilePath + ".bkp" + cnt); }

                prev--;
            }

            if (File.Exists(FilePath))
            { File.Move(FilePath, FilePath + ".bkp0"); }
        }

        public void LoadBackup(string path)
        {
            File.Copy(path, FilePath + ".restore", true);

            HandleBackup();

            File.Move(FilePath + ".restore", FilePath);
        }

        private void GetSystemTypeFromXML()
        {
            /* tbd
               <!--<systemtype name="">
                <invokemethod name="">
                  <invokeargs name="" type="" makereftype="" value="" />
                <invokemethod name=""ToString"" />
                <invokereturntype name=""System.Collections.Generic.List`1[System.String]"" />   
                <castmethod name="" />
              </systemtype>-->
             *   <assoc name=""userparameters"" systemtype=""WtsUserConfig"">
             */
        }

        private void LoadAssemblyTypes()
        {
            /* tbd
             Assembly[] x = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> y = new List<Type>();

            foreach (Assembly z in x)
            { 
                y.AddRange(z.GetTypes());
            }

            y.OrderBy(c => c.Namespace).ThenBy(c => c.Name).ToList();
            */
        }

        private string GetDefaultXML()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<AttributeTypes>
  <!--Associators-->
  <assoc name=""samaccounttype"" enum=""ADS_SAMACCOUNT_TYPE"" />
  <assoc name=""grouptype"" enum=""ADS_GROUP_TYPE"" />
  <assoc name=""useraccountcontrol"" enum=""ADS_USER_FLAG"" />
  <assoc name=""systemflags"" enum=""ADS_SYSTEMFLAG"" />
  <assoc name=""searchflags"" enum=""ADS_SEARCHFLAG"" />
  <assoc name=""schemaflagsex"" enum=""ADS_SEARCHFLAGEX"" />
  <assoc name=""userParameter"" berdecode=""UserParameters"" />
  <assoc name=""StatsData"" berdecode=""StatsData"" />
  <assoc name=""supportedControl"" dict=""SUPPORTED_CONTROL"" />
  <assoc name=""supportedCapabilities"" dict=""SUPPORTED_CAPABILITIES"" />
  <assoc name=""domainFunctionality"" enum=""FUNCTIONAL_LEVEL"" />
  <assoc name=""forestFunctionality"" enum=""FUNCTIONAL_LEVEL"" />
  <assoc name=""domainControllerFunctionality"" enum=""FUNCTIONAL_LEVEL"" />
  <assoc name=""msds-behavior-version"" enum=""DS_BEHAVIOR"" />
  <assoc name=""msds-supportedencryptiontypes"" enum=""SUPPORTED_ENCRYPTION_TYPES"" />
  <assoc name=""trustattributes"" enum=""TRUST_ATTRIBUTES"" />
  <assoc name=""trustdirection"" enum=""TRUST_DIRECTION"" />
  <assoc name=""trusttype"" enum=""TRUST_TYPE"" />
  <assoc name=""instancetype"" enum=""INSTANCE_TYPE"" />
  <assoc name=""msDS-User-Account-Control-Computed"" enum=""ADS_USER_FLAG"" />
  <!--BerDedoder-->
  <berdecode name=""StatsData"" value=""iiiiiiiiiaiaiiiiiiiiiiiiii""  tags=""true"">
    <field name=""threadCount"" />
    <field name=""callTime"" />
    <field name=""entriesReturned"" />
    <field name=""entriesVisited"" />
    <field name=""filter"" />
    <field name=""index"" />
    <field name=""pagesReferenced"" />
    <field name=""pagesRead"" />
    <field name=""pagesPreread"" />
    <field name=""pagesDirtied"" />
    <field name=""pagesRedirtied"" />
    <field name=""logRecordCount"" />
    <field name=""logRecordBytes"" />
  </berdecode>
  <berdecode name=""StatsData"" value=""iiiiiiiiiaia""  tags=""true"">>
    <field name=""threadCount"" />
    <field name=""callTime"" />
    <field name=""entriesReturned"" />
    <field name=""entriesVisited"" />
    <field name=""filter"" />
    <field name=""index"" />
  </berdecode>
  <!--Enums-->
  <enum name=""ADS_GROUP_TYPE"" flags=""true"">
    <field name=""BUILTIN_GROUP"" value=""0x00000001"" />
    <field name=""GLOBAL_GROUP"" value=""0x00000002"" />
    <field name=""DOMAIN_LOCAL_GROUP"" value=""0x00000004"" />
    <field name=""UNIVERSAL_GROUP"" value=""0x00000008"" />
    <field name=""SECURITY_ENABLED"" value=""-0x80000000"" />
  </enum>
  <enum name=""ADS_USER_FLAG"" flags=""true"">
    <field name=""SCRIPT"" value=""0x00000001"" />
    <field name=""ACCOUNTDISABLE"" value=""0x00000002"" />
    <field name=""HOMEDIR_REQUIRED"" value=""0x00000008"" />
    <field name=""LOCKOUT"" value=""0x00000010"" />
    <field name=""PASSWD_NOTREQD"" value=""0x00000020"" />
    <field name=""PASSWD_CANT_CHANGE"" value=""0x00000040"" />
    <field name=""ENCRYPTED_PASSWORD_ALLOWED"" value=""0x00000080"" />
    <field name=""TEMP_DUPLICATE_ACCOUNT"" value=""0x00000100"" />
    <field name=""NORMAL_ACCOUNT"" value=""0x00000200"" />
    <field name=""INTERDOMAIN_TRUST_ACCOUNT"" value=""0x00000800"" />
    <field name=""WORKSTATION_TRUST_ACCOUNT"" value=""0x00001000"" />;
    <field name=""SERVER_TRUST_ACCOUNT"" value=""0x00002000"" />
    <field name=""DONT_EXPIRE_PASSWD"" value=""0x00010000"" />
    <field name=""MNS_LOGON_ACCOUNT"" value=""0x00020000"" />
    <field name=""SMARTCARD_REQUIRED"" value=""0x00040000"" />
    <field name=""TRUSTED_FOR_DELEGATION"" value=""0x00080000"" />
    <field name=""NOT_DELEGATED"" value=""0x00100000"" />
    <field name=""USE_DES_KEY_ONLY"" value=""0x00200000"" />
    <field name=""DONT_REQUIRE_PREAUTH"" value=""0x00400000"" />
    <field name=""PASSWORD_EXPIRED"" value=""0x00800000"" />
    <field name=""TRUSTED_AUTH_FOR_DELEGATION"" value=""0x01000000"" />
  </enum>
  <enum name=""ADS_SAMACCOUNT_TYPE"" flags=""true"">
    <field name=""DOMAIN_OBJECT"" value=""0x0"" />
    <field name=""GROUP_OBJECT"" value=""0x10000000"" />
    <field name=""NON_SECURITY_GROUP_OBJECT"" value=""0x10000001"" />
    <field name=""ALIAS_OBJECT"" value=""0x20000000"" />
    <field name=""NON_SECURITY_ALIAS_OBJECT"" value=""0x20000001"" />
    <field name=""USER_OBJECT"" value=""0x30000000"" />
    <field name=""MACHINE_ACCOUNT"" value=""0x30000001"" />
    <field name=""TRUST_ACCOUNT"" value=""0x30000002"" />
    <field name=""APP_BASIC_GROUP"" value=""0x40000000"" />
    <field name=""APP_QUERY_GROUP"" value=""0x40000001"" />
    <field name=""ACCOUNT_TYPE_MAX"" value=""0x7fffffff"" />
  </enum>
  <enum name=""ADS_SYSTEMFLAG"" flags=""true"">
    <field name=""CR_NTDS_NC"" value=""0x00000001"" />
    <field name=""CR_NTDS_DOMAIN"" value=""0x00000002"" />
    <field name=""ATTR_IS_CONSTRUCTED"" value=""0x00000004"" />
    <field name=""SCHEMA_BASE_OBJECT"" value=""0x00000010"" />
    <field name=""DISALLOW_MOVE_ON_DELETE"" value=""0x02000000"" />
    <field name=""DISALLOW_MOVE"" value=""0x04000000"" />
    <field name=""DISALLOW_RENAME"" value=""0x08000000"" />
    <field name=""CONFIG_ALLOW_LIMITED_MOVE"" value=""0x10000000"" />
    <field name=""CONFIG_ALLOW_MOVE"" value=""0x20000000"" />
    <field name=""CONFIG_ALLOW_RENAME"" value=""0x40000000"" />
    <field name=""DISALLOW_DELETE"" value=""0x80000000"" />
  </enum>
  <enum name=""ADS_SEARCHFLAG"" flags=""true"">
    <field name=""INDEX_ATTRIBUTE"" value=""0x00000001"" />
    <field name=""CONTAINER_INDEX_ATTRIBUTE"" value=""0x00000002"" />
    <field name=""ANR_INDEX_ATTRIBUTE"" value=""0x00000004"" />
    <field name=""PRESERVE_ON_TOMBSTONE_ATTRIBUTE"" value=""0x00000008"" />
    <field name=""COPY_ATTRIBUTE"" value=""0x00000010"" />
    <field name=""TUPLE_INDEX_ATTRIBUTE"" value=""0x00000020"" />
    <field name=""VLV_PERFORMANCE_ATTRIBUTE"" value=""0x00000040"" />
    <field name=""CONFIDENTIAL_ATTRIBUTE"" value=""0x00000080"" />
    <field name=""DISABLE_AUDITING_ATTRIBUTE"" value=""0x00000100"" />
    <field name=""FILTERED_SET_ATTRIBUTE"" value=""0x00000200"" />
  </enum>
  <enum name=""ADS_SEARCHFLAGEX"" flags=""true"">
    <field name=""FLAG_ATTR_IS_CRITICAL"" value=""0x00000001"" />
  </enum>
  <enum name=""FUNCTIONAL_LEVEL"">
    <field name=""W2K_MODE"" value=""0x0"" />
    <field name=""W2K3_INTERIM_MODE"" value=""0x1"" />
    <field name=""W2K3_MODE"" value=""0x2"" />
    <field name=""W2K8_MODE"" value=""0x3"" />
    <field name=""W2K8R2_MODE"" value=""0x4"" />
    <field name=""W2K12_MODE"" value=""0x5"" />
    <field name=""W2K12R2_MODE"" value=""0x6"" />
  </enum>
  <enum name=""DS_BEHAVIOR"">
    <field name=""DS_BEHAVIOR_WIN2000"" value=""0x0"" />
    <field name=""DS_BEHAVIOR_WIN2003_MIXED"" value=""0x1"" />
    <field name=""DS_BEHAVIOR_WIN2003"" value=""0x2"" />
    <field name=""DS_BEHAVIOR_WIN2008"" value=""0x3"" />
    <field name=""DS_BEHAVIOR_WIN2008R2"" value=""0x4"" />
    <field name=""DS_BEHAVIOR_WIN2012"" value=""0x5"" />
    <field name=""DS_BEHAVIOR_WIN2012R2"" value=""0x6"" />
  </enum>
  <enum name=""SUPPORTED_ENCRYPTION_TYPES"" flags=""true"">
    <field name=""DES-CBC-CRC"" value=""0x1"" />
    <field name=""DES-CBC-MD5"" value=""0x2"" />
    <field name=""RC4-HMAC"" value=""0x4"" />
    <field name=""AES128-CTS-HMAC-SHA1-96"" value=""0x8"" />
    <field name=""AES256-CTS-HMAC-SHA1-96"" value=""0x10"" />
    <field name=""FAST_SUPPORTED"" value=""0x10000"" />
    <field name=""COUMPOUND_IDENTITY_SUPPORTED"" value=""0x200000"" />
    <field name=""CLAIMS_SUPPORTED"" value=""0x400000"" />
    <field name=""RESOURCE_SID_COMPRESSION"" value=""0x800000"" />
  </enum>
  <enum name=""TRUST_ATTRIBUTES"" flags=""true"">
    <field name=""NON_TRANSITIVE"" value=""0x1"" />
    <field name=""UPLEVEL_ONLY"" value=""0x2"" />
    <field name=""SID_FILTERING_QUARANTINED_DOMAIN"" value=""0x4"" />
    <field name=""FOREST_TRANSITIVE"" value=""0x8"" />
    <field name=""SELECTIVE_AUTH_CROSS_ORGANIZATION"" value=""0x10"" />
    <field name=""WITHIN_FOREST"" value=""0x20"" />
    <field name=""TREAT_AS_EXTERNAL"" value=""0x40"" />
    <field name=""USES_RC4_ENCRYPTION"" value=""0x80"" />
    <field name=""CROSS_ORGANIZATION_NO_TGT_DELEGATION"" value=""0x200"" />
    <field name=""PIM_TRUST"" value=""0x400"" />
  </enum>
  <enum name=""TRUST_DIRECTION"" flags=""false"">
    <field name=""INBOUND"" value=""0x1"" />
    <field name=""OUTBOUND"" value=""0x2"" />
    <field name=""BIDIRECTIONAL"" value=""0x3"" />
  </enum>
  <enum name=""TRUST_TYPE"" flags=""false"">
    <field name=""DOWNLEVEL"" value=""0x1"" />
    <field name=""UPLEVEL"" value=""0x2"" />
    <field name=""MIT"" value=""0x3"" />
    <field name=""DCE"" value=""0x4"" />
  </enum>
  <enum name=""INSTANCE_TYPE"" flags=""true"">
    <field name=""IS_NC_HEAD"" value=""0x1"" />
    <field name=""NO_INSTANCE_REPLICA"" value=""0x2"" />
    <field name=""WRITABLE"" value=""0x4"" />
    <field name=""NC_ABOVE"" value=""0x8"" />
    <field name=""CONSTRUCTING"" value=""0x10"" />
    <field name=""REMOVING"" value=""0x20"" />
  </enum>
  <!--Dictionaries-->
  <dict name=""SUPPORTED_CONTROL"">
    <field name=""1.2.840.113556.1.4.319"" value =""LDAP_PAGED_RESULT_OID_STRING"" />
    <field name=""1.2.840.113556.1.4.521"" value =""LDAP_SERVER_CROSSDOM_MOVE_TARGET_OID"" />
    <field name=""1.2.840.113556.1.4.841"" value =""LDAP_SERVER_DIRSYNC_OID"" />
    <field name=""1.2.840.113556.1.4.1339"" value =""LDAP_SERVER_DOMAIN_SCOPE_OID"" />
    <field name=""1.2.840.113556.1.4.529"" value =""LDAP_SERVER_EXTENDED_DN_OID"" />
    <field name=""1.2.840.113556.1.4.970"" value =""LDAP_SERVER_GET_STATS_OID"" />
    <field name=""1.2.840.113556.1.4.619"" value =""LDAP_SERVER_LAZY_COMMIT_OID"" />
    <field name=""1.2.840.113556.1.4.1413"" value =""LDAP_SERVER_PERMISSIVE_MODIFY_OID"" />
    <field name=""1.2.840.113556.1.4.528"" value =""LDAP_SERVER_NOTIFICATION_OID"" />
    <field name=""1.2.840.113556.1.4.474"" value =""LDAP_SERVER_RESP_SORT_OID"" />
    <field name=""1.2.840.113556.1.4.801"" value =""LDAP_SERVER_SD_FLAGS_OID"" />
    <field name=""1.2.840.113556.1.4.1340"" value =""LDAP_SERVER_SEARCH_OPTIONS_OID"" />
    <field name=""1.2.840.113556.1.4.473"" value =""LDAP_SERVER_SORT_OID"" />
    <field name=""1.2.840.113556.1.4.417"" value =""LDAP_SERVER_SHOW_DELETED_OID"" />
    <field name=""1.2.840.113556.1.4.805"" value =""LDAP_SERVER_TREE_DELETE_OID"" />
    <field name=""1.2.840.113556.1.4.1338"" value =""LDAP_SERVER_VERIFY_NAME_OID"" />
    <field name=""2.16.840.1.113730.3.4.9"" value =""LDAP_CONTROL_VLVREQUEST"" />
    <field name=""2.16.840.1.113730.3.4.10"" value =""LDAP_CONTROL_VLVRESPONSE"" />
    <field name=""1.2.840.113556.1.4.1504"" value =""LDAP_SERVER_ASQ_OID"" />
    <field name=""1.2.840.113556.1.4.1852"" value =""LDAP_SERVER_QUOTA_CONTROL_OID"" />
    <field name=""1.2.840.113556.1.4.802"" value =""LDAP_SERVER_RANGE_OPTION_OID"" />
    <field name=""1.2.840.113556.1.4.1907"" value =""LDAP_SERVER_SHUTDOWN_NOTIFY_OID"" />
    <field name=""1.2.840.113556.1.4.1974"" value =""LDAP_SERVER_FORCE_UPDATE_OID"" />
    <field name=""1.2.840.113556.1.4.1948"" value =""LDAP_SERVER_RANGE_RETRIEVAL_NOERR_OID"" />
    <field name=""1.2.840.113556.1.4.1341"" value =""LDAP_SERVER_RODC_DCPROMO_OID"" />
    <field name=""1.2.840.113556.1.4.2026"" value =""LDAP_SERVER_DN_INPUT_OID"" />
    <field name=""1.2.840.113556.1.4.2065"" value =""LDAP_SERVER_SHOW_DEACTIVATED_LINK_OID"" />
    <field name=""1.2.840.113556.1.4.2064"" value =""LDAP_SERVER_SHOW_RECYCLED_OID"" />
    <field name=""1.2.840.113556.1.4.2066"" value =""LDAP_SERVER_POLICY_HINTS_DEPRECATED_OID"" />
    <field name=""1.2.840.113556.1.4.2090"" value =""LDAP_SERVER_DIRSYNC_EX_OID"" />
    <field name=""1.2.840.113556.1.4.2205"" value =""LDAP_SERVER_UPDATE_STATS_OID"" />
    <field name=""1.2.840.113556.1.4.2204"" value =""LDAP_SERVER_TREE_DELETE_EX_OID"" />
    <field name=""1.2.840.113556.1.4.2206"" value =""LDAP_SERVER_SEARCH_HINTS_OID"" />
    <field name=""1.2.840.113556.1.4.2211"" value =""LDAP_SERVER_EXPECTED_ENTRY_COUNT_OID"" />
    <field name=""1.2.840.113556.1.4.2239"" value =""LDAP_SERVER_POLICY_HINTS_OID"" />
    <field name=""1.2.840.113556.1.4.2255"" value =""LDAP_SERVER_SET_OWNER_OID"" />
    <field name=""1.2.840.113556.1.4.2256"" value =""LDAP_SERVER_BYPASS_QUOTA_OID"" />
  </dict>
  <dict name=""SUPPORTED_CAPABILITIES"">
    <field name=""1.2.840.113556.1.4.800"" value=""LDAP_CAP_ACTIVE_DIRECTORY_OID"" />
    <field name=""1.2.840.113556.1.4.1670"" value=""LDAP_CAP_ACTIVE_DIRECTORY_V51_OID"" />
    <field name=""1.2.840.113556.1.4.1791"" value=""LDAP_CAP_ACTIVE_DIRECTORY_LDAP_INTEG_OID"" />
    <field name=""1.2.840.113556.1.4.1935"" value=""LDAP_CAP_ACTIVE_DIRECTORY_V61_OID"" />
    <field name=""1.2.840.113556.1.4.2080"" value=""LDAP_CAP_ACTIVE_DIRECTORY_V61_R2_OID"" />
    <field name=""1.2.840.113556.1.4.2237"" value=""LDAP_CAP_ACTIVE_DIRECTORY_W8_OID"" />
    <field name=""1.2.840.113556.1.4.1920"" value=""LDAP_CAP_ACTIVE_DIRECTORY_PARTIAL_SECRETS_OID"" />
    <field name=""1.2.840.113556.1.4.1851"" value=""LDAP_CAP_ACTIVE_DIRECTORY_ADAM_OID"" />
    <field name=""1.2.840.113556.1.4.1880"" value=""LDAP_CAP_ACTIVE_DIRECTORY_ADAM_DIGEST_OID"" />
  </dict>
</AttributeTypes>";
        } 
    
        #endregion
    }
}
