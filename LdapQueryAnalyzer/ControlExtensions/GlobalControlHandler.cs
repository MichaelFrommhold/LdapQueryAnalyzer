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

using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class GlobalControlHandler
    {
        #region thread safety

        public static void ControlSetVisibility(Control ctrl, bool visible)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetVisibility(ctrl, visible); };

                try
                { ctrl.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ctrl.Visible = visible;
                ctrl.Refresh();
            }
        }

        public static void ControlSetState(Control ctrl, bool enabled)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetState(ctrl, enabled); };

                try
                { ctrl.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ctrl.Enabled = enabled;
                ctrl.Refresh();
            }
        }

        public static void ControlSetLocation(Control ctrl, Point where)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetLocation(ctrl, where); };

                try
                { ctrl.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ctrl.Location = where;
                ctrl.Refresh();
            }
        }

        public static void ControlSetSize(Control ctrl, Size measure)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetSize(ctrl, measure); };

                try
                { ctrl.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ctrl.Size = measure;
                ctrl.Refresh();
            }
        }
        
        public static void ControlSetText(Control ctrl, string msg)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetText(ctrl, msg); };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ctrl.Text = msg;
                ctrl.Refresh();
            }
        }

        public static string ControlGetText(Control ctrl)
        {
            string ret = null;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = ctrl.Text; };

                try
                { 
                    ctrl.Invoke(delcall);
                    
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ret = ctrl.Text;
            }

            return ret;
        }

        public static void ControlCheck(Control ctrl, bool value)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlCheck(ctrl, value); };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = value;
                }

                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Checked = value;
                }
                
                ctrl.Refresh();
            }
        }

        public static bool ControlIsChecked(Control ctrl)
        {
            bool ret = false;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = MyControlIsChecked(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                ret = MyControlIsChecked(ctrl);
            }

            return ret;
        }

        protected static bool MyControlIsChecked(Control ctrl)
        {
            bool ret = false;

            if (ctrl is CheckBox)
            {
                ret = ((CheckBox)ctrl).Checked;
            }

            else if (ctrl is RadioButton)
            {
                ret = ((RadioButton)ctrl).Checked;
            }

            return ret;
        }

        public static void ControlSetValue(Control ctrl, decimal value)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetValue(ctrl, value); };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                if (ctrl is NumericUpDown)
                {
                    ((NumericUpDown)ctrl).Value = value;
                }

                ctrl.Refresh();
            }
        }

        public static decimal ControlGetValue(Control ctrl)
        {
            decimal ret = 0;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => {

                    if (ctrl is NumericUpDown)
                    {
                        ret = ((NumericUpDown)ctrl).Value;
                    }
                };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                if (ctrl is NumericUpDown)
                {
                    ret = ((NumericUpDown)ctrl).Value;
                }
            }

            return ret;
        }

        public static object ControlGetTag(Control ctrl)
        {
            object ret = null;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = ctrl.Tag; };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            { ret = ctrl.Tag; }

            return ret;
        }

        public static void ControlSetTag(Control ctrl, object tag)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ctrl.Tag = tag; };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            { ctrl.Tag = tag; }
        }

        public static void ControlClearItems(Control ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlClearItems(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                object items = ctrl.GetPropertyValue("Items");

                if (items != null)
                { items.InvokeSafe("Clear"); }
            }
        }

        public static void ControlAddItemRange(Control ctrl, object[] values)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlAddItemRange(ctrl, values); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                object items = ctrl.GetPropertyValue("Items");

                if (items != null)
                { items.InvokeMethod("AddRange", ref values); }
            }
        }

        public static void ControlAddItem(Control ctrl, object value)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlAddItem(ctrl, value); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                object items = ctrl.GetPropertyValue("Items");

                if (items != null)
                {
                    object[] temp = new object[] { value };

                    items.InvokeMethod("Add", ref temp); 
                }
            }
        }

        public static void ControlSetIndex(Control ctrl, int index)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSetIndex(ctrl, index); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            { ctrl.SetPropertyValue("SelectedIndex", index); }
        }

        public static void ControlSortItems(Control ctrl, bool sort)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ControlSortItems(ctrl, sort); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            { ctrl.SetPropertyValue("Sorted", sort); }
        }
        
        public static void MenuSetState(ToolStripMenuItem item, bool enabled)
        {
            ToolStrip par = item.Owner; ;
            
            if (par.InvokeRequired)
            {
                MethodInvoker delcall = () => { MenuSetState(item, enabled); };

                try
                { par.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            { item.Enabled = enabled; }
        }

        public static void MenuSetCheckedState(ToolStripMenuItem item, bool check)
        {
            ToolStrip par = item.Owner; // GetCurrentParent();

            if (par.InvokeRequired)
            {
                MethodInvoker delcall = () => { MenuSetCheckedState(item, check); };

                try
                { par.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            { item.Checked = check; }
        }

        public static void MenuSetText(ToolStripTextBox item, string value)
        {
            ToolStrip par = item.GetCurrentParent();

            if (par.InvokeRequired)
            {
                MethodInvoker delcall = () => { MenuSetText(item, value); };

                try
                { par.Invoke(delcall); }

                catch { }
            }

            else
            { item.Text = value; }
        }


        public static void ListViewSetColumnWidth(ListView ctrl, 
                                                  int col, 
                                                  ColumnHeaderAutoResizeStyle autoSet = ColumnHeaderAutoResizeStyle.ColumnContent, 
                                                  int width = -1)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ListViewSetColumnWidth(ctrl, col, autoSet, width); };

                ctrl.Invoke(delcall);
            }

            else
            {
                if (width > 0)
                {
                    if (col > 0)
                    { ctrl.Columns[col].Width = width; }
                }

                else
                {
                    if (col < 0)
                    { ctrl.AutoResizeColumns(autoSet); }

                    else
                    { ctrl.AutoResizeColumn(col, autoSet); }
                }
            }
        }

        public static ListView.ColumnHeaderCollection ListViewGetColumns(ListView ctrl)
        {
            ListView.ColumnHeaderCollection ret = null;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = ctrl.Columns; };

                ctrl.Invoke(delcall);
            }

            else
            {
                ret = ctrl.Columns;
            }

            return ret;
        }

        public static void ListViewSetColumnTag(ListView ctrl, int col, object tag)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ctrl.Columns[col].Tag = tag; };

                ctrl.Invoke(delcall);
            }

            else
            { ctrl.Columns[col].Tag = tag; }
        }

        public static object ListViewGetColumnTag(ListView ctrl, int col)
        {
            object ret = null;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = ctrl.Columns[col].Tag; };

                ctrl.Invoke(delcall);
            }

            else
            { ret = ctrl.Columns[col].Tag; }

            return ret;
        }

        public static List<ListViewItem> ListViewGetItems(ListView ctrl)
        {
            List<ListViewItem> ret = new List<ListViewItem> { };

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = CopyListViewItems(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            { ret = CopyListViewItems(ctrl); }

            return ret;
        }

        protected static List<ListViewItem> CopyListViewItems(ListView ctrl)
        {
            List<ListViewItem> ret = new List<ListViewItem> { };

            foreach (ListViewItem item in ctrl.Items)
            { ret.Add(item); }

            return ret;
        }

        public static bool ListViewRemoveSelectedItem(ListView ctrl)
        {
            bool ret = false;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = ListViewRemoveSelectedItem(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                if (ctrl.SelectedIndices.Count > 0)
                { 
                    ctrl.Items.RemoveAt(ctrl.SelectedIndices[0]);

                    ret = true;
                }
            }

            return ret;
        }

        public static bool ListViewRemoveSelectedItems(ListView ctrl)
        {
            bool ret = false;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ListViewRemoveSelectedItems(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                if (ctrl.SelectedIndices.Count > 0)
                {
                    foreach (int idx in ctrl.SelectedIndices)
                    { ctrl.Items.RemoveAt(idx); }

                    ret = true;
                }
            }

            return ret;
        }

        public static void ListViewClearItems(ListView ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ctrl.Items.Clear(); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            { ctrl.Items.Clear(); }
        }

        public static void ListViewResetSelectedItem(ListView ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ListViewResetSelectedItem(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                int cols = ctrl.Columns.Count;

                List<string> items = new List<string> { };

                for (int cnt = 1; cnt <= cols; cnt++)
                { items.Add(""); }

                ListViewItem li = new ListViewItem(items.ToArray());
                
                if (ctrl.Items.Count == 0)
                {
                    ctrl.Items.Add(li);
                }

                else
                {
                    int pos = 0;

                    if (ctrl.SelectedIndices.Count > 0)
                    { pos = ctrl.SelectedIndices[0]; }

                    ctrl.Items.RemoveAt(pos);

                    ctrl.Items.Insert(pos, li);
                }
            }
        }

        public static bool ListViewAddItem(ListView ctrl, object tag, bool unique, params string[] subItems)
        {
            bool ret = false;

            ret = ListViewInsertItemAt(ctrl, tag, -1, unique, subItems);

            return ret;
        }

        public static bool ListViewInsertItemAt(ListView ctrl, object tag, int pos, bool unique, params string[] subItems)
        {
            bool ret = false;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ret = ListViewInsertItemAt(ctrl, tag, pos, unique, subItems); };

                try
                { ctrl.Invoke(delcall); }

                catch { }
            }

            else
            {
                ListViewItem li = MakeListViewItem(tag, subItems);

                ret = InsertListViewItem(ctrl, li, pos, unique); 
            }

            return ret;
        }

        public static bool InsertListViewItem(ListView ctrl, ListViewItem lvItem, int pos, bool unique)
        {
            bool save = true;
                                    
            if (unique)
            {
                string hash = lvItem.Tag.GetMD5Hash();

                foreach (ListViewItem item in ctrl.GetItems())
                {
                    string thash = item.Tag.GetMD5Hash();

                    if (hash == thash)
                    { save = false; break; }
                }
            }

            if (save)
            {
                
                if (pos >= 0)
                {
                    if (ctrl.Items.Count == 0)
                    { ctrl.Items.Add(lvItem); }

                    else
                    { ctrl.Items.Insert(pos, lvItem); }
                }

                else
                { ctrl.Items.Add(lvItem); }
            }

            return save;
        }

        protected static ListViewItem MakeListViewItem(object tag, params string[] subItems)
        {
            ListViewItem ret = new ListViewItem();

            ret = new ListViewItem(subItems);

            ret.Tag = tag;

            return ret;
        }

        #endregion

        public static void CopyFromListBox(object sender)
        {
            try
            {
                ListBox lb = (ListBox)((ContextMenuStrip)(((ToolStripItem)(sender)).Owner)).SourceControl;

                Clipboard.SetText(lb.Text, TextDataFormat.Text);
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public static void CopyListBoxItems(object sender)
        {
            ListBox lb = (ListBox)((ContextMenuStrip)(((ToolStripItem)(sender)).Owner)).SourceControl;

            string[] items = new string[lb.Items.Count];

            lb.Items.CopyTo(items, 0);

            CopyItems(items);
        }

        public static void CopyItems(string[] items)
        { Clipboard.SetText(String.Join(Environment.NewLine, items)); }

        public static void CopyFromCombo(object sender)
        {
            try
            {
                ComboBox cmb = (ComboBox)((ContextMenuStrip)(((ToolStripItem)(sender)).Owner)).SourceControl;

                Clipboard.SetText(cmb.Text, TextDataFormat.Text);
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public static void CopyComboBoxItems(object sender)
        {
            try
            {
                ComboBox cmb = (ComboBox)((ContextMenuStrip)(((ToolStripItem)(sender)).Owner)).SourceControl;

                string[] items = new string[cmb.Items.Count];

                cmb.Items.CopyTo(items, 0);

                CopyItems(items);
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public static void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListBox lb = sender as ListBox;

                int wantedindex = lb.IndexFromPoint(e.X, e.Y);

                if (wantedindex != ListBox.NoMatches)
                { lb.SelectedIndex = wantedindex; }
            }
        }

        public static void FindStringInTextBox(Control control, string find, int start)
        {
            int hit = 0;

            hit = control.Text.IndexOf(find, start, StringComparison.InvariantCultureIgnoreCase);

            if (hit > 0)
            {
                control.SetPropertyValue("SelectionStart", hit);
                control.SetPropertyValue("SelectionLength", find.Length);
                control.InvokeSafe("ScrollToCaret");

                //control.SelectionStart = hit;
                //control.SelectionLength = 0;
                //control.ScrollToCaret();
            }
        }

        public static void StartTimer(System.Windows.Forms.Timer listTimer)
        {
            listTimer.Tick += new EventHandler(ListTimerEvent);

            listTimer.Enabled = true;
        }

        public static void ListTimerEvent(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer lt = (System.Windows.Forms.Timer)sender;

            try
            { lt.Tick -= new EventHandler(ListTimerEvent); }
            catch (Exception ex) { ex.ToDummy(); ; }

            try
            { lt.Enabled = false; }
            catch (Exception ex) { ex.ToDummy(); }
        }

        public static void UpdateGroupBoxText(GroupBox control, string msg)
        {
            if (control.InvokeRequired)
            {
                MethodInvoker delcall = () => { UpdateGroupBoxText(control, msg); };

                try
                { control.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                control.Text = msg;
                control.Refresh();
            }
        }

        public static void UpdateControlVisibility(Control ctrl, bool visible)
        {
            if (ctrl.Visible == visible) return;

            if (ctrl.InvokeRequired)
            {
                MethodInvoker delcall = () => { ctrl.Visible = visible; };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            { ctrl.Visible = visible; }
        }

        public static List<string> GetLinesFromTextBox(Control ctrl)
        {
            List<string> ret = null;

            if (ctrl.InvokeRequired)
            {
                TextBox blub = new TextBox();

                MethodInvoker delcall = () => { ret = GetLinesFromTextBox(ctrl); };

                try
                { ctrl.Invoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                string[] temp = (string[])ctrl.GetPropertyValue("Lines");
                ret = temp.ToList();

                //ret = ctrl.Lines.ToList();
            }

            return ret;
        }

        public static void AppendToTextBox(TextBox control, string msg)
        {
            if (control.InvokeRequired)
            {
                MethodInvoker delcall = () => { AppendToTextBox(control, msg); };

                try
                { control.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                string[] lines = control.Lines;

                lines.AddSafe(msg, ref lines);

                control.Lines = lines; 
            }
        }

        public static void UpdateTextBox(TextBox control, string msg, bool append = false, int insertAt = -1)
        {
            List<string> lmsg = new List<string> { msg };

            UpdateTextBox(control, lmsg, append, insertAt);
        }
        
        public static void UpdateTextBox(Control control, List<string> msg, bool append = false, int insertAt = -1)
        {
            if (control.InvokeRequired)
            {
                MethodInvoker delcall = () => { UpdateTextBox(control, msg, append, insertAt); };

                try
                { control.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                List<string> temp = new List<string>();

                if (!append)
                {
                    control.InvokeSafe("Clear");

                    temp = msg;

                    object[] args = new object[] { 0, 0 };

                    control.InvokeMethod("Select", ref args);

                    //control.Select(0, 0);

                    control.InvokeSafe("ScrollToCaret");

                    //control.ScrollToCaret();
                }

                else
                {
                    temp = ((string[])control.GetPropertyValue("Lines")).ToList();
                    //temp = control.Lines.ToList();

                    //if ((control.Lines.Count() < 1000) || (msg.Count < 1000))
                    //{ temp = control.Lines.ToList(); }

                    //else
                    //{
                    //    temp.Add(control.Lines[control.Lines.Count() - 2]);
                    //    temp.Add(control.Lines[control.Lines.Count() - 1]);
                    //}

                    if (insertAt == -1)
                    { temp.AddRange(msg); }

                    else
                    { temp.InsertRange(insertAt, msg); }                    
                }

                control.SetPropertyValue("Lines", temp.ToArray());
                //control.Lines = temp.ToArray();

                if (insertAt == -1)
                {
                    string txt = (string)control.GetPropertyValue("Text");

                    object[] args = new object[] { txt.Length, 0 };

                    control.InvokeMethod("Select", ref args);
                    //control.Select(control.Text.Length, 0); 
                }

                else
                {
                    object[] args = new object[] { insertAt, 0 };

                    control.InvokeMethod("Select", ref args);

                    // control.Select(insertAt, 0); 
                }

                control.InvokeSafe("ScrollToCaret");
                //control.ScrollToCaret();

                control.Refresh();
            }
        }

        public static void ClearTextBox(Control control)
        { control.InvokeSafe("Clear"); }

        public static void AppendTextToListBox(ListBox control, string msg)
        {
            if (control.InvokeRequired)
            {
                MethodInvoker delcall = () => { AppendTextToListBox(control, msg); };

                try
                { control.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                foreach (string line in msg.Split(new char[] { '\n' }))
                {
                    control.Items.Add(line);

                    int itemsPerPage = (int)(control.Height / control.ItemHeight);

                    control.TopIndex = control.Items.Count - itemsPerPage;

                    control.Refresh();
                }
            }
        }

        public static void ListBoxKeyEventSearch(object sender, KeyEventArgs e, ref Timer listTimer, ref string searchString, bool suppress = true)
        {
            e.Handled = true;

            e.SuppressKeyPress = suppress;

            ListBox lbox = (ListBox)sender;

            if (((Keys.D9 < e.KeyCode) && (e.KeyCode < Keys.LWin)) || (e.KeyCode == Keys.OemMinus))
            {
                if (listTimer.Enabled)
                {
                    if (e.KeyCode == Keys.OemMinus)
                    { searchString += "-"; }

                    else
                    { searchString += e.KeyCode.ToString(); }

                    listTimer.Enabled = false;
                    GlobalControlHandler.StartTimer(listTimer);
                }

                else
                {
                    GlobalControlHandler.StartTimer(listTimer);

                    if (e.KeyCode == Keys.OemMinus)
                    { searchString = "-"; }

                    else
                    { searchString = e.KeyCode.ToString(); }
                }

                int currow = lbox.FindString(searchString, 0);

                lbox.SelectedIndex = currow;
            }

            else
            { ListBoxKeyEvent(sender, e); }


        }

        public static void ListBoxKeyEvent(object sender, KeyEventArgs e)
        {
            ListBox lbox = (ListBox)sender;

            if (e.KeyCode == Keys.Down)
            {
                try
                { lbox.SelectedIndex += 1; }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else if (e.KeyCode == Keys.Up)
            {
                try
                { lbox.SelectedIndex -= 1; }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else if (e.KeyCode == Keys.PageDown)
            {
                try
                { lbox.SelectedIndex += 8; }

                catch (Exception ex)
                {
                    lbox.SelectedIndex = lbox.Items.Count - 1;

                    ex.ToDummy();
                }
            }

            else if (e.KeyCode == Keys.PageUp)
            {
                try
                { lbox.SelectedIndex -= 8; }

                catch (Exception ex)
                {
                    lbox.SelectedIndex = 0;
                    ex.ToDummy();
                }
            }

        }

        public static bool ListViewDeleteItem(object sender, out TagInfo tag)
        {
            bool ret = false;

            tag = new TagInfo();

            ListView lv = (ListView)((ContextMenuStrip)(((ToolStripItem)(sender)).Owner)).SourceControl;

            if (lv.SelectedItems.Count > 0)
            {
                int pos = lv.SelectedIndices[0];

                tag.Name = lv.Items[pos].Text;

                tag.Tag = lv.Items[pos].Tag;

                if (tag.Tag == null)
                { tag.Tag = pos; }

                lv.Items.RemoveAt(pos);

                pos = (pos <= lv.Items.Count - 1) ? pos : pos - 1;

                lv.Items[pos].Selected = true; 

                ret = true;
            }

            return ret;
        }

        public static int ListViewAddItem(ListView control, int subCount, bool insert)
        {

            int ret = 0;

            ListViewItem item = new ListViewItem(new string[] { String.Empty });

            if (subCount != 1)
            {            
                for (int cnt = 1; cnt <= subCount; cnt++)
                { item.SubItems.Add(String.Empty); }
            }
            
            ret = control.Items.Count;

            if ((insert) && (control.SelectedItems.Count != 0))
            {
                ret = control.SelectedIndices[0];

                control.Items.Insert(ret, item);
            }

            else
            { control.Items.Add(item); }

            return ret;
        }

        public static int ListViewMoveItem(object sender, int step)
        {
            int ret = -1;

            ListView lv = (ListView)((ContextMenuStrip)(((ToolStripItem)(sender)).Owner)).SourceControl;

            if (lv.SelectedItems.Count > 0)
            {
                int pos = lv.SelectedIndices[0];

                ListViewItem item = lv.Items[pos];

                if ((pos + step < 0) || (pos + step > lv.Items.Count - 1))
                { return ret; }

                lv.Items.RemoveAt(pos);

                lv.Items.Insert(pos + step, item);

                ret = pos;
            }

            return ret;
        }

        public static void ListViewRightMouseClick(object sender, MouseEventArgs e, ContextMenuStrip menu = null, int[] enabledItems = null)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
            { return; }

            ListView lv = (ListView)sender;

            ListViewClearSelecion(lv);

            int mpos = e.Y;

            foreach (ListViewItem item in lv.Items)
            {
                if ((mpos >= item.Position.Y) && (mpos <= item.Position.Y + item.Bounds.Height))
                { 
                    item.Selected = true;

                    break;
                }
            }

            if (menu != null)
            { ResetContextMenu(menu, enabledItems); }

            if (menu != null)
            { menu.Show(lv, e.Location); }
        }

        public static void ListViewClearSelecion(ListView control)
        {
            foreach (ListViewItem item in control.SelectedItems)
            { item.Selected = false; }
        }

        public static void ListViewEditItem(ListView control, int curPos, Control[] editControls, bool edit = false)
        {
            control.Items[curPos].Selected = true;
            
            control.Items[curPos].EnsureVisible();

            Point startpos = control.Items[curPos].Position;

            List<int> widths = new List<int>();

            int top = control.Top;

            int left = 0 + control.Left;

            foreach (ColumnHeader col in control.Columns)
            { widths.Add(col.Width); }

            for (int cnt = 0; cnt < editControls.Count(); cnt++)
            {
                editControls[cnt].Text = control.Items[curPos].SubItems[cnt].Text;

                if ((editControls[cnt] is ComboBox) && (control.Items[curPos].SubItems[cnt].Text.Length == 0))
                { ((ComboBox)editControls[cnt]).SelectedIndex = 0; }

                editControls[cnt].Top = top + startpos.Y;

                editControls[cnt].Left = left;

                editControls[cnt].Width = widths[cnt];

                SubItemTag tag = new SubItemTag(control, curPos, cnt, edit);

                tag.Controls = editControls;

                editControls[cnt].Tag = tag;

                editControls[cnt].Visible = true;
                editControls[cnt].Enabled = true;
                editControls[cnt].BringToFront();

                left += widths[cnt] + 1;

                if (cnt == 0)
                { editControls[cnt].Select(); }
            }

            ((SubItemTag)editControls[editControls.Count() - 1].Tag).Controls = editControls;
        }

        public static void ListViewEditFieldKeyPress(Control editControl, Control nextControl, KeyEventArgs e, out bool update)
        {
            update = false;

            SubItemTag tag = (SubItemTag)editControl.Tag;

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return) || (e.KeyCode == Keys.Tab))
            {
                e.SuppressKeyPress = true;

                tag.SubItem.Text = editControl.Text; 

                if (nextControl != null)
                { nextControl.Select(); }

                else
                {
                    HideEditControls(editControl);

                    update = true;
                }
            }

            else if ((e.KeyCode == Keys.Cancel) || (e.KeyCode == Keys.Escape))
            {
                if (!tag.IsEdit)
                { tag.List.Items.Remove(tag.Item); }
                
                HideEditControls(editControl);
            }

            else
            {
                if (editControl is TextBox)
                { tag.SubItem.Text = editControl.Text; }
            }
        }

        public static void ListViewRemoveItem(ListView control, string itemKey, object tagId = null)
        {
            if (tagId == null)
            {
                int lastpos = control.Items[itemKey].Index;

                control.Items[itemKey].Remove();

                ListViewSelectExistant(control, lastpos);
            }

            else
            {
                foreach (ListViewItem item in control.Items)
                {
                    int lastpos = 0;

                    if ((item.Name == itemKey) && (item.Tag.Equals(tagId)))
                    {
                        lastpos = item.Index;

                        control.Items.Remove(item);

                        ListViewSelectExistant(control, lastpos);

                        break;
                    }
                }  
            }
        }

        public static void ListViewSelectExistant(ListView control, int lastPos)
        {
            if (lastPos != 0)
            { lastPos--; }

            control.Items[lastPos].Selected = true;
        }

        public static void HideEditControls(Control editControl)
        {
            if (((SubItemTag)editControl.Tag).Controls == null)
            { 
                editControl.Visible = false;

                editControl.Enabled = false;
            }

            else
            {
                foreach (Control ctrl in ((SubItemTag)editControl.Tag).Controls)
                { 
                    ctrl.Visible = false;

                    ctrl.Enabled = false;
                }
            }
        }

        public static void ResetContextMenu(ContextMenuStrip menu, int[] enabledItems)
        {           
            if (enabledItems != null)
            {
                foreach (ToolStripItem item in menu.Items)
                { item.Enabled = false; }

                foreach (int it in enabledItems)
                {
                    menu.Items[it].Enabled = true;
                }
            }
        }

        public static void KeySuppress(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.C) && (e.Modifiers == Keys.Control))
            { e.SuppressKeyPress = false; }

            else
            { e.SuppressKeyPress = true; }
        }

        public static void TreeNodeMouseClickSelect(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            { ((TreeView)sender).SelectedNode = e.Node; }
        }

        public static void CallBrowser(ref LDAPBrowser lDP, LDAPBrowser.CALLING_CONTROL whoCalled, QueryControl mustQuery, string entryPoint, string dcName, ForestInfo fiH)
        {
            if (lDP == null) 
            { lDP = new LDAPBrowser(whoCalled, mustQuery, entryPoint, dcName, fiH); }

            else if (lDP.IsDisposed)
            { lDP = new LDAPBrowser(whoCalled, mustQuery, entryPoint, dcName, fiH); }

            else
            { lDP.ShowTree(whoCalled, mustQuery, entryPoint, dcName, fiH); }
        }

        public static void SetGroupBoxAlert(GroupBox[] gbList, bool alert)
        {
            foreach (GroupBox gb in gbList)
            { 
                gb.Tag = alert ? 1 : 0;

                foreach (Control ctrl in gb.Controls)
                {
                    if ((ctrl is Button) && (ctrl.Tag != null))
                    { ctrl.Enabled = alert; }
                }

                gb.Refresh();
            }
        }

        public static void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gb = (GroupBox)sender;

            int tag = (gb.Tag == null) ? 0 : (int)gb.Tag;

            if (tag == 1)
            { ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.DarkRed, ButtonBorderStyle.Solid); }

            else
            { ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, SystemColors.Control, ButtonBorderStyle.None); }
        }

        public static void UpdateLabel(Label control, string msg = null, bool visible = true)
        {
            if (control.InvokeRequired)
            {
                MethodInvoker delcall = () => { UpdateLabel(control, msg); };

                try
                { control.BeginInvoke(delcall); }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            else
            {
                control.Visible = visible;

                control.Text = msg;

                control.Refresh();
            }
        }
    }
}
