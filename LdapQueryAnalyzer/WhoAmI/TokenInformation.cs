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
using System.Runtime.InteropServices;
using System.Security.Principal;


namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class TokenInformation
    {
        #region fields

        public List<string> SidList { get; private set; }

        public List<SidInfo> Groups { get; private set; }

        public List<string> PrivilegesStringList { get; private set; }

        public List<PrivilegeInfo> Privileges { get; set; }

        public string CallingIdentity { get; private set; }

        public string CurrentIdentity { get; private set; }

        public string Sid { get; private set; }

        public bool Success { get; private set; }

        public bool IsElevated { get; private set; }

        private IntPtr TokenHandle { get; set; }

        private TOKEN_GROUPS_AND_PRIILEGES TGP { get; set; }

        #endregion

        #region constructor

        public TokenInformation(string userName)
        {
            LoadInternal(userName);

            Marshal.FreeHGlobal(TokenHandle);
        }

        ~TokenInformation()
        { }

        #endregion

        #region methods

        private void LoadInternal(string userName)
        {
            this.SidList = new List<string> { };

            this.Groups = new List<SidInfo> { };

            this.PrivilegesStringList = new List<string> { };

            this.Privileges = new List<PrivilegeInfo> { };

            this.CallingIdentity = GlobalUserStore.Name;

            this.CurrentIdentity = GlobalUserStore.Name;

            this.Sid = GlobalUserStore.SDDLSid;

            this.IsElevated = GlobalUserStore.IsElevated;  

            if (!String.IsNullOrEmpty(userName))
            {
                this.Success = false;

                try
                {
                    using (WindowsIdentity secp = new WindowsIdentity(userName))
                    {
                        if (GlobalUserStore.UPN.ToLowerInvariant() != userName)
                        {
                            this.CurrentIdentity = secp.Name;

                            this.Sid = secp.User.Value;
                        }

                        this.TGP = GetTokenGroupsAndPrivileges(secp);
                    }                        

                    this.Success = true;
                }

                catch (Exception ex)
                { GlobalEventHandler.RaiseDebugInfoOccured("Failed to get target identity {0} {1} ({2})", userName, ex.Message, ex.GetType()); }
            }

            if (this.Success)
            {
                DecodeGroups();

                DecodePrivileges();
            }
        }

        public void AddLdapGroups(List<SidInfo> sidList, bool isDC)
        {
            foreach (SidInfo sinfo in sidList)
            {
                if (isDC)
                { CheckAddGroup(sinfo); }

                else if (!sinfo.Sid.IsWellKnownSid(38))
                { CheckAddGroup(sinfo); }
            }
        }

        public void AddIdentityGroups(bool isDC)
        {
            foreach (SidInfo sinfo in GlobalUserStore.Groups)
            {
                if (isDC)
                { CheckAddGroup(sinfo); }

                else if (!sinfo.Sid.IsWellKnownSid(38))
                { CheckAddGroup(sinfo); }
            }            
        }

        private void CheckAddGroup(SidInfo sInfo)
        {
            if (this.SidList.AddSafe(sInfo.Sid))
            {
                int pos = this.Groups.Count + 1;

                sInfo.Position = pos;

                this.Groups.AddSafe(sInfo);
            }
        }

        public void CheckLocalNesting(LocalSam samInfo)
        {
            foreach (GroupInfo ginfo in samInfo.Groups)
            {
                foreach (string sid in ginfo.SidList)
                { CheckAddGroup((SidInfo)ginfo); }
            }
        }

        private void DecodeGroups()
        {
            IntPtr ipsids = TGP.Sids;

            for (int sidcnt = 1; sidcnt <= TGP.SidCount; sidcnt++)
            {
                SID_AND_ATTRIBUTES sa = (SID_AND_ATTRIBUTES)Marshal.PtrToStructure(ipsids, typeof(SID_AND_ATTRIBUTES));

                SidInfo sinfo = new SidInfo(sidcnt, sa);

                this.SidList.AddSafe(sinfo.Sid);

                this.Groups.AddSafe(sinfo);

                try
                {
                    ipsids = (IntPtr)((Int64)ipsids + Marshal.SizeOf(typeof(SID_AND_ATTRIBUTES)));
                }

                catch (Exception)
                { break; }
            }
        }

        private void DecodePrivileges()
        {
            IntPtr ipprivs = TGP.Privileges;

            for (int privcnt = 1; privcnt <= TGP.PrivilegeCount; privcnt++)
            {
                LUID_AND_ATTRIBUTES la = (LUID_AND_ATTRIBUTES)Marshal.PtrToStructure(ipprivs, typeof(LUID_AND_ATTRIBUTES));

                PrivilegeInfo pinfo = new PrivilegeInfo(privcnt, la);

                Privileges.AddSafe(pinfo);

                PrivilegesStringList.AddSafe(pinfo.Name);

                try
                {
                    ipprivs = (IntPtr)((Int64)ipprivs + Marshal.SizeOf(typeof(LUID_AND_ATTRIBUTES)));
                }

                catch (Exception)
                { break; }
            }
        }

        private TOKEN_GROUPS_AND_PRIILEGES GetTokenGroupsAndPrivileges(WindowsIdentity secp)
        {
            TOKEN_GROUPS_AND_PRIILEGES ret = default(TOKEN_GROUPS_AND_PRIILEGES);

            int tlength = 0;

            this.TokenHandle = IntPtr.Zero;

            this.Success = Advapi32.GetTokenInformation(secp.Token, TOKEN_INFORMATION_CLASS.TokenGroupsAndPrivileges, this.TokenHandle, tlength, out tlength);

            this.TokenHandle = Marshal.AllocHGlobal(tlength);

            this.Success = Advapi32.GetTokenInformation(secp.Token, TOKEN_INFORMATION_CLASS.TokenGroupsAndPrivileges, this.TokenHandle, tlength, out tlength);

            if (this.Success)
            { ret = (TOKEN_GROUPS_AND_PRIILEGES)Marshal.PtrToStructure(TokenHandle, typeof(TOKEN_GROUPS_AND_PRIILEGES)); }

            else
            { GlobalEventHandler.RaiseDebugInfoOccured("Failed on GetTokenInformation"); }

            return ret;
        }

        #endregion
    }
}
