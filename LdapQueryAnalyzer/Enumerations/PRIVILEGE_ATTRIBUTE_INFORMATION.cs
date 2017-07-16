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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    [Flags]
    public enum PRIVILEGE_ATTRIBUTE_INFORMATION : long
    {
        SE_PRIVILEGE_DISABLED = 0x00000000L,
        SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001L,
        SE_PRIVILEGE_ENABLED = 0x00000002L,
        SE_PRIVILEGE_REMOVED = 0x00000004L,
        SE_PRIVILEGE_FROM_ENUM = 0x00000400L,
        SE_PRIVILEGE_FROM_ELEVATION_ONLY = 0x00000800L,
        SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000L
    }

}
