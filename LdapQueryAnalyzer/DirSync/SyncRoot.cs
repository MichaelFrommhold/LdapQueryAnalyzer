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
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.IO;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SyncRoot : SyncBase
    {
        #region fields
        //see SyncBase
        #endregion

        #region constructor

        internal SyncRoot()
            : base()
        { }

        internal SyncRoot(string forestName, string name, string path)
            : base()
        { LoadInternal(forestName, name, path); }

        #endregion

        #region methods

        public SyncRun AddRun(string dn, string filter, string attribs, SearchScope scope, byte[] cookie = null, ReplicationCursorCollection replInfo = null)
        {
            SyncRun ret = new SyncRun();

            string name = Guid.NewGuid().ToString();

            ret.ForestName = this.ForestName;
            ret.RootName = this.RootName;
            ret.RunName = name;
            ret.RunPath = Path.Combine(this.RootPath, name);

            GlobalHelper.CreateDirectory(ret.RunPath);

            ret.EntryPoint = dn;
            ret.Filter = filter;
            ret.Attributes = attribs;

            ret.Serialize();

            if (cookie != null)
            { ret.AddCookieRun(cookie, replInfo); }

            SyncRuns.Add(ret);

            return ret;
        }

        private void LoadInternal(string forestName, string name, string path)
        {
            ForestName = forestName;
            RootName = name;
            RootPath = path;

            SubFolders = GlobalHelper.FoldersInPath(path);

            CheckRuns();
        }

        private void CheckRuns()
        {
            foreach (KeyValuePair<string, string> run in SubFolders)
            {
                if (GlobalHelper.PathContainsFile(run.Value, XML_NAME))
                { SyncRuns.Add(new SyncRun(ForestName, RootName, run.Key, run.Value)); }
            }
        }

        #endregion
    }
}
