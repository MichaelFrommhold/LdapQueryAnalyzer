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


namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class UserRights : IDisposable
    {
        #region fields

        public List<PrivilegeAndPrincipals> Privileges { get; set; }

        private IntPtr PolicyHandle { get; set; }

        private bool IsDisposed { get; set; }

        public bool Success { get; protected set; }

        public List<string> Messages { get; protected set; }

        #endregion

        #region constructors

        public UserRights(string target = null)
        {
            LoadInternal(target);
        }

        ~UserRights()
        { Dispose(); }

        #endregion

        #region methods

        protected void LoadInternal(string target)
        {
            this.Messages = new List<string> { };

            this.Privileges = new List<PrivilegeAndPrincipals> { };

            this.IsDisposed = false;

            this.Success = false;

            this.PolicyHandle = IntPtr.Zero;

            IntPtr temphandle = IntPtr.Zero;

            LSA_OBJECT_ATTRIBUTES lsainfo = NativeHelper.InitLSAObjectAttributes();

            LSA_UNICODE_STRING systemname = (target == null) ? default(LSA_UNICODE_STRING) : NativeHelper.InitLSAString(target);

            uint rc = Advapi32.LsaOpenPolicy(systemname, ref lsainfo, LSA_POLICY_ACCESS.POLICY_ALL_ACCESS, out temphandle);
            
            this.Success = (rc == 0);

            if (this.Success)
            {
                this.PolicyHandle = temphandle;

                List<string> privlist = NativeHelper.GetPrivilegeList();

                foreach (string priv in privlist)
                {
                    GetPrivilegeAndPrincipals(priv);
                }
            }

            else
            {
                string errmsg = null;

                if (NativeHelper.GetLastError(out errmsg) != 0)
                { Messages.Add("\tLsaOpenPolicy: " + errmsg); }
            }
        }

        protected void GetPrivilegeAndPrincipals(string privName)
        {            
            IntPtr enumbuffer = IntPtr.Zero;

            ulong cnt = 0;

            LSA_UNICODE_STRING lsapriv = NativeHelper.InitLSAString(privName);

            uint rc = Advapi32.LsaEnumerateAccountsWithUserRight(PolicyHandle, lsapriv, out enumbuffer, out cnt);

            if (rc == 0)
            {
                PrivilegeAndPrincipals privinfo = new PrivilegeAndPrincipals(privName);

                for (int step = 0; step < (int)cnt; step++)
                {
                    LSA_ENUMERATION_INFORMATION lsaenum = (LSA_ENUMERATION_INFORMATION)Marshal.PtrToStructure(enumbuffer, typeof(LSA_ENUMERATION_INFORMATION));

                    SidInfo sinfo = new SidInfo(lsaenum.Sid);

                    privinfo.Principals.AddSafe(sinfo);

                    privinfo.SidPrincipals.AddSafe(sinfo.Sid);

                    try
                    { enumbuffer = (IntPtr)((Int64)enumbuffer + Marshal.SizeOf(typeof(LSA_ENUMERATION_INFORMATION))); }

                    catch (Exception)
                    { break; }
                }

                this.Privileges.AddSafe(privinfo);
            }

            else
            {
                string errmsg = null;

                if (NativeHelper.GetLastError(out errmsg) != 0)
                { Messages.Add("\tLsaEnumerateAccountsWithUserRight: " + errmsg); }
            }
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                this.IsDisposed = true;

                if (this.PolicyHandle != IntPtr.Zero)
                {
                    Advapi32.LsaClose(PolicyHandle);

                    this.PolicyHandle = IntPtr.Zero;
                }
            }
        }

        #endregion
    }
}
