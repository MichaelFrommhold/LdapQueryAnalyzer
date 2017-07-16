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
    public enum SID_ATTRIBUTE_INFORMATION : long
    {
        SE_GROUP_USER = 0x00000000L,
        SE_GROUP_MANDATORY = 0x00000001L,
        SE_GROUP_ENABLED_BY_DEFAULT = 0x00000002L,
        SE_GROUP_ENABLED = 0x00000004L,
        SE_GROUP_OWNER = 0x00000008L,
        SE_GROUP_USE_FOR_DENY_ONLY = 0x00000010L,
        SE_GROUP_INTEGRITY = 0x00000020L,
        SE_GROUP_INTEGRITY_ENABLED = 0x00000040L,
        SE_GROUP_FROM_ENUM = 0x00000400L,
        SE_GROUP_FROM_ELEVATION_ONLY = 0x00000800L,
        SE_GROUP_USER_FROM_ENUM = 0x00001000L,
        SE_GROUP_LOGON_ID = 0xC0000000L,
        SE_GROUP_RESOURCE = 0x20000000L //The SID identifies a domain-local group.        
    }

}
