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
using System.Security;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class Credentials
    {
        #region fields

        public string ForestName;
        public string DomainName = null;
        public string Sam = null;
        public string UserName = null;

        public bool ElevateThis = false;
        public bool RestartThis = false;

        public bool HasCreds { get { return CheckCreds(); } }

        public bool HasError = false;
        public string ErrorMsg = String.Empty;

        public NetworkCredential NetCreds = null;

        public SecureString Pwd = new SecureString();

        #endregion

        #region constructor

        public Credentials()
        { }

        public Credentials(string domainName, string userName, ref string passWord, bool elevate, bool restart)
        {
            DomainName = domainName;
            Sam = userName;

            SetPassword(ref passWord); ;

            ElevateThis = elevate;
            RestartThis = restart;
        }

        public Credentials(string forestName, string domainName, string userName, ref string passWord)
        {
            ForestName = forestName;
            DomainName = domainName;
            Sam = userName;

            SetPassword(ref passWord); ;
        }

        #endregion

        #region methods

        public void SetPassword(ref string passWord)
        {
            Pwd = passWord.ToSecureStringSecure(ref passWord);

            try
            { NetCreds = new NetworkCredential(Sam, Pwd.ToUnsecureString(), DomainName); }

            catch (Exception ex)
            {
                HasError = true;

                ErrorMsg = ex.Message;
            }
            
            if (DomainName == null)
            { DomainName = String.Empty; }

            UserName = (DomainName.Length != 0) ? DomainName + "\\" + Sam : Sam;
        }

        public bool ValidateCreds()
        {
            bool ret = false;

            LdapDirectoryIdentifier ldapid = null;

            LdapConnector ldapcheck = null;

            try
            {
                ldapid = new LdapDirectoryIdentifier(ForestName, false, false);

                ldapcheck = new LdapConnector(ldapid);

                ldapcheck.AutoBind = false;
                ldapcheck.AuthType = AuthType.Basic;

                ldapcheck.SessionOptions.FastConcurrentBind();

                ldapcheck.Bind(NetCreds);

                ret = true;
            }

            catch (Exception ex)
            {
                ErrorMsg = String.Format("{0} ({1}\\{2})", ex.Message, DomainName, UserName);
            }

            finally
            {
                try
                { ldapcheck.Dispose(); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            HasError = !ret;

            return ret;
        }

        private bool CheckCreds()
        {
            bool ret = false;

            if (UserName != null)
            {
                if (Pwd.Length != 0)
                { ret = true; }
            }

            return ret;
        }

        #endregion
    }
}
