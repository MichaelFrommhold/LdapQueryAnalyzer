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
    public enum QUERY_RESULT_EVENT_TYPE
    {
        NONE = 0x0,
        IS_PARTIAL = 0x1,
        IS_COMPLETED = 0x2,
        IS_ASYNC = 0x4,
        FROM_SINGLE_OBJECT_PATH = 0x8,
        FROM_SCHEMA = 0x10,
        FROM_DECODING = 0x20,
        FROM_GUI = 0x40,
        FROM_BROWSER = 0x80
    }

    public enum MATCH_POSITION
    {
        STARTSWITH,
        INSTRING,
        ENDSWITH
    }

    public enum FILTER_GROUPS
    {
        OPEN,
        CLOSE,
        NONE
    }

    public enum CUSTOM_SCOPE
    {
        BASE,
        ONELEVEL,
        SUBTREE,
        PHANTOMROOT,
        ROOTDSE
    }

    public enum NC_IDs
    {
        defaultnamingContext,
        schemaNamingContext,
        configurationNamingContext,
        rootDomainNamingContext,
        aggregateSchema,
        other
    }

    public enum ASSOCIATE_FROM
    {
        ENUM,
        DICTIONARY,
        BERCONVERTER,        
        //TYPE,
        NONE
    }
    
    // Summary:
    //     The System.DirectoryServices.ActiveDirectoryRights enumeration specifies
    //     the access rights that are assigned to an Active Directory Domain Services
    //     object.
    [Flags]
    public enum AD_ACCESS_MASK
    {
        CreateChild = 0x1,
        DeleteChild = 0x2,
        ListChildren = 0x4,
        Self = 0x8,
        ReadProperty = 0x10,
        WriteProperty = 0x20,
        DeleteTree = 0x40,
        ListObject = 0x80,
        ControlAccess = 0x100,
        Delete = 0x10000,
        ReadControl = 0x20000,
        WriteDacl = 0x40000,
        WriteOwner = 0x80000,
        Synchronize = 0x100000,

        FullControl = 0x000f01ff,

        AccessSystemSecurity = 0x1000000

        //GenericExecute = 131076, 
        //GenericWrite = 131112,
        //GenericRead = 131220,
        //GenericAll = 983551,        
    }

    public enum DS_HEURISTICS
    {
        fSupFirstLastANR = 0x1,
        fSupLastFirstANR,
        fDoListObject,
        fDoNickRes,
        fLDAPUsePermMod,
        ulHideDSID,
        fLDAPBlockAnonOps,
        fAllowAnonNSPI,
        fUserPwdSupport,
        tenthChar,
        fSpecifyGUIDOnAdd,
        fDontStandardizeSDs,
        fAllowPasswordOperationsOverNonSecureConnection,
        fDontPropagateOnNoChangeUpdate,
        fComputeANRStats,
        dwAdminSDExMask,
        fKVNOEmuW2K,
        fLDAPBypassUpperBoundsOnLimits,
        fDisableAutoIndexingOnSchemaUpdate,
        twentiethChar,
        DoNotVerifyUPNAndOrSPNUniqueness,
        Extension
    }
}
