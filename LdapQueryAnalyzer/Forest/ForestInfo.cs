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

using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class ForestInfo : SchemaLoader
    {
        #region fields
        
        // see ADBase

        #endregion

        #region constructors
        public ForestInfo() : base()
        {  }

        #endregion

        #region methods

        public DomainControllerHelper GetDC(string domainDNS)
        {
            DomainControllerHelper ret = default(DomainControllerHelper);

            ret = DCLocator.GetClostesDC(domainDNS);

            return ret;
        }

        public List<DomainControllerHelper> GetDcList(string domainDNS, string ldapPath)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DCLocator.DnsGetDomaindDCs(domainDNS, false);

            return ret;
        }

        public bool Load(string domainDNS = null, Credentials userCreds = null, DateTime startUpdate = default(DateTime))
        {
            bool ret = false;

            base.UpdateStarted = startUpdate;

            ForestBase.Loading = true;

            ForestBase.CurrentDomain = domainDNS;

            ForestBase.GivenCreds = userCreds;

            PreLoad();
            
            ret = LoadCurrentDomain(domainDNS);

            if (ret)
            {
                (new Thread(TrustThread)).Start();

                (new Thread(SchemaThread)).Start();

                (new Thread(DomainsThread)).Start();

                int fincnt = 0;

                while (!base.HasError)
                {
                    WaitHandle.WaitAny(base.Signaler);

                    fincnt++;

                    if (fincnt > 1)
                    { break; }
                }                                
            }

            PostLoad();

            GlobalEventHandler.RaiseFinishedDiscovering();

            return ret;
        }

        public DomainController GetDcByName(string dcName)
        {
            DomainController ret = null;

            try
            {
                DirectoryContext ctx = new DirectoryContext(DirectoryContextType.DirectoryServer, dcName);

                ret = DomainController.FindOne(ctx);
            }

            catch (Exception ex)
            { base.SetError(String.Format("GetDcByName {0}: {1}", dcName, ex.Message)); }

            return ret;
        }

        private void PreLoad()
        {
            ForestBase.DomainDCs = new Dictionary<string, List<string>>(StringComparer.InvariantCultureIgnoreCase) { };

            ForestBase.DCList = new Dictionary<string, DomainControllerHelper>(StringComparer.InvariantCultureIgnoreCase) { };

            ForestBase.NCsToDCs = new Dictionary<string, List<string>>(StringComparer.InvariantCultureIgnoreCase) { };

            ForestBase.Trusts = new Dictionary<string, TrustRelationshipInformation> { };

            ForestBase.DomainSids = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

            ForestBase.QueryPolicies = new Dictionary<string, QueryPolicy>(StringComparer.InvariantCultureIgnoreCase) { };

            ForestBase.NTDSSettings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

            base.Signaler = new AutoResetEvent[2] { new AutoResetEvent(false),
                                               new AutoResetEvent(false) };
        }

        private void PostLoad()
        {
            foreach (AutoResetEvent ar in base.Signaler)
            { ar.InvokeSafe("Dispose"); }
        }

        private bool LoadCurrentDomain(string domainDNS)
        {
            bool ret = false;

            DateTime starttime = DateTime.Now;

            ForestBase.DefaultDC = GetDC(domainDNS);

            ForestBase.DefaultDC.Load(DomainControllerHelper.UDPAttributes);

            ret = ForestBase.DefaultDC.Success;

            if (ret)
            {
                GlobalEventHandler.RaiseUDPPingProceeded((long)DateTime.Now.Subtract(starttime).TotalMilliseconds, (long)DateTime.Now.Subtract(base.UpdateStarted).TotalMilliseconds);
                starttime = DateTime.Now;

                ForestBase.RootDomainNamingContext = ForestBase.DefaultDC.RootDomainNamingContext;
                ForestBase.SchemaNamingContext = ForestBase.DefaultDC.SchemaNamingContext;
                ForestBase.ConfigurationNamingContext = ForestBase.DefaultDC.ConfigurationNamingContext;

                ForestBase.CurrentDomain = ForestBase.DefaultDC.DefaultNamingContext;

                ForestBase.CurrentDC = ForestBase.DefaultDC.Name;

                ret = LoadConfig();

                if (ret)
                { ret = LoadForestName(); }

                if (ret)
                {
                    ForestBase.DefaultDC.GetQueryPolicy();

                    StoreDC(ForestBase.DefaultDC, ForestBase.DefaultDC.DefaultNamingContext);

                    GlobalEventHandler.RaiseDiscoveredCurrentDomain((long)DateTime.Now.Subtract(starttime).TotalMilliseconds, (long)DateTime.Now.Subtract(base.UpdateStarted).TotalMilliseconds);

                    GlobalEventHandler.RaiseDiscoveredCurrentDomainNCs((long)0, (long)DateTime.Now.Subtract(base.UpdateStarted).TotalMilliseconds);
                }
            }

            else
            {
                if (domainDNS == null)
                { domainDNS = "current domain"; }

                base.SetError(String.Format("LoadCurrentDomain: Could not detect a DC in {0}", domainDNS));

                GlobalEventHandler.RaiseFinishedDiscovering();
            }

            return ret;
        }

        private bool LoadForestName()
        {
            bool ret = false;

            string domdns = null;

            SearchRequest request = new SearchRequest(ForestBase.RootDomainNamingContext, "(objectClass=*)", SearchScope.Base, new string[] { "canonicalName", "objectSid" });

            LdapDirectoryIdentifier ldapid = new LdapDirectoryIdentifier(ForestBase.DefaultDC.Name, 389, true, false);

            using (LdapConnection ldapcon = new LdapConnection(ldapid))
            {
                if ((ForestBase.GivenCreds != null) && (ForestBase.GivenCreds.HasCreds))
                { ldapcon.Credential = ForestBase.GivenCreds.NetCreds; }
                
                try
                { 
                    ldapcon.Bind();

                    SearchResponse response = (SearchResponse)ldapcon.SendRequest(request);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        domdns = null;

                        entry.GetStringAttributeSafe("canonicalName", out domdns);

                        ForestBase.ForestName = domdns.TrimChar("/");

                        List<DomainControllerHelper> dclist = GetDcList(ForestBase.ForestName, entry.DistinguishedName);

                        if ((dclist.Count == 1) && (dclist[0].Success == false))
                        {
                            string temp = domdns;

                            if (temp == null)
                            { temp = "current domain"; }

                            base.SetError(String.Format("LoadForestName: Could not detect a DC in {0}", temp)); 
                        }

                        else
                        {
                            foreach (DomainControllerHelper dchelper in dclist)
                            {
                                if (dchelper.Success)
                                {
                                    StoreDC(dchelper, entry.DistinguishedName);

                                    ret = true;
                                }
                            }

                            List<byte[]> bsids;

                            entry.GetAttributeSafe("objectSid", out bsids);

                            SecurityIdentifier dsid = new SecurityIdentifier(bsids[0], 0);

                            ForestBase.DomainSids.AddSafe(entry.DistinguishedName, dsid.ToString());
                        }
                    }
                }
                
                catch (Exception ex)
                {
                    base.SetError(String.Format("LoadForestName: {0} ({1})", ex.Message, ex.GetType().Name));
                }                
            }

            if (ret && (ForestBase.DCList.Count == 0))
            {
                base.SetError("LoadForestName: Could not detect a DC in current forest");

                ret = false;

                GlobalEventHandler.RaiseFinishedDiscovering();
            }

            return ret;
        }

        private bool LoadConfig()
        {
            bool ret = false;

            SearchRequest request = new SearchRequest(ForestBase.ConfigurationNamingContext, 
                                                      "(|(objectClass=nTDSDSA)(objectClass=queryPolicy))", 
                                                      SearchScope.Subtree, 
                                                      new string[] { "objectClass", "whenCreated", "queryPolicyObject", "lDAPAdminLimits" });

            LdapDirectoryIdentifier ldapid = new LdapDirectoryIdentifier(ForestBase.DefaultDC.Name, 389, true, false);
                        
            using (LdapConnection ldapcon = new LdapConnection(ldapid))
            {
                try
                {
                    if ((ForestBase.GivenCreds != null) && (ForestBase.GivenCreds.HasCreds))
                    { ldapcon.Credential = ForestBase.GivenCreds.NetCreds; }

                    ldapcon.Bind();

                    SearchResponse response = (SearchResponse)ldapcon.SendRequest(request);

                    List<SearchResultEntry> ntds = new List<SearchResultEntry> { };

                    List<QueryPolicy> policies = new List<QueryPolicy> { };

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        List<string> classes = new List<string> { };

                        entry.GetStringAttributeSafe("objectClass", out classes);

                        if (classes.Contains("nTDSDSA"))
                        { ntds.Add(entry); }

                        else if (classes.Contains("queryPolicy"))
                        { policies.Add(new QueryPolicy(entry)); }
                    }

                    policies.OrderByField("WhenCreated", false);

                    foreach (QueryPolicy pol in policies)
                    { ForestBase.QueryPolicies.AddSafe(pol.DN, pol); }

                    foreach (SearchResultEntry entry in ntds)
                    {
                        string qpol = String.Empty;

                        entry.GetStringAttributeSafe("queryPolicyObject", out qpol);

                        if (qpol != String.Empty)
                        { ForestBase.NTDSSettings.AddSafe(entry.DistinguishedName, qpol); }

                        else
                        { ForestBase.NTDSSettings.AddSafe(entry.DistinguishedName, policies[0].DN); }
                    }

                    ret = true;
                }

                catch (Exception ex)
                { SetError(ex.Message); }
            }

            return ret;
        }
        
        private void TrustThread()
        {
            ForestBase.Trusts.Clear();

            try
            {
                Domain userdomain = Domain.GetCurrentDomain();

                TrustRelationshipInformationCollection trustlist = userdomain.Forest.GetAllTrustRelationships();

                if (trustlist != null)
                {
                    foreach (ForestTrustRelationshipInformation trust in trustlist)
                    {
                        ForestBase.Trusts.AddSafe(trust.TargetName, trust);
                    }
                }

                trustlist = userdomain.GetAllTrustRelationships();

                if (trustlist != null)
                {
                    foreach (TrustRelationshipInformation trust in trustlist)
                    {
                        ForestBase.Trusts.AddSafe(trust.TargetName, trust);
                    }
                }
            }

            catch { }

            //foreach (TrustRelationshipInformation trust in Forest.GetForest(new DirectoryContext(DirectoryContextType.Forest, ForestBase.ForestName)).GetAllTrustRelationships())
            //{
            //    ForestBase.Trusts.AddSafe(trust.TargetName, trust.TrustDirection);
            //}

            //foreach (TrustRelationshipInformation trust in Domain.GetDomain(new DirectoryContext(DirectoryContextType.Domain, ForestBase.CurrentDomain)).GetAllTrustRelationships())
            //{
            //    ForestBase.Trusts.AddSafe(trust.TargetName, trust.TrustDirection);
            //}
        }

        private void SchemaThread()
        {
            if (base.HasError) return;

            Stopwatch stp = new Stopwatch();

            stp.Start();

            GetAggregateSchema(ForestBase.DefaultDC.Name);

            stp.Stop();
            base.Signaler[0].Set();

            GlobalEventHandler.RaiseDiscoveredSchema(stp.ElapsedMilliseconds, (long)DateTime.Now.Subtract(base.UpdateStarted).TotalMilliseconds);
        }

        private void DomainsThread()
        {
            if (base.HasError) return;

            Stopwatch stp = new Stopwatch();

            stp.Start();

            LoadDomains();

            stp.Stop(); ;
            base.Signaler[1].Set();

            GlobalEventHandler.RaiseDiscoveredForest(stp.ElapsedMilliseconds, (long)DateTime.Now.Subtract(base.UpdateStarted).TotalMilliseconds);
            //GlobalEventHandler.RaiseDomainsUpdated(stp.ElapsedMilliseconds, (long)DateTime.Now.Subtract(UpdateStarted).TotalMilliseconds);
        }

        private void LoadDomains()
        {
            SearchRequest request = new SearchRequest(null, "(objectClass=domainDNS)", SearchScope.Subtree, new string[] { "canonicalName", "objectSid" });

            PageResultRequestControl pagecookie = new PageResultRequestControl(1000);
            request.Controls.Add(pagecookie);

            SearchOptionsControl phantomcookie = new SearchOptionsControl(SearchOption.PhantomRoot);
            request.Controls.Add(phantomcookie);

            LdapDirectoryIdentifier ldapid = new LdapDirectoryIdentifier(ForestBase.DefaultDC.Name, 389, true, false);

            using (LdapConnection ldapcon = new LdapConnection(ldapid))
            {
                if ((ForestBase.GivenCreds != null) && (ForestBase.GivenCreds.HasCreds))
                { ldapcon.Credential = ForestBase.GivenCreds.NetCreds; }

                ldapcon.SessionOptions.ReferralChasing = ReferralChasingOptions.All;

                ldapcon.Bind();

                SearchResponse response = (SearchResponse)ldapcon.SendRequest(request);

                foreach (SearchResultEntry entry in response.Entries)
                {
                    if (entry.DistinguishedName.ToLowerInvariant() != ForestBase.RootDomainNamingContext.ToLowerInvariant())
                    {
                        string domdns = null;

                        entry.GetStringAttributeSafe("canonicalName", out domdns);

                        domdns = domdns.TrimChar("/");

                        foreach (DomainControllerHelper dchelper in GetDcList(domdns, entry.DistinguishedName))
                        { StoreDC(dchelper, entry.DistinguishedName); }
                        
                        List<byte[]> bsids;

                        entry.GetAttributeSafe("objectSid", out bsids);

                        SecurityIdentifier dsid = new SecurityIdentifier(bsids[0], 0);

                        ForestBase.DomainSids.AddSafe(entry.DistinguishedName, dsid.ToString());
                    }
                }
            }
        }

        #endregion
    }
}
