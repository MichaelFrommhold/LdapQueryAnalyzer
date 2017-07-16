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
using System.Security.Principal;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SidInfo
    {
        #region fields

        public int Position { get; set; }

        public SID_ATTRIBUTE_INFORMATION Attributes { get; set; }

        public string Sid { get; set; }

        public string NTName { get; set; }

        public bool IsGroup
        { get { return ((Attributes & SID_ATTRIBUTE_INFORMATION.SE_GROUP_USER) != SID_ATTRIBUTE_INFORMATION.SE_GROUP_USER_FROM_ENUM); } }

        #endregion

        #region constructors

        public SidInfo(int pos, SID_AND_ATTRIBUTES sa)
        {
            this.Position = pos;

            this.Attributes = (SID_ATTRIBUTE_INFORMATION)sa.Attributes;

            SecurityIdentifier sid = new SecurityIdentifier(sa.PSiD);

            ResolveSid(sid);
        }

        public SidInfo(IntPtr pSid)
        {
            this.Position = -1;

            this.Attributes = SID_ATTRIBUTE_INFORMATION.SE_GROUP_USER;

            SecurityIdentifier sid = new SecurityIdentifier(pSid);

            ResolveSid(sid);
        }

        public SidInfo(byte[] bSid)
        {
            this.Position = -10;

            this.Attributes = SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ENUM | SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ELEVATION_ONLY;

            SecurityIdentifier sid = new SecurityIdentifier(bSid, 0);

            ResolveSid(sid);
        }

        public SidInfo(SecurityIdentifier sid)
        {
            this.Position = -10;

            this.Attributes = SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ENUM | SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ELEVATION_ONLY;
                       
            ResolveSid(sid);
        }

        public SidInfo(string sddlSid, string name, SID_NAME_USE usage = SID_NAME_USE.SidTypeGroup)
        {
            this.Position = -10;

            this.Attributes = ((usage & SID_NAME_USE.SidTypeGroup) == SID_NAME_USE.SidTypeGroup) ? SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ENUM :
                                                                                              SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ENUM |
                                                                                              SID_ATTRIBUTE_INFORMATION.SE_GROUP_USER_FROM_ENUM;

            this.Sid = sddlSid;

            this.NTName = name;

            if (this.IsGroup)
            { CheckSidType(sddlSid); }
        }

        ~SidInfo()
        { }

        #endregion

        #region methods

        private void ResolveSid(SecurityIdentifier sid)
        {
            this.Sid = sid.Value;

            this.NTName = Sid;

            if (GlobalUserStore.ResolveSids)
            {
                if (GlobalUserStore.SidStore.ContainsKey(this.Sid))
                {
                    this.NTName = GlobalUserStore.SidStore[this.Sid];
                }

                else
                {
                    try
                    {
                        NTAccount nt = (NTAccount)sid.Translate(typeof(NTAccount));

                        this.NTName = nt.Value;
                    }

                    catch (Exception) { this.NTName = "not resolved"; }

                    GlobalUserStore.SidStore.Add(this.Sid, this.NTName);
                }
            }
        }

        private void CheckSidType(string sddlSid)
        {
            SecurityIdentifier csid = new SecurityIdentifier(sddlSid);

            if (csid.IsWellKnown(WellKnownSidType.AccountAdministratorSid) ||
                csid.IsWellKnown(WellKnownSidType.AccountDomainAdminsSid) ||
                csid.IsWellKnown(WellKnownSidType.AccountEnterpriseAdminsSid) ||
                csid.IsWellKnown(WellKnownSidType.AccountPolicyAdminsSid) ||
                csid.IsWellKnown(WellKnownSidType.AccountSchemaAdminsSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinAccountOperatorsSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinBackupOperatorsSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinIncomingForestTrustBuildersSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinNetworkConfigurationOperatorsSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinPerformanceLoggingUsersSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinPerformanceMonitoringUsersSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinPrintOperatorsSid) ||
                csid.IsWellKnown(WellKnownSidType.BuiltinSystemOperatorsSid) ||
                csid.IsWellKnown(WellKnownSidType.EnterpriseControllersSid))
            {
                this.Attributes = (Attributes == SID_ATTRIBUTE_INFORMATION.SE_GROUP_USER) ? SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ELEVATION_ONLY : Attributes | SID_ATTRIBUTE_INFORMATION.SE_GROUP_FROM_ELEVATION_ONLY;
            }


        }

        public override string ToString()
        {
            string ret = String.Empty;

            if (this.Position > -1)
            { ret = String.Format("{0}\t{1}", this.Position.ToString(), this.Sid); }

            else
            { ret = String.Format("\t{0}", this.Sid); }

            if (GlobalUserStore.ResolveSids)
            { ret += String.Format(" ({0})", this.NTName); }

            if (this.Position != -1)
            { ret += String.Format(" : ({0})", this.Attributes); }

            return ret;
        }

        #endregion
    }

}
