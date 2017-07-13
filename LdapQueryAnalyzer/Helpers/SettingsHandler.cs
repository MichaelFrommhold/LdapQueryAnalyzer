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

using System.IO;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SettingsHandler
    {
        #region fields

        public const string CachePath = "Cache\\Settings";
        public const string FileName = "Settings.xml";

        private bool registered = false;

        private bool innerAutoPage = true;
        private bool innerDoValueRangeRetrieval = true;
        private bool innerSortAscending = false;
        private bool innerSortDescending = false;
        private bool innerDecodeGUID = true;
        private bool innerDecodeSID = true;
        private bool innerDecodeUserParameters = false;
        private bool innerResolveSids = false;
        private bool innerDecodeSD = false;
        private bool innerDecodeOctetStrings = false;
        private bool innerDecodeReplicaLinks = false;
        private bool innerDecodePrimaryGroupID = false;
        private bool innerUseLocalTime = true;
        private bool innerShowPartialResults = false;
        private int innerMaxResultListCount = 20;
        private long innerMaxResultCount = 1000;
        private bool innerIgnoreEmpty = true;
        private bool innerHistoryBase = false;
        private bool innerHistoryScope = false;
        private bool innerHistoryASQ = false;
        private bool innerHistorySorting = false;
        private bool innerHistoryDC = false;
        private bool innerHistoryPort= false;
        private bool innerHistoryAll = false;
        public bool AutoPage
        {
            get { return innerAutoPage; }
            set {innerAutoPage = value; }
        }

        public bool DoValueRangeRetrieval
        {
            get { return innerDoValueRangeRetrieval; }
            set {innerDoValueRangeRetrieval = value; }
        }

        public bool SortAscending
        {
            get { return innerSortAscending; }
            set {innerSortAscending = value; }
        }

        public bool SortDescending
        {
            get { return innerSortDescending; }
            set {innerSortDescending = value; }
        }

        public bool DecodeGUID
        {
            get { return innerDecodeGUID; }
            set {innerDecodeGUID = value; }
        }

        public bool DecodeSID
        {
            get { return innerDecodeSID; }
            set {innerDecodeSID = value; }
        }

        public bool DecodeUserParameters
        {
            get { return innerDecodeUserParameters; }
            set {innerDecodeUserParameters = value; }
        }

        public bool ResolveSids
        {
            get { return innerResolveSids; }
            set {innerResolveSids = value; }
        }

        public bool DecodeSD
        {
            get { return innerDecodeSD; }
            set {innerDecodeSD = value; }
        }

        public bool DecodeOctetStrings
        {
            get { return innerDecodeOctetStrings; }
            set {innerDecodeOctetStrings = value; }
        }

        public bool DecodeReplicaLinks
        {
            get { return innerDecodeReplicaLinks; }
            set {innerDecodeReplicaLinks = value; }
        }

        public bool DecodePrimaryGroupID
        {
            get { return innerDecodePrimaryGroupID; }
            set {innerDecodePrimaryGroupID = value; }
        }

        public bool UseLocalTime
        {
            get { return innerUseLocalTime; }
            set {innerUseLocalTime = value; }
        }

        public bool ShowPartialResults
        {
            get { return innerShowPartialResults; }
            set { innerShowPartialResults = value; }
        }

        public int MaxResultListCount
        {
            get { return innerMaxResultListCount; }
            set { innerMaxResultListCount = value; }
        }

        public long MaxResultCount
        {
            get { return innerMaxResultCount; }
            set { innerMaxResultCount = value; }
        }

        public bool IgnoreEmpty
        {
            get { return innerIgnoreEmpty; }
            set { innerIgnoreEmpty = value; }
        }
        
        public bool HistoryBase
        {
            get { return innerHistoryBase; }
            set { innerHistoryBase = value; }
        }

        public bool HistoryScope
        {
            get { return innerHistoryScope; }
            set { innerHistoryScope = value; }
        }

        public bool HistoryASQ
        {
            get { return innerHistoryASQ; }
            set { innerHistoryASQ = value; }
        }

        public bool HistorySorting
        {
            get { return innerHistorySorting; }
            set { innerHistorySorting = value; }
        }

        public bool HistoryDC
        {
            get { return innerHistoryDC; }
            set { innerHistoryDC = value; }
        }

        public bool HistoryPort
        {
            get { return innerHistoryPort; }
            set { innerHistoryPort = value; }
        }

        public bool HistoryAll
        {
            get { return innerHistoryAll; }
            set { innerHistoryAll = value; }
        }

        public bool LoadingGui { get; set; }

        #endregion

        #region constructor

        public SettingsHandler()
        { LoadInternal(false); }

        public SettingsHandler(bool load)
        { LoadInternal(load); }

        #endregion

        #region methods

        public void RegisterEvents()
        {
            if (!registered)
            { 
                GlobalEventHandler.SettingChanged += SettingChanged;

                registered = true;
            }
        }

        public void UnRegisterEvents()
        {
            if (registered)
            {
                GlobalEventHandler.SettingChanged -= SettingChanged;

                registered = false;
            }
        }

        public static SettingsHandler Load()
        {
            SettingsHandler ret = new SettingsHandler();;

            string filepath = GetFilePath();

            string sertext = string.Empty;

            if (!File.Exists(filepath))
            {
                sertext = ret.SerializeThis();

                sertext.WriteToFile(filepath, false);
            }

            else
            {
                filepath.ReadFromFile(out sertext);

                sertext.DeSerializeFromString(out ret);
            }

            ret.RegisterEvents();

            return ret;
        }

        private void LoadInternal(bool load)
        {
            LoadingGui = false;

            if (load) RegisterEvents();
        }

        private void SettingChanged(object settingName, GlobalEventArgs args)
        {
            if (LoadingGui) return;

            object ctrlval = this.GetPropertyValue(settingName.ToString());

            if (args.ObjectVal[0] != ctrlval)
            {
                this.SetPropertyValue(settingName.ToString(), args.ObjectVal[0]);

                string sertext = this.SerializeThis();

                string filepath = GetFilePath();

                sertext.WriteToFile(filepath, false);
            }
        }

        private static string GetFilePath()
        {
            string ret = string.Empty;

            bool success;

            ret = GlobalHelper.PathInCurrentDirectory(CachePath, out success);

            if (!success)
            { Directory.CreateDirectory(ret); }

            ret = Path.Combine(ret, FileName);

            return ret;
        }

        #endregion
    }
}
