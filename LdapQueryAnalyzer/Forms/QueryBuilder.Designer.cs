namespace CodingFromTheField.LdapQueryAnalyzer
{
    partial class QueryBuilder
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryBuilder));
            this.gbGroups = new System.Windows.Forms.GroupBox();
            this.cmdUnDo = new System.Windows.Forms.Button();
            this.cmdClearFilter = new System.Windows.Forms.Button();
            this.cmdSet = new System.Windows.Forms.Button();
            this.cmdCloseAllGroups = new System.Windows.Forms.Button();
            this.cmdNewOrGroup = new System.Windows.Forms.Button();
            this.cmdCloseGroup = new System.Windows.Forms.Button();
            this.cmdNewAndGroup = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.FilterContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FQBilter_NewAnd_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FQBilter_NewOr_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.FQBilter_Close_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FQBilter_CloseAll_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.FQBilter_Clear_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbClasses = new System.Windows.Forms.GroupBox();
            this.cmdClassNeq = new System.Windows.Forms.Button();
            this.cmdClassEq = new System.Windows.Forms.Button();
            this.lbClasses = new System.Windows.Forms.ListBox();
            this.ClassesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Classes_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Classes_CopyAll_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Classes_Equals__ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Classes_NotEquals_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.cmdChain = new System.Windows.Forms.Button();
            this.cmdNEQ = new System.Windows.Forms.Button();
            this.cmdEQ = new System.Windows.Forms.Button();
            this.cmdNOR = new System.Windows.Forms.Button();
            this.cmdNAND = new System.Windows.Forms.Button();
            this.cmdLEQ = new System.Windows.Forms.Button();
            this.cmdGEQ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGuid = new System.Windows.Forms.CheckBox();
            this.cmdSetLogicalFilter = new System.Windows.Forms.Button();
            this.cmdHideEnum = new System.Windows.Forms.Button();
            this.lbAttribs = new System.Windows.Forms.ListBox();
            this.AttributesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Attributes_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_CopyAll_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Attributes_Equals_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_NotEquals_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Attributes_And_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Or_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Chain_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.cmdOR = new System.Windows.Forms.Button();
            this.cmdAND = new System.Windows.Forms.Button();
            this.lvEnum = new System.Windows.Forms.ListView();
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListTimer = new System.Windows.Forms.Timer(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.File_Apply_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.File_Close_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Filter_And_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Filter_Or_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Filter_CloseCurrent_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Filter_CloseAll_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Filter_Clear_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClassesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Classes_Equals_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Classes_NotEquals_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttributesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Equal_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_NotEqual_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_LessOrEqual_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_GreaterOrEqual_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.Attributes_And_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Nand_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Or_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Attributes_Nor_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.Attributes_Chain_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.ValueContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Value_Copy_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Value_Paste_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.Value_Claer_ContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbGroups.SuspendLayout();
            this.FilterContextMenu.SuspendLayout();
            this.gbClasses.SuspendLayout();
            this.ClassesContextMenu.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.AttributesContextMenu.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.ValueContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGroups
            // 
            this.gbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGroups.Controls.Add(this.cmdUnDo);
            this.gbGroups.Controls.Add(this.cmdClearFilter);
            this.gbGroups.Controls.Add(this.cmdSet);
            this.gbGroups.Controls.Add(this.cmdCloseAllGroups);
            this.gbGroups.Controls.Add(this.cmdNewOrGroup);
            this.gbGroups.Controls.Add(this.cmdCloseGroup);
            this.gbGroups.Controls.Add(this.cmdNewAndGroup);
            this.gbGroups.Controls.Add(this.txtFilter);
            this.gbGroups.Location = new System.Drawing.Point(2, 28);
            this.gbGroups.Name = "gbGroups";
            this.gbGroups.Size = new System.Drawing.Size(587, 123);
            this.gbGroups.TabIndex = 22;
            this.gbGroups.TabStop = false;
            this.gbGroups.Text = "Filter";
            // 
            // cmdUnDo
            // 
            this.cmdUnDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUnDo.Enabled = false;
            this.cmdUnDo.ForeColor = System.Drawing.Color.Maroon;
            this.cmdUnDo.Location = new System.Drawing.Point(492, 14);
            this.cmdUnDo.Name = "cmdUnDo";
            this.cmdUnDo.Size = new System.Drawing.Size(43, 23);
            this.cmdUnDo.TabIndex = 20;
            this.cmdUnDo.Text = "UnDo";
            this.cmdUnDo.UseVisualStyleBackColor = true;
            this.cmdUnDo.Click += new System.EventHandler(this.cmdUnDo_Click);
            // 
            // cmdClearFilter
            // 
            this.cmdClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClearFilter.ForeColor = System.Drawing.Color.Maroon;
            this.cmdClearFilter.Location = new System.Drawing.Point(539, 14);
            this.cmdClearFilter.Name = "cmdClearFilter";
            this.cmdClearFilter.Size = new System.Drawing.Size(43, 23);
            this.cmdClearFilter.TabIndex = 19;
            this.cmdClearFilter.Text = "Clear";
            this.cmdClearFilter.UseVisualStyleBackColor = true;
            this.cmdClearFilter.Click += new System.EventHandler(this.cmdClearFilter_Click);
            // 
            // cmdSet
            // 
            this.cmdSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdSet.Location = new System.Drawing.Point(539, 95);
            this.cmdSet.Name = "cmdSet";
            this.cmdSet.Size = new System.Drawing.Size(43, 23);
            this.cmdSet.TabIndex = 13;
            this.cmdSet.Text = "Apply";
            this.cmdSet.UseVisualStyleBackColor = true;
            this.cmdSet.Click += new System.EventHandler(this.cmdSet_Click);
            // 
            // cmdCloseAllGroups
            // 
            this.cmdCloseAllGroups.Enabled = false;
            this.cmdCloseAllGroups.Location = new System.Drawing.Point(105, 19);
            this.cmdCloseAllGroups.Name = "cmdCloseAllGroups";
            this.cmdCloseAllGroups.Size = new System.Drawing.Size(27, 20);
            this.cmdCloseAllGroups.TabIndex = 18;
            this.cmdCloseAllGroups.Text = "))";
            this.cmdCloseAllGroups.UseVisualStyleBackColor = true;
            this.cmdCloseAllGroups.Click += new System.EventHandler(this.cmdCloseAllGroups_Click);
            // 
            // cmdNewOrGroup
            // 
            this.cmdNewOrGroup.Location = new System.Drawing.Point(39, 19);
            this.cmdNewOrGroup.Name = "cmdNewOrGroup";
            this.cmdNewOrGroup.Size = new System.Drawing.Size(27, 20);
            this.cmdNewOrGroup.TabIndex = 16;
            this.cmdNewOrGroup.Text = "( |";
            this.cmdNewOrGroup.UseVisualStyleBackColor = true;
            this.cmdNewOrGroup.Click += new System.EventHandler(this.cmdNewOrGroup_Click);
            // 
            // cmdCloseGroup
            // 
            this.cmdCloseGroup.Enabled = false;
            this.cmdCloseGroup.Location = new System.Drawing.Point(72, 19);
            this.cmdCloseGroup.Name = "cmdCloseGroup";
            this.cmdCloseGroup.Size = new System.Drawing.Size(27, 20);
            this.cmdCloseGroup.TabIndex = 17;
            this.cmdCloseGroup.Text = ")";
            this.cmdCloseGroup.UseVisualStyleBackColor = true;
            this.cmdCloseGroup.Click += new System.EventHandler(this.cmdCloseGroup_Click);
            // 
            // cmdNewAndGroup
            // 
            this.cmdNewAndGroup.Location = new System.Drawing.Point(6, 19);
            this.cmdNewAndGroup.Name = "cmdNewAndGroup";
            this.cmdNewAndGroup.Size = new System.Drawing.Size(27, 20);
            this.cmdNewAndGroup.TabIndex = 15;
            this.cmdNewAndGroup.Text = "( &&";
            this.cmdNewAndGroup.UseVisualStyleBackColor = true;
            this.cmdNewAndGroup.Click += new System.EventHandler(this.cmdNewAndGroup_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.ContextMenuStrip = this.FilterContextMenu;
            this.txtFilter.Location = new System.Drawing.Point(6, 42);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(577, 49);
            this.txtFilter.TabIndex = 14;
            // 
            // FilterContextMenu
            // 
            this.FilterContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FQBilter_NewAnd_MenuItem,
            this.FQBilter_NewOr_MenuItem,
            this.toolStripSeparator8,
            this.FQBilter_Close_MenuItem,
            this.FQBilter_CloseAll_MenuItem,
            this.toolStripSeparator9,
            this.FQBilter_Clear_MenuItem});
            this.FilterContextMenu.Name = "FilterContextMenu";
            this.FilterContextMenu.Size = new System.Drawing.Size(180, 126);
            // 
            // FQBilter_NewAnd_MenuItem
            // 
            this.FQBilter_NewAnd_MenuItem.Name = "FQBilter_NewAnd_MenuItem";
            this.FQBilter_NewAnd_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.FQBilter_NewAnd_MenuItem.Text = "New AND group (&&";
            this.FQBilter_NewAnd_MenuItem.Click += new System.EventHandler(this.Filter_And_MenuItem_Click);
            // 
            // FQBilter_NewOr_MenuItem
            // 
            this.FQBilter_NewOr_MenuItem.Name = "FQBilter_NewOr_MenuItem";
            this.FQBilter_NewOr_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.FQBilter_NewOr_MenuItem.Text = "New OR group (|";
            this.FQBilter_NewOr_MenuItem.Click += new System.EventHandler(this.Filter_Or_MenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(176, 6);
            // 
            // FQBilter_Close_MenuItem
            // 
            this.FQBilter_Close_MenuItem.Name = "FQBilter_Close_MenuItem";
            this.FQBilter_Close_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.FQBilter_Close_MenuItem.Text = "Close current group";
            this.FQBilter_Close_MenuItem.Click += new System.EventHandler(this.Filter_CloseCurrent_MenuItem_Click);
            // 
            // FQBilter_CloseAll_MenuItem
            // 
            this.FQBilter_CloseAll_MenuItem.Name = "FQBilter_CloseAll_MenuItem";
            this.FQBilter_CloseAll_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.FQBilter_CloseAll_MenuItem.Text = "Close all groups";
            this.FQBilter_CloseAll_MenuItem.Click += new System.EventHandler(this.Filter_CloseAll_MenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(176, 6);
            // 
            // FQBilter_Clear_MenuItem
            // 
            this.FQBilter_Clear_MenuItem.Name = "FQBilter_Clear_MenuItem";
            this.FQBilter_Clear_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.FQBilter_Clear_MenuItem.Text = "Clear Filter";
            this.FQBilter_Clear_MenuItem.Click += new System.EventHandler(this.Filter_Clear_MenuItem_Click);
            // 
            // gbClasses
            // 
            this.gbClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbClasses.Controls.Add(this.cmdClassNeq);
            this.gbClasses.Controls.Add(this.cmdClassEq);
            this.gbClasses.Controls.Add(this.lbClasses);
            this.gbClasses.Location = new System.Drawing.Point(6, 8);
            this.gbClasses.Name = "gbClasses";
            this.gbClasses.Size = new System.Drawing.Size(260, 200);
            this.gbClasses.TabIndex = 20;
            this.gbClasses.TabStop = false;
            this.gbClasses.Text = "ObjectClasses";
            // 
            // cmdClassNeq
            // 
            this.cmdClassNeq.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClassNeq.Location = new System.Drawing.Point(226, 52);
            this.cmdClassNeq.Name = "cmdClassNeq";
            this.cmdClassNeq.Size = new System.Drawing.Size(27, 20);
            this.cmdClassNeq.TabIndex = 2;
            this.cmdClassNeq.Text = "!=";
            this.cmdClassNeq.UseVisualStyleBackColor = true;
            this.cmdClassNeq.Click += new System.EventHandler(this.cmdClassNeq_Click);
            // 
            // cmdClassEq
            // 
            this.cmdClassEq.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClassEq.Location = new System.Drawing.Point(226, 26);
            this.cmdClassEq.Name = "cmdClassEq";
            this.cmdClassEq.Size = new System.Drawing.Size(27, 20);
            this.cmdClassEq.TabIndex = 1;
            this.cmdClassEq.Text = "=";
            this.cmdClassEq.UseVisualStyleBackColor = true;
            this.cmdClassEq.Click += new System.EventHandler(this.cmdClassEq_Click);
            // 
            // lbClasses
            // 
            this.lbClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbClasses.ContextMenuStrip = this.ClassesContextMenu;
            this.lbClasses.FormattingEnabled = true;
            this.lbClasses.Location = new System.Drawing.Point(6, 21);
            this.lbClasses.Name = "lbClasses";
            this.lbClasses.Size = new System.Drawing.Size(214, 173);
            this.lbClasses.TabIndex = 0;
            this.lbClasses.SelectedIndexChanged += new System.EventHandler(this.lbClasses_SelectedIndexChanged);
            this.lbClasses.DoubleClick += new System.EventHandler(this.lbClasses_DoubleClick);
            this.lbClasses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBox_KeyDownEvents);
            this.lbClasses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListBox_KeyEvents);
            // 
            // ClassesContextMenu
            // 
            this.ClassesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Classes_Copy_ContextItem,
            this.Classes_CopyAll_ContextItem,
            this.toolStripSeparator2,
            this.Classes_Equals__ContextItem,
            this.Classes_NotEquals_ContextItem});
            this.ClassesContextMenu.Name = "ContextMenuClasses";
            this.ClassesContextMenu.Size = new System.Drawing.Size(150, 98);
            // 
            // Classes_Copy_ContextItem
            // 
            this.Classes_Copy_ContextItem.Name = "Classes_Copy_ContextItem";
            this.Classes_Copy_ContextItem.Size = new System.Drawing.Size(149, 22);
            this.Classes_Copy_ContextItem.Text = "Copy Selected";
            this.Classes_Copy_ContextItem.Click += new System.EventHandler(this.Classes_Copy_ContextItem_Click);
            // 
            // Classes_CopyAll_ContextItem
            // 
            this.Classes_CopyAll_ContextItem.Name = "Classes_CopyAll_ContextItem";
            this.Classes_CopyAll_ContextItem.Size = new System.Drawing.Size(149, 22);
            this.Classes_CopyAll_ContextItem.Text = "Copy All";
            this.Classes_CopyAll_ContextItem.Click += new System.EventHandler(this.Classes_CopyAll_ContextItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // Classes_Equals__ContextItem
            // 
            this.Classes_Equals__ContextItem.Name = "Classes_Equals__ContextItem";
            this.Classes_Equals__ContextItem.Size = new System.Drawing.Size(149, 22);
            this.Classes_Equals__ContextItem.Text = "Equals";
            this.Classes_Equals__ContextItem.Click += new System.EventHandler(this.Classes_Equals_MenuItem_Click);
            // 
            // Classes_NotEquals_ContextItem
            // 
            this.Classes_NotEquals_ContextItem.Name = "Classes_NotEquals_ContextItem";
            this.Classes_NotEquals_ContextItem.Size = new System.Drawing.Size(149, 22);
            this.Classes_NotEquals_ContextItem.Text = "Not Equals";
            this.Classes_NotEquals_ContextItem.Click += new System.EventHandler(this.Classes_NotEquals_MenuItem_Click);
            // 
            // gbAttributes
            // 
            this.gbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAttributes.Controls.Add(this.cmdChain);
            this.gbAttributes.Controls.Add(this.cmdNEQ);
            this.gbAttributes.Controls.Add(this.cmdEQ);
            this.gbAttributes.Controls.Add(this.cmdNOR);
            this.gbAttributes.Controls.Add(this.cmdNAND);
            this.gbAttributes.Controls.Add(this.cmdLEQ);
            this.gbAttributes.Controls.Add(this.cmdGEQ);
            this.gbAttributes.Controls.Add(this.label1);
            this.gbAttributes.Controls.Add(this.cbGuid);
            this.gbAttributes.Controls.Add(this.cmdSetLogicalFilter);
            this.gbAttributes.Controls.Add(this.cmdHideEnum);
            this.gbAttributes.Controls.Add(this.lbAttribs);
            this.gbAttributes.Controls.Add(this.txtVal);
            this.gbAttributes.Controls.Add(this.cmdOR);
            this.gbAttributes.Controls.Add(this.cmdAND);
            this.gbAttributes.Controls.Add(this.lvEnum);
            this.gbAttributes.Location = new System.Drawing.Point(269, 8);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Size = new System.Drawing.Size(314, 200);
            this.gbAttributes.TabIndex = 21;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Attributes";
            // 
            // cmdChain
            // 
            this.cmdChain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChain.Enabled = false;
            this.cmdChain.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChain.Location = new System.Drawing.Point(237, 113);
            this.cmdChain.Name = "cmdChain";
            this.cmdChain.Size = new System.Drawing.Size(30, 20);
            this.cmdChain.TabIndex = 10;
            this.cmdChain.Text = "->>";
            this.cmdChain.UseVisualStyleBackColor = true;
            this.cmdChain.Click += new System.EventHandler(this.cmdChain_Click);
            // 
            // cmdNEQ
            // 
            this.cmdNEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNEQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNEQ.Location = new System.Drawing.Point(237, 44);
            this.cmdNEQ.Name = "cmdNEQ";
            this.cmdNEQ.Size = new System.Drawing.Size(30, 20);
            this.cmdNEQ.TabIndex = 7;
            this.cmdNEQ.Text = "!=";
            this.cmdNEQ.UseVisualStyleBackColor = true;
            this.cmdNEQ.Click += new System.EventHandler(this.cmdNEQ_Click);
            // 
            // cmdEQ
            // 
            this.cmdEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEQ.Location = new System.Drawing.Point(237, 21);
            this.cmdEQ.Name = "cmdEQ";
            this.cmdEQ.Size = new System.Drawing.Size(30, 20);
            this.cmdEQ.TabIndex = 6;
            this.cmdEQ.Text = "=";
            this.cmdEQ.UseVisualStyleBackColor = true;
            this.cmdEQ.Click += new System.EventHandler(this.cmdEQ_Click);
            // 
            // cmdNOR
            // 
            this.cmdNOR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNOR.Enabled = false;
            this.cmdNOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNOR.Location = new System.Drawing.Point(278, 90);
            this.cmdNOR.Name = "cmdNOR";
            this.cmdNOR.Size = new System.Drawing.Size(30, 20);
            this.cmdNOR.TabIndex = 30;
            this.cmdNOR.Text = "!|";
            this.cmdNOR.UseVisualStyleBackColor = true;
            this.cmdNOR.Click += new System.EventHandler(this.cmdNor_Click);
            // 
            // cmdNAND
            // 
            this.cmdNAND.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNAND.Enabled = false;
            this.cmdNAND.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNAND.Location = new System.Drawing.Point(237, 90);
            this.cmdNAND.Name = "cmdNAND";
            this.cmdNAND.Size = new System.Drawing.Size(30, 20);
            this.cmdNAND.TabIndex = 29;
            this.cmdNAND.Text = "!&&";
            this.cmdNAND.UseVisualStyleBackColor = true;
            this.cmdNAND.Click += new System.EventHandler(this.cmdNand_Click);
            // 
            // cmdLEQ
            // 
            this.cmdLEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLEQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLEQ.Location = new System.Drawing.Point(278, 21);
            this.cmdLEQ.Name = "cmdLEQ";
            this.cmdLEQ.Size = new System.Drawing.Size(30, 20);
            this.cmdLEQ.TabIndex = 25;
            this.cmdLEQ.Text = "<=";
            this.cmdLEQ.UseVisualStyleBackColor = true;
            this.cmdLEQ.Click += new System.EventHandler(this.cmdLEQ_Click);
            // 
            // cmdGEQ
            // 
            this.cmdGEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGEQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGEQ.Location = new System.Drawing.Point(278, 44);
            this.cmdGEQ.Name = "cmdGEQ";
            this.cmdGEQ.Size = new System.Drawing.Size(30, 20);
            this.cmdGEQ.TabIndex = 26;
            this.cmdGEQ.Text = ">=";
            this.cmdGEQ.UseVisualStyleBackColor = true;
            this.cmdGEQ.Click += new System.EventHandler(this.cmdGEQ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Value";
            // 
            // cbGuid
            // 
            this.cbGuid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGuid.AutoSize = true;
            this.cbGuid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGuid.Location = new System.Drawing.Point(254, 131);
            this.cbGuid.Name = "cbGuid";
            this.cbGuid.Size = new System.Drawing.Size(55, 17);
            this.cbGuid.TabIndex = 5;
            this.cbGuid.Text = "isGuid";
            this.cbGuid.UseVisualStyleBackColor = true;
            this.cbGuid.CheckedChanged += new System.EventHandler(this.cbGuid_CheckedChanged);
            // 
            // cmdSetLogicalFilter
            // 
            this.cmdSetLogicalFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetLogicalFilter.Location = new System.Drawing.Point(262, 21);
            this.cmdSetLogicalFilter.Name = "cmdSetLogicalFilter";
            this.cmdSetLogicalFilter.Size = new System.Drawing.Size(46, 20);
            this.cmdSetLogicalFilter.TabIndex = 11;
            this.cmdSetLogicalFilter.Text = "set";
            this.cmdSetLogicalFilter.UseVisualStyleBackColor = true;
            this.cmdSetLogicalFilter.Visible = false;
            this.cmdSetLogicalFilter.Click += new System.EventHandler(this.cmdSetLogicalFilter_Click);
            // 
            // cmdHideEnum
            // 
            this.cmdHideEnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHideEnum.Location = new System.Drawing.Point(262, 44);
            this.cmdHideEnum.Name = "cmdHideEnum";
            this.cmdHideEnum.Size = new System.Drawing.Size(46, 20);
            this.cmdHideEnum.TabIndex = 12;
            this.cmdHideEnum.Text = "hide";
            this.cmdHideEnum.UseVisualStyleBackColor = true;
            this.cmdHideEnum.Visible = false;
            this.cmdHideEnum.Click += new System.EventHandler(this.cmdHideEnum_Click);
            // 
            // lbAttribs
            // 
            this.lbAttribs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAttribs.ContextMenuStrip = this.AttributesContextMenu;
            this.lbAttribs.FormattingEnabled = true;
            this.lbAttribs.Location = new System.Drawing.Point(6, 21);
            this.lbAttribs.Name = "lbAttribs";
            this.lbAttribs.Size = new System.Drawing.Size(225, 108);
            this.lbAttribs.TabIndex = 3;
            this.lbAttribs.SelectedIndexChanged += new System.EventHandler(this.lbAttribs_SelectedIndexChanged);
            this.lbAttribs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBox_KeyDownEvents);
            this.lbAttribs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListBox_KeyEvents);
            // 
            // AttributesContextMenu
            // 
            this.AttributesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Attributes_Copy_ContextItem,
            this.Attributes_CopyAll_ContextItem,
            this.toolStripSeparator1,
            this.Attributes_Equals_ContextItem,
            this.Attributes_NotEquals_ContextItem,
            this.toolStripSeparator3,
            this.Attributes_And_ContextItem,
            this.Attributes_Or_ContextItem,
            this.Attributes_Chain_ContextItem});
            this.AttributesContextMenu.Name = "ContextMenuAttribs";
            this.AttributesContextMenu.Size = new System.Drawing.Size(156, 170);
            // 
            // Attributes_Copy_ContextItem
            // 
            this.Attributes_Copy_ContextItem.Name = "Attributes_Copy_ContextItem";
            this.Attributes_Copy_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_Copy_ContextItem.Text = "Copy Selected";
            this.Attributes_Copy_ContextItem.Click += new System.EventHandler(this.Attributes_Copy_ContextItem_Click);
            // 
            // Attributes_CopyAll_ContextItem
            // 
            this.Attributes_CopyAll_ContextItem.Name = "Attributes_CopyAll_ContextItem";
            this.Attributes_CopyAll_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_CopyAll_ContextItem.Text = "Copy All";
            this.Attributes_CopyAll_ContextItem.Click += new System.EventHandler(this.Attributes_CopyAll_ContextItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // Attributes_Equals_ContextItem
            // 
            this.Attributes_Equals_ContextItem.Name = "Attributes_Equals_ContextItem";
            this.Attributes_Equals_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_Equals_ContextItem.Text = "Equals";
            this.Attributes_Equals_ContextItem.Click += new System.EventHandler(this.Attributes_Equal_MenuItem_Click);
            // 
            // Attributes_NotEquals_ContextItem
            // 
            this.Attributes_NotEquals_ContextItem.Name = "Attributes_NotEquals_ContextItem";
            this.Attributes_NotEquals_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_NotEquals_ContextItem.Text = "Not Equals";
            this.Attributes_NotEquals_ContextItem.Click += new System.EventHandler(this.Attributes_NotEqual_MenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // Attributes_And_ContextItem
            // 
            this.Attributes_And_ContextItem.Name = "Attributes_And_ContextItem";
            this.Attributes_And_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_And_ContextItem.Text = "Bitwise AND";
            this.Attributes_And_ContextItem.Click += new System.EventHandler(this.Attributes_And_MenuItem_Click);
            // 
            // Attributes_Or_ContextItem
            // 
            this.Attributes_Or_ContextItem.Name = "Attributes_Or_ContextItem";
            this.Attributes_Or_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_Or_ContextItem.Text = "Bitwise OR";
            this.Attributes_Or_ContextItem.Click += new System.EventHandler(this.Attributes_Or_MenuItem_Click);
            // 
            // Attributes_Chain_ContextItem
            // 
            this.Attributes_Chain_ContextItem.Name = "Attributes_Chain_ContextItem";
            this.Attributes_Chain_ContextItem.Size = new System.Drawing.Size(155, 22);
            this.Attributes_Chain_ContextItem.Text = "Match In Chain";
            this.Attributes_Chain_ContextItem.Click += new System.EventHandler(this.Attributes_Chain_MenuItem_Click);
            // 
            // txtVal
            // 
            this.txtVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVal.Location = new System.Drawing.Point(6, 148);
            this.txtVal.Multiline = true;
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(302, 46);
            this.txtVal.TabIndex = 4;
            this.txtVal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVal_KeyDown);
            this.txtVal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVal_KeyUp);
            // 
            // cmdOR
            // 
            this.cmdOR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOR.Enabled = false;
            this.cmdOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOR.Location = new System.Drawing.Point(278, 67);
            this.cmdOR.Name = "cmdOR";
            this.cmdOR.Size = new System.Drawing.Size(30, 20);
            this.cmdOR.TabIndex = 9;
            this.cmdOR.Text = "|";
            this.cmdOR.UseVisualStyleBackColor = true;
            this.cmdOR.Click += new System.EventHandler(this.cmdOr_Click);
            // 
            // cmdAND
            // 
            this.cmdAND.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAND.Enabled = false;
            this.cmdAND.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAND.Location = new System.Drawing.Point(237, 67);
            this.cmdAND.Name = "cmdAND";
            this.cmdAND.Size = new System.Drawing.Size(30, 20);
            this.cmdAND.TabIndex = 8;
            this.cmdAND.Text = "&&";
            this.cmdAND.UseVisualStyleBackColor = true;
            this.cmdAND.Click += new System.EventHandler(this.cmdAnd_Click);
            // 
            // lvEnum
            // 
            this.lvEnum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEnum.CheckBoxes = true;
            this.lvEnum.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Value});
            this.lvEnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvEnum.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvEnum.Location = new System.Drawing.Point(6, 21);
            this.lvEnum.Name = "lvEnum";
            this.lvEnum.Size = new System.Drawing.Size(247, 108);
            this.lvEnum.TabIndex = 24;
            this.lvEnum.TabStop = false;
            this.lvEnum.UseCompatibleStateImageBehavior = false;
            this.lvEnum.View = System.Windows.Forms.View.Details;
            this.lvEnum.Visible = false;
            this.lvEnum.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvEnum_ItemChecked);
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 208;
            // 
            // ListTimer
            // 
            this.ListTimer.Interval = 500;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.FilterMenu,
            this.ClassesMenu,
            this.AttributesMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(591, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_Apply_MenuItem,
            this.toolStripSeparator7,
            this.File_Close_MenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "File";
            // 
            // File_Apply_MenuItem
            // 
            this.File_Apply_MenuItem.Name = "File_Apply_MenuItem";
            this.File_Apply_MenuItem.Size = new System.Drawing.Size(152, 22);
            this.File_Apply_MenuItem.Text = "Apply Filter";
            this.File_Apply_MenuItem.Click += new System.EventHandler(this.File_Apply_MenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(149, 6);
            // 
            // File_Close_MenuItem
            // 
            this.File_Close_MenuItem.Name = "File_Close_MenuItem";
            this.File_Close_MenuItem.Size = new System.Drawing.Size(152, 22);
            this.File_Close_MenuItem.Text = "Close";
            this.File_Close_MenuItem.Click += new System.EventHandler(this.File_Close_MenuItem_Click);
            // 
            // FilterMenu
            // 
            this.FilterMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Filter_And_MenuItem,
            this.Filter_Or_MenuItem,
            this.toolStripSeparator4,
            this.Filter_CloseCurrent_MenuItem,
            this.Filter_CloseAll_MenuItem,
            this.toolStripSeparator5,
            this.Filter_Clear_MenuItem});
            this.FilterMenu.Name = "FilterMenu";
            this.FilterMenu.Size = new System.Drawing.Size(83, 20);
            this.FilterMenu.Text = "FilterGroups";
            // 
            // Filter_And_MenuItem
            // 
            this.Filter_And_MenuItem.Name = "Filter_And_MenuItem";
            this.Filter_And_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.Filter_And_MenuItem.Text = "New AND group (&&";
            this.Filter_And_MenuItem.Click += new System.EventHandler(this.Filter_And_MenuItem_Click);
            // 
            // Filter_Or_MenuItem
            // 
            this.Filter_Or_MenuItem.Name = "Filter_Or_MenuItem";
            this.Filter_Or_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.Filter_Or_MenuItem.Text = "New OR group (|";
            this.Filter_Or_MenuItem.Click += new System.EventHandler(this.Filter_Or_MenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(176, 6);
            // 
            // Filter_CloseCurrent_MenuItem
            // 
            this.Filter_CloseCurrent_MenuItem.Name = "Filter_CloseCurrent_MenuItem";
            this.Filter_CloseCurrent_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.Filter_CloseCurrent_MenuItem.Text = "Close current group";
            this.Filter_CloseCurrent_MenuItem.Click += new System.EventHandler(this.Filter_CloseCurrent_MenuItem_Click);
            // 
            // Filter_CloseAll_MenuItem
            // 
            this.Filter_CloseAll_MenuItem.Name = "Filter_CloseAll_MenuItem";
            this.Filter_CloseAll_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.Filter_CloseAll_MenuItem.Text = "Close all groups";
            this.Filter_CloseAll_MenuItem.Click += new System.EventHandler(this.Filter_CloseAll_MenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(176, 6);
            // 
            // Filter_Clear_MenuItem
            // 
            this.Filter_Clear_MenuItem.Name = "Filter_Clear_MenuItem";
            this.Filter_Clear_MenuItem.Size = new System.Drawing.Size(179, 22);
            this.Filter_Clear_MenuItem.Text = "Clear Filter";
            this.Filter_Clear_MenuItem.Click += new System.EventHandler(this.Filter_Clear_MenuItem_Click);
            // 
            // ClassesMenu
            // 
            this.ClassesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Classes_Equals_MenuItem,
            this.Classes_NotEquals_MenuItem});
            this.ClassesMenu.Name = "ClassesMenu";
            this.ClassesMenu.Size = new System.Drawing.Size(57, 20);
            this.ClassesMenu.Text = "Classes";
            // 
            // Classes_Equals_MenuItem
            // 
            this.Classes_Equals_MenuItem.Name = "Classes_Equals_MenuItem";
            this.Classes_Equals_MenuItem.Size = new System.Drawing.Size(152, 22);
            this.Classes_Equals_MenuItem.Text = "Equals";
            this.Classes_Equals_MenuItem.Click += new System.EventHandler(this.Classes_Equals_MenuItem_Click);
            // 
            // Classes_NotEquals_MenuItem
            // 
            this.Classes_NotEquals_MenuItem.Name = "Classes_NotEquals_MenuItem";
            this.Classes_NotEquals_MenuItem.Size = new System.Drawing.Size(152, 22);
            this.Classes_NotEquals_MenuItem.Text = "Not Equals";
            this.Classes_NotEquals_MenuItem.Click += new System.EventHandler(this.Classes_NotEquals_MenuItem_Click);
            // 
            // AttributesMenu
            // 
            this.AttributesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Attributes_Equal_MenuItem,
            this.Attributes_NotEqual_MenuItem,
            this.Attributes_LessOrEqual_MenuItem,
            this.Attributes_GreaterOrEqual_MenuItem,
            this.toolStripSeparator6,
            this.Attributes_And_MenuItem,
            this.Attributes_Nand_MenuItem,
            this.Attributes_Or_MenuItem,
            this.Attributes_Nor_MenuItem,
            this.toolStripSeparator11,
            this.Attributes_Chain_MenuItem});
            this.AttributesMenu.Name = "AttributesMenu";
            this.AttributesMenu.Size = new System.Drawing.Size(71, 20);
            this.AttributesMenu.Text = "Attributes";
            // 
            // Attributes_Equal_MenuItem
            // 
            this.Attributes_Equal_MenuItem.Name = "Attributes_Equal_MenuItem";
            this.Attributes_Equal_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_Equal_MenuItem.Text = "Equals";
            this.Attributes_Equal_MenuItem.Click += new System.EventHandler(this.Attributes_Equal_MenuItem_Click);
            // 
            // Attributes_NotEqual_MenuItem
            // 
            this.Attributes_NotEqual_MenuItem.Name = "Attributes_NotEqual_MenuItem";
            this.Attributes_NotEqual_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_NotEqual_MenuItem.Text = "Not Equals";
            this.Attributes_NotEqual_MenuItem.Click += new System.EventHandler(this.Attributes_NotEqual_MenuItem_Click);
            // 
            // Attributes_LessOrEqual_MenuItem
            // 
            this.Attributes_LessOrEqual_MenuItem.Name = "Attributes_LessOrEqual_MenuItem";
            this.Attributes_LessOrEqual_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_LessOrEqual_MenuItem.Text = "Less Or Equals";
            this.Attributes_LessOrEqual_MenuItem.Click += new System.EventHandler(this.Attributes_LessOrEqual_MenuItem_Click);
            // 
            // Attributes_GreaterOrEqual_MenuItem
            // 
            this.Attributes_GreaterOrEqual_MenuItem.Name = "Attributes_GreaterOrEqual_MenuItem";
            this.Attributes_GreaterOrEqual_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_GreaterOrEqual_MenuItem.Text = "Greater or Equals";
            this.Attributes_GreaterOrEqual_MenuItem.Click += new System.EventHandler(this.Attributes_GreaterOrEqual_MenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
            // 
            // Attributes_And_MenuItem
            // 
            this.Attributes_And_MenuItem.Enabled = false;
            this.Attributes_And_MenuItem.Name = "Attributes_And_MenuItem";
            this.Attributes_And_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_And_MenuItem.Text = "BitWise AND";
            this.Attributes_And_MenuItem.Click += new System.EventHandler(this.Attributes_And_MenuItem_Click);
            // 
            // Attributes_Nand_MenuItem
            // 
            this.Attributes_Nand_MenuItem.Enabled = false;
            this.Attributes_Nand_MenuItem.Name = "Attributes_Nand_MenuItem";
            this.Attributes_Nand_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_Nand_MenuItem.Text = "Not BitWise AND";
            this.Attributes_Nand_MenuItem.Click += new System.EventHandler(this.Attributes_Nand_MenuItem_Click);
            // 
            // Attributes_Or_MenuItem
            // 
            this.Attributes_Or_MenuItem.Enabled = false;
            this.Attributes_Or_MenuItem.Name = "Attributes_Or_MenuItem";
            this.Attributes_Or_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_Or_MenuItem.Text = "BitWise OR";
            this.Attributes_Or_MenuItem.Click += new System.EventHandler(this.Attributes_Or_MenuItem_Click);
            // 
            // Attributes_Nor_MenuItem
            // 
            this.Attributes_Nor_MenuItem.Enabled = false;
            this.Attributes_Nor_MenuItem.Name = "Attributes_Nor_MenuItem";
            this.Attributes_Nor_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_Nor_MenuItem.Text = "Not BitWise OR";
            this.Attributes_Nor_MenuItem.Click += new System.EventHandler(this.Attributes_Nor_MenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(161, 6);
            // 
            // Attributes_Chain_MenuItem
            // 
            this.Attributes_Chain_MenuItem.Enabled = false;
            this.Attributes_Chain_MenuItem.Name = "Attributes_Chain_MenuItem";
            this.Attributes_Chain_MenuItem.Size = new System.Drawing.Size(164, 22);
            this.Attributes_Chain_MenuItem.Text = "Match in Chain";
            this.Attributes_Chain_MenuItem.Click += new System.EventHandler(this.Attributes_Chain_MenuItem_Click);
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.gbClasses);
            this.gbInfo.Controls.Add(this.gbAttributes);
            this.gbInfo.Location = new System.Drawing.Point(2, 150);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(587, 213);
            this.gbInfo.TabIndex = 9;
            this.gbInfo.TabStop = false;
            // 
            // ValueContextMenu
            // 
            this.ValueContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Value_Copy_ContextItem,
            this.Value_Paste_ContextItem,
            this.toolStripSeparator10,
            this.Value_Claer_ContextItem});
            this.ValueContextMenu.Name = "ValueContextMenu";
            this.ValueContextMenu.Size = new System.Drawing.Size(103, 76);
            this.ValueContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ValueContextMenu_Opening);
            // 
            // Value_Copy_ContextItem
            // 
            this.Value_Copy_ContextItem.Name = "Value_Copy_ContextItem";
            this.Value_Copy_ContextItem.Size = new System.Drawing.Size(102, 22);
            this.Value_Copy_ContextItem.Text = "Copy";
            this.Value_Copy_ContextItem.Click += new System.EventHandler(this.Value_Copy_ContextItem_Click);
            // 
            // Value_Paste_ContextItem
            // 
            this.Value_Paste_ContextItem.Name = "Value_Paste_ContextItem";
            this.Value_Paste_ContextItem.Size = new System.Drawing.Size(102, 22);
            this.Value_Paste_ContextItem.Text = "Paste";
            this.Value_Paste_ContextItem.Click += new System.EventHandler(this.Value_Paste_ContextItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(99, 6);
            // 
            // Value_Claer_ContextItem
            // 
            this.Value_Claer_ContextItem.Name = "Value_Claer_ContextItem";
            this.Value_Claer_ContextItem.Size = new System.Drawing.Size(102, 22);
            this.Value_Claer_ContextItem.Text = "Clear";
            this.Value_Claer_ContextItem.Click += new System.EventHandler(this.Value_Claer_ContextItem_Click);
            // 
            // QueryBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 364);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.gbGroups);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryBuilder";
            this.Text = "QueryBuilder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueryBuilder_FormClosing);
            this.Load += new System.EventHandler(this.QueryBuilder_Load);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.gbGroups.ResumeLayout(false);
            this.gbGroups.PerformLayout();
            this.FilterContextMenu.ResumeLayout(false);
            this.gbClasses.ResumeLayout(false);
            this.ClassesContextMenu.ResumeLayout(false);
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            this.AttributesContextMenu.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.gbInfo.ResumeLayout(false);
            this.ValueContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGroups;
        private System.Windows.Forms.GroupBox gbClasses;
        private System.Windows.Forms.Button cmdSet;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.Button cmdNewOrGroup;
        private System.Windows.Forms.Button cmdCloseGroup;
        private System.Windows.Forms.Button cmdNewAndGroup;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.Button cmdOR;
        private System.Windows.Forms.Button cmdChain;
        private System.Windows.Forms.Button cmdEQ;
        private System.Windows.Forms.Button cmdNEQ;
        private System.Windows.Forms.Button cmdAND;
        private System.Windows.Forms.Button cmdCloseAllGroups;
        private System.Windows.Forms.ListBox lbClasses;
        private System.Windows.Forms.Timer ListTimer;
        private System.Windows.Forms.Button cmdClassNeq;
        private System.Windows.Forms.Button cmdClassEq;
        private System.Windows.Forms.ListBox lbAttribs;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem File_Apply_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem File_Close_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem FilterMenu;
        private System.Windows.Forms.ToolStripMenuItem Filter_And_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_Or_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_CloseCurrent_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_CloseAll_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Filter_Clear_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClassesMenu;
        private System.Windows.Forms.ToolStripMenuItem Classes_Equals_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Classes_NotEquals_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem AttributesMenu;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Equal_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_NotEqual_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_And_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Or_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Chain_MenuItem;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.ContextMenuStrip ClassesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Classes_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Classes_Equals__ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Classes_NotEquals_ContextItem;
        private System.Windows.Forms.ContextMenuStrip AttributesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Equals_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_NotEquals_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_And_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Or_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Chain_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Classes_CopyAll_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_CopyAll_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ListView lvEnum;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.Button cmdSetLogicalFilter;
        private System.Windows.Forms.Button cmdHideEnum;
        private System.Windows.Forms.Button cmdClearFilter;
        private System.Windows.Forms.ContextMenuStrip FilterContextMenu;
        private System.Windows.Forms.ToolStripMenuItem FQBilter_Clear_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem FQBilter_NewAnd_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem FQBilter_NewOr_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem FQBilter_Close_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem FQBilter_CloseAll_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.CheckBox cbGuid;
        private System.Windows.Forms.ContextMenuStrip ValueContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Value_Copy_ContextItem;
        private System.Windows.Forms.ToolStripMenuItem Value_Paste_ContextItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem Value_Claer_ContextItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdLEQ;
        private System.Windows.Forms.Button cmdGEQ;
        private System.Windows.Forms.Button cmdUnDo;
        private System.Windows.Forms.Button cmdNOR;
        private System.Windows.Forms.Button cmdNAND;
        private System.Windows.Forms.ToolStripMenuItem Attributes_LessOrEqual_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_GreaterOrEqual_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Nand_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Attributes_Nor_MenuItem;
    }
}