namespace CodingFromTheField.LdapQueryAnalyzer
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            try
            { base.Dispose(disposing); }

            catch { };
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("EntryPoint");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Filter");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Attributes");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Scope");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.gbCon = new System.Windows.Forms.GroupBox();
            this.cmdDC = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbNCs = new System.Windows.Forms.ComboBox();
            this.CombosContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Combos_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Combos_CopyAll_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbDCs = new System.Windows.Forms.ComboBox();
            this.DCComboContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DCCombo_Info_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.DCCombo_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DCCombo_CopyAll_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbDomains = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSBase = new System.Windows.Forms.TextBox();
            this.BaseContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Base_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Base_Paste_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.Base_Browse_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.Base_Restore_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbQuery = new System.Windows.Forms.GroupBox();
            this.gbCancel = new System.Windows.Forms.GroupBox();
            this.cmdCancelAll = new System.Windows.Forms.Button();
            this.cmdCancelQuery = new System.Windows.Forms.Button();
            this.cmbHistory = new System.Windows.Forms.Button();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.txtSort = new System.Windows.Forms.TextBox();
            this.AttributesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Attributes_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Paste_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.Attributes_Clear_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Attributes_Load_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbReverse = new System.Windows.Forms.CheckBox();
            this.labSort = new System.Windows.Forms.Label();
            this.cmbAttribs = new System.Windows.Forms.ComboBox();
            this.labASQ = new System.Windows.Forms.Label();
            this.lvSort = new System.Windows.Forms.ListView();
            this.colReverse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAttrib = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AttributesSortContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AttributesSort_Remove_ContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AttributesSort_Clear_ContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.AttributesSort_Load_ContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.iSort = new System.Windows.Forms.ImageList(this.components);
            this.cbASQ = new System.Windows.Forms.CheckBox();
            this.txtASQAttribs = new System.Windows.Forms.TextBox();
            this.txtAttributes = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbSort = new System.Windows.Forms.CheckBox();
            this.lvFilterHistory = new System.Windows.Forms.ListView();
            this.colCheck = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFilter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAttribs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colASQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScope = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterHistoryContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FilterHistory_Base_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_Scope_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_ASQ_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_Sort_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_DC_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_Port_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_All_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FilterHistory_Bulk_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.FilterHistory_DeleteCurrent_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_DeleteSelected_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterHistory_Clear_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbBasetype = new System.Windows.Forms.GroupBox();
            this.cbRootDseExt = new System.Windows.Forms.CheckBox();
            this.rbDN = new System.Windows.Forms.RadioButton();
            this.rbRootDSE = new System.Windows.Forms.RadioButton();
            this.rbPhantomRoot = new System.Windows.Forms.RadioButton();
            this.cmdRestore = new System.Windows.Forms.Button();
            this.gbReferral = new System.Windows.Forms.GroupBox();
            this.cbRefExternal = new System.Windows.Forms.CheckBox();
            this.cbRefSubordinate = new System.Windows.Forms.CheckBox();
            this.rbRefNone = new System.Windows.Forms.RadioButton();
            this.lbHistory = new System.Windows.Forms.Label();
            this.gbScope = new System.Windows.Forms.GroupBox();
            this.rbOneLevel = new System.Windows.Forms.RadioButton();
            this.rbBase = new System.Windows.Forms.RadioButton();
            this.rbSubtree = new System.Windows.Forms.RadioButton();
            this.gbReturn = new System.Windows.Forms.GroupBox();
            this.cbFullStats = new System.Windows.Forms.CheckBox();
            this.cbResults = new System.Windows.Forms.CheckBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.FilterContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Filter_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Filter_Paste_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.Filter_Clear_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.Filter_Wizard_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbDC_GC = new System.Windows.Forms.GroupBox();
            this.PortsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Ports_SetLDAP_ContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Ports_SetLDAPValue_Text = new System.Windows.Forms.ToolStripTextBox();
            this.Ports_SetGC_ContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Port_SetGCValue_Text = new System.Windows.Forms.ToolStripTextBox();
            this.nuoPort = new System.Windows.Forms.NumericUpDown();
            this.rbCustomPort = new System.Windows.Forms.RadioButton();
            this.rbGC = new System.Windows.Forms.RadioButton();
            this.rbDC = new System.Windows.Forms.RadioButton();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.rbNonPagedQuery = new System.Windows.Forms.RadioButton();
            this.rbDirSync = new System.Windows.Forms.RadioButton();
            this.rbPagedQuery = new System.Windows.Forms.RadioButton();
            this.cmdLoadSync = new System.Windows.Forms.Button();
            this.lbDebugLog = new System.Windows.Forms.ListBox();
            this.DebugContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Debug_Copy_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.Debug_Clear_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.gbFind = new System.Windows.Forms.GroupBox();
            this.cmdCloseFind = new System.Windows.Forms.Button();
            this.cmdFind = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.gbResStore = new System.Windows.Forms.GroupBox();
            this.cmdClearResults = new System.Windows.Forms.Button();
            this.cmdLastResult = new System.Windows.Forms.Button();
            this.cmdFirstResult = new System.Windows.Forms.Button();
            this.cmdNextResult = new System.Windows.Forms.Button();
            this.cmdPrevResult = new System.Windows.Forms.Button();
            this.txtOutputOld = new System.Windows.Forms.TextBox();
            this.ResultsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Results_CopySelected_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_CopyAll_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.Result_Remembered_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Remembered_First_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Remembered_Previous_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Remembered_Next_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Remembered_Last_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Remembered_Clear_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.Results_Clear_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.Result_Search_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Search_String_MenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.Result_Search_Find_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Result_Search_FindNext_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.Result_Font_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbDebug = new System.Windows.Forms.GroupBox();
            this.gbProps = new System.Windows.Forms.GroupBox();
            this.AttributesListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AttributesList_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttributesList_CopyAll_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.AttributesList_Add_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AttributesList_Hide_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.lbAttribs = new System.Windows.Forms.ListBox();
            this.ListTimer = new System.Windows.Forms.Timer(this.components);
            this.MainMenue = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Close_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.User_Whoami_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.User_Elevate_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.User_Elevate_2nd_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.User_Restart_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.User_Restart_Elevate_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.User_Runas_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.User_Runas_Elevate_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Connection_Refresh_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.Connection_Ports_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ports_LDAP_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ports_LDAP_Value_Text = new System.Windows.Forms.ToolStripTextBox();
            this.Ports_GC_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ports_GC_Value_Text = new System.Windows.Forms.ToolStripTextBox();
            this.Connection_TimeOut_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Connection_TimeOutValue_MenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.Connection_ConnectForest_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Connection_PassCredentials_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Browse_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_Execute_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_Provider_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Provider_Combo_MenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.Query_BaseType_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_BaseType_Combo_MenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.Query_Type_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Type_Combo_MenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.Query_Scope_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Scope_Combo_MenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.Query_Return_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Return_Results_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Return_Statistics_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_Return_RootDSEExtended_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_Async_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_AutoPage_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_ValRange_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_ShowDeleted_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_ShowRecycled_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_Sort_Results_Asc_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Sort_Results_Desc_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.Query_ASQ_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Query_Sort_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Filter_Wizard_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Filter_ClearHistory_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttributesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Load_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Hide_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecodeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_GUIDs_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_SIDs_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_UserParams_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_ResolveSids_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_SD_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_OctetString_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_ReplicaLinks_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_PGID_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_TimeBias_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Decode_IgnoreEmpty_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_ResultsCount_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_MaxResultsCount_MenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.Results_ShowPartial_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.Results_CopySelected_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_CopyAll_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.Results_RemeberedCount_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_MaxResults_MenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.Results_Remembered_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_Remembered_First_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_Remembered_Prev_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_Remembered_Next_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_Remembered_Last_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Results_Remembered_Clear_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.Results_Clear_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.Font_Picker_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Tools_Builder_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.Sizer_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diaFont = new System.Windows.Forms.FontDialog();
            this.gbRunInfo = new System.Windows.Forms.GroupBox();
            this.tvSyncRuns = new System.Windows.Forms.TreeView();
            this.ilObjects = new System.Windows.Forms.ImageList(this.components);
            this.lvCurRun = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValues = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilDirSync = new System.Windows.Forms.ImageList(this.components);
            this.tvSync = new System.Windows.Forms.TreeView();
            this.gbCon.SuspendLayout();
            this.CombosContextMenu.SuspendLayout();
            this.DCComboContextMenu.SuspendLayout();
            this.BaseContextMenu.SuspendLayout();
            this.gbQuery.SuspendLayout();
            this.gbCancel.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.AttributesContextMenu.SuspendLayout();
            this.AttributesSortContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.FilterHistoryContextMenu.SuspendLayout();
            this.gbBasetype.SuspendLayout();
            this.gbReferral.SuspendLayout();
            this.gbScope.SuspendLayout();
            this.gbReturn.SuspendLayout();
            this.FilterContextMenu.SuspendLayout();
            this.gbDC_GC.SuspendLayout();
            this.PortsContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuoPort)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.DebugContextMenu.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.gbFind.SuspendLayout();
            this.gbResStore.SuspendLayout();
            this.ResultsContextMenu.SuspendLayout();
            this.gbDebug.SuspendLayout();
            this.gbProps.SuspendLayout();
            this.AttributesListContextMenu.SuspendLayout();
            this.MainMenue.SuspendLayout();
            this.gbRunInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCon
            // 
            this.gbCon.Controls.Add(this.cmdDC);
            this.gbCon.Controls.Add(this.label7);
            this.gbCon.Controls.Add(this.pbLoad);
            this.gbCon.Controls.Add(this.label3);
            this.gbCon.Controls.Add(this.label2);
            this.gbCon.Controls.Add(this.label1);
            this.gbCon.Controls.Add(this.cmbNCs);
            this.gbCon.Controls.Add(this.cmbDCs);
            this.gbCon.Controls.Add(this.cmbDomains);
            this.gbCon.Location = new System.Drawing.Point(3, 34);
            this.gbCon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbCon.Name = "gbCon";
            this.gbCon.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbCon.Size = new System.Drawing.Size(795, 143);
            this.gbCon.TabIndex = 0;
            this.gbCon.TabStop = false;
            this.gbCon.Text = "ConnectionInfo ()";
            // 
            // cmdDC
            // 
            this.cmdDC.Enabled = false;
            this.cmdDC.Location = new System.Drawing.Point(53, 55);
            this.cmdDC.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDC.Name = "cmdDC";
            this.cmdDC.Size = new System.Drawing.Size(25, 28);
            this.cmdDC.TabIndex = 201;
            this.cmdDC.Text = "ping";
            this.cmdDC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDC.UseVisualStyleBackColor = true;
            this.cmdDC.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Load";
            // 
            // pbLoad
            // 
            this.pbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLoad.Location = new System.Drawing.Point(80, 122);
            this.pbLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbLoad.MarqueeAnimationSpeed = 1;
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(707, 10);
            this.pbLoad.Step = 5;
            this.pbLoad.TabIndex = 200;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "NCs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "DCs";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Domains";
            // 
            // cmbNCs
            // 
            this.cmbNCs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNCs.ContextMenuStrip = this.CombosContextMenu;
            this.cmbNCs.FormattingEnabled = true;
            this.cmbNCs.Location = new System.Drawing.Point(80, 86);
            this.cmbNCs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbNCs.Name = "cmbNCs";
            this.cmbNCs.Size = new System.Drawing.Size(705, 24);
            this.cmbNCs.TabIndex = 2;
            this.cmbNCs.SelectedIndexChanged += new System.EventHandler(this.cmbNCs_SelectedIndexChanged);
            // 
            // CombosContextMenu
            // 
            this.CombosContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CombosContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Combos_Copy_ContextItem,
            this.Combos_CopyAll_ContextItem});
            this.CombosContextMenu.Name = "ContextMenuCombos";
            this.CombosContextMenu.Size = new System.Drawing.Size(180, 56);
            // 
            // Combos_Copy_ContextItem
            // 
            this.Combos_Copy_ContextItem.Name = "Combos_Copy_ContextItem";
            this.Combos_Copy_ContextItem.Size = new System.Drawing.Size(179, 26);
            this.Combos_Copy_ContextItem.Text = "Copy Selected";
            this.Combos_Copy_ContextItem.Click += new System.EventHandler(this.Combos_Copy_ContextItem_Click);
            // 
            // Combos_CopyAll_ContextItem
            // 
            this.Combos_CopyAll_ContextItem.Name = "Combos_CopyAll_ContextItem";
            this.Combos_CopyAll_ContextItem.Size = new System.Drawing.Size(179, 26);
            this.Combos_CopyAll_ContextItem.Text = "Copy All";
            this.Combos_CopyAll_ContextItem.Click += new System.EventHandler(this.Combos_CopyAll_ContextItem_Click);
            // 
            // cmbDCs
            // 
            this.cmbDCs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDCs.ContextMenuStrip = this.DCComboContextMenu;
            this.cmbDCs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDCs.FormattingEnabled = true;
            this.cmbDCs.Location = new System.Drawing.Point(80, 57);
            this.cmbDCs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbDCs.Name = "cmbDCs";
            this.cmbDCs.Size = new System.Drawing.Size(705, 24);
            this.cmbDCs.TabIndex = 1;
            this.cmbDCs.SelectedIndexChanged += new System.EventHandler(this.cmbDCs_SelectedIndexChanged);
            // 
            // DCComboContextMenu
            // 
            this.DCComboContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.DCComboContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DCCombo_Info_ContextItem,
            this.toolStripSeparator32,
            this.DCCombo_Copy_ContextItem,
            this.DCCombo_CopyAll_ContextItem});
            this.DCComboContextMenu.Name = "ContextMenuCombos";
            this.DCComboContextMenu.Size = new System.Drawing.Size(180, 88);
            // 
            // DCCombo_Info_ContextItem
            // 
            this.DCCombo_Info_ContextItem.Name = "DCCombo_Info_ContextItem";
            this.DCCombo_Info_ContextItem.Size = new System.Drawing.Size(181, 26);
            this.DCCombo_Info_ContextItem.Text = "Show DC Info";
            this.DCCombo_Info_ContextItem.Click += new System.EventHandler(this.DCCombo_Info_ContextItem_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(178, 6);
            // 
            // DCCombo_Copy_ContextItem
            // 
            this.DCCombo_Copy_ContextItem.Name = "DCCombo_Copy_ContextItem";
            this.DCCombo_Copy_ContextItem.Size = new System.Drawing.Size(181, 26);
            this.DCCombo_Copy_ContextItem.Text = "Copy Selected";
            this.DCCombo_Copy_ContextItem.Click += new System.EventHandler(this.Combos_Copy_ContextItem_Click);
            // 
            // DCCombo_CopyAll_ContextItem
            // 
            this.DCCombo_CopyAll_ContextItem.Name = "DCCombo_CopyAll_ContextItem";
            this.DCCombo_CopyAll_ContextItem.Size = new System.Drawing.Size(181, 26);
            this.DCCombo_CopyAll_ContextItem.Text = "Copy All";
            this.DCCombo_CopyAll_ContextItem.Click += new System.EventHandler(this.Combos_CopyAll_ContextItem_Click);
            // 
            // cmbDomains
            // 
            this.cmbDomains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDomains.ContextMenuStrip = this.CombosContextMenu;
            this.cmbDomains.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDomains.FormattingEnabled = true;
            this.cmbDomains.Location = new System.Drawing.Point(80, 25);
            this.cmbDomains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbDomains.Name = "cmbDomains";
            this.cmbDomains.Size = new System.Drawing.Size(705, 24);
            this.cmbDomains.TabIndex = 0;
            this.cmbDomains.SelectedIndexChanged += new System.EventHandler(this.cmbDomains_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Base";
            // 
            // txtSBase
            // 
            this.txtSBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSBase.ContextMenuStrip = this.BaseContextMenu;
            this.txtSBase.Location = new System.Drawing.Point(79, 21);
            this.txtSBase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSBase.Name = "txtSBase";
            this.txtSBase.Size = new System.Drawing.Size(889, 22);
            this.txtSBase.TabIndex = 0;
            this.txtSBase.TextChanged += new System.EventHandler(this.txtSBase_TextChanged);
            // 
            // BaseContextMenu
            // 
            this.BaseContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.BaseContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Base_Copy_ContextItem,
            this.Base_Paste_ContextItem,
            this.toolStripSeparator16,
            this.Base_Browse_ContextItem,
            this.toolStripSeparator26,
            this.Base_Restore_ContextItem});
            this.BaseContextMenu.Name = "BaseContextMenu";
            this.BaseContextMenu.Size = new System.Drawing.Size(135, 120);
            this.BaseContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.BaseContextMenu_Opening);
            // 
            // Base_Copy_ContextItem
            // 
            this.Base_Copy_ContextItem.Name = "Base_Copy_ContextItem";
            this.Base_Copy_ContextItem.Size = new System.Drawing.Size(134, 26);
            this.Base_Copy_ContextItem.Text = "Copy";
            this.Base_Copy_ContextItem.Click += new System.EventHandler(this.Base_Copy_ContextItem_Click);
            // 
            // Base_Paste_ContextItem
            // 
            this.Base_Paste_ContextItem.Name = "Base_Paste_ContextItem";
            this.Base_Paste_ContextItem.Size = new System.Drawing.Size(134, 26);
            this.Base_Paste_ContextItem.Text = "Paste";
            this.Base_Paste_ContextItem.Click += new System.EventHandler(this.Base_Paste_ContextItem_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(131, 6);
            // 
            // Base_Browse_ContextItem
            // 
            this.Base_Browse_ContextItem.Name = "Base_Browse_ContextItem";
            this.Base_Browse_ContextItem.Size = new System.Drawing.Size(134, 26);
            this.Base_Browse_ContextItem.Text = "Browse";
            this.Base_Browse_ContextItem.Click += new System.EventHandler(this.Base_Browse_ContextItem_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(131, 6);
            // 
            // Base_Restore_ContextItem
            // 
            this.Base_Restore_ContextItem.Name = "Base_Restore_ContextItem";
            this.Base_Restore_ContextItem.Size = new System.Drawing.Size(134, 26);
            this.Base_Restore_ContextItem.Text = "Restore";
            this.Base_Restore_ContextItem.Click += new System.EventHandler(this.Base_Restore_ContextItem_Click);
            // 
            // gbQuery
            // 
            this.gbQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbQuery.Controls.Add(this.gbCancel);
            this.gbQuery.Controls.Add(this.cmbHistory);
            this.gbQuery.Controls.Add(this.gbAttributes);
            this.gbQuery.Controls.Add(this.lvFilterHistory);
            this.gbQuery.Controls.Add(this.gbBasetype);
            this.gbQuery.Controls.Add(this.cmdRestore);
            this.gbQuery.Controls.Add(this.gbReferral);
            this.gbQuery.Controls.Add(this.lbHistory);
            this.gbQuery.Controls.Add(this.gbScope);
            this.gbQuery.Controls.Add(this.gbReturn);
            this.gbQuery.Controls.Add(this.cmdSearch);
            this.gbQuery.Controls.Add(this.label5);
            this.gbQuery.Controls.Add(this.txtFilter);
            this.gbQuery.Controls.Add(this.label4);
            this.gbQuery.Controls.Add(this.gbDC_GC);
            this.gbQuery.Controls.Add(this.txtSBase);
            this.gbQuery.Controls.Add(this.gbOptions);
            this.gbQuery.Location = new System.Drawing.Point(3, 178);
            this.gbQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbQuery.Size = new System.Drawing.Size(1228, 256);
            this.gbQuery.TabIndex = 1;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "Query";
            // 
            // gbCancel
            // 
            this.gbCancel.Controls.Add(this.cmdCancelAll);
            this.gbCancel.Controls.Add(this.cmdCancelQuery);
            this.gbCancel.Location = new System.Drawing.Point(867, 151);
            this.gbCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbCancel.Name = "gbCancel";
            this.gbCancel.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbCancel.Size = new System.Drawing.Size(100, 73);
            this.gbCancel.TabIndex = 7;
            this.gbCancel.TabStop = false;
            this.gbCancel.Text = "Cancel";
            this.gbCancel.Visible = false;
            // 
            // cmdCancelAll
            // 
            this.cmdCancelAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdCancelAll.Location = new System.Drawing.Point(7, 44);
            this.cmdCancelAll.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancelAll.Name = "cmdCancelAll";
            this.cmdCancelAll.Size = new System.Drawing.Size(87, 25);
            this.cmdCancelAll.TabIndex = 10;
            this.cmdCancelAll.TabStop = false;
            this.cmdCancelAll.Text = "All";
            this.cmdCancelAll.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancelAll.UseVisualStyleBackColor = true;
            this.cmdCancelAll.Click += new System.EventHandler(this.CmdCancelAll_Click);
            // 
            // cmdCancelQuery
            // 
            this.cmdCancelQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdCancelQuery.Location = new System.Drawing.Point(7, 16);
            this.cmdCancelQuery.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancelQuery.Name = "cmdCancelQuery";
            this.cmdCancelQuery.Size = new System.Drawing.Size(87, 25);
            this.cmdCancelQuery.TabIndex = 9;
            this.cmdCancelQuery.TabStop = false;
            this.cmdCancelQuery.Text = "Query";
            this.cmdCancelQuery.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancelQuery.UseVisualStyleBackColor = true;
            this.cmdCancelQuery.Click += new System.EventHandler(this.cmdCancelQuery_Click);
            // 
            // cmbHistory
            // 
            this.cmbHistory.BackColor = System.Drawing.SystemColors.Control;
            this.cmbHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbHistory.Location = new System.Drawing.Point(7, 121);
            this.cmbHistory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbHistory.Name = "cmbHistory";
            this.cmbHistory.Size = new System.Drawing.Size(70, 27);
            this.cmbHistory.TabIndex = 40;
            this.cmbHistory.Text = "History";
            this.cmbHistory.UseVisualStyleBackColor = false;
            this.cmbHistory.Click += new System.EventHandler(this.cmbHistory_Click);
            // 
            // gbAttributes
            // 
            this.gbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAttributes.Controls.Add(this.txtSort);
            this.gbAttributes.Controls.Add(this.cbReverse);
            this.gbAttributes.Controls.Add(this.labSort);
            this.gbAttributes.Controls.Add(this.cmbAttribs);
            this.gbAttributes.Controls.Add(this.labASQ);
            this.gbAttributes.Controls.Add(this.lvSort);
            this.gbAttributes.Controls.Add(this.cbASQ);
            this.gbAttributes.Controls.Add(this.txtASQAttribs);
            this.gbAttributes.Controls.Add(this.txtAttributes);
            this.gbAttributes.Controls.Add(this.panel1);
            this.gbAttributes.Location = new System.Drawing.Point(977, 12);
            this.gbAttributes.Margin = new System.Windows.Forms.Padding(4);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Padding = new System.Windows.Forms.Padding(4);
            this.gbAttributes.Size = new System.Drawing.Size(251, 240);
            this.gbAttributes.TabIndex = 39;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Attributes";
            // 
            // txtSort
            // 
            this.txtSort.ContextMenuStrip = this.AttributesContextMenu;
            this.txtSort.Enabled = false;
            this.txtSort.Location = new System.Drawing.Point(5, 185);
            this.txtSort.Margin = new System.Windows.Forms.Padding(4);
            this.txtSort.Name = "txtSort";
            this.txtSort.Size = new System.Drawing.Size(240, 22);
            this.txtSort.TabIndex = 35;
            this.txtSort.Visible = false;
            this.txtSort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSort_MouseDown);
            // 
            // AttributesContextMenu
            // 
            this.AttributesContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AttributesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Attributes_Copy_ContextItem,
            this.Attributes_Paste_ContextItem,
            this.toolStripSeparator18,
            this.Attributes_Clear_ContextItem,
            this.toolStripSeparator4,
            this.Attributes_Load_ContextItem});
            this.AttributesContextMenu.Name = "ContextMenuAttribs";
            this.AttributesContextMenu.Size = new System.Drawing.Size(213, 120);
            this.AttributesContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.AttributesContextMenu_Opening);
            // 
            // Attributes_Copy_ContextItem
            // 
            this.Attributes_Copy_ContextItem.Name = "Attributes_Copy_ContextItem";
            this.Attributes_Copy_ContextItem.Size = new System.Drawing.Size(212, 26);
            this.Attributes_Copy_ContextItem.Text = "Copy";
            this.Attributes_Copy_ContextItem.Click += new System.EventHandler(this.Attributes_Copy_ContextItem_Click);
            // 
            // Attributes_Paste_ContextItem
            // 
            this.Attributes_Paste_ContextItem.Name = "Attributes_Paste_ContextItem";
            this.Attributes_Paste_ContextItem.Size = new System.Drawing.Size(212, 26);
            this.Attributes_Paste_ContextItem.Text = "Paste";
            this.Attributes_Paste_ContextItem.Click += new System.EventHandler(this.Attributes_Paste_ContextItem_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(209, 6);
            // 
            // Attributes_Clear_ContextItem
            // 
            this.Attributes_Clear_ContextItem.Name = "Attributes_Clear_ContextItem";
            this.Attributes_Clear_ContextItem.Size = new System.Drawing.Size(212, 26);
            this.Attributes_Clear_ContextItem.Text = "Clear";
            this.Attributes_Clear_ContextItem.Click += new System.EventHandler(this.Attributes_Clear_ContextItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(209, 6);
            // 
            // Attributes_Load_ContextItem
            // 
            this.Attributes_Load_ContextItem.Name = "Attributes_Load_ContextItem";
            this.Attributes_Load_ContextItem.Size = new System.Drawing.Size(212, 26);
            this.Attributes_Load_ContextItem.Text = "Load Attributes List";
            this.Attributes_Load_ContextItem.Click += new System.EventHandler(this.Attributes_Load_MenuItem_Click);
            // 
            // cbReverse
            // 
            this.cbReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbReverse.AutoSize = true;
            this.cbReverse.Enabled = false;
            this.cbReverse.Location = new System.Drawing.Point(118, 162);
            this.cbReverse.Margin = new System.Windows.Forms.Padding(4);
            this.cbReverse.Name = "cbReverse";
            this.cbReverse.Size = new System.Drawing.Size(124, 21);
            this.cbReverse.TabIndex = 1;
            this.cbReverse.Text = "Reverse Order";
            this.cbReverse.UseVisualStyleBackColor = true;
            this.cbReverse.Visible = false;
            this.cbReverse.CheckedChanged += new System.EventHandler(this.cbReverse_CheckedChanged);
            // 
            // labSort
            // 
            this.labSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labSort.AutoSize = true;
            this.labSort.Location = new System.Drawing.Point(8, 164);
            this.labSort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSort.Name = "labSort";
            this.labSort.Size = new System.Drawing.Size(91, 17);
            this.labSort.TabIndex = 32;
            this.labSort.Text = "Sort Attribute";
            this.labSort.Visible = false;
            // 
            // cmbAttribs
            // 
            this.cmbAttribs.Enabled = false;
            this.cmbAttribs.FormattingEnabled = true;
            this.cmbAttribs.Location = new System.Drawing.Point(4, 21);
            this.cmbAttribs.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAttribs.Name = "cmbAttribs";
            this.cmbAttribs.Size = new System.Drawing.Size(241, 24);
            this.cmbAttribs.TabIndex = 34;
            this.cmbAttribs.Visible = false;
            this.cmbAttribs.SelectedIndexChanged += new System.EventHandler(this.cmbAttribs_SelectedIndexChanged);
            // 
            // labASQ
            // 
            this.labASQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labASQ.AutoSize = true;
            this.labASQ.Location = new System.Drawing.Point(8, 48);
            this.labASQ.Name = "labASQ";
            this.labASQ.Size = new System.Drawing.Size(101, 17);
            this.labASQ.TabIndex = 30;
            this.labASQ.Text = "ASQ Attributes";
            this.labASQ.Visible = false;
            // 
            // lvSort
            // 
            this.lvSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSort.CheckBoxes = true;
            this.lvSort.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colReverse,
            this.colAttrib});
            this.lvSort.ContextMenuStrip = this.AttributesSortContextMenu;
            this.lvSort.Enabled = false;
            this.lvSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSort.GridLines = true;
            this.lvSort.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem6.StateImageIndex = 0;
            this.lvSort.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6});
            this.lvSort.Location = new System.Drawing.Point(4, 158);
            this.lvSort.Margin = new System.Windows.Forms.Padding(4);
            this.lvSort.Name = "lvSort";
            this.lvSort.Scrollable = false;
            this.lvSort.Size = new System.Drawing.Size(241, 51);
            this.lvSort.SmallImageList = this.iSort;
            this.lvSort.TabIndex = 31;
            this.lvSort.UseCompatibleStateImageBehavior = false;
            this.lvSort.View = System.Windows.Forms.View.Details;
            this.lvSort.Visible = false;
            this.lvSort.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvSort_ItemChecked);
            this.lvSort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvSort_MouseClick);
            // 
            // colReverse
            // 
            this.colReverse.DisplayIndex = 1;
            this.colReverse.Text = "Rev";
            this.colReverse.Width = 37;
            // 
            // colAttrib
            // 
            this.colAttrib.DisplayIndex = 0;
            this.colAttrib.Text = "Attribute";
            this.colAttrib.Width = 140;
            // 
            // AttributesSortContextMenu
            // 
            this.AttributesSortContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AttributesSortContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttributesSort_Remove_ContextMenu,
            this.AttributesSort_Clear_ContextMenu,
            this.toolStripSeparator35,
            this.AttributesSort_Load_ContextMenu});
            this.AttributesSortContextMenu.Name = "ContextMenuAttribs";
            this.AttributesSortContextMenu.Size = new System.Drawing.Size(213, 88);
            // 
            // AttributesSort_Remove_ContextMenu
            // 
            this.AttributesSort_Remove_ContextMenu.Enabled = false;
            this.AttributesSort_Remove_ContextMenu.Name = "AttributesSort_Remove_ContextMenu";
            this.AttributesSort_Remove_ContextMenu.Size = new System.Drawing.Size(212, 26);
            this.AttributesSort_Remove_ContextMenu.Text = "Delete current";
            this.AttributesSort_Remove_ContextMenu.Visible = false;
            this.AttributesSort_Remove_ContextMenu.Click += new System.EventHandler(this.AttributesSort_Remove_ContextMenu_Click);
            // 
            // AttributesSort_Clear_ContextMenu
            // 
            this.AttributesSort_Clear_ContextMenu.Name = "AttributesSort_Clear_ContextMenu";
            this.AttributesSort_Clear_ContextMenu.Size = new System.Drawing.Size(212, 26);
            this.AttributesSort_Clear_ContextMenu.Text = "Clear";
            this.AttributesSort_Clear_ContextMenu.Click += new System.EventHandler(this.AttributesSort_Clear_ContextMenu_Click);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new System.Drawing.Size(209, 6);
            // 
            // AttributesSort_Load_ContextMenu
            // 
            this.AttributesSort_Load_ContextMenu.Name = "AttributesSort_Load_ContextMenu";
            this.AttributesSort_Load_ContextMenu.Size = new System.Drawing.Size(212, 26);
            this.AttributesSort_Load_ContextMenu.Text = "Load Attributes List";
            this.AttributesSort_Load_ContextMenu.Click += new System.EventHandler(this.AttributesSort_Load_ContextMenu_Click);
            // 
            // iSort
            // 
            this.iSort.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iSort.ImageSize = new System.Drawing.Size(14, 14);
            this.iSort.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbASQ
            // 
            this.cbASQ.AutoSize = true;
            this.cbASQ.Location = new System.Drawing.Point(5, 213);
            this.cbASQ.Margin = new System.Windows.Forms.Padding(4);
            this.cbASQ.Name = "cbASQ";
            this.cbASQ.Size = new System.Drawing.Size(131, 21);
            this.cbASQ.TabIndex = 10;
            this.cbASQ.Text = "AttributeScoped";
            this.cbASQ.UseVisualStyleBackColor = true;
            this.cbASQ.CheckedChanged += new System.EventHandler(this.cbASQ_CheckedChanged);
            // 
            // txtASQAttribs
            // 
            this.txtASQAttribs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtASQAttribs.ContextMenuStrip = this.AttributesContextMenu;
            this.txtASQAttribs.Enabled = false;
            this.txtASQAttribs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtASQAttribs.Location = new System.Drawing.Point(4, 68);
            this.txtASQAttribs.Margin = new System.Windows.Forms.Padding(4);
            this.txtASQAttribs.Multiline = true;
            this.txtASQAttribs.Name = "txtASQAttribs";
            this.txtASQAttribs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtASQAttribs.Size = new System.Drawing.Size(241, 141);
            this.txtASQAttribs.TabIndex = 4;
            this.txtASQAttribs.Visible = false;
            this.txtASQAttribs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtASQAttribs_MouseDown);
            // 
            // txtAttributes
            // 
            this.txtAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttributes.ContextMenuStrip = this.AttributesContextMenu;
            this.txtAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAttributes.Location = new System.Drawing.Point(4, 21);
            this.txtAttributes.Margin = new System.Windows.Forms.Padding(4);
            this.txtAttributes.Multiline = true;
            this.txtAttributes.Name = "txtAttributes";
            this.txtAttributes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAttributes.Size = new System.Drawing.Size(241, 187);
            this.txtAttributes.TabIndex = 3;
            this.txtAttributes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtAttributes_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cbSort);
            this.panel1.Location = new System.Drawing.Point(135, 193);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 42);
            this.panel1.TabIndex = 33;
            // 
            // cbSort
            // 
            this.cbSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSort.AutoSize = true;
            this.cbSort.Location = new System.Drawing.Point(31, 21);
            this.cbSort.Margin = new System.Windows.Forms.Padding(4);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(72, 21);
            this.cbSort.TabIndex = 0;
            this.cbSort.Text = "Sorted";
            this.cbSort.UseVisualStyleBackColor = true;
            this.cbSort.CheckedChanged += new System.EventHandler(this.cbSort_CheckedChanged);
            // 
            // lvFilterHistory
            // 
            this.lvFilterHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFilterHistory.CheckBoxes = true;
            this.lvFilterHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCheck,
            this.colFilter,
            this.colAttribs,
            this.colASQ,
            this.colSort,
            this.colBase,
            this.colDC,
            this.colScope,
            this.colPort});
            this.lvFilterHistory.ContextMenuStrip = this.FilterHistoryContextMenu;
            this.lvFilterHistory.FullRowSelect = true;
            this.lvFilterHistory.GridLines = true;
            this.lvFilterHistory.Location = new System.Drawing.Point(80, 54);
            this.lvFilterHistory.Margin = new System.Windows.Forms.Padding(4);
            this.lvFilterHistory.Name = "lvFilterHistory";
            this.lvFilterHistory.Size = new System.Drawing.Size(251, 38);
            this.lvFilterHistory.TabIndex = 16;
            this.lvFilterHistory.UseCompatibleStateImageBehavior = false;
            this.lvFilterHistory.View = System.Windows.Forms.View.Details;
            this.lvFilterHistory.Visible = false;
            this.lvFilterHistory.DoubleClick += new System.EventHandler(this.lvFilterHistory_DoubleClick);
            this.lvFilterHistory.Leave += new System.EventHandler(this.lvFilterHistory_Leave);
            this.lvFilterHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvFilterHistory_MouseClick);
            // 
            // colCheck
            // 
            this.colCheck.Text = "Bulk";
            this.colCheck.Width = 33;
            // 
            // colFilter
            // 
            this.colFilter.Text = "Filter";
            this.colFilter.Width = 25;
            // 
            // colAttribs
            // 
            this.colAttribs.Text = "Attributes";
            this.colAttribs.Width = 239;
            // 
            // colASQ
            // 
            this.colASQ.Text = "ASQ";
            this.colASQ.Width = 1;
            // 
            // colSort
            // 
            this.colSort.Text = "Sort";
            this.colSort.Width = 1;
            // 
            // colBase
            // 
            this.colBase.Text = "Base";
            this.colBase.Width = 1;
            // 
            // colDC
            // 
            this.colDC.Text = "DC";
            this.colDC.Width = 1;
            // 
            // colScope
            // 
            this.colScope.Text = "Scope";
            this.colScope.Width = 1;
            // 
            // colPort
            // 
            this.colPort.Text = "Port";
            this.colPort.Width = 1;
            // 
            // FilterHistoryContextMenu
            // 
            this.FilterHistoryContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.FilterHistoryContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterHistory_Base_ContextItem,
            this.FilterHistory_Scope_ContextItem,
            this.FilterHistory_ASQ_ContextItem,
            this.FilterHistory_Sort_ContextItem,
            this.FilterHistory_DC_ContextItem,
            this.FilterHistory_Port_ContextItem,
            this.FilterHistory_All_ContextItem,
            this.toolStripSeparator3,
            this.FilterHistory_Bulk_ContextItem,
            this.toolStripSeparator33,
            this.FilterHistory_DeleteCurrent_ContextItem,
            this.FilterHistory_DeleteSelected_ContextItem,
            this.FilterHistory_Clear_ContextItem});
            this.FilterHistoryContextMenu.Name = "ContextMenuFilterHistory";
            this.FilterHistoryContextMenu.Size = new System.Drawing.Size(203, 302);
            // 
            // FilterHistory_Base_ContextItem
            // 
            this.FilterHistory_Base_ContextItem.CheckOnClick = true;
            this.FilterHistory_Base_ContextItem.Name = "FilterHistory_Base_ContextItem";
            this.FilterHistory_Base_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_Base_ContextItem.Text = "Use SearchBase";
            this.FilterHistory_Base_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_Base_ContextItem_CheckedChanged);
            // 
            // FilterHistory_Scope_ContextItem
            // 
            this.FilterHistory_Scope_ContextItem.CheckOnClick = true;
            this.FilterHistory_Scope_ContextItem.Name = "FilterHistory_Scope_ContextItem";
            this.FilterHistory_Scope_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_Scope_ContextItem.Text = "User SearchScope";
            this.FilterHistory_Scope_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_Scope_ContextItem_CheckedChanged);
            // 
            // FilterHistory_ASQ_ContextItem
            // 
            this.FilterHistory_ASQ_ContextItem.CheckOnClick = true;
            this.FilterHistory_ASQ_ContextItem.Name = "FilterHistory_ASQ_ContextItem";
            this.FilterHistory_ASQ_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_ASQ_ContextItem.Text = "Use ASQ";
            this.FilterHistory_ASQ_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_ASQ_ContextItem_CheckedChanged);
            // 
            // FilterHistory_Sort_ContextItem
            // 
            this.FilterHistory_Sort_ContextItem.CheckOnClick = true;
            this.FilterHistory_Sort_ContextItem.Name = "FilterHistory_Sort_ContextItem";
            this.FilterHistory_Sort_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_Sort_ContextItem.Text = "Use Sorting";
            this.FilterHistory_Sort_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_Sort_ContextItem_CheckedChanged);
            // 
            // FilterHistory_DC_ContextItem
            // 
            this.FilterHistory_DC_ContextItem.CheckOnClick = true;
            this.FilterHistory_DC_ContextItem.Name = "FilterHistory_DC_ContextItem";
            this.FilterHistory_DC_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_DC_ContextItem.Text = "Use DC";
            this.FilterHistory_DC_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_DC_ContextItem_CheckedChanged);
            // 
            // FilterHistory_Port_ContextItem
            // 
            this.FilterHistory_Port_ContextItem.CheckOnClick = true;
            this.FilterHistory_Port_ContextItem.Name = "FilterHistory_Port_ContextItem";
            this.FilterHistory_Port_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_Port_ContextItem.Text = "Use Port";
            this.FilterHistory_Port_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_Port_ContextItem_CheckedChanged);
            // 
            // FilterHistory_All_ContextItem
            // 
            this.FilterHistory_All_ContextItem.CheckOnClick = true;
            this.FilterHistory_All_ContextItem.Name = "FilterHistory_All_ContextItem";
            this.FilterHistory_All_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_All_ContextItem.Text = "Use All";
            this.FilterHistory_All_ContextItem.CheckedChanged += new System.EventHandler(this.FilterHistory_All_ContextItem_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(199, 6);
            // 
            // FilterHistory_Bulk_ContextItem
            // 
            this.FilterHistory_Bulk_ContextItem.Name = "FilterHistory_Bulk_ContextItem";
            this.FilterHistory_Bulk_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_Bulk_ContextItem.Text = "Bulk Query";
            this.FilterHistory_Bulk_ContextItem.Click += new System.EventHandler(this.FilterHistory_Bulk_ContextItem_Click);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(199, 6);
            // 
            // FilterHistory_DeleteCurrent_ContextItem
            // 
            this.FilterHistory_DeleteCurrent_ContextItem.Name = "FilterHistory_DeleteCurrent_ContextItem";
            this.FilterHistory_DeleteCurrent_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_DeleteCurrent_ContextItem.Text = "Delete Current";
            this.FilterHistory_DeleteCurrent_ContextItem.Click += new System.EventHandler(this.FilterHistory_DeleteCurrent_ContextItem_Click);
            // 
            // FilterHistory_DeleteSelected_ContextItem
            // 
            this.FilterHistory_DeleteSelected_ContextItem.Name = "FilterHistory_DeleteSelected_ContextItem";
            this.FilterHistory_DeleteSelected_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_DeleteSelected_ContextItem.Text = "Delete Selected";
            this.FilterHistory_DeleteSelected_ContextItem.Click += new System.EventHandler(this.FilterHistory_DeleteSelected_ContextItem_Click);
            // 
            // FilterHistory_Clear_ContextItem
            // 
            this.FilterHistory_Clear_ContextItem.Name = "FilterHistory_Clear_ContextItem";
            this.FilterHistory_Clear_ContextItem.Size = new System.Drawing.Size(202, 26);
            this.FilterHistory_Clear_ContextItem.Text = "Clear History";
            this.FilterHistory_Clear_ContextItem.Click += new System.EventHandler(this.Filter_ClearHistory_MenuItem_Click);
            // 
            // gbBasetype
            // 
            this.gbBasetype.Controls.Add(this.cbRootDseExt);
            this.gbBasetype.Controls.Add(this.rbDN);
            this.gbBasetype.Controls.Add(this.rbRootDSE);
            this.gbBasetype.Controls.Add(this.rbPhantomRoot);
            this.gbBasetype.Location = new System.Drawing.Point(183, 151);
            this.gbBasetype.Margin = new System.Windows.Forms.Padding(4);
            this.gbBasetype.Name = "gbBasetype";
            this.gbBasetype.Padding = new System.Windows.Forms.Padding(4);
            this.gbBasetype.Size = new System.Drawing.Size(275, 101);
            this.gbBasetype.TabIndex = 38;
            this.gbBasetype.TabStop = false;
            this.gbBasetype.Text = "Base type";
            // 
            // cbRootDseExt
            // 
            this.cbRootDseExt.AutoSize = true;
            this.cbRootDseExt.Location = new System.Drawing.Point(105, 73);
            this.cbRootDseExt.Margin = new System.Windows.Forms.Padding(4);
            this.cbRootDseExt.Name = "cbRootDseExt";
            this.cbRootDseExt.Size = new System.Drawing.Size(164, 21);
            this.cbRootDseExt.TabIndex = 6;
            this.cbRootDseExt.Text = "operational attributes";
            this.cbRootDseExt.UseVisualStyleBackColor = true;
            this.cbRootDseExt.CheckedChanged += new System.EventHandler(this.cbRootDseExt_CheckedChanged);
            // 
            // rbDN
            // 
            this.rbDN.AutoSize = true;
            this.rbDN.Checked = true;
            this.rbDN.Location = new System.Drawing.Point(13, 21);
            this.rbDN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDN.Name = "rbDN";
            this.rbDN.Size = new System.Drawing.Size(49, 21);
            this.rbDN.TabIndex = 5;
            this.rbDN.TabStop = true;
            this.rbDN.Text = "DN";
            this.rbDN.UseVisualStyleBackColor = true;
            this.rbDN.CheckedChanged += new System.EventHandler(this.rbDN_CheckedChanged);
            // 
            // rbRootDSE
            // 
            this.rbRootDSE.AutoSize = true;
            this.rbRootDSE.Location = new System.Drawing.Point(12, 73);
            this.rbRootDSE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbRootDSE.Name = "rbRootDSE";
            this.rbRootDSE.Size = new System.Drawing.Size(82, 21);
            this.rbRootDSE.TabIndex = 4;
            this.rbRootDSE.Text = "rootDSE";
            this.rbRootDSE.UseVisualStyleBackColor = true;
            this.rbRootDSE.CheckedChanged += new System.EventHandler(this.rbRootDSE_CheckedChanged);
            // 
            // rbPhantomRoot
            // 
            this.rbPhantomRoot.AutoSize = true;
            this.rbPhantomRoot.Location = new System.Drawing.Point(12, 47);
            this.rbPhantomRoot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbPhantomRoot.Name = "rbPhantomRoot";
            this.rbPhantomRoot.Size = new System.Drawing.Size(115, 21);
            this.rbPhantomRoot.TabIndex = 0;
            this.rbPhantomRoot.Text = "PhantomRoot";
            this.rbPhantomRoot.UseVisualStyleBackColor = true;
            this.rbPhantomRoot.CheckedChanged += new System.EventHandler(this.rbPhantomRoot_CheckedChanged);
            // 
            // cmdRestore
            // 
            this.cmdRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRestore.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRestore.Enabled = false;
            this.cmdRestore.ForeColor = System.Drawing.Color.Teal;
            this.cmdRestore.Location = new System.Drawing.Point(7, 20);
            this.cmdRestore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(70, 27);
            this.cmdRestore.TabIndex = 37;
            this.cmdRestore.Text = "Restore";
            this.cmdRestore.UseVisualStyleBackColor = false;
            this.cmdRestore.Visible = false;
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // gbReferral
            // 
            this.gbReferral.Controls.Add(this.cbRefExternal);
            this.gbReferral.Controls.Add(this.cbRefSubordinate);
            this.gbReferral.Controls.Add(this.rbRefNone);
            this.gbReferral.Location = new System.Drawing.Point(573, 151);
            this.gbReferral.Margin = new System.Windows.Forms.Padding(4);
            this.gbReferral.Name = "gbReferral";
            this.gbReferral.Padding = new System.Windows.Forms.Padding(4);
            this.gbReferral.Size = new System.Drawing.Size(127, 101);
            this.gbReferral.TabIndex = 5;
            this.gbReferral.TabStop = false;
            this.gbReferral.Text = "Referrals";
            // 
            // cbRefExternal
            // 
            this.cbRefExternal.AutoSize = true;
            this.cbRefExternal.Location = new System.Drawing.Point(12, 73);
            this.cbRefExternal.Margin = new System.Windows.Forms.Padding(4);
            this.cbRefExternal.Name = "cbRefExternal";
            this.cbRefExternal.Size = new System.Drawing.Size(81, 21);
            this.cbRefExternal.TabIndex = 5;
            this.cbRefExternal.Text = "External";
            this.cbRefExternal.UseVisualStyleBackColor = true;
            this.cbRefExternal.CheckedChanged += new System.EventHandler(this.cbRefExternal_CheckedChanged);
            // 
            // cbRefSubordinate
            // 
            this.cbRefSubordinate.AutoSize = true;
            this.cbRefSubordinate.Location = new System.Drawing.Point(12, 47);
            this.cbRefSubordinate.Margin = new System.Windows.Forms.Padding(4);
            this.cbRefSubordinate.Name = "cbRefSubordinate";
            this.cbRefSubordinate.Size = new System.Drawing.Size(107, 21);
            this.cbRefSubordinate.TabIndex = 4;
            this.cbRefSubordinate.Text = "Subordinate";
            this.cbRefSubordinate.UseVisualStyleBackColor = true;
            this.cbRefSubordinate.CheckedChanged += new System.EventHandler(this.cbRefSubordinate_CheckedChanged);
            // 
            // rbRefNone
            // 
            this.rbRefNone.AutoSize = true;
            this.rbRefNone.Checked = true;
            this.rbRefNone.Location = new System.Drawing.Point(12, 21);
            this.rbRefNone.Margin = new System.Windows.Forms.Padding(4);
            this.rbRefNone.Name = "rbRefNone";
            this.rbRefNone.Size = new System.Drawing.Size(63, 21);
            this.rbRefNone.TabIndex = 2;
            this.rbRefNone.TabStop = true;
            this.rbRefNone.Text = "None";
            this.rbRefNone.UseVisualStyleBackColor = true;
            this.rbRefNone.CheckedChanged += new System.EventHandler(this.rbRefNone_CheckedChanged);
            // 
            // lbHistory
            // 
            this.lbHistory.AutoSize = true;
            this.lbHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbHistory.Location = new System.Drawing.Point(9, 126);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.Size = new System.Drawing.Size(52, 17);
            this.lbHistory.TabIndex = 34;
            this.lbHistory.Text = "History";
            // 
            // gbScope
            // 
            this.gbScope.Controls.Add(this.rbOneLevel);
            this.gbScope.Controls.Add(this.rbBase);
            this.gbScope.Controls.Add(this.rbSubtree);
            this.gbScope.Location = new System.Drawing.Point(460, 151);
            this.gbScope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbScope.Name = "gbScope";
            this.gbScope.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbScope.Size = new System.Drawing.Size(111, 101);
            this.gbScope.TabIndex = 4;
            this.gbScope.TabStop = false;
            this.gbScope.Text = "Scope";
            // 
            // rbOneLevel
            // 
            this.rbOneLevel.AutoSize = true;
            this.rbOneLevel.Location = new System.Drawing.Point(12, 47);
            this.rbOneLevel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbOneLevel.Name = "rbOneLevel";
            this.rbOneLevel.Size = new System.Drawing.Size(90, 21);
            this.rbOneLevel.TabIndex = 2;
            this.rbOneLevel.Text = "OneLevel";
            this.rbOneLevel.UseVisualStyleBackColor = true;
            this.rbOneLevel.CheckedChanged += new System.EventHandler(this.rbOneLevel_CheckedChanged);
            // 
            // rbBase
            // 
            this.rbBase.AutoSize = true;
            this.rbBase.Location = new System.Drawing.Point(12, 21);
            this.rbBase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbBase.Name = "rbBase";
            this.rbBase.Size = new System.Drawing.Size(61, 21);
            this.rbBase.TabIndex = 1;
            this.rbBase.Text = "Base";
            this.rbBase.UseVisualStyleBackColor = true;
            this.rbBase.CheckedChanged += new System.EventHandler(this.rbBase_CheckedChanged);
            // 
            // rbSubtree
            // 
            this.rbSubtree.AutoSize = true;
            this.rbSubtree.Checked = true;
            this.rbSubtree.Location = new System.Drawing.Point(12, 73);
            this.rbSubtree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSubtree.Name = "rbSubtree";
            this.rbSubtree.Size = new System.Drawing.Size(79, 21);
            this.rbSubtree.TabIndex = 3;
            this.rbSubtree.TabStop = true;
            this.rbSubtree.Text = "Subtree";
            this.rbSubtree.UseVisualStyleBackColor = true;
            this.rbSubtree.CheckedChanged += new System.EventHandler(this.rbSubtree_CheckedChanged);
            // 
            // gbReturn
            // 
            this.gbReturn.Controls.Add(this.cbFullStats);
            this.gbReturn.Controls.Add(this.cbResults);
            this.gbReturn.Location = new System.Drawing.Point(867, 151);
            this.gbReturn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbReturn.Name = "gbReturn";
            this.gbReturn.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbReturn.Size = new System.Drawing.Size(100, 73);
            this.gbReturn.TabIndex = 6;
            this.gbReturn.TabStop = false;
            this.gbReturn.Text = "Return";
            // 
            // cbFullStats
            // 
            this.cbFullStats.AutoSize = true;
            this.cbFullStats.Location = new System.Drawing.Point(12, 47);
            this.cbFullStats.Margin = new System.Windows.Forms.Padding(4);
            this.cbFullStats.Name = "cbFullStats";
            this.cbFullStats.Size = new System.Drawing.Size(86, 21);
            this.cbFullStats.TabIndex = 1;
            this.cbFullStats.Text = "Statistics";
            this.cbFullStats.UseVisualStyleBackColor = true;
            this.cbFullStats.CheckedChanged += new System.EventHandler(this.cbFullStats_CheckedChanged);
            // 
            // cbResults
            // 
            this.cbResults.AutoSize = true;
            this.cbResults.Checked = true;
            this.cbResults.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbResults.Location = new System.Drawing.Point(12, 21);
            this.cbResults.Margin = new System.Windows.Forms.Padding(4);
            this.cbResults.Name = "cbResults";
            this.cbResults.Size = new System.Drawing.Size(77, 21);
            this.cbResults.TabIndex = 0;
            this.cbResults.Text = "Results";
            this.cbResults.UseVisualStyleBackColor = true;
            this.cbResults.CheckedChanged += new System.EventHandler(this.cbResults_CheckedChanged);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdSearch.Location = new System.Drawing.Point(867, 225);
            this.cmdSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(100, 26);
            this.cmdSearch.TabIndex = 7;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Filter";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.ContextMenuStrip = this.FilterContextMenu;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(79, 54);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFilter.MaxLength = 512000;
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFilter.Size = new System.Drawing.Size(889, 91);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            // 
            // FilterContextMenu
            // 
            this.FilterContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.FilterContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Filter_Copy_ContextItem,
            this.Filter_Paste_ContextItem,
            this.toolStripSeparator19,
            this.Filter_Clear_MenuItem,
            this.toolStripSeparator10,
            this.Filter_Wizard_ContextItem});
            this.FilterContextMenu.Name = "ContextMenuFilter";
            this.FilterContextMenu.Size = new System.Drawing.Size(169, 120);
            this.FilterContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FilterContextMenu_Opening);
            // 
            // Filter_Copy_ContextItem
            // 
            this.Filter_Copy_ContextItem.Name = "Filter_Copy_ContextItem";
            this.Filter_Copy_ContextItem.Size = new System.Drawing.Size(168, 26);
            this.Filter_Copy_ContextItem.Text = "Copy";
            this.Filter_Copy_ContextItem.Click += new System.EventHandler(this.Filter_Copy_ContextItem_Click);
            // 
            // Filter_Paste_ContextItem
            // 
            this.Filter_Paste_ContextItem.Name = "Filter_Paste_ContextItem";
            this.Filter_Paste_ContextItem.Size = new System.Drawing.Size(168, 26);
            this.Filter_Paste_ContextItem.Text = "Paste";
            this.Filter_Paste_ContextItem.Click += new System.EventHandler(this.Filter_Paste_ContextItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(165, 6);
            // 
            // Filter_Clear_MenuItem
            // 
            this.Filter_Clear_MenuItem.Name = "Filter_Clear_MenuItem";
            this.Filter_Clear_MenuItem.Size = new System.Drawing.Size(168, 26);
            this.Filter_Clear_MenuItem.Text = "Clear";
            this.Filter_Clear_MenuItem.Click += new System.EventHandler(this.Filter_Clear_MenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(165, 6);
            // 
            // Filter_Wizard_ContextItem
            // 
            this.Filter_Wizard_ContextItem.Name = "Filter_Wizard_ContextItem";
            this.Filter_Wizard_ContextItem.Size = new System.Drawing.Size(168, 26);
            this.Filter_Wizard_ContextItem.Text = "Filter Wizard";
            this.Filter_Wizard_ContextItem.Click += new System.EventHandler(this.Filter_Wizard_MenuItem_Click);
            // 
            // gbDC_GC
            // 
            this.gbDC_GC.ContextMenuStrip = this.PortsContextMenu;
            this.gbDC_GC.Controls.Add(this.nuoPort);
            this.gbDC_GC.Controls.Add(this.rbCustomPort);
            this.gbDC_GC.Controls.Add(this.rbGC);
            this.gbDC_GC.Controls.Add(this.rbDC);
            this.gbDC_GC.Location = new System.Drawing.Point(5, 151);
            this.gbDC_GC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDC_GC.Name = "gbDC_GC";
            this.gbDC_GC.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDC_GC.Size = new System.Drawing.Size(175, 101);
            this.gbDC_GC.TabIndex = 3;
            this.gbDC_GC.TabStop = false;
            this.gbDC_GC.Text = "Port";
            // 
            // PortsContextMenu
            // 
            this.PortsContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PortsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ports_SetLDAP_ContextMenu,
            this.Ports_SetGC_ContextMenu});
            this.PortsContextMenu.Name = "PortsContextMenu";
            this.PortsContextMenu.Size = new System.Drawing.Size(200, 56);
            // 
            // Ports_SetLDAP_ContextMenu
            // 
            this.Ports_SetLDAP_ContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ports_SetLDAPValue_Text});
            this.Ports_SetLDAP_ContextMenu.Name = "Ports_SetLDAP_ContextMenu";
            this.Ports_SetLDAP_ContextMenu.Size = new System.Drawing.Size(199, 26);
            this.Ports_SetLDAP_ContextMenu.Tag = "true";
            this.Ports_SetLDAP_ContextMenu.Text = "Define LDAP Port";
            // 
            // Ports_SetLDAPValue_Text
            // 
            this.Ports_SetLDAPValue_Text.Name = "Ports_SetLDAPValue_Text";
            this.Ports_SetLDAPValue_Text.Size = new System.Drawing.Size(100, 27);
            this.Ports_SetLDAPValue_Text.Tag = "true";
            this.Ports_SetLDAPValue_Text.Text = "389";
            this.Ports_SetLDAPValue_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ports_SetLDAPValue_Text_KeyPress);
            // 
            // Ports_SetGC_ContextMenu
            // 
            this.Ports_SetGC_ContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Port_SetGCValue_Text});
            this.Ports_SetGC_ContextMenu.Name = "Ports_SetGC_ContextMenu";
            this.Ports_SetGC_ContextMenu.Size = new System.Drawing.Size(199, 26);
            this.Ports_SetGC_ContextMenu.Tag = "false";
            this.Ports_SetGC_ContextMenu.Text = "Define GC Port";
            // 
            // Port_SetGCValue_Text
            // 
            this.Port_SetGCValue_Text.Name = "Port_SetGCValue_Text";
            this.Port_SetGCValue_Text.Size = new System.Drawing.Size(100, 27);
            this.Port_SetGCValue_Text.Tag = "false";
            this.Port_SetGCValue_Text.Text = "3268";
            this.Port_SetGCValue_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Port_SetGCValue_Text_KeyPress);
            // 
            // nuoPort
            // 
            this.nuoPort.Enabled = false;
            this.nuoPort.Location = new System.Drawing.Point(99, 71);
            this.nuoPort.Margin = new System.Windows.Forms.Padding(4);
            this.nuoPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.nuoPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuoPort.Name = "nuoPort";
            this.nuoPort.Size = new System.Drawing.Size(71, 22);
            this.nuoPort.TabIndex = 3;
            this.nuoPort.Value = new decimal(new int[] {
            389,
            0,
            0,
            0});
            this.nuoPort.ValueChanged += new System.EventHandler(this.nuoPort_ValueChanged);
            // 
            // rbCustomPort
            // 
            this.rbCustomPort.AutoSize = true;
            this.rbCustomPort.Location = new System.Drawing.Point(12, 73);
            this.rbCustomPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbCustomPort.Name = "rbCustomPort";
            this.rbCustomPort.Size = new System.Drawing.Size(80, 21);
            this.rbCustomPort.TabIndex = 2;
            this.rbCustomPort.Text = "Custom:";
            this.rbCustomPort.UseVisualStyleBackColor = true;
            this.rbCustomPort.CheckedChanged += new System.EventHandler(this.rbCustomPort_CheckedChanged);
            // 
            // rbGC
            // 
            this.rbGC.AutoSize = true;
            this.rbGC.Location = new System.Drawing.Point(12, 47);
            this.rbGC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbGC.Name = "rbGC";
            this.rbGC.Size = new System.Drawing.Size(49, 21);
            this.rbGC.TabIndex = 1;
            this.rbGC.Text = "GC";
            this.rbGC.UseVisualStyleBackColor = true;
            this.rbGC.CheckedChanged += new System.EventHandler(this.rbGC_CheckedChanged);
            // 
            // rbDC
            // 
            this.rbDC.AutoSize = true;
            this.rbDC.Checked = true;
            this.rbDC.Location = new System.Drawing.Point(12, 21);
            this.rbDC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDC.Name = "rbDC";
            this.rbDC.Size = new System.Drawing.Size(65, 21);
            this.rbDC.TabIndex = 0;
            this.rbDC.TabStop = true;
            this.rbDC.Text = "LDAP";
            this.rbDC.UseVisualStyleBackColor = true;
            this.rbDC.CheckedChanged += new System.EventHandler(this.rbDC_CheckedChanged);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.rbNonPagedQuery);
            this.gbOptions.Controls.Add(this.rbDirSync);
            this.gbOptions.Controls.Add(this.rbPagedQuery);
            this.gbOptions.Controls.Add(this.cmdLoadSync);
            this.gbOptions.Location = new System.Drawing.Point(703, 151);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gbOptions.Size = new System.Drawing.Size(161, 101);
            this.gbOptions.TabIndex = 36;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Query type";
            // 
            // rbNonPagedQuery
            // 
            this.rbNonPagedQuery.AutoSize = true;
            this.rbNonPagedQuery.Checked = true;
            this.rbNonPagedQuery.Location = new System.Drawing.Point(12, 47);
            this.rbNonPagedQuery.Margin = new System.Windows.Forms.Padding(4);
            this.rbNonPagedQuery.Name = "rbNonPagedQuery";
            this.rbNonPagedQuery.Size = new System.Drawing.Size(101, 21);
            this.rbNonPagedQuery.TabIndex = 9;
            this.rbNonPagedQuery.TabStop = true;
            this.rbNonPagedQuery.Text = "Non-Paged";
            this.rbNonPagedQuery.UseVisualStyleBackColor = true;
            // 
            // rbDirSync
            // 
            this.rbDirSync.AutoSize = true;
            this.rbDirSync.Enabled = false;
            this.rbDirSync.Location = new System.Drawing.Point(12, 73);
            this.rbDirSync.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDirSync.Name = "rbDirSync";
            this.rbDirSync.Size = new System.Drawing.Size(78, 21);
            this.rbDirSync.TabIndex = 2;
            this.rbDirSync.Text = "DirSync";
            this.rbDirSync.UseVisualStyleBackColor = true;
            this.rbDirSync.CheckedChanged += new System.EventHandler(this.rbDirSync_CheckedChanged);
            // 
            // rbPagedQuery
            // 
            this.rbPagedQuery.AutoSize = true;
            this.rbPagedQuery.Location = new System.Drawing.Point(12, 21);
            this.rbPagedQuery.Margin = new System.Windows.Forms.Padding(4);
            this.rbPagedQuery.Name = "rbPagedQuery";
            this.rbPagedQuery.Size = new System.Drawing.Size(70, 21);
            this.rbPagedQuery.TabIndex = 0;
            this.rbPagedQuery.Text = "Paged";
            this.rbPagedQuery.UseVisualStyleBackColor = true;
            this.rbPagedQuery.CheckedChanged += new System.EventHandler(this.rbPagedQuery_CheckedChanged);
            // 
            // cmdLoadSync
            // 
            this.cmdLoadSync.Enabled = false;
            this.cmdLoadSync.Location = new System.Drawing.Point(100, 73);
            this.cmdLoadSync.Margin = new System.Windows.Forms.Padding(4);
            this.cmdLoadSync.Name = "cmdLoadSync";
            this.cmdLoadSync.Size = new System.Drawing.Size(55, 25);
            this.cmdLoadSync.TabIndex = 8;
            this.cmdLoadSync.Text = "Load";
            this.cmdLoadSync.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdLoadSync.UseVisualStyleBackColor = true;
            this.cmdLoadSync.Visible = false;
            this.cmdLoadSync.Click += new System.EventHandler(this.cmdLoadSync_Click);
            // 
            // lbDebugLog
            // 
            this.lbDebugLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDebugLog.BackColor = System.Drawing.SystemColors.Control;
            this.lbDebugLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDebugLog.ContextMenuStrip = this.DebugContextMenu;
            this.lbDebugLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDebugLog.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbDebugLog.FormattingEnabled = true;
            this.lbDebugLog.ItemHeight = 17;
            this.lbDebugLog.Location = new System.Drawing.Point(4, 17);
            this.lbDebugLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbDebugLog.Name = "lbDebugLog";
            this.lbDebugLog.Size = new System.Drawing.Size(428, 102);
            this.lbDebugLog.TabIndex = 13;
            this.lbDebugLog.TabStop = false;
            // 
            // DebugContextMenu
            // 
            this.DebugContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.DebugContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Debug_Copy_MenuItem,
            this.toolStripSeparator12,
            this.Debug_Clear_MenuItem});
            this.DebugContextMenu.Name = "DebugContextMenu";
            this.DebugContextMenu.Size = new System.Drawing.Size(119, 62);
            // 
            // Debug_Copy_MenuItem
            // 
            this.Debug_Copy_MenuItem.Name = "Debug_Copy_MenuItem";
            this.Debug_Copy_MenuItem.Size = new System.Drawing.Size(118, 26);
            this.Debug_Copy_MenuItem.Text = "Copy";
            this.Debug_Copy_MenuItem.Click += new System.EventHandler(this.Debug_Copy_MenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(115, 6);
            // 
            // Debug_Clear_MenuItem
            // 
            this.Debug_Clear_MenuItem.Name = "Debug_Clear_MenuItem";
            this.Debug_Clear_MenuItem.Size = new System.Drawing.Size(118, 26);
            this.Debug_Clear_MenuItem.Text = "Clear";
            this.Debug_Clear_MenuItem.Click += new System.EventHandler(this.Debug_Clear_MenuItem_Click);
            // 
            // gbResult
            // 
            this.gbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResult.Controls.Add(this.tvSync);
            this.gbResult.Controls.Add(this.gbFind);
            this.gbResult.Controls.Add(this.gbResStore);
            this.gbResult.Controls.Add(this.txtOutputOld);
            this.gbResult.Location = new System.Drawing.Point(3, 439);
            this.gbResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbResult.Name = "gbResult";
            this.gbResult.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbResult.Size = new System.Drawing.Size(1228, 314);
            this.gbResult.TabIndex = 8;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Results";
            // 
            // gbFind
            // 
            this.gbFind.Controls.Add(this.cmdCloseFind);
            this.gbFind.Controls.Add(this.cmdFind);
            this.gbFind.Controls.Add(this.txtFind);
            this.gbFind.Location = new System.Drawing.Point(457, 0);
            this.gbFind.Name = "gbFind";
            this.gbFind.Size = new System.Drawing.Size(336, 33);
            this.gbFind.TabIndex = 17;
            this.gbFind.TabStop = false;
            this.gbFind.Tag = "0";
            this.gbFind.Text = "Find";
            this.gbFind.Visible = false;
            this.gbFind.VisibleChanged += new System.EventHandler(this.gbFind_VisibleChanged);
            // 
            // cmdCloseFind
            // 
            this.cmdCloseFind.Location = new System.Drawing.Point(301, 6);
            this.cmdCloseFind.Name = "cmdCloseFind";
            this.cmdCloseFind.Size = new System.Drawing.Size(29, 22);
            this.cmdCloseFind.TabIndex = 2;
            this.cmdCloseFind.Text = "X";
            this.cmdCloseFind.UseVisualStyleBackColor = true;
            this.cmdCloseFind.Click += new System.EventHandler(this.cmdCloseFind_Click);
            // 
            // cmdFind
            // 
            this.cmdFind.Location = new System.Drawing.Point(267, 6);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(29, 22);
            this.cmdFind.TabIndex = 1;
            this.cmdFind.Text = "?";
            this.cmdFind.UseVisualStyleBackColor = true;
            this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(39, 6);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(224, 22);
            this.txtFind.TabIndex = 0;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // gbResStore
            // 
            this.gbResStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResStore.Controls.Add(this.cmdClearResults);
            this.gbResStore.Controls.Add(this.cmdLastResult);
            this.gbResStore.Controls.Add(this.cmdFirstResult);
            this.gbResStore.Controls.Add(this.cmdNextResult);
            this.gbResStore.Controls.Add(this.cmdPrevResult);
            this.gbResStore.Location = new System.Drawing.Point(793, 0);
            this.gbResStore.Margin = new System.Windows.Forms.Padding(4);
            this.gbResStore.Name = "gbResStore";
            this.gbResStore.Padding = new System.Windows.Forms.Padding(4);
            this.gbResStore.Size = new System.Drawing.Size(435, 33);
            this.gbResStore.TabIndex = 5;
            this.gbResStore.TabStop = false;
            this.gbResStore.Text = "Remembered results (0 of 0)    ";
            this.gbResStore.Visible = false;
            // 
            // cmdClearResults
            // 
            this.cmdClearResults.Enabled = false;
            this.cmdClearResults.ForeColor = System.Drawing.Color.Maroon;
            this.cmdClearResults.Location = new System.Drawing.Point(389, 0);
            this.cmdClearResults.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClearResults.Name = "cmdClearResults";
            this.cmdClearResults.Size = new System.Drawing.Size(37, 27);
            this.cmdClearResults.TabIndex = 9;
            this.cmdClearResults.Text = " x";
            this.cmdClearResults.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdClearResults.UseVisualStyleBackColor = true;
            this.cmdClearResults.Click += new System.EventHandler(this.cmdClearResults_Click);
            // 
            // cmdLastResult
            // 
            this.cmdLastResult.Enabled = false;
            this.cmdLastResult.Location = new System.Drawing.Point(344, 0);
            this.cmdLastResult.Margin = new System.Windows.Forms.Padding(4);
            this.cmdLastResult.Name = "cmdLastResult";
            this.cmdLastResult.Size = new System.Drawing.Size(37, 27);
            this.cmdLastResult.TabIndex = 8;
            this.cmdLastResult.Text = ">>";
            this.cmdLastResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdLastResult.UseVisualStyleBackColor = true;
            this.cmdLastResult.Click += new System.EventHandler(this.cmdLastResult_Click);
            // 
            // cmdFirstResult
            // 
            this.cmdFirstResult.Enabled = false;
            this.cmdFirstResult.Location = new System.Drawing.Point(208, 0);
            this.cmdFirstResult.Margin = new System.Windows.Forms.Padding(4);
            this.cmdFirstResult.Name = "cmdFirstResult";
            this.cmdFirstResult.Size = new System.Drawing.Size(37, 27);
            this.cmdFirstResult.TabIndex = 7;
            this.cmdFirstResult.Text = "<<";
            this.cmdFirstResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdFirstResult.UseVisualStyleBackColor = true;
            this.cmdFirstResult.Click += new System.EventHandler(this.cmdFirstResult_Click);
            // 
            // cmdNextResult
            // 
            this.cmdNextResult.Enabled = false;
            this.cmdNextResult.Location = new System.Drawing.Point(299, 0);
            this.cmdNextResult.Margin = new System.Windows.Forms.Padding(4);
            this.cmdNextResult.Name = "cmdNextResult";
            this.cmdNextResult.Size = new System.Drawing.Size(37, 27);
            this.cmdNextResult.TabIndex = 6;
            this.cmdNextResult.Text = ">";
            this.cmdNextResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdNextResult.UseVisualStyleBackColor = true;
            this.cmdNextResult.Click += new System.EventHandler(this.cmdNextResult_Click);
            // 
            // cmdPrevResult
            // 
            this.cmdPrevResult.Enabled = false;
            this.cmdPrevResult.Location = new System.Drawing.Point(253, 0);
            this.cmdPrevResult.Margin = new System.Windows.Forms.Padding(4);
            this.cmdPrevResult.Name = "cmdPrevResult";
            this.cmdPrevResult.Size = new System.Drawing.Size(37, 27);
            this.cmdPrevResult.TabIndex = 5;
            this.cmdPrevResult.Text = "<";
            this.cmdPrevResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPrevResult.UseVisualStyleBackColor = true;
            this.cmdPrevResult.Click += new System.EventHandler(this.cmdPrevResult_Click);
            // 
            // txtOutputOld
            // 
            this.txtOutputOld.AcceptsReturn = true;
            this.txtOutputOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputOld.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutputOld.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputOld.ContextMenuStrip = this.ResultsContextMenu;
            this.txtOutputOld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputOld.HideSelection = false;
            this.txtOutputOld.Location = new System.Drawing.Point(3, 20);
            this.txtOutputOld.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutputOld.Multiline = true;
            this.txtOutputOld.Name = "txtOutputOld";
            this.txtOutputOld.ReadOnly = true;
            this.txtOutputOld.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutputOld.Size = new System.Drawing.Size(1223, 292);
            this.txtOutputOld.TabIndex = 0;
            this.txtOutputOld.WordWrap = false;
            // 
            // ResultsContextMenu
            // 
            this.ResultsContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ResultsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Results_CopySelected_ContextItem,
            this.Results_CopyAll_ContextItem,
            this.toolStripSeparator7,
            this.Result_Remembered_ContextItem,
            this.toolStripSeparator22,
            this.Results_Clear_ContextItem,
            this.toolStripSeparator8,
            this.Result_Search_MenuItem,
            this.toolStripSeparator14,
            this.Result_Font_MenuItem});
            this.ResultsContextMenu.Name = "ContextMenuResults";
            this.ResultsContextMenu.Size = new System.Drawing.Size(221, 184);
            // 
            // Results_CopySelected_ContextItem
            // 
            this.Results_CopySelected_ContextItem.Name = "Results_CopySelected_ContextItem";
            this.Results_CopySelected_ContextItem.Size = new System.Drawing.Size(220, 26);
            this.Results_CopySelected_ContextItem.Text = "Copy Selected";
            this.Results_CopySelected_ContextItem.Click += new System.EventHandler(this.Results_CopySelected_ContextItem_Click);
            // 
            // Results_CopyAll_ContextItem
            // 
            this.Results_CopyAll_ContextItem.Name = "Results_CopyAll_ContextItem";
            this.Results_CopyAll_ContextItem.Size = new System.Drawing.Size(220, 26);
            this.Results_CopyAll_ContextItem.Text = "Copy All";
            this.Results_CopyAll_ContextItem.Click += new System.EventHandler(this.Results_CopyAll_ContextItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(217, 6);
            // 
            // Result_Remembered_ContextItem
            // 
            this.Result_Remembered_ContextItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Result_Remembered_First_ContextItem,
            this.Result_Remembered_Previous_ContextItem,
            this.Result_Remembered_Next_ContextItem,
            this.Result_Remembered_Last_ContextItem,
            this.Result_Remembered_Clear_ContextItem});
            this.Result_Remembered_ContextItem.Enabled = false;
            this.Result_Remembered_ContextItem.Name = "Result_Remembered_ContextItem";
            this.Result_Remembered_ContextItem.Size = new System.Drawing.Size(220, 26);
            this.Result_Remembered_ContextItem.Text = "Remembered results";
            // 
            // Result_Remembered_First_ContextItem
            // 
            this.Result_Remembered_First_ContextItem.Enabled = false;
            this.Result_Remembered_First_ContextItem.Name = "Result_Remembered_First_ContextItem";
            this.Result_Remembered_First_ContextItem.Size = new System.Drawing.Size(180, 26);
            this.Result_Remembered_First_ContextItem.Text = "Show first";
            this.Result_Remembered_First_ContextItem.Click += new System.EventHandler(this.Result_Remembered_First_ContextItem_Click);
            // 
            // Result_Remembered_Previous_ContextItem
            // 
            this.Result_Remembered_Previous_ContextItem.Enabled = false;
            this.Result_Remembered_Previous_ContextItem.Name = "Result_Remembered_Previous_ContextItem";
            this.Result_Remembered_Previous_ContextItem.Size = new System.Drawing.Size(180, 26);
            this.Result_Remembered_Previous_ContextItem.Text = "Show previous";
            this.Result_Remembered_Previous_ContextItem.Click += new System.EventHandler(this.Result_Remembered_Previous_ContextItem_Click);
            // 
            // Result_Remembered_Next_ContextItem
            // 
            this.Result_Remembered_Next_ContextItem.Enabled = false;
            this.Result_Remembered_Next_ContextItem.Name = "Result_Remembered_Next_ContextItem";
            this.Result_Remembered_Next_ContextItem.Size = new System.Drawing.Size(180, 26);
            this.Result_Remembered_Next_ContextItem.Text = "Show next";
            this.Result_Remembered_Next_ContextItem.Click += new System.EventHandler(this.Result_Remembered_Next_ContextItem_Click);
            // 
            // Result_Remembered_Last_ContextItem
            // 
            this.Result_Remembered_Last_ContextItem.Enabled = false;
            this.Result_Remembered_Last_ContextItem.Name = "Result_Remembered_Last_ContextItem";
            this.Result_Remembered_Last_ContextItem.Size = new System.Drawing.Size(180, 26);
            this.Result_Remembered_Last_ContextItem.Text = "Show current";
            this.Result_Remembered_Last_ContextItem.Click += new System.EventHandler(this.Result_Remembered_Last_ContextItem_Click);
            // 
            // Result_Remembered_Clear_ContextItem
            // 
            this.Result_Remembered_Clear_ContextItem.Enabled = false;
            this.Result_Remembered_Clear_ContextItem.Name = "Result_Remembered_Clear_ContextItem";
            this.Result_Remembered_Clear_ContextItem.Size = new System.Drawing.Size(180, 26);
            this.Result_Remembered_Clear_ContextItem.Text = "Clear list";
            this.Result_Remembered_Clear_ContextItem.Click += new System.EventHandler(this.Result_Remembered_Clear_ContextItem_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(217, 6);
            this.toolStripSeparator22.Visible = false;
            // 
            // Results_Clear_ContextItem
            // 
            this.Results_Clear_ContextItem.Enabled = false;
            this.Results_Clear_ContextItem.Name = "Results_Clear_ContextItem";
            this.Results_Clear_ContextItem.Size = new System.Drawing.Size(220, 26);
            this.Results_Clear_ContextItem.Text = "Clear";
            this.Results_Clear_ContextItem.Visible = false;
            this.Results_Clear_ContextItem.Click += new System.EventHandler(this.Results_Clear_MenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(217, 6);
            // 
            // Result_Search_MenuItem
            // 
            this.Result_Search_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Result_Search_String_MenuItem,
            this.toolStripSeparator9,
            this.Result_Search_Find_MenuItem,
            this.Result_Search_FindNext_MenuItem});
            this.Result_Search_MenuItem.Enabled = false;
            this.Result_Search_MenuItem.Name = "Result_Search_MenuItem";
            this.Result_Search_MenuItem.Size = new System.Drawing.Size(220, 26);
            this.Result_Search_MenuItem.Text = "Search String";
            this.Result_Search_MenuItem.Visible = false;
            this.Result_Search_MenuItem.Click += new System.EventHandler(this.Result_Search_MenuItem_Click);
            // 
            // Result_Search_String_MenuItem
            // 
            this.Result_Search_String_MenuItem.Name = "Result_Search_String_MenuItem";
            this.Result_Search_String_MenuItem.Size = new System.Drawing.Size(152, 27);
            this.Result_Search_String_MenuItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Result_Search_String_MenuItem_KeyUp);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(215, 6);
            // 
            // Result_Search_Find_MenuItem
            // 
            this.Result_Search_Find_MenuItem.Name = "Result_Search_Find_MenuItem";
            this.Result_Search_Find_MenuItem.Size = new System.Drawing.Size(218, 26);
            this.Result_Search_Find_MenuItem.Text = "Find";
            this.Result_Search_Find_MenuItem.Click += new System.EventHandler(this.Result_Search_Find_MenuItem_Click);
            // 
            // Result_Search_FindNext_MenuItem
            // 
            this.Result_Search_FindNext_MenuItem.Name = "Result_Search_FindNext_MenuItem";
            this.Result_Search_FindNext_MenuItem.Size = new System.Drawing.Size(218, 26);
            this.Result_Search_FindNext_MenuItem.Text = "Find next";
            this.Result_Search_FindNext_MenuItem.Click += new System.EventHandler(this.Result_Search_FindNext_MenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(217, 6);
            // 
            // Result_Font_MenuItem
            // 
            this.Result_Font_MenuItem.Name = "Result_Font_MenuItem";
            this.Result_Font_MenuItem.Size = new System.Drawing.Size(220, 26);
            this.Result_Font_MenuItem.Text = "Change Font";
            this.Result_Font_MenuItem.Click += new System.EventHandler(this.Font_Picker_MenuItem_Click);
            // 
            // gbDebug
            // 
            this.gbDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDebug.Controls.Add(this.lbDebugLog);
            this.gbDebug.Location = new System.Drawing.Point(796, 34);
            this.gbDebug.Margin = new System.Windows.Forms.Padding(4);
            this.gbDebug.Name = "gbDebug";
            this.gbDebug.Padding = new System.Windows.Forms.Padding(4);
            this.gbDebug.Size = new System.Drawing.Size(435, 143);
            this.gbDebug.TabIndex = 5;
            this.gbDebug.TabStop = false;
            this.gbDebug.Text = "Debug";
            // 
            // gbProps
            // 
            this.gbProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProps.ContextMenuStrip = this.AttributesListContextMenu;
            this.gbProps.Controls.Add(this.cmdAdd);
            this.gbProps.Controls.Add(this.lbAttribs);
            this.gbProps.Location = new System.Drawing.Point(796, 34);
            this.gbProps.Margin = new System.Windows.Forms.Padding(4);
            this.gbProps.Name = "gbProps";
            this.gbProps.Padding = new System.Windows.Forms.Padding(4);
            this.gbProps.Size = new System.Drawing.Size(435, 143);
            this.gbProps.TabIndex = 9;
            this.gbProps.TabStop = false;
            this.gbProps.Text = "Attributes from Schema";
            this.gbProps.Visible = false;
            // 
            // AttributesListContextMenu
            // 
            this.AttributesListContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AttributesListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttributesList_Copy_ContextItem,
            this.AttributesList_CopyAll_ContextItem,
            this.toolStripSeparator5,
            this.AttributesList_Add_ContextItem,
            this.toolStripSeparator6,
            this.AttributesList_Hide_ContextItem});
            this.AttributesListContextMenu.Name = "ContextMenuAttribList";
            this.AttributesListContextMenu.Size = new System.Drawing.Size(237, 120);
            // 
            // AttributesList_Copy_ContextItem
            // 
            this.AttributesList_Copy_ContextItem.Name = "AttributesList_Copy_ContextItem";
            this.AttributesList_Copy_ContextItem.Size = new System.Drawing.Size(236, 26);
            this.AttributesList_Copy_ContextItem.Text = "Copy Selected";
            this.AttributesList_Copy_ContextItem.Click += new System.EventHandler(this.AttributesList_Copy_ContextItem_Click);
            // 
            // AttributesList_CopyAll_ContextItem
            // 
            this.AttributesList_CopyAll_ContextItem.Name = "AttributesList_CopyAll_ContextItem";
            this.AttributesList_CopyAll_ContextItem.Size = new System.Drawing.Size(236, 26);
            this.AttributesList_CopyAll_ContextItem.Text = "Copy All";
            this.AttributesList_CopyAll_ContextItem.Click += new System.EventHandler(this.AttributesList_CopyAll_ContextItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(233, 6);
            // 
            // AttributesList_Add_ContextItem
            // 
            this.AttributesList_Add_ContextItem.Name = "AttributesList_Add_ContextItem";
            this.AttributesList_Add_ContextItem.Size = new System.Drawing.Size(236, 26);
            this.AttributesList_Add_ContextItem.Text = "Add Selected Attribute";
            this.AttributesList_Add_ContextItem.Click += new System.EventHandler(this.AttributesList_Add_ContextItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(233, 6);
            // 
            // AttributesList_Hide_ContextItem
            // 
            this.AttributesList_Hide_ContextItem.Name = "AttributesList_Hide_ContextItem";
            this.AttributesList_Hide_ContextItem.Size = new System.Drawing.Size(236, 26);
            this.AttributesList_Hide_ContextItem.Text = "Hide Attribute List";
            this.AttributesList_Hide_ContextItem.Click += new System.EventHandler(this.Attributes_Hide_MenuItem_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdAdd.Location = new System.Drawing.Point(389, 64);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(44, 25);
            this.cmdAdd.TabIndex = 37;
            this.cmdAdd.Text = "add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // lbAttribs
            // 
            this.lbAttribs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAttribs.ContextMenuStrip = this.AttributesListContextMenu;
            this.lbAttribs.FormattingEnabled = true;
            this.lbAttribs.ItemHeight = 16;
            this.lbAttribs.Location = new System.Drawing.Point(8, 25);
            this.lbAttribs.Margin = new System.Windows.Forms.Padding(4);
            this.lbAttribs.Name = "lbAttribs";
            this.lbAttribs.Size = new System.Drawing.Size(379, 100);
            this.lbAttribs.TabIndex = 0;
            this.lbAttribs.TabStop = false;
            this.lbAttribs.DoubleClick += new System.EventHandler(this.lbAttribs_DoubleClick);
            this.lbAttribs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbAttribs_KeyDown);
            this.lbAttribs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbAttribs_KeyUp);
            // 
            // ListTimer
            // 
            this.ListTimer.Interval = 500;
            // 
            // MainMenue
            // 
            this.MainMenue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainMenue.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenue.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.UserMenu,
            this.ConnectionMenu,
            this.QueryMenu,
            this.FilterMenu,
            this.AttributesMenu,
            this.DecodeMenu,
            this.ResultsMenu,
            this.ToolsMenu});
            this.MainMenue.Location = new System.Drawing.Point(0, 0);
            this.MainMenue.Name = "MainMenue";
            this.MainMenue.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.MainMenue.Size = new System.Drawing.Size(597, 28);
            this.MainMenue.TabIndex = 14;
            this.MainMenue.Text = "MainMenu";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_Close_MenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.FileMenu.Size = new System.Drawing.Size(44, 24);
            this.FileMenu.Text = "File";
            // 
            // File_Close_MenuItem
            // 
            this.File_Close_MenuItem.Name = "File_Close_MenuItem";
            this.File_Close_MenuItem.Size = new System.Drawing.Size(120, 26);
            this.File_Close_MenuItem.Text = "Close";
            this.File_Close_MenuItem.Click += new System.EventHandler(this.File_Close_MenuItem_Click);
            // 
            // UserMenu
            // 
            this.UserMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.User_Whoami_MenuItem,
            this.toolStripSeparator34,
            this.User_Elevate_MenuItem,
            this.User_Elevate_2nd_MenuItem,
            this.toolStripSeparator25,
            this.User_Restart_MenuItem,
            this.User_Restart_Elevate_MenuItem,
            this.User_Runas_MenuItem,
            this.User_Runas_Elevate_MenuItem});
            this.UserMenu.Name = "UserMenu";
            this.UserMenu.Size = new System.Drawing.Size(50, 24);
            this.UserMenu.Text = "User";
            // 
            // User_Whoami_MenuItem
            // 
            this.User_Whoami_MenuItem.Name = "User_Whoami_MenuItem";
            this.User_Whoami_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Whoami_MenuItem.Text = "WhoAmI";
            this.User_Whoami_MenuItem.Click += new System.EventHandler(this.User_Whoami_MenuItem_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(282, 6);
            // 
            // User_Elevate_MenuItem
            // 
            this.User_Elevate_MenuItem.Image = ((System.Drawing.Image)(resources.GetObject("User_Elevate_MenuItem.Image")));
            this.User_Elevate_MenuItem.Name = "User_Elevate_MenuItem";
            this.User_Elevate_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Elevate_MenuItem.Text = "Restart elevated";
            this.User_Elevate_MenuItem.Click += new System.EventHandler(this.User_Elevate_MenuItem_Click);
            // 
            // User_Elevate_2nd_MenuItem
            // 
            this.User_Elevate_2nd_MenuItem.Image = ((System.Drawing.Image)(resources.GetObject("User_Elevate_2nd_MenuItem.Image")));
            this.User_Elevate_2nd_MenuItem.Name = "User_Elevate_2nd_MenuItem";
            this.User_Elevate_2nd_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Elevate_2nd_MenuItem.Text = "Run elevated (2nd instance)";
            this.User_Elevate_2nd_MenuItem.Click += new System.EventHandler(this.User_Elevate_2nd_MenuItem_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(282, 6);
            // 
            // User_Restart_MenuItem
            // 
            this.User_Restart_MenuItem.Name = "User_Restart_MenuItem";
            this.User_Restart_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Restart_MenuItem.Text = "Restart as";
            this.User_Restart_MenuItem.Click += new System.EventHandler(this.User_Restart_MenuItem_Click);
            // 
            // User_Restart_Elevate_MenuItem
            // 
            this.User_Restart_Elevate_MenuItem.Image = ((System.Drawing.Image)(resources.GetObject("User_Restart_Elevate_MenuItem.Image")));
            this.User_Restart_Elevate_MenuItem.Name = "User_Restart_Elevate_MenuItem";
            this.User_Restart_Elevate_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Restart_Elevate_MenuItem.Text = "Restart as elevated";
            this.User_Restart_Elevate_MenuItem.Click += new System.EventHandler(this.User_Restart_Elevate_MenuItem_Click);
            // 
            // User_Runas_MenuItem
            // 
            this.User_Runas_MenuItem.Name = "User_Runas_MenuItem";
            this.User_Runas_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Runas_MenuItem.Text = "Run as (2nd instance)";
            this.User_Runas_MenuItem.Click += new System.EventHandler(this.User_Runas_MenuItem_Click);
            // 
            // User_Runas_Elevate_MenuItem
            // 
            this.User_Runas_Elevate_MenuItem.Image = ((System.Drawing.Image)(resources.GetObject("User_Runas_Elevate_MenuItem.Image")));
            this.User_Runas_Elevate_MenuItem.Name = "User_Runas_Elevate_MenuItem";
            this.User_Runas_Elevate_MenuItem.Size = new System.Drawing.Size(285, 26);
            this.User_Runas_Elevate_MenuItem.Text = "Run as elevated (2nd instance)";
            this.User_Runas_Elevate_MenuItem.Click += new System.EventHandler(this.User_Runas_Elevate_MenuItem_Click);
            // 
            // ConnectionMenu
            // 
            this.ConnectionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connection_Refresh_MenuItem,
            this.toolStripSeparator24,
            this.Connection_Ports_MenuItem,
            this.Connection_TimeOut_MenuItem,
            this.toolStripSeparator20,
            this.Connection_ConnectForest_MenuItem,
            this.Connection_PassCredentials_MenuItem});
            this.ConnectionMenu.Name = "ConnectionMenu";
            this.ConnectionMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.ConnectionMenu.Size = new System.Drawing.Size(96, 24);
            this.ConnectionMenu.Text = "Connection";
            // 
            // Connection_Refresh_MenuItem
            // 
            this.Connection_Refresh_MenuItem.Name = "Connection_Refresh_MenuItem";
            this.Connection_Refresh_MenuItem.Size = new System.Drawing.Size(190, 26);
            this.Connection_Refresh_MenuItem.Text = "Refresh Forest";
            this.Connection_Refresh_MenuItem.Click += new System.EventHandler(this.Connection_Refresh_MenuItem_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(187, 6);
            // 
            // Connection_Ports_MenuItem
            // 
            this.Connection_Ports_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ports_LDAP_MenuItem,
            this.Ports_GC_MenuItem});
            this.Connection_Ports_MenuItem.Name = "Connection_Ports_MenuItem";
            this.Connection_Ports_MenuItem.Size = new System.Drawing.Size(190, 26);
            this.Connection_Ports_MenuItem.Text = "Set Ports";
            // 
            // Ports_LDAP_MenuItem
            // 
            this.Ports_LDAP_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ports_LDAP_Value_Text});
            this.Ports_LDAP_MenuItem.Name = "Ports_LDAP_MenuItem";
            this.Ports_LDAP_MenuItem.Size = new System.Drawing.Size(120, 26);
            this.Ports_LDAP_MenuItem.Text = "LDAP";
            // 
            // Ports_LDAP_Value_Text
            // 
            this.Ports_LDAP_Value_Text.Name = "Ports_LDAP_Value_Text";
            this.Ports_LDAP_Value_Text.Size = new System.Drawing.Size(100, 27);
            this.Ports_LDAP_Value_Text.Tag = "true";
            this.Ports_LDAP_Value_Text.Text = "389";
            this.Ports_LDAP_Value_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ports_LDAP_Value_Text_KeyPress);
            // 
            // Ports_GC_MenuItem
            // 
            this.Ports_GC_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ports_GC_Value_Text});
            this.Ports_GC_MenuItem.Name = "Ports_GC_MenuItem";
            this.Ports_GC_MenuItem.Size = new System.Drawing.Size(120, 26);
            this.Ports_GC_MenuItem.Text = "GC";
            // 
            // Ports_GC_Value_Text
            // 
            this.Ports_GC_Value_Text.Name = "Ports_GC_Value_Text";
            this.Ports_GC_Value_Text.Size = new System.Drawing.Size(100, 27);
            this.Ports_GC_Value_Text.Tag = "false";
            this.Ports_GC_Value_Text.Text = "3268";
            this.Ports_GC_Value_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ports_GC_Value_Text_KeyPress);
            // 
            // Connection_TimeOut_MenuItem
            // 
            this.Connection_TimeOut_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connection_TimeOutValue_MenuItem});
            this.Connection_TimeOut_MenuItem.Name = "Connection_TimeOut_MenuItem";
            this.Connection_TimeOut_MenuItem.Size = new System.Drawing.Size(190, 26);
            this.Connection_TimeOut_MenuItem.Text = "Set Timeout (s)";
            // 
            // Connection_TimeOutValue_MenuItem
            // 
            this.Connection_TimeOutValue_MenuItem.Name = "Connection_TimeOutValue_MenuItem";
            this.Connection_TimeOutValue_MenuItem.Size = new System.Drawing.Size(100, 27);
            this.Connection_TimeOutValue_MenuItem.Text = "120";
            this.Connection_TimeOutValue_MenuItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Connection_TimeOutValue_MenuItem_KeyPress);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(187, 6);
            // 
            // Connection_ConnectForest_MenuItem
            // 
            this.Connection_ConnectForest_MenuItem.Name = "Connection_ConnectForest_MenuItem";
            this.Connection_ConnectForest_MenuItem.Size = new System.Drawing.Size(190, 26);
            this.Connection_ConnectForest_MenuItem.Text = "Connect Forest";
            this.Connection_ConnectForest_MenuItem.Click += new System.EventHandler(this.Connection_NewForest_MenuItem_Click);
            // 
            // Connection_PassCredentials_MenuItem
            // 
            this.Connection_PassCredentials_MenuItem.Name = "Connection_PassCredentials_MenuItem";
            this.Connection_PassCredentials_MenuItem.Size = new System.Drawing.Size(190, 26);
            this.Connection_PassCredentials_MenuItem.Text = "Pass Credentials";
            this.Connection_PassCredentials_MenuItem.Click += new System.EventHandler(this.Connection_PassCredentials_MenuItem_Click);
            // 
            // QueryMenu
            // 
            this.QueryMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Query_Browse_MenuItem,
            this.toolStripSeparator17,
            this.Query_Execute_MenuItem,
            this.toolStripSeparator1,
            this.Query_Provider_MenuItem,
            this.Query_BaseType_MenuItem,
            this.Query_Type_MenuItem,
            this.Query_Scope_MenuItem,
            this.Query_Return_MenuItem,
            this.toolStripSeparator15,
            this.Query_Async_MenuItem,
            this.Query_AutoPage_MenuItem,
            this.Query_ValRange_MenuItem,
            this.toolStripSeparator27,
            this.Query_ShowDeleted_MenuItem,
            this.Query_ShowRecycled_MenuItem,
            this.toolStripSeparator28,
            this.Query_Sort_Results_Asc_MenuItem,
            this.Query_Sort_Results_Desc_MenuItem,
            this.toolStripSeparator29,
            this.Query_ASQ_MenuItem,
            this.Query_Sort_MenuItem});
            this.QueryMenu.Name = "QueryMenu";
            this.QueryMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.QueryMenu.Size = new System.Drawing.Size(60, 24);
            this.QueryMenu.Text = "Query";
            // 
            // Query_Browse_MenuItem
            // 
            this.Query_Browse_MenuItem.Name = "Query_Browse_MenuItem";
            this.Query_Browse_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Browse_MenuItem.Text = "Browse (Base)";
            this.Query_Browse_MenuItem.Click += new System.EventHandler(this.Query_Browse_MenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(315, 6);
            // 
            // Query_Execute_MenuItem
            // 
            this.Query_Execute_MenuItem.Name = "Query_Execute_MenuItem";
            this.Query_Execute_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Execute_MenuItem.Text = "Execute Query";
            this.Query_Execute_MenuItem.Click += new System.EventHandler(this.Query_Execute_MenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(315, 6);
            // 
            // Query_Provider_MenuItem
            // 
            this.Query_Provider_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Query_Provider_Combo_MenuItem});
            this.Query_Provider_MenuItem.Name = "Query_Provider_MenuItem";
            this.Query_Provider_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Provider_MenuItem.Text = "Provider";
            // 
            // Query_Provider_Combo_MenuItem
            // 
            this.Query_Provider_Combo_MenuItem.Items.AddRange(new object[] {
            "LDAP",
            "GC"});
            this.Query_Provider_Combo_MenuItem.Name = "Query_Provider_Combo_MenuItem";
            this.Query_Provider_Combo_MenuItem.Size = new System.Drawing.Size(152, 28);
            this.Query_Provider_Combo_MenuItem.Text = "LDAP";
            this.Query_Provider_Combo_MenuItem.SelectedIndexChanged += new System.EventHandler(this.Query_Provider_Combo_MenuItem_SelectedIndexChanged);
            // 
            // Query_BaseType_MenuItem
            // 
            this.Query_BaseType_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Query_BaseType_Combo_MenuItem});
            this.Query_BaseType_MenuItem.Name = "Query_BaseType_MenuItem";
            this.Query_BaseType_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_BaseType_MenuItem.Text = "Base type";
            // 
            // Query_BaseType_Combo_MenuItem
            // 
            this.Query_BaseType_Combo_MenuItem.Items.AddRange(new object[] {
            "DN",
            "PhantomRoot",
            "rootDSE"});
            this.Query_BaseType_Combo_MenuItem.Name = "Query_BaseType_Combo_MenuItem";
            this.Query_BaseType_Combo_MenuItem.Size = new System.Drawing.Size(121, 28);
            this.Query_BaseType_Combo_MenuItem.Text = "DN";
            this.Query_BaseType_Combo_MenuItem.Click += new System.EventHandler(this.Query_BaseType_Combo_MenuItem_Click);
            // 
            // Query_Type_MenuItem
            // 
            this.Query_Type_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Query_Type_Combo_MenuItem});
            this.Query_Type_MenuItem.Name = "Query_Type_MenuItem";
            this.Query_Type_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Type_MenuItem.Text = "Query type";
            // 
            // Query_Type_Combo_MenuItem
            // 
            this.Query_Type_Combo_MenuItem.Items.AddRange(new object[] {
            "Non-Paged",
            "Paged",
            "DirSync"});
            this.Query_Type_Combo_MenuItem.Name = "Query_Type_Combo_MenuItem";
            this.Query_Type_Combo_MenuItem.Size = new System.Drawing.Size(121, 28);
            this.Query_Type_Combo_MenuItem.Text = "Non-Paged";
            this.Query_Type_Combo_MenuItem.Click += new System.EventHandler(this.Query_Type_Combo_MenuItem_Click);
            // 
            // Query_Scope_MenuItem
            // 
            this.Query_Scope_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Query_Scope_Combo_MenuItem});
            this.Query_Scope_MenuItem.Name = "Query_Scope_MenuItem";
            this.Query_Scope_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Scope_MenuItem.Text = "SearchScope";
            // 
            // Query_Scope_Combo_MenuItem
            // 
            this.Query_Scope_Combo_MenuItem.Items.AddRange(new object[] {
            "Base",
            "OneLevel",
            "SubTree"});
            this.Query_Scope_Combo_MenuItem.Name = "Query_Scope_Combo_MenuItem";
            this.Query_Scope_Combo_MenuItem.Size = new System.Drawing.Size(152, 28);
            this.Query_Scope_Combo_MenuItem.Text = "SubTree";
            this.Query_Scope_Combo_MenuItem.SelectedIndexChanged += new System.EventHandler(this.Query_Scope_Combo_MenuItem_SelectedIndexChanged);
            // 
            // Query_Return_MenuItem
            // 
            this.Query_Return_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Query_Return_Results_MenuItem,
            this.Query_Return_Statistics_MenuItem,
            this.toolStripSeparator21,
            this.Query_Return_RootDSEExtended_MenuItem});
            this.Query_Return_MenuItem.Name = "Query_Return_MenuItem";
            this.Query_Return_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Return_MenuItem.Text = "Return";
            // 
            // Query_Return_Results_MenuItem
            // 
            this.Query_Return_Results_MenuItem.CheckOnClick = true;
            this.Query_Return_Results_MenuItem.Name = "Query_Return_Results_MenuItem";
            this.Query_Return_Results_MenuItem.Size = new System.Drawing.Size(287, 26);
            this.Query_Return_Results_MenuItem.Text = "Results";
            this.Query_Return_Results_MenuItem.Click += new System.EventHandler(this.Query_Return_Results_MenuItem_Click);
            // 
            // Query_Return_Statistics_MenuItem
            // 
            this.Query_Return_Statistics_MenuItem.CheckOnClick = true;
            this.Query_Return_Statistics_MenuItem.Name = "Query_Return_Statistics_MenuItem";
            this.Query_Return_Statistics_MenuItem.Size = new System.Drawing.Size(287, 26);
            this.Query_Return_Statistics_MenuItem.Text = "Satistics";
            this.Query_Return_Statistics_MenuItem.Click += new System.EventHandler(this.Query_Return_Statistics_MenuItem_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(284, 6);
            // 
            // Query_Return_RootDSEExtended_MenuItem
            // 
            this.Query_Return_RootDSEExtended_MenuItem.CheckOnClick = true;
            this.Query_Return_RootDSEExtended_MenuItem.Name = "Query_Return_RootDSEExtended_MenuItem";
            this.Query_Return_RootDSEExtended_MenuItem.Size = new System.Drawing.Size(287, 26);
            this.Query_Return_RootDSEExtended_MenuItem.Text = "operational rootDSE attributes";
            this.Query_Return_RootDSEExtended_MenuItem.Click += new System.EventHandler(this.Query_Return_RootDSEExtended_MenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(315, 6);
            // 
            // Query_Async_MenuItem
            // 
            this.Query_Async_MenuItem.CheckOnClick = true;
            this.Query_Async_MenuItem.Enabled = false;
            this.Query_Async_MenuItem.Name = "Query_Async_MenuItem";
            this.Query_Async_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Async_MenuItem.Text = "Run Async";
            this.Query_Async_MenuItem.Visible = false;
            this.Query_Async_MenuItem.Click += new System.EventHandler(this.Query_Async_MenuItem_Click);
            // 
            // Query_AutoPage_MenuItem
            // 
            this.Query_AutoPage_MenuItem.CheckOnClick = true;
            this.Query_AutoPage_MenuItem.Name = "Query_AutoPage_MenuItem";
            this.Query_AutoPage_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_AutoPage_MenuItem.Text = "Autoswitch to PagedQuery";
            this.Query_AutoPage_MenuItem.CheckedChanged += new System.EventHandler(this.Query_AutoPage_MenuItem_CheckChanged);
            // 
            // Query_ValRange_MenuItem
            // 
            this.Query_ValRange_MenuItem.CheckOnClick = true;
            this.Query_ValRange_MenuItem.Name = "Query_ValRange_MenuItem";
            this.Query_ValRange_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_ValRange_MenuItem.Text = "Value Range Retrieval";
            this.Query_ValRange_MenuItem.CheckedChanged += new System.EventHandler(this.Query_ValRange_MenuItem_CheckChanged);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(315, 6);
            // 
            // Query_ShowDeleted_MenuItem
            // 
            this.Query_ShowDeleted_MenuItem.CheckOnClick = true;
            this.Query_ShowDeleted_MenuItem.Name = "Query_ShowDeleted_MenuItem";
            this.Query_ShowDeleted_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_ShowDeleted_MenuItem.Text = "Show deleted";
            this.Query_ShowDeleted_MenuItem.CheckedChanged += new System.EventHandler(this.Query_ShowDeleted_MenuItem_CheckedChanged);
            // 
            // Query_ShowRecycled_MenuItem
            // 
            this.Query_ShowRecycled_MenuItem.CheckOnClick = true;
            this.Query_ShowRecycled_MenuItem.Enabled = false;
            this.Query_ShowRecycled_MenuItem.Name = "Query_ShowRecycled_MenuItem";
            this.Query_ShowRecycled_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_ShowRecycled_MenuItem.Text = "Show recycled";
            this.Query_ShowRecycled_MenuItem.CheckedChanged += new System.EventHandler(this.Query_ShowRecycled_MenuItem_CheckedChanged);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(315, 6);
            // 
            // Query_Sort_Results_Asc_MenuItem
            // 
            this.Query_Sort_Results_Asc_MenuItem.CheckOnClick = true;
            this.Query_Sort_Results_Asc_MenuItem.Name = "Query_Sort_Results_Asc_MenuItem";
            this.Query_Sort_Results_Asc_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Sort_Results_Asc_MenuItem.Text = "Sort results ascending (client side)";
            this.Query_Sort_Results_Asc_MenuItem.CheckedChanged += new System.EventHandler(this.Query_Sort_Results_Asc_MenuItem_CheckedChanged);
            // 
            // Query_Sort_Results_Desc_MenuItem
            // 
            this.Query_Sort_Results_Desc_MenuItem.CheckOnClick = true;
            this.Query_Sort_Results_Desc_MenuItem.Name = "Query_Sort_Results_Desc_MenuItem";
            this.Query_Sort_Results_Desc_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Sort_Results_Desc_MenuItem.Text = "Sort results descending (client side)";
            this.Query_Sort_Results_Desc_MenuItem.CheckedChanged += new System.EventHandler(this.Query_Sort_Results_Desc_MenuItem_CheckedChanged);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(315, 6);
            // 
            // Query_ASQ_MenuItem
            // 
            this.Query_ASQ_MenuItem.CheckOnClick = true;
            this.Query_ASQ_MenuItem.Name = "Query_ASQ_MenuItem";
            this.Query_ASQ_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_ASQ_MenuItem.Text = "Attribute Scoped Query";
            this.Query_ASQ_MenuItem.Click += new System.EventHandler(this.Query_ASQ_MenuItem_Click);
            // 
            // Query_Sort_MenuItem
            // 
            this.Query_Sort_MenuItem.CheckOnClick = true;
            this.Query_Sort_MenuItem.Name = "Query_Sort_MenuItem";
            this.Query_Sort_MenuItem.Size = new System.Drawing.Size(318, 26);
            this.Query_Sort_MenuItem.Text = "Sorted Query (sever side)";
            this.Query_Sort_MenuItem.Click += new System.EventHandler(this.Query_Sort_MenuItem_Click);
            // 
            // FilterMenu
            // 
            this.FilterMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Filter_Wizard_MenuItem,
            this.toolStripSeparator2,
            this.Filter_ClearHistory_MenuItem});
            this.FilterMenu.Name = "FilterMenu";
            this.FilterMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.FilterMenu.Size = new System.Drawing.Size(54, 24);
            this.FilterMenu.Text = "Filter";
            // 
            // Filter_Wizard_MenuItem
            // 
            this.Filter_Wizard_MenuItem.Name = "Filter_Wizard_MenuItem";
            this.Filter_Wizard_MenuItem.Size = new System.Drawing.Size(206, 26);
            this.Filter_Wizard_MenuItem.Text = "Filter Wizard";
            this.Filter_Wizard_MenuItem.Click += new System.EventHandler(this.Filter_Wizard_MenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // Filter_ClearHistory_MenuItem
            // 
            this.Filter_ClearHistory_MenuItem.Name = "Filter_ClearHistory_MenuItem";
            this.Filter_ClearHistory_MenuItem.Size = new System.Drawing.Size(206, 26);
            this.Filter_ClearHistory_MenuItem.Text = "Clear Filter History";
            this.Filter_ClearHistory_MenuItem.Click += new System.EventHandler(this.Filter_ClearHistory_MenuItem_Click);
            // 
            // AttributesMenu
            // 
            this.AttributesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Attributes_Load_MenuItem,
            this.Attributes_Hide_MenuItem});
            this.AttributesMenu.Name = "AttributesMenu";
            this.AttributesMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.AttributesMenu.Size = new System.Drawing.Size(86, 24);
            this.AttributesMenu.Text = "Attributes";
            // 
            // Attributes_Load_MenuItem
            // 
            this.Attributes_Load_MenuItem.Name = "Attributes_Load_MenuItem";
            this.Attributes_Load_MenuItem.Size = new System.Drawing.Size(304, 26);
            this.Attributes_Load_MenuItem.Text = "Load Attributes List from Schema";
            this.Attributes_Load_MenuItem.Click += new System.EventHandler(this.Attributes_Load_MenuItem_Click);
            // 
            // Attributes_Hide_MenuItem
            // 
            this.Attributes_Hide_MenuItem.Name = "Attributes_Hide_MenuItem";
            this.Attributes_Hide_MenuItem.Size = new System.Drawing.Size(304, 26);
            this.Attributes_Hide_MenuItem.Text = "Hide Attributes List";
            this.Attributes_Hide_MenuItem.Click += new System.EventHandler(this.Attributes_Hide_MenuItem_Click);
            // 
            // DecodeMenu
            // 
            this.DecodeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Decode_GUIDs_MenuItem,
            this.Decode_SIDs_MenuItem,
            this.Decode_UserParams_MenuItem,
            this.Decode_ResolveSids_MenuItem,
            this.Decode_SD_MenuItem,
            this.Decode_OctetString_MenuItem,
            this.Decode_ReplicaLinks_MenuItem,
            this.Decode_PGID_MenuItem,
            this.Decode_TimeBias_MenuItem,
            this.Decode_IgnoreEmpty_MenuItem});
            this.DecodeMenu.Name = "DecodeMenu";
            this.DecodeMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.DecodeMenu.Size = new System.Drawing.Size(73, 24);
            this.DecodeMenu.Text = "Decode";
            // 
            // Decode_GUIDs_MenuItem
            // 
            this.Decode_GUIDs_MenuItem.Checked = true;
            this.Decode_GUIDs_MenuItem.CheckOnClick = true;
            this.Decode_GUIDs_MenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Decode_GUIDs_MenuItem.Name = "Decode_GUIDs_MenuItem";
            this.Decode_GUIDs_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_GUIDs_MenuItem.Text = "GUIDs";
            this.Decode_GUIDs_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_GUIDs_MenuItem_CheckChanged);
            // 
            // Decode_SIDs_MenuItem
            // 
            this.Decode_SIDs_MenuItem.Checked = true;
            this.Decode_SIDs_MenuItem.CheckOnClick = true;
            this.Decode_SIDs_MenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Decode_SIDs_MenuItem.Name = "Decode_SIDs_MenuItem";
            this.Decode_SIDs_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_SIDs_MenuItem.Text = "SIDs";
            this.Decode_SIDs_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_SIDs_MenuItem_CheckChanged);
            // 
            // Decode_UserParams_MenuItem
            // 
            this.Decode_UserParams_MenuItem.CheckOnClick = true;
            this.Decode_UserParams_MenuItem.Name = "Decode_UserParams_MenuItem";
            this.Decode_UserParams_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_UserParams_MenuItem.Text = "userParameters";
            this.Decode_UserParams_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_UserParams_MenuItem_CheckChanged);
            // 
            // Decode_ResolveSids_MenuItem
            // 
            this.Decode_ResolveSids_MenuItem.CheckOnClick = true;
            this.Decode_ResolveSids_MenuItem.Name = "Decode_ResolveSids_MenuItem";
            this.Decode_ResolveSids_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_ResolveSids_MenuItem.Text = "Resolve Sids";
            this.Decode_ResolveSids_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_ResolveSids_MenuItem_CheckChanged);
            // 
            // Decode_SD_MenuItem
            // 
            this.Decode_SD_MenuItem.CheckOnClick = true;
            this.Decode_SD_MenuItem.Name = "Decode_SD_MenuItem";
            this.Decode_SD_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_SD_MenuItem.Text = "SecurityDescriptors";
            this.Decode_SD_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_SD_MenuItem_CheckChanged);
            // 
            // Decode_OctetString_MenuItem
            // 
            this.Decode_OctetString_MenuItem.CheckOnClick = true;
            this.Decode_OctetString_MenuItem.Name = "Decode_OctetString_MenuItem";
            this.Decode_OctetString_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_OctetString_MenuItem.Text = "OctetStrings";
            this.Decode_OctetString_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_OctetString_MenuItem_CheckChanged);
            // 
            // Decode_ReplicaLinks_MenuItem
            // 
            this.Decode_ReplicaLinks_MenuItem.CheckOnClick = true;
            this.Decode_ReplicaLinks_MenuItem.Name = "Decode_ReplicaLinks_MenuItem";
            this.Decode_ReplicaLinks_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_ReplicaLinks_MenuItem.Text = "ReplicaLinks";
            this.Decode_ReplicaLinks_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_ReplicaLinks_MenuItem_CheckChanged);
            // 
            // Decode_PGID_MenuItem
            // 
            this.Decode_PGID_MenuItem.CheckOnClick = true;
            this.Decode_PGID_MenuItem.Name = "Decode_PGID_MenuItem";
            this.Decode_PGID_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_PGID_MenuItem.Text = "memberOf includes primaryGroupID";
            this.Decode_PGID_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_PGID_MenuItem_CheckChanged);
            // 
            // Decode_TimeBias_MenuItem
            // 
            this.Decode_TimeBias_MenuItem.Checked = true;
            this.Decode_TimeBias_MenuItem.CheckOnClick = true;
            this.Decode_TimeBias_MenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Decode_TimeBias_MenuItem.Name = "Decode_TimeBias_MenuItem";
            this.Decode_TimeBias_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_TimeBias_MenuItem.Text = "To local time";
            this.Decode_TimeBias_MenuItem.CheckedChanged += new System.EventHandler(this.Decode_TimeBias_MenuItem_CheckChanged);
            // 
            // Decode_IgnoreEmpty_MenuItem
            // 
            this.Decode_IgnoreEmpty_MenuItem.Checked = true;
            this.Decode_IgnoreEmpty_MenuItem.CheckOnClick = true;
            this.Decode_IgnoreEmpty_MenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Decode_IgnoreEmpty_MenuItem.Name = "Decode_IgnoreEmpty_MenuItem";
            this.Decode_IgnoreEmpty_MenuItem.Size = new System.Drawing.Size(325, 26);
            this.Decode_IgnoreEmpty_MenuItem.Text = "Ignore not present attributes";
            this.Decode_IgnoreEmpty_MenuItem.Click += new System.EventHandler(this.Decode_IgnoreEmpty_MenuItem_Click);
            // 
            // ResultsMenu
            // 
            this.ResultsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Results_ResultsCount_MenuItem,
            this.Results_ShowPartial_MenuItem,
            this.toolStripSeparator30,
            this.Results_CopySelected_MenuItem,
            this.Results_CopyAll_MenuItem,
            this.toolStripSeparator11,
            this.Results_RemeberedCount_MenuItem,
            this.Results_Remembered_MenuItem,
            this.toolStripSeparator23,
            this.Results_Clear_MenuItem,
            this.toolStripSeparator13,
            this.Font_Picker_MenuItem});
            this.ResultsMenu.Name = "ResultsMenu";
            this.ResultsMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Pause)));
            this.ResultsMenu.Size = new System.Drawing.Size(67, 24);
            this.ResultsMenu.Text = "Results";
            // 
            // Results_ResultsCount_MenuItem
            // 
            this.Results_ResultsCount_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Results_MaxResultsCount_MenuItem});
            this.Results_ResultsCount_MenuItem.Name = "Results_ResultsCount_MenuItem";
            this.Results_ResultsCount_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_ResultsCount_MenuItem.Text = "Max results to be returned";
            // 
            // Results_MaxResultsCount_MenuItem
            // 
            this.Results_MaxResultsCount_MenuItem.Name = "Results_MaxResultsCount_MenuItem";
            this.Results_MaxResultsCount_MenuItem.Size = new System.Drawing.Size(100, 27);
            this.Results_MaxResultsCount_MenuItem.Text = "1000";
            this.Results_MaxResultsCount_MenuItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Results_MaxResultsCount_MenuItem_KeyPress);
            // 
            // Results_ShowPartial_MenuItem
            // 
            this.Results_ShowPartial_MenuItem.CheckOnClick = true;
            this.Results_ShowPartial_MenuItem.Name = "Results_ShowPartial_MenuItem";
            this.Results_ShowPartial_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_ShowPartial_MenuItem.Text = "Show partial results";
            this.Results_ShowPartial_MenuItem.CheckedChanged += new System.EventHandler(this.Results_ShowPartial_MenuItem_CheckedChanged);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(254, 6);
            // 
            // Results_CopySelected_MenuItem
            // 
            this.Results_CopySelected_MenuItem.Name = "Results_CopySelected_MenuItem";
            this.Results_CopySelected_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_CopySelected_MenuItem.Text = "Copy selected";
            this.Results_CopySelected_MenuItem.Click += new System.EventHandler(this.Results_CopySelected_MenuItem_Click);
            // 
            // Results_CopyAll_MenuItem
            // 
            this.Results_CopyAll_MenuItem.Name = "Results_CopyAll_MenuItem";
            this.Results_CopyAll_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_CopyAll_MenuItem.Text = "Copy all";
            this.Results_CopyAll_MenuItem.Click += new System.EventHandler(this.Results_CopyAll_MenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(254, 6);
            // 
            // Results_RemeberedCount_MenuItem
            // 
            this.Results_RemeberedCount_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Results_MaxResults_MenuItem});
            this.Results_RemeberedCount_MenuItem.Name = "Results_RemeberedCount_MenuItem";
            this.Results_RemeberedCount_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_RemeberedCount_MenuItem.Text = "Max remebered results";
            // 
            // Results_MaxResults_MenuItem
            // 
            this.Results_MaxResults_MenuItem.Name = "Results_MaxResults_MenuItem";
            this.Results_MaxResults_MenuItem.Size = new System.Drawing.Size(100, 27);
            this.Results_MaxResults_MenuItem.Text = "20";
            this.Results_MaxResults_MenuItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Results_MaxResults_MenuItem_KeyPress);
            // 
            // Results_Remembered_MenuItem
            // 
            this.Results_Remembered_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Results_Remembered_First_MenuItem,
            this.Results_Remembered_Prev_MenuItem,
            this.Results_Remembered_Next_MenuItem,
            this.Results_Remembered_Last_MenuItem,
            this.Results_Remembered_Clear_MenuItem});
            this.Results_Remembered_MenuItem.Enabled = false;
            this.Results_Remembered_MenuItem.Name = "Results_Remembered_MenuItem";
            this.Results_Remembered_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_Remembered_MenuItem.Text = "Remembered results";
            // 
            // Results_Remembered_First_MenuItem
            // 
            this.Results_Remembered_First_MenuItem.Enabled = false;
            this.Results_Remembered_First_MenuItem.Name = "Results_Remembered_First_MenuItem";
            this.Results_Remembered_First_MenuItem.Size = new System.Drawing.Size(180, 26);
            this.Results_Remembered_First_MenuItem.Text = "Show first";
            this.Results_Remembered_First_MenuItem.Click += new System.EventHandler(this.Results_Remembered_First_MenuItem_Click);
            // 
            // Results_Remembered_Prev_MenuItem
            // 
            this.Results_Remembered_Prev_MenuItem.Enabled = false;
            this.Results_Remembered_Prev_MenuItem.Name = "Results_Remembered_Prev_MenuItem";
            this.Results_Remembered_Prev_MenuItem.Size = new System.Drawing.Size(180, 26);
            this.Results_Remembered_Prev_MenuItem.Text = "Show previous";
            this.Results_Remembered_Prev_MenuItem.Click += new System.EventHandler(this.Results_Remembered_Prev_MenuItem_Click);
            // 
            // Results_Remembered_Next_MenuItem
            // 
            this.Results_Remembered_Next_MenuItem.Enabled = false;
            this.Results_Remembered_Next_MenuItem.Name = "Results_Remembered_Next_MenuItem";
            this.Results_Remembered_Next_MenuItem.Size = new System.Drawing.Size(180, 26);
            this.Results_Remembered_Next_MenuItem.Text = "Show next";
            this.Results_Remembered_Next_MenuItem.Click += new System.EventHandler(this.Results_Remembered_Next_MenuItem_Click);
            // 
            // Results_Remembered_Last_MenuItem
            // 
            this.Results_Remembered_Last_MenuItem.Enabled = false;
            this.Results_Remembered_Last_MenuItem.Name = "Results_Remembered_Last_MenuItem";
            this.Results_Remembered_Last_MenuItem.Size = new System.Drawing.Size(180, 26);
            this.Results_Remembered_Last_MenuItem.Text = "Show current";
            this.Results_Remembered_Last_MenuItem.Click += new System.EventHandler(this.Results_Remembered_Last_MenuItem_Click);
            // 
            // Results_Remembered_Clear_MenuItem
            // 
            this.Results_Remembered_Clear_MenuItem.Enabled = false;
            this.Results_Remembered_Clear_MenuItem.Name = "Results_Remembered_Clear_MenuItem";
            this.Results_Remembered_Clear_MenuItem.Size = new System.Drawing.Size(180, 26);
            this.Results_Remembered_Clear_MenuItem.Text = "Clear list";
            this.Results_Remembered_Clear_MenuItem.Click += new System.EventHandler(this.Results_Remembered_Clear_MenuItem_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(254, 6);
            // 
            // Results_Clear_MenuItem
            // 
            this.Results_Clear_MenuItem.Name = "Results_Clear_MenuItem";
            this.Results_Clear_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Results_Clear_MenuItem.Text = "Clear Results";
            this.Results_Clear_MenuItem.Click += new System.EventHandler(this.Results_Clear_MenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(254, 6);
            // 
            // Font_Picker_MenuItem
            // 
            this.Font_Picker_MenuItem.Name = "Font_Picker_MenuItem";
            this.Font_Picker_MenuItem.Size = new System.Drawing.Size(257, 26);
            this.Font_Picker_MenuItem.Text = "Change Font";
            this.Font_Picker_MenuItem.Click += new System.EventHandler(this.Font_Picker_MenuItem_Click);
            // 
            // ToolsMenu
            // 
            this.ToolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tools_Builder_MenuItem,
            this.toolStripSeparator31,
            this.Sizer_MenuItem});
            this.ToolsMenu.Name = "ToolsMenu";
            this.ToolsMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.ToolsMenu.Size = new System.Drawing.Size(57, 24);
            this.ToolsMenu.Text = "Tools";
            // 
            // Tools_Builder_MenuItem
            // 
            this.Tools_Builder_MenuItem.Name = "Tools_Builder_MenuItem";
            this.Tools_Builder_MenuItem.Size = new System.Drawing.Size(221, 26);
            this.Tools_Builder_MenuItem.Text = "DynamicTypeBuilder";
            this.Tools_Builder_MenuItem.Click += new System.EventHandler(this.Tools_Builder_MenuItem_Click);
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(218, 6);
            // 
            // Sizer_MenuItem
            // 
            this.Sizer_MenuItem.Name = "Sizer_MenuItem";
            this.Sizer_MenuItem.Size = new System.Drawing.Size(221, 26);
            this.Sizer_MenuItem.Text = "Sizer";
            this.Sizer_MenuItem.Visible = false;
            // 
            // gbRunInfo
            // 
            this.gbRunInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRunInfo.Controls.Add(this.tvSyncRuns);
            this.gbRunInfo.Controls.Add(this.lvCurRun);
            this.gbRunInfo.Enabled = false;
            this.gbRunInfo.Location = new System.Drawing.Point(3, 438);
            this.gbRunInfo.Margin = new System.Windows.Forms.Padding(4);
            this.gbRunInfo.Name = "gbRunInfo";
            this.gbRunInfo.Padding = new System.Windows.Forms.Padding(4);
            this.gbRunInfo.Size = new System.Drawing.Size(1228, 315);
            this.gbRunInfo.TabIndex = 15;
            this.gbRunInfo.TabStop = false;
            this.gbRunInfo.Text = "Previous DirSync runs";
            this.gbRunInfo.Visible = false;
            // 
            // tvSyncRuns
            // 
            this.tvSyncRuns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSyncRuns.ImageIndex = 0;
            this.tvSyncRuns.ImageList = this.ilObjects;
            this.tvSyncRuns.Location = new System.Drawing.Point(5, 17);
            this.tvSyncRuns.Margin = new System.Windows.Forms.Padding(4);
            this.tvSyncRuns.Name = "tvSyncRuns";
            this.tvSyncRuns.SelectedImageIndex = 0;
            this.tvSyncRuns.Size = new System.Drawing.Size(1213, 201);
            this.tvSyncRuns.TabIndex = 1;
            // 
            // ilObjects
            // 
            this.ilObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilObjects.ImageStream")));
            this.ilObjects.TransparentColor = System.Drawing.Color.Transparent;
            this.ilObjects.Images.SetKeyName(0, "Domain.ico");
            this.ilObjects.Images.SetKeyName(1, "ou.ico");
            this.ilObjects.Images.SetKeyName(2, "runs.ico");
            this.ilObjects.Images.SetKeyName(3, "run.ico");
            this.ilObjects.Images.SetKeyName(4, "config.ico");
            // 
            // lvCurRun
            // 
            this.lvCurRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCurRun.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colValues});
            this.lvCurRun.GridLines = true;
            this.lvCurRun.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvCurRun.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.lvCurRun.Location = new System.Drawing.Point(5, 220);
            this.lvCurRun.Margin = new System.Windows.Forms.Padding(4);
            this.lvCurRun.Name = "lvCurRun";
            this.lvCurRun.Size = new System.Drawing.Size(1215, 88);
            this.lvCurRun.TabIndex = 0;
            this.lvCurRun.UseCompatibleStateImageBehavior = false;
            this.lvCurRun.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Attributes";
            this.colName.Width = 75;
            // 
            // colValues
            // 
            this.colValues.Text = "Values";
            this.colValues.Width = 727;
            // 
            // ilDirSync
            // 
            this.ilDirSync.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilDirSync.ImageStream")));
            this.ilDirSync.TransparentColor = System.Drawing.Color.Transparent;
            this.ilDirSync.Images.SetKeyName(0, "Forest.ico");
            this.ilDirSync.Images.SetKeyName(1, "NamingContext.ico");
            this.ilDirSync.Images.SetKeyName(2, "Filter.ico");
            this.ilDirSync.Images.SetKeyName(3, "Attributes.ico");
            this.ilDirSync.Images.SetKeyName(4, "runs.ico");
            // 
            // tvSync
            // 
            this.tvSync.ImageIndex = 0;
            this.tvSync.ImageList = this.ilDirSync;
            this.tvSync.Location = new System.Drawing.Point(3, 92);
            this.tvSync.Name = "tvSync";
            this.tvSync.SelectedImageIndex = 0;
            this.tvSync.Size = new System.Drawing.Size(1223, 173);
            this.tvSync.TabIndex = 18;
            this.tvSync.Visible = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 756);
            this.Controls.Add(this.gbCon);
            this.Controls.Add(this.MainMenue);
            this.Controls.Add(this.gbQuery);
            this.Controls.Add(this.gbProps);
            this.Controls.Add(this.gbDebug);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.gbRunInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GUI";
            this.Text = "LDAPQueryAnalyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
            this.Load += new System.EventHandler(this.GUI_Load);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.gbCon.ResumeLayout(false);
            this.gbCon.PerformLayout();
            this.CombosContextMenu.ResumeLayout(false);
            this.DCComboContextMenu.ResumeLayout(false);
            this.BaseContextMenu.ResumeLayout(false);
            this.gbQuery.ResumeLayout(false);
            this.gbQuery.PerformLayout();
            this.gbCancel.ResumeLayout(false);
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            this.AttributesContextMenu.ResumeLayout(false);
            this.AttributesSortContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.FilterHistoryContextMenu.ResumeLayout(false);
            this.gbBasetype.ResumeLayout(false);
            this.gbBasetype.PerformLayout();
            this.gbReferral.ResumeLayout(false);
            this.gbReferral.PerformLayout();
            this.gbScope.ResumeLayout(false);
            this.gbScope.PerformLayout();
            this.gbReturn.ResumeLayout(false);
            this.gbReturn.PerformLayout();
            this.FilterContextMenu.ResumeLayout(false);
            this.gbDC_GC.ResumeLayout(false);
            this.gbDC_GC.PerformLayout();
            this.PortsContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nuoPort)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.DebugContextMenu.ResumeLayout(false);
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.gbFind.ResumeLayout(false);
            this.gbFind.PerformLayout();
            this.gbResStore.ResumeLayout(false);
            this.ResultsContextMenu.ResumeLayout(false);
            this.gbDebug.ResumeLayout(false);
            this.gbProps.ResumeLayout(false);
            this.AttributesListContextMenu.ResumeLayout(false);
            this.MainMenue.ResumeLayout(false);
            this.MainMenue.PerformLayout();
            this.gbRunInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbNCs;
        private System.Windows.Forms.ComboBox cmbDCs;
        private System.Windows.Forms.ComboBox cmbDomains;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSBase;
        private System.Windows.Forms.GroupBox gbQuery;
        private System.Windows.Forms.GroupBox gbScope;
        private System.Windows.Forms.RadioButton rbSubtree;
        private System.Windows.Forms.RadioButton rbOneLevel;
        private System.Windows.Forms.RadioButton rbBase;
        private System.Windows.Forms.GroupBox gbDC_GC;
        private System.Windows.Forms.RadioButton rbGC;
        private System.Windows.Forms.RadioButton rbDC;
        private System.Windows.Forms.GroupBox gbReturn;
        private System.Windows.Forms.CheckBox cbFullStats;
        private System.Windows.Forms.CheckBox cbResults;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label labASQ;
        private System.Windows.Forms.TextBox txtAttributes;
        private System.Windows.Forms.ListBox lbDebugLog;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox txtOutputOld;
        private System.Windows.Forms.GroupBox gbDebug;
        private System.Windows.Forms.RadioButton rbPhantomRoot;
        private System.Windows.Forms.Label lbHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar pbLoad;
        private System.Windows.Forms.GroupBox gbProps;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.ListBox lbAttribs;
        private System.Windows.Forms.Timer ListTimer;
        private System.Windows.Forms.MenuStrip MainMenue;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem File_Close_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem QueryMenu;
        private System.Windows.Forms.ToolStripMenuItem Query_Execute_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem FilterMenu;
        private System.Windows.Forms.ToolStripMenuItem Filter_Wizard_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_ClearHistory_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem AttributesMenu;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Load_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Hide_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResultsMenu;
        private System.Windows.Forms.ToolStripMenuItem Results_CopyAll_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Clear_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Provider_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Scope_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Return_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Return_Results_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Return_Statistics_MenuItem;
        private System.Windows.Forms.ToolStripComboBox Query_Provider_Combo_MenuItem;
        private System.Windows.Forms.ToolStripComboBox Query_Scope_Combo_MenuItem;
        private System.Windows.Forms.ContextMenuStrip AttributesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Paste_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Load_ContextItem;
        private System.Windows.Forms.ContextMenuStrip AttributesListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AttributesList_Add_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem AttributesList_Hide_ContextItem;
        private System.Windows.Forms.ContextMenuStrip ResultsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Results_CopyAll_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Clear_ContextItem;
        private System.Windows.Forms.ContextMenuStrip FilterContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Filter_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_Paste_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_Wizard_ContextItem;
        private System.Windows.Forms.ContextMenuStrip CombosContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Combos_Copy_ContextItem;
        private System.Windows.Forms.ContextMenuStrip FilterHistoryContextMenu;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_Clear_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem DecodeMenu;
        private System.Windows.Forms.ToolStripMenuItem Decode_SD_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_GUIDs_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_SIDs_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_ResolveSids_MenuItem;
        private System.Windows.Forms.ContextMenuStrip DebugContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Debug_Copy_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Debug_Clear_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Combos_CopyAll_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Search_MenuItem;
        private System.Windows.Forms.ToolStripTextBox Result_Search_String_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Search_FindNext_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Search_Find_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem AttributesList_CopyAll_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem AttributesList_Copy_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem Font_Picker_MenuItem;
        private System.Windows.Forms.FontDialog diaFont;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem Result_Font_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConnectionMenu;
        private System.Windows.Forms.ToolStripMenuItem Connection_ConnectForest_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Connection_PassCredentials_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem Query_Async_MenuItem;
        private System.Windows.Forms.GroupBox gbReferral;
        private System.Windows.Forms.RadioButton rbRefNone;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.ContextMenuStrip BaseContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Base_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Base_Paste_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem Base_Browse_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsMenu;
        private System.Windows.Forms.ToolStripMenuItem Tools_Builder_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Browse_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Clear_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem Filter_Clear_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_PGID_MenuItem;
        private System.Windows.Forms.RadioButton rbRootDSE;
        private System.Windows.Forms.ToolStripMenuItem Connection_Ports_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Ports_LDAP_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Ports_GC_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripTextBox Ports_LDAP_Value_Text;
        private System.Windows.Forms.ToolStripTextBox Ports_GC_Value_Text;
        private System.Windows.Forms.ToolStripMenuItem Connection_Refresh_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem Query_Return_RootDSEExtended_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_OctetString_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_ValRange_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_ReplicaLinks_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_UserParams_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_ShowDeleted_MenuItem;
        private System.Windows.Forms.GroupBox gbResStore;
        private System.Windows.Forms.Button cmdLastResult;
        private System.Windows.Forms.Button cmdFirstResult;
        private System.Windows.Forms.Button cmdNextResult;
        private System.Windows.Forms.Button cmdPrevResult;
        private System.Windows.Forms.Button cmdClearResults;
        private System.Windows.Forms.ToolStripMenuItem Result_Remembered_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem Result_Remembered_First_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Remembered_Previous_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Remembered_Next_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Remembered_Last_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Result_Remembered_Clear_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Remembered_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Remembered_First_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Remembered_Prev_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Remembered_Next_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Remembered_Last_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_Remembered_Clear_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.Button cmdLoadSync;
        private System.Windows.Forms.RadioButton rbDirSync;
        private System.Windows.Forms.RadioButton rbPagedQuery;
        private System.Windows.Forms.ToolStripMenuItem Query_Type_MenuItem;
        private System.Windows.Forms.ToolStripComboBox Query_Type_Combo_MenuItem;
        private System.Windows.Forms.GroupBox gbRunInfo;
        private System.Windows.Forms.ListView lvCurRun;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colValues;
        private System.Windows.Forms.TreeView tvSyncRuns;
        private System.Windows.Forms.ImageList ilObjects;
        private System.Windows.Forms.ToolStripMenuItem Results_CopySelected_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Results_CopySelected_MenuItem;
        private System.Windows.Forms.RadioButton rbNonPagedQuery;
        private System.Windows.Forms.ToolStripMenuItem Query_AutoPage_MenuItem;
        private System.Windows.Forms.NumericUpDown nuoPort;
        private System.Windows.Forms.RadioButton rbCustomPort;
        private System.Windows.Forms.ToolStripMenuItem Query_ASQ_MenuItem;
        private System.Windows.Forms.CheckBox cbASQ;
        private System.Windows.Forms.ContextMenuStrip PortsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Ports_SetLDAP_ContextMenu;
        private System.Windows.Forms.ToolStripTextBox Ports_SetLDAPValue_Text;
        private System.Windows.Forms.ToolStripMenuItem Ports_SetGC_ContextMenu;
        private System.Windows.Forms.ToolStripTextBox Port_SetGCValue_Text;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem Connection_TimeOut_MenuItem;
        private System.Windows.Forms.ToolStripTextBox Connection_TimeOutValue_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_ShowRecycled_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserMenu;
        private System.Windows.Forms.ToolStripMenuItem User_Elevate_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem User_Runas_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem User_Runas_Elevate_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem User_Restart_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem User_Elevate_2nd_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem User_Restart_Elevate_MenuItem;
        private System.Windows.Forms.Button cmdRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem Base_Restore_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_TimeBias_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Sort_Results_Asc_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Query_Sort_Results_Desc_MenuItem;
        private System.Windows.Forms.GroupBox gbBasetype;
        private System.Windows.Forms.RadioButton rbDN;
        private System.Windows.Forms.CheckBox cbRefExternal;
        private System.Windows.Forms.CheckBox cbRefSubordinate;
        private System.Windows.Forms.CheckBox cbRootDseExt;
        private System.Windows.Forms.TextBox txtASQAttribs;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_DeleteCurrent_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Query_BaseType_MenuItem;
        private System.Windows.Forms.ToolStripComboBox Query_BaseType_Combo_MenuItem;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem Results_ShowPartial_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripMenuItem Results_RemeberedCount_MenuItem;
        private System.Windows.Forms.ToolStripTextBox Results_MaxResults_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Results_ResultsCount_MenuItem;
        private System.Windows.Forms.ToolStripTextBox Results_MaxResultsCount_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
        private System.Windows.Forms.ToolStripMenuItem Sizer_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Decode_IgnoreEmpty_MenuItem;
        private System.Windows.Forms.Button cmdDC;
        private System.Windows.Forms.ContextMenuStrip DCComboContextMenu;
        private System.Windows.Forms.ToolStripMenuItem DCCombo_Info_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
        private System.Windows.Forms.ToolStripMenuItem DCCombo_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem DCCombo_CopyAll_ContextItem;
        private System.Windows.Forms.ListView lvFilterHistory;
        private System.Windows.Forms.ColumnHeader colFilter;
        private System.Windows.Forms.ColumnHeader colAttribs;
        private System.Windows.Forms.ColumnHeader colBase;
        private System.Windows.Forms.ColumnHeader colCheck;
        private System.Windows.Forms.ColumnHeader colScope;
        private System.Windows.Forms.ColumnHeader colPort;
        private System.Windows.Forms.ColumnHeader colDC;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_DC_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_Base_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_Scope_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_Port_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_Bulk_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_DeleteSelected_ContextItem;
        private System.Windows.Forms.ListView lvSort;
        private System.Windows.Forms.ColumnHeader colReverse;
        private System.Windows.Forms.ColumnHeader colAttrib;
        private System.Windows.Forms.Label labSort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbSort;
        private System.Windows.Forms.Button cmbHistory;
        private System.Windows.Forms.ToolStripMenuItem Query_Sort_MenuItem;
        private System.Windows.Forms.ContextMenuStrip AttributesSortContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AttributesSort_Clear_ContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator35;
        private System.Windows.Forms.ToolStripMenuItem AttributesSort_Load_ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AttributesSort_Remove_ContextMenu;
        private System.Windows.Forms.ComboBox cmbAttribs;
        private System.Windows.Forms.ColumnHeader colASQ;
        private System.Windows.Forms.ColumnHeader colSort;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_ASQ_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_Sort_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem FilterHistory_All_ContextItem;
        private System.Windows.Forms.ImageList iSort;
        private System.Windows.Forms.TextBox txtSort;
        private System.Windows.Forms.CheckBox cbReverse;
        private System.Windows.Forms.GroupBox gbCancel;
        private System.Windows.Forms.Button cmdCancelAll;
        private System.Windows.Forms.Button cmdCancelQuery;
        private System.Windows.Forms.GroupBox gbFind;
        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Button cmdCloseFind;
        private System.Windows.Forms.ToolStripMenuItem User_Whoami_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator34;
        private System.Windows.Forms.ImageList ilDirSync;
        private System.Windows.Forms.TreeView tvSync;
    }
}