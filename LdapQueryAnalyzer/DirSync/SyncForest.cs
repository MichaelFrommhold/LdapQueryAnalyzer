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
    public class SyncForest : SyncBase
    {
        #region fields
        // see SyncBase
        #endregion

        #region constructor

        internal SyncForest()
            : base()
        { }

        internal SyncForest(string name, string path)
            : base()
        { LoadInternal(name, path); }

        #endregion

        #region methods

        public SyncRoot AddRoot(string name)
        {
            name = name.ToFileNameValid();

            if (RootList.ContainsKey(name))
            { return RootList[name]; }

            SyncRoot ret = new SyncRoot();

            ret.ForestName = this.ForestName;
            ret.RootName = name;
            ret.RootPath = Path.Combine(this.ForestPath, name);

            GlobalHelper.CreateDirectory(ret.RootPath);

            RootList.Add(name, ret);

            return ret;
        }

        private void LoadInternal(string name, string path)
        {
            ForestName = name;

            ForestPath = path;

            SubFolders = GlobalHelper.FoldersInPath(path);

            if (SubFolders.Count == 0)
            { return; }

            CheckForestRuns();
        }

        private void CheckForestRuns()
        {
            Dictionary<string, string> temp = SubFolders;

            foreach (string name in temp.Keys)
            {
                if (GlobalHelper.PathContainsFile(SubFolders[name], XML_NAME))
                {
                    SyncRuns.Add(new SyncRun(ForestName, ForestName, name, temp[name]));

                    SubFolders.Remove(name);
                }
            }
        }

        private void GetRoots()
        {
            foreach (KeyValuePair<string, string> kvproot in SubFolders)
            { RootList.Add(kvproot.Key, new SyncRoot(ForestName, kvproot.Key, kvproot.Value)); }
        }

        #endregion
    }
}
