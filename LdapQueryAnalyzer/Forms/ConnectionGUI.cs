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
using System.Windows.Forms;
using System.Security.Principal;
using System.DirectoryServices.ActiveDirectory;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public partial class ConnectionGUI : Form
    {
        #region fields

        private string ForestName = null;
        private bool CredsOnly;

        #endregion

        #region constructor

        public ConnectionGUI(string forestName = null, bool credsOnly = false)
        {
            CredsOnly = credsOnly;
            
            ForestName = forestName;

            InitializeComponent();
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            List<string> doms = new List<string> { };

            if (ForestName != null)
            { 
                this.txtForest.Text = ForestName; 

                doms.Add(ForestName);
            }

            else
            { doms.Add(""); }

            if (!CredsOnly)
            {
                if (ForestBase.Trusts.Count > 0)
                {
                    doms.AddRange(ForestBase.Trusts.Where(k => ((k.Value.TrustType != TrustType.ParentChild) && 
                                                                (k.Value.TrustType != TrustType.TreeRoot) && 
                                                                ((k.Value.TrustDirection == TrustDirection.Bidirectional) || 
                                                                (k.Value.TrustDirection == TrustDirection.Outbound)))).ToList().Select(v => v.Key)); 
                }

                this.cmbTrusts.Items.AddRange(doms.ToArray());

                this.cmbTrusts.SelectedItem = 0;
            }

            this.cmbTrusts.Enabled = !CredsOnly;

            this.txtForest.Enabled = !CredsOnly;

            this.txtDomain.Select();

            WindowsIdentity myself = WindowsIdentity.GetCurrent();

            try
            {
                if (myself.Name.Contains("\\"))
                {
                    this.txtDomain.Text = myself.Name.Split('\\')[0];

                    this.txtUser.Text = myself.Name.Split('\\')[1];
                }

                else
                { this.txtUser.Text = myself.Name; }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

        }

        private void Unload()
        { this.Close(); }

        private void Connection_FormClosing(object sender, FormClosingEventArgs e)
        { GlobalEventHandler.RaiseConnectionSignaled(null);  }

        #endregion

        #region custom

        private void ConnectThis()
        {
            string pwd = this.txtPwd.Text;
            
            Credentials creds = null;

            if ((this.txtUser.Text.Length != 0) && (pwd.Length != 0))
            { 
                creds = new Credentials(this.txtForest.Text, this.txtDomain.Text, this.txtUser.Text, ref pwd);

                if (creds.HasError)
                {
                    DialogResult ret = MessageBox.Show("Credential error", creds.ErrorMsg, MessageBoxButtons.RetryCancel);

                    if (ret == DialogResult.Cancel)
                    { Unload(); }
                }
            }

            else
            { creds = new Credentials(this.txtForest.Text, this.txtDomain.Text, this.txtUser.Text, ref pwd); }

            GlobalEventHandler.RaiseConnectionSignaled(creds); 
        }

        #endregion

        #region controls

        private void cmdConnect_Click(object sender, EventArgs e)
        { ConnectThis(); }
            
        private void cmdCancel_Click(object sender, EventArgs e)
        { Unload(); }

        private void txtPwd_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            { ConnectThis(); }
        }

        private void lbPwd_DoubleClick(object sender, EventArgs e)
        { this.txtPwd.UseSystemPasswordChar = !this.txtPwd.UseSystemPasswordChar; }

        #endregion

        private void cmbTrusts_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtForest.Text = this.cmbTrusts.Text;
        }
    }
}
