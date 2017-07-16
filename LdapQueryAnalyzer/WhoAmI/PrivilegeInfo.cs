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
using System.Runtime.InteropServices;
using System.Text;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class PrivilegeInfo
    {
        #region fields

        public int Position { get; set; }

        PRIVILEGE_ATTRIBUTE_INFORMATION Attributes { get; set; }

        public string Name { get; set; }

        #endregion

        #region constructors

        public PrivilegeInfo(int pos, LUID_AND_ATTRIBUTES la)
        {
            Position = pos;

            Attributes = (PRIVILEGE_ATTRIBUTE_INFORMATION)la.Attributes;

            StringBuilder privname = new StringBuilder();

            IntPtr ipluid = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LUID)));

            Marshal.StructureToPtr(la.Luid, ipluid, false);

            int cap = 0;

            Advapi32.LookupPrivilegeName(null, ipluid, privname, ref cap);

            privname.EnsureCapacity(cap);

            Advapi32.LookupPrivilegeName(null, ipluid, privname, ref cap);

            Marshal.FreeHGlobal(ipluid);

            Name = privname.ToString();
        }

        public PrivilegeInfo(int pos, string name, bool fromEnum = true)
        {
            this.Position = pos;

            this.Name = name;

            this.Attributes = (fromEnum) ? PRIVILEGE_ATTRIBUTE_INFORMATION.SE_PRIVILEGE_FROM_ENUM : PRIVILEGE_ATTRIBUTE_INFORMATION.SE_PRIVILEGE_FROM_ENUM | PRIVILEGE_ATTRIBUTE_INFORMATION.SE_PRIVILEGE_FROM_ELEVATION_ONLY;
        }

        ~PrivilegeInfo()
        { }

        #endregion

        #region methods

        public override string ToString()
        {
            return String.Format("{0}\t{1} : ({2})", this.Position.ToString(), this.Name, this.Attributes);
        }

        #endregion
    }
}
