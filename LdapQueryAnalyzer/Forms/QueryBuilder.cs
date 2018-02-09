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
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public partial class QueryBuilder : Form
    {
        #region fields

        public string Filtertext { get { return this.txtFilter.Text; } }

        private HexInfo HexConverter = new HexInfo();
        
        //private int CurrentClassesRowIndex = 0;
        //private int CurrentAttributesRowIndex = 0;
        private int GroupsCount = 0;
        private string ListQuery = String.Empty;

        private long CurrentFlags = 0;

        private List<FilterBlock> AddedFilters = new List<FilterBlock> { };

        private enum SPECIAL_OP
        {
            Equals,
            Not_Equals,
            Less_Or_Equals,
            Greater_Or_Equals,
            Bitwise_And,
            Bitwise_Nand,
            BitWise_Or,
            BitWise_Nor,
            MatchInChain,           
            None
        }

        #endregion

        #region LDAP constants

        internal const string MATCHING_RULE_BIT_AND = "1.2.840.113556.1.4.803";
        internal const string MATCHING_RULE_BIT_OR = "1.2.840.113556.1.4.804";
        internal const string MATCHING_RULE_IN_CHAIN = "1.2.840.113556.1.4.1941";

        #endregion

        #region constructor

        public QueryBuilder(string dcName) 
        { InitializeComponent(); }

        private void QueryBuilder_Load(object sender, EventArgs e)
        {
            FillClasses();

            //CurrentClassesRowIndex = 0;

            ToolTip thetips = new ToolTip();

            thetips.AutoPopDelay = 5000;
            thetips.InitialDelay = 500;
            thetips.ReshowDelay = 500;
            thetips.ShowAlways = true;

            thetips.SetToolTip(this.cmdNewAndGroup, "Open AND group");
            thetips.SetToolTip(this.cmdNewOrGroup, "Open OR group");
            thetips.SetToolTip(this.cmdCloseGroup, "Close current group");
            thetips.SetToolTip(this.cmdCloseAllGroups, "Close all open groups");
                        
            thetips.SetToolTip(this.cmdEQ, "Equals");
            thetips.SetToolTip(this.cmdNEQ, "Not Equals");
            thetips.SetToolTip(this.cmdLEQ, "Less or Equals");
            thetips.SetToolTip(this.cmdGEQ, "Greater or Equals");
            thetips.SetToolTip(this.cmdAND, "Bitwise AND operator");
            thetips.SetToolTip(this.cmdOR, "Bitwise OR operator");
            thetips.SetToolTip(this.cmdNAND, "Not Bitwise AND operator");
            thetips.SetToolTip(this.cmdNOR, "Not Bitwise OR operator");
            thetips.SetToolTip(this.cmdChain, "Match in Chain");
            thetips.SetToolTip(this.cmdClassEq, "Equals");
            thetips.SetToolTip(this.cmdClassNeq, "Not Equals");

            thetips.SetToolTip(this.cmdSet, "Apply filter to main window");
            thetips.SetToolTip(this.cmdClearFilter, "Clear filter");
            thetips.SetToolTip(this.cmdUnDo, "Undo last filter");
        }

        private void QueryBuilder_FormClosing(object sender, FormClosingEventArgs e)
        { GlobalEventHandler.RaiseFilterSignaled(null, true); }

        #endregion

        #region "custom"

        private void FillClasses()
        {
            lbClasses.Items.AddRange(ForestBase.ClassCache.Values.ToArray());
            this.lbClasses.Sorted = true;

            FillAttributes(0);
        }

        private void FillAttributes(int row)
        {
            if (row == ListBox.NoMatches) return;

            string classname = this.lbClasses.Items[row].ToString();

            if ((string)this.lbAttribs.Tag == classname)
            { return; }

            string localname = ForestBase.ClassCache.Where(c => c.Value == classname).Select(l => l.Key).ToList()[0];
            List<string> attribs = ForestInfo.LoadClassInfoFromCache(localname);

            this.lbAttribs.Items.Clear();

            this.lbAttribs.Tag = classname;

            this.lbAttribs.Items.AddRange(attribs.ToArray());
            this.lbAttribs.Sorted = true;
            
            //CurrentAttributesRowIndex = 0;
        }

        private void HandleAttribChange(int row)
        {
            if (row == ListBox.NoMatches) return;

            bool bitwise = false;
            bool matchinchain = false;
            bool isbool = false;

            this.cbGuid.Checked = false;

            string attrib = this.lbAttribs.Items[row].ToString();
                        
            if (ForestBase.AttributeCache.ContainsKey(attrib))
            {
                bitwise = ForestBase.AttributeCache[attrib].BitWise;
                matchinchain = ForestBase.AttributeCache[attrib].MatchInChain;
                isbool = ForestBase.AttributeCache[attrib].IsBool;

                if (attrib.ToLowerInvariant().Contains("guid"))
                { this.cbGuid.Checked = (ForestBase.AttributeCache[attrib].Syntax == ActiveDirectorySyntax.OctetString); }
            }

            this.cmdAND.Enabled = bitwise;

            this.cmdOR.Enabled = bitwise;

            this.cmdNAND.Enabled = bitwise;
            
            this.cmdNOR.Enabled = bitwise;
            
            this.Attributes_And_MenuItem.Enabled = bitwise;

            this.Attributes_Or_MenuItem.Enabled = bitwise;

            this.Attributes_Nand_MenuItem.Enabled = bitwise;

            this.Attributes_Nor_MenuItem.Enabled = bitwise;

            this.cmdChain.Enabled = matchinchain;

            this.Attributes_Chain_MenuItem.Enabled = matchinchain;

            this.txtVal.Visible = !isbool;
            this.cmdLEQ.Enabled = !isbool;
            this.cmdGEQ.Enabled = !isbool;
            this.cbGuid.Enabled = !isbool;
            this.txtBoolFrame.Visible = isbool;
            this.rbBoolFalse.Visible = isbool;
            this.rbBoolSet.Visible = isbool;
            this.rbBoolTrue.Visible = isbool;            
        }

        private void Apply()
        {
            cmdCloseAllGroups_Click(null, null);

            GlobalEventHandler.RaiseFilterSignaled(this.txtFilter.Text, false);

            cmdClearFilter_Click(null, null);

            this.Hide();
        }

        private void NewFilterGroup(string op, bool start = false)
        {            
            FilterBlock newfilter = new FilterBlock(" (  " + op + " ", FILTER_GROUPS.OPEN);

            if (start)
            { InsertGroup(start, newfilter); }

            else
            {
                if ((GroupsCount == 0) && (AddedFilters.Count > 0))
                {
                    FilterBlock openfilter = new FilterBlock(" (  & ", FILTER_GROUPS.OPEN);

                    InsertGroup(true, openfilter);
                }

                InsertGroup(false, newfilter);
            }
            

            SetFilterText();

            UpdateCMDGroup();
        }

        private void InsertGroup(bool start, FilterBlock newFilter)
        {
            if (start)
            { AddedFilters.Insert(0, newFilter); }

            else
            { AddedFilters.Add(newFilter); }

            GroupsCount++;
        }

        private void CloseFilterGroup()
        {
            if (GroupsCount != 0)            
            {
                string filter = (GroupsCount == 1) ? "  )" : " )";

                GroupsCount--;

                AddedFilters.Add(new FilterBlock(filter, FILTER_GROUPS.CLOSE));

                SetFilterText();

                UpdateCMDGroup();
            }            
        }

        private void CloseAllFilterGroups()
        {
            while (GroupsCount > 0)
            {
                CloseFilterGroup();
            }

            UpdateCMDGroup();

            AddedFilters.Clear();
        }

        private void UpdateCMDGroup()
        {
            this.Filter_CloseAll_MenuItem.Text = String.Format("close groups ({0})", GroupsCount);

            if (GroupsCount == 0)
            {
                this.cmdCloseGroup.Enabled = false;
                this.cmdCloseAllGroups.Enabled = false;
                this.Filter_CloseCurrent_MenuItem.Enabled = false;
                this.Filter_CloseAll_MenuItem.Enabled = false;
            }

            else
            {
                this.cmdCloseGroup.Enabled = true;
                this.cmdCloseAllGroups.Enabled = true;
                this.Filter_CloseCurrent_MenuItem.Enabled = true;
                this.Filter_CloseAll_MenuItem.Enabled = true;
            }
        }

        private void UpdateFilter(ListBox source, SPECIAL_OP op)
        {           
            if (source.SelectedIndex == ListBox.NoMatches) source.SelectedIndex = 0;

            string queryvalue = null;

            string valuename = null;

            if (source.Name == "lbClasses")
            { 
                valuename = "objectClass"; 
            
                queryvalue = source.SelectedItem.ToString();
            }

            else
            {
                valuename = source.SelectedItem.ToString();
                
                queryvalue = CheckValueHandling(valuename);

                if (queryvalue.Length == 0) queryvalue = "*";
            }

            string newfilter = String.Empty;

            switch (op)
            {
                case SPECIAL_OP.Equals:
                    newfilter = String.Format(" ({0}={1})", valuename, queryvalue);
                    break;

                case SPECIAL_OP.Not_Equals:
                    newfilter  = String.Format(" ( ! ({0}={1}) )", valuename, queryvalue);
                    break;

                case SPECIAL_OP.Less_Or_Equals:
                    newfilter = String.Format(" ({0}<={1})", valuename, queryvalue);
                    break;

                case SPECIAL_OP.Greater_Or_Equals:
                    newfilter = String.Format(" ({0}>={1})", valuename, queryvalue);
                    break;

                case SPECIAL_OP.Bitwise_And:
                    newfilter = String.Format(" ({0}:{1}:={2})", valuename, MATCHING_RULE_BIT_AND, queryvalue);
                    break;

                case SPECIAL_OP.Bitwise_Nand:
                    newfilter = String.Format(" ( ! ({0}:{1}:={2}) )", valuename, MATCHING_RULE_BIT_AND, queryvalue);
                    break;

                case SPECIAL_OP.BitWise_Or:
                    newfilter = String.Format(" ({0}:{1}:={2})", valuename, MATCHING_RULE_BIT_OR, queryvalue);
                    break;

                case SPECIAL_OP.BitWise_Nor:
                    newfilter = String.Format(" ( ! ({0}:{1}:={2}) )", valuename, MATCHING_RULE_BIT_OR, queryvalue);
                    break;

                case SPECIAL_OP.MatchInChain:
                    newfilter = String.Format(" ({0}:{1}:={2})", valuename, MATCHING_RULE_IN_CHAIN, queryvalue);
                    break;

            }

            if (newfilter.Length != 0)
            {
                if ((this.txtFilter.Text.Length != 0) && (GroupsCount == 0))
                { NewFilterGroup("&", true); }

                AddedFilters.Add(new FilterBlock(newfilter, FILTER_GROUPS.NONE));

                SetFilterText();                
            }

            this.cmdUnDo.Enabled = (AddedFilters.Count > 0);
        }

        private void SetFilterText()
        {
            this.txtFilter.Clear();

            if (AddedFilters.Count > 0)
            {
                foreach (FilterBlock curfilter in AddedFilters)
                {
                    this.txtFilter.Text += curfilter.Filter;
                }
            }
        }

        private void UnDoFilter()
        {
            if (AddedFilters.Count > 0)
            {
                FilterBlock curfilter = AddedFilters.Last();

                GroupsCount += curfilter.CounterHandle;

                AddedFilters.Remove(curfilter);
                
                if ((GroupsCount == 1) && (AddedFilters.Count == 2))
                {
                    GroupsCount = 0;

                    AddedFilters.RemoveAt(0);
                }
                
                SetFilterText();                
            }

            this.cmdUnDo.Enabled = (AddedFilters.Count > 0);

            UpdateCMDGroup();
        }

        private string CheckHex(string valueName)
        {
            string ret = this.txtVal.Text;

            ret = HexConverter.ToHex(ret, valueName);

            return ret;
        }

        private string CheckValueHandling(string valueName)
        {
            string ret = this.txtVal.Text;

            if (this.txtVal.Visible == false)
            {
                ret = "*";

                if (this.rbBoolFalse.Checked == true)
                { ret = "FALSE"; }

                else if (this.rbBoolTrue.Checked == true)
                { ret = "TRUE"; }
            }

            else
            {
                ret = HexConverter.ToHex(ret, valueName);
            }

            return ret;
        }

        private void LogicalFilterHandling(SPECIAL_OP op)
        {
            this.lvEnum.Tag = op;

            this.lvEnum.Items.Clear();

            if (this.lbAttribs.SelectedIndex == ListBox.NoMatches) this.lbAttribs.SelectedIndex = 0;

            string attributename = this.lbAttribs.SelectedItem.ToString();

            Dictionary<string, long> lvdata = new Dictionary<string, long>();

            Type entype = ForestBase.ADHelperDynamicDLL.GetEnumFromDynamicAssociator(attributename);

            if (entype != null)
            { 
                lvdata = EnumToListView(entype);

                foreach (KeyValuePair<string, long> item in lvdata)
                {
                    ListViewItem lvitem = new ListViewItem(item.Key);
                    lvitem.Tag = item.Value;

                    this.lvEnum.Items.Add(lvitem);
                }
            }

            if (this.lvEnum.Items.Count > 0)
            { 
                CmdVisibility(false);

                this.txtVal.Text = String.Empty; 
            }

            CurrentFlags = 0;                       
        }

        private void CmdVisibility(bool visible)
        {
            this.cmdEQ.Visible = visible;
            this.cmdNEQ.Visible = visible;

            this.cmdLEQ.Visible = visible;
            this.cmdGEQ.Visible = visible;

            this.cmdAND.Visible = visible;
            this.cmdOR.Visible = visible;

            this.cmdNAND.Visible = visible;
            this.cmdNOR.Visible = visible;

            this.cmdChain.Visible = visible;

            this.lbAttribs.Visible = visible;

            this.lvEnum.Visible = !visible;

            if (!visible)
            { this.lvEnum.BringToFront(); }

            else
            { this.lbAttribs.BringToFront(); }

            this.cmdHideEnum.Visible = !visible;

            this.cmdSetLogicalFilter.Visible = !visible;
            
        }

        private Dictionary<string, long> EnumToListView(Type enumType)
        {
            Dictionary<string, long> ret = new Dictionary<string, long>();

            foreach (object val in Enum.GetValues(enumType))
            {
                long lval = 0;

                try
                { lval = (long)Convert.ChangeType(val, typeof(long)); }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    try
                    { lval = (long)(int)Convert.ChangeType(val, typeof(int)); }

                    catch (Exception iex)
                    { iex.ToDummy(); }
                }

                ret.Add(val.ToString(), lval);
            }

            return ret;
        }

        
        #endregion

        #region "controls"
        
        private void ListBox_KeyEvents(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (((ListBox)sender).Name == "lbClasses")
                { UpdateFilter(this.lbClasses, SPECIAL_OP.Equals); }

                else
                { UpdateFilter(this.lbAttribs, SPECIAL_OP.Equals); }
            }

            else
            { GlobalControlHandler.ListBoxKeyEventSearch(sender, e, ref this.ListTimer, ref this.ListQuery); }
        }

        private void ListBox_KeyDownEvents(object sender, KeyEventArgs e)
        { GlobalControlHandler.KeySuppress(e); }

        private void txtVal_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            { e.SuppressKeyPress = true; }
        }

        private void txtVal_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            { UpdateFilter(this.lbAttribs, SPECIAL_OP.Equals); }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Control curcontrol = this.ActiveControl;

            if (curcontrol == null)
            { curcontrol = this.txtFilter; }

            if (this.Size.Height < 364)
            { this.Size = new Size(this.Size.Width, 364); }

            if (this.Size.Width < 607)
            { this.Size = new Size(607, this.Size.Height); }

            curcontrol.Select();
        }

        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        { FillAttributes(this.lbClasses.SelectedIndex); }
        
        private void lbClasses_DoubleClick(object sender, EventArgs e)
        { UpdateFilter(this.lbClasses, SPECIAL_OP.Equals); }

        private void lbAttribs_SelectedIndexChanged(object sender, EventArgs e)
        { HandleAttribChange(this.lbAttribs.SelectedIndex); }

        private void cmdSet_Click(object sender, EventArgs e)
        { Apply(); }

        private void cmdClearFilter_Click(object sender, EventArgs e)
        {
            CloseAllFilterGroups();

            this.txtFilter.Text = String.Empty;
        }

        private void cmdUnDo_Click(object sender, EventArgs e)
        { UnDoFilter(); }

        private void cmdEQ_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Equals); }

        private void cmdNEQ_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Not_Equals); }
        
        private void cmdLEQ_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Less_Or_Equals); }

        private void cmdGEQ_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Greater_Or_Equals); }

        private void cmdAnd_Click(object sender, EventArgs e)
        { LogicalFilterHandling(SPECIAL_OP.Bitwise_And); }

        private void cmdOr_Click(object sender, EventArgs e)
        { LogicalFilterHandling(SPECIAL_OP.BitWise_Or); }
        
        private void cmdNand_Click(object sender, EventArgs e)
        { LogicalFilterHandling(SPECIAL_OP.Bitwise_Nand); }

        private void cmdNor_Click(object sender, EventArgs e)
        { LogicalFilterHandling(SPECIAL_OP.BitWise_Nor); }

        private void lvEnum_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                CurrentFlags += (long)e.Item.Tag;
            }

            else
            {
                CurrentFlags -= (long)e.Item.Tag;
            }

            this.txtVal.Text = CurrentFlags.ToString();
        }

        private void cmdSetLogicalFilter_Click(object sender, EventArgs e)
        {
            UpdateFilter(this.lbAttribs, (SPECIAL_OP)this.lvEnum.Tag);

            cmdHideEnum_Click(null, null);
        }

        private void cmdHideEnum_Click(object sender, EventArgs e)
        { CmdVisibility(true); }

        private void cmdChain_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.MatchInChain); }
       
        private void cmdNewAndGroup_Click(object sender, EventArgs e)
        { NewFilterGroup("&"); }

        private void cmdCloseGroup_Click(object sender, EventArgs e)
        { CloseFilterGroup();}

        private void cmdNewOrGroup_Click(object sender, EventArgs e)
        { NewFilterGroup("|"); }

        private void cmdCloseAllGroups_Click(object sender, EventArgs e)
        { CloseAllFilterGroups(); }

        private void cmdClassEq_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbClasses, SPECIAL_OP.Equals);}

        private void cmdClassNeq_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbClasses, SPECIAL_OP.Not_Equals); }

        private void cbGuid_CheckedChanged(object sender, EventArgs e)
        { HexConverter.IsGuid = this.cbGuid.Checked; }

        #endregion

        #region "menu"
               
        private void Filter_Clear_MenuItem_Click(object sender, EventArgs e)
        {
            this.txtFilter.Text = null;
            
            GroupsCount = 0;

            this.cmdCloseAllGroups.Text = String.Format("close all ({0})", GroupsCount);
            this.Filter_CloseAll_MenuItem.Text = String.Format("close groups ({0})", GroupsCount);
        }

        private void File_Apply_MenuItem_Click(object sender, EventArgs e)
        { Apply();}

        private void Filter_And_MenuItem_Click(object sender, EventArgs e)
        { NewFilterGroup("&"); }

        private void Filter_Or_MenuItem_Click(object sender, EventArgs e)
        { NewFilterGroup("|"); }

        private void Filter_CloseCurrent_MenuItem_Click(object sender, EventArgs e)
        { CloseFilterGroup(); }

        private void Filter_CloseAll_MenuItem_Click(object sender, EventArgs e)
        { CloseAllFilterGroups(); }
        
        private void File_Close_MenuItem_Click(object sender, EventArgs e)
        { this.Close(); }
        

        private void Classes_Equals_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbClasses, SPECIAL_OP.Equals); }

        private void Classes_NotEquals_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbClasses, SPECIAL_OP.Not_Equals); }

        private void Classes_Copy_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyFromListBox(sender); }

        private void Classes_CopyAll_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyListBoxItems(sender); }


        private void Attributes_Equal_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Equals); }

        private void Attributes_NotEqual_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Not_Equals); }

        private void Attributes_LessOrEqual_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Less_Or_Equals); }

        private void Attributes_GreaterOrEqual_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Greater_Or_Equals); }

        private void Attributes_And_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Bitwise_And); }

        private void Attributes_Or_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.BitWise_Or); }
        
        private void Attributes_Nand_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.Bitwise_Nand); }

        private void Attributes_Nor_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.BitWise_Nor); }

        private void Attributes_Chain_MenuItem_Click(object sender, EventArgs e)
        { UpdateFilter(this.lbAttribs, SPECIAL_OP.MatchInChain); }
                
        private void Attributes_Copy_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyFromListBox(sender); }

        private void Attributes_CopyAll_ContextItem_Click(object sender, EventArgs e)
        { GlobalControlHandler.CopyListBoxItems(sender); }
 

        private void ValueContextMenu_Opening(object sender, CancelEventArgs e)
        { this.Value_Paste_ContextItem.Enabled = Clipboard.ContainsText(); }

        private void Value_Copy_ContextItem_Click(object sender, EventArgs e)
        { this.txtVal.Copy(); }

        private void Value_Paste_ContextItem_Click(object sender, EventArgs e)
        { this.txtVal.Paste(); }

        private void Value_Claer_ContextItem_Click(object sender, EventArgs e)
        { this.txtVal.Clear(); }
        
        #endregion             
    }
}

