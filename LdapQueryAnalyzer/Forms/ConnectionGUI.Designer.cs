namespace CodingFromTheField.LdapQueryAnalyzer
{
    partial class ConnectionGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionGUI));
            this.txtForest = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lpPwd = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.ttCon = new System.Windows.Forms.ToolTip(this.components);
            this.cmbTrusts = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtForest
            // 
            this.txtForest.Location = new System.Drawing.Point(63, 21);
            this.txtForest.Name = "txtForest";
            this.txtForest.Size = new System.Drawing.Size(187, 20);
            this.txtForest.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Forest";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "UserName";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(63, 74);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(208, 20);
            this.txtUser.TabIndex = 2;
            // 
            // lpPwd
            // 
            this.lpPwd.AutoSize = true;
            this.lpPwd.Location = new System.Drawing.Point(3, 104);
            this.lpPwd.Name = "lpPwd";
            this.lpPwd.Size = new System.Drawing.Size(53, 13);
            this.lpPwd.TabIndex = 5;
            this.lpPwd.Text = "Password";
            this.ttCon.SetToolTip(this.lpPwd, "DoubleClicvk to show / hide PWD characters");
            this.lpPwd.DoubleClick += new System.EventHandler(this.lbPwd_DoubleClick);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(63, 101);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(208, 20);
            this.txtPwd.TabIndex = 3;
            this.txtPwd.UseSystemPasswordChar = true;
            this.txtPwd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyUp);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(149, 127);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(58, 23);
            this.cmdConnect.TabIndex = 4;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(213, 127);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(58, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Domain";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(63, 48);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(208, 20);
            this.txtDomain.TabIndex = 1;
            // 
            // cmbTrusts
            // 
            this.cmbTrusts.FormattingEnabled = true;
            this.cmbTrusts.Location = new System.Drawing.Point(63, 21);
            this.cmbTrusts.Name = "cmbTrusts";
            this.cmbTrusts.Size = new System.Drawing.Size(208, 21);
            this.cmbTrusts.TabIndex = 19;
            this.cmbTrusts.SelectedIndexChanged += new System.EventHandler(this.cmbTrusts_SelectedIndexChanged);
            // 
            // ConnectionGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 153);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.lpPwd);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtForest);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmbTrusts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionGUI";
            this.Text = "Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Connection_FormClosing);
            this.Load += new System.EventHandler(this.Connection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtForest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lpPwd;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.ToolTip ttCon;
        private System.Windows.Forms.ComboBox cmbTrusts;
    }
}