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
using System.Security.Principal;
using System.Xml.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public static class MainBase
    {
        public static DynamicTypeLoader DynamicDll;

        public static SettingsHandler UserSettings;

        public static RootDSEAttributes RootDseOperational;

        #region constructor

        static MainBase() { }

        #endregion
    }

    public class SchemaBase
    {
        #region fields

        private const string innerXMLName = ".\\Cache\\Schema\\XMLName.sch";
        private const string innerCacheVersion = "2";

        private bool Loaded = false;

        public string CacheVersion { get { return innerCacheVersion; } }

        public DateTime CurrentSchemaTimeStamp { get; set; }

        public string CurrentForestName { get; set; }

        public CustomDictionary<string, AttributeSchema> AttributeCache { get; set; }

        public CustomDictionary<string, ClassSchema> ClassCache { get; set; }

        public CustomDictionary<string, string> RightsGuids { get; set; }

        #endregion

        #region constructor

        public SchemaBase(string forestName)
        { LoadInternal(forestName); }

        public SchemaBase()
        { LoadInternal(null); }

        #endregion

        #region methods

        public void LoadInternal(string forestName)
        {
            CurrentForestName = forestName;

            AttributeCache = new CustomDictionary<string, AttributeSchema>();

            ClassCache = new CustomDictionary<string, ClassSchema>();

            RightsGuids = new CustomDictionary<string, string>();
        }

        public static string XMLName(string forestName = null)
        {
            string ret = innerXMLName;

            ret = ret.Replace("XMLName", forestName);

            return ret;
        }

        public bool MustLoadFromAD(DateTime modifiedTimeStamp)
        {
            bool ret = false;

            ret = !((Loaded) && (modifiedTimeStamp < CurrentSchemaTimeStamp));

            return ret;
        }

        public void IsLoaded()
        { Loaded = true; }

        public static SchemaBase LoadFromFile(string forestName)
        {
            SchemaBase ret = new SchemaBase();

            if (XMLName(forestName).DeSerializeFromFile(out ret))
            { ret.IsLoaded(); }

            return ret;
        }

        #endregion
    }

    public static class ForestBase
    {
        #region fields

        #region ForestInfo

        public static DomainControllerHelper DefaultDC { get; set; }

        public static Dictionary<string, List<string>> DomainDCs { get; set; }

        public static Dictionary<string, DomainControllerHelper> DCList { get; set; }

        public static Dictionary<string, List<string>> NCsToDCs { get; set; }

        public static Dictionary<string, string> DomainSids { get; set; }

        public static Dictionary<string, TrustRelationshipInformation> Trusts { get; set;}

        public static Dictionary<string, QueryPolicy> QueryPolicies { get; set; }

        public static Dictionary<string, string> NTDSSettings { get; set; }

        public static string Name { get; set; }

        public static string RootDomainNamingContext { get; set; }

        public static string ConfigurationNamingContext { get; set; }

        public static string SchemaNamingContext { get; set; }

        #endregion

        #region Schema

        public static List<SchemaBase> SchemaStore {get; set;}

        #endregion

        #region SchemaLoader

        public static bool SchemaLoaded = false;
        public static bool AttributesLoaded = false;
        public static bool Loading = false;
        public static int AllClassesCount = 0;
        public static int CurClassesCount = 0;

        #endregion

        #region ADHelper

        public static DynamicTypeLoader ADHelperDynamicDLL = new DynamicTypeLoader();

        public static string ForestName = String.Empty;
        public static string CurrentDomain = String.Empty;
        public static string LastGoodDomain = String.Empty;
        public static string CurrentDC = String.Empty;

        public static TimeSpan TimeOutInterval = new TimeSpan(0, 2, 0);
        public static Ports CurrentPorts = new Ports();

        public static int CurrentTimeOut
        {
            get { return (int)TimeOutInterval.TotalSeconds; }
            set { TimeOutInterval = TimeSpan.FromSeconds((double)value); }
        }

        public static List<string> DomainNames = new List<string> { };

        //public static ActiveDirectorySchema SchemaCache;

        public static StatsData Statistics = null;

        public static Dictionary<SecurityIdentifier, string> SidCache = new Dictionary<SecurityIdentifier, string>();

        public static SearchRequestExtender CurrentRequestExtender;

        //http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx 
        public const string GENERALIZED_TIME_FORMAT = "yyyyMMddHHmmss.0Z";

        public static ReferralCallback RefCallback;

        #endregion

        #region SchemaCache

        public static bool SchemaCacheIsDirty = false;

        public static Dictionary<string, AttributeSchema> AttributeCache = new Dictionary<string, AttributeSchema>(StringComparer.InvariantCultureIgnoreCase) { };
        public static Dictionary<string, string> RightsGuids = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };
        public static Dictionary<string, string> ClassCache = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

        public static Credentials GivenCreds = null;

        public const string NamePattern = @"'[\w?\-?/.?]+'";

        public const string XMLName = ".\\Cache\\Schema\\SchemaCache.xml";
        public const string RootName = "SchemaCache";

        public static XDocument XMLCache = null;

        public static XElement CurrentForestNode = null;
        public static XElement CurrentClassesNode = null;
        public static XElement CurrentAttributesNode = null;
        public static XElement CurrentExtendedRightsNode = null;
        public static DateTime CurrentSchemaTimeStamp;
        public static string CurrentForestName = null;

        public static string GivenForestName = null;


        public static Dictionary<string, XElement> LoadedForests = new Dictionary<string, XElement>(StringComparer.InvariantCultureIgnoreCase) { };
        public static Dictionary<string, string> LoadedAttributes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

        #endregion

        #region AttributeSyntax

        public static Dictionary<string, ActiveDirectorySyntax> AggregateSyntaxes = new Dictionary<string, ActiveDirectorySyntax>(StringComparer.InvariantCultureIgnoreCase) { };

        public static Dictionary<string, Dictionary<int, ActiveDirectorySyntax>> LDAPSyntaxes;

        #endregion

        #endregion

        #region constructor

        static ForestBase() { SchemaStore = new List<SchemaBase> { }; }

        #endregion
    }

}
