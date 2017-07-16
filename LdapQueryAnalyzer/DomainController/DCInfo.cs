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
using System.Reflection;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DCInfo
    {
        #region fields

        public bool Success = false;

        public string ErrorString { get; set; }
        public string DCName { get; set; }
        public string DCAddress { get; set; }
        public Guid DomainGUID { get; set; }
        public string DomainName { get; set; }
        public string DNSForestname { get; set; }
        public string DCSiteName { get; set; }

        public NamingContextsInfo NCInfo { get; set; }

        public string ClientSiteName { get; set; }
        public bool IsIPAddress { get; set; }
        public bool IsNetBIOSAddress { get; set; }
        public bool IsPDC { get; set; }
        public bool IsGC { get; set; }
        public bool IsLDAPServer { get; set; }
        public bool IsDC { get; set; }
        public bool IsKDC { get; set; }
        public bool IsTimerServer { get; set; }
        public bool IsInClosestSite { get; set; }
        public bool IsWritable { get; set; }
        public bool IsGoodTimerServer { get; set; }
        public bool IsNonDomainNC { get; set; }
        public bool HasSelectedSecrets { get; set; }
        public bool HasAllSecrets { get; set; }
        public bool HasWebService { get; set; }
        public bool DCIsDNSName { get; set; }
        public bool DomainIsDNSName { get; set; }

        public bool IsDirectoryService2012 { get; set; }
        public bool IsDirectoryService2012R2 { get; set; }

        #endregion

        #region methods

        public string PrintFields()
        {

            string sRet = String.Empty;


            foreach (PropertyInfo oField in this.GetType().GetProperties())
            {
                switch (oField.Name)
                {

                    case "Success":
                    case "ErrorString":

                        if (!Success)
                        {
                            sRet = string.Format("{0}{1} = {2}", (sRet.Length == 0) ? null : string.Format("{0}{1}", sRet, Environment.NewLine), oField.Name, (oField.GetValue(this, null) == null) ? null : oField.GetValue(this, null).ToString());

                        }

                        break;
                    default:

                        if (Success)
                        {
                            sRet = string.Format("{0}{1} = {2}", (sRet.Length == 0) ? null : string.Format("{0}{1}", sRet, Environment.NewLine), oField.Name, (oField.GetValue(this, null) == null) ? null : oField.GetValue(this, null).ToString());

                        }

                        break;

                }

            }

            return sRet;

        }

        #endregion
    }
}
