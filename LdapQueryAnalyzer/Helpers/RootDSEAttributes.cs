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

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class RootDSEAttributes
    {
        #region fields

        public const string CachePath = "Cache\\Settings";
        public const string FileName = "RootDSE.xml";

        private string[] userOperationalAttributes = new string[] { "approximateHighestInternalObjectID", "dsaVersionString", "dsSchemaAttrCount", 
                                                                        "dsSchemaClassCount", "dsSchemaPrefixCount", "msDS-PortLDAP", 
                                                                        "msDS-PortSSL", "msDS-PrincipalName", "msDS-ReplAllInboundNeighbors", 
                                                                        "msDS-ReplAllOutboundNeighbors", "msDS-ReplConnectionFailures", "msDS-ReplLinkFailures", 
                                                                        "msDS-ReplPendingOps", "msDS-ReplQueueStatistics", "msDS-TopQuotaUsage", 
                                                                        "netlogon", "pendingPropagations", "serviceAccountInfo", 
                                                                        "spnRegistrationResult", "supportedConfigurableSettings", "supportedExtension", 
                                                                        "tokenGroups", "usnAtRifm", "validFSMOs"};

        public List<string> OperationalAttributes
        {
            get { return userOperationalAttributes.ToList(); }
            set { userOperationalAttributes = value.ToArray(); }
        }

        #endregion

        #region constructors

        public RootDSEAttributes() { }

        #endregion

        #region methods

        public static RootDSEAttributes Load()
        {
            RootDSEAttributes ret = new RootDSEAttributes();

            string filepath = GetFilePath();

            string sertext = string.Empty;

            if (!File.Exists(filepath))
            {
                sertext = ret.SerializeThis();

                sertext.WriteToFile(filepath, false);
            }

            else
            {
                filepath.ReadFromFile(out sertext);

                sertext.DeSerializeFromString(out ret);
            }

            return ret;
        }

        private static string GetFilePath()
        {
            string ret = string.Empty;

            bool success;

            ret = GlobalHelper.PathInCurrentDirectory(CachePath, out success);

            ret = Path.Combine(ret, FileName);

            return ret;
        }

        #endregion
    }

}
