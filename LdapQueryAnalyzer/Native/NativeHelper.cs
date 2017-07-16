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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class NativeHelper
    {
        public static string GetLastError()
        {
            string ret = null;

            int erc = Marshal.GetLastWin32Error();

            Win32Exception wex = new Win32Exception(erc);

            ret = string.Format("{0} (RC: {1})", wex.Message, erc);

            if (wex.InnerException != null)
            { ret = string.Format("{0} [{1}]", ret, wex.InnerException.Message); }

            return ret;
        }
        
        public static LSA_UNICODE_STRING InitLSAString(string val)
        {
            LSA_UNICODE_STRING lsastring = new LSA_UNICODE_STRING();

            lsastring.Buffer = val;

            lsastring.Length = (UInt16)(val.Length * sizeof(char));

            lsastring.MaximumLength = (UInt16)(lsastring.Length + sizeof(char));

            return lsastring;
        }

        public static LSA_OBJECT_ATTRIBUTES InitLSAObjectAttributes()
        {
            LSA_OBJECT_ATTRIBUTES ret = new LSA_OBJECT_ATTRIBUTES();

            ret.Length = (ulong)Marshal.SizeOf(typeof(LSA_OBJECT_ATTRIBUTES));

            ret.RootDirectory = IntPtr.Zero;

            ret.ObjectName = IntPtr.Zero;

            ret.Attributes = 0;

            ret.SecurityDescriptor = IntPtr.Zero;

            ret.SecurityQualityOfService = IntPtr.Zero;

            return ret;
        }

        public static List<string> GetPrivilegeList()
        {
            List<string> ret = new List<string> { "SeAssignPrimaryTokenPrivilege", "SeAuditPrivilege", "SeBackupPrivilege", 
                                                  "SeBatchLogonRight", "SeChangeNotifyPrivilege", "SeCreateGlobalPrivilege", 
                                                  "SeCreatePagefilePrivilege", "SeCreatePermanentPrivilege", "SeCreateSymbolicLinkPrivilege", 
                                                  "SeCreateTokenPrivilege", "SeDebugPrivilege", "SeDenyBatchLogonRight", 
                                                  "SeDenyInteractiveLogonRight", "SeDenyNetworkLogonRight", "SeDenyRemoteInteractiveLogonRight", 
                                                  "SeDenyServiceLogonRight", "SeEnableDelegationPrivilege", "SeImpersonatePrivilege", 
                                                  "SeIncreaseBasePriorityPrivilege", "SeIncreaseQuotaPrivilege", "SeIncreaseWorkingSetPrivilege", 
                                                  "SeInteractiveLogonRight", "SeLoadDriverPrivilege", "SeLockMemoryPrivilege", 
                                                  "SeManageVolumePrivilege", "SeNetworkLogonRight", "SeProfileSingleProcessPrivilege", 
                                                  "SeRelabelPrivilege", "SeRemoteInteractiveLogonRight", "SeRemoteShutdownPrivilege", 
                                                  "SeRestorePrivilege", "SeSecurityPrivilege", "SeServiceLogonRight", 
                                                  "SeShutdownPrivilege", "SeSyncAgentPrivilege", "SeSystemEnvironmentPrivilege", 
                                                  "SeSystemProfilePrivilege", "SeSystemtimePrivilege", "SeTakeOwnershipPrivilege", 
                                                  "SeTcbPrivilege", "SeTimeZonePrivilege", "SeTrustedCredManAccessPrivilege", 
                                                  "SeUndockPrivilege", "SeDelegateSessionUserImpersonatePrivilege" };

            return ret;
        }
    }
}
