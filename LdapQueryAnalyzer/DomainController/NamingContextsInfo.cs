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
using System.DirectoryServices.Protocols;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class NamingContextsInfo
    {
        #region internal fields

        protected string defNC;

        protected string confNC;

        protected string rootNC;

        protected string schemaNC;

        protected List<string> allNCs;

        #endregion

        #region public fields

        public bool Success { get; set; }

        public string ErrorString { get; set; }

        public static string[] NCAttributes
        {
            get
            {
                return new String[] { "defaultNamingContext", 
                                      "schemaNamingContext", 
                                      "configurationNamingContext", 
                                      "rootDomainNamingContext", 
                                      "namingContexts" };
            }
        }

        public string DefaultNamingContext
        { get { return defNC; } }

        public string ConfigurationNamingContext
        { get { return confNC; } }

        public string RootDomainNamingContext
        { get { return rootNC; } }

        public string SchemaNamingContext
        { get { return schemaNC; } }

        public List<string> NamingContexts
        {
            get { return allNCs; }
            set { allNCs = value; }
        }

        #endregion

        #region constructor

        public NamingContextsInfo()
        { }

        public NamingContextsInfo(string dcName)
        { LoadNCs(dcName, NCAttributes); }

        #endregion

        #region methods

        private void LoadNCs(string dcName, string[] attributes)
        {
            Success = false;

            try
            {
                LdapDirectoryIdentifier ldapid = new LdapDirectoryIdentifier(dcName, 389, true, true);

                LdapConnection ldapcon = new LdapConnection(ldapid);
                ldapcon.Timeout = new TimeSpan(0, 2, 30);

                SearchRequest request = new SearchRequest("",
                                                          "(objectClass=*)",
                                                          SearchScope.Base,
                                                          attributes);

                SearchResponse response = (SearchResponse)ldapcon.SendRequest(request);

                foreach (SearchResultEntry entry in response.Entries)
                {
                    entry.GetStringAttributeSafe("defaultNamingContext", out defNC);

                    entry.GetStringAttributeSafe("configurationNamingContext", out confNC);

                    entry.GetStringAttributeSafe("schemaNamingContext", out schemaNC);

                    entry.GetStringAttributeSafe("rootDomainNamingContext", out rootNC);

                    entry.GetStringAttributeSafe("namingContexts", out allNCs);
                }

                Success = true;
            }

            catch (Exception ex)
            { ErrorString = ex.Message; }
        }

        #endregion
    }
}
