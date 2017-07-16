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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class PrivilegeAndPrincipals
    {
        #region fields

        public string Name { get; set; }

        public List<string> SidPrincipals { get; set; }

        public List<SidInfo> Principals { get; set; }

        #endregion

        #region constructors

        public PrivilegeAndPrincipals(string name)
        {
            this.Name = name;

            this.SidPrincipals = new List<string> { };

            this.Principals = new List<SidInfo> { };
        }

        ~PrivilegeAndPrincipals()
        { }

        #endregion

        #region methods

        public void CheckApplicability(ref TokenInformation token)
        {
            if (token.PrivilegesStringList.Contains(Name))
            { return; }

            foreach (string id in this.SidPrincipals)
            {
                if (token.SidList.Contains(id))
                {
                    int pos = token.Privileges.Count + 2;

                    token.Privileges.Add(new PrivilegeInfo(pos, Name));

                    break;
                }
            }
        }

        public override string ToString()
        {
            string ret = this.Name;

            foreach (SidInfo sinfo in this.Principals)
            {
                ret += "\r\n\t" + sinfo.ToString();
            }

            return ret;
        }

        #endregion
    }
}
