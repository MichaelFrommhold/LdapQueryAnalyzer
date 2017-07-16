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


namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class ADObjectInfo
    {
        #region Fields

        public bool Tag { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string ObjectGuid { get; set; }

        public string ObjectClass { get; set; }

        public string LastKnownParent { get; set; }
              
        public string LastKnownRDN { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsRecycled { get; set; }

        #endregion

        #region constructors

        internal ADObjectInfo()
        { LoadInternal(null, null, null, null, null, null, false, false); }

        internal ADObjectInfo(string rdn, 
                              string dn, 
                              string oGuid, 
                              string oClass,
                              string lastDN = null, 
                              string lastRDN = null, 
                              bool deleted = false, 
                              bool recycled = false)
        { LoadInternal(rdn, dn, oGuid, oClass, lastDN, lastRDN, deleted, recycled); }

        #endregion

        #region methods

        private void LoadInternal(string rdn,
                                    string dn,
                                    string oGuid,
                                    string oClass,
                                    string lastDN,
                                    string lastRDN,
                                    bool deleted,
                                    bool recycled)
        {
            Name = rdn;
            Path = dn;
            ObjectGuid = oGuid;
            LastKnownParent = lastDN;
            LastKnownRDN = lastRDN;
            IsDeleted = deleted;
            IsRecycled = recycled;
            Tag = false;
        }

        #endregion
    }
}
