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
using System.Linq;
using System.Windows.Forms;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public partial class LDAPBrowser : Form
    {
        #region enums

        public enum CALLING_CONTROL
        {
            GUI_BASE,
            GUI_ASQ,
            WIZARD
        }

        public enum OBJECT_TYPES
        {
            NC = 0x0,
            USER = 0x1,
            INETORGPERSON = USER,
            CONTACT = USER,
            CONTAINER = 0x2,
            ORGANIZATIONALUNIT,
            GROUP,
            COMPUTER,
            FOREIGNSECURITYPRINCIPAL,
            DEFAULT = CONTAINER
        }

        #endregion

        #region fields

        public CALLING_CONTROL Caller = CALLING_CONTROL.GUI_BASE;

        private QueryControl QueryCtrl = new QueryControl() { AutoPage = true, CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_BROWSER };

        public ForestInfo ThisForestInfo;
        public string DC;

        //private string BrowsingBase;

        //private bool Expanding = false;
        bool Collapsing = false;
        public bool IsHidden = false;

        #endregion

        #region constructors

        public LDAPBrowser(CALLING_CONTROL whoCalled, QueryControl mustQuery, string entryPoint, string dcName, ForestInfo fiH)
        { 
            InitializeComponent();

            LoadInternal(whoCalled, mustQuery, entryPoint, dcName, fiH);
        }

        private void Browser_Load(object sender, EventArgs e)
        { }

        #endregion

        #region custom

        public void ShowTree(CALLING_CONTROL whoCalled, QueryControl mustQuery, string entryPoint = null, string dcName = null, ForestInfo fiH = null)
        {
            bool mustreload = false;

            Caller = whoCalled;

            if (entryPoint != null)
            { mustreload = FindNode(entryPoint); }

            if (QueryCtrl.ShowDeleted != mustQuery.ShowDeleted)
            { mustreload = true; }
            QueryCtrl.ShowDeleted = mustQuery.ShowDeleted;

            if (QueryCtrl.ShowRecycled != mustQuery.ShowRecycled)
            { mustreload = true; }
            QueryCtrl.ShowRecycled = mustQuery.ShowRecycled;

            if (QueryCtrl.Port != mustQuery.Port)
            { mustreload = true; }
            QueryCtrl.Port = mustQuery.Port;

            if (dcName != null)
            { DC = dcName; }

            if (fiH != null)
            { ThisForestInfo = fiH; }

            TreeNode parent = null;

            if (mustreload)
            {
                ADObjectInfo oinfo = new ADObjectInfo() { Name = entryPoint, Path = entryPoint, ObjectClass = "nc" };
                mustreload = AddEntry(ref parent, oinfo);
            }

            this.Show();

            if (mustreload)
            { ExpandNode(parent); }

            this.tvLdap.Sort();
        }

        private void LoadInternal(CALLING_CONTROL whoCalled, QueryControl mustQuery, string entryPoint, string dcName, ForestInfo fiH)
        { ShowTree(whoCalled, mustQuery, entryPoint, dcName, fiH); }

        private void WalkSubs(TreeNode parentNode)
        {
            GlobalControlHandler.UpdateLabel(this.lbProcess, "walking children");
            
            this.Cursor = Cursors.WaitCursor;

            bool isbad;
            DirectoryAttribute dat;

            string[] attribs = new string[] { "ou", "cn", "distinguishedName", "objectClass", "objectGUID", "lastKnownParent", "msDS-LastKnownRDN", "isDeleted", "isRecycled" };

            List<SearchResultEntry> result = ThisForestInfo.Query(DC,
                                                                  parentNode.Name,
                                                                  "(objectClass=*)",
                                                                  attribs,
                                                                  SearchScope.OneLevel,
                                                                  ReferralChasingOptions.None,
                                                                  QueryCtrl,
                                                                  returnResults:true);                                   

            if (ThisForestInfo.HasError)
            { GlobalControlHandler.UpdateLabel(this.lbProcess, "Failed: walking children: " + ThisForestInfo.ErrorMSG); }

            else
            {
                GlobalControlHandler.UpdateLabel(this.lbProcess, "Found " + result.Count);


                long cnt = 0;
                long step = 100;

                bool storedecodeguid = MainBase.UserSettings.DecodeGUID;

                MainBase.UserSettings.DecodeGUID = true;

                foreach (SearchResultEntry res in result)
                {
                    ADObjectInfo oinfo = new ADObjectInfo();

                    cnt++;

                    if (cnt == step)
                    {
                        GlobalControlHandler.UpdateLabel(this.lbProcess, String.Format("Found {0} (walked {1})", result.Count, cnt));

                        step += 100;
                    }

                    string nodename = String.Empty;
                    string nodepath = String.Empty;
                    string objectclass = OBJECT_TYPES.DEFAULT.ToString();
                    int successcount = 0;

                    if (res.Attributes.Contains("ou"))
                    {
                        dat = res.Attributes["ou"];

                        oinfo.Name = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.CaseIgnoreString, out isbad, true)[0];

                        successcount = 1;
                    }

                    else if (res.Attributes.Contains("cn"))
                    {
                        dat = res.Attributes["cn"];

                        oinfo.Name = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.CaseIgnoreString, out isbad, true)[0];

                        successcount = 1;
                    }

                    if (res.Attributes.Contains("distinguishedName"))
                    {
                        dat = res.Attributes["distinguishedName"];

                        oinfo.Path = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.DN, out isbad, true)[0];

                        if (nodename.Length == 0)
                        { nodename = nodepath; }

                        successcount++;
                    }

                    if (res.Attributes.Contains("objectClass"))
                    {
                        dat = res.Attributes["objectClass"];

                        List<string> ocl = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.DirectoryString, out isbad, true);

                        oinfo.ObjectClass = ocl[ocl.Count - 1];
                    }

                    if (res.Attributes.Contains("objectGuid"))
                    {
                        dat = res.Attributes["objectGuid"];

                        List<string> guidl = ThisForestInfo.DecodeByteData(dat, ActiveDirectorySyntax.OctetString, out isbad, false, true);

                        oinfo.ObjectGuid = guidl[0];
                    }

                    if (res.Attributes.Contains("lastKnownParent"))
                    {
                        dat = res.Attributes["lastKnownParent"];

                        oinfo.LastKnownParent = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.DN, out isbad, true)[0];
                    }

                    if (res.Attributes.Contains("msDS-LastKnownRDN"))
                    {
                        dat = res.Attributes["msDS-LastKnownRDN"];

                        oinfo.LastKnownRDN = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.DirectoryString, out isbad, true)[0];
                    }

                    if (res.Attributes.Contains("isDeleted"))
                    {
                        dat = res.Attributes["isDeleted"];

                        string temp = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.Bool, out isbad, true)[0];

                        bool btmp = false;

                        Boolean.TryParse(temp, out btmp);

                        oinfo.IsDeleted = btmp;
                    }

                    if (res.Attributes.Contains("isRecycled"))
                    {
                        dat = res.Attributes["isRecycled"];

                        string temp = ThisForestInfo.DecodeStringData(dat, ActiveDirectorySyntax.Bool, out isbad, true)[0];

                        bool btmp = false;

                        Boolean.TryParse(temp, out btmp);

                        oinfo.IsRecycled = btmp;
                    }

                    if (successcount == 2)
                    { AddEntry(ref parentNode, oinfo); }
                }

                MainBase.UserSettings.DecodeGUID = storedecodeguid;

                GlobalControlHandler.UpdateLabel(this.lbProcess, visible: false);
            }

            this.Cursor = Cursors.Default;
        }

        private bool AddEntry(ref TreeNode parentNode, ADObjectInfo objectInfo)
        {
            bool ret = true;

            TreeNode child = new TreeNode();
            
            child.Name = objectInfo.Path;
            child.Text = objectInfo.Name;
            child.ImageIndex = (int)GetImageIndex(objectInfo.ObjectClass);
            child.SelectedImageIndex = child.ImageIndex;
            child.Tag = objectInfo;

            if (parentNode == null)
            {
                if (this.tvLdap.Nodes.ContainsKey(objectInfo.Path))
                { ret = false; }

                else
                {
                    this.tvLdap.Nodes.Add(child);

                    parentNode = child;
                }
            }

            else
            { 
                parentNode.Nodes.Add(child);
                
                ((ADObjectInfo)parentNode.Tag).Tag = true;
            }

            return ret;
        }

        private OBJECT_TYPES GetImageIndex(string objectClass)
        {
            OBJECT_TYPES ret = OBJECT_TYPES.DEFAULT;

            object temp = OBJECT_TYPES.DEFAULT;

            if (objectClass.EnumTryParse(typeof(OBJECT_TYPES), out temp))
            { ret = (OBJECT_TYPES)temp; }
            
            return ret;
        }

        private void ExpandNode(TreeNode parentNode)
        {
            if (Collapsing)
            {
                Collapsing = false;

                return;
            }

            if (!((ADObjectInfo)parentNode.Tag).Tag)
            { WalkSubs(parentNode); }

            List<TreeNode> nodes = new List<TreeNode> { };

            GetParents(parentNode, ref nodes);

            for (int cnt = nodes.Count - 1; cnt > -1; cnt--)
            {
                nodes[cnt].Expand();
            }

            try
            {
                parentNode.Expand();

                parentNode.EnsureVisible();
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        private void GetParents(TreeNode node, ref List<TreeNode> nodes)
        {
            if (node.Parent != null)
            {
                if (!node.Parent.IsExpanded)
                { nodes.Add(node.Parent); }

                GetParents(node.Parent, ref nodes);
            }
        }

        private void SendPath(bool selectPath = true, bool queryGui = false)
        {
            IsHidden = false;

            string path = this.tvLdap.SelectedNode.Name;

            switch (Caller)
            {
                case CALLING_CONTROL.GUI_BASE:
                    ADObjectInfo objectinfo = (ADObjectInfo)this.tvLdap.SelectedNode.Tag;

                    GlobalEventHandler.RaiseGuiBaseObjectSelected(objectinfo, queryGui);        

                    break;

                case CALLING_CONTROL.GUI_ASQ:
                    GlobalEventHandler.RaiseGuiAsqPathSelected(path);

                    break;

                case CALLING_CONTROL.WIZARD:
                    GlobalEventHandler.RaiseWizardPathSelected(path);

                    break;

                default:
                    break;
            }

            if (selectPath)
            {
                IsHidden = true;
                this.Hide(); 
            }
        }

        private void Recall()
        {
            TreeNode store;

            TreeNode storeparent = null;

            if (this.tvLdap.SelectedNode == null)
            { store = this.tvLdap.Nodes[0]; }

            else
            { store = this.tvLdap.SelectedNode; }

            storeparent = store.Parent;

            store.Remove();
            
            TreeNode newnode = new TreeNode();

            newnode.Name = store.Name;
            newnode.Text = store.Text;
            newnode.ImageIndex = store.ImageIndex;
            newnode.SelectedImageIndex = store.ImageIndex;
            newnode.Tag = store.Tag;
            ((ADObjectInfo)newnode.Tag).Tag = false;
            
            if (storeparent != null)
            { storeparent.Nodes.Add(newnode); }

            else
            { this.tvLdap.Nodes.Add(newnode); }

            ExpandNode(newnode);

            this.tvLdap.Sort();
        }

        private bool FindNode(string entryPoint)
        {
            bool ret = true;

            TreeNode[] entrynodes = this.tvLdap.Nodes.Find(entryPoint, true);

            if (entrynodes.Count() != 0)
            {
                ret = false;

                ExpandNode(entrynodes[0]);
            }

            return ret;
        }

        #endregion

        #region controls

        private void tvLdap_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        { Collapsing = true; }
        
        private void tvLdap_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.tvLdap.SelectedNode != null)
            { ExpandNode(this.tvLdap.SelectedNode); }             
        }
 
        private void tvLdap_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        { GlobalControlHandler.TreeNodeMouseClickSelect(sender, e); }

        private void cmdSet_Click(object sender, EventArgs e)
        { SendPath(); }

        private void cmdRecall_Click(object sender, EventArgs e)
        { Recall(); }

        private void cmdCancel_Click(object sender, EventArgs e)
        { this.Hide(); }

        #endregion

        #region menues

        private void Tree_Select_ContextMenuItem_Click(object sender, EventArgs e)
        { SendPath(); }
        
        private void Tree_Recall_MenuItem_Click(object sender, EventArgs e)
        { Recall(); }

        private void Tree_ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.Tree_Query_ContextMenuItem.Enabled = (Caller == CALLING_CONTROL.GUI_BASE);
            this.Tree_Query_ContextMenuItem.Visible = (Caller == CALLING_CONTROL.GUI_BASE);

            this.Tree_SelectQuery_ContextMenuItem.Enabled = (Caller == CALLING_CONTROL.GUI_BASE);
            this.Tree_SelectQuery_ContextMenuItem.Visible = (Caller == CALLING_CONTROL.GUI_BASE);
        }
            
        private void Tree_Query_ContextMenuItem_Click(object sender, EventArgs e)
        { SendPath(false, true); }

        private void Tree_SelectQuery_ContextMenuItem_Click(object sender, EventArgs e)
        { SendPath(true, true); }

        #endregion        
    }
}
