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

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SyncBase
    {
        #region constants

        public const string XML_NAME = "run.xml";

        public const string ROOT_ID = "SyncRun";
        public const string DN_ID = "EntryPoint";
        public const string FILTER_ID = "Filter";
        public const string ATTRIBUTES_ID = "Attributes";
        public const string SCOPE_ID = "Scope";
        public const string VALUE_ID = "Value";

        public const string USN_FILE_NAME = "usn.store";

        public const string COOKIE_NAME = "cookie.store";               

        #endregion

        #region common

        public List<SyncRun> SyncRuns = new List<SyncRun> { };

        internal Dictionary<string, string> SubFolders;

        #endregion

        #region SyncCollection

        public Dictionary<string, SyncForest> ForestList = new Dictionary<string, SyncForest>(StringComparer.InvariantCultureIgnoreCase);

        internal string SyncRunsPath = "Cache\\SyncRuns";

        #endregion

        #region SyncForest

        public string ForestName;
        public string ForestPath;

        public Dictionary<string, SyncRoot> RootList = new Dictionary<string, SyncRoot>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region SyncRoot

        public string RootPath;
        public string RootName;

        #endregion

        #region SyncRun

        public bool IsFirstRun = false;
        public string RunPath;
        public string RunName;

        public string EntryPoint;
        public string Filter;
        public string Attributes;
        public string Scope;

        public byte[] CurrentCookie = null;

        public Dictionary<string, SyncCookieRun> CookieRuns = new Dictionary<string, SyncCookieRun>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region SyncCookie

        public string CookieName;

        public string CookiePath;

        public DateTime CookieDate;

        public byte[] Cookie;

        public Dictionary<string, Int64> UsnVectors = new Dictionary<string, Int64>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region constructor

        internal SyncBase()
        { }

        #endregion
    
        #region methods



        #endregion
    }

    public class SyncCollection : SyncBase
    {       
        #region fields
        // see SyncBase
        #endregion
        
        #region constructor

        internal SyncCollection() : base()
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

    public class SyncForest : SyncBase
    {
        #region fields
        // see SyncBase
        #endregion

        #region constructor

        internal SyncForest() : base()
        { }

        internal SyncForest(string name, string path) : base()
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

    public class SyncRoot : SyncBase
    {       
        #region fields
        //see SyncBase
        #endregion

        #region constructor

        internal SyncRoot() : base()
        { }

        internal SyncRoot(string forestName, string name, string path) : base()
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
            ret.Scope = scope.ToString();

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

    public class SyncRun : SyncBase
    {        
        #region fields
        //see SyncBase
        #endregion

        #region constructor

        internal SyncRun() : base()
        { }

        internal SyncRun(string forestName, string rootName, string name, string path) : base()
        { LoadInternal(forestName, rootName, name, path); }

        #endregion

        #region methods

        public void Serialize()
        {
            string xmlfile = Path.Combine(RunPath, XML_NAME);

            XDocument runxml = new XDocument();

            XElement runroot = new XElement(ROOT_ID);

            runroot.Add(new XElement(DN_ID, new XAttribute(VALUE_ID, EntryPoint)));

            runroot.Add(new XElement(FILTER_ID, new XAttribute(VALUE_ID, Filter)));

            runroot.Add(new XElement(ATTRIBUTES_ID, new XAttribute(VALUE_ID, Attributes)));

            runroot.Add(new XElement(SCOPE_ID, new XAttribute(VALUE_ID, Scope)));

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
                Scope = ReadXmlInfo(runxml, SCOPE_ID);
                
            }
        }

        private string ReadXmlInfo(XDocument runXml, string valueName)
        {
            string ret = String.Empty;

            XElement xmlinfo = runXml.Descendants(valueName).FirstOrDefault();

            if (xmlinfo != null)
            { ret = xmlinfo.Attribute(VALUE_ID).Value; }

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

    public class SyncCookieRun : SyncBase
    {
        #region fields
        //see SyncBase
        #endregion

        #region constructor

        internal SyncCookieRun(string cookieRunName, string cookieRunPath, byte[] binCookie = null, ReplicationCursorCollection replInfo = null)
        { LoadInternal(cookieRunName, cookieRunPath, binCookie, replInfo); }

        #endregion

        #region methods

        private void LoadInternal(string cookieRunName, string cookieRunPath, byte[] binCookie, ReplicationCursorCollection replInfo)
        {
            CookieName = cookieRunName;

            CookiePath = cookieRunPath;

            CookieDate = Directory.GetCreationTime(CookiePath);

            LoadCookie(binCookie);

            LoadUsn(replInfo);            
        }

        private void LoadCookie(byte[] binCookie)
        {
            if (binCookie != null)
            { SerializeCookie(binCookie); }

            else if (GlobalHelper.PathContainsFile(CookiePath, COOKIE_NAME))
            {
                using (FileStream fs = new FileStream(Path.Combine(CookiePath, COOKIE_NAME), FileMode.Open, FileAccess.Read))
                {
                    Cookie = (byte[])new BinaryFormatter().Deserialize(fs);

                    fs.Close();
                }
            }
        }

        private void SerializeCookie(byte[] binCookie)
        {
            Cookie = binCookie;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(CookiePath, FileMode.Create))
                {
                    formatter.Serialize(fs, binCookie);

                    fs.Flush();

                    fs.Close();
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        private void LoadUsn(ReplicationCursorCollection replInfo)
        {
            if (replInfo != null)
            { SerializeUsn(replInfo); }

            else if (GlobalHelper.PathContainsFile(CookiePath, USN_FILE_NAME))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CookiePath, USN_FILE_NAME)))
                {
                    string infoline = String.Empty;

                    while (!sr.EndOfStream)
                    {
                        infoline = sr.ReadLine();

                        UsnVectors.Add((infoline.Split(';'))[0], Convert.ToInt64((infoline.Split(';'))[1]));
                    }

                    sr.Close();
                }
            }
        }

        private void SerializeUsn(ReplicationCursorCollection replInfo)
        {
            using (StreamWriter swriter = new StreamWriter(Path.Combine(CookiePath, USN_FILE_NAME), false))
            {
                foreach (ReplicationCursor replcursor in replInfo)
                {
                    if (!(replcursor.SourceServer == null))
                    {
                        swriter.WriteLine(replcursor.SourceServer + ";" + replcursor.UpToDatenessUsn);
                        UsnVectors.Add(replcursor.SourceServer, replcursor.UpToDatenessUsn);
                    }
                }

                swriter.Close();
            }
        }

        #endregion
    }
}
