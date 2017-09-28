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
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public partial class GUI : Form
    {

        #region fields

        protected RichTextBoxExtension txtOutput;

        protected bool ElevateMe = false;
        protected bool DontLoad = false;

        protected int CurrentResultPosition = 0;
        protected List<MessageCache> ResultList = new List<MessageCache> { };

        protected string CurrrentStatsData = null;

        protected QueryBuilder QBuilder;
        protected bool QBLoaded = false;

        protected ConnectionGUI CBuilder;
        protected bool CBuilderLoaded = false;

        protected bool AuthLoaded = false;

        protected AttributeTypeAssociator ATABuilder;
        protected bool ATABuilderLoaded = false;

        protected StatsGUI StatsForm = null;

        protected LDAPBrowser LdapTree = null;
        protected string LastBase = null;

        protected InfoGui ShowInfo = null;
        protected bool ShowInfoLoaded = false;

        protected SearchScope QueryScope;
        protected CUSTOM_SCOPE CurrentScope;

        protected SyncCollection DirSyncs;
        protected SyncRun CurrentSyncRun;

        protected string EndFilter = "|#|ENDFILTER|#|" + Environment.NewLine;
        protected const string WrapAttributes = "|#|ATTRIBS|#|";
        protected static string FilterHistoryFile = "filterhistory_v" + FilterInfo.FilterVersion + ".ctrl";
        protected string FilterHistoryPath = "Cache\\FilterHistory\\" + FilterHistoryFile;
        protected bool UpdatingFilterHistory = false;

        protected bool UpdatingAttributes = false;

        protected ForestInfo ForestStore;
        protected bool UpdatingForest = false;
        protected DateTime StartUpdateForest;

        protected bool HandleProgress = false;
        protected int DCCount = 1;
        protected int CurrentDCNumber = 0;
        protected int PreDCProgress = 30;
        protected string ListQuery = String.Empty;

        protected QueryControl QueryInfo = new QueryControl() { FromGui = true };

        protected Dictionary<string, List<double>> DecodeStatistics;
        protected long DecodeHandler;
        protected const long DecodeHandlerStep = 500;

        protected string LastGoodDC;

        protected DateTime CurrentCodeStartTime = DateTime.MinValue;
        protected DateTime CurrentStartTime = DateTime.MinValue;
        protected double CurrentCallTime = 0;
        protected int CurrentRuns = 0;
        protected double CurrentCodeTime = 0;
        protected long CurrentResultCount = 0;
        protected double ThreadRunTime = 0;
        protected List<SearchResultEntry> CurrentEntries = new List<SearchResultEntry>() { Capacity = 0 };

        protected bool IsElevated = false;
        protected string CallingUser = String.Empty;

        public bool AddASQAttribute = false;
        public bool AddSortAttribute = false;

        public bool CancelToken = false;
        public bool CancelResults = false;
        public List<string> CurrentMessages = new List<string> { };
        public bool NextBulk = false;

        protected WhoAmI MySelf;

        #endregion

        #region constructors

        public GUI(string[] args)
        {
            if (args.Length != 0)
            { 
                ElevateMe = (args[0] == "1") ? true : false; 

                if (args.Contains("none"))
                { this.DontLoad = true; }
            }

            InitializeComponent(); 
        }

        protected void GUI_Load(object sender, EventArgs e)
        { LoadInternal(); }

        protected void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DeRegisterFindEvents();
                //CurCreds.Revert();

                //GlobalEventHandler.ClipBoardChanged -= ClipBoardChangedEvent;
                //CPWatch.StopWatching(); 
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        #endregion

        #region loading

        [STAThread]
        protected void LoadInternal()
        {
            if (ElevateMe)
            { StartThis(true, true); }

            PreLoad();

            if (!DontLoad)
            { StartRefreshForest(); }

            PostLoad();            
        }

        protected void CheckAdmin()
        {
            GlobalUserStore.Load();

            this.CallingUser = GlobalUserStore.Name;

            this.IsElevated = GlobalUserStore.IsElevated;

            this.User_Elevate_MenuItem.Enabled = !this.IsElevated;

            this.SetText(IsElevated ? String.Format("LDAPQueryAnalyzer ({0} [elevated])", CallingUser) : 
                                      String.Format("LDAPQueryAnalyzer ({0})", CallingUser));         
        }

        protected void PreLoad()
        {
            LoadCtrlExtensions();

            MySelf = new WhoAmI();

            CheckAdmin();

            rbBase.Checked = true;

            FilterHistoryPath = Path.Combine(GlobalHelper.CurrentDirectory, FilterHistoryPath);

            MainBase.UserSettings = new SettingsHandler();

            (new Thread(LoadDynamicDll)).Start();

            (new Thread(LoadSettings)).Start();

            (new Thread(LoadRootDseOperational)).Start();

            this.lvFilterHistory.Tag = new FilterHistoryControl();

            (new Thread(LoadFilterHistory)).Start();

            LoadToolTips();

            RegisterFindEvents();

            this.Query_Return_Results_MenuItem.Checked = true;

            this.Show();
            this.Refresh();

            GlobalEventHandler.SignalError += ErrorSignaled;
        }

        protected void LoadCtrlExtensions()
        {
            this.txtOutput = new RichTextBoxExtension();

            this.gbResult.Controls.Add(this.txtOutput);
            //this.txtOutput.AcceptsReturn = true;
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.ContextMenuStrip = this.ResultsContextMenu;
            this.txtOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.HideSelection = false;
            this.txtOutput.Location = new System.Drawing.Point(3, 20);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(1223, 292);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.WordWrap = false;
            this.txtOutput.SuppressTab = true;

            this.txtOutputOld.TabIndex = 10000;
            this.txtOutputOld.Visible = false;
        }

        protected void LoadDynamicDll()
        {
            if (MainBase.DynamicDll == null)
            { MainBase.DynamicDll = new DynamicTypeLoader(); }
        }

        protected void LoadSettings()
        {
            MainBase.UserSettings = SettingsHandler.Load();

            MainBase.UserSettings.LoadingGui = true;

            GlobalControlHandler.MenuSetCheckedState(this.Query_AutoPage_MenuItem, MainBase.UserSettings.AutoPage);
            GlobalControlHandler.MenuSetCheckedState(this.Query_ValRange_MenuItem, MainBase.UserSettings.DoValueRangeRetrieval);
            GlobalControlHandler.MenuSetCheckedState(this.Query_Sort_Results_Asc_MenuItem, MainBase.UserSettings.SortAscending);
            GlobalControlHandler.MenuSetCheckedState(this.Query_Sort_Results_Desc_MenuItem, MainBase.UserSettings.SortDescending);

            GlobalControlHandler.MenuSetCheckedState(this.Decode_GUIDs_MenuItem, MainBase.UserSettings.DecodeGUID);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_SIDs_MenuItem, MainBase.UserSettings.DecodeSID);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_UserParams_MenuItem, MainBase.UserSettings.DecodeUserParameters);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_ResolveSids_MenuItem, MainBase.UserSettings.ResolveSids);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_SD_MenuItem, MainBase.UserSettings.DecodeSD);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_OctetString_MenuItem, MainBase.UserSettings.DecodeOctetStrings);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_ReplicaLinks_MenuItem, MainBase.UserSettings.DecodeReplicaLinks);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_PGID_MenuItem, MainBase.UserSettings.DecodePrimaryGroupID);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_TimeBias_MenuItem, MainBase.UserSettings.UseLocalTime);
            GlobalControlHandler.MenuSetCheckedState(this.Decode_IgnoreEmpty_MenuItem, MainBase.UserSettings.IgnoreEmpty);

            GlobalControlHandler.MenuSetText(this.Results_MaxResultsCount_MenuItem, MainBase.UserSettings.MaxResultCount.ToString());
            GlobalControlHandler.MenuSetCheckedState(this.Results_ShowPartial_MenuItem, MainBase.UserSettings.ShowPartialResults);
            GlobalControlHandler.MenuSetText(this.Results_MaxResults_MenuItem, MainBase.UserSettings.MaxResultListCount.ToString());

            GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_All_ContextItem, MainBase.UserSettings.HistoryAll);
            
            if (!MainBase.UserSettings.HistoryAll)
            {
                GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_ASQ_ContextItem, MainBase.UserSettings.HistoryASQ);
                GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_Base_ContextItem, MainBase.UserSettings.HistoryBase);
                GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_DC_ContextItem, MainBase.UserSettings.HistoryDC);
                GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_Port_ContextItem, MainBase.UserSettings.HistoryPort);
                GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_Scope_ContextItem, MainBase.UserSettings.HistoryScope);
                GlobalControlHandler.MenuSetCheckedState(this.FilterHistory_Sort_ContextItem, MainBase.UserSettings.HistorySorting);
            }

            QueryInfo.AutoPage = MainBase.UserSettings.AutoPage;
            QueryInfo.DoValueRangeRetrieval = MainBase.UserSettings.DoValueRangeRetrieval;
            QueryInfo.SortAscending = MainBase.UserSettings.SortAscending;
            QueryInfo.SortResults = (MainBase.UserSettings.SortAscending || MainBase.UserSettings.SortDescending);

            MainBase.UserSettings.LoadingGui = false;
        }

        protected void LoadRootDseOperational()
        { MainBase.RootDseOperational = RootDSEAttributes.Load(); }

        protected void LoadToolTips()
        {
            ToolTip thetips = new ToolTip();

            thetips.AutoPopDelay = 5000;
            thetips.InitialDelay = 500;
            thetips.ReshowDelay = 500;
            thetips.ShowAlways = true;

            thetips.SetToolTip(this.cmdSearch, "Execute query");
            thetips.SetToolTip(this.cmdRestore, "Restore object");
            thetips.SetToolTip(this.cmdAdd, "Add attribute to attributes to load");
            thetips.SetToolTip(this.cmdFirstResult, "Show previous first search result");
            thetips.SetToolTip(this.cmdPrevResult, "Show previous remembered search result");
            thetips.SetToolTip(this.cmdNextResult, "Show next remembered search result");
            thetips.SetToolTip(this.cmdLastResult, "Show current remembered search result");
            thetips.SetToolTip(this.cmdClearResults, "Clear remembered result list");
            thetips.SetToolTip(this.rbDN, "Use Base as search base");
            thetips.SetToolTip(this.rbPhantomRoot, "Perform a forest wide query");
            thetips.SetToolTip(this.rbRootDSE, "return rootDSE attributes from UDP ping");
            thetips.SetToolTip(this.cbRootDseExt, "return operational rootDSE attributes from UDP ping");
            thetips.SetToolTip(this.txtSort, "Attribute used for server side sorting.");
            thetips.SetToolTip(this.cbReverse, "Check for revserse order sorting");
            thetips.SetToolTip(this.cmdCancelAll, "Cancel query call and handling of already received results");
            thetips.SetToolTip(this.cmdCancelQuery, "Cancel query call but handle already received results");
            thetips.SetToolTip(this.cmdFind, "Find string in result text");
            thetips.SetToolTip(this.cmdCloseFind, "Close find dialogue");
        }

        protected void PostLoad()
        {
            new Thread(new ThreadStart(LoadSyncCollection)).Start();

            this.rbSubtree.Checked = true;
            QueryScope = SearchScope.Subtree;

            this.cmdSearch.Select();

            //RegisterAsyncEvents();
            GlobalEventHandler.ParallelQueriesCompleted += ParallelQueriesCompleted;
        }

        protected void LoadDynamicDllInfo()
        {
            if (MainBase.DynamicDll.HasTypes)
            {
                string info = null;

                foreach (KeyValuePair<string, Type> dynenum in MainBase.DynamicDll.EnumList)
                {
                    info += dynenum.Key + ":\n";

                    foreach (object val in Enum.GetValues(dynenum.Value))
                    {
                        bool ibool = false;
                        int ival = 0;
                        long lval = 0;

                        try
                        {
                            ival = (int)val;
                            ibool = true;

                            info += String.Format("{0} = {1}", val.ToString(), ival) + "\n";
                        }

                        catch (Exception ex)
                        { ex.ToDummy();}

                        if (!ibool)
                        {
                            try
                            {
                                lval = (long)val;

                                info += String.Format("{0} = {1}", val.ToString(), lval) + "\n";
                            }

                            catch (Exception ex)
                            { ex.ToDummy();}
                        }
                    }
                }
            }
        }

        protected void LoadSyncCollection()
        { DirSyncs = new SyncCollection(); }

        
        #endregion

        #region load ad data

        protected void StartRefreshForest(string forestName = null, Credentials userCreds = null)
        {
            StartUpdateForest = DateTime.Now;

            ThreadRunTime = 0;

            HandleLoadError(true);

            UpdatingForest = true;
            
            PreDCProgress = 30;

            StartProgress();

            
            ForestStore = new ForestInfo();

            GlobalEventHandler.UDPPingProceeded += UDPPingEvent;
            GlobalEventHandler.DiscoveredCurrentDomain += CurrentDomainEvent;
            GlobalEventHandler.DiscoveredCurrentDomainNCs += CurrentDomainNCEvent;
            GlobalEventHandler.DiscoveredForest += ForestEvent;
            GlobalEventHandler.CountOfDCs += DCCountEvent;
            GlobalEventHandler.DCDiscovered += DCDiscoveredEvent;
            GlobalEventHandler.DiscoveredSchema += SchemaEvent;
            GlobalEventHandler.FinishedDiscovering += EndRefreshForest;

            LogDebugString(String.Format((forestName == null) ? "Query statistics loading forest" : "Query statistics loading forest: '{0}'", forestName));

            Task.Factory.StartNew(() => { ForestStore.Load(forestName, userCreds, StartUpdateForest); }, TaskCreationOptions.LongRunning);

            //if (this.ForestStore.HasError)
            //{
            //    if (userCreds != null)
            //    { HandleLoadError(); }

            //    else
            //    { HandleLoadError(); }
            //}

            //else
            //{
            //    this.gbCon.SetText(String.Format("ConnectionInfo ({0})", ForestBase.ForestName));

            //    this.cmbDomains.Items.Clear();
            //    this.cmbDomains.Items.AddRange(ForestBase.DomainDCs.Keys.ToArray());

            //    this.cmbDomains.SetText(ForestBase.CurrentDomain);

            //    UpdateDCList();

            //    this.cmbNCs.SetText(ForestBase.DCList[this.cmbDCs.GetText()].DefaultNamingContext);
            //}

            //LogDebugString(String.Format("ThreadRunTime (sum): {0} ms", ThreadRunTime));

            //LogDebugString(String.Format("TotalLoadTime: {0} ms", (long)DateTime.Now.Subtract(StartUpdateForest).TotalMilliseconds));

            //EndProgress();

            //UpdatingForest = false;

            //UpdateNCList();

            //UpdateDNAttributes();

            //GlobalEventHandler.UDPPingProceeded -= UDPPingEvent;
            //GlobalEventHandler.DiscoveredCurrentDomain -= CurrentDomainEvent;
            //GlobalEventHandler.DiscoveredCurrentDomainNCs -= CurrentDomainNCEvent;
            //GlobalEventHandler.DiscoveredForest -= ForestEvent;            
            //GlobalEventHandler.CountOfDCs -= DCCountEvent;
            //GlobalEventHandler.DCDiscovered -= DCDiscoveredEvent;
            //GlobalEventHandler.DiscoveredSchema -= SchemaEvent;
        }

        protected void EndRefreshForest(object sender, GlobalEventArgs args)
        {
            if(!this.ForestStore.HasError)
            {
                this.gbCon.SetText(String.Format("ConnectionInfo ({0})", ForestBase.ForestName));

                this.cmbDomains.ClearItems(); //.Items.Clear();
                this.cmbDomains.AddItemRange(new object[] { ForestBase.DomainDCs.Keys.ToArray() }); //.Items.AddRange(ForestBase.DomainDCs.Keys.ToArray());

                this.cmbDomains.SetText(ForestBase.CurrentDomain);

                UpdateDCList();

                this.cmbNCs.SetText(ForestBase.DCList[this.cmbDCs.GetText()].DefaultNamingContext);

                LogDebugString(String.Format("ThreadRunTime (sum): {0} ms", ThreadRunTime));

                LogDebugString(String.Format("TotalLoadTime: {0} ms", (long)DateTime.Now.Subtract(StartUpdateForest).TotalMilliseconds));

                EndProgress();

                UpdatingForest = false;

                UpdateNCList();

                UpdateDNAttributes();
            }

            else
            {
                HandleLoadError();

                EndProgress();

                UpdatingForest = false;
            }

            GlobalEventHandler.UDPPingProceeded -= UDPPingEvent;
            GlobalEventHandler.DiscoveredCurrentDomain -= CurrentDomainEvent;
            GlobalEventHandler.DiscoveredCurrentDomainNCs -= CurrentDomainNCEvent;
            GlobalEventHandler.DiscoveredForest -= ForestEvent;
            GlobalEventHandler.CountOfDCs -= DCCountEvent;
            GlobalEventHandler.DCDiscovered -= DCDiscoveredEvent;
            GlobalEventHandler.DiscoveredSchema -= SchemaEvent;
            GlobalEventHandler.FinishedDiscovering -= EndRefreshForest;
        }

        protected void HandleLoadError(bool success = false)
        {
            this.gbCon.SetState(success);
            this.gbProps.SetState(success);
            this.gbQuery.SetState(success);

            this.txtOutput.SetForeColor(success ? Color.Black : Color.DarkRed);

            if (!success)
            { LogMessage(String.Format("ERROR: {0}", this.ForestStore.ErrorMSG), this.txtOutput); }

            this.QueryMenu.SetState(success);
            this.FilterMenu.SetState(success);
            this.AttributesMenu.SetState(success);
            this.ResultsMenu.SetState(success);
        }

        protected void UpdateDCList()
        {
            if (this.cmbDomains.GetText() != null)
            { ForestBase.CurrentDomain = this.cmbDomains.GetText(); }

            this.cmbDCs.ClearItems(); 

            //this.cmbDCs.Items.AddRange(ForestBase.DomainDCs[this.cmbDomains.GetText()].ToArray());

            foreach (string dc in ForestBase.DomainDCs[ForestBase.CurrentDomain])
            {
                this.cmbDCs.AddItem(ForestBase.DCList[dc]); //.Items.Add(ForestBase.DCList[dc]);
            }        
            
            if (ForestBase.DomainDCs[this.cmbDomains.GetText()].Contains(ForestBase.CurrentDC))
            { this.cmbDCs.SetText(ForestBase.CurrentDC); }

            else
            {
                this.cmbDCs.SetIndex(0);

                ForestBase.CurrentDC = this.cmbDCs.GetText();
            }

            LastGoodDC = this.cmbDCs.GetText();

            EndProgress();
        }

        protected void UpdateNCList()
        {
            this.cmbNCs.ClearItems();

            DomainControllerHelper curdc = ForestBase.DCList[this.cmbDCs.GetText()]; 

             if (!curdc.UDPPinged)
            {                
                curdc = DomainControllerHelper.UDPPing(curdc.Name);
                                
                ForestStore.StoreDC(curdc, curdc.DefaultNamingContext, true);
            }
                        
            if (curdc.UDPPingSuccess)
            {
                this.cmbNCs.AddItemRange(new object[] { curdc.NamingContexts.ToArray() });

                this.Query_ShowRecycled_MenuItem.SetState(curdc.SupportedControls.Contains(SearchRequestExtender.SHOW_RECYCLED_CONTROL_OID));

                int ix = 0;

                try
                { ix = this.cmbNCs.FindString(curdc.DefaultNamingContext); }

                catch (Exception ex)
                { ex.ToDummy(); }
                
                this.cmbNCs.SetIndex(ix); 

                LastGoodDC = this.cmbDCs.GetText();

                UpdatingForest = true;

                UpdateDCList();

                try
                { ix = this.cmbDCs.FindString(curdc.Name); }

                catch (Exception ex)
                { ex.ToDummy(); }

                this.cmbDCs.SetIndex(ix); 

                UpdatingForest = false;
            }

            else
            {
                LogDebugString(String.Format("DC ERROR - {0} ({1})", this.cmbDCs.GetText(), curdc.ErrorString));

                int ix = 0;

                try
                { ix = this.cmbDCs.FindString(LastGoodDC); }

                catch (Exception ex)
                { ex.ToDummy(); }

                this.cmbDCs.SetIndex(ix);                
            }

            ForestBase.CurrentDC = LastGoodDC;
        }
        
        protected void UpdateDNAttributes()
        {
            this.UpdatingAttributes = true;

            this.cmbAttribs.ClearItems();

            foreach (AttributeSchema attrib in ForestBase.AttributeCache.Values.Where(a => a.Syntax == ActiveDirectorySyntax.DN).ToArray())
            {
                this.cmbAttribs.AddItem(attrib);
            }

            this.cmbAttribs.SortItems(true);

            this.cmbAttribs.SetIndex(0);

            this.UpdatingAttributes = false;
        }

        #endregion

        #region whomai

        protected void CallWhoAmi()
        {
            MySelf.Load(this.cmbDCs.Text);

            LogMessage(MySelf.Info, this.txtOutput);
        }

        #endregion

        #region query

        protected void ExecQuery(string[] attributes = null)
        {
            CurrentMessages = new List<string> { };

            string sbase = CheckQueryCall(QueryInfo.RootDse);

            QueryInfo.SortKeys.Clear();

            //if (callrootdse)
            //{
            //    ExecRootDSEQuery();

            //    return;
            //}

            RecallStatsForm();

            // tbd - add sortkeys to QueryInfo.

            if (attributes == null)
            {
                if (QueryInfo.PerformSort)
                {
                    if (this.txtSort.GetTag() != null)
                    { QueryInfo.SortKeys.Add(((SortInfo)this.txtSort.GetTag()).BaseSortKey); }

                    //foreach(ListViewItem item in this.lvSort.GetItems())
                    //{
                    //    if (item.Tag != null)
                    //    { QueryInfo.SortKeys.Add(((SortInfo)item.Tag).BaseSortKey); }
                    //}
                }

                if (!QueryInfo.PerformASQ)
                { attributes = this.txtAttributes.GetLines().ToArray(); }

                else
                {
                    if (this.txtAttributes.GetLines().Count() == 0)
                    {
                        LogDebugString("ERROR: No attribute to query given!", true);

                        return;
                    }

                    attributes.AddSafe(this.txtAttributes.GetLines()[0], ref attributes);
                    
                    foreach (string item in this.txtASQAttribs.GetLines())
                    { attributes.AddSafe(item, ref attributes); }
                }  
            }

            if ((attributes.Count() == 0) && (!this.rbRootDSE.Checked))
            { attributes.AddSafe("0", ref attributes); }

            ResetQueryInfo();

            CurrentStartTime = DateTime.Now;

            ReferralChasingOptions refchasing = GetRefOptionsfromGui();

            ForestStore.CheckCustomAttributes(ref attributes);



            if (!QueryInfo.RunAsync)
            {
                RegisterQueryEvent();
                
                ForestStore.Query(this.cmbDCs.GetText(),
                                    sbase,
                                    this.txtFilter.GetText(),
                                    attributes,
                                    QueryScope,
                                    refchasing,
                                    QueryInfo);
                
            }

            else
            {
                GlobalEventHandler.ClearAsyncPartialResult();

                RegisterAsyncEvents();

                CurrentEntries = new List<SearchResultEntry>();

                ForestStore.AsyncQuery(this.cmbDCs.GetText(),
                                       sbase,
                                       this.txtFilter.GetText(),
                                       attributes,
                                       QueryScope,
                                       refchasing,
                                       QueryInfo);
            }
        }

        protected void CancelQuery(bool cancelResults = false)
        {
            CancelToken = true;

            CancelResults = cancelResults;

            LogDebugString(String.Format("We cancelled -> Query: {0}  show received results: {1}", CancelToken.ToString(), (!CancelResults).ToString()));

            CurrentMessages.AddFormatted("We cancelled -> Query: {0}  show received results: {1}", CancelToken.ToString(), (!CancelResults).ToString());

            GlobalEventHandler.RaiseSearchCancelled();

            this.gbCancel.SetVisibility(false);

            LogDebugString("Action cancelled");
        }

        protected void ExecRootDSEQuery()
        {
            this.txtAttributes.SetText("*");

            QueryInfo.QueryRuns = 0;

            ExecQuery();

            if (QueryInfo.QueryExtendedRootDSE)
            {         
                foreach (string attrib in MainBase.RootDseOperational.OperationalAttributes)
                {
                    QueryInfo.QueryRuns++;

                    ExecQuery(new string[] { attrib });
                }

                FinishResultHandling(true);
            }
            
        }

        protected string CheckQueryCall(bool callRootDse)
        {
            string ret = null;

            //callRootDse = false; // this.rbRootDSE.IsChecked();

            string filter = this.txtFilter.GetText();

            ret = this.txtSBase.GetText();

            if (callRootDse)
            {
                HandlePorts(true);
                
                HandleControls(true);

                if (filter.ToLowerInvariant() != "(objectClass=*)")
                { this.txtFilter.SetText("(objectClass=*)"); }

                return "";
            }

            else if ((ret == null) || (ret.Length == 0))
            { HandlePorts(false); }

            if ((filter == null) || (filter.Length == 0))
            { this.txtFilter.SetText("(objectClass=*)"); }            

            return ret;
        }

        protected void HandlePorts(bool assumeLDAP)
        {
            if (!rbCustomPort.IsChecked())
            {
                if (assumeLDAP)
                { 
                    this.rbDC.Check(true);

                    this.nuoPort.SetValue(ForestBase.CurrentPorts.LDAPPort);
                }

                else
                {
                    this.rbGC.Check(true);

                    this.nuoPort.SetValue(ForestBase.CurrentPorts.GCPort);
                }
            }
        }

        protected void HandlePortChange(bool assumeLDAP)
        {

        }

        protected void HandleControls(bool isRootDseCall)
        {
            if (isRootDseCall)
            {
                if (this.cbFullStats.Checked)
                {
                    LogDebugString("rootDSE call ->");
                    LogDebugString("  disabling Statistics");

                    this.cbFullStats.Checked = false;
                }

                if (this.Query_ShowDeleted_MenuItem.Checked)
                {
                    LogDebugString("rootDSE call ->");
                    LogDebugString("  disabling ShowDeleted");

                    this.Query_ShowDeleted_MenuItem.Checked = false;
                }

                if (this.Query_ShowRecycled_MenuItem.Checked)
                {
                    LogDebugString("rootDSE call ->");
                    LogDebugString("  disabling ShowRecycled");

                    this.Query_ShowRecycled_MenuItem.Checked = false;
                }
            }
        }

        protected void ResetQueryInfo()
        {
            CurrentCallTime = 0;
            CurrentCodeTime = 0;
            CurrentResultCount = 0;
            CurrentRuns = 0;

            QueryInfo.ContainsConstructedAttribute = false;
            QueryInfo.MustGetSingleObjectPath = false;
            QueryInfo.PartialRuns = -1;

            this.gbResult.SetText("Results (0)");            
        }

        protected ReferralChasingOptions GetRefOptionsfromGui()
        {
            ReferralChasingOptions ret = ReferralChasingOptions.None;

            if ((this.cbRefExternal.Checked) && (this.cbRefSubordinate.Checked))
            { ret = ret = ReferralChasingOptions.All; }

            else if (this.cbRefExternal.Checked)
            { ret = ret = ReferralChasingOptions.External; }

            else if (this.cbRefSubordinate.Checked)
            { ret = ret = ReferralChasingOptions.Subordinate; }

            return ret;
        }

        protected void HandlePartialResult(List<SearchResultEntry> entries, bool isComplete, SearchRequestExtender infoStore = null)
        {
            bool bgood = true;

            if (infoStore == null)
            { infoStore = ForestBase.CurrentRequestExtender; }
                        
            CurrentEntries.Capacity += (entries != null) ? entries.Count : 0;

            if (cbResults.Checked)
            {
                if ((entries != null) && (entries.Count != 0))
                {
                    if ((QueryInfo.SortResults) || (!MainBase.UserSettings.ShowPartialResults))
                    { CurrentEntries.AddRange(entries); }

                    else
                    { HandleResults(entries, infoStore, true); }
                }

                else
                { LogMessage("\tno results", this.txtOutput); }
            }

            if (isComplete)
            {
                if ((QueryInfo.SortResults) || (!MainBase.UserSettings.ShowPartialResults))
                { HandleResults(CurrentEntries, infoStore); }

                CurrentEntries.Clear();
                CurrentEntries.Capacity = 0;
                
                entries.Clear();

                if (!QueryInfo.QueryExtendedRootDSE)
                { FinishResultHandling(bgood); }

                GlobalEventHandler.RaiseDecodingCompleted();

                this.gbCancel.SetVisibility(false);
            }            
        }

        protected bool CheckQueryError(SearchRequestExtender infostore)
        {
            bool ret = infostore.HasError;

            if (infostore.HasError)
            { LogDebugString(infostore.ErrorMSG); }

            return ret;
        }

        protected void HandleResults(List<SearchResultEntry> result, SearchRequestExtender infostore, bool isPartial = false)
        {
            CurrentCallTime = (long)DateTime.Now.Subtract(CurrentStartTime).TotalMilliseconds;

            LogDebugString(String.Format("Handling results after calltime: {0} ms", CurrentCallTime.ToString()));

            List<string> res = new List<string> { };

            if (infostore.HasError)
            { res.AddFormatted("{1}{0}{1}", infostore.ErrorMSG, Environment.NewLine); }

            if (this.cbResults.Checked)
            {
                if ((result != null) && (result.Count != 0))
                {
                    CurrentCodeStartTime = DateTime.Now;

                    GlobalEventHandler.SubsequentQuery += SubsequentQuery;

                    if (QueryInfo.SortResults)
                    {
                        DateTime ordertime = DateTime.Now;

                        result = result.OrderByField("DistinguishedName", ascending: QueryInfo.SortAscending, startFieldValueAt: 2);

                        double tt = DateTime.Now.Subtract(ordertime).TotalMilliseconds;

                        LogDebugString(String.Format("Result sort time: {0} ms", DateTime.Now.Subtract(ordertime).TotalMilliseconds));
                    }

                    res = DecodeSearchResults(result, infostore);

                    GlobalEventHandler.SubsequentQuery -= SubsequentQuery;

                    CurrentCodeTime = (long)DateTime.Now.Subtract(CurrentCodeStartTime).TotalMilliseconds;

                    LogDebugString(String.Format("Subsequent queries: {0} ", CurrentRuns.ToString()));
                    LogDebugString(String.Format("Query time: {0} ms", CurrentCallTime.ToString()));
                    LogDebugString(String.Format("Code time: {0} ms", CurrentCodeTime.ToString()));
                }

                else
                {
                    if (QueryInfo.QueryRuns == 0)
                    { res.Add("\tno results"); }

                    else
                    {
                        res.AddFormatted("\t({0}){1}\t\t<no such attribute(s)>{1}", String.Join(", ", infostore.Attributes), Environment.NewLine);
                    }

                }
            }

            else
            {
                CurrentResultCount = CurrentEntries.Capacity;

                res.Add("No results required");
            }

            if (cbFullStats.Checked)
            {
                FillStatsData(infostore, (long)CurrentCodeTime);

                res.Add(Environment.NewLine + CurrrentStatsData);
            }

            bool append = (QueryInfo.PartialRuns == 0) ? false : isPartial;

            if (!append)
            { append = ((QueryInfo.QueryExtendedRootDSE) && (QueryInfo.QueryRuns != 0)); }
            
            LogMessage(res, this.txtOutput, append: append);


            if (((isPartial) && (QueryInfo.PartialRuns < 0)) || (!append))
            {
                res = new List<string> { };
                                
                res.AddRange(BuildQueryMessage(CurrentResultCount));

                res.AddRange(infostore.GetMessages());

                LogMessage(res, this.txtOutput, append: true, insertAt:0);
            }
            
            res.InvokeSafe("Clear");

            res = new List<string> { };
        }

        protected List<string> BuildQueryMessage(long resultCount)
        {
            List<string> ret = new List<string> { };

            ret.Add("DC: \t\t" + ForestBase.CurrentDC + ":" + QueryInfo.Port.ToString());

            if (QueryInfo.RootDse)
            { ret.Add("Base: \t\tString.Empty (rootDSE)"); }

            else if (QueryInfo.PhantomRoot)
            { ret.Add("Base: \t\tnull (PhantomRoot)"); }

            else
            { ret.Add("Base: \t\t" + this.txtSBase.GetText()); }

            ret.Add("Filter: \t\t" + this.txtFilter.GetText());

            ret.Add("Scope: \t\t" + QueryScope.ToString());

            ret.Add("ReferralChasing: \t" + QueryInfo.RefOptions.ToString());

            if (resultCount > -1)
            { ret.Add("ResultCount: \t" + resultCount); }

            ret.Add("");

            if (CurrentMessages.Count > 0)
            {
                ret.AddRange(CurrentMessages);

                ret.Add("");
            }

            return ret;
        }

        protected void FinishResultHandling(bool goodQuery)
        {
            this.gbResult.SetText(String.Format("Results ({0})", CurrentResultCount));

            QueryInfo.QueryRuns = 0;
            QueryInfo.PartialRuns = 10;

            if (goodQuery) UpdateFilterHistory();

            if (ForestBase.SchemaCacheIsDirty)
            { this.ForestStore.SaveCache(); }
        }

        protected void FillStatsData(SearchRequestExtender infoStore, long ms, bool invoking = false)
        {
            CurrrentStatsData = null;

            if (!invoking)
            {
                MethodInvoker delcall = () => { FillStatsData(infoStore, ms, true); };

                try
                { 
                    this.Invoke(delcall); 
                }

                catch (Exception ex)
                { ex.ToDummy(); }

            }

            else
            {
                if (infoStore.Statistics.Count != 0)
                {
                    int xpos = 0;
                    int ypos = 0;

                    xpos = this.Location.X + this.gbResult.Location.X + 400;
                    ypos = this.Location.Y + this.gbResult.Location.Y + 30;

                    StatsData statistics = new StatsData(infoStore.Statistics);

                    if (statistics.Filter.Length == 0)
                    { 
                        statistics.Filter = String.Format("null{0}(Assuming missing permissions or elevation required)", Environment.NewLine);

                        LogDebugString("Statistics: Assuming missing permissions or elevation required");
                    }

                    if (statistics.Index.Length == 0)
                    { statistics.Index = "null"; }

                    StatsForm = new StatsGUI(statistics.ToListView(ms), statistics.Filter, statistics.Index, xpos, ypos);
                                        
                    CurrrentStatsData = statistics.FormatToString(ms);

                    StatsForm.FormattedStatistics = CurrrentStatsData;

                    StatsForm.Show();                    
                }
            }
        }
 
        protected void RecallStatsForm()
        {
            try
            {
                if (StatsForm != null)
                {
                    StatsForm.Close();
                    StatsForm.Dispose();
                    StatsForm = null;
                }
            }
            catch (Exception ex) { ex.ToDummy(); }
        }

        #endregion

        #region dirsync

        /*
            forest im 0
            root im 1
            runs im 2
            run (datetime) im 3
        */
        
        protected void NewSyncRun()
        {
            string forestname = ForestBase.ForestName.ToFileNameValid();

            string rootname = this.cmbDomains.GetText().ToFileNameValid();

            if (this.rbPhantomRoot.Checked)
            { rootname = forestname; }

            //tbd
        }

        protected void LoadSyncRuns()
        {
            this.gbRunInfo.Visible = true;

            this.gbRunInfo.Enabled = true;

            QueryInfo.DirSyncReRun = false;
            QueryInfo.DirSyncCookie = null;

            foreach (KeyValuePair<string,SyncForest> kvpforest in DirSyncs.ForestList.Where(kvp => kvp.Key.ToLowerInvariant() == ForestBase.CurrentForestName))
            { AddForest(kvpforest); }
        }

        protected void AddForest(KeyValuePair<string, SyncForest> kvpForest)
        {
            TreeNode forest = new TreeNode();

            forest.Text = kvpForest.Key;
            forest.Name = kvpForest.Key;
            forest.Tag = kvpForest.Value;
            forest.ImageIndex = 0;
            forest.SelectedImageIndex = 0;

            this.tvSyncRuns.Nodes.Add(forest);

            WalkRuns(forest, kvpForest.Value.SyncRuns);

            WalkRoots(forest, kvpForest.Value.RootList);
        }

        protected void WalkRuns(TreeNode parent, List<SyncRun> syncRuns)
        {
            foreach (SyncRun run in syncRuns)
            {
                //add run

                //ImageIndex 2
            }
        }

        protected void WalkRoots(TreeNode parent, Dictionary<string, SyncRoot> rootList)
        {
            //ImageIndex 1

            //foreach root
            //{
                //add root

                //WalkRuns
            //}
        }

        protected void WalkCookies(TreeNode parent, SyncRun run)
        {
            //ImageIndex 3
        }

        #endregion

        #region filterhistory

        protected void LoadFilterHistory()
        {
            if (!(this.lvFilterHistory.GetItems().Count == 0))
            { this.lvFilterHistory.ClearItems(); }

            if (!Directory.Exists(new FileInfo(FilterHistoryPath).Directory.FullName))
            { Directory.CreateDirectory(new FileInfo(FilterHistoryPath).Directory.FullName); }

            if ((File.Exists(Path.Combine(GlobalHelper.CurrentDirectory, FilterHistoryFile))) && (!File.Exists(FilterHistoryPath)))
            { File.Move(Path.Combine(GlobalHelper.CurrentDirectory, FilterHistoryFile), FilterHistoryPath); }

            if (File.Exists(FilterHistoryPath))
            {
                UpdatingFilterHistory = true;

                string content = null;
                string[] sep = new string[1] { FilterInfo.EndFilter };

                content = File.ReadAllText(FilterHistoryPath, Encoding.Unicode);

                string[] history = content.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in history)
                {

                    FilterInfo fi = new FilterInfo(item);

                    ListViewItem li = new ListViewItem(fi.Items());
                        
                    li.Tag = fi;

                    this.lvFilterHistory.AddItem(li, 0, false);
                }

                UpdatingFilterHistory = false;
            }

            this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.Check, ColumnHeaderAutoResizeStyle.HeaderSize);

            this.lvFilterHistory.SetColumnTag((int)HISTORY_COLUMNS.Filter, true);
            this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.Filter, ColumnHeaderAutoResizeStyle.ColumnContent);

            this.lvFilterHistory.SetColumnTag((int)HISTORY_COLUMNS.Attributes, true);
            this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.Attributes, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        protected void UpdateFilterHistory(bool delete = false, bool remove = false, bool removeSelected = false)
        {
            bool save = false;

            if (delete)
            {
                this.lvFilterHistory.ClearItems();

                try
                { File.Delete(FilterHistoryPath); }

                catch { }
            }

            else if (remove)
            { save = this.lvFilterHistory.RemoveSelectedItem(); }

            else if (removeSelected)
            { save = this.lvFilterHistory.RemoveSelectedItems(); }

            else
            {
                FilterInfo fi = new FilterInfo();

                fi.Filter = this.txtFilter.GetText();

                fi.Attributes = this.txtAttributes.GetLines().ToArray();
                if (fi.Attributes.Count() == 0)
                { fi.Attributes = new string[] { "0" }; }

                fi.ASQ = this.cbASQ.IsChecked() ? this.txtASQAttribs.GetLines().ToArray() : new string[] { "0" }; ;
                if (fi.ASQ.Count() == 0)
                { fi.ASQ = new string[] { "0" }; }

                //fi.Sort = this.cbSort.IsChecked() ? (SortInfo)this.lvSort.GetItems()[0].Tag : null;

                fi.Sort = this.cbSort.IsChecked() ? (SortInfo)this.txtSort.GetTag() : null;

                fi.DC = this.cmbDCs.GetText();

                fi.SearchBase = QueryInfo.RootDse ? "" : this.txtSBase.GetText();

                fi.Scope = (int)QueryScope;

                fi.Port = this.QueryInfo.Port;

                save = this.lvFilterHistory.InsertItemAt(fi, 0, true, fi.Items());
            }

            if (save)
            {
                List<string> content = new List<string> { };
                
                List<ListViewItem> col = this.lvFilterHistory.GetItems();

                foreach (ListViewItem litem in col)
                {
                    content.Add(((FilterInfo)litem.Tag).ToLine());
                }

                File.WriteAllText(FilterHistoryPath, String.Join("", content), Encoding.Unicode);

                LoadFilterHistory();
            }
        }

        protected void PassFilterFromHistory(int curPos = -1)
        {
            if (!(UpdatingFilterHistory))
            {
                if ((this.lvFilterHistory.Items.Count > 0) && (this.lvFilterHistory.SelectedItems.Count > 0))
                {
                    curPos = (curPos != -1) ? curPos : 0;

                    FilterInfo fi = (FilterInfo)this.lvFilterHistory.SelectedItems[curPos].Tag;

                    this.txtAttributes.SetLines(fi.Attributes);

                    this.txtFilter.SetText(fi.Filter);
                    this.txtFilter.Select();

                    //FilterHistoryControl fhc = (FilterHistoryControl)this.lvFilterHistory.Tag;

                    if ((MainBase.UserSettings.HistoryASQ) && (fi.ASQ != null) && (fi.ASQ[0] != "0"))
                    {
                        this.cbASQ.Check(true);

                        int idx = this.cmbAttribs.FindString(fi.Attributes[0]);

                        if (idx >= 0)
                        { this.cmbAttribs.SelectedIndex = idx; }

                        this.txtASQAttribs.SetLines(fi.ASQ);
                    }

                    else
                    { this.cbASQ.Check(false); }

                    if ((MainBase.UserSettings.HistorySorting) && (fi.Sort != null))
                    {
                        this.cbSort.Check(true);

                        AddSortingAttribute(fi.Sort.AttributeName, fi.Sort.ReverseOrder);
                    }

                    else
                    { this.cbSort.Check(false); }

                    if ((MainBase.UserSettings.HistoryDC) && (fi.DC != "0"))
                    {
                        int pos = this.cmbDCs.FindString(fi.DC, 0);

                        if (pos != -1)
                        {
                            this.cmbDCs.SelectedIndex = pos;
                        }

                        else
                        { LogDebugString(String.Format("WARNING: DC {0} from saved filter set is not present in this domain", fi.DC)); }
                    }

                    if ((MainBase.UserSettings.HistoryPort) && (fi.Port != 0))
                    {
                        if (fi.Port == ForestBase.CurrentPorts.LDAPPort)
                        { this.rbDC.Check(true); }

                        else if (fi.Port == ForestBase.CurrentPorts.GCPort)
                        { this.rbGC.Check(true); }

                        else
                        {
                            this.rbCustomPort.Check(true);

                            this.nuoPort.SetValue(fi.Port);
                        }                         
                    }

                    if ((MainBase.UserSettings.HistoryBase) && (fi.SearchBase != "0"))
                    {
                        if (fi.SearchBase == "")
                        {
                            this.rbRootDSE.Check(true);
                        }

                        else if (fi.SearchBase == "null")
                        {
                            this.rbPhantomRoot.Check(true);
                        }

                        else
                        {
                            this.txtSBase.SetText(fi.SearchBase);
                        }
                    }

                    if ((MainBase.UserSettings.HistoryScope) && (fi.Scope != -1))
                    {
                        switch (fi.Scope)
                        {
                            case 0:
                                this.rbBase.Check(true);
                                break;

                            case 1:
                                this.rbOneLevel.Check(true);
                                break;

                            case 2:
                                this.rbSubtree.Check(true);
                                break;
                        }
                    }
                }
            }

            //this.lvFilterHistory.SelectedItems[0].Checked = false;
        }

        protected void UpdateFilterHistoryTag(HISTORY_COLUMNS col, bool value)
        {
            MainBase.UserSettings.RegisterEvents();

            GlobalEventHandler.RaiseSettingChanged(col.ToString(), value);

            MainBase.UserSettings.UnRegisterEvents();

            FilterHistoryControl fhc = (FilterHistoryControl)this.lvFilterHistory.GetTag();

            switch (col)
            {
                case HISTORY_COLUMNS.HistoryASQ:
                    fhc.UseASQ = value;
                    break;

                case HISTORY_COLUMNS.HistorySorting:
                    fhc.UseSort = value;
                    break;

                case HISTORY_COLUMNS.HistoryBase:
                    fhc.UseBase = value;
                    break;

                case HISTORY_COLUMNS.HistoryDC:
                    fhc.UseDC = value;
                    break;

                case HISTORY_COLUMNS.HistoryScope:
                    fhc.UseScope = value;
                    break;

                case HISTORY_COLUMNS.HistoryPort:
                    fhc.UsePort = value;
                    break;
            }
        }

        #endregion

        #region attributes

        protected void LoadAttribsFromSchema(bool showList = true)
        {
            if (this.lbAttribs.Items.Count == 0)
            {
                this.lbAttribs.Items.Add("0 -> no attribs");
                this.lbAttribs.Items.Add("* -> all attribs");
                this.lbAttribs.Items.AddRange(ForestBase.AttributeCache.Select(a => a.Key).ToArray());
                this.lbAttribs.Sorted = true;
            }

            if (showList)
            {
                this.gbProps.Visible = true;

                this.lbAttribs.Select();
            }
        }

        protected void AddAttribute()
        {
            if (this.lbAttribs.SelectedIndex == ListBox.NoMatches) return;

            string attrib = this.lbAttribs.SelectedItem.ToString();

            List<string> lines = null;
            
            if (this.AddASQAttribute)
            {
                lines = this.txtASQAttribs.GetLines();

                lines.Add(attrib);

                this.txtASQAttribs.SetLines(lines);
            }

            else if (this.AddSortAttribute)
            {
                AddSortingAttribute(attrib);
            }

            else
            {
                lines = this.txtAttributes.GetLines();

                lines.Add(this.lbAttribs.SelectedItem.ToString());

                this.txtAttributes.SetLines(lines);
            }            
        }

        protected void AddSortingAttribute(string attrib, bool? reverse = null)
        {
            string removeattrib = null;
                      
            string temp = this.txtSort.GetText();

            if ((temp != null) && (temp.Length > 0))
            { removeattrib = temp; }

            if (reverse == null)
            { reverse = this.cbReverse.IsChecked(); }

            SortInfo tag = new SortInfo(attrib, (bool)reverse, null);

            this.txtSort.SetText(attrib);
            this.txtSort.SetTag(tag);

            //ListViewItem li = new ListViewItem(new string[] { "", attrib });
            //li.Tag = tag;

            //save = this.lvSort.InsertItemAt(li, 0, true);

            //if ((save) && (this.lvSort.Items.Count > 1))
            //{
            //    if (this.lvSort.Items[this.lvSort.Items.Count - 1].Tag != null)
            //    { removeattrib = ((SortInfo)this.lvSort.Items[this.lvSort.Items.Count - 1].Tag).AttributeName; }

            //    this.lvSort.Items.RemoveAt(this.lvSort.Items.Count - 1);
            //}

            HandleSortAttributes(attrib, removeattrib);
        }

        protected void HandleSortAttributes(string attrib, string removeAttrib = null)
        {
            List<string> lines = null;

            if ((this.txtASQAttribs.Visible) && (this.AddSortAttribute))
            {
                lines = this.txtASQAttribs.GetLines();

                if (!lines.Contains(attrib))
                {
                    lines.Add(attrib);

                    this.txtASQAttribs.SetLines(lines);
                }
            }

            else if (this.AddSortAttribute)
            {
                bool save = false;

                lines = this.txtAttributes.GetLines();

                if (!lines.Contains(attrib))
                {
                    lines.Add(attrib);

                    save = true;
                }

                // to be thought about :-)
                //if ((removeAttrib != null) && (lines.Contains(removeAttrib)))
                //{
                //    lines.Remove(removeAttrib);

                //    save = true;
                //}

                if (save)
                { this.txtAttributes.SetLines(lines); }
            }
        }

        #endregion

        #region output

        protected void ChangeFont()
        {
            DialogResult fres = diaFont.ShowDialog();

            if (fres == System.Windows.Forms.DialogResult.OK)
            { this.txtOutput.Font = diaFont.Font; }
        }

        protected void FindInResults()
        {
            int start = 0;

            if ((int)this.gbFind.Tag != 0)
            {
                try
                { start = this.txtOutput.SelectionStart + 1; }

                catch 
                { start = 0; }
            }

            GlobalControlHandler.FindStringInTextBox(this.txtOutput, this.txtFind.Text, start);

            this.gbFind.Tag = (int)this.gbFind.Tag + 1;

            this.txtOutput.Focus();
        }

        #endregion
        
        #region wizards

        protected void LoadFilterWizard()
        {
            if (!(QBLoaded))
            {
                QBuilder = new QueryBuilder(this.cmbDCs.GetText()); //, ForestStore);

                GlobalEventHandler.FilterSignaled += UpdateFilter;
            }

            QBuilder.Show();

            QBLoaded = true;
        }

        protected void LoadConnectionWizard(bool credsOnly = false)
        {
            if (!CBuilderLoaded)
            {
                CBuilder = new ConnectionGUI(ForestBase.ForestName, credsOnly);

                GlobalEventHandler.ConnectionSignaled += ConnectionSignaled;

                CBuilder.Show();

                CBuilderLoaded = true;
            }
        }

        protected void CallBrowser(LDAPBrowser.CALLING_CONTROL whocalled)
        {
            switch (whocalled)
            { 
                case LDAPBrowser.CALLING_CONTROL.GUI_BASE:
                    GlobalEventHandler.GuiBaseObjectSelected += BaseObjectSelectedEvent;

                    LastBase = this.txtSBase.GetText();

                    break;

                case LDAPBrowser.CALLING_CONTROL.GUI_ASQ:

                    break;

                default:
                    break;
            }
            

            GlobalControlHandler.CallBrowser(ref LdapTree, whocalled, QueryInfo, this.txtSBase.GetText(), this.cmbDCs.GetText(), ForestStore); 
        }
        
        protected void LoadAttributeTypeAssociator()
        {
            if ((!ATABuilderLoaded) || (ATABuilder.IsDisposed))
            {
                ATABuilder = new AttributeTypeAssociator();

                GlobalEventHandler.FormExited -= ATABuilderClosed; ;
            }

            ATABuilder.Show();

            ATABuilderLoaded = true;
        }

        #endregion

        #region messages
        
        protected void ClearResult()
        {
            GlobalControlHandler.ClearTextBox(this.txtOutput);

            this.gbResult.SetText("Results");
        }

        protected void MoveResult()
        {
            LogMessage(ResultList[CurrentResultPosition].DecodedMsg, this.txtOutput, isMove:true);

            this.gbResult.SetText(String.Format("Results ({0})", ResultList[CurrentResultPosition].ResultCount));

            this.txtOutput.Select(0, 0);

            this.txtOutput.ScrollToCaret();
        }

        protected void LogMessage(List<string> text, Control txt, bool isMove = false, bool append = false, int insertAt = -1)
        { 
            GlobalControlHandler.UpdateTextBox(txt, text, append, insertAt:insertAt);

            if (txt.Name == this.txtOutput.Name)
            { HandleResultList(text, isMove, append, insertAt: insertAt); }
        }

        protected void HandleResultList(List<string> text, bool isMove, bool append, bool clear = false, int insertAt = -1)
        {
            bool storei = false;
            bool previ = false;
            bool nexti = false;
            bool cleari = false;
            string storetext = "Remembered results (0 of 0)";

            List<string> textlist = new List<string> { };

            if ((append) && (text != null))
            {
                textlist.AddRange(text);

                if (ResultList.Count == 0)
                { ResultList.Add(new MessageCache(textlist, CurrentResultCount)); }

                else if (insertAt == -1)
                { ResultList[CurrentResultPosition].AddMessages(textlist, CurrentResultCount); }

                else
                { ResultList[CurrentResultPosition].InsertMessages(insertAt, textlist); }
            }

            else if (clear)
            {
                CurrentResultPosition = 0;

                ResultList.DisposeSafe();

                ClearResult();
            }

            else if ((!isMove) && (text != null))
            {
                textlist.AddRange(text);

                if (ResultList.Count >= MainBase.UserSettings.MaxResultListCount)
                { ResultList.RemoveAt(0); }

                ResultList.Add(new MessageCache(textlist, CurrentResultCount));

                CurrentResultPosition = ResultList.Count - 1;
            }

            if (ResultList.Count > 1)
            {
                storei = true;
                storetext = String.Format("Remembered results ({0} of {1})", CurrentResultPosition + 1, ResultList.Count);                

                cleari = true;
                nexti = (CurrentResultPosition != ResultList.Count - 1);
                previ = (CurrentResultPosition != 0);
            }

            HandleResultBoxSize();

            GlobalControlHandler.ControlSetText(this.gbResStore, storetext);
            GlobalControlHandler.ControlSetVisibility(this.gbResStore, storei);

            GlobalControlHandler.ControlSetState(this.cmdClearResults, cleari);

            GlobalControlHandler.ControlSetState(this.cmdNextResult, nexti);
            GlobalControlHandler.ControlSetState(this.cmdLastResult, nexti);

            GlobalControlHandler.ControlSetState(this.cmdPrevResult, previ);
            GlobalControlHandler.ControlSetState(this.cmdFirstResult, previ);

            GlobalControlHandler.MenuSetState(this.Result_Remembered_ContextItem, storei);
            GlobalControlHandler.MenuSetState(this.Results_Remembered_MenuItem, storei);

            GlobalControlHandler.MenuSetState(this.Result_Remembered_Clear_ContextItem, cleari);
            GlobalControlHandler.MenuSetState(this.Results_Remembered_Clear_MenuItem, cleari);

            GlobalControlHandler.MenuSetState(this.Result_Remembered_First_ContextItem, previ);
            GlobalControlHandler.MenuSetState(this.Results_Remembered_First_MenuItem, previ);

            GlobalControlHandler.MenuSetState(this.Result_Remembered_Previous_ContextItem, previ);
            GlobalControlHandler.MenuSetState(this.Results_Remembered_Prev_MenuItem, previ);

            GlobalControlHandler.MenuSetState(this.Result_Remembered_Next_ContextItem, nexti);
            GlobalControlHandler.MenuSetState(this.Results_Remembered_Next_MenuItem, nexti);

            GlobalControlHandler.MenuSetState(this.Result_Remembered_Last_ContextItem, nexti);
            GlobalControlHandler.MenuSetState(this.Results_Remembered_Last_MenuItem, nexti);
        }

        protected void HandleResultBoxSize()
        {
            int ypoint = 16;

            if (this.gbResStore.Visible)
            { ypoint = this.gbResStore.Location.Y + gbResStore.Height; }

            else if (this.gbFind.Visible)
            { ypoint = this.gbFind.Location.Y + this.gbFind.Height; }

            GlobalControlHandler.ControlSetLocation(this.txtOutput, new Point(2, ypoint));
            GlobalControlHandler.ControlSetSize(this.txtOutput, new Size(this.gbResult.Width - 4, this.gbResult.Height - (ypoint + 2)));
        }

        protected void FirstResult()
        {
            CurrentResultPosition = 0;

            MoveResult();
        }

        protected void PrevResult()
        { 
            CurrentResultPosition--;

            MoveResult();
        }

        protected void NextResult()
        {
            CurrentResultPosition++;

            MoveResult();
        }
        
        protected void LastResult()
        {
            CurrentResultPosition = ResultList.Count - 1;

            MoveResult();
        }

        protected void ClearResultList()
        { HandleResultList(null, false, false, true); }

        protected void LogMessage(string text, Control txt)
        { LogMessage(new List<string> { text }, txt, append: true); }

        protected void LogDebugString(string text, bool showtext = false)
        {
            GlobalControlHandler.UpdateControlVisibility(this.gbProps, false);

            GlobalControlHandler.AppendTextToListBox(this.lbDebugLog, text);            

            if (showtext)
            { LogMessage(text, this.txtOutput); }
        }

        #endregion

        #region decoding

        protected List<string> DecodeSearchResults(List<SearchResultEntry> result, SearchRequestExtender infoStore)
        {
            LogDebugString("Decoding results");

            List<string> ret = new List<string> { };

            DecodeStatistics = new Dictionary<string, List<double>> ();

            DecodeHandler = DecodeHandlerStep;

            long count = CurrentResultCount;

            string pgid = String.Empty;

            CurrentResultCount += result.Count;

            if (cbResults.Checked)
            {
                ret.AddRange(WalkEntries(result, infoStore, ref count));

                ReportDecodeStatistics();
            }

            if (infoStore.QueryInfo.MustGetSingleObjectPath)
            { CurrentRuns += result.Count; }

            this.gbResult.SetText(String.Format("Results ({0} of {1})", count, CurrentResultCount));

            return ret;
        }

        protected string DNHelper(SearchResultEntry sre)
        {
            string ret = null;

            if (this.rbRootDSE.Checked)
            { ret = String.Format("{0}/rootDSE", ForestBase.CurrentDC); }

            else
            { ret = sre.DistinguishedName; }

            return ret;
        }

        protected List<string> WalkEntries(List<SearchResultEntry> result, SearchRequestExtender infoStore, ref long count)
        {
            List<string> ret = new List<string> { };

            DomainController metadc = null;

            if ((QueryInfo.PerformDirSync) && (QueryInfo.DirSyncReRun))
            { metadc = ForestStore.GetDcByName(this.cmbDCs.GetText()); }

            foreach (SearchResultEntry entry in result)
            {
                count++;

                if (QueryInfo.FirstRun)
                {
                    if (!infoStore.ShowAttributes)
                    { ret.AddFormatted("{1} DN: <{0}>", DNHelper(entry), count); }

                    else
                    { ret.AddFormatted("{1} {0}DN: <{1}>", Environment.NewLine, DNHelper(entry), count); }
                     
                }

                if (infoStore.ShowAttributes)
                {
                    if (entry.Attributes.Values.Count == 0)
                    {
                        if (infoStore.Attributes != null)
                        { ret.AddFormatted("\t({0}){1}\t\t<no such attribute(s)>", String.Join(", ", infoStore.Attributes), Environment.NewLine); }

                        else
                        { ret.Add("\t(no attributes returned)"); }
                    }

                    else
                    {
                        ActiveDirectoryReplicationMetadata metadata = null;

                        if ((metadc != null) && (QueryInfo.PerformDirSync) && (QueryInfo.DirSyncReRun))
                        { metadata = metadc.GetReplicationMetadata(entry.DistinguishedName); }

                        infoStore.AdjustAttributesToDisplay(entry.Attributes, QueryInfo.DoValueRangeRetrieval);

                        ret.AddRange(CheckRangedAttributes(entry, infoStore));

                        ret.AddRange(WalkNonRangedAttributes(entry, infoStore, metadata));

                        ret.AddRange(WalkRangedAttributes(entry, infoStore));
                    }
                }

                if (infoStore.ShowAttributes)
                { ret.Add(""); }

                ShowDecodingProgress(count, CurrentResultCount);

                if (CancelResults)
                { break; }
            }

            return ret;
        }

        protected List<string> CheckRangedAttributes(SearchResultEntry entry, SearchRequestExtender infoStore)
        {
            List<string> ret = new List<string> { };

            if ((infoStore.CurrentRangeRetrieval.Count != 0) && !QueryInfo.DoValueRangeRetrieval)
            {
                foreach (RangeInfo range in infoStore.CurrentRangeRetrieval.Values)
                {
                    GlobalEventHandler.RaiseErrorOccured(String.Format("{0}{3}  {1} exceeds maxValRange (retreived {2})", entry.DistinguishedName, range.Name, range.CurrentRangeName, Environment.NewLine));
                      
                    ret.AddFormatted("\t{0} exceeds maxValRange (retreived {1})", range.Name, range.CurrentRangeName); 
                }
            }

            return ret;
        }

        protected List<string> WalkNonRangedAttributes(SearchResultEntry entry, SearchRequestExtender infoStore, ActiveDirectoryReplicationMetadata metaData)
        {
            List<string> ret = new List<string> { };

            foreach (string attribname in infoStore.CurrentAttributesToDisplay.Keys)
            {              
                if (!(entry.Attributes.Contains(attribname)) && !MainBase.UserSettings.IgnoreEmpty)
                {
                    ret.AddFormatted("\t{0}:", attribname);
                    ret.Add("\t\t<not present>"); 
                }

                else if (entry.Attributes.Contains(attribname))
                {
                    ret.AddFormatted("\t{0}:", attribname);

                    ret.AddRange(WalkAttribute(entry, attribname, metaData)); 
                }
            }

            return ret;
        }
                
        protected List<string> WalkRangedAttributes(SearchResultEntry entry, SearchRequestExtender infoStore)
        {
            List<string> ret = new List<string> { };

            if ((infoStore.CurrentRangeRetrieval.Count != 0) && QueryInfo.DoValueRangeRetrieval)
            {
                foreach (RangeInfo range in infoStore.CurrentRangeRetrieval.Values)
                {
                    if (!(entry.Attributes.Contains(range.Name)) && !MainBase.UserSettings.IgnoreEmpty)
                    {
                        ret.AddFormatted("\t{0}:", range.Name);

                        ret.Add("\t\t<not present>"); 
                    }

                    else if (entry.Attributes.Contains(range.Name))
                    {
                        ret.AddFormatted("\t{0}:", range.Name);

                        GlobalEventHandler.RaiseErrorOccured(String.Format("{0}{2}  Starting ValueRangeRetreival for {1}", entry.DistinguishedName, range.FirstRangeName, Environment.NewLine));

                        ret.AddFormatted("\t   <({0})>", range.CurrentRangeName);

                        ret.AddRange(WalkAttribute(entry, range.FirstRangeName));

                        string sbase = entry.DistinguishedName;

                        SearchScope scope = SearchScope.Base;

                        string filter = "(objectClass=*)";

                        ReferralChasingOptions refchasing = GetRefOptionsfromGui();

                        bool breakwhile = false;

                        DateTime start;
                        
                        while (!breakwhile)
                        {
                            breakwhile = CancelResults ? true : breakwhile;

                            //LogDebugString("Canceltoken: " + CancelToken.ToString());
                            string nextrange = range.NextRange();

                            GlobalEventHandler.RaiseErrorOccured(String.Format("  ValueRangeRetreival {0}", nextrange));

                            string[] attributes = new string[] { nextrange };

                            start = DateTime.Now;

                            QUERY_RESULT_EVENT_TYPE storetype = QueryInfo.CurrentResultEventType;

                            QueryInfo.CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_GUI;

                            //List<SearchResultEntry> result = ForestStore.Query(this.cmbDCs.GetText(),
                            //                                                    sbase,
                            //                                                    filter,
                            //                                                    attributes,
                            //                                                    scope,
                            //                                                    refchasing,
                            //                                                    new QueryControl() { CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_GUI },
                            //                                                    returnResults: true);

                            
                            List<SearchResultEntry> result = ForestStore.Query(this.cmbDCs.GetText(),
                                                                                sbase,
                                                                                filter,
                                                                                attributes,
                                                                                scope,
                                                                                refchasing,
                                                                                QueryInfo,
                                                                                returnResults: true);

                            GlobalEventHandler.RaiseSubsequentQuery((long)DateTime.Now.Subtract(start).TotalMilliseconds);

                            QueryInfo.CurrentResultEventType = storetype;

                            if (result.Count != 0)
                            {
                                foreach (string name in result[0].Attributes.AttributeNames)
                                {
                                    List<string> tempret = WalkAttribute(result[0], name);

                                    if (name.ToLowerInvariant() == attributes[0].ToLowerInvariant())
                                    { ret.AddFormatted("\t   <({0})>", range.CurrentRangeName); }

                                    else
                                    {   
                                        ret.AddFormatted("\t   <(range={0}-{1})>", range.Low, range.Low + tempret.Count);

                                        string rinfo = string.Format("{0};range={1}-{2}", range.Name, range.Low, range.Low + tempret.Count);

                                        GlobalEventHandler.RaiseErrorOccured(String.Format("  Last ValueRangeRetreival {0}", rinfo));

                                        breakwhile = true;
                                    }

                                    ret.AddRange(tempret);

                                    break;
                                }
                            }

                            else
                            { break; }

                            if (breakwhile)
                            { break; }
                        }
                    }                    
                }
            }

            return ret;
        }

        protected List<string> WalkAttribute(SearchResultEntry entry, string attribName, ActiveDirectoryReplicationMetadata metaData = null)
        {
            List<string> ret = new List<string> { };

            DirectoryAttribute deAttrib = entry.Attributes[attribName];

            // to be deleted
            //bool tsmatch = false;
            //bool tsexclusive = false;
            //ActiveDirectorySyntax tssyntax = ActiveDirectorySyntax.OctetString;

            //List<string> tsres = new List<string>();

            //DirectoryAttribute tsattrib = new DirectoryAttribute("userParameters");

            //tsres = ForestStore.TryCustomDecoding(tsattrib, entry, ref tsmatch, ref tsexclusive, tssyntax);

            //if (tsmatch)
            //{ ret.AddRange(tsres); }
            // to be deleted

            if (deAttrib.Count == 0)
            { ret.Add("\t\t<not set>"); }

            else
            {
                DateTime start = DateTime.Now;

                if (metaData != null)
                { ret.Add(OriginatingChange(metaData, attribName)); }

                bool isbad;

                ActiveDirectorySyntax asyntax = ForestStore.GetAttributeSyntax(attribName, out isbad);

                double ms = DateTime.Now.Subtract(start).TotalMilliseconds;

                if (ms > 20)
                { LogDebugString(String.Format("{0} SyntaxCheck in: {1} ms", attribName, ms)); }

                if (isbad)
                { ret.AddFormatted("\t\t<UnKnown syntax> {0}", deAttrib.Name); }

                start = DateTime.Now;

                bool exclusive = false;

                List<string> tres = new List<string>();

                tres = ForestStore.TryDynamicTypeDecoding(deAttrib, ref exclusive);

                if (exclusive)
                { ret.AddRange(tres); }

                else
                {
                    bool match = false;

                    tres = new List<string>();

                    tres = ForestStore.TryCustomDecoding(deAttrib, entry, ref match, ref exclusive, asyntax);

                    if (match)
                    { ret.AddRange(tres); }
                }

                if (!exclusive)
                {
                    bool ismatch = false;

                    List<string> rlist;

                    rlist = ForestStore.TryDynamicTypeDecoding(deAttrib, ref ismatch);

                    if (ismatch)
                    { ret.AddRange(rlist); }

                    else
                    {
                        switch (asyntax)
                        {
                            case ActiveDirectorySyntax.OctetString:
                            case ActiveDirectorySyntax.Sid:
                            case ActiveDirectorySyntax.ReplicaLink:

                                ret.AddRange(DecodeByteData(deAttrib, asyntax, out isbad));
                                break;

                            case ActiveDirectorySyntax.Int64:
                            case ActiveDirectorySyntax.GeneralizedTime:
                            case ActiveDirectorySyntax.UtcTime:

                                ret.AddRange(DecodeInt64Data(deAttrib, asyntax, out isbad));
                                break;


                            case ActiveDirectorySyntax.SecurityDescriptor:

                                ret.AddRange(DecodeSDData(deAttrib, asyntax));
                                break;

                            case ActiveDirectorySyntax.Int:

                                ret.AddRange(DecodeIntData(deAttrib, asyntax, out isbad));
                                break;

                            default:

                                ret.AddRange(DecodeStringData(deAttrib, asyntax, out isbad));
                                break;
                        }

                        ms = DateTime.Now.Subtract(start).TotalMilliseconds;

                        StoreDecodeStatistics(attribName, ms);

                        if (isbad)
                        { this.ForestStore.MarkAttributeAsBad(ForestBase.ForestName, attribName); }
                    }
                }
            }

            return ret;
        }

        protected string OriginatingChange(ActiveDirectoryReplicationMetadata metaData, string attribName)
        {
            string ret = null;

            try
            {
                if (metaData.AttributeNames.Contains(attribName))
                {
                    if (metaData[attribName].OriginatingServer != null)
                    {
                        //if (UtdVectors[metaData[attribName].OriginatingServer] < metaData[attribName].OriginatingChangeUsn)
                        //{
                        //    ret = String.Format(" (on {0})", metaData[attribName].OriginatingServer);
                        //}

                        ret = String.Format("\t (on {0})", metaData[attribName].OriginatingServer);
                    }
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        protected List<string> DecodeByteData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad)
        {
            List<String> ret = new List<string> { };

            ret = this.ForestStore.DecodeByteData(attrib, syntax, out isBad);

            return ret;
        }

        protected List<string> DecodeStringData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad)
        {
            List<String> ret = new List<string> { };

            ret = this.ForestStore.DecodeStringData(attrib, syntax, out isBad);

            return ret;
        }

        protected List<string> DecodeIntData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad)
        {
            List<String> ret = new List<string> { };

            ret = this.ForestStore.DecodeIntData(attrib, syntax, out isBad);

            return ret;
        }

        protected List<string> DecodeInt64Data(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad)
        {
            List<String> ret = new List<string> { };

            string plaind = null;

            ret = this.ForestStore.DecodeInt64Data(attrib, syntax, out isBad, out plaind);

            return ret;
        }

        protected List<string> DecodeSDData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax)
        {
            List<String> ret = new List<string> { };

            ret = this.ForestStore.DecodeSDData(attrib, syntax);

            return ret;
        }

        protected void StoreDecodeStatistics(string name, double ms)
        {
            DecodeStatistics.AddSafe<string, List<double>>(name, new List<double> { ms });

            DecodeStatistics[name].Add(ms);
        }

        protected void ReportDecodeStatistics()
        {
            foreach (KeyValuePair<string, List<double>> stats in DecodeStatistics)
            {
                double ms = stats.Value.Sum() / stats.Value.Count;

                string avg = (stats.Value.Count > 1) ? " (avg)" : String.Empty;

                if (ms > 10)
                { LogDebugString(String.Format("{0} decoded in: {1} ms{2}", stats.Key, Math.Round(ms, 3), avg)); }
            }
        }

        protected void ShowDecodingProgress(long current, long overAll)
        {
            if (current == 0)
            {
                this.gbResult.SetText(String.Format("(0 of {0})", overAll));
            }

            else
            {
                if ((double)(current / DecodeHandler) == 1)
                {
                    this.gbResult.SetText(String.Format("({0} of {1})", current, overAll));
                    DecodeHandler += DecodeHandlerStep;
                }
            }
        }

        #endregion
    
        #region event handling

        protected void RegisterQueryEvent()
        { GlobalEventHandler.QueryCompleted += QueryCompleted; }

        protected void DeRegisterQueryEvent()
        { GlobalEventHandler.QueryCompleted -= QueryCompleted; }

        protected void QueryCompleted(object infoStore, GlobalEventArgs args)
        {
            if ((args.ResultEventType & QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH) == QUERY_RESULT_EVENT_TYPE.FROM_SINGLE_OBJECT_PATH)
            { return; }

            bool iscompleted = (args.ResultEventType == QUERY_RESULT_EVENT_TYPE.IS_COMPLETED);

            if (iscompleted)
            { 
                DeRegisterQueryEvent();
                QueryInfo.PartialRuns = -10;
            }

            else
            { QueryInfo.PartialRuns++; }

            HandlePartialResult(args.Entries, iscompleted, (SearchRequestExtender)infoStore);
        }

        protected void DecodingCompleted(object sender, GlobalEventArgs args)
        { NextBulk = true; }

        protected void RegisterAsyncEvents()
        { GlobalEventHandler.AsyncPartialResult += AsyncPartialResult; }

        protected void DeregisterAsyncEvents()
        { GlobalEventHandler.AsyncPartialResult -= AsyncPartialResult; }

        protected void AsyncPartialResult(object entries, GlobalEventArgs args)
        {
            if (args.BoolVal[0])
            {
                CurrentCallTime = DateTime.Now.Subtract(CurrentCodeStartTime).TotalMilliseconds;

                //DeregisterAsyncEvents(); 
            }

            //HandlePartialResult((List<SearchResultEntry>)entries, args.BoolVal[0]);
        }

        protected void ParallelQueriesCompleted(object sender, GlobalEventArgs args)
        {
            this.ForestStore.RunningParallel = false;

            
            HandlePartialResult(ForestStore.ParallelResults, true);

            ForestStore.ParallelResults = new List<SearchResultEntry> { };

            GlobalEventHandler.RaiseAsyncQureyIsCompleted();
        }

        protected void StartProgress()
        {
            if (!(HandleProgress))
            {
                if (this.pbLoad.InvokeRequired)
                {
                    MethodInvoker delcall = () => { StartProgress(); };

                    try
                    { this.BeginInvoke(delcall); }

                    catch (Exception ex)
                    { ex.ToDummy(); }
                }

                else
                {
                    HandleProgress = true;
                    this.pbLoad.Maximum = 100;
                    this.pbLoad.Value = 0;

                    this.pbLoad.Update();
                }

            }
        }

        protected void UpdateProgress(int stepval, int maxval = 100)
        {
            if (this.pbLoad.InvokeRequired)
            {
                MethodInvoker delcall = () => { UpdateProgress(stepval, maxval); };

                try
                { this.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                if (!(HandleProgress)) return;

                try
                {
                    this.pbLoad.Maximum = maxval;
                    this.pbLoad.Step = stepval;
                    this.pbLoad.PerformStep();
                }

                catch (Exception ex)
                { ex.ToDummy();}

                this.pbLoad.Update();
                this.Refresh();
            }
        }

        protected void EndProgress()
        {
            if (HandleProgress)
            {
                if (this.pbLoad.InvokeRequired)
                {
                    MethodInvoker delcall = () => { EndProgress(); };

                    try
                    { this.BeginInvoke(delcall); }

                    catch (Exception ex)
                    { ex.ToDummy(); }
                }

                else
                {
                    HandleProgress = false;

                    this.pbLoad.Value = 0;
                    this.pbLoad.Update();
                }
            }
        }

        protected void UDPPingEvent(object ms, GlobalEventArgs args)
        {
            ThreadRunTime += (long)ms;

            GlobalEventHandler.UDPPingProceeded -= UDPPingEvent;

            LogDebugString(String.Format("UDPPing: time elapsed {0} ({1}) ms", (long)ms, args.LongVal[0]));

            UpdateProgress(5);
        }

        protected void CurrentDomainEvent(object ms, GlobalEventArgs args)
        {
            ThreadRunTime += (long)ms;

            GlobalEventHandler.DiscoveredCurrentDomain -= CurrentDomainEvent;

            LogDebugString(String.Format("GetCurrentDomain: time elapsed {0} ({1}) ms", (long)ms, args.LongVal[0]));

            UpdateProgress(10);
        }

        protected void CurrentDomainNCEvent(object ms, GlobalEventArgs args)
        {
            ThreadRunTime += (long)ms;

            GlobalEventHandler.DiscoveredCurrentDomainNCs -= CurrentDomainNCEvent;

            LogDebugString(String.Format("GetCurrentDomain NCs: time elapsed {0} ({1}) ms", (long)ms, args.LongVal[0]));

            UpdateProgress(10);
        }

        protected void ForestEvent(object ms, GlobalEventArgs args)
        {
            ThreadRunTime += (long)ms;

            GlobalEventHandler.DiscoveredForest -= ForestEvent;

            LogDebugString(String.Format("GetCurrentForest: time elapsed {0} ({1}) ms", (long)ms, args.LongVal[0]));

            UpdateProgress(10);
        }

        protected void SchemaEvent(object ms, GlobalEventArgs args)
        {
            ThreadRunTime += (long)ms;

            GlobalEventHandler.DiscoveredSchema -= SchemaEvent;

            LogDebugString(String.Format("CacheCurrentSchema: time elapsed {0} ({1}) ms", (long)ms, args.LongVal[0]));
            LogDebugString(String.Format("  Classes count: {0}", ForestBase.ClassCache.Count()));
            LogDebugString(String.Format("  Attributes count: {0}", ForestBase.AttributeCache.Count()));
        }

        protected void DCCountEvent(object count, GlobalEventArgs args)
        {
            DCCount = (int)count;
            GlobalEventHandler.CountOfDCs -= DCCountEvent;
        }

        protected void DCDiscoveredEvent(object ms, GlobalEventArgs args)
        {
            ThreadRunTime += (long)ms;

            CurrentDCNumber += 1;

            if (DCCount == CurrentDCNumber)
            { GlobalEventHandler.DCDiscovered -= DCDiscoveredEvent; }

            UpdateProgress(5, (5 * DCCount) + PreDCProgress);
        }

        protected void UpdateFilter(object filterText, GlobalEventArgs args)
        {
            if (!args.BoolVal[0])
            { this.txtFilter.SetText(filterText.ToString()); }

            QBLoaded = !args.BoolVal[0];

            if (!(QBLoaded))
            {
                try
                { GlobalEventHandler.FilterSignaled -= UpdateFilter; }

                catch (Exception ex) { ex.ToDummy();}

                QBuilder = null;
            }
        }

        protected void ConnectionSignaled(object credInfo, GlobalEventArgs args)
        {
            bool success = false;

            GlobalEventHandler.ConnectionSignaled -= ConnectionSignaled;

            CBuilder.Close();

            CBuilderLoaded = false;

            if ((Credentials)credInfo == null)
            { return; }

            string forestname = ((Credentials)credInfo).ForestName;

            if (!((Credentials)credInfo).HasCreds)
            { credInfo = null; }

            if (credInfo == null)
            { success = true; }

            else
            { success = ((Credentials)credInfo).ValidateCreds(); }

            if (!success)
            {
                GlobalEventHandler.RaiseErrorOccured(String.Format("ERROR: {0}", ((Credentials)credInfo).ErrorMsg));

                this.txtOutput.ForeColor = Color.DarkRed;

                LogMessage(String.Format("ERROR: {0}", ((Credentials)credInfo).ErrorMsg), this.txtOutput);

                credInfo = null;

                return;
            }

            this.txtOutput.ForeColor = Color.Black;

            if (forestname.ToLowerInvariant() == ForestBase.ForestName.ToLowerInvariant())
            { if (credInfo == null) return; }
            
            LogDebugString(String.Format("{2}Loading forest '{0}' with user '{1}'{2}", forestname, (credInfo != null) ? ((Credentials)credInfo).UserName : WindowsIdentity.GetCurrent().Name, Environment.NewLine));

            StartRefreshForest(forestname, (Credentials)credInfo);
        }

        protected void CredentialsSignaled(object credInfo, GlobalEventArgs args)
        {            
            GlobalEventHandler.CredentialsSignaled -= CredentialsSignaled;

            AuthLoaded = false;
            
            if ((Credentials)credInfo != null)
            { StartThis(((Credentials)credInfo).ElevateThis, ((Credentials)credInfo).RestartThis, (Credentials)credInfo); }            
        }

        protected void ClipBoardChangedEvent(object hasText, GlobalEventArgs args)
        {
            this.Filter_Paste_ContextItem.Enabled = (bool)hasText;
            this.Attributes_Paste_ContextItem.Enabled = (bool)hasText;
            this.Base_Paste_ContextItem.Enabled = (bool)hasText;
        }

        protected void ErrorSignaled(object errorMsg, GlobalEventArgs args)
        { LogDebugString(errorMsg.ToString(), args.BoolVal[0]); }

        protected void RegisterDecodingProcessEvent()
        {
            GlobalEventHandler.DecodingProcess += DecodingProcessEvent;

            StartProgress();
        }

        protected void DeRegisterDecodingProcessEvent()
        { 
            GlobalEventHandler.DecodingProcess -= DecodingProcessEvent;
            
            EndProgress();
        }

        protected void DecodingProcessEvent(object current, GlobalEventArgs args)
        {
            ShowDecodingProgress((long)current, args.LongVal[0]);

            long div = 1;

            if (args.LongVal[0] > 30000)
            {
                div = (args.LongVal[0] / 3000);

                args.LongVal[0] = args.LongVal[0] / div;
            }

            int stepval = 0;

            long step = (long)(args.LongVal[0] / 10);

            if ((long)current < 2)
            { stepval = (int)(((long)current + step) / div); }

            else
            { stepval = 1; }

            int maxval = (int)(args.LongVal[0] + step);

            UpdateProgress(stepval, maxval);
        }

        protected void BasePathSelectedEvent(object path, GlobalEventArgs args)
        {
            this.txtSBase.SetText(path.ToString());

            GlobalEventHandler.GuiBasePathSelected -= BasePathSelectedEvent;
        }

        protected void BaseObjectSelectedEvent(object objectInfo, GlobalEventArgs args)
        {
            this.txtSBase.Tag = (ADObjectInfo)objectInfo;

            this.txtSBase.SetText(((ADObjectInfo)objectInfo).Path);

            if (!LdapTree.IsHidden)
            { GlobalEventHandler.GuiBaseObjectSelected -= BaseObjectSelectedEvent; }

            if (args.BoolVal[0])
            { ExecQuery(new string[] { "*" }); }            
        }

        protected void ATABuilderClosed(object name, GlobalEventArgs args)
        {
            if (name.ToString() != "")
            { return; }

            GlobalEventHandler.FormExited -= ATABuilderClosed;

            ATABuilderLoaded = false;
        }

        protected void DomainFailed(string domainName, string msg)
        {
            LogDebugString(String.Format("ERROR loading {0} ({1})", domainName, msg));

            LogMessage(String.Format("{2}\tERROR loading {0} ({1}){2}", domainName, msg, Environment.NewLine), this.txtOutput);
        }

        protected void RegisterFindEvents()
        {
            GlobalEventHandler.StartSearch += StartSearch;

            GlobalEventHandler.ContinueSearch += ContinueSearch;
        }

        protected void DeRegisterFindEvents()
        {
            GlobalEventHandler.StartSearch += StartSearch;

            GlobalEventHandler.ContinueSearch += ContinueSearch;
        }

        protected void StartSearch(object sender, GlobalEventArgs args)
        { this.gbFind.Visible = true; }

        protected void ContinueSearch(object sender, GlobalEventArgs args)
        { FindInResults(); }

        protected void SubsequentQuery(object ms, GlobalEventArgs args)
        { 
            CurrentRuns++;

            CurrentCallTime += (long)ms;
        }

        #endregion

        #region restart / runas

        protected void CallUser(bool elevate, bool restart)
        {
            if (!AuthLoaded)
            {
                GlobalEventHandler.CredentialsSignaled += CredentialsSignaled;

                (new AuthGUI(elevate, restart)).Show();

                AuthLoaded = true;
            }
        }

        protected void StartThis(bool elevate, bool restart = true, Credentials credInfo = null)
        {
            string errcap = "Failed to start";

            ProcessStartInfo pstart = new ProcessStartInfo();
           
            pstart.WorkingDirectory = Environment.CurrentDirectory;
            pstart.FileName = Application.ExecutablePath;

            pstart.UseShellExecute = true;

            if (credInfo != null)
            {
                pstart.UseShellExecute = false;

                pstart.Domain = credInfo.DomainName;
                pstart.UserName = credInfo.Sam;
                pstart.Password = credInfo.Pwd;

                if (elevate)
                { pstart.Arguments = "1"; }
            }            
            

            if ((!IsElevated && elevate) || (credInfo != null && elevate))
            {
                pstart.Verb = "runas";

                errcap = "Failed to elevate";
            }                

            try
            {
                Process myself = Process.Start(pstart);

                if (restart)
                    this.Close();                    
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errcap, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region restore

        protected void RestoreObject()
        {
            if (this.txtOutput.GetLines().Count() == 0)
            { LogMessage("Restore started:", this.txtOutput); }

            else
            { LogMessage(new List<string> { "Restore started:" }, this.txtOutput); }

            this.ForestStore.RestoreObject(this.cmbDCs.GetText(), (ADObjectInfo)this.txtSBase.Tag, QueryInfo);

            this.txtSBase.SetText(LastBase);
        }

        #endregion

        #region controls

        protected void HandleReferral(ReferralChasingOptions refOption, bool add)
        {
            if (refOption == ReferralChasingOptions.None)
            { 
                QueryInfo.RefOptions = ReferralChasingOptions.None;

                this.cbRefExternal.Checked = false;
                this.cbRefSubordinate.Checked = false;
            }

            else if (add)
            {
                if ((QueryInfo.RefOptions & refOption) != refOption)
                {  QueryInfo.RefOptions = QueryInfo.RefOptions | refOption; }
            }

            else
            {
                if ((QueryInfo.RefOptions & refOption) == refOption)
                { QueryInfo.RefOptions = QueryInfo.RefOptions ^ refOption; }
            }

            if (QueryInfo.RefOptions == ReferralChasingOptions.None)
            {
                if (!this.rbRefNone.Checked)
                { this.rbRefNone.Checked = true; }
            }

            else
            { this.rbRefNone.Checked = false; }
        }

        protected void Form_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            { return; }

            Control curcontrol = this.ActiveControl;

            if (curcontrol == null)
            { curcontrol = this.cmdSearch; }

            if (this.Size.Height < 390)
            { this.Size = new Size(this.Size.Width, 390); }

            if (this.Size.Width < 814)
            { this.Size = new Size(814, this.Size.Height); }

            curcontrol.Select();
        }

        protected void cmbDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(UpdatingForest)) UpdateDCList();
        }
        
        protected void cmbDCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UpdatingForest) return;

            UpdateNCList();

            ToolStripComboBox prov = (ToolStripComboBox)this.Query_Provider_Combo_MenuItem;

            prov.Items.Clear();

            HandlePorts(true);

            if (ForestBase.DCList[this.cmbDCs.GetText()].IsGC)
            {
                prov.Items.Add("GC");
                prov.Items.Add("LDAP");
                prov.SelectedIndex = 1;

                this.rbGC.Enabled = true;
            }

            else
            {
                prov.Items.Add("LDAP");
                prov.SelectedIndex = 0;

                this.rbGC.Enabled = false;
            }            
        }
        
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            CancelToken = false;
            
            CancelResults = false;

            Task.Factory.StartNew(() =>
            {

                if (QueryInfo.RootDse) // ((QueryInfo.RootDse) && (QueryInfo.QueryExtendedRootDSE))
                {
                    Task.Factory.StartNew(() => { ExecRootDSEQuery(); }, TaskCreationOptions.LongRunning);
                    //ExecRootDSEQuery(); 
                }

                else if (QueryInfo.PerformDirSync)
                {
                    //tbd
                }

                else
                {
                    Task.Factory.StartNew(() => { ExecQuery(); }, TaskCreationOptions.LongRunning);
                    //ExecQuery(); 
                }
            }, TaskCreationOptions.LongRunning);

            this.gbCancel.SetVisibility(true);
        }
        
        private void cmdCancelQuery_Click(object sender, EventArgs e)
        { CancelQuery(); }
        
        private void CmdCancelAll_Click(object sender, EventArgs e)
        { CancelQuery(true); }
       
        protected void rbDC_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripComboBox prov = (ToolStripComboBox)this.Query_Provider_Combo_MenuItem;

            if (this.rbDC.Checked)
            {
                prov.SelectedItem = prov.FindString("LDAP", 0);

                this.nuoPort.Value = ForestBase.CurrentPorts.LDAPPort;
            }
        }

        protected void rbGC_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripComboBox prov = (ToolStripComboBox)this.Query_Provider_Combo_MenuItem;

            if (this.rbGC.Checked)
            {
                prov.SelectedItem = prov.FindString("GC", 0);

                this.nuoPort.Value = ForestBase.CurrentPorts.GCPort;
            }
        }
        
        protected void rbCustomPort_CheckedChanged(object sender, EventArgs e)
        { this.nuoPort.Enabled = this.rbCustomPort.Checked; }

        protected void nuoPort_ValueChanged(object sender, EventArgs e)
        {
            this.QueryInfo.Port = (int)this.nuoPort.Value;
        }

        protected void rbRefNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbRefNone.Checked)
            { HandleReferral(ReferralChasingOptions.None, false); }
        }

        protected void cbRefSubordinate_CheckedChanged(object sender, EventArgs e)
        { HandleReferral(ReferralChasingOptions.Subordinate, this.cbRefSubordinate.Checked); }

        protected void cbRefExternal_CheckedChanged(object sender, EventArgs e)
        { HandleReferral(ReferralChasingOptions.External, this.cbRefExternal.Checked); }   

        protected void rbPagedQuery_CheckedChanged(object sender, EventArgs e)
        {
            QueryInfo.PerformPagedQuery = this.rbPagedQuery.Checked;

            SetQueryTypeMenu();
        }

        protected void cbASQ_CheckedChanged(object sender, EventArgs e)
        {
            QueryInfo.PerformASQ = this.cbASQ.Checked;

            Query_ASQ_MenuItem.Checked = this.cbASQ.Checked;

            CUSTOM_SCOPE temp = this.CurrentScope;

            this.rbBase.Checked = this.cbASQ.Checked;

            this.CurrentScope = temp;

            ExtAttribHandling();
        }
        
        protected void cbSort_CheckedChanged(object sender, EventArgs e)
        {
            QueryInfo.PerformSort = this.cbSort.Checked;

            Query_Sort_MenuItem.Checked = this.cbSort.Checked;

            ExtAttribHandling();
        }

        protected void cmbAttribs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.UpdatingAttributes)
            { this.txtAttributes.Text = ((AttributeSchema)this.cmbAttribs.SelectedItem).Name; }
        }

        protected void ExtAttribHandling()
        {
            this.AddASQAttribute = false;
            this.AddSortAttribute = false;

            if (!this.cbASQ.Checked)
            {
                this.cmbAttribs.Visible = false; 
                this.cmbAttribs.Enabled = false;
                this.labASQ.Visible = false;
                this.txtASQAttribs.Visible = false;
                this.txtASQAttribs.Enabled = false;

                switch (this.CurrentScope)
                {
                    case CUSTOM_SCOPE.BASE:
                        this.rbBase.Checked = true;
                        break;

                    case CUSTOM_SCOPE.ONELEVEL:
                        this.rbOneLevel.Checked = true;
                        break;


                    case CUSTOM_SCOPE.SUBTREE:
                        this.rbSubtree.Checked = true;
                        break;


                    case CUSTOM_SCOPE.PHANTOMROOT:
                        this.rbPhantomRoot.Checked = true;
                        break;

                    case CUSTOM_SCOPE.ROOTDSE:
                        this.rbRootDSE.Checked = true;
                        break;
                } 
            }

            if (!this.cbSort.Checked)
            {
                this.labSort.Visible = false;
                this.cbReverse.Visible = false;
                this.cbReverse.Enabled = false;

                this.txtSort.Visible = false;
                this.txtSort.Enabled = false;

                //this.lvSort.Visible = false;
                //this.lvSort.Enabled = false;
            }

            if ((!this.cbASQ.Checked) && (!this.cbSort.Checked))
            {
                this.txtAttributes.Size = new Size(this.txtAttributes.Width, 151);
                this.txtAttributes.Multiline = true;
                this.cmbAttribs.Visible = false;
                this.cmbAttribs.Enabled = false;
            }

            if ((this.cbASQ.Checked) && (!this.cbSort.Checked))
            {
                this.txtAttributes.Multiline = false;
                this.cmbAttribs.Visible = true;
                this.cmbAttribs.Enabled = true;
                cmbAttribs_SelectedIndexChanged(null, null);

                this.labASQ.Visible = true;

                this.txtASQAttribs.Size = new Size(this.txtASQAttribs.Width, 113);
                this.txtASQAttribs.Visible = true;
                this.txtASQAttribs.Enabled = true;               
            }

            else if ((!this.cbASQ.Checked) && (this.cbSort.Checked))
            {
                this.txtAttributes.Multiline = true;
                this.txtAttributes.Size = new Size(this.txtAttributes.Width, 112);

                this.labSort.Visible = true;
                //this.cbReverse.Visible = true;
                //this.cbReverse.Enabled = true;

                this.txtSort.Visible = false;
                this.txtSort.Enabled = false;

                this.txtSort.Visible = true;
                this.txtSort.Enabled = true;

                //this.lvSort.Visible = true;
                //this.lvSort.Enabled = true;
                //this.lvSort.SetColumnWidth(1, width: 141);
                //this.lvSort.SetColumnWidth(0, width: 37);
                
            }

            else if ((this.cbASQ.Checked) && (this.cbSort.Checked))
            {
                this.txtAttributes.Multiline = false;
                this.cmbAttribs.Visible = true;
                this.cmbAttribs.Enabled = true;
                cmbAttribs_SelectedIndexChanged(null, null);

                this.labASQ.Visible = true;

                this.txtASQAttribs.Size = new Size(this.txtASQAttribs.Width, 74);
                this.txtASQAttribs.Visible = true;
                this.txtASQAttribs.Enabled = true;

                this.labSort.Visible = true;
                //this.cbReverse.Visible = true;
                //this.cbReverse.Enabled = true;

                this.txtSort.Visible = true;
                this.txtSort.Enabled = true;

                //this.lvSort.Visible = true;
                //this.lvSort.Enabled = true;
                //this.lvSort.SetColumnWidth(1, width: 141);
                //this.lvSort.SetColumnWidth(0, width: 37);

                //foreach (ListViewItem item in this.lvSort.Items)
                //{
                //    if (item.Tag != null)
                //    { HandleSortAttributes(((SortInfo)item.Tag).AttributeName); }
                //}
            }
        }

        protected void rbDirSync_CheckedChanged(object sender, EventArgs e)
        {
            QueryInfo.PerformDirSync = this.rbDirSync.Checked;

            this.cmdLoadSync.Enabled = QueryInfo.PerformDirSync;
            this.cmdLoadSync.Visible = QueryInfo.PerformDirSync;

            SetQueryTypeMenu();
        }

        protected void cmdLoadSync_Click(object sender, EventArgs e)
        { LoadSyncRuns(); }

        protected void rbSubtree_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSubtree.Checked)
            {
                QueryScope = SearchScope.Subtree;

                SetQueryScopeMenu("SubTree");
            }
        }

        protected void rbOneLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOneLevel.Checked)
            {
                QueryScope = SearchScope.OneLevel;

                SetQueryScopeMenu("OneLevel");
            }
        }

        protected void rbBase_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBase.Checked)
            {
                QueryScope = SearchScope.Base;

                SetQueryScopeMenu("Base");
            }

            else
            { this.cbASQ.Checked = false; }
        }

        protected void rbDN_CheckedChanged(object sender, EventArgs e)
        { SetBaseTypeMenu("DN"); }

        protected void rbPhantomRoot_CheckedChanged(object sender, EventArgs e)
        {
            QueryInfo.PhantomRoot = this.rbPhantomRoot.Checked;

            if (this.rbPhantomRoot.Checked)
            {
                this.rbSubtree.Checked = true;

                SetBaseTypeMenu("PhantomRoot");

                this.gbScope.Enabled = false;
            }

            else
            { this.gbScope.Enabled = true; }
        }
         
        protected void rbRootDSE_CheckedChanged(object sender, EventArgs e)
        {
            QueryInfo.RootDse = this.rbRootDSE.Checked;

            if (QueryInfo.RootDse)
            {
                if (this.rbPagedQuery.Checked)
                { this.rbNonPagedQuery.Checked = true; }

                this.gbOptions.Enabled = false;

                if (!this.cbResults.Checked)
                { this.cbResults.Checked = true; }

                if (this.cbFullStats.Checked)
                { this.cbFullStats.Checked = false; }

                //this.rbDirSync.Enabled = !this.rbRootDSE.Checked;
                //this.cmdLoadSync.Visible = !this.rbRootDSE.Checked;

                this.rbBase.Checked = true;

                this.gbScope.Enabled = false;

                SetBaseTypeMenu("rootDSE");

                QueryInfo.QueryExtendedRootDSE = this.Query_Return_RootDSEExtended_MenuItem.Checked;                       
            }

            else
            {
                this.cbRootDseExt.Checked = false;

                this.gbOptions.Enabled = true;

                this.rbSubtree.Checked = true;

                this.gbScope.Enabled = true; 
            }
        }

        protected void cbRootDseExt_CheckedChanged(object sender, EventArgs e)
        { SetRootDseExtMenu(this.cbRootDseExt.Checked); }

        protected void cbResults_CheckedChanged(object sender, EventArgs e)
        {  this.Query_Return_Results_MenuItem.Checked = this.cbResults.Checked; }

        protected void cbFullStats_CheckedChanged(object sender, EventArgs e)
        { 
            this.Query_Return_Statistics_MenuItem.Checked = this.cbFullStats.Checked;

            QueryInfo.ExecStatsQuery = this.cbFullStats.Checked;
        }

        protected void cbAsync_CheckedChanged(object sender, EventArgs e)
        { 
            //MustQuery.RunAsync = this.cbAsync.Checked;

            //this.Query_Async_MenuItem.Checked = MustQuery.RunAsync;
        }
        
        protected void cmbNCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSBase.SetText(cmbNCs.GetText());
            this.txtSBase.Tag = null;
        }
        
        protected void txtSBase_TextChanged(object sender, EventArgs e)
        {
            this.cmdRestore.Visible = false;
            this.cmdRestore.Enabled = false;
            this.Base_Restore_ContextItem.Enabled = false;

            if (this.txtSBase.Tag != null)
            {
                ADObjectInfo oinfo = (ADObjectInfo)this.txtSBase.Tag;

                if (oinfo.IsDeleted && !oinfo.IsRecycled)
                {
                    this.cmdRestore.Visible = true;
                    this.cmdRestore.Enabled = true;
                    this.Base_Restore_ContextItem.Enabled = true;
                }
            }
        }
              
        protected void cmbHistory_Click(object sender, EventArgs e)
        {
            this.lvFilterHistory.Location = new Point(59, 44);
            this.lvFilterHistory.Size = new Size(this.txtFilter.Width, 196);

            //FilterHistory_Base_ContextItem_Click(null, null);
            //FilterHistory_Scope_ContextItem_Click(null, null);

            this.lvFilterHistory.Visible = true;
            //try { this.lvFilterHistory.Select(); }
            //catch { }
        }

        protected void lvFilterHistory_DoubleClick(object sender, EventArgs e)
        {
            PassFilterFromHistory();
        }

        protected void lvFilterHistory_Leave(object sender, EventArgs e)
        {
            this.lvFilterHistory.Visible = false;
        }
        
        protected void lvFilterHistory_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, FilterHistoryContextMenu); }
                  

        protected void cmdAdd_Click(object sender, EventArgs e)
        { AddAttribute(); }

        protected void lbAttribs_KeyDown(object sender, KeyEventArgs e)
        { GlobalControlHandler.KeySuppress(e); }

        protected void lbAttribs_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return) || (e.KeyCode == Keys.Enter))
            { AddAttribute(); }

            GlobalControlHandler.ListBoxKeyEventSearch(sender, e, ref this.ListTimer, ref this.ListQuery);
        }

        protected void lbAttribs_DoubleClick(object sender, EventArgs e)
        { AddAttribute(); }

        protected void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                e.SuppressKeyPress = true;
                cmdSearch_Click(null, null); 
            }

            else
            { e.SuppressKeyPress = false; }
        }

        protected void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            { e.SuppressKeyPress = true; }

            else
            { e.SuppressKeyPress = false;  }
        }

        protected void cmdPrevResult_Click(object sender, EventArgs e)
        { PrevResult(); }

        protected void cmdNextResult_Click(object sender, EventArgs e)
        { NextResult(); }
    
        protected void cmdFirstResult_Click(object sender, EventArgs e)
        { FirstResult(); }

        protected void cmdLastResult_Click(object sender, EventArgs e)
        { LastResult(); }
        
        protected void cmdClearResults_Click(object sender, EventArgs e)
        { ClearResultList(); }
        
        protected void cmdRestore_Click(object sender, EventArgs e)
        { RestoreObject(); }
       
        private void txtAttributes_MouseDown(object sender, MouseEventArgs e)
        { this.AddASQAttribute = false; this.AddSortAttribute = false; }

        private void txtASQAttribs_MouseDown(object sender, MouseEventArgs e)
        { this.AddASQAttribute = true; this.AddSortAttribute = false; }

        private void txtSort_MouseDown(object sender, MouseEventArgs e)
        { this.AddSortAttribute = true; this.AddASQAttribute = false; }

        private void cbReverse_CheckedChanged(object sender, EventArgs e)
        {
            object tag = this.txtSort.GetTag();

            if (tag != null)
            {
                ((SortInfo)tag).ReverseOrder = this.cbReverse.IsChecked();

                this.txtSort.SetTag(tag);
            }
        }
        
        protected void lvSort_MouseClick(object sender, MouseEventArgs e)
        { 
            this.AddSortAttribute = true; 
            this.AddASQAttribute = false;

            GlobalControlHandler.ListViewRightMouseClick(sender, e, AttributesSortContextMenu);
        }
        
        protected void lvSort_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag != null)
            { ((SortInfo)e.Item.Tag).ReverseOrder = e.Item.Checked; }

            else if (e.Item.Checked)
            { e.Item.Checked = false; }
        }
 
        private void gbFind_VisibleChanged(object sender, EventArgs e)
        {
            this.gbFind.Tag = 0;

            if (this.gbFind.Visible)
            { this.txtFind.Focus(); }

            HandleResultBoxSize();            
        }
                
        private void cmdFind_Click(object sender, EventArgs e)
        { FindInResults(); }

        private void cmdCloseFind_Click(object sender, EventArgs e)
        { this.gbFind.Visible = false; }
        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return) || (e.KeyCode == Keys.Enter))
            { FindInResults(); }
        }

        #endregion

        #region menu

        #region common handling

        protected void SetUserSettings(string settingName, object value)
        {
            MainBase.UserSettings.RegisterEvents();

            GlobalEventHandler.RaiseSettingChanged(settingName, value);

            MainBase.UserSettings.UnRegisterEvents();
        }

        protected void SetRootDseExtMenu(bool value)
        {
            if (this.cbRootDseExt.Checked != value)
            { this.cbRootDseExt.Checked = value; }

            if (this.Query_Return_RootDSEExtended_MenuItem.Checked != value)
            { this.Query_Return_RootDSEExtended_MenuItem.Checked = value; }

            if (!this.cbRootDseExt.Checked)
            { QueryInfo.QueryExtendedRootDSE = false; }

            else if (this.rbRootDSE.Checked)
            { QueryInfo.QueryExtendedRootDSE = this.cbRootDseExt.Checked; }

        }

        protected void SetQueryTypeMenu()
        {
            ToolStripComboBox prov = (ToolStripComboBox)this.Query_Type_Combo_MenuItem;

            string value = QueryInfo.PerformDirSync ? "Non-Paged" : "DirSync";

            if (QueryInfo.PerformPagedQuery)
            { value = "Paged"; }

            if (prov.SelectedItem.ToString() != value)
            { prov.SelectedItem = prov.FindString(value, 0); }
        }

        protected void SetBaseTypeMenu(string value)
        {
            ToolStripComboBox prov = (ToolStripComboBox)this.Query_BaseType_Combo_MenuItem;

            if (prov.SelectedItem.ToString() != value)
            { prov.SelectedItem = prov.FindString(value, 0); }

            switch (value)
            {
                case "PhantomRoot":
                    CurrentScope = CUSTOM_SCOPE.PHANTOMROOT;
                    break;

                case "rootDSE":
                    CurrentScope = CUSTOM_SCOPE.ROOTDSE;
                    break;

                default:
                    break;
            }
        }

        protected void SetQueryScopeMenu(string value)
        {
            ToolStripComboBox prov = (ToolStripComboBox)this.Query_Scope_Combo_MenuItem;

            if (prov.SelectedItem.ToString() != value)
            { prov.SelectedItem = prov.FindString(value, 0); }

            switch (value)
            {
                case "Base":
                    CurrentScope = CUSTOM_SCOPE.BASE;
                    break;

                case "OneLevel":
                    CurrentScope = CUSTOM_SCOPE.ONELEVEL;
                    break;

                case "SubTree":
                    CurrentScope = CUSTOM_SCOPE.SUBTREE;
                    break;

                default:
                    break;

            }
        }

        protected void SetQueryMenu()
        {
            this.Query_Async_MenuItem.Checked = QueryInfo.RunAsync;
            this.Query_ShowDeleted_MenuItem.Checked = QueryInfo.ShowDeleted;
            this.Query_ValRange_MenuItem.Checked = QueryInfo.DoValueRangeRetrieval;
        }

        protected void SetPortsMenu(object sender, KeyPressEventArgs e)
        {
            if (!e.IsNumeric())
            {
                e.Handled = true;

                if (e.IsBackSpace())
                { e.Handled = false; }

                if (e.IsEnter())
                {
                    ToolStripTextBox tb = (System.Windows.Forms.ToolStripTextBox)sender;

                    bool isLDAP = true;

                    Boolean.TryParse((string)tb.Tag, out isLDAP);

                    int port = 0;

                    if (!Int32.TryParse(tb.Text, out port))
                    { port = isLDAP ? 389 : 3268; }

                    switch (isLDAP)
                    {
                        case true:
                            ForestBase.CurrentPorts.LDAPPort = port;

                            Ports_SetLDAPValue_Text.Text = tb.Text;
                            Ports_LDAP_Value_Text.Text = tb.Text;

                            break;

                        default:
                            ForestBase.CurrentPorts.GCPort = port;

                            Port_SetGCValue_Text.Text = tb.Text;
                            Ports_GC_Value_Text.Text = tb.Text;

                            break;
                    }

                    HandlePorts(this.rbDC.Checked);
                }
            }
        }
        
        protected void SetPortsMenu(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                e.SuppressKeyPress = true;

                ToolStripTextBox tb = (System.Windows.Forms.ToolStripTextBox)sender;

                bool isLDAP = true;

                Boolean.TryParse((string)tb.Tag, out isLDAP);

                int port = 0;                    

                if (!Int32.TryParse(tb.Text, out port))
                { port = isLDAP ? 389 : 3268; }

                switch (isLDAP)
                {
                    case true:
                        ForestBase.CurrentPorts.LDAPPort = port;

                        Ports_SetLDAPValue_Text.Text = tb.Text;
                        Ports_LDAP_Value_Text.Text = tb.Text;

                        break;

                    default:
                        ForestBase.CurrentPorts.GCPort = port;

                        Port_SetGCValue_Text.Text = tb.Text;
                        Ports_GC_Value_Text.Text = tb.Text;

                        break;
                }

                HandlePorts(this.rbDC.Checked);
            }            
        }

        protected void SetTimeOut(object sender, KeyPressEventArgs e)
        {
            if (!e.IsNumeric())
            {
                e.Handled = true;

                if (e.IsBackSpace())
                { e.Handled = false; }

                if (e.IsEnter())
                {
                    ToolStripTextBox tb = (System.Windows.Forms.ToolStripTextBox)sender;

                    int tout = 120;

                    if (Int32.TryParse(tb.Text, out tout))
                    { ForestBase.CurrentTimeOut = tout; }
                }
            }
        }


        protected void SetTimeOut(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode <= Keys.D0) && (e.KeyCode >= Keys.D9))
            {
                e.SuppressKeyPress = true;

                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
                {
                    ToolStripTextBox tb = (System.Windows.Forms.ToolStripTextBox)sender;

                    int tout = 120;

                    if (Int32.TryParse(tb.Text, out tout))
                    { ForestBase.CurrentTimeOut = tout; }
                }
            }
        }

        protected void Font_Picker_MenuItem_Click(object sender, EventArgs e)
        { ChangeFont(); }
        
        protected void Combos_Copy_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyFromCombo(sender); }

        protected void Combos_CopyAll_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyComboBoxItems(sender); }
        
        #endregion

        #region File

        protected void File_Close_MenuItem_Click(object sender, EventArgs e)
        { this.Close(); }
        
        #endregion

        #region User

        protected void User_Elevate_MenuItem_Click(object sender, EventArgs e)
        { StartThis(true, true); }
        
        protected void User_Elevate_2nd_MenuItem_Click(object sender, EventArgs e)
        { StartThis(true, false); }

        protected void User_Restart_MenuItem_Click(object sender, EventArgs e)
        { CallUser(false, true); }

        protected void User_Restart_Elevate_MenuItem_Click(object sender, EventArgs e)
        { CallUser(true, true); }

        protected void User_Runas_MenuItem_Click(object sender, EventArgs e)
        { CallUser(false, false); }

        protected void User_Runas_Elevate_MenuItem_Click(object sender, EventArgs e)
        { CallUser(true, false); }
        
        private void User_Whoami_MenuItem_Click(object sender, EventArgs e)
        { CallWhoAmi(); }

        #endregion
        
        #region Connection

        protected void Connection_NewForest_MenuItem_Click(object sender, EventArgs e)
        { LoadConnectionWizard(); }

        protected void Connection_PassCredentials_MenuItem_Click(object sender, EventArgs e)
        { LoadConnectionWizard(true); }
        
        protected void Connection_Refresh_MenuItem_Click(object sender, EventArgs e)
        { StartRefreshForest(ForestBase.ForestName, ForestBase.GivenCreds); }
       
        protected void Ports_LDAP_Value_Text_KeyPress(object sender, KeyPressEventArgs e)
        { SetPortsMenu(sender, e); }

        protected void Ports_GC_Value_Text_KeyPress(object sender, KeyPressEventArgs e)
        { SetPortsMenu(sender, e); }

        protected void Ports_SetLDAPValue_Text_KeyPress(object sender, KeyPressEventArgs e)
        { SetPortsMenu(sender, e); }

        protected void Port_SetGCValue_Text_KeyPress(object sender, KeyPressEventArgs e)
        { SetPortsMenu(sender, e); }
        
        protected void Connection_TimeOutValue_MenuItem_KeyPress(object sender, KeyPressEventArgs e)
        { SetTimeOut(sender, e); }

        #endregion
        
        #region Query

        protected void Query_Execute_MenuItem_Click(object sender, EventArgs e)
        { cmdSearch_Click(null, null); }

        protected void Query_Provider_Combo_MenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox prov = (ToolStripComboBox)sender;

            switch (prov.SelectedItem.ToString())
            {
                case "LDAP":
                    this.rbDC.Checked = true;
                    break;

                case "GC":
                    this.rbGC.Checked = true;
                    break;
            }
        }

        protected void Query_Scope_Combo_MenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox prov = (ToolStripComboBox)sender;

            switch (prov.SelectedItem.ToString())
            {
                case "Base":
                    this.rbBase.Checked = true;
                    break;

                case "OneLevel":
                    this.rbOneLevel.Checked = true;
                    break;

                case "SubTree":
                    this.rbSubtree.Checked = true;
                    break;

                default:
                    this.rbSubtree.Checked = true;
                    break;
            }
        }

        protected void Query_BaseType_Combo_MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripComboBox prov = (ToolStripComboBox)sender;

            switch (prov.SelectedItem.ToString())
            {
                case "PhantomRoot":
                    this.rbPhantomRoot.Checked = true;
                    break;

                case "rootDSE":
                    this.rbRootDSE.Checked = true;
                    break;

                default:
                    this.rbDN.Checked = true;

                    break;
            }
        }

        protected void Query_Type_Combo_MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripComboBox prov = (ToolStripComboBox)sender;

            switch (prov.SelectedItem.ToString())
            {
                case "Paged":
                    this.rbPagedQuery.Checked = true;

                    break;

                case "DirSync":
                    this.rbDirSync.Checked = true;
                    break;

                default:
                    this.rbNonPagedQuery.Checked = true;

                    break;
            }
        }

        protected void Query_Return_Results_MenuItem_Click(object sender, EventArgs e)
        { this.cbResults.Checked = this.Query_Return_Results_MenuItem.Checked; }

        protected void Query_Return_Statistics_MenuItem_Click(object sender, EventArgs e)
        { this.cbFullStats.Checked = this.Query_Return_Statistics_MenuItem.Checked; }
               
        protected void Query_Return_RootDSEExtended_MenuItem_Click(object sender, EventArgs e)
        { SetRootDseExtMenu(this.Query_Return_RootDSEExtended_MenuItem.Checked); }

        protected void Query_Browse_MenuItem_Click(object sender, EventArgs e)
        { CallBrowser(LDAPBrowser.CALLING_CONTROL.GUI_BASE); }
  
        protected void Query_Async_MenuItem_Click(object sender, EventArgs e)
        { 
            //MustQuery.RunAsync = this.Query_Async_MenuItem.Checked;

            //cbAsync.Checked = MustQuery.RunAsync;
            QueryInfo.RunAsync = true;
            SetQueryMenu();
        }
        
        protected void Query_AutoPage_MenuItem_CheckChanged(object sender, EventArgs e)
        { 
            QueryInfo.AutoPage = this.Query_AutoPage_MenuItem.Checked;

            this.rbPagedQuery.ForeColor = QueryInfo.AutoPage ? Color.DarkGreen : SystemColors.ControlText;
            this.rbPagedQuery.SetText(QueryInfo.AutoPage ? "Paged (auto)" : "Paged");

            SetUserSettings("AutoPage", QueryInfo.AutoPage);
        }

        protected void Query_ValRange_MenuItem_CheckChanged(object sender, EventArgs e)
        { 
            QueryInfo.DoValueRangeRetrieval = this.Query_ValRange_MenuItem.Checked;

            SetUserSettings("DoValueRangeRetrieval", QueryInfo.DoValueRangeRetrieval);
        }

        protected void Query_ASQ_MenuItem_Click(object sender, EventArgs e)
        { this.cbASQ.Checked = true; }

        protected void Query_Sort_MenuItem_Click(object sender, EventArgs e)
        { this.cbSort.Checked = true; }

        protected void Query_ShowDeleted_MenuItem_CheckedChanged(object sender, EventArgs e)
        { QueryInfo.ShowDeleted = this.Query_ShowDeleted_MenuItem.Checked; }

        protected void Query_ShowRecycled_MenuItem_CheckedChanged(object sender, EventArgs e)
        { QueryInfo.ShowRecycled = this.Query_ShowRecycled_MenuItem.Checked; }

        protected void Query_Sort_Results_Asc_MenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Query_Sort_Results_Asc_MenuItem.Checked)
            { this.Query_Sort_Results_Desc_MenuItem.Checked = false; }
            
            QueryInfo.SortResults = this.Query_Sort_Results_Asc_MenuItem.Checked;

            QueryInfo.SortAscending = true;

            SetUserSettings("SortAscending", this.Query_Sort_Results_Asc_MenuItem.Checked);
        }

        protected void Query_Sort_Results_Desc_MenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Query_Sort_Results_Desc_MenuItem.Checked)
            { this.Query_Sort_Results_Asc_MenuItem.Checked = false; }

            QueryInfo.SortResults = this.Query_Sort_Results_Desc_MenuItem.Checked;

            QueryInfo.SortAscending = false;

            SetUserSettings("SortDescending", this.Query_Sort_Results_Desc_MenuItem.Checked);
        }

        #endregion
        
        #region Filter

        protected void FilterContextMenu_Opening(object sender, CancelEventArgs e)
        { ClipBoardChangedEvent(Clipboard.ContainsText(), null); }

        protected void Filter_Wizard_MenuItem_Click(object sender, EventArgs e)
        { LoadFilterWizard(); }
        
        protected void Filter_Clear_MenuItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.ClearTextBox(this.txtFilter); }

        protected void Filter_ClearHistory_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilterHistory(delete: true); }

        protected void FilterHistory_DeleteCurrent_ContextItem_Click(object sender, EventArgs e)
        { UpdateFilterHistory(remove: true); }

        protected void FilterHistory_DeleteSelected_ContextItem_Click(object sender, EventArgs e)
        { UpdateFilterHistory(removeSelected: true); }

        private void FilterHistory_ASQ_ContextItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistoryASQ, this.FilterHistory_ASQ_ContextItem.Checked);

            if (this.FilterHistory_ASQ_ContextItem.Checked)
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryASQ, ColumnHeaderAutoResizeStyle.ColumnContent); }

            else
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryASQ, width: 1); }
        }

        private void FilterHistory_Sort_ContextItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistorySorting, this.FilterHistory_Sort_ContextItem.Checked);

            if (this.FilterHistory_Sort_ContextItem.Checked)
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistorySorting, ColumnHeaderAutoResizeStyle.ColumnContent); }

            else
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistorySorting, width: 1); }
        }

        protected void FilterHistory_DC_ContextItem_CheckedChanged(object sender, EventArgs e)
        {

            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistoryDC, this.FilterHistory_DC_ContextItem.Checked);

            if (this.FilterHistory_DC_ContextItem.Checked)
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryDC, ColumnHeaderAutoResizeStyle.ColumnContent); }

            else
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryDC, width: 1); }
        }

        protected void FilterHistory_Base_ContextItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistoryBase, this.FilterHistory_Base_ContextItem.Checked);

            if (this.FilterHistory_Base_ContextItem.Checked)
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryBase, ColumnHeaderAutoResizeStyle.ColumnContent); }

            else
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryBase, width: 1); }
        }

        protected void FilterHistory_Scope_ContextItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistoryScope, this.FilterHistory_Scope_ContextItem.Checked);

            if (this.FilterHistory_Scope_ContextItem.Checked)
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryScope, ColumnHeaderAutoResizeStyle.ColumnContent); }

            else
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryScope, width: 1); }
        }

        protected void FilterHistory_Port_ContextItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistoryPort, this.FilterHistory_Port_ContextItem.Checked);

            if (this.FilterHistory_Port_ContextItem.Checked)
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryPort, ColumnHeaderAutoResizeStyle.ColumnContent); }

            else
            { this.lvFilterHistory.SetColumnWidth((int)HISTORY_COLUMNS.HistoryPort, width: 1); }
        }
        
        private void FilterHistory_All_ContextItem_CheckedChanged(object sender, EventArgs e)
        {
            bool check = this.FilterHistory_All_ContextItem.Checked;

            UpdateFilterHistoryTag(HISTORY_COLUMNS.HistoryAll, check);

            FilterHistory_ASQ_ContextItem.Checked = check;

            FilterHistory_Port_ContextItem.Checked = check;

            FilterHistory_Scope_ContextItem.Checked = check;

            FilterHistory_Base_ContextItem.Checked = check;

            FilterHistory_DC_ContextItem.Checked = check;

            FilterHistory_Sort_ContextItem.Checked = check;
        }
       
        protected async void FilterHistory_Bulk_ContextItem_Click(object sender, EventArgs e)
        {
            GlobalEventHandler.DecodingCompleted += DecodingCompleted;

            foreach (ListViewItem li in this.lvFilterHistory.GetItems())
            {
                if (li.Checked)
                {
                    li.Selected = true;

                    PassFilterFromHistory(li.Index);

                    NextBulk = false;

                    await Task.Run(() => { Thread.Sleep(100); });
                    
                    cmdSearch_Click(null, null);

                    await Task.Run(() =>
                    {
                        while (!NextBulk)
                        { Thread.Sleep(100); }
                    });
                }
            }

            GlobalEventHandler.DecodingCompleted -= DecodingCompleted;
        }

        protected void Filter_Copy_ContextItem_Click(object sender, EventArgs e)
        {
            this.txtFilter.SelectAll();
            this.txtFilter.Copy();
            this.txtFilter.SelectionLength = 0;
        } 

        protected void Filter_Paste_ContextItem_Click(object sender, EventArgs e)
        { this.txtFilter.Paste(); }

        protected void FilterHistory_Copy_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyFromCombo(sender); }

        protected void FilterHistory_CopyAll_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyComboBoxItems(sender); }

        

        #endregion
        
        #region Attributes

        

        protected void AttributesContextMenu_Opening(object sender, CancelEventArgs e)
        { ClipBoardChangedEvent(Clipboard.ContainsText(), null); }

        protected void Attributes_Load_MenuItem_Click(object sender, EventArgs e)
        { LoadAttribsFromSchema();}

        protected void Attributes_Hide_MenuItem_Click(object sender, EventArgs e)
        { this.gbProps.Visible = false; }

        protected void Attributes_Copy_ContextItem_Click(object sender, EventArgs e)
        {
            this.txtAttributes.SelectAll();
            this.txtAttributes.Copy();
            this.txtAttributes.SelectionLength = 0;
        }

        protected void Attributes_Paste_ContextItem_Click(object sender, EventArgs e)
        { this.txtAttributes.Paste(); }
 
        protected void Attributes_Clear_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.ClearTextBox(this.txtAttributes); }

        protected void AttributesList_CopyAll_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyListBoxItems(sender); }

        protected void AttributesList_Copy_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyFromListBox(sender); }
    
        protected void AttributesList_Add_ContextItem_Click(object sender, EventArgs e)
        { AddAttribute(); }
 
        protected void AttributesSort_Load_ContextMenu_Click(object sender, EventArgs e)
        { LoadAttribsFromSchema(); }

        protected void AttributesSort_Clear_ContextMenu_Click(object sender, EventArgs e)
        { this.lvSort.ResetSelectedItem(); }

        protected void AttributesSort_Remove_ContextMenu_Click(object sender, EventArgs e)
        { this.lvSort.RemoveSelectedItem(); }

        #endregion
        
        #region Decode

        protected void Decode_SD_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodeSD", this.Decode_SD_MenuItem.Checked); }

        protected void Decode_ResolveSids_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("ResolveSids", this.Decode_ResolveSids_MenuItem.Checked); }

        protected void Decode_GUIDs_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodeGUID", this.Decode_GUIDs_MenuItem.Checked); }

        protected void Decode_SIDs_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodeSID", this.Decode_SIDs_MenuItem.Checked); }

        protected void Decode_PGID_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodePrimaryGroupID", this.Decode_PGID_MenuItem.Checked); }

        protected void Decode_OctetString_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodeOctetStrings", this.Decode_OctetString_MenuItem.Checked); }

        protected void Decode_ReplicaLinks_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodeReplicaLinks", this.Decode_ReplicaLinks_MenuItem.Checked); }

        protected void Decode_UserParams_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("DecodeUserParameters", this.Decode_UserParams_MenuItem.Checked); }
        
        protected void Decode_TimeBias_MenuItem_CheckChanged(object sender, EventArgs e)
        { SetUserSettings("UseLocalTime", this.Decode_TimeBias_MenuItem.Checked); }
        
        protected void Decode_IgnoreEmpty_MenuItem_Click(object sender, EventArgs e)
        {
            SetUserSettings("IgnoreEmpty", this.Decode_IgnoreEmpty_MenuItem.Checked);
        }
        #endregion

        #region Results

        protected void Results_Clear_MenuItem_Click(object sender, EventArgs e)
        { ClearResult();}

        protected void Results_CopyAll_MenuItem_Click(object sender, EventArgs e)
        {
            this.txtOutput.SelectAll();
            this.txtOutput.Copy();
            this.txtOutput.SelectionLength = 0;
        }
       
        protected void Results_CopyAll_ContextItem_Click(object sender, EventArgs e)
        {
            this.txtOutput.SelectAll();
            this.txtOutput.Copy();
            this.txtOutput.SelectionLength = 0;
        }

        protected void Results_CopySelected_ContextItem_Click(object sender, EventArgs e)
        { this.txtOutput.Copy(); }

        protected void Results_CopySelected_MenuItem_Click(object sender, EventArgs e)
        { this.txtOutput.Copy(); }

        protected void Result_Search_String_MenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            { GlobalControlHandler.FindStringInTextBox(this.txtOutput, this.Result_Search_String_MenuItem.Text, 0); }
        }

        protected void Result_Search_MenuItem_Click(object sender, EventArgs e)
        { this.Result_Search_String_MenuItem.Focus(); }

        protected void Result_Search_FindNext_MenuItem_Click(object sender, EventArgs e)
        {
            int start = 0;
            try
            { start = this.txtOutput.SelectionStart + 1; }

            catch (Exception ex)
            { start = 0; ex.ToDummy(); }

            GlobalControlHandler.FindStringInTextBox(this.txtOutput, this.Result_Search_String_MenuItem.Text, start); 
        }
                
        protected void Result_Search_Find_MenuItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.FindStringInTextBox(this.txtOutput, this.Result_Search_String_MenuItem.Text, 0); }

        protected void Results_MaxResults_MenuItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.IsEnter())
            {
                e.Handled = true;

                int mcnt;

                if (!Int32.TryParse(this.Results_MaxResults_MenuItem.Text, out mcnt))
                {
                    mcnt = 20;
                    this.Results_MaxResults_MenuItem.Text = "20";
                }

                SetUserSettings("MaxResultListCount", mcnt);
            }

            else if (!e.IsNumeric())
            { e.Handled = true; }
        }
        
        protected void Results_MaxResultsCount_MenuItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.IsEnter())
            {
                e.Handled = true;

                long mcnt = 1000;

                if (this.Results_MaxResultsCount_MenuItem.Text == "*")
                { mcnt = long.MaxValue; }

                else
                { long.TryParse(this.Results_MaxResultsCount_MenuItem.Text, out mcnt); }

                this.Results_MaxResultsCount_MenuItem.Text = mcnt.ToString();

                SetUserSettings("MaxResultListCount", mcnt);
            }

            else if (!e.IsNumeric())
            {
                e.Handled = true;

                if (e.IsBackSpace())
                { e.Handled = false; }

                else if (e.EqualsChar('*') && (this.Results_MaxResultsCount_MenuItem.Text.Length == 0))
                { e.Handled = false; }
            }

            else
            {
                if (this.Results_MaxResultsCount_MenuItem.Text == "*")
                { this.Results_MaxResultsCount_MenuItem.Text = String.Empty; }
            }
        }
        
        protected void Result_Remembered_First_ContextItem_Click(object sender, EventArgs e)
        { FirstResult(); }

        protected void Result_Remembered_Previous_ContextItem_Click(object sender, EventArgs e)
        { PrevResult(); }

        protected void Result_Remembered_Next_ContextItem_Click(object sender, EventArgs e)
        { NextResult(); }

        protected void Result_Remembered_Last_ContextItem_Click(object sender, EventArgs e)
        { LastResult(); }

        protected void Result_Remembered_Clear_ContextItem_Click(object sender, EventArgs e)
        { ClearResultList(); }

        protected void Results_Remembered_First_MenuItem_Click(object sender, EventArgs e)
        { FirstResult(); }

        protected void Results_Remembered_Prev_MenuItem_Click(object sender, EventArgs e)
        { PrevResult(); }

        protected void Results_Remembered_Next_MenuItem_Click(object sender, EventArgs e)
        { NextResult(); }

        protected void Results_Remembered_Last_MenuItem_Click(object sender, EventArgs e)
        { LastResult(); }

        protected void Results_Remembered_Clear_MenuItem_Click(object sender, EventArgs e)
        { ClearResultList(); }
        
        protected void Results_ShowPartial_MenuItem_CheckedChanged(object sender, EventArgs e)
        { SetUserSettings("ShowPartialResults", this.Results_ShowPartial_MenuItem.Checked); }

        #endregion
        
        #region Tools

        protected void Tools_Builder_MenuItem_Click(object sender, EventArgs e)
        { LoadAttributeTypeAssociator(); }

        #endregion
       
        #region Debug context menu
        protected void Debug_Copy_MenuItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyListBoxItems(sender); }

        protected void Debug_Clear_MenuItem_Click(object sender, EventArgs e)
        { this.lbDebugLog.Items.Clear(); }

        #endregion

        #region Base context menu

        protected void BaseContextMenu_Opening(object sender, CancelEventArgs e)
        { ClipBoardChangedEvent(Clipboard.ContainsText(), null); }

        protected void Base_Browse_ContextItem_Click(object sender, EventArgs e)
        { CallBrowser(LDAPBrowser.CALLING_CONTROL.GUI_BASE); }

        protected void Base_Copy_ContextItem_Click(object sender, EventArgs e)
        {
            this.txtSBase.SelectAll();
            this.txtSBase.Copy();
            this.txtSBase.SelectionLength = 0;
        }

        protected void Base_Paste_ContextItem_Click(object sender, EventArgs e)
        { this.txtSBase.Paste(); }

        protected void Base_Restore_ContextItem_Click(object sender, EventArgs e)
        { RestoreObject(); }

        #endregion  

        #region DC combo

        protected void DCCombo_Info_ContextItem_Click(object sender, EventArgs e)
        {
            DomainControllerHelper dc = (DomainControllerHelper)this.cmbDCs.SelectedItem;

            LogMessage(dc.Print(), this.txtOutput);

            //if (ShowInfoLoaded)
            //{ ShowInfo.Close(); }

            //ShowInfo = new InfoGui();

            //ShowInfo.LoadInfo("DC Info: " + dc.Name, dc.Print(), this.Location);

            //ShowInfoLoaded = true;
        }

        #endregion

        private void label2_Click(object sender, EventArgs e)
        {

        }





        #endregion   
    }
}
