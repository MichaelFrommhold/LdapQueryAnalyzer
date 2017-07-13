namespace CodingFromTheField.LdapQueryAnalyzer
{
    partial class StatsGUI
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Code Time");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Thread Count");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Call Time");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Entries Returned");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Entries Visited");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Pages Referenced");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Pages Read From Disk");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Pages Pre-read From Disk");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Clean Pages Modified");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Dirty Pages Modified");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Log Records Genreated");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Log Record bytes Genreated");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatsGUI));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvStats = new System.Windows.Forms.ListView();
            this.colItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textStats = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUsedFilter = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIndexes = new System.Windows.Forms.TextBox();
            this.cmdClip = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvStats);
            this.groupBox3.Controls.Add(this.textStats);
            this.groupBox3.Location = new System.Drawing.Point(373, 2);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(266, 246);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // lvStats
            // 
            this.lvStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colItem,
            this.colValue});
            this.lvStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStats.GridLines = true;
            this.lvStats.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.lvStats.Location = new System.Drawing.Point(2, 15);
            this.lvStats.Name = "lvStats";
            this.lvStats.Size = new System.Drawing.Size(262, 229);
            this.lvStats.TabIndex = 13;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            // 
            // colItem
            // 
            this.colItem.Text = "Item";
            this.colItem.Width = 152;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 105;
            // 
            // textStats
            // 
            this.textStats.BackColor = System.Drawing.SystemColors.Window;
            this.textStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStats.Location = new System.Drawing.Point(2, 13);
            this.textStats.Margin = new System.Windows.Forms.Padding(2);
            this.textStats.Multiline = true;
            this.textStats.Name = "textStats";
            this.textStats.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textStats.Size = new System.Drawing.Size(344, 69);
            this.textStats.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUsedFilter);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 157);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Used Filter";
            // 
            // txtUsedFilter
            // 
            this.txtUsedFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsedFilter.Location = new System.Drawing.Point(3, 16);
            this.txtUsedFilter.Multiline = true;
            this.txtUsedFilter.Name = "txtUsedFilter";
            this.txtUsedFilter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUsedFilter.Size = new System.Drawing.Size(362, 138);
            this.txtUsedFilter.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIndexes);
            this.groupBox2.Location = new System.Drawing.Point(2, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 89);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Used Indexes";
            // 
            // txtIndexes
            // 
            this.txtIndexes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIndexes.Location = new System.Drawing.Point(3, 16);
            this.txtIndexes.Multiline = true;
            this.txtIndexes.Name = "txtIndexes";
            this.txtIndexes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIndexes.Size = new System.Drawing.Size(362, 70);
            this.txtIndexes.TabIndex = 0;
            // 
            // cmdClip
            // 
            this.cmdClip.Location = new System.Drawing.Point(516, 250);
            this.cmdClip.Name = "cmdClip";
            this.cmdClip.Size = new System.Drawing.Size(58, 23);
            this.cmdClip.TabIndex = 6;
            this.cmdClip.Text = "clipboard";
            this.cmdClip.UseVisualStyleBackColor = true;
            this.cmdClip.Click += new System.EventHandler(this.cmdClip_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(580, 250);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(58, 23);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // StatsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 276);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdClip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatsGUI";
            this.Text = "Query Statistics";
            this.Load += new System.EventHandler(this.StatsGUI_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvStats;
        private System.Windows.Forms.ColumnHeader colItem;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.TextBox textStats;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUsedFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIndexes;
        private System.Windows.Forms.Button cmdClip;
        private System.Windows.Forms.Button cmdClose;
    }
}