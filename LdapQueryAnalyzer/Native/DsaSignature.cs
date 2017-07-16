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

                ret.AddFormatted("\t\t<V{0}: Flags = 0x{1}; LatencySecs = {2}; DsaGUID = {3}>",
                                        sig.dwVersion, sig.State.dwFlags, sig.State.dstimeBackupErrorLatencySecs,
                                        sig.State.dsaGuid.ToString());

                Marshal.FreeHGlobal(ipsig);
            }

            catch (Exception ex)
            {
                ex.ToDummy();

                ret.AddFormatted("\t\t<not decoded>: {0}", value.GetType().ToString());
            }

            return ret;
        }

        #endregion
    }
}
