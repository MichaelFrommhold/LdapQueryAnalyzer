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
using System.Xml.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SyncBase
    {
        #region constants

        public const string XML_NAME = "run.xml";

        public const string ROOT_ID = "SyncRuns";
        public const string FOREST_ID = "Forest";
        public const string DN_ID = "NamingContext";
        public const string FILTER_ID = "Filter";
        public const string ATTRIBUTES_ID = "Attributes";
        public const string FOLDER_ID = "ID";

        public const string USN_FILE_NAME = "usn.store";

        public const string COOKIE_NAME = "cookie.store";

        #endregion

        #region common

        public List<SyncRun> SyncRuns = new List<SyncRun> { };

        internal Dictionary<string, string> SubFolders;

        #endregion

        #region SyncCollection

        public Dictionary<string, SyncForest> ForestList = new Dictionary<string, SyncForest>(StringComparer.InvariantCultureIgnoreCase);

        internal string SyncRunsPath = "Cache\\SyncRuns";

        #endregion

        #region SyncForest

        public string ForestName;
        public string ForestPath;

        public Dictionary<string, SyncRoot> RootList = new Dictionary<string, SyncRoot>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region SyncRoot

        public string RootPath;
        public string RootName;

        #endregion

        #region SyncRun

        public bool IsFirstRun = false;

        public string RunPath {get; set; }

        public string RunName { get; set; }

        public string EntryPoint { get; set; }

        public string Filter { get; set; }

        public string Attributes { get; set; }

        public byte[] CurrentCookie = null;

        public Dictionary<string, SyncCookieRun> CookieRuns = new Dictionary<string, SyncCookieRun>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region SyncCookie

        public string CookieName;

        public string CookiePath;

        public DateTime CookieDate;

        public byte[] Cookie;

        public Dictionary<string, Int64> UsnVectors = new Dictionary<string, Int64>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region constructor

        internal SyncBase()
        { }

        #endregion

        #region methods

        public XDocument CreateSyncXML()
        {
            XDocument ret = XDocument.Parse(@"<?xml version=""1.0""?>
<Run>
   <Forest>
      <NamingContext>
         <Filter>
            <Attributes>
               <Run/>
            </Attributes>
         </Filter>
      </NamingContext>
   </Forest>
</Run>");

            return ret;
        }

        public XDocument CreateRootXML()
        {
            XDocument ret = XDocument.Parse(@"<?xml version=""1.0""?>
<SyncRuns>
   <Forest>
      <NamingContext>
         <Filter>
            <Attributes>
               <ID/>
            </Attributes>
         </Filter>
      </NamingContext>
   </Forest>
</SyncRuns>");

            return ret;
        }

        #endregion
    }
}