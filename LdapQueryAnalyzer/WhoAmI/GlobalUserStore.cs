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
using System.Security.Principal;
using System.Text;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public static class GlobalUserStore
    {
        public static bool ShowGroups = true;
        public static bool ShowPrivs = true;
        public static bool ShowURA = true;
        public static bool ResolveSids = true;

        public static string Name { get; set; }

        public static List<SidInfo> Groups { get; set; }

        public static bool IsElevated { get; set; }

        public static string UserName { get; set; }

        public static SecurityIdentifier Sid { get; set; }

        public static string SDDLSid { get { return (Sid != null) ? Sid.Value : null; } }

        public static string UPN { get; set; }

        public static Dictionary<string, string> SidStore { get; set; }

        public static void Load()
        {
            Groups = new List<SidInfo> { };

            SidStore = new Dictionary<string, string> { };
            
            // fatal killer
            //UPN =  GetUPN();

            using (WindowsIdentity curid = WindowsIdentity.GetCurrent())
            {
                Name = curid.Name;

                if (Name.Contains("\\"))
                { UserName = Name.Split((char)92)[1]; }

                else
                { UserName = Name; }                

                Sid = curid.User;

                IsElevated = curid.IsElevated();                
            }               
        }

        private static string GetUPN()
        {
            string ret = string.Empty;

            IntPtr name = IntPtr.Zero;

            int size = 0;

            // GetUserNameEx -> fatal killer
            try
            {
                int rc = Secur32.GetUserNameEx((int)EXTENDED_NAME_FORMAT.NameUserPrincipal, name, ref size);

                if (size > 0)
                {
                    name = Marshal.AllocHGlobal(size);

                    rc = Secur32.GetUserNameEx((int)EXTENDED_NAME_FORMAT.NameUserPrincipal, name, ref size);
                }

                if (rc != 0)
                {
                    ret = Marshal.PtrToStringAuto(name);

                    Marshal.FreeHGlobal(name);
                }
            }

            catch { }

            return ret;
        }
    }
}
