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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public enum LdapOption
    {
        LDAP_OPT_DESC = 1,
        LDAP_OPT_DEREF,
        LDAP_OPT_SIZELIMIT,
        LDAP_OPT_TIMELIMIT, // LDAP server timoouts for bind and query
        LDAP_OPT_REFERRALS = 8,
        LDAP_OPT_RESTART,
        LDAP_OPT_SSL,
        LDAP_OPT_REFERRAL_HOP_LIMIT = 16,
        LDAP_OPT_VERSION,
        LDAP_OPT_API_FEATURE_INFO = 21,
        LDAP_OPT_HOST_NAME = 48,
        LDAP_OPT_ERROR_NUMBER,
        LDAP_OPT_ERROR_STRING,
        LDAP_OPT_SERVER_ERROR,
        LDAP_OPT_SERVER_EXT_ERROR,
        LDAP_OPT_HOST_REACHABLE = 62,
        LDAP_OPT_PING_KEEP_ALIVE = 54,
        LDAP_OPT_PING_WAIT_TIME,
        LDAP_OPT_PING_LIMIT,
        LDAP_OPT_DNSDOMAIN_NAME = 59,
        LDAP_OPT_GETDSNAME_FLAGS = 61,
        LDAP_OPT_PROMPT_CREDENTIALS = 63,
        LDAP_OPT_TCP_KEEPALIVE,
        LDAP_OPT_FAST_CONCURRENT_BIND,
        LDAP_OPT_SEND_TIMEOUT,
        LDAP_OPT_REFERRAL_CALLBACK = 112,
        LDAP_OPT_CLIENT_CERTIFICATE = 128,
        LDAP_OPT_SERVER_CERTIFICATE,
        LDAP_OPT_AUTO_RECONNECT = 145,
        LDAP_OPT_SSPI_FLAGS,
        LDAP_OPT_SSL_INFO,
        LDAP_OPT_SIGN = 149,
        LDAP_OPT_ENCRYPT,
        LDAP_OPT_SASL_METHOD,
        LDAP_OPT_AREC_EXCLUSIVE,
        LDAP_OPT_SECURITY_CONTEXT,
        LDAP_OPT_ROOTDSE_CACHE
    }
}
