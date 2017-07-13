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
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    
    public partial class AttributeTypeAssociator : Form
    {
        #region nested classes

        private class EnumField
        {
            public string Name;
            public string Value;

            public EnumField()
            { }

            public EnumField(string fieldName, string fieldValue)
            {
                Name = fieldName;
                Value = fieldValue;
            }            
        }

        private class BerField
        {
            public string Name;
            public string TypeName;

            public BerField()
            { }

            public BerField(string fieldName, string fieldType)
            {
                Name = fieldName;
                TypeName = fieldType;
            }
        }

        #endregion

        #region fields

        private bool IsDirty = false;
        private bool Loading = true;
        private bool HandleIncludeTags = true;

        private DynamicEnum TempEnum;
        private DynamicBerConverter TempBer;
        private DynamicAttributeAssociator TempAssoc;
        private Dictionary<string, string> TempDict;

        private TextBoxExtension txtAssocName;
        private TextBoxExtension txtEnumName;
        private TextBoxExtension txtValueName;
        private TextBoxExtension txtValue;
        private TextBoxExtension txtBerName;
        private ComboBoxExtension cmbTypes;
        private TextBoxExtension txtFieldName;
        private TextBoxExtension txtDictName;
        private TextBoxExtension txtKeyName;
        private TextBoxExtension txtValName;

        #endregion

        #region constructor

        public AttributeTypeAssociator()
        { 
            InitializeComponent();

            InitializeExtensionComponents();
        }

        private void DynamicTypeBuilder_Load(object sender, EventArgs e)
        { }

        private void DynamicTypeBuilder_Shown(object sender, EventArgs e)
        { LoadInternal(); }

        private void DynamicTypeBuilder_FormClosing(object sender, FormClosingEventArgs e)
        { GlobalEventHandler.RaiseFormExited(this.Name); }

        #endregion

        #region custom

        private void LoadInternal()
        {
            ResetForm();

            this.cmbTypes.SelectedIndex = 0;

            if (MainBase.DynamicDll == null)
            { MainBase.DynamicDll = new DynamicTypeLoader(); }

            foreach (KeyValuePair<string, DynamicEnum> dynenum in MainBase.DynamicDll.DynamicEnumList)
            {
                this.lvEnums.Items.Add(new ListViewItem(dynenum.Key) { Tag = dynenum.Value, Name = dynenum.Key });
            }

            Loading = false;

            GlobalControlHandler.ListViewClearSelecion(this.lvEnums);

            if (this.lvEnums.Items.Count > 0)
            { this.lvEnums.Items[0].Selected = true; }

            Loading = true;

            foreach (KeyValuePair<string, DynamicBerConverter> dynber in MainBase.DynamicDll.BerConverterList)
            {
                foreach (int pos in dynber.Value.ConversionRules.Keys)
                { this.lvBer.Items.Add(new ListViewItem(dynber.Key) { Tag = pos, Name = dynber.Key }); }
            }

            Loading = false;

            GlobalControlHandler.ListViewClearSelecion(this.lvBer);

            if (this.lvBer.Items.Count > 0)
            { this.lvBer.Items[0].Selected = true; }

            Loading = true;

            foreach (KeyValuePair<string, Dictionary<string, string>> dyndict in MainBase.DynamicDll.DictionaryList)
            {
                this.lvDict.Items.Add(new ListViewItem(dyndict.Key) { Tag = dyndict.Value, Name = dyndict.Key });
            }

            Loading = false;

            GlobalControlHandler.ListViewClearSelecion(this.lvDict);

            if (this.lvDict.Items.Count > 0)
            { this.lvDict.Items[0].Selected = true; }

            Loading = true;

            this.cmbTypeAssoc.DataSource = Enum.GetValues(typeof(ASSOCIATE_FROM));
            this.cmbTypeAssoc.SetText(ASSOCIATE_FROM.NONE.ToString());

            UpdateEnumCombo();

            UpdateDictCombo();

            UpdateBerCombo();

            foreach (KeyValuePair<string, DynamicAttributeAssociator> assoc in MainBase.DynamicDll.AssociatorList)
            { this.lvAssoc.Items.Add(new ListViewItem(assoc.Key) { Tag = assoc.Value, Name = assoc.Key }); }

            Loading = false;

            GlobalControlHandler.ListViewClearSelecion(this.lvAssoc);

            if (this.lvAssoc.Items.Count > 0)
            { this.lvAssoc.Items[0].Selected = true; }

            GetBackups();
        }

        private void ResetForm()
        {
            Loading = true;

            this.lvAssoc.Items.Clear();
            this.lvBer.Items.Clear();
            this.lvBerValues.Items.Clear();
            this.lvDict.Items.Clear();
            this.lvEnums.Items.Clear();
            this.lvKeyVal.Items.Clear();
            this.lvValues.Items.Clear();
            this.cmbAssocBer.Items.Clear();
            this.cmbAssocDict.Items.Clear();
            this.cmbAssocEnum.Items.Clear();
        }

        private void LoadDefault()
        { LoadInternal(); }

        private void DoBackup(object path, GlobalEventArgs args)
        {
            if (path == null)
            { return; }

            GlobalEventHandler.DoBackup -= DoBackup;

            MainBase.DynamicDll.LoadBackup(path.ToString());

            MainBase.DynamicDll = new DynamicTypeLoader();

            LoadInternal();

            GetBackups();
        }

        private void UpdateEnumCombo(string val = "NONE")
        {
            this.cmbAssocEnum.Items.Clear();

            this.cmbAssocEnum.Items.Add("NONE");

            for (int cnt = 0; cnt < this.lvEnums.Items.Count; cnt++)
            { this.cmbAssocEnum.Items.Add(this.lvEnums.Items[cnt].Text); }

            this.cmbAssocEnum.SetText(val);
        }

        private void UpdateBerCombo(string val = "NONE")
        {
            this.cmbAssocBer.Items.Clear();

            this.cmbAssocBer.Items.Add("NONE");

            for (int cnt = 0; cnt < this.lvBer.Items.Count; cnt++)
            { this.cmbAssocBer.Items.Add(this.lvBer.Items[cnt].Text); }

            this.cmbAssocBer.SetText(val);
        }

        private void UpdateDictCombo(string val = "NONE")
        {
            this.cmbAssocDict.Items.Clear();

            this.cmbAssocDict.Items.Add("NONE");

            for (int cnt = 0; cnt < this.lvDict.Items.Count; cnt++)
            { this.cmbAssocDict.Items.Add(this.lvDict.Items[cnt].Text); }

            this.cmbAssocDict.SetText(val);
        }

        private void GetBackups()
        {
            Loading = true;

            this.File_Restore_Picker_MenuItem.Items.Clear();

            foreach (FileInfo bkp in (new DirectoryInfo(MainBase.DynamicDll.CachePath)).GetFiles("*.bkp*"))
            {
                TagInfo bkptag = new TagInfo() { Name = bkp.LastWriteTime.ToString(), Tag = bkp.FullName };

                this.File_Restore_Picker_MenuItem.Items.Add(bkptag);
            }

            if (this.File_Restore_Picker_MenuItem.Items.Count != 0)
            { this.File_Restore_Picker_MenuItem.SelectedIndex = 0; }

            Loading = false;
        }

        #region assoc

        private void AssocChanged()
        {
            if ((this.lvAssoc.SelectedItems.Count != 0) && (this.lvAssoc.SelectedItems[0].Tag != null))
            {
                this.gbAssoc.SetText(String.Format("Associations ({0})", this.lvAssoc.SelectedItems[0].Text));

                DisplayAssoc((DynamicAttributeAssociator)this.lvAssoc.SelectedItems[0].Tag);
            }
        }

        private void DisplayAssoc(DynamicAttributeAssociator assoc)
        {
            this.cmbTypeAssoc.SelectItem(assoc.AssociateFrom);

            this.cmbAssocEnum.SetText("NONE");
            this.cmbAssocBer.SetText("NONE");
            this.cmbAssocDict.SetText("NONE");

            if (assoc.AssociateFrom == ASSOCIATE_FROM.ENUM)
            { this.cmbAssocEnum.SelectItem(assoc.EnumName); }

            else if (assoc.AssociateFrom == ASSOCIATE_FROM.BERCONVERTER)
            { this.cmbAssocBer.SelectItem(assoc.BerConverterName); }

            else if (assoc.AssociateFrom == ASSOCIATE_FROM.DICTIONARY)
            { this.cmbAssocDict.SelectItem(assoc.DictionaryName); }
        }

        private void AddAssoc()
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvAssoc, 1, false);

            GlobalControlHandler.ListViewEditItem(this.lvAssoc, curpos, new Control[] { this.txtAssocName });
        }

        private void UpdateNewAssocInfo()
        {   
            TempAssoc = new DynamicAttributeAssociator();
            TempAssoc.Name = this.lvAssoc.SelectedItems[0].Text;

            this.lvAssoc.SelectedItems[0].Tag = TempAssoc;

            this.lvAssoc.SelectedItems[0].Name = TempAssoc.Name;

            AssocChanged();

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbAssoc }, true);

            this.lvAssoc.Enabled = false;

            AssocChanged();
        }

        private void SaveAssoc(bool save = true)
        {
            bool isenum = false;

            ASSOCIATE_FROM assoc = ASSOCIATE_FROM.NONE;

            if (save)
            {
                object dassoc = ASSOCIATE_FROM.NONE;

                this.cmbTypeAssoc.Text.EnumTryParse(typeof(ASSOCIATE_FROM), out dassoc);

                assoc = (ASSOCIATE_FROM)dassoc;

                save = (assoc != ASSOCIATE_FROM.NONE);

                if (save)
                {
                    isenum = (this.cmbTypeAssoc.Text == ASSOCIATE_FROM.ENUM.ToString());

                    if (isenum)
                    { save = (this.cmbAssocEnum.Text != ASSOCIATE_FROM.NONE.ToString()); }

                    else
                    { save = (this.cmbAssocBer.Text != ASSOCIATE_FROM.NONE.ToString()); }
                }
            }

            if (save)
            {

                TempAssoc.AssociateFrom = assoc;

                switch (assoc)
                {
                    case ASSOCIATE_FROM.BERCONVERTER:
                        TempAssoc.BerConverterName = this.cmbAssocBer.Text;

                        break;

                    case ASSOCIATE_FROM.ENUM:
                        TempAssoc.EnumName = this.cmbAssocEnum.Text;

                        break;
                }

                MainBase.DynamicDll.AssociatorList.Add(TempAssoc.Name, TempAssoc);
            }

            else
            {
                GlobalControlHandler.ListViewRemoveItem(this.lvAssoc, TempAssoc.Name); 

                AssocChanged();
            }

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbAssoc }, false);

            this.lvAssoc.Enabled = true;

            TempAssoc = new DynamicAttributeAssociator(); TempAssoc = null;
        }

        #endregion

        #region enum 

        private void EnumChanged()
        {
            if ((this.lvEnums.SelectedItems.Count != 0) && (this.lvEnums.SelectedItems[0].Tag != null))
            {
                this.gbEnumValues.SetText(String.Format("Values ({0})", this.lvEnums.SelectedItems[0].Text));

                DisplayEnumValues((DynamicEnum)this.lvEnums.SelectedItems[0].Tag);
            }
        }

        private void DisplayEnumValues(DynamicEnum dynEnum)
        {
            this.lvValues.Items.Clear();

            TagInfo tag = new TagInfo() { Tag = 0, Name = dynEnum.Name };

            foreach (KeyValuePair<string, string> value in dynEnum.StringValues)
            {
                this.lvValues.Items.Add(new ListViewItem(new string[] { value.Key, value.Value }) { Tag = dynEnum.Name });
            }
        }

        private void AddEnum()
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvEnums, 1, false);

            GlobalControlHandler.ListViewEditItem(this.lvEnums, curpos, new Control[] { this.txtEnumName });
        }

        private void UpdateNewEnumInfo()
        {
            TempEnum = new DynamicEnum();
            TempEnum.Name = this.lvEnums.SelectedItems[0].Text;

            this.lvEnums.SelectedItems[0].Tag = TempEnum;

            this.lvEnums.SelectedItems[0].Name = TempEnum.Name;

            EnumChanged();

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbEnum, this.gbEnumValues }, true);

            this.lvEnums.Enabled = false;
        }

        private void SaveEnum(bool save = true)
        {
            if (save)
            { save = (this.lvEnums.Items.Count != 0); }

            if (save)
            {
                if (!MainBase.DynamicDll.DynamicEnumList.ContainsKey(TempEnum.Name))
                {
                    MainBase.DynamicDll.DynamicEnumList.Add(TempEnum.Name, TempEnum);

                    MainBase.DynamicDll.GenerateEnum(TempEnum);

                    UpdateEnumCombo(this.cmbAssocEnum.Text);

                    this.IsDirty = true;
                }
            }

            else
            {
                GlobalControlHandler.ListViewRemoveItem(this.lvEnums, TempEnum.Name);

                EnumChanged();
            }
            
            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbEnum, this.gbEnumValues }, false);

            this.lvEnums.Enabled = true;

            TempEnum = new DynamicEnum(); TempEnum = null;
        }

        private void AddValue(bool insert = false)
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvValues, 2, insert);

            GlobalControlHandler.ListViewEditItem(this.lvValues, curpos, new Control[] { this.txtValueName, this.txtValue });
        }

        private void UpdateValue()
        {
            int curpos = 0;

            if (this.lvValues.SelectedItems.Count != 0)
            {
                curpos = this.lvValues.SelectedIndices[0];

                GlobalControlHandler.ListViewEditItem(this.lvValues, curpos, new Control[] { this.txtValueName, this.txtValue }, true);
            }
        }

        private void UpdateValueInfo()
        {
            if (this.lvValues.Items.Count == 0)
            { return; }

            if (TempEnum == null)
            {
                string tag = (string)this.lvValues.Items[0].Tag;

                TempEnum = MainBase.DynamicDll.DynamicEnumList.Where(e => e.Key == tag).FirstOrDefault().Value;

                TempEnum.StringValues.Clear();

                TempEnum.Values.Clear();

                foreach (ListViewItem item in lvValues.Items)
                { TempEnum.AddEnumField(item.SubItems[0].Text, item.SubItems[1].Text); }
            }

            else 
            {
                foreach (ListViewItem item in lvValues.Items)
                { TempEnum.AddEnumField(item.SubItems[0].Text, item.SubItems[1].Text); }

                
            }

            TempEnum.IsDirty = true;

            IsDirty = true;

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbEnum, this.gbEnumValues }, true);
        }

        #endregion

        #region ber

        private void BerChanged()
        {
            if ((this.lvBer.SelectedItems.Count != 0) && (this.lvBer.SelectedItems[0].Tag != null))
            {
                this.gbFields.SetText(String.Format("Fields ({0})", this.lvBer.SelectedItems[0].Text));

                this.gbRule.SetText(String.Format("ConversionRule ({0})", this.lvBer.SelectedItems[0].Text));

                HandleIncludeTags = false;

                DisplayBerValues(this.lvBer.SelectedItems[0].Text, (int)this.lvBer.SelectedItems[0].Tag);

                HandleIncludeTags = true;
            }
        }

        private void DisplayBerValues(string berName, int pos)
        {
            this.lvBerValues.Items.Clear();
            this.txtRule.SetText(String.Empty);

            if (berName == null)
            { return; }

            DynamicBerConverter dynber = MainBase.DynamicDll.BerConverterList.Where(b => b.Key == berName).FirstOrDefault().Value;

            if (dynber == null)
            { return; }

            this.txtRule.SetText(dynber.ConversionRulesPrint[pos]);

            this.cbTags.Checked = dynber.IncludeTags;
            
            TagInfo tag = new TagInfo() { Tag = pos, Name = berName };

            foreach (KeyValuePair<int, string> fieldname in dynber.FieldNames[pos])
            {
                int id = fieldname.Key;

                Type fieldtype = dynber.FieldTypes[pos][id];

                this.lvBerValues.Items.Add(new ListViewItem(new string[] { fieldname.Value, fieldtype.FullName }) { Tag = tag });
            }

        }

        private void AddBer()
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvBer, 1, false);

            GlobalControlHandler.ListViewEditItem(this.lvBer, curpos, new Control[] { this.txtBerName });
        }

        private void UpdateNewBerInfo()
        {
            if (MainBase.DynamicDll.BerConverterList.ContainsKey(this.lvBer.SelectedItems[0].Text))
            {
                TempBer = MainBase.DynamicDll.BerConverterList[this.lvBer.SelectedItems[0].Text];

                this.lvBer.SelectedItems[0].Text = TempBer.Name;
            }

            else
            {
                TempBer = new DynamicBerConverter();
                TempBer.Name = this.lvBer.SelectedItems[0].Text;
            }

            int berpos = TempBer.SetConversionRule(String.Empty);

            this.lvBer.SelectedItems[0].Tag = berpos;

            this.lvBer.SelectedItems[0].Name = TempBer.Name;

            BerChanged();

            this.lvBer.Enabled = false;

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbConverter, this.gbRule, this.gbFields }, true);
        }

        private void SaveBer(bool save = true)
        {
            if (save)
            {
                if (TempBer.ConversionRules.Count == 1)
                { MainBase.DynamicDll.BerConverterList.Add(TempBer.Name, TempBer); }

                UpdateBerCombo(this.cmbAssocBer.Text);

                this.IsDirty = true;
            }

            else
            {
                int berpos = TempBer.ConversionRules.Count - 1;

                if (TempBer.ConversionRules.Count > 1)
                { TempBer.RemoveFromBerList(berpos); }

                GlobalControlHandler.ListViewRemoveItem(this.lvBer, TempBer.Name, berpos);

                BerChanged();
            }

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbConverter, this.gbRule, this.gbFields }, false);

            this.lvBer.Enabled = true;

            TempBer = new DynamicBerConverter(); TempBer = null;
        }

        private void DeleteBer(TagInfo tag)
        {
            if (MainBase.DynamicDll.BerConverterList.Where(b => b.Key == tag.Name) != null)
            {
                DynamicBerConverter dynber = MainBase.DynamicDll.BerConverterList.Where(b => b.Key == tag.Name).FirstOrDefault().Value;

                if (dynber.ConversionRules.Count == 1)
                { MainBase.DynamicDll.BerConverterList.Remove(tag.Name); }

                else
                {
                    int berpos = (int)tag.Tag;

                    dynber.ConversionRules.Remove(berpos);

                    dynber.ConversionRulesPrint.Remove(berpos);

                    dynber.FieldNames.Remove(berpos);

                    dynber.FieldTypes.Remove(berpos);
                }
            }
        }

        private void AddField(bool insert = false)
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvBerValues, 2, insert);

            GlobalControlHandler.ListViewEditItem(this.lvBerValues, curpos, new Control[] { this.txtFieldName, this.cmbTypes });
        }

        private void UpdateField()
        {
            int curpos = 0;

            if (this.lvBerValues.SelectedItems.Count != 0)
            {
                curpos = this.lvBerValues.SelectedIndices[0];

                GlobalControlHandler.ListViewEditItem(this.lvBerValues, curpos, new Control[] { this.txtFieldName, this.cmbTypes }, true);
            }
        }
        
        private void UpdateFieldInfo()
        {
            if (this.lvBerValues.Items.Count == 0)
            { return; }

            int berpos = 0;

            DynamicBerConverter dynber = TempBer;

            TagInfo tag;

            if (TempBer == null)
            {
                tag = (TagInfo)this.lvBerValues.Items[0].Tag;

                berpos = (int)tag.Tag;

                dynber = MainBase.DynamicDll.BerConverterList.Where(b => b.Key == tag.Name).FirstOrDefault().Value;
            }

            else
            {
                tag = new TagInfo() { Tag = dynber.ConversionRules.Count - 1, Name = dynber.Name };
            }

            Dictionary<int, string> tempfields = new Dictionary<int, string>();

            Dictionary<int, Type> temptypes = new Dictionary<int, Type>();

            List<string> lrule = new List<string>();
            
            int currentpos = 0;

            string tagchar = dynber.IncludeTags ? "i" : String.Empty;

            foreach (ListViewItem item in this.lvBerValues.Items)
            {
                if (item.SubItems[0].Text != null)
                {
                    item.SubItems[0].Tag = tag;

                    tempfields.Add(currentpos, item.SubItems[0].Text);

                    Type fieldtype = null;

                    if (!item.SubItems[1].Text.TryParseType(out fieldtype))
                    { fieldtype = typeof(int); }

                    temptypes.Add(currentpos, fieldtype);

                    if (fieldtype == typeof(int))
                    { lrule.Add(tagchar + "i"); }

                    else if (fieldtype == typeof(string))
                    { lrule.Add(tagchar + "a"); }

                    currentpos++;
                }
            }

            dynber.FieldNames[berpos] = tempfields;

            dynber.FieldTypes[berpos] = temptypes;

            dynber.ConversionRulesPrint[berpos] = String.Join(" ", lrule.ToArray());

            dynber.ConversionRules[berpos] = "{" + String.Join("", lrule.ToArray()) + "}";

            this.txtRule.SetText(dynber.ConversionRulesPrint[berpos]);

            dynber.IsDirty = true;

            this.IsDirty = true;
        }

        private void IncludeTagsChanged()
        {
            if (Loading) return;

            if (!HandleIncludeTags) return; 

            if (TempBer == null)
            {
                string bername = this.lvBer.SelectedItems[0].Text;

                DynamicBerConverter dynber = MainBase.DynamicDll.BerConverterList.Where(b => b.Key == bername).FirstOrDefault().Value;

                dynber.IncludeTags = this.cbTags.Checked;
            }

            else
            { TempBer.IncludeTags = this.cbTags.Checked; }
        }

        #endregion

        #region dict

        private void DictChanged()
        {
            if ((this.lvDict.SelectedItems.Count != 0) && (this.lvDict.SelectedItems[0].Tag != null))
            {
                this.gbKeyVal.SetText(String.Format("KeyValuePairs ({0})", this.lvDict.SelectedItems[0].Text));

                DisplayDictValues((Dictionary<string, string>)this.lvDict.SelectedItems[0].Tag, this.lvDict.SelectedItems[0].Text);
            }
        }

        private void DisplayDictValues(Dictionary<string, string> dynDict, string dictName)
        {
            this.lvKeyVal.Items.Clear();

            foreach (KeyValuePair<string, string> value in dynDict)
            {
                this.lvKeyVal.Items.Add(new ListViewItem(new string[] { value.Key, value.Value }) { Tag = dictName });
            }
        }

        private void AddDict()
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvDict, 1, false);

            GlobalControlHandler.ListViewEditItem(this.lvDict, curpos, new Control[] { this.txtDictName });
        }

        private void UpdateNewDictInfo()
        {
            TempDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            this.lvDict.SelectedItems[0].Tag = TempDict;

            this.lvDict.SelectedItems[0].Name = this.lvDict.SelectedItems[0].Text;

            DictChanged();

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbDicts, this.gbKeyVal }, true);

            this.lvDict.Enabled = false;
        }

        private void SaveDict(bool save = true)
        {
            if (save)
            { save = (this.lvDict.Items.Count != 0); }

            if (save)
            {
                MainBase.DynamicDll.DictionaryList.Add(this.lvDict.SelectedItems[0].Text, TempDict);                               

                UpdateDictCombo(this.cmbAssocDict.Text);

                this.IsDirty = true;
            }

            else
            {
                GlobalControlHandler.ListViewRemoveItem(this.lvDict, this.lvDict.SelectedItems[0].Text);

                DictChanged();
            }

            GlobalControlHandler.SetGroupBoxAlert(new GroupBox[] { this.gbDicts, this.gbKeyVal }, false);

            this.lvDict.Enabled = true;

            TempDict = new Dictionary<string, string>(); TempDict = null;
        }

        private void AddKeyValPair(bool insert = false)
        {
            int curpos = GlobalControlHandler.ListViewAddItem(this.lvKeyVal, 2, insert);

            GlobalControlHandler.ListViewEditItem(this.lvKeyVal, curpos, new Control[] { this.txtKeyName, this.txtValName });
        }

        private void UpdateKeyValPair()
        {
            int curpos = 0;

            if (this.lvKeyVal.SelectedItems.Count != 0)
            {
                curpos = this.lvKeyVal.SelectedIndices[0];

                GlobalControlHandler.ListViewEditItem(this.lvKeyVal, curpos, new Control[] { this.txtKeyName, this.txtValName }, true);
            }
        }

        private void UpdateKeyValPairInfo()
        {
            if (this.lvKeyVal.Items.Count == 0)
            { return; }

            if (TempDict == null)
            {
                TagInfo tag = (TagInfo)this.lvKeyVal.Items[0].Tag;

                Dictionary<string, string> dyndict = MainBase.DynamicDll.DictionaryList.Where(e => e.Key == tag.Name).FirstOrDefault().Value;

                dyndict.Clear();

                foreach (ListViewItem item in lvKeyVal.Items)
                { dyndict.Add(item.SubItems[0].Text, item.SubItems[1].Text); }
            }

            else
            {
                foreach (ListViewItem item in lvKeyVal.Items)
                { TempDict.Add(item.SubItems[0].Text, item.SubItems[1].Text); }
            }

            IsDirty = true;  
        }

        #endregion

        private void Save()
        {
            if (!IsDirty) return;

            MainBase.DynamicDll.Save();

            MainBase.DynamicDll = new DynamicTypeLoader();

            GetBackups();
        }

        #endregion

        #region controls

        #region assoc controls

        private void lvAssoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading) return;

            AssocChanged();
        }
        
        private void lvAttribs_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, Assoc_ContextMenu, new int[] { 0, 1 }); }

        private void Assoc_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(Assoc_ContextMenu, new int[] { 0 }); }

        private void Assoc_Add_MenuItem_Click(object sender, EventArgs e)
        { AddAssoc(); }

        private void Assoc_Delete_MenuItem_Click(object sender, EventArgs e)
        {
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            { 
                lvAssoc_SelectedIndexChanged(sender, e);

                MainBase.DynamicDll.AssociatorList.Remove(tag.Name);

                this.IsDirty = true;
            }
        }

        private void txtAssocName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtAssocName, null, e, out update);

            if (update)
            { UpdateNewAssocInfo(); }
        }

        private void cmdSaveAssocc_Click(object sender, EventArgs e)
        { SaveAssoc(); }

        private void cmdDiscardAssoc_Click(object sender, EventArgs e)
        { SaveAssoc(false); }

        private void gbAssoc_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }

        private void lvAssoc_ItemActivate(object sender, EventArgs e)
        { }

        #endregion

        #region enum controls

        private void lvEnums_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading) return;

            EnumChanged();
        }

        private void lvEnumsMouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, Enums_ContextMenu, new int[] { 0, 1 }); }

        private void Enums_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(Enums_ContextMenu, new int[] { 0 }); }
        
        private void Enum_Add_MenuItem_Click(object sender, EventArgs e)
        { AddEnum(); }

        private void Enum_Delete_MenuItem_Click(object sender, EventArgs e)
        {
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            { 
                lvEnums_SelectedIndexChanged(sender, e);

                MainBase.DynamicDll.EnumList.Remove(tag.Name);
                MainBase.DynamicDll.DynamicEnumList.Remove(tag.Name);

                this.IsDirty = true;
            }                      
        }

        private void txtEnumName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtEnumName, null, e, out update);

            if (update)
            { UpdateNewEnumInfo(); }
        }

        private void cmdSaveEnum_Click(object sender, EventArgs e)
        { SaveEnum(); }

        private void cmdDiscardEnum_Click(object sender, EventArgs e)
        { SaveEnum(false); }

        private void gbEnum_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }

        private void lvEnums_ItemActivate(object sender, EventArgs e)
        { }

        #endregion

        #region enum value controls

        private void lvValues_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, Value_ContextMenu, new int[] { 0, 2, 3, 5, 6, 8 }); }
        
        private void Value_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(Value_ContextMenu, new int[] { 6 }); }

        private void Value_Edit_MenuItem_Click(object sender, EventArgs e)
        { UpdateValue(); }
        
        private void Value_MoveUp_MenuItem_Click(object sender, EventArgs e)
        { 
            GlobalControlHandler.ListViewMoveItem(sender, -1);

            UpdateValueInfo();
        }

        private void Value_MoveDown_MenuItem_Click(object sender, EventArgs e)
        { 
            GlobalControlHandler.ListViewMoveItem(sender, 1);
            
            UpdateValueInfo();
        }

        private void Value_Insert_MenuItem_Click(object sender, EventArgs e)
        { AddValue(true); }

        private void Value_Append_MenuItem_Click(object sender, EventArgs e)
        { AddValue(); }
               
        private void Value_Delete_MenuItem_Click(object sender, EventArgs e)
        { 
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            { UpdateValueInfo(); }
        }
                
        private void txtValueName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtValueName, this.txtValue, e, out update);

            if (update)
            { UpdateValueInfo(); }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtValue, null, e, out update);

            if (update)
            { UpdateValueInfo(); }
        }

        private void gbEnumValues_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }
        
        private void lvValues_ItemActivate(object sender, EventArgs e)
        { }

        #endregion
      
        #region ber controls

        private void lvBer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading) return;

            BerChanged();
        }
        
        private void cbTags_CheckedChanged(object sender, EventArgs e)
        { IncludeTagsChanged(); }

        private void lvBer_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, Ber_ContextMenu, new int[] { 0, 1 }); }

        private void Ber_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(Ber_ContextMenu, new int[] { 0 }); }

        private void Ber_Add_MenuItem_Click(object sender, EventArgs e)
        { AddBer(); }

        private void Ber_Delete_MenuItem_Click(object sender, EventArgs e)
        {
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            { 
                lvBer_SelectedIndexChanged(sender, e);

                DeleteBer(tag);

                this.IsDirty = true;
            }           
        }
                
        private void txtBerName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtBerName, null, e, out update);

            if (update)
            { UpdateNewBerInfo(); }
        }

        private void cmdSaveBer_Click(object sender, EventArgs e)
        { SaveBer(); }

        private void cmdDiscardBer_Click(object sender, EventArgs e)
        { SaveBer(false); }

        private void gbConverter_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }

        private void gbRule_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }
        
        private void lvBer_ItemActivate(object sender, EventArgs e)
        { }

        #endregion

        #region ber field controls

        private void lvBerValues_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, Field_ContextMenu, new int[] { 0, 2, 3, 5, 6, 8 }); }
        
        private void Field_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(Field_ContextMenu, new int[] { 6 }); }
       
        private void Field_Edit_MenuItem_Click(object sender, EventArgs e)
        { UpdateField(); }
        
        private void Field_MoveUp_MenuItem_Click(object sender, EventArgs e)
        {
            GlobalControlHandler.ListViewMoveItem(sender, -1);

            UpdateFieldInfo();
        }

        private void Field_MoveDown_MenuItem_Click(object sender, EventArgs e)
        {
            GlobalControlHandler.ListViewMoveItem(sender, 1);

            UpdateFieldInfo();
        }

        private void Field_Insert_MenuItem_Click(object sender, EventArgs e)
        { AddField(true); }

        private void Field_Append_MenuItem_Click(object sender, EventArgs e)
        { AddField(); }
               
        private void Field_Delete_MenuItem_Click(object sender, EventArgs e)
        { 
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            { UpdateFieldInfo(); }
        }

        private void txtFieldName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtFieldName, this.cmbTypes, e, out update);

            if (update)
            { UpdateFieldInfo(); }
        }

        private void cmbTypes_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.cmbTypes, null, e, out update);

            if (update)
            { UpdateFieldInfo(); }
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbTypes.Tag == null)
            { return; }

            SubItemTag tag = (SubItemTag)this.cmbTypes.Tag;

            tag.SubItem.Text = this.cmbTypes.Text;

            if (tag.Controls == null)
            { this.cmbTypes.Visible = false; }

            else
            {
                foreach (Control ctrl in tag.Controls)
                { ctrl.Visible = false; }
            }

            UpdateFieldInfo();
        }

        private void gbFields_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }
        
        private void lvBerValues_ItemActivate(object sender, EventArgs e)
        { }

        #endregion

        #region dict controls

        private void lvDict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading) return;

            DictChanged();
        }
        
        private void lvDict_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, Dict_ContextMenu, new int[] { 0, 1 }); }

        private void Dict_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(Dict_ContextMenu, new int[] { 0 }); }

        private void Dict_Add_MenuItem_Click(object sender, EventArgs e)
        { AddDict(); }
        
        private void Dict_Delete_MenuItem_Click(object sender, EventArgs e)
        {
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            {
                lvDict_SelectedIndexChanged(sender, e);

                MainBase.DynamicDll.DictionaryList.Remove(tag.Name);

                this.IsDirty = true;
            }   
        }
        
        private void txtDictName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtDictName, null, e, out update);

            if (update)
            { UpdateNewDictInfo(); }
        }

        private void cmdSaveDict_Click(object sender, EventArgs e)
        { SaveDict(); }

        private void cmdDiscardDict_Click(object sender, EventArgs e)
        { SaveDict(false); }

        private void gbDicts_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }

        private void lvDict_ItemActivate(object sender, EventArgs e)
        { }

        #endregion

        #region keyval controls
        
        private void lvKeyVal_MouseClick(object sender, MouseEventArgs e)
        { GlobalControlHandler.ListViewRightMouseClick(sender, e, KeyVal_ContextMenu, new int[] { 0, 2, 3, 5, 6, 8 }); }

        private void KeyVal_ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        { GlobalControlHandler.ResetContextMenu(KeyVal_ContextMenu, new int[] { 6 }); }

        private void KeyVal_Edit_MenuItem_Click(object sender, EventArgs e)
        { UpdateKeyValPair(); }

        private void KeyVal_MoveUp_MenuItem_Click(object sender, EventArgs e)
        {
            GlobalControlHandler.ListViewMoveItem(sender, -1);

            UpdateKeyValPairInfo();
        }

        private void KeyVal_MoveDown_MenuItem_Click(object sender, EventArgs e)
        {
            GlobalControlHandler.ListViewMoveItem(sender, 1);

            UpdateKeyValPairInfo();
        }

        private void KeyVal_Insert_MenuItem_Click(object sender, EventArgs e)
        { AddKeyValPair(true); }

        private void KeyVal_Append_MenuItem_Click(object sender, EventArgs e)
        { AddKeyValPair(); }

        private void KeyVal_Delete_MenuItem_Click(object sender, EventArgs e)
        {
            TagInfo tag;

            if (GlobalControlHandler.ListViewDeleteItem(sender, out tag))
            { UpdateKeyValPairInfo(); }
        }
        
        private void txtKeyName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtKeyName, this.txtValName, e, out update);

            if (update)
            { UpdateKeyValPairInfo(); }
        }

        private void txtValName_KeyDown(object sender, KeyEventArgs e)
        {
            bool update = false;

            GlobalControlHandler.ListViewEditFieldKeyPress(this.txtValName, null, e, out update);

            if (update)
            { UpdateKeyValPairInfo(); }
        }

        private void gbKeyVal_Paint(object sender, PaintEventArgs e)
        { GlobalControlHandler.GroupBox_Paint(sender, e); }

        private void lvKeyVal_ItemActivate(object sender, EventArgs e)
        { }

        #endregion

        private void cmdSave_Click(object sender, EventArgs e)
        { Save(); }

        private void cmdClose_Click(object sender, EventArgs e)
        { this.Close(); }

        private void txtRule_KeyEvent(object sender, KeyEventArgs e)
        { GlobalControlHandler.KeySuppress(e); }
        
        #endregion               
    
        #region extension controls

        private void InitializeExtensionComponents()
        {
            #region assoc

            this.txtAssocName = new TextBoxExtension();
            this.gbAssoc.Controls.Add(this.txtAssocName);
             
            this.txtAssocName.Enabled = false;
            this.txtAssocName.Location = new System.Drawing.Point(14, 74);
            this.txtAssocName.Name = "txtAssocName";
            this.txtAssocName.Size = new System.Drawing.Size(220, 20);
            this.txtAssocName.TabIndex = 16;
            this.txtAssocName.Visible = false;
            this.txtAssocName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAssocName_KeyDown);
            this.txtAssocName.SuppressTab = true;

            #endregion

            #region enum

            this.txtEnumName = new TextBoxExtension();
            this.gbEnum.Controls.Add(this.txtEnumName);

            this.txtEnumName.Enabled = false;
            this.txtEnumName.Location = new System.Drawing.Point(6, 132);
            this.txtEnumName.Name = "txtEnumName";
            this.txtEnumName.Size = new System.Drawing.Size(220, 20);
            this.txtEnumName.TabIndex = 15;
            this.txtEnumName.Visible = false;
            this.txtEnumName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnumName_KeyDown);
            this.txtEnumName.SuppressTab = true;

            #endregion

            #region enumvalues

            this.txtValueName = new TextBoxExtension();
            this.gbEnumValues.Controls.Add(this.txtValueName);

            this.txtValueName.Enabled = false;
            this.txtValueName.Location = new System.Drawing.Point(17, 132);
            this.txtValueName.Name = "txtValueName";
            this.txtValueName.Size = new System.Drawing.Size(192, 20);
            this.txtValueName.TabIndex = 16;
            this.txtValueName.Visible = false;
            this.txtValueName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValueName_KeyDown);


            this.txtValue = new TextBoxExtension();
            this.gbEnumValues.Controls.Add(this.txtValue);

            this.txtValue.Enabled = false;
            this.txtValue.Location = new System.Drawing.Point(230, 132);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(54, 20);
            this.txtValue.TabIndex = 17;
            this.txtValue.Visible = false;
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            this.txtValue.SuppressTab = true;

            #endregion

            #region ber

            this.txtBerName = new TextBoxExtension();
            this.gbConverter.Controls.Add(this.txtBerName);

            this.txtBerName.Enabled = false;
            this.txtBerName.Location = new System.Drawing.Point(17, 136);
            this.txtBerName.Name = "txtBerName";
            this.txtBerName.Size = new System.Drawing.Size(228, 20);
            this.txtBerName.TabIndex = 15;
            this.txtBerName.Visible = false;
            this.txtBerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBerName_KeyDown);
            this.txtBerName.SuppressTab = true;

            #endregion

            #region berfields

            this.txtFieldName = new TextBoxExtension();
            this.gbFields.Controls.Add(this.txtFieldName);
            
            this.txtFieldName.Enabled = false;
            this.txtFieldName.Location = new System.Drawing.Point(15, 86);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(153, 20);
            this.txtFieldName.TabIndex = 16;
            this.txtFieldName.Visible = false;
            this.txtFieldName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFieldName_KeyDown);

            this.cmbTypes = new ComboBoxExtension();
            this.gbFields.Controls.Add(this.cmbTypes);
            
            this.cmbTypes.DropDownHeight = 40;
            this.cmbTypes.Enabled = false;
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.IntegralHeight = false;
            this.cmbTypes.Items.AddRange(new object[] {
            "System.Int32",
            "System.String"});
            this.cmbTypes.Location = new System.Drawing.Point(174, 85);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(109, 21);
            this.cmbTypes.TabIndex = 17;
            this.cmbTypes.Visible = false;
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTypes_SelectedIndexChanged);
            this.cmbTypes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypes_KeyDown);
            this.cmbTypes.SuppressTab = true;            

            #endregion

            #region dict

            this.txtDictName = new TextBoxExtension();
            this.gbDicts.Controls.Add(this.txtDictName);

            this.txtDictName.Enabled = false;
            this.txtDictName.Location = new System.Drawing.Point(174, 85);
            this.txtDictName.Name = "txtDictName";
            this.txtDictName.Size = new System.Drawing.Size(109, 21);
            this.txtDictName.TabIndex = 15;
            this.txtDictName.Visible = false;
            this.txtDictName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDictName_KeyDown);
            this.txtDictName.SuppressTab = true;

            #endregion

            #region keyvalpairs

            this.txtKeyName = new TextBoxExtension();
            this.gbKeyVal.Controls.Add(this.txtKeyName);

            this.txtKeyName.Enabled = false;
            this.txtKeyName.Location = new System.Drawing.Point(15, 86);
            this.txtKeyName.Name = "txtFieldName";
            this.txtKeyName.Size = new System.Drawing.Size(153, 20);
            this.txtKeyName.TabIndex = 16;
            this.txtKeyName.Visible = false;
            this.txtKeyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyName_KeyDown);

            this.txtValName = new TextBoxExtension();
            this.gbKeyVal.Controls.Add(this.txtValName);

            this.txtValName.Enabled = false;
            this.txtValName.Location = new System.Drawing.Point(15, 86);
            this.txtValName.Name = "txtValName";
            this.txtValName.Size = new System.Drawing.Size(153, 20);
            this.txtValName.TabIndex = 16;
            this.txtValName.Visible = false;
            this.txtValName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValName_KeyDown);
            this.txtValName.SuppressTab = true;

            #endregion
        }
        
        #endregion                      
        
        #region menu

        private void File_Default_MenuItem_Click(object sender, EventArgs e)
        { LoadDefault(); } 

        private void File_Save_MenuItem_Click(object sender, EventArgs e)
        { Save(); }

        private void File_Close_MenuItem_Click(object sender, EventArgs e)
        { this.Close(); }
        
        private void File_Restore_MenuItem_DropDownOpening(object sender, EventArgs e)
        { GetBackups(); }
        
        private void File_Restore_Picker_MenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Loading)
            {
                TagInfo bkptag = (TagInfo)this.File_Restore_Picker_MenuItem.SelectedItem;

                GlobalEventHandler.DoBackup += DoBackup;

                RestoreDialog rsd = new RestoreDialog(bkptag.Tag.ToString());

                rsd.SetDesktopLocation(this.Location.X + 20, this.Location.Y + 40);

                rsd.Show();
            }

            this.lvAssoc.Select();
        }

        private void cmbTypeAssoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            object dassoc = ASSOCIATE_FROM.NONE;

            this.cmbTypeAssoc.Text.EnumTryParse(typeof(ASSOCIATE_FROM), out dassoc);

            if ((ASSOCIATE_FROM)dassoc != ASSOCIATE_FROM.NONE)
            { this.tbTypes.SelectTab((int)dassoc); }

            this.cmbAssocEnum.Enabled = false;
            this.cmbAssocDict.Enabled = false;
            this.cmbAssocBer.Enabled = false;

            switch ((ASSOCIATE_FROM)dassoc)
            {
                case ASSOCIATE_FROM.ENUM:
                    this.cmbAssocEnum.Enabled = true;                    
                    break;

                case ASSOCIATE_FROM.DICTIONARY:
                    this.cmbAssocDict.Enabled = true;
                    break;

                case ASSOCIATE_FROM.BERCONVERTER:
                    this.cmbAssocBer.Enabled = true;
                    break;
            }
        }

        private void cmbAssocEnum_SelectedIndexChanged(object sender, EventArgs e)
        { Assoc_SelectedIndexChanged(sender, this.lvEnums); }

        private void cmbAssocDict_SelectedIndexChanged(object sender, EventArgs e)
        { Assoc_SelectedIndexChanged(sender, this.lvDict); }

        private void cmbAssocBer_SelectedIndexChanged(object sender, EventArgs e)
        { Assoc_SelectedIndexChanged(sender, this.lvBer); }

        private void Assoc_SelectedIndexChanged(object sender, ListView lv)
        {
            if (sender is ComboBox)
            {
                ComboBox cmb = (ComboBox)sender;

                string dynassoc = null;

                if (cmb.SelectedItem != null)
                { dynassoc = cmb.SelectedItem.ToString(); }

                if ((dynassoc != null) && (dynassoc != "NONE") && (dynassoc.Length > 0))
                {
                    ListViewItem[] items = lv.Items.Find(dynassoc, true);

                    if (items.Count() > 0)
                    {
                        lv.Select();
                        lv.Items[items[0].Index].Selected = true;
                    }

                }
            }
        }

        #endregion     
    }

    public class TagInfo
    {
        public object Tag;

        public string Name;

        public override string ToString()
        { return Name; }
    }
}
