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

using System.DirectoryServices.Protocols;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DomainControllerHelper : NamingContextsInfo
    {
        #region internal fields

        private string name;

        private string ntds;

        private bool gc;

        private string domdns;

        private string sitesrv;

        private List<string> controls;

        #endregion

        #region public fields

        public static string[] UDPAttributes
        {
            get
            {
                return new String[] { "defaultNamingContext", 
                                      "schemaNamingContext", 
                                      "configurationNamingContext", 
                                      "rootDomainNamingContext", 
                                      "namingContexts", 
                                      "dnsHostName", 
                                      "dsServiceName", 
                                      "isGlobalCatalogReady", 
                                      "ldapServiceName", 
                                      "serverName", 
                                      "supportedControl"};
            }
        }
        
        public bool FromUDPPing { get; set; }

        public string Name
        {
            get { return name.ToLowerInvariant(); }
            set { name = value; }
        }

        public string NTDSObject
        {
            get { return ntds; }
            set { ntds = value; }
        }

        public string QueryPolicyObject { get; set; }

        public bool IsGC
        {
            get { return gc; }
            set { gc = value; }
        }

        public string DomainName
        {
            get { return domdns; }
            set { domdns = value; }
        }

        public string SiteServerObject
        {
            get { return sitesrv; }
            set { sitesrv = value; }
        }

        public List<string> SupportedControls
        {
            get { return controls; }

            set { controls = value; }
        }

        public List<string> SupportedControlsDecoded
        { get { return DecodeControls(); } }

        public QueryPolicy LdapLimits { get; set; }

        public bool Loaded { get; set; }
        public bool Pinged { get; set; }
        public bool PingSuccess { get; set; }
        public bool UDPPinged { get; set; }
        public bool UDPPingSuccess { get; set; }

        #endregion

        #region constructor

        public DomainControllerHelper() : base()
        { }

        public DomainControllerHelper(string domainDNS, string dcName) : base()
        {
            Name = dcName;
            DomainName = domainDNS;
            Success = true;
            FromUDPPing = false;
            Loaded = false;
            Pinged = false;
            PingSuccess = false;
            UDPPinged = false;
            UDPPingSuccess = false;

            // Load(UDPAttributes);
        }
        
        public DomainControllerHelper(string dcName): base()
        { Name = dcName; Load(UDPAttributes); }

        #endregion

        #region methods

        public static DomainControllerHelper UDPPing(string dcName = null)
        {
            DomainControllerHelper ret = new DomainControllerHelper() { Name = dcName, FromUDPPing = true };

            ret.Load(UDPAttributes);

            return ret;
        }

        public static DomainControllerHelper FromStruct(NetApi32.DOMAIN_CONTROLLER_INFO dcInfo)
        {
            DomainControllerHelper ret = new DomainControllerHelper() { Name = dcInfo.DomainControllerName.Replace("\\\\", null), FromUDPPing = false };

            //ret.Load(UDPAttributes);

            return ret;
        }

        public void Load(string[] attributes)
        {
            Success = false;
            UDPPinged = true;
            
            try
            {
                LdapDirectoryIdentifier ldapid = new LdapDirectoryIdentifier(Name, 389, true, true);

                LdapConnection ldapcon = new LdapConnection(ldapid);
                ldapcon.Timeout = new TimeSpan(0, 2, 30);

                SearchRequest request = new SearchRequest("",
                                                          "(objectClass=*)",
                                                          SearchScope.Base,
                                                          attributes);

                SearchResponse response = (SearchResponse)ldapcon.SendRequest(request);

                foreach (SearchResultEntry entry in response.Entries)
                {
                    entry.GetStringAttributeSafe("defaultNamingContext", out defNC);

                    entry.GetStringAttributeSafe("configurationNamingContext", out confNC);

                    entry.GetStringAttributeSafe("schemaNamingContext", out schemaNC);

                    entry.GetStringAttributeSafe("rootDomainNamingContext", out rootNC);

                    entry.GetStringAttributeSafe("namingContexts", out allNCs);

                    entry.GetStringAttributeSafe("dnsHostName", out name);

                    entry.GetStringAttributeSafe("dsServiceName", out ntds);

                    GetQueryPolicy();

                    entry.GetStringAttributeSafe("serverName", out sitesrv);

                    entry.GetStringAttributeSafe("supportedControl", out controls);

                    string gcstring = String.Empty;

                    entry.GetStringAttributeSafe("isGlobalCatalogReady", out gcstring);

                    bool.TryParse(gcstring, out gc);

                    entry.GetStringAttributeSafe("ldapServiceName", out domdns);

                    if (domdns.Length > 0)
                    { domdns = domdns.Split(new char[] { '@' })[1]; }
                }

                Success = true;
                UDPPingSuccess = true;
            }

            catch (Exception ex)
            { ErrorString = ex.Message; }
        }

        public void GetQueryPolicy()
        {
            if ((ntds != null) && (ntds.Length != 0))
            {
                QueryPolicyObject = ForestBase.NTDSSettings.GetValueSafe(ntds);

                if (QueryPolicyObject != null)
                { LdapLimits = ForestBase.QueryPolicies.GetValueSafe(QueryPolicyObject); }
            }
        }

        public List<string> DecodeControls()
        {
            List<string> ret = new List<string>();

            Dictionary<string, string> dyndict = ForestBase.ADHelperDynamicDLL.DictionaryList.GetValueSafe<string, Dictionary<string, string>>("SUPPORTED_CONTROL");

            if (dyndict != null)
            {
                try
                {
                    foreach (string value in controls)
                    {
                        if (dyndict.ContainsKey(value))
                        { ret.Add(String.Format("\t{0} ({1})", dyndict[value], value)); }

                        else
                        { ret.Add(String.Format("\t<not decoded>: ({0})", value)); }
                    }
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            return ret;
        }

        public List<string> Print()
        {
            List<string> ret = new List<string> { };

            ret.Add(String.Format("Name:{0}\t{1}{0}", Environment.NewLine, Name));
            ret.Add(String.Format("IsGC:{0}\t{1}{0}", Environment.NewLine, IsGC.ToString()));
            ret.Add(String.Format("DomainName:{0}\t{1}{0}", Environment.NewLine, DomainName));
            ret.Add("LdapAdminLimits");
            ret.AddRange(LdapLimits.Print("\t"));
            ret.Add("");
            ret.Add(String.Format("SiteServerObject:{0}\t{1}{0}", Environment.NewLine, SiteServerObject));
            ret.Add(String.Format("NTDSObject:{0}\t{1}{0}", Environment.NewLine, NTDSObject));
            ret.Add(String.Format("QueryPolicyObject:{0}\t{1}{0}", Environment.NewLine, QueryPolicyObject));            
            ret.Add("SupportedControls");
            if (SupportedControls != null)
            { ret.AddRange(SupportedControlsDecoded); }

            return ret;
        }

        public override string ToString()
        { return Name; }

        #endregion
    }
}
