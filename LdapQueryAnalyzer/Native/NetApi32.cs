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
    public class NetApi32
    {

        #region api calls

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DsGetDcName([MarshalAs(UnmanagedType.LPTStr)]
                                              string ComputerName,
                                              [MarshalAs(UnmanagedType.LPTStr)]
                                              string DomainName,
                                              Guid DomainGuid,
                                              [MarshalAs(UnmanagedType.LPTStr)]
                                              string SiteName,
                                              DS_FLAGS Flags,
                                              ref IntPtr pDOMAIN_CONTROLLER_INFO);

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DsGetDcOpen([MarshalAs(UnmanagedType.LPTStr)] 
                                              string DnsName,
                                              DS_OPTION_FLAGS OptionFlags,
                                              [MarshalAs(UnmanagedType.LPTStr)] 
                                              string SiteName,
                                              IntPtr DomainGuid,
                                              [MarshalAs(UnmanagedType.LPTStr)]  
                                              string DnsForestName,
                                              DS_OPEN_FLAGS DcFlags,
                                              out IntPtr RetGetDcContext);

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DsGetDcNext(IntPtr GetDcContextHandle,
                                              [In, Out] ref IntPtr SockAddressCount,
                                              out IntPtr SockAddresses,
                                              [Out, MarshalAs(UnmanagedType.LPTStr)]
                                              out string DnsHostName);

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void DsGetDcClose(IntPtr GetDcContextHandle);

        [DllImport("Netapi32.dll", SetLastError = true)]
        public static extern int NetApiBufferFree(IntPtr Buffer);

        [DllImport("netapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint NetLocalGroupEnum(    [MarshalAs(UnmanagedType.LPWStr)]
                                                    string servername,
                                                    int level,
                                                    ref IntPtr bufptr,
                                                    uint prefmaxlen,
                                                    out uint entriesread,
                                                    out uint totalentries,
                                                    IntPtr resumeHandle);

        [DllImport("netapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint NetLocalGroupGetMembers(  [MarshalAs(UnmanagedType.LPWStr)]
                                                          string servername,
                                                            [MarshalAs(UnmanagedType.LPWStr)]
                                                          string localgroupname,
                                                            int level,
                                                            ref IntPtr bufptr,
                                                            uint prefmaxlen,
                                                            out uint entriesread,
                                                            out uint totalentries,
                                                            IntPtr resumehandle);

        #endregion

        #region enums

        [Flags]
        public enum DS_FLAGS
        {
            DS_FORCE_REDISCOVERY = 0x1,
            DS_DIRECTORY_SERVICE_REQUIRED = 0x10,
            DS_DIRECTORY_SERVICE_PREFERRED = 0x20,
            DS_GC_SERVER_REQUIRED = 0x40,
            DS_PDC_REQUIRED = 0x80,
            DS_BACKGROUND_ONLY = 0x100,
            DS_IP_REQUIRED = 0x200,
            DS_KDC_REQUIRED = 0x400,
            DS_TIMESERV_REQUIRED = 0x800,
            DS_WRITABLE_REQUIRED = 0x1000,
            DS_GOOD_TIMESERV_PREFERRED = 0x2000,
            DS_AVOID_SELF = 0x4000,
            DS_ONLY_LDAP_NEEDED = 0x8000,
            DS_IS_FLAT_NAME = 0x10000,
            DS_IS_DNS_NAME = 0x20000,
            DS_TRY_NEXTCLOSEST_SITE = 0x40000,
            DS_DIRECTORY_SERVICE_6_REQUIRED = 0x80000,
            DS_WEB_SERVICE_REQUIRED = 0x100000,
            DS_RETURN_DNS_NAME = 0x40000000,
            //DS_RETURN_FLAT_NAME = 0x80000000
        }

        [Flags]
        public enum DS_OPTION_FLAGS : uint
        {
            DS_OPTION_NONE = 0,
            DS_ONLY_DO_SITE_NAME = 0x01,
            DS_NOTIFY_AFTER_SITE_RECORDS = 0x02
        }

        [Flags]
        public enum DS_OPEN_FLAGS : uint
        {
            DS_FORCE_REDISCOVERY = 0x1,
            DS_GC_SERVER_REQUIRED = 0x40,
            DS_PDC_REQUIRED = 0x80,
            DS_WRITABLE_REQUIRED = 0x1000,
            DS_KDC_REQUIRED = 0x400,
            DS_ONLY_LDAP_NEEDED = 0x8000
        }

        [Flags]
        public enum DS_RETURNED_FLAGS
        {
            DS_PDC_FLAG = 0x1,
            DS_GC_FLAG = 0x4,
            DS_LDAP_FLAG = 0x8,
            DS_DS_FLAG = 0x10,
            DS_KDC_FLAG = 0x20,
            DS_TIMESERV_FLAG = 0x40,
            DS_CLOSEST_FLAG = 0x80,
            DS_WRITABLE_FLAG = 0x100,
            DS_GOOD_TIMESERV_FLAG = 0x200,
            DS_NDNC_FLAG = 0x400,
            DS_SELECT_SECRET_DOMAIN_6_FLAG = 0x800,
            DS_FULL_SECRET_DOMAIN_6_FLAG = 0x1000,
            DS_WS_FLAG = 0x2000,
            DS_DS_8_FLAG = 0x4000,
            DS_DS_9_FLAG = 0x8000,
            //DS_PING_FLAG = &HFFFFF
            DS_DNS_CONTROLLER_FLAG = 0x20000000,
            DS_DNS_DOMAIN_FLAG = 0x40000000,
            //DS_DNS_FOREST_FLAG = 0x80000000
        }

        public enum ERROR_LIST : int
        {
            ERROR_SUCCESS = 0,
            ERROR_NOT_ENOUGH_MEMORY = 8,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_NO_MORE_ITEMS = 259,
            ERROR_INVALID_FLAGS = 1004,
            ERROR_FILEMARK_DETECTED = 110
        }

        #endregion

        #region structs

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DOMAIN_CONTROLLER_INFO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DomainControllerName;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string DomainControllerAddress;

            public uint DomainControllerAddressType;

            public Guid DomainGuid;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string DomainName;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string DnsForestName;

            public NetApi32.DS_RETURNED_FLAGS Flags;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string DcSiteName;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string ClientSiteName;
        }


        #endregion
    }
}
