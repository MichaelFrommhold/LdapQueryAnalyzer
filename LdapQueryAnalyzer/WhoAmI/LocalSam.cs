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
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class LocalSam
    {
        #region fields

        public List<GroupInfo> Groups { get; private set; }

        public string MachineName { get; private set; }

        public string MachineSid { get; private set; }

        public bool IsDC { get; private set; }

        public bool Success { get; private set; }

        #endregion

        #region constructors

        public LocalSam()
        {
            LoadInternal();
        }

        ~LocalSam()
        { }

        #endregion

        #region methods

        private void LoadInternal()
        {
            this.Success = true;

            this.Groups = new List<GroupInfo> { };

            this.MachineName = Environment.MachineName;

            GetMachineSid();

            CheckMachineRole();

            if (!IsDC)
            { WalkMachineGroups(); }
        }

        private void WalkMachineGroups()
        {
            IntPtr enumbuffer = IntPtr.Zero;
            uint entriesread = 0;
            uint totalentries = 0;

            uint rc = NetApi32.NetLocalGroupEnum(null, 0, ref enumbuffer, 0xFFFFFFFF, out entriesread, out totalentries, IntPtr.Zero);

            this.Success = (rc == 0);

            if (this.Success)
            {
                for (int cnt = 0; cnt < totalentries; cnt++)
                {
                    LOCALGROUP_INFO_0 ginfo = (LOCALGROUP_INFO_0)Marshal.PtrToStructure(enumbuffer, typeof(LOCALGROUP_INFO_0));

                    HandleLocalGroup(ginfo);

                    try
                    { enumbuffer = (IntPtr)((Int64)enumbuffer + Marshal.SizeOf(typeof(LOCALGROUP_INFO_0))); }

                    catch (Exception)
                    { break; }
                }

                NetApi32.NetApiBufferFree(enumbuffer);
            }
        }

        private void HandleLocalGroup(LOCALGROUP_INFO_0 ginfo)
        {
            IntPtr enumbuffer = IntPtr.Zero;
            uint entriesread = 0;
            uint totalentries = 0;

            string sid = GetAccountSid(ginfo.lgrpi0_name);

            GroupInfo group = new GroupInfo(sid, ginfo.lgrpi0_name);

            uint rc = NetApi32.NetLocalGroupGetMembers(null, group.NTName, 1, ref enumbuffer, 0xFFFFFFFF, out entriesread, out totalentries, IntPtr.Zero);

            this.Success = (rc == 0);

            if (this.Success)
            {
                for (int cnt = 0; cnt < totalentries; cnt++)
                {
                    LOCALGROUP_MEMBERS_INFO_1 member = (LOCALGROUP_MEMBERS_INFO_1)Marshal.PtrToStructure(enumbuffer, typeof(LOCALGROUP_MEMBERS_INFO_1));

                    string msid = GetAccountSid(member.lgrmi1_sid);

                    SidInfo sinfo = new SidInfo(msid, member.lgrmi1_name, member.lgrmi1_sidusage);

                    group.Members.Add(sinfo);

                    group.SidList.Add(msid);

                    try
                    { enumbuffer = (IntPtr)((Int64)enumbuffer + Marshal.SizeOf(typeof(LOCALGROUP_MEMBERS_INFO_1))); }

                    catch (Exception)
                    { break; }
                }

                this.Groups.AddSafe(group);

                NetApi32.NetApiBufferFree(enumbuffer);
            }
        }

        private void GetMachineSid()
        {
            this.MachineSid = GetAccountSid(MachineName);

            if (!this.Success)
            { GlobalEventHandler.RaiseDebugInfoOccured("Failed to resolve machine sid!"); }
        }

        private string GetAccountSid(string name)
        {
            string ret = name;

            uint sidlength = 0;
            uint domainnamelength = 0;
            SID_NAME_USE siduse;
            StringBuilder domainname = new StringBuilder();
            IntPtr psid = IntPtr.Zero;

            this.Success = Advapi32.LookupAccountName(null, name, psid, ref sidlength, domainname, ref domainnamelength, out siduse);

            psid = Marshal.AllocHGlobal((int)sidlength);

            domainname.EnsureCapacity((int)domainnamelength);

            this.Success = Advapi32.LookupAccountName(null, name, psid, ref sidlength, domainname, ref domainnamelength, out siduse);

            if (this.Success)
            {
                SecurityIdentifier msid = new SecurityIdentifier(psid);

                ret = msid.Value;

                Marshal.FreeHGlobal(psid);
            }

            return ret;
        }

        private string GetAccountSid(IntPtr psid)
        {
            string ret = String.Empty;

            SecurityIdentifier SID = new SecurityIdentifier(psid);

            ret = SID.Value;

            return ret;
        }

        private void CheckMachineRole()
        {
            int role = 0;

            using (ManagementObjectSearcher wmi = new ManagementObjectSearcher(@"Select DomainRole from Win32_ComputerSystem"))
            {
                try
                {
                    foreach (ManagementObject result in wmi.Get())
                    {
                        int.TryParse(result.Properties["DomainRole"].Value.ToString(), out role);

                        break;
                    }
                }

                catch (Exception) { }
            }

            this.IsDC = (role > 3);
        }

        private void WalkNesting()
        {
            foreach (GroupInfo group in this.Groups)
            {
                foreach (GroupInfo checkgroup in this.Groups)
                {
                    if (group.Sid != checkgroup.Sid)
                    {
                        if (checkgroup.SidList.Contains(group.Sid))
                        { GlobalEventHandler.RaiseNestingFound(checkgroup.Sid, (SidInfo)group); }
                    }
                }
            }
        }

        #endregion
    }
}
