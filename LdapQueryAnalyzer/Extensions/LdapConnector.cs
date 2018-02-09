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
using System.DirectoryServices.Protocols;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class LdapConnector : LdapConnection
    {
        #region constants

        internal const int DefaultServerTimeoutSeconds = 30;

        #endregion

        #region fields

        internal TimeSpan inServerTimeOut = new TimeSpan(0, 0, DefaultServerTimeoutSeconds);

        internal TimeSpan inCurrentServerTimeOut = new TimeSpan(0, 0, DefaultServerTimeoutSeconds);

        /// <summary>
        /// property contains the length of time, in seconds, before the LDAP server times out for bind or query operations.
        /// </summary>
        /// <returns>The length of time, in seconds, before the LDAP server times out for bind or query operations.</returns>
        public TimeSpan ServerTimeOut
        {
            get { return inServerTimeOut; }

            set
            {
                if (value < TimeSpan.Zero)
                { throw new ArgumentException("No negative time span allowed", "TimeLimit"); }

                else if (value > TimeSpan.MaxValue)
                { throw new ArgumentException("Time span max value exceeded", "TimeLimit"); }

                inServerTimeOut = value;
            }
        }

        #endregion

        #region ctor

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified server.
        /// The logon credentials and the Negotiate Authentication are used to connect to the LDAP server.
        /// Server timeout is controlled from LDAP server settings.
        /// </summary>
        /// <param name="server">A string specifying the server  which can be a domain name, LDAP server name or dotted strings representing the IP address of the LDAP server. Optionally, this parameter may also include a port number, separated from the right end of the string by a colon (:).</param>
        public LdapConnector(string server) : base(server)
        { Connect(); }

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified server.
        /// The logon credentials and the Negotiate Authentication are used to connect to the LDAP server.
        /// Server timeout for bind and query can be configured here.
        /// </summary>
        /// <param name="server">A string specifying the server  which can be a domain name, LDAP server name or dotted strings representing the IP address of the LDAP server. Optionally, this parameter may also include a port number, separated from the right end of the string by a colon (:).</param>
        /// <param name="bindLimit"> A <see cref="System.TimeSpan"/> defining the LDAP server timeouts for bind and query operations.</param>
        public LdapConnector(string server, TimeSpan bindLimit) : base(server)
        {
            ServerTimeOut = bindLimit;

            Connect();
        }


        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified directory identifier. 
        /// The logon credentials and the Negotiate Authentication are used to connect to the LDAP server.
        /// Server timeout is controlled from LDAP server settings.
        /// </summary>
        /// <param name="identifier">A <see cref="System.DirectoryServices.Protocols.LdapDirectoryIdentifier" /> object that specifies the server.</param>
        public LdapConnector(LdapDirectoryIdentifier identifier) : base(identifier)
        { Connect(); }

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified directory identifier. 
        /// The logon credentials and the Negotiate Authentication are used to connect to the LDAP server.
        /// Server timeout for bind and query can be configured here.
        /// </summary>
        /// <param name="identifier">A <see cref="System.DirectoryServices.Protocols.LdapDirectoryIdentifier" /> object that specifies the server.</param>
        /// <param name="bindLimit"> A <see cref="System.TimeSpan"/> defining the LDAP server timeouts for bind and query operations.</param>
        public LdapConnector(LdapDirectoryIdentifier identifier, TimeSpan bindLimit) : base(identifier)
        {
            ServerTimeOut = bindLimit;

            Connect();
        }

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified directory identifier and network credentials.
        /// The logon credentials and the Negotiate Authentication are used to connect to the LDAP server.
        /// Server timeout is controlled from LDAP server settings.
        /// </summary>
        /// <param name="identifier">A <see cref="System.DirectoryServices.Protocols.LdapDirectoryIdentifier" /> object that specifies the server.</param>
        /// <param name="credential">A <see cref="System.Net.NetworkCredential" /> object that specifies the credentials to use.</param>
        public LdapConnector(LdapDirectoryIdentifier identifier, NetworkCredential credential) : base(identifier, credential)
        { Connect(); }

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified directory identifier and network credentials.
        /// The logon credentials and the Negotiate Authentication are used to connect to the LDAP server.
        /// Server timeout for bind and query can be configured here.
        /// </summary>
        /// <param name="identifier">A <see cref="System.DirectoryServices.Protocols.LdapDirectoryIdentifier" /> object that specifies the server.</param>
        /// <param name="credential">A <see cref="System.Net.NetworkCredential" /> object that specifies the credentials to use.</param>
        /// <param name="bindLimit"> A <see cref="System.TimeSpan"/> defining the LDAP server timeouts for bind and query operations.</param>
        public LdapConnector(LdapDirectoryIdentifier identifier, NetworkCredential credential, TimeSpan bindLimit) : base(identifier, credential)
        {
            ServerTimeOut = bindLimit;

            Connect();
        }

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified directory identifier, network credentials, and authentication type.
        /// Server timeout is controlled from LDAP server settings.
        /// </summary>
        /// <param name="identifier">A <see cref="System.DirectoryServices.Protocols.LdapDirectoryIdentifier" /> object that specifies the server.</param>
        /// <param name="credential">A <see cref="System.Net.NetworkCredential" /> object that specifies the credentials to use.</param>
        /// <param name="authType">A <see cref="System.DirectoryServices.Protocols.AuthType" /> values that specifies the type of authentication to use.</param>
        public LdapConnector(LdapDirectoryIdentifier identifier, NetworkCredential credential, AuthType authType) : base(identifier, credential, authType)
        { Connect(); }

        /// <summary>
        /// constructor creates an instance of the <see cref="LdapConnector" /> class using the specified directory identifier, network credentials, and authentication type.
        /// Server timeou for bind and query can be configured here.
        /// </summary>
        /// <param name="identifier">A <see cref="System.DirectoryServices.Protocols.LdapDirectoryIdentifier" /> object that specifies the server.</param>
        /// <param name="credential">A <see cref="System.Net.NetworkCredential" /> object that specifies the credentials to use.</param>
        /// <param name="authType">A <see cref="System.DirectoryServices.Protocols.AuthType" /> values that specifies the type of authentication to use.</param>
        /// <param name="bindLimit"> A <see cref="System.TimeSpan"/> defining the LDAP server timeouts for bind and query operations.</param>
        public LdapConnector(LdapDirectoryIdentifier identifier, NetworkCredential credential, AuthType authType, TimeSpan bindLimit) : base(identifier, credential, authType)
        {
            ServerTimeOut = bindLimit;

            Connect();
        }

        #endregion

        #region methods

        /// <summary>
        /// Call 'Connect()' method from underlying LdapConnection class and set field 'connected' to true.
        /// We need the connection to be established before we can use the ldapHandle to set option LDAP_OPT_TIMELIMIT.
        /// </summary>
        public void Connect()
        {
            // get private method 'Connect()' from LdapConnection
            MethodInfo mi = typeof(LdapConnection).GetMethod("Connect",
                                                             BindingFlags.Instance | BindingFlags.NonPublic,
                                                             null,
                                                             CallingConventions.Any,
                                                             Type.EmptyTypes,
                                                             null);

            if (mi != null)
            {
                // invoke 'Connect()' method on the underlying LdapConnection object
                mi.Invoke(this, null);

                // get internal field (bool) 'connected'
                FieldInfo fi = typeof(LdapConnection).GetField("connected", BindingFlags.Instance | BindingFlags.NonPublic);

                // set field 'connected' to true
                fi.SetValue(this, true);

                ApplyServerTimeOut();
            }

            else
            { throw new NullReferenceException("Could not load method Connect from System.DirectoryServices.Protocols.LdapConnection"); }
        }

        /// <summary>
        /// override 'Bind()' method from underlying LdapConnection class
        /// </summary>
        public new void Bind()
        {
            // set option LDAP_OPT_TIMELIMIT
            ApplyServerTimeOut();

            // call 'Bind()' method from underlying LdapConnection object 
            base.Bind();
        }

        /// <summary>
        /// override 'Bind(NetworkCredential newCredential)' method from underlying LdapConnection class
        /// </summary>
        /// <param name="newCredential"></param>
        public new void Bind(NetworkCredential newCredential)
        {
            // set option LDAP_OPT_TIMELIMIT
            ApplyServerTimeOut();

            // call 'Bind(NetworkCredential newCredential)' method from underlying LdapConnection object 
            base.Bind(newCredential);
        }

        /// <summary>
        /// Implements ldap_set_option call with option = LDAP_OPT_TIMELIMIT.
        /// Controls LDAP server timeouts for bind and query
        /// </summary>
        public void ApplyServerTimeOut()
        {
            // anything to commit?
            if (ServerTimeOut.TotalSeconds != inCurrentServerTimeOut.TotalSeconds)
            {
                // get internal field 'ldapHandle' from LdapConnection 
                FieldInfo fi = typeof(LdapConnection).GetField("ldapHandle", BindingFlags.Instance | BindingFlags.NonPublic);

                if (fi != null)
                {
                    // get ldapHandle value
                    object rawhandle = fi.GetValue(this);

                    // pointer to ldapHandle - to be used in ldap_set_option
                    IntPtr hndl = IntPtr.Zero;

                    // depending on the targeted .Net framework we may recieve:
                    //  - until .Net 3.5 -> IntPtr
                    //  - from .net 4.0 -> ConnectionHandle : SafeHandleZeroOrMinusOneIsInvalid : SafeHandle
                    // therefore we need to treat the returned value differently

                    // returned value is IntPtr -> assign directly to hndl
                    if (rawhandle is IntPtr)
                    { hndl = (IntPtr)rawhandle; }

                    // returned value is SafeHandle -> get internal field 'handle' from returned SafeHandle object
                    else if (rawhandle is SafeHandle)
                    {
                        // get internal field 'handle' from returned SafeHandle object 
                        FieldInfo sfi = rawhandle.GetType().GetField("handle", BindingFlags.Instance | BindingFlags.NonPublic);

                        if (sfi != null)
                        {
                            // get IntPtr value from internal field 'handle' from returned SafeHandle object
                            hndl = (IntPtr)sfi.GetValue(rawhandle);
                        }
                    }

                    // calculate timeout for API call
                    int newval = (int)(inServerTimeOut.TotalSeconds);

                    if (hndl != IntPtr.Zero)
                    {
                        // call API ldap_set_option with LDAP_OPT_TIMELIMIT and timeout value to set
                        int ret = ldap_set_option(hndl, LdapOption.LDAP_OPT_TIMELIMIT, ref newval);

                        if (ret == 0)
                        { inCurrentServerTimeOut = new TimeSpan(0, 0, newval); }

                        else
                        { throw new LdapException(ret, string.Format("ldap_set_option_int(LDAP_OPT_TIMELIMIT = {0} failed", inServerTimeOut.TotalSeconds)); }
                    }


                    else
                    { throw new Exception("Failed to retrieve ldap handle from connection"); }
                }
            }
        }

        #endregion

        #region PInvoke

        /// <summary>
        /// see https://msdn.microsoft.com/en-us/library/aa366993(v=vs.85).aspx
        /// </summary>
        /// <param name="ldapHandle">handle to the established LDAP connection</param>
        /// <param name="option">LdapOption to be set</param>
        /// <param name="inValue">Value to be set for the defined LdapOption</param>
        /// <returns></returns>
        [DllImport("wldap32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "ldap_set_optionW")]
        internal static extern int ldap_set_option([In] IntPtr ldapHandle, [In] LdapOption option, ref int inValue);

        #endregion
    }
}
