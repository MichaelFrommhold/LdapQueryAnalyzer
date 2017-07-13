namespace CodingFromTheField.LdapQueryAnalyzer
{
    partial class LDAPBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LDAPBrowser));
            this.tvLdap = new System.Windows.Forms.TreeView();
            this.Tree_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Tree_Select_ContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Tree_Recall_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilObjects = new System.Windows.Forms.ImageList(this.components);
            this.cmdSet = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdRecall = new System.Windows.Forms.Button();
            this.lbProcess = new System.Windows.Forms.Label();
            this.Tree_Query_ContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_SelectQuery_ContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvLdap
            // 
            this.tvLdap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvLdap.ContextMenuStrip = this.Tree_ContextMenu;
            this.tvLdap.ImageIndex = 0;
            this.tvLdap.ImageList = this.ilObjects;
            this.tvLdap.Location = new System.Drawing.Point(1, 2);
            this.tvLdap.Name = "tvLdap";
            this.tvLdap.SelectedImageIndex = 0;
            this.tvLdap.Size = new System.Drawing.Size(283, 232);
            this.tvLdap.TabIndex = 0;
            this.tvLdap.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvLdap_BeforeCollapse);
            this.tvLdap.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLdap_NodeMouseClick);
            this.tvLdap.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLdap_NodeMouseDoubleClick);
            // 
            // Tree_ContextMenu
            // 
            this.Tree_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tree_Query_ContextMenuItem,
            this.Tree_Select_ContextMenuItem,
            this.Tree_SelectQuery_ContextMenuItem,
            this.toolStripSeparator1,
            this.Tree_Recall_MenuItem});
            this.Tree_ContextMenu.Name = "Tree_ContextMenu";
            this.Tree_ContextMenu.Size = new System.Drawing.Size(154, 120);
            this.Tree_ContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.Tree_ContextMenu_Opening);
            // 
            // Tree_Select_ContextMenuItem
            // 
            this.Tree_Select_ContextMenuItem.Name = "Tree_Select_ContextMenuItem";
            this.Tree_Select_ContextMenuItem.Size = new System.Drawing.Size(153, 22);
            this.Tree_Select_ContextMenuItem.Text = "Select";
            this.Tree_Select_ContextMenuItem.Click += new System.EventHandler(this.Tree_Select_ContextMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // Tree_Recall_MenuItem
            // 
            this.Tree_Recall_MenuItem.Name = "Tree_Recall_MenuItem";
            this.Tree_Recall_MenuItem.Size = new System.Drawing.Size(153, 22);
            this.Tree_Recall_MenuItem.Text = "Recall";
            this.Tree_Recall_MenuItem.Click += new System.EventHandler(this.Tree_Recall_MenuItem_Click);
            // 
            // ilObjects
            // 
            this.ilObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilObjects.ImageStream")));
            this.ilObjects.TransparentColor = System.Drawing.Color.Transparent;
            this.ilObjects.Images.SetKeyName(0, "Domain.ico");
            this.ilObjects.Images.SetKeyName(1, "user.ico");
            this.ilObjects.Images.SetKeyName(2, "Container.ico");
            this.ilObjects.Images.SetKeyName(3, "ou.ico");
            this.ilObjects.Images.SetKeyName(4, "group.ico");
            this.ilObjects.Images.SetKeyName(5, "computer.ico");
            this.ilObjects.Images.SetKeyName(6, "foreignsecurityprincipal.ico");
            this.ilObjects.Images.SetKeyName(7, "config.ico");
            // 
            // cmdSet
            // 
            this.cmdSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdSet.Location = new System.Drawing.Point(186, 236);
            this.cmdSet.Name = "cmdSet";
            this.cmdSet.Size = new System.Drawing.Size(45, 23);
            this.cmdSet.TabIndex = 1;
            this.cmdSet.Text = "select";
            this.cmdSet.UseVisualStyleBackColor = true;
            this.cmdSet.Click += new System.EventHandler(this.cmdSet_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmdCancel.Location = new System.Drawing.Point(237, 236);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(45, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdRecall
            // 
            this.cmdRecall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRecall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdRecall.Location = new System.Drawing.Point(2, 236);
            this.cmdRecall.Name = "cmdRecall";
            this.cmdRecall.Size = new System.Drawing.Size(45, 23);
            this.cmdRecall.TabIndex = 3;
            this.cmdRecall.Text = "recall";
            this.cmdRecall.UseVisualStyleBackColor = true;
            this.cmdRecall.Click += new System.EventHandler(this.cmdRecall_Click);
            // 
            // lbProcess
            // 
            this.lbProcess.AutoSize = true;
            this.lbProcess.Location = new System.Drawing.Point(12, 220);
            this.lbProcess.Name = "lbProcess";
            this.lbProcess.Size = new System.Drawing.Size(0, 13);
            this.lbProcess.TabIndex = 4;
            // 
            // Tree_Query_ContextMenuItem
            // 
            this.Tree_Query_ContextMenuItem.Name = "Tree_Query_ContextMenuItem";
            this.Tree_Query_ContextMenuItem.Size = new System.Drawing.Size(153, 22);
            this.Tree_Query_ContextMenuItem.Text = "Query";
            this.Tree_Query_ContextMenuItem.Click += new System.EventHandler(this.Tree_Query_ContextMenuItem_Click);
            // 
            // Tree_SelectQuery_ContextMenuItem
            // 
            this.Tree_SelectQuery_ContextMenuItem.Name = "Tree_SelectQuery_ContextMenuItem";
            this.Tree_SelectQuery_ContextMenuItem.Size = new System.Drawing.Size(153, 22);
            this.Tree_SelectQuery_ContextMenuItem.Text = "Select && Query";
            this.Tree_SelectQuery_ContextMenuItem.Click += new System.EventHandler(this.Tree_SelectQuery_ContextMenuItem_Click);
            // 
            // LDAPBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbProcess);
            this.Controls.Add(this.cmdRecall);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSet);
            this.Controls.Add(this.tvLdap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LDAPBrowser";
            this.Text = "Tree";
            this.Load += new System.EventHandler(this.Browser_Load);
            this.Tree_ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvLdap;
        private System.Windows.Forms.ImageList ilObjects;
        private System.Windows.Forms.Button cmdSet;
        private System.Windows.Forms.ContextMenuStrip Tree_ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem Tree_Select_ContextMenuItem;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolStripMenuItem Tree_Recall_MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button cmdRecall;
        private System.Windows.Forms.Label lbProcess;
        private System.Windows.Forms.ToolStripMenuItem Tree_Query_ContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Tree_SelectQuery_ContextMenuItem;
    }
}