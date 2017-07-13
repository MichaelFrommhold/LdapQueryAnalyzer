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

    public class WtsApi
    {
        #region PInvoke

        public enum WTS_CONFIG_CLASS : int
        {
            InitialProgram = 0,
            WorkingDirectory,
            InheritInitialProgram,
            AllowLogonTerminalServer,
            TimeoutSettingsConnections,
            TimeoutSettingsDisconnections,
            TimeoutSettingsIdle,
            DeviceClientDrives,
            DeviceClientPrinters,
            DeviceClientDefaultPrinter,
            BrokenTimeoutSettings,
            ReconnectSettings,
            ModemCallbackSettings,
            ModemCallbackPhoneNumber,
            ShadowingSettings,
            TerminalServerProfilePath,
            TerminalServerHomeDir,
            TerminalServerHomeDirDrive,
            TerminalServerRemoteHomeDir
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WTSUSERCONFIG 
        {  
            public WTS_CONFIG_SOURCE Source;

            public bool InheritInitialProgram;

            public bool AllowLogonTerminalServer;

            public long TimeoutSettingsConnections;

            public long TimeoutSettingsDisconnections;

            public long TimeoutSettingsIdle;

            public bool DeviceClientDrives;

            public bool DeviceClientPrinters;

            public bool ClientDefaultPrinter;

            public WTS_BROKEN_TIMEOUT BrokenTimeoutSettings;

            public WTS_RECONNECT ReconnectSettings;
            
            public WTS_USER_SHADOWNG ShadowingSettings;

            public WTS_USER_MODEM_CALLBACK ModemCallbackSettings;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string ModemCallbackPhoneNumber;          

            [MarshalAs(UnmanagedType.LPWStr)]
            public string InitialProgram;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string WorkDirectory;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string TerminalServerProfilePath;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string TerminalServerHomeDir;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string TerminalServerHomeDirDrive;

            public WTS_REMOTE_HOMEDIR TerminalServerRemoteHomeDir;
        }

        public enum WTS_CONFIG_SOURCE : int
        {   
            WTSUserConfigSourceSAM
        }

        public enum WTS_USER_SHADOWNG : int
        {
            DISABLE = 0,
            ENABLE_INPUT_NOTIFY,
            ENABLE_INPUT_NO_NOTIFY,
            ENABLE_NO_INPUT_NOTIFY,
            ENABLE_NO_INPUT_NO_NOTIFY
        }

        public enum WTS_USER_MODEM_CALLBACK
        {
            DISABLE = 0,
            PROMPT,
            AUTOMATIC
        }

        public enum WTS_BROKEN_TIMEOUT : int
        { 
            DISCONNECT = 0,
            TERMINATE
        }

        public enum WTS_RECONNECT : int
        {
            FROM_ANY_CLIENT = 0,
            FROM_SAME_CLIENT
        }

        public enum WTS_REMOTE_HOMEDIR : int
        {
            LOCAL_PATH = 0,
            REMOTE_PATH
        }

        internal enum NET_API_STATUS
        {
            SUCCESS = 0,
            ERROR_PATH_NOT_FOUND = 53,
            ERROR_DC_NOT_FOUND = 2453
        }

        [DllImport("wtsapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int WTSQueryUserConfig(  [MarshalAs(UnmanagedType.LPTStr)]
                                                      string pServerName,
                                                        [MarshalAs(UnmanagedType.LPTStr)]
                                                      string pUserName,
                                                      int WTSConfigClass,
                                                      out IntPtr ppBuffer,
                                                      out long pBytesReturned);
        
        [DllImport("wtsapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int WTSSetUserConfig(    [MarshalAs(UnmanagedType.LPTStr)]
                                                    string pServerName,
                                                        [MarshalAs(UnmanagedType.LPTStr)]
                                                    string pUserName,
                                                    WTS_CONFIG_CLASS WTSConfigClass,
                                                    IntPtr pBuffer,
                                                    long DataLength);

        [DllImport("wtsapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern void WTSFreeMemory(IntPtr pMemory);

        #endregion

        #region fields

        private const int WTS_CONFIG_CLASS_CONFIG_USER = 19;

        private string DC;

        private string User;

        public WTSUSERCONFIG WtsInfo;

        public Dictionary<WTS_CONFIG_CLASS, object> Values = new Dictionary<WTS_CONFIG_CLASS, object>();

        public bool HasError;

        public string ErrorMessage;

        #endregion

        #region contructor

        internal WtsApi(string dcName, string sAMAccountName)
        {
            DC = dcName;

            if (DC.Contains("."))
            { DC = DC.Substring(0, DC.IndexOf(".")); }

            User = sAMAccountName;

            LoadInternal();
        }
        
        #endregion

        #region methods

        private void LoadInternal()
        {
            if (!CheckArguments())
            { return; }

            //GetFieldStruct();

            foreach (WTS_CONFIG_CLASS fieldid in Enum.GetValues(typeof(WTS_CONFIG_CLASS)))
            { Values.Add(fieldid, DecodeField(fieldid)); }

        }

        private void GetFieldStruct()
        {
            IntPtr pbuffer = new IntPtr();
            long psize = 0;
                      
            int rc = WTSQueryUserConfig(DC, User, WTS_CONFIG_CLASS_CONFIG_USER, out pbuffer, out psize);

            if (pbuffer != IntPtr.Zero)
            {
                WtsInfo = new WTSUSERCONFIG();

                WtsInfo = (WTSUSERCONFIG)Marshal.PtrToStructure(pbuffer, typeof(WTSUSERCONFIG));

                WTSFreeMemory(pbuffer); 
            }
        }

        public object GetFieldValue(WTS_CONFIG_CLASS fieldId)
        {
            object ret = null;

            ret = Values[fieldId];

            return ret;
        }

        public static string SetFieldValue(string dcName, string sAMAccountName, WTS_CONFIG_CLASS fieldID, object value, out bool success)
        {
            success = false;

            string ret = String.Format("Result setting {0} : ", fieldID.ToString());

            long datalength = Marshal.SizeOf(value.GetType());

            GCHandle buffer = GCHandle.Alloc(value);

            int rc = WTSSetUserConfig(dcName, sAMAccountName, fieldID, (IntPtr)buffer, datalength);


            return ret;
        }
        
        public List<string> Print()
        {
            List<string> ret = new List<string>();

            foreach (KeyValuePair<WTS_CONFIG_CLASS, object> val in Values)
            { ret.Add(String.Format("\t\t<{0}: {1}>", val.Key.ToString(), val.Value.ToString())); }

            return ret;
        }

        private object DecodeField(WTS_CONFIG_CLASS fieldId)
        {
            object ret = null;

            IntPtr pbuffer = IntPtr.Zero;
            long psize = 0;

            int rc = WTSQueryUserConfig(DC, User, (int)fieldId, out pbuffer, out psize);

            ret = MarshalField(fieldId, pbuffer);

            if (pbuffer != IntPtr.Zero)
            { WTSFreeMemory(pbuffer); }

            return ret;
        }

        private object MarshalField(WTS_CONFIG_CLASS fieldId, IntPtr pBuffer)
        {
            object ret = null;

            switch (fieldId)
            {
                case WTS_CONFIG_CLASS.AllowLogonTerminalServer:
                case WTS_CONFIG_CLASS.DeviceClientDefaultPrinter:
                case WTS_CONFIG_CLASS.DeviceClientDrives:
                case WTS_CONFIG_CLASS.DeviceClientPrinters:
                case WTS_CONFIG_CLASS.InheritInitialProgram:                
                
                    if (pBuffer == IntPtr.Zero)
                    { ret = false; }

                    else
                    { ret = Convert.ToBoolean(Marshal.ReadInt16(pBuffer)); }

                    break;

                case WTS_CONFIG_CLASS.InitialProgram:
                case WTS_CONFIG_CLASS.ModemCallbackPhoneNumber:
                case WTS_CONFIG_CLASS.TerminalServerHomeDir:
                case WTS_CONFIG_CLASS.TerminalServerHomeDirDrive:
                case WTS_CONFIG_CLASS.TerminalServerProfilePath:
                case WTS_CONFIG_CLASS.WorkingDirectory:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    { 
                        ret = Marshal.PtrToStringUni(pBuffer);

                        if (ret.ToString().Length == 0)
                        { ret = "<not set>"; }
                    }

                    break;

                case WTS_CONFIG_CLASS.TimeoutSettingsConnections:
                case WTS_CONFIG_CLASS.TimeoutSettingsDisconnections:
                case WTS_CONFIG_CLASS.TimeoutSettingsIdle:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    { ret = Marshal.ReadInt64(pBuffer); }

                    break;

                case WTS_CONFIG_CLASS.BrokenTimeoutSettings:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    {
                        ret = Marshal.ReadInt32(pBuffer);

                        ret = (WTS_BROKEN_TIMEOUT)ret;
                    }

                    break;

                case WTS_CONFIG_CLASS.ModemCallbackSettings:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    { 
                        ret = Marshal.ReadInt32(pBuffer);

                        ret = (WTS_USER_MODEM_CALLBACK)ret;
                    }

                    break;

                case WTS_CONFIG_CLASS.ShadowingSettings:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    { 
                        ret = Marshal.ReadInt32(pBuffer);

                        ret = (WTS_USER_SHADOWNG)ret;
                    }

                    break;

                case WTS_CONFIG_CLASS.ReconnectSettings:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    {
                        ret = Marshal.ReadInt32(pBuffer);

                        ret = (WTS_RECONNECT)ret;
                    }

                    break;

                case WTS_CONFIG_CLASS.TerminalServerRemoteHomeDir:

                    if (pBuffer == IntPtr.Zero)
                    { ret = "<not set>"; }

                    else
                    {
                        ret = Marshal.ReadInt32(pBuffer);

                        ret = (WTS_REMOTE_HOMEDIR)ret;
                    }

                    break;
            }

           

            return ret;
        }

        private bool CheckArguments()
        {
            bool ret = true;
            
            HasError = false;

            if ((DC == null) || (DC.Length == 0))
            { ret = false; }

            if (ret)
            {
                if ((User == null) || (User.Length == 0))
                { ret = false; }
            }

            if (!ret)
            {
                HasError = true;

                ErrorMessage = "ArgumentException";
            }

            return ret;
        }
        
        #endregion
    }

    public class DsaSignature
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DSA_SIGNATURE_STATE
        {
            public uint dwFlags;

            public long dstimeBackupErrorLatencySecs;

            public Guid dsaGuid;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DSA_SIGNATURE_BLOB
        {
            public uint dwVersion;

            public uint cbSize;

            public DSA_SIGNATURE_STATE State;
        }

        #region constructors

        internal DsaSignature()
        { }

        #endregion

        #region methods

        public static List<string> Decode(byte[] value)
        {
            List<string> ret = new List<string>();

            try
            {
                IntPtr ipsig = Marshal.AllocHGlobal(value.Length);

                Marshal.Copy(value, 0, ipsig, value.Length);

                DSA_SIGNATURE_BLOB sig = (DSA_SIGNATURE_BLOB)Marshal.PtrToStructure(ipsig, typeof(DSA_SIGNATURE_BLOB));

                ret.Add(String.Format("\t\t<V{0}: Flags = 0x{1}; LatencySecs = {2}; DsaGUID = {3}>",
                                        sig.dwVersion, sig.State.dwFlags, sig.State.dstimeBackupErrorLatencySecs, 
                                        sig.State.dsaGuid.ToString()));

                Marshal.FreeHGlobal(ipsig);
            }

            catch (Exception ex)
            { 
                ex.ToDummy();

                ret.Add(string.Format("\t\t<not decoded>: {0}", value.GetType().ToString()));
            }

            return ret;
        }

        #endregion
    }
}
