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
    public class SearchRequestExtender
    {
        #region fields

        public bool DoPaging = false;

        public byte[] PagingCookie = null;
        public int PageCount = 0;
        public PageResultRequestControl PageControl;
        public int CurrentPageSize = 0;
        public int LastPageSize = 0;
        public bool FirstPagingRun = true;

        public bool MoreData = false;

        public VlvRequestControl VlvControl;
        public int VlvStart = 0;
        public int VlvRange = 499;

        public bool RetreiveStatistics = false;
        public List<StatsData> Statistics = new List<StatsData> { };

        

        public Dictionary<string, short> CurrentAttributesToDisplay = new Dictionary<string, short>(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<string, RangeInfo> CurrentRangeRetrieval = new Dictionary<string, RangeInfo>(StringComparer.InvariantCultureIgnoreCase);
        public List<string> RangeRetrievalAlerts = new List<string> { };

        public bool ASQRequired = false;
        public AsqRequestControl AsqControl;

        public SortRequestControl SortControl;

        public bool ShowDeleted = false;
        public bool ShowRecycled = false;
        
        public bool HasError = false;
        public string ErrorMSG = null;

        public bool MessagesRetreived = false;

        protected List<string> Messages = new List<string> { };

        public SearchRequest Request;

        public QueryControl QueryInfo;

        public string DC { get; set; }
        public string[] Attributes;
        public SearchScope Scope { get; set; }
        public string LdapFilter { get; set; }
        public string BasePath { get; set; }
        public ReferralChasingOptions ReferralChasing { get; set; }

        public bool ShowAttributes = true;

        public static string STATISTCS_CONTROL_OID { get { return "1.2.840.113556.1.4.970"; } }

        public static string SHOW_RECYCLED_CONTROL_OID { get { return "1.2.840.113556.1.4.2064"; } }

        public DirSyncRequestControl DirSyncControl;

        protected DirectorySynchronizationOptions DirSyncFlags = DirectorySynchronizationOptions.IncrementalValues;

        #endregion

        #region constructor

        public SearchRequestExtender(string searchBase,
                                    string ldapFilter,
                                    string[] propertiesToLoad,
                                    SearchScope scope,
                                    byte[] inPagingCookie,
                                    QueryControl mustQuery)
        {  LoadInternal(searchBase, ldapFilter, propertiesToLoad, scope, inPagingCookie, mustQuery); }

        public SearchRequestExtender()
        { }

        #endregion

        #region methods

        protected void LoadInternal(string searchBase,
                                    string ldapFilter,
                                    string[] propertiesToLoad,
                                    SearchScope scope,
                                    byte[] inPagingCookie,
                                    QueryControl queryInfo)
        {
            QueryInfo = queryInfo;

            ASQRequired = QueryInfo.PerformASQ;

            ShowDeleted = QueryInfo.ShowDeleted;

            ShowRecycled = QueryInfo.ShowRecycled;

            PagingCookie = inPagingCookie;

            BasePath = searchBase;

            LdapFilter = ldapFilter;

            Scope = scope;

            HandleAttributes(propertiesToLoad);

            BuildRequest(searchBase, ldapFilter, Attributes, scope);

        }

        public void HandleAttributes(string[] propertiesToLoad)
        {
            if (propertiesToLoad.Length == 0)
            { propertiesToLoad = null; }

            else if (propertiesToLoad.Length == 1) 
            { 
                switch(propertiesToLoad[0].ToLowerInvariant())
                {
                    case "*":
                    case "* -> all attribs":
                        propertiesToLoad = null; 

                        break;

                    case "null":
                    case "none":
                    case "0":
                    case "0 -> no attribs":
                        propertiesToLoad = new string[] { "distinguishedName" };                        
                        ShowAttributes = false;
                        
                        break;
                }                
            }

            Attributes = propertiesToLoad;

            if (Attributes != null)
            {
                int startcnt = 0;

                if (ASQRequired)
                {
                    AsqControl = new AsqRequestControl(Attributes[0]);
                    
                    Attributes.RemoveSafe(0, ref Attributes);

                    if (Attributes.Length == 0)
                    { Attributes.AddSafe(null, ref Attributes); }
                }

                for (int cnt = startcnt; cnt < Attributes.Length; cnt++)
                { CurrentAttributesToDisplay.AddSafe<string, short>(Attributes[cnt], 0); }          
            }
        }

        protected void BuildRequest(string searchBase,
                                    string ldapFilter,
                                    string[] propertiesToLoad,
                                    SearchScope scope)
        {
            Request = new SearchRequest(searchBase,
                                        ldapFilter,
                                        scope,
                                        propertiesToLoad);

            Request.TimeLimit = TimeSpan.FromSeconds((double)ForestBase.CurrentTimeOut);

            if (QueryInfo.PhantomRoot)
            {
                Request.DistinguishedName = null;
 
                SearchOptionsControl sopcontrol = new SearchOptionsControl(SearchOption.PhantomRoot);
                Request.Controls.Add(sopcontrol);
            }

            if (QueryInfo.PerformDirSync)
            {
                Request.TimeLimit = new TimeSpan(0, 10, 0);

                DirSyncCookie(); 
            }

            else
            {
                if (ASQRequired)
                { AsqCookie(); }

                if (QueryInfo.PerformSort)
                { SortCookie(); }

                if (QueryInfo.VlvRequired)
                { VlvControls(); }

                else if (QueryInfo.PerformPagedQuery)
                { PageCookie(1000); }

                StatsCookie();
            }

            if (QueryInfo.ShowDeleted)
            {
                ShowDeletedControl showdeleted = new ShowDeletedControl { ServerSide = true };
                Request.Controls.Add(showdeleted);
            }

            if (QueryInfo.ShowRecycled)
            {
                DirectoryControl showrecycled = new DirectoryControl(SHOW_RECYCLED_CONTROL_OID, null, true, true);
                Request.Controls.Add(showrecycled);
            }
        }
        
        protected void DirSyncCookie()
        {
            DirSyncControl = new DirSyncRequestControl(QueryInfo.DirSyncCookie, DirSyncFlags, Int32.MaxValue);

            Request.Controls.Add(DirSyncControl);
        }

        protected void VlvControls()
        {
            if (Attributes != null)
            {
                CurrentAttributesToDisplay.AddSafe<string, short>("sAMAccountName", 0);

                SortRequestControl sortcontrol = new SortRequestControl("sAMAccountName", false);

                Request.Controls.Add(sortcontrol);

                VlvControl = new VlvRequestControl(VlvStart, VlvRange, VlvRange + 1);

                Request.Controls.Add(VlvControl);
            }
        }

        public void PageCookie(int pageSize)
        {
            FirstPagingRun = true;

            LastPageSize = pageSize;

            byte[] cvalue = BerConverter.Encode("{io}", new object[] { pageSize, PagingCookie });

            DirectoryControl dcpage = new DirectoryControl("1.2.840.113556.1.4.319", cvalue, true, true);

            if (PagingCookie != null)
            { PageControl = new PageResultRequestControl(PagingCookie); }

            else if (Scope != SearchScope.Base)
            { PageControl = new PageResultRequestControl(pageSize); }

            if (PageControl != null)
            {
                PageCount = 0;

                Request.Controls.Add(PageControl);

                DoPaging = true;
            }
        }

        protected void AsqCookie()
        {
            if (AsqControl != null)
            { Request.Controls.Add(AsqControl); }
        }

        protected void SortCookie()
        {
            if ((QueryInfo.PerformSort) && (QueryInfo.SortKeys.Count > 0))
            {
                SortControl = new SortRequestControl(QueryInfo.SortKeys.ToArray());

                Request.Controls.Add(SortControl);
            }
        }

        protected void StatsCookie()
        {
            if (QueryInfo.ExecStatsQuery)
            {
                if (QueryInfo.RootDse)
                { GlobalEventHandler.RaiseErrorOccured("Retreiving statistics from a rootDSE query cannot be done", true);
                }

                else if (QueryInfo.MustGetSingleObjectPath)
                {
                    GlobalEventHandler.RaiseErrorOccured(String.Format("Switched to single object path base query {0}\t-> retreiving staistics doesn't make sense", Environment.NewLine), true);
                }

                else
                {
                    RetreiveStatistics = QueryInfo.ExecStatsQuery;

                    byte[] controlvalue = BerConverter.Encode("{i}", new object[] { 0x1 });

                    controlvalue = null;

                    Request.Controls.Add(new DirectoryControl(STATISTCS_CONTROL_OID, controlvalue, false, true));
                }                 
            }
        }

        public void AddMessage(string msg)
        { Messages.Add(msg); }

        public List<string> GetMessages()
        {
            List<string> ret = new List<string> { };

            if (Messages.Count > 0)
            { 
                ret.AddRange(Messages);

                ret.Add("");
            }

            MessagesRetreived = true;

            return ret;
        }

        public void AdjustAttributesToDisplay(SearchResultAttributeCollection attributeList, bool doValRangeRetrieval)
        {
            CurrentAttributesToDisplay.Clear();
            CurrentRangeRetrieval.Clear();

            if (Attributes != null)
            { 
                foreach (string name in Attributes)
                { CurrentAttributesToDisplay.AddSafe<string, short>(name, 0); }
            }

            foreach (string name in attributeList.AttributeNames)
            {
                string attribName = null;
                int lowval = 0;  int maxval = 0;                

                if (name.IsRangeAttribute(out attribName, out lowval, out maxval))
                {
                    CurrentRangeRetrieval.Add(attribName, new RangeInfo(lowval, maxval, name, attribName));

                    RangeRetrievalAlerts.AddSafe<string>(name);

                    if (!doValRangeRetrieval)
                    { CurrentAttributesToDisplay.AddSafe<string, short>(name, 0); }
                }

                else 
                { CurrentAttributesToDisplay.AddSafe<string, short>(name, 0); }
            }

            foreach (string name in CurrentRangeRetrieval.Keys)
            { CurrentAttributesToDisplay.RemoveSafe<string, short>(name); }

            CurrentAttributesToDisplay = CurrentAttributesToDisplay.OrderByKeySafe();
        }

        public void UpdatePagingCookie(DirectoryControl dControl, int pageSize)
        {
            if (FirstPagingRun)
            {
                CurrentPageSize = pageSize;

                FirstPagingRun = false;
            }

            else if (pageSize > CurrentPageSize)
            { CurrentPageSize = pageSize; }


            Request.Controls.Remove(PageControl);

            MoreData = false;

            if (dControl != null)
            {
                PageResultResponseControl response = (PageResultResponseControl)dControl;

                PagingCookie = response.Cookie;
            }

            if ((PagingCookie != null) && (PagingCookie.Length != 0))
            { MoreData = true; }

            else
            { MoreData = false; }

            if (MoreData)
            {
                PageControl = new PageResultRequestControl(PagingCookie);

                PageControl.PageSize = pageSize;

                Request.Controls.Add(PageControl);
            }
        }

        public void UpdateDirSyncCookie(DirectoryControl dControl)
        {
            Request.Controls.Remove(DirSyncControl);

            MoreData = false;

            DirSyncResponseControl response = null;

            if (dControl != null)
            {
                response = (DirSyncResponseControl)dControl;

                MoreData = response.MoreData;
            }

            if (MoreData)
            {
                QueryInfo.DirSyncCookie = response.Cookie;
                
                DirSyncCookie();
            }
        }

        public void UpdateVlv()
        {
            VlvStart = VlvRange + 1;

            Request.Controls.Remove(VlvControl);

            VlvControl = new VlvRequestControl(VlvStart, VlvRange, VlvRange + 1);

            Request.Controls.Add(VlvControl);
        }

        public SearchRequestExtender Copy()
        {
            SearchRequestExtender ret = new SearchRequestExtender();

            ret.PagingCookie = PagingCookie;
            ret.PageCount = PageCount;
            ret.PageControl = PageControl;
            ret.CurrentPageSize = CurrentPageSize;

            ret.RetreiveStatistics = RetreiveStatistics;

            ret.Statistics = Statistics;

            ret.Attributes = Attributes;

            ret.CurrentAttributesToDisplay = CurrentAttributesToDisplay;
            ret.CurrentRangeRetrieval = CurrentRangeRetrieval;

            ret.HasError = HasError;
            ret.ErrorMSG = ErrorMSG;

            ret.Request = Request;

            return ret;
        }

        #endregion
    }
}
