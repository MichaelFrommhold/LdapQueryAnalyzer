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
using System.Security;
using System.Text;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class Advapi32
    {
        #region "impersonate"

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername,
                                            String lpszDomain,
                                            String lpszPassword,
                                            SECURITY_LOGON_TYPE dwLogonType,
                                            LOGON_PROVIDER dwLogonProvider,
                                            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
                                                 SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
                                                 ref IntPtr DuplicateTokenHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle,
                                                      TOKEN_INFORMATION_CLASS TokenInformationClass,
                                                      IntPtr TokenInformation,
                                                      int TokenInformationLength,
                                                      out int ReturnLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool LookupPrivilegeName(string lpSystemName,
                                                      IntPtr lpLuid,
                                                      StringBuilder lpName,
                                                      ref int cchName);

        #endregion

        #region user rights assignment

        [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]
        internal static extern uint LsaOpenPolicy(LSA_UNICODE_STRING SystemName,
                                                  ref LSA_OBJECT_ATTRIBUTES ObjectAttributes,
                                                  LSA_POLICY_ACCESS AccessMask,
                                                  out IntPtr PolicyHandle);

        [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]
        internal static extern uint LsaEnumerateAccountsWithUserRight(IntPtr PolicyHandle,
                                                                      LSA_UNICODE_STRING UserRights,
                                                                      out IntPtr EnumerationBuffer,
                                                                      out ulong CountReturned);

        [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]
        internal static extern uint LsaEnumerateAccountsWithUserRight(IntPtr PolicyHandle,
                                                                      LSA_UNICODE_STRING[] UserRights,
                                                                      out IntPtr EnumerationBuffer,
                                                                      out ulong CountReturned);

        [DllImport("advapi32")]
        internal static extern int LsaNtStatusToWinError(int Status);

        [DllImport("advapi32")]
        internal static extern int LsaClose(IntPtr ObjectHandle);

        [DllImport("advapi32")]
        internal static extern int LsaFreeMemory(IntPtr Buffer);

        #endregion

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool LookupAccountName([MarshalAs(UnmanagedType.LPWStr)]
                                                    string lpSystemName,
                                                    [MarshalAs(UnmanagedType.LPWStr)]
                                                    string lpAccountName,
                                                    IntPtr Sid,
                                                    ref uint cbSid,
                                                    StringBuilder ReferencedDomainName,
                                                    ref uint cchReferencedDomainName,
                                                    out SID_NAME_USE peUse);
    }
}
