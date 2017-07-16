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
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SyncRun : SyncBase
    {
        #region fields
        //see SyncBase
        #endregion

        #region constructor

        internal SyncRun()
            : base()
        { }

        internal SyncRun(string forestName, string rootName, string name, string path)
            : base()
        { LoadInternal(forestName, rootName, name, path); }

        #endregion

        #region methods

        public void Serialize()
        {
            string xmlfile = Path.Combine(RunPath, XML_NAME);

            XDocument runxml = new XDocument();

            XElement runroot = new XElement(ROOT_ID);

            //runroot.Add(new XElement(DN_ID, new XAttribute(RUN_ID, EntryPoint)));

            //runroot.Add(new XElement(FILTER_ID, new XAttribute(RUN_ID, Filter)));

            //runroot.Add(new XElement(ATTRIBUTES_ID, new XAttribute(RUN_ID, Attributes)));

            runxml.Add(runroot);

            runxml.Save(xmlfile);
        }

        public SyncCookieRun AddCookieRun(byte[] binCookie, ReplicationCursorCollection replInfo)
        {
            SyncCookieRun ret = SerializeCookieRun(binCookie, replInfo);

            if (ret != null)
            { CookieRuns.Add(ret.CookieName, ret); }

            return ret;
        }

        private void LoadInternal(string forestName, string rootName, string name, string path)
        {
            ForestName = forestName;
            RootName = rootName;
            RunName = name;
            RunPath = path;

            SubFolders = GlobalHelper.FoldersInPath(path);

            LoadXml();

            LoadCookieRuns();
        }

        private void LoadXml()
        {
            if (GlobalHelper.PathContainsFile(RunPath, XML_NAME))
            {
                string xmlfile = Path.Combine(RunPath, XML_NAME);

                XDocument runxml = XDocument.Parse(File.ReadAllText(xmlfile));

                EntryPoint = ReadXmlInfo(runxml, DN_ID);
                Filter = ReadXmlInfo(runxml, FILTER_ID);
                Attributes = ReadXmlInfo(runxml, ATTRIBUTES_ID);

            }
        }

        private string ReadXmlInfo(XDocument runXml, string valueName)
        {
            string ret = String.Empty;

            XElement xmlinfo = runXml.Descendants(valueName).FirstOrDefault();

            //if (xmlinfo != null)
            //{ ret = xmlinfo.Attribute(RUN_ID).Value; }

            return ret;
        }

        private void LoadCookieRuns()
        {
            Dictionary<string, SyncCookieRun> temp = new Dictionary<string, SyncCookieRun>(StringComparer.InvariantCultureIgnoreCase);

            foreach (KeyValuePair<string, string> cookierun in SubFolders)
            {
                temp.Add(cookierun.Key, new SyncCookieRun(cookierun.Key, cookierun.Value));
            }

            foreach (KeyValuePair<string, SyncCookieRun> kvpcookie in temp.OrderBy(c => c.Value.CookieDate))
            { CookieRuns.Add(kvpcookie.Key, kvpcookie.Value); }
        }

        private SyncCookieRun SerializeCookieRun(byte[] binCookie, ReplicationCursorCollection replInfo)
        {
            SyncCookieRun ret = null;

            string crname = Guid.NewGuid().ToString();

            string crpath = Path.Combine(RunPath, crname);

            Directory.CreateDirectory(crpath);

            ret = new SyncCookieRun(crname, crpath, binCookie, replInfo);

            CookieRuns.Add(crname, ret);

            return ret;
        }

        #endregion
    }
}
