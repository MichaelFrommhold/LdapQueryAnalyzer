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
using System.DirectoryServices.Protocols;
using System.Globalization;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class QueryPolicy
    {
        #region fields
        private List<string> limits;

        public string DN { get; set; }

        public DateTime WhenCreated { get; set; }
        
        public int InitRecvTimeout { get; set; }
        public int MaxConnections { get; set; }
        public int MaxConnIdleTime { get; set; }
        public int MaxNotificationPerConnection { get; set; }
        public int MaxPageSize { get; set; }
        public int MaxQueryDuration { get; set; }
        public int MaxTempTableSize { get; set; }
        public int MaxResultSetSize { get; set; }
        public int MaxPoolThreads { get; set; }
        public int MaxDatagramRecv { get; set; }
        public int MaxReceiveBuffer { get; set; }
        public int MaxValRange { get; set; }

        #endregion

        #region constructor

        public QueryPolicy(SearchResultEntry pol)
        {
            DN = pol.DistinguishedName;

            List<string> pnames = this.GetPropertyNames(true);

            DateTime temp;

            string value = "";
            pol.GetStringAttributeSafe("whenCreated", out value);

            if (DateTime.TryParseExact(value, ForestBase.GENERALIZED_TIME_FORMAT, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, out temp))
            { WhenCreated = (temp < DateTime.FromFileTimeUtc(0)) ? DateTime.FromFileTimeUtc(0) : temp; }

            else
            { WhenCreated = DateTime.FromFileTimeUtc(0); }

            pol.GetStringAttributeSafe("lDAPAdminLimits", out limits);        

            foreach (string item in limits)
            {
                string limitname = item.Split(new char[] { '=' })[0].Trim();
                string limitval = item.Split(new char[] { '=' })[1].Trim();

                string prop = pnames.GetMatchingItem(limitname);

                if (prop.Length > 0)
                {
                    int numval = 0;

                    int.TryParse(limitval, out numval);

                    this.SetPropertyValue(prop, numval);
                }
            }
        }

        public List<string> Print(string prefix = "")
        {
            List<string> ret = new List<string> { };
            if (limits != null)
            { ret = limits.Select(v => prefix + v).ToList(); }

            return ret;
        }

        #endregion
    }
}
