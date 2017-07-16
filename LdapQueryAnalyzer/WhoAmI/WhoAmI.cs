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
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class WhoAmI
    {
        #region fields

        protected bool Loaded { get; set; }

        protected TokenInformation TokenInfo { get; set; }

        protected LocalSam SamInfo { get; set; }

        protected UserRights URAInfoLocal { get; set; }

        protected UserRights URAInfoRemote { get; set; }

        public List<string> Info { get; set; }

        #endregion

        #region contructor

        public WhoAmI()
        { LoadInternal(); }

        #endregion

        #region methods

        protected void LoadInternal()
        {
            Info = new List<string> { };

            //GlobalUserStore.Load();

            Loaded = false;
        }

        public void Load(string targetDC)
        {
            if (!Loaded)
            {
                Info.Clear();

                Info.AddFormatted("WhoAmI: {0}\r\n", GlobalUserStore.Name);

                List<SidInfo> tokengoups = GetTokenGroups(targetDC, GlobalUserStore.SDDLSid);

                SamInfo = new LocalSam();

                if (!SamInfo.IsDC && !SamInfo.Success)
                { 
                    Info.Add("Failed to read local SAM -> eval will not be complete!"); 

                    GlobalEventHandler.RaiseDebugInfoOccured("Failed to read local SAM -> eval will not be complete!");
                }

                string uname = (GlobalUserStore.UPN != null) ? GlobalUserStore.UPN : GlobalUserStore.Name;

                TokenInfo = new TokenInformation(uname);

                if (!TokenInfo.Success)
                {
                    GlobalEventHandler.RaiseDebugInfoOccured("Failed to inspect token -> exiting WhoAmI!");

                    return;
                }
                
                if (SamInfo.IsDC || (!TokenInfo.Sid.StartsWith(SamInfo.MachineSid)))
                {
                    if (tokengoups.Count > 0)
                    { TokenInfo.AddLdapGroups(tokengoups, SamInfo.IsDC); }

                    else
                    {
                        Info.Add("Failed to read token from AD -> eval will not be complete!");

                        GlobalEventHandler.RaiseDebugInfoOccured("Failed to read token from AD -> eval will not be complete!"); 
                    }
                }

                if (!SamInfo.IsDC)
                { TokenInfo.CheckLocalNesting(SamInfo); }

                TokenInformation temptoken = null;

                if (TokenInfo.IsElevated)
                {
                    URAInfoLocal = new UserRights();

                    temptoken = TokenInfo;

                    if (URAInfoLocal.Success)
                    {
                        foreach (PrivilegeAndPrincipals privinfo in URAInfoLocal.Privileges)
                        {
                            privinfo.CheckApplicability(ref temptoken);
                        }
                    }
                    
                    string dcnb = (targetDC.Contains(".")) ? targetDC.Split('.')[0] : targetDC;

                    if (dcnb.ToLowerInvariant() != SamInfo.MachineName.ToLowerInvariant())
                    {                        
                        URAInfoRemote = new UserRights(targetDC);

                        if (URAInfoRemote.Success)
                        {
                            foreach (PrivilegeAndPrincipals privinfo in URAInfoRemote.Privileges)
                            {
                                privinfo.CheckApplicability(ref temptoken);
                            }
                        }
                    }

                    TokenInfo = temptoken;
                }
                              

                if (GlobalUserStore.ShowGroups)
                {
                    Info.Add("\r\ngroups info:\r\n");

                    foreach (SidInfo group in TokenInfo.Groups)
                    { Info.Add(group.ToString()); }
                }


                if (GlobalUserStore.ShowPrivs)
                {
                    Info.Add("\r\nprivileges info:\r\n");

                    foreach (PrivilegeInfo priv in TokenInfo.Privileges)
                    { Info.Add(priv.ToString()); }
                }

                if (GlobalUserStore.ShowURA)
                {
                    Info.AddFormatted("\r\nuser rights assignments on {0}:\r\n", SamInfo.MachineName);

                    if (TokenInfo.IsElevated)
                    {
                        if (URAInfoLocal.Success)
                        {
                            foreach (PrivilegeAndPrincipals privinfo in URAInfoLocal.Privileges)
                            { Info.Add(privinfo.ToString()); }
                        }

                        else
                        { Info.AddRange(URAInfoLocal.Messages); }

                        Info.AddFormatted("\r\nuser rights assignments on {0}:\r\n", targetDC);

                        if (URAInfoRemote != null)
                        {
                            if (URAInfoRemote.Success)
                            {
                                foreach (PrivilegeAndPrincipals privinfo in URAInfoRemote.Privileges)
                                { Info.Add(privinfo.ToString()); }
                            }

                            else
                            { Info.AddRange(URAInfoRemote.Messages); }
                        }

                        else
                        { Info.Add("\tDid not evaluate - local and remote machine are the same"); }
                    }

                    else
                    { Info.Add("\tThread is not elevated - cannot evaluate"); }
                }

                Loaded = true;
            }
        }

        protected List<SidInfo> GetTokenGroups(string targetDC, string sddlSid)
        {
            List<SidInfo> ret = new List<SidInfo> { };

            LDAPHelper ldap = new LDAPHelper();

            List<SearchResultEntry> res = ldap.Query(targetDC,
                                                     String.Format("<SID={0}>", sddlSid),
                                                     "(objectClass=*)",
                                                     new string[] { "tokenGroups", "userPrincipalName" },
                                                     SearchScope.Base,
                                                     ReferralChasingOptions.None,
                                                     new QueryControl() { CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_DECODING },
                                                     returnResults: true);

            if (res.Count > 0)            {

                foreach (byte[] bsid in res[0].Attributes["tokenGroups"].GetValues(typeof(byte[])))
                {
                    SecurityIdentifier sid = new SecurityIdentifier(bsid, 0);

                    SidInfo sinfo = new SidInfo(bsid);

                    ret.Add(sinfo);
                }

                foreach (string upn in res[0].Attributes["userPrincipalName"].GetValues(typeof(string)))
                {
                    GlobalUserStore.UPN = upn;
                }
            }

            else
            {
                Info.AddFormatted("Failed on TokenGroups for {0}", sddlSid);

                GlobalEventHandler.RaiseDebugInfoOccured("Failed on TokenGroups for {0}", sddlSid);
            }

            return ret;
        }
        
        #endregion
    }
}