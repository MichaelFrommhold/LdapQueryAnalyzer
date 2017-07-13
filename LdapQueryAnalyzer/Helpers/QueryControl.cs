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

using System.Collections.Generic;
using System.DirectoryServices.Protocols;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class QueryControl
    {
        public int Port = 389;

        public bool PerformPagedQuery = false;

        public ReferralChasingOptions RefOptions = ReferralChasingOptions.None;

        public bool AutoPage = false;

        public bool ExecStatsQuery = false;

        public bool RunAsync = false;

        public bool DoValueRangeRetrieval = false;

        public bool ShowDeleted = false;

        public bool ShowRecycled = false;

        public bool PerformASQ = false;

        public bool PerformSort = false;

        public List<SortKey> SortKeys = new List<SortKey> { };

        public bool PerformDirSync = false;

        public bool DirSyncReRun = false;
        public byte[] DirSyncCookie = null;
        public SyncRun DirSyncRun;

        public bool VlvRequired = false;

        public bool SortResults = false;

        public bool SortAscending = true;

        public bool FromGui = false;

        public bool ContainsConstructedAttribute = false;

        public bool MustGetSingleObjectPath = false;

        public QUERY_RESULT_EVENT_TYPE CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.NONE;

        public bool PhantomRoot = false;

        public bool RootDse = false;
        public bool QueryExtendedRootDSE = false;

        public int QueryRuns = 0;

        public int PartialRuns = -1;

        public QueryControl() { }

        public bool FirstRun
        {
            get
            {
                if (((QueryExtendedRootDSE) && (QueryRuns == 0)) || (!QueryExtendedRootDSE))
                { return true; }

                else
                { return false; }
            }
        }
    }

}
