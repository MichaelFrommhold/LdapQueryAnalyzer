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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public partial class RestoreDialog : Form
    {
        private string BkpPath;
        private bool Restore = false;

        public RestoreDialog(string path)
        {
            InitializeComponent();

            BkpPath = path;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Restore = true;

            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void RestoreDialog_FormClosing(object sender, FormClosingEventArgs e)
        { GlobalEventHandler.RaiseDoBackup(Restore ? BkpPath : null); }
    }
}
