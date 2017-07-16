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
using System.Linq;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Reflection;
using System.Threading;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class LDAPHelper : SchemaCache
    {
        #region fields
        // see ADBase
        #endregion

        #region constructor

        public LDAPHelper() : base() 
        { GlobalEventHandler.AsyncPartialResult += AsyncPartialResult; }

        #endregion

        #region methods
        
        public void Disconnect()
        {
            try
            {
                base.Connection.InvokeSafe("Dispose");
                base.Connection = null;

                base.IsConnected = false;

                base.AsyncConnection.InvokeSafe("Dispose");
                base.AsyncConnection = null;
            }

            catch (Exception ex)
            { ex.ToDummy(); }            
        }

        private void Connect(string dc,
                              ReferralChasingOptions referralChasing, 
                              bool isAsync = false, 
                              bool connectionLess = false)
        {
            int port = ForestBase.CurrentPorts.SelectedPort;

            base.IsConnected = false;

            string inex = null;
            LdapConnection tempcon = null;

            try
            {
                LdapDirectoryIdentifier ldapid = new LdapDirectoryIdentifier(dc, port, true, connectionLess);

                tempcon = GetLdapConnection(ldapid, ForestBase.GivenCreds);

                tempcon.SessionOptions.ReferralChasing = referralChasing;

                tempcon.Timeout = TimeSpan.FromSeconds((double)ForestBase.CurrentTimeOut); //new TimeSpan(0, 2, 0);
                

                if (!isAsync)
                { base.Connection = tempcon; }

                else
                { base.AsyncConnection = tempcon; }

                base.IsConnected = true;
            }

            catch (Exception ex)
            { inex = "Connect LDAP: " + ex.Message; }

            if (inex != null)
            { base.SetError(inex); }
        }


        public List<SearchResultEntry> Query(string dc,
                                             string searchBase,
                                             string ldapFilter,
                                             string[] propertiesToLoad,
                                             SearchScope scope,
                                             ReferralChasingOptions referralChasing,
                                             QueryControl queryInfo, 
                                             string[] attributesRemebered = null, 
                                             bool returnResults = false)
        {
            CancelToken = false;

            GlobalEventHandler.ClearSearchCancelled();

            GlobalEventHandler.SearchCancelled += ReceivedCancellation;

            List<SearchResultEntry> ret = new List<SearchResultEntry> { };

            List<SearchResultEntry> fire = new List<SearchResultEntry> { };

            QueryHasConstructedAttribute(propertiesToLoad, scope, ref queryInfo);

            if (queryInfo.MustGetSingleObjectPath)
            {
                GetSingleObjectPaths(dc,
                                     searchBase,
                                     ldapFilter,
                                     propertiesToLoad,
                                     scope,
                                     referralChasing,
                                     queryInfo);
                return ret;
            }

            ForestBase.CurrentPorts.SelectedPort = queryInfo.Port;

            byte[] pagingCookie = null;

            SearchRequestExtender reqstore = new SearchRequestExtender(searchBase,
                                                                       ldapFilter,
                                                                       propertiesToLoad,
                                                                       scope,
                                                                       pagingCookie,
                                                                       queryInfo);

            reqstore.DC = dc;

            reqstore.ReferralChasing = referralChasing;

            propertiesToLoad = reqstore.Attributes;

            while (true)
            {
                if (!reqstore.HasError)
                { reqstore.PageCount++; }

                SearchResultEntryCollection colresult = null;

                if (!CancelToken)
                {
                    if (!queryInfo.PerformDirSync)
                    {
                        DateTime starttime = DateTime.Now;

                        colresult = PagedQuery(ref reqstore);

                        if (reqstore.CurrentPageSize > 0)
                        { GlobalEventHandler.RaiseMessageOccured(String.Format("Page: {0} ({1}) [{2}] ms]", reqstore.PageCount, reqstore.CurrentPageSize, DateTime.Now.Subtract(starttime).TotalMilliseconds)); }
                    }

                    else
                    { colresult = DirSyncQuery(ref reqstore); }
                }

                else 
                { break; }

                if (reqstore.HasError)
                { break; }

                if ((colresult != null) && (colresult.Count > 0) && ((colresult[0].DistinguishedName.Length != 0) ||(searchBase == "")))
                {
                    SearchResultEntry[] temp = new SearchResultEntry[colresult.Count];

                    colresult.CopyTo(temp, 0);

                    fire = temp.ToList();

                    if (returnResults)
                    { ret.AddRange(fire); }
                }

                if ((queryInfo.CurrentResultEventType == QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH) && (attributesRemebered != null))
                { reqstore.HandleAttributes(attributesRemebered); }

                if (reqstore.MoreData)
                {
                    if ((fire.Count > 0) && !returnResults)
                    { GlobalEventHandler.RaiseQueryCompleted(fire, reqstore, QUERY_RESULT_EVENT_TYPE.IS_PARTIAL | queryInfo.CurrentResultEventType); }

                    if ((queryInfo.CurrentResultEventType == QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH) && (attributesRemebered != null))
                    { reqstore.HandleAttributes(propertiesToLoad); }
                }

                else
                { break; }
            }

            if (reqstore.DoPaging)
            {
                reqstore.AddMessage(String.Format("PageSize: {0}", reqstore.CurrentPageSize));
                reqstore.AddMessage(String.Format("Pages: {0}", reqstore.PageCount));
            }

            if (!returnResults)
            { GlobalEventHandler.RaiseQueryCompleted(fire, reqstore, QUERY_RESULT_EVENT_TYPE.IS_COMPLETED | queryInfo.CurrentResultEventType); }

            Disconnect();

            return ret;
        }

        private void GetSingleObjectPaths(string dc,
                                              string searchBase,
                                              string ldapFilter,
                                              string[] propertiesToLoad,
                                              SearchScope scope,
                                              ReferralChasingOptions referralChasing,
                                              QueryControl queryInfo)
        {
            GlobalEventHandler.RaiseErrorOccured(String.Format("Attribute list contains (a) constructed attribute(s) {0}\t-> changing to single object path base query", Environment.NewLine));

            if ((queryInfo.PhantomRoot) && (propertiesToLoad.ContainsSubString("tokengroups", MATCH_POSITION.STARTSWITH)))
            {
                GlobalEventHandler.RaiseErrorOccured(String.Format("Turning off PhantomRoot {0}\t-> attribute(s) contain tokenGroups*", Environment.NewLine));

                queryInfo.PhantomRoot = false;
            }

            GlobalEventHandler.QueryCompleted += QuerySingleObjectPaths;

            string[] attribs = new string[] { "cn" };

            Query(dc,
                  searchBase,
                  ldapFilter,
                  attribs,
                  scope,
                  referralChasing,
                  queryInfo, 
                  propertiesToLoad);
        }

        private void QuerySingleObjectPaths(object infoStore, GlobalEventArgs args)
        {
            if ((args.ResultEventType & QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH) != QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH)
            { return; }

            if ((args.ResultEventType & QUERY_RESULT_EVENT_TYPE.IS_COMPLETED) == QUERY_RESULT_EVENT_TYPE.IS_COMPLETED)
            { GlobalEventHandler.QueryCompleted -= QuerySingleObjectPaths; }

            SearchRequestExtender reqstore = (SearchRequestExtender)infoStore;

            if (!reqstore.HasError)
            { GlobalEventHandler.RaiseErrorOccured(String.Format("Found {0} paths to handle", args.Entries.Count)); }

            reqstore.QueryInfo.CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.IS_PARTIAL;

            int cnt = 0;

            foreach (SearchResultEntry entry in args.Entries)
            {
                cnt++;

                if ((cnt == args.Entries.Count) && (!reqstore.MoreData))
                { reqstore.QueryInfo.CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.NONE; }

                Query(reqstore.DC,
                      entry.DistinguishedName,
                      reqstore.LdapFilter,
                      reqstore.Attributes,
                      SearchScope.Base,
                      reqstore.ReferralChasing,
                      reqstore.QueryInfo);

                GlobalEventHandler.RaiseErrorOccured(String.Format("Subsequent query: #{0}", cnt)); 
            }
        }

        private SearchResultEntryCollection PagedQuery(ref SearchRequestExtender reqStore)
        {
            SearchResultEntryCollection ret = null;

            SearchResponse sresponse = null;

            bool goon = false;

            try
            {
                if ((base.IsConnected == false) || (base.Connection == null))
                { Connect(reqStore.DC, reqStore.ReferralChasing, connectionLess: reqStore.QueryInfo.RootDse); }

                sresponse = (SearchResponse)base.Connection.SendRequest(reqStore.Request);

                goon = true;
            }

            catch (LdapException ldapEx)
            {
                base.SetError(ldapEx.Message);

                reqStore.HasError = true;
                reqStore.ErrorMSG = ldapEx.Message;

                return null;
            }

            catch (DirectoryOperationException direx)
            {
                if (direx.Response != null)
                {
                    if (direx.Response.ResultCode == ResultCode.SizeLimitExceeded)
                    {
                        if (reqStore.QueryInfo.AutoPage)
                        {
                            GlobalEventHandler.RaiseErrorOccured(String.Format("Non-PagedQuery: {0} - switched to PagedQuery", direx.Response.ResultCode.ToString()));

                            reqStore.AddMessage(String.Format("Non-PagedQuery: {0} - switched to PagedQuery", direx.Response.ResultCode.ToString()));

                            reqStore.PageCookie(((SearchResponse)direx.Response).Entries.Count);

                            reqStore.MoreData = true;                            
                        }

                        else
                        {
                            sresponse = (SearchResponse)direx.Response;

                            GlobalEventHandler.RaiseErrorOccured(String.Format("\tNon-PagedQuery: {0} - returned first {1} entries", direx.Response.ResultCode.ToString(), sresponse.Entries.Count));

                            reqStore.AddMessage(String.Format("Non-PagedQuery: {0} - returned first {1} entries", direx.Response.ResultCode.ToString(), sresponse.Entries.Count));
                        }

                        goon = true;
                    }

                    else if ((direx.Response.ResultCode == ResultCode.UnavailableCriticalExtension) && 
                              direx.Response.ErrorMessage.StartsWith("00002040") &&
                              (reqStore.PageControl != null) && 
                              (reqStore.ReferralChasing != ReferralChasingOptions.None))
                    {
                        reqStore.PageCount--;

                        string msg = "Multiple page cookies from referrals.";

                        msg = String.Format("{0} ({1})", msg, direx.Message);

                        if (direx.Response.ErrorMessage != null)
                        { msg = String.Format("{0}: ({1})", msg, direx.Response.ErrorMessage); }

                        base.SetError(msg);

                        reqStore.HasError = true;

                        reqStore.ErrorMSG = base.ErrorMSG;

                        //goon = true;
                    }

                    else
                    {
                        string msg = direx.Message;

                        if (direx.Response.ErrorMessage != null)
                        { msg = String.Format("{0}: ({1})", msg, direx.Response.ErrorMessage); }

                        base.SetError(msg);

                        reqStore.HasError = true;

                        reqStore.ErrorMSG = base.ErrorMSG;
                    }
                }

                else // if (!goon)
                {
                    base.SetError(direx.Message);

                    reqStore.HasError = true;
                    reqStore.ErrorMSG = base.ErrorMSG;
                }                
            }

            catch (Exception ex)
            {
                base.SetError(ex.Message);

                reqStore.HasError = true;
                reqStore.ErrorMSG = ex.Message;

                return null;
            }

            if (goon)
            {
                if (sresponse != null)
                {
                    if (reqStore.RetreiveStatistics)
                    {
                        DirectoryControl dcStats = GetControl(sresponse.Controls, SearchRequestExtender.STATISTCS_CONTROL_OID);

                        if (dcStats != null)
                        { reqStore.Statistics.Add(new StatsData(dcStats.GetValue())); }

                        else
                        { GlobalEventHandler.RaiseErrorOccured("WARNING: No Query Statistics data returned"); }
                    }

                    if (reqStore.PageControl != null)
                    {
                        DirectoryControl pageRespControl = GetControl(sresponse.Controls, reqStore.PageControl.Type);

                        reqStore.UpdatePagingCookie(pageRespControl, sresponse.Entries.Count);
                    }
                    
                    ret = sresponse.Entries;
                }                
            }

            return ret;
        }

        private bool RunParallelQueries(string dc,
                                          string searchBase,
                                          string ldapFilter,
                                          ref string[] propertiesToLoad,
                                          SearchScope scope,
                                          ReferralChasingOptions referralChasing,
                                          QueryControl queryInfo,
                                          out SearchRequestExtender reqStore)
        {
            bool ret = false;

            reqStore = null;

            if ((searchBase == null) || (searchBase.Length == 0))
            { return ret; }

            reqStore = new SearchRequestExtender(searchBase,
                                                  ldapFilter,
                                                  propertiesToLoad,
                                                  scope,
                                                  null,
                                                  queryInfo);



            if ((scope == SearchScope.Subtree) && (queryInfo.PerformPagedQuery) && ((referralChasing & ReferralChasingOptions.Subordinate) == ReferralChasingOptions.Subordinate))
            { ret = true; }

            if (!ret)
            { return ret; }

            Dictionary<string, string> querylist = SubNCsToDCs(searchBase, dc);

            base.ParallelRuns = querylist.Count;

            if (base.ParallelRuns == 0)
            { return ret; }

            
            ret = true;

            referralChasing = ReferralChasingOptions.None;
            string[] attributes = propertiesToLoad;

            foreach(KeyValuePair<string, string> kvp in querylist)
            {
                base.RunningParallel = true;

                Thread thquery = new Thread(() => { AsyncQuery(kvp.Value, kvp.Key, ldapFilter, attributes, scope, referralChasing, queryInfo); });

                thquery.Start();
            }

            return ret;
        }

        private SearchResultEntryCollection DirSyncQuery(ref SearchRequestExtender reqStore)
        {
            SearchResultEntryCollection ret = null;

            try
            {
                SearchResponse sresponse = (SearchResponse)base.Connection.SendRequest(reqStore.Request);

                DirectoryControl dscontrol = GetControl(sresponse.Controls, reqStore.DirSyncControl.Type);

                reqStore.UpdateDirSyncCookie(dscontrol);

                ret = sresponse.Entries;
            }

            catch (LdapException ldapEx)
            {
                reqStore.HasError = true;
                reqStore.ErrorMSG = ldapEx.Message;

                return null;
            }

            catch (Exception ex)
            {
                reqStore.HasError = true;
                reqStore.ErrorMSG = ex.Message;

                return null;
            }

            return ret;
        }

        public void AsyncQuery(string dc,
                               string searchBase,
                               string ldapFilter,
                               string[] propertiesToLoad,
                               SearchScope scope,
                               ReferralChasingOptions referralChasing,
                               QueryControl queryInfo,
                               bool performASQ = false)
        {
            CancelToken = false;

            GlobalEventHandler.ClearSearchCancelled();
            
            GlobalEventHandler.SearchCancelled += ReceivedCancellation;

            byte[] pagingCookie = null;

            ForestBase.CurrentPorts.SelectedPort = queryInfo.Port;

            Connect(dc, referralChasing, true, connectionLess:(searchBase.Length == 0));

            if (propertiesToLoad.Length == 0)
            { propertiesToLoad = null; }

            else if ((propertiesToLoad.Length == 1) && (propertiesToLoad[0] == "*"))
            { propertiesToLoad = null; }

            ForestBase.CurrentRequestExtender = new SearchRequestExtender(searchBase,
                                                               ldapFilter,
                                                               propertiesToLoad,
                                                               scope,
                                                               pagingCookie,
                                                               queryInfo);


            base.AsyncComplete = false;
            base.AsyncCalls = 0;

            GlobalEventHandler.AsyncQureyIsCompleted += AsyncSearchComplete;

            PagedAsyncQuery();

        }

        private void PagedAsyncQuery()
        {
            bool success = false;

            try
            {
                base.AsyncCalls++;

                IAsyncResult asResult = base.AsyncConnection.BeginSendRequest(ForestBase.CurrentRequestExtender.Request,
                                                                                    PartialResultProcessing.ReturnPartialResultsAndNotifyCallback,
                                                                                    PagedAsyncQueryCallBackPartial,
                                                                                    null);

                success = true;
            }

            catch (LdapException ldapEx)
            {
                ForestBase.CurrentRequestExtender.HasError = true;
                ForestBase.CurrentRequestExtender.ErrorMSG = ldapEx.Message;                                
            }

            catch (Exception ex)
            {
                ForestBase.CurrentRequestExtender.HasError = true;
                ForestBase.CurrentRequestExtender.ErrorMSG = ex.Message;
            }

            if (!success)
            { GlobalEventHandler.RaiseAsyncPartialResult(null, true); }
        }

        private void PagedAsyncQueryCallBackPartial(IAsyncResult Result)
        {
            PartialResultsCollection pResults = null;

            SearchResultEntry[] temp = new SearchResultEntry[0];

            if (!Result.IsCompleted)
            {
                pResults = base.Connection.GetPartialResults(Result);

                if (pResults != null)
                {
                    temp = new SearchResultEntry[pResults.Count];

                    pResults.CopyTo(temp, 0);                    
                }
            }
            else
            {
                SearchResponse sresponse = null;

                try
                {
                    sresponse = (SearchResponse)base.AsyncConnection.EndSendRequest(Result);
                }

                catch (DirectoryOperationException direx)
                {
                    if (direx.Response != null)
                    {
                        if (direx.Response.ResultCode == ResultCode.SizeLimitExceeded)
                        {
                            if (ForestBase.CurrentRequestExtender.QueryInfo.AutoPage)
                            {
                                GlobalEventHandler.RaiseErrorOccured(String.Format("Non-PagedQuery: {0} - switched to PagedQuery", direx.Response.ResultCode.ToString()));

                                ForestBase.CurrentRequestExtender.AddMessage(String.Format("Non-PagedQuery: {0} - switched to PagedQuery", direx.Response.ResultCode.ToString()));

                                ForestBase.CurrentRequestExtender.PageCookie(((SearchResponse)direx.Response).Entries.Count);

                                ForestBase.CurrentRequestExtender.MoreData = true;

                                PagedAsyncQuery(); 
                            }
                        }
                    }

                }

                catch (Exception ex)
                { string x = ex.Message; }

                temp = new SearchResultEntry[sresponse.Entries.Count];

                sresponse.Entries.CopyTo(temp, 0);

                if (ForestBase.CurrentRequestExtender.RetreiveStatistics)
                {
                    DirectoryControl dcStats = GetControl(sresponse.Controls, SearchRequestExtender.STATISTCS_CONTROL_OID);

                    if (dcStats != null)
                    { ForestBase.CurrentRequestExtender.Statistics.Add(new StatsData(dcStats.GetValue())); }

                    else
                    { GlobalEventHandler.RaiseErrorOccured("WARNING: No Query Statistics data returned"); }
                }

                DirectoryControl pageRespControl = GetControl(sresponse.Controls, ForestBase.CurrentRequestExtender.PageControl.Type);

                ForestBase.CurrentRequestExtender.UpdatePagingCookie(pageRespControl, sresponse.Entries.Count);
                

                base.AsyncCalls--;
                base.ParallelRuns--;
            }

            GlobalEventHandler.RaiseAsyncPartialResult(temp.ToList(), !ForestBase.CurrentRequestExtender.MoreData);

            if (!CancelToken)
            {
                if (ForestBase.CurrentRequestExtender.MoreData)
                { PagedAsyncQuery(); }

                else if (base.ParallelRuns < 1)
                { GlobalEventHandler.RaiseParallelQueriesCompleted(); }
            }

            else { GlobalEventHandler.RaiseParallelQueriesCompleted(); }
        }
       

        public Dictionary<string, string> SubNCsToDCs(string nc, string currentDC)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { };

            Dictionary<string, DomainControllerHelper> temp = new Dictionary<string, DomainControllerHelper>(ForestBase.DCList);

            foreach (DomainControllerHelper dc in temp.Values)
            { 
                if (!dc.UDPPingSuccess)
                {
                    DomainControllerHelper tempdc = DomainControllerHelper.UDPPing(dc.Name);

                    StoreDC(tempdc, tempdc.DefaultNamingContext);
                }
            }

            foreach (KeyValuePair<string, List<string>> ncinfo in ForestBase.NCsToDCs.Where(n => n.Key.ToLowerInvariant().EndsWith(nc.ToLowerInvariant())))
            {
                if (ncinfo.Value.Contains(currentDC))
                { ret.AddSafe(ncinfo.Key, currentDC); }

                else
                { ret.AddSafe(ncinfo.Key, ncinfo.Value[0]); }
            }

            return ret;
        }

        public void StoreDC(DomainControllerHelper dcHelper, string domainDN, bool force = false)
        {
            if (dcHelper.Success)
            {
                ForestBase.DCList.AddSafe(dcHelper.Name, dcHelper, force);

                if ((!ForestBase.DCList[dcHelper.Name].UDPPingSuccess) && (dcHelper.UDPPingSuccess))
                { ForestBase.DCList[dcHelper.Name] = dcHelper; }

                ForestBase.DomainDCs.AddSafe(domainDN, new List<string> { });

                ForestBase.DomainDCs[domainDN].AddSafe(dcHelper.Name, force);

                if (dcHelper.UDPPingSuccess)
                {
                    foreach (string nc in dcHelper.NamingContexts)
                    {
                        ForestBase.NCsToDCs.AddSafe(nc, new List<string> { });

                        ForestBase.NCsToDCs[nc].AddSafe(dcHelper.Name, force);
                    }

                    ForestBase.NCsToDCs = ForestBase.NCsToDCs.OrderByKeyLengthSafe();
                }
            }

            else
            { /* tbd fire fatal event */}
        }

        private void QueryHasConstructedAttribute(string[] propertiesToLoad, SearchScope scope, ref QueryControl queryInfo)
        {
            if ((scope != SearchScope.Base) && queryInfo.FromGui && !queryInfo.MustGetSingleObjectPath && !queryInfo.QueryExtendedRootDSE)
            {
                foreach (string attrib in propertiesToLoad)
                {
                    KeyValuePair<string, AttributeSchema> kvpinfo = ForestBase.AttributeCache.Where(a => a.Key.Equals(attrib, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                    if (kvpinfo.Value != null)
                    {
                        queryInfo.ContainsConstructedAttribute = kvpinfo.Value.IsConstructed;

                        queryInfo.MustGetSingleObjectPath = kvpinfo.Value.IsConstructed;
                    }

                    if (queryInfo.MustGetSingleObjectPath) break;
                }
            }

            else if (queryInfo.MustGetSingleObjectPath)
            { 
                queryInfo.MustGetSingleObjectPath = false;

                queryInfo.CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH;
            }
        }

        private DirectoryControl GetControl(DirectoryControl[] controls, string OID)
        {
            DirectoryControl dcret = null;

            try
            { dcret = controls.ToList().Where(d => d.Type == OID).FirstOrDefault(); }

            catch (Exception ex)
            { ex.ToDummy(); }

            return dcret;
        }

        private DirectoryContext GetDirectoryContext(string domainName, Credentials credInfo, out bool isValid)
        {
            isValid = false;

            DirectoryContext ret = null;

            if (domainName == null)
            {
                if (credInfo == null)
                { ret = new DirectoryContext(DirectoryContextType.Domain, ForestBase.CurrentDomain); }

                else
                { ret = GetCtx(credInfo); }
            }

            else
            {
                if (credInfo == null)
                { ret = new DirectoryContext(DirectoryContextType.Domain, domainName); }

                else
                { ret = GetCtx(domainName, credInfo); }
            }
                       
            object[] args = new object[] { ret, DirectoryContextType.Domain };

            object valid = ret.InvokeMethod("IsContextValid", BindingFlags.NonPublic | BindingFlags.Static, new Type[] { typeof(DirectoryContext), typeof(DirectoryContextType) }, ref args);

            if (valid != null)
            { isValid = (bool)valid; }

            if (!isValid)
            { ret = null; }

            return ret;
        }

        private DirectoryContext GetCtx(string domainName, Credentials credInfo)
        {
            DirectoryContext ret = null;

            if (credInfo.HasCreds)
            {
                ret = new DirectoryContext(DirectoryContextType.Domain, domainName,
                                           ForestBase.GivenCreds.UserName, ForestBase.GivenCreds.Pwd.ToUnsecureString());
            }

            else
            { ret = new DirectoryContext(DirectoryContextType.Domain, domainName); }

            return ret;
        }

        private DirectoryContext GetCtx(Credentials credInfo)
        {
            DirectoryContext ret = null;

            if (credInfo.HasCreds)
            {
                ret = new DirectoryContext(DirectoryContextType.Domain, ForestBase.CurrentDomain,
                                            ForestBase.GivenCreds.UserName, ForestBase.GivenCreds.Pwd.ToUnsecureString());
            }

            else
            { ret = new DirectoryContext(DirectoryContextType.Domain, ForestBase.CurrentDomain); }

            return ret;
        }

        private LdapConnection GetLdapConnection(LdapDirectoryIdentifier ldapId, Credentials credInfo)
        {
            LdapConnection ret = null;

            try
            {
                if (credInfo != null)
                { ret = GetLdapCon(ldapId, credInfo); }

                else
                { ret = new LdapConnection(ldapId); }
            }

            catch (Exception ex)
            { base.SetError(String.Format("GetLdapConnection: {0} ({1})", ex.Message, ex.GetType().Name)); }

            return ret;
        }

        private LdapConnection GetLdapCon(LdapDirectoryIdentifier ldapId, Credentials credInfo)
        {
            LdapConnection ret = null;

            try
            {
                if (credInfo.HasCreds)
                { ret = new LdapConnection(ldapId, credInfo.NetCreds); }

                else
                { ret = new LdapConnection(ldapId); }
            }

            catch (Exception ex)
            { base.SetError(String.Format("GetLdapCon: {0} ({1})", ex.Message, ex.GetType().Name)); }

            return ret;
        }

        private void AsyncSearchComplete(object sender, GlobalEventArgs args)
        {
            base.AsyncComplete = true;

            try
             {
                 base.AsyncConnection.Dispose();

                 base.AsyncConnection = null;

                 base.IsConnected = false;
             }

             catch (Exception ex)
            { ex.ToDummy(); }

            GlobalEventHandler.AsyncQureyIsCompleted -= AsyncSearchComplete;
        }

        private void AsyncPartialResult(object entries, GlobalEventArgs args)
        {
            try
            {
                lock (base.ParallelResults)
                { base.ParallelResults.AddRange((List<SearchResultEntry>)entries); }
            }

            catch (Exception ex)
            { var x = 0;}
        }

        public bool RestoreObject(string dc, 
                                  ADObjectInfo objectInfo, 
                                  QueryControl queryInfo)
        {
            bool ret = false;

            ForestBase.CurrentPorts.SelectedPort = queryInfo.Port;

            Connect(dc, ReferralChasingOptions.None);

            DirectoryAttributeModification damdel = new DirectoryAttributeModification() { Name = "isDeleted", Operation = DirectoryAttributeOperation.Delete };
            
            string newdn = (objectInfo.ObjectClass.ToLowerInvariant() == "organizationalunit") ? "OU=" : "CN=";

            newdn = newdn + objectInfo.LastKnownRDN.ToEscapedLdapComponent() + ",";

            newdn = newdn + objectInfo.LastKnownParent;

            GlobalEventHandler.RaiseMessageOccured("Restoring:", true);
            GlobalEventHandler.RaiseMessageOccured("\t" + objectInfo.Path, true);
            GlobalEventHandler.RaiseMessageOccured("to:", true);
            GlobalEventHandler.RaiseMessageOccured("\t" + newdn, true);

            DirectoryAttributeModification damdn = new DirectoryAttributeModification() { Name = "distinguishedName", Operation = DirectoryAttributeOperation.Replace };

            damdn.Add(newdn);

            DirectoryAttributeModification[] damlist = new DirectoryAttributeModification[] { damdel, damdn };

            string delpath = String.Format("<GUID={0}>", objectInfo.ObjectGuid);

            delpath = objectInfo.Path;

            ModifyRequest mrcall = new ModifyRequest(delpath, damlist);

            mrcall.Controls.Add(new ShowDeletedControl { ServerSide = true });
            
            mrcall.Controls.Add(new PermissiveModifyControl());

            try
            {
                ModifyResponse mrresult = (ModifyResponse)base.Connection.SendRequest(mrcall);

                if (mrresult.ResultCode == ResultCode.Success)
                {
                    GlobalEventHandler.RaiseMessageOccured("Restored " + objectInfo.LastKnownRDN, true);

                    ret = true;
                }

                else
                { GlobalEventHandler.RaiseErrorOccured("ERROR: " + mrresult.ResultCode.ToString(), true); }                
            }

            catch (DirectoryOperationException doex)
            { GlobalEventHandler.RaiseErrorOccured("ERROR: " + doex.Message, true); }

            catch (Exception ex)
            { GlobalEventHandler.RaiseErrorOccured("ERROR: " + ex.Message, true); }

            return ret;
        }

        public void ReceivedCancellation(object sender, GlobalEventArgs args)
        {
            CancelToken = true;

            GlobalEventHandler.SearchCancelled -= ReceivedCancellation;
        }

        #endregion
    }
}
