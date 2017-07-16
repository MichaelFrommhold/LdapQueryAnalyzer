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
        internal static extern int WTSQueryUserConfig([MarshalAs(UnmanagedType.LPTStr)]
                                                      string pServerName,
                                                      [MarshalAs(UnmanagedType.LPTStr)]
                                                      string pUserName,
                                                    int WTSConfigClass,
                                                    out IntPtr ppBuffer,
                                                    out long pBytesReturned);

        [DllImport("wtsapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int WTSSetUserConfig([MarshalAs(UnmanagedType.LPTStr)]
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
            { ret.AddFormatted("\t\t<{0}: {1}>", val.Key.ToString(), val.Value.ToString()); }

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
}
