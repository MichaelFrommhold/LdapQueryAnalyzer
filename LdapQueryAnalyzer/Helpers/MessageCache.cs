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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class MessageCache
    {
        #region fields

        private List<string> innerMsg = new List<string> { };

        public List<string> DecodedMsg
        { get { return innerMsg; } }

        public long ResultCount = 0;

        #endregion

        #region constructors

        internal MessageCache(List<string> results, long count)
        {
            innerMsg = results;

            ResultCount = count;
        }

        #endregion

        #region methods

        public void AddMessages(List<string> value, long count)
        {
            ResultCount = count;
            innerMsg.AddRange(value);
        }

        public void InsertMessages(int insertAt, List<string> value)
        { innerMsg.InsertRange(insertAt, value); }

        public void Dispose()
        {
            innerMsg.Clear();
            innerMsg = null;
        }

        #endregion
    }
}
