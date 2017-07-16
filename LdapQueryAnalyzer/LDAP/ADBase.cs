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

using System.DirectoryServices.Protocols;
using System.Threading;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class ADBase
    {
        #region ForestInfo

        internal AutoResetEvent[] Signaler;
        internal DateTime UpdateStarted;

        #endregion

        #region ADHelper

        internal LdapConnection Connection;
        internal LdapConnection AsyncConnection;
        internal bool IsConnected = false;

        internal bool AsyncComplete;
        public int AsyncCalls = 0;

        public List<SearchResultEntry> ParallelResults = new List<SearchResultEntry> { };
        public int ParallelRuns = 0;
        public bool RunningParallel = false;
        public bool CancelToken = false;
        
        #endregion

        #region SchemaCache

        public bool MustLoadSchemaFromAD = false;

        //private int Counter = 0;

        #endregion

        #region messages

        public bool HasError = false;
        public string ErrorMSG = null;

        internal void SetError(string errorMsg)
        {
            ErrorMSG = "ERROR " + errorMsg;
            HasError = true;

            GlobalEventHandler.RaiseErrorOccured(ErrorMSG);
        }

        private void ResetError()
        {
            ErrorMSG = String.Empty;
            HasError = false;
        }

        #endregion
    }
}
