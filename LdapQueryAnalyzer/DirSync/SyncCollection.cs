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
using System.IO;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SyncCollection : SyncBase
    {
        #region fields
        // see SyncBase
        #endregion

        #region constructor

        internal SyncCollection()
            : base()
        { LoadInternal(); }

        #endregion

        #region methods

        public SyncForest AddForest(string name)
        {
            name = name.ToFileNameValid();

            if (ForestList.ContainsKey(name))
            { return ForestList[name]; }

            SyncForest ret = new SyncForest();

            ret.ForestName = name;
            ret.ForestPath = Path.Combine(this.SyncRunsPath, name);

            GlobalHelper.CreateDirectory(ret.ForestPath);

            ForestList.Add(name, ret);

            return ret;
        }

        private void LoadInternal()
        {
            GetForests();
        }

        private void GetForests()
        {
            bool tmp;

            SyncRunsPath = GlobalHelper.PathInCurrentDirectory(SyncRunsPath, out tmp);

            foreach (KeyValuePair<string, string> kvpforest in GlobalHelper.FoldersInPath(SyncRunsPath))
            { ForestList.Add(kvpforest.Key, new SyncForest(kvpforest.Key, kvpforest.Value)); }
        }

        #endregion
    }
}
 