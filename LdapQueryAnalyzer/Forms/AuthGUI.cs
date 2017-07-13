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
using System.Windows.Forms;
using System.Security.Principal;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public partial class AuthGUI : Form
    {
        #region fields

        private bool CredsSent { get; set; }

        private bool ElevateThis { get; set; }
        private bool RestartThis { get; set; }

        #endregion

        #region constructor

        public AuthGUI(bool elevate, bool restart)
        {
            CredsSent = false;

            ElevateThis = elevate;

            RestartThis = restart;           

            InitializeComponent();
        }
        
        private void AuthGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CredsSent)
            { GlobalEventHandler.RaiseCredentialsSignaled(null); }
        }

        private void AuthGUI_Load(object sender, EventArgs e)
        {
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
    
        #endregion

        #region controls

        private void lpPwd_DoubleClick(object sender, EventArgs e)
        {
            this.txtPwd.UseSystemPasswordChar = !this.txtPwd.UseSystemPasswordChar;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void cmdConnect_Click(object sender, EventArgs e)
        { SendCreds(); }
        
        #endregion

        #region methods

        private void SendCreds()
        {
            string pwd = this.txtPwd.Text;

            Credentials creds = null;

            if ((this.txtUser.Text.Length != 0) && (pwd.Length != 0))
            {
                creds = new Credentials(this.txtDomain.Text, this.txtUser.Text, ref pwd, ElevateThis, RestartThis);

                if (creds.HasError)
                {
                    DialogResult ret = MessageBox.Show("Credential error", creds.ErrorMsg, MessageBoxButtons.RetryCancel);

                    if (ret == DialogResult.Cancel)
                    { this.Close(); }
                }

                else
                { RaiseCreds(creds); }
            }

            else
            { 
                creds = new Credentials(this.txtDomain.Text, this.txtUser.Text, ref pwd, ElevateThis, RestartThis);

                RaiseCreds(creds);
            }            
        }

        private void RaiseCreds(Credentials creds)
        {
            CredsSent = true;

            GlobalEventHandler.RaiseCredentialsSignaled(creds);

            this.Close();
        }

        #endregion

    }
}
