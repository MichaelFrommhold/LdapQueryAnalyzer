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

namespace CodingFromTheField.LdapQueryAnalyzer
{
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

}
