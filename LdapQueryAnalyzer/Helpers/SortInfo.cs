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

using System.DirectoryServices.Protocols;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SortInfo : SortKey
    {
        public SortKey BaseSortKey { get { return new SortKey(base.AttributeName, base.MatchingRule, base.ReverseOrder); } }
        public SortInfo()
            : base()
        { }

        public SortInfo(string attributeName, bool reverseOrder, string matchingRule = "1.2.840.113556.1.4.1499")
            : base(attributeName, matchingRule, reverseOrder)
        { }

        public string ToMD5Hash()
        {
            string ret = base.AttributeName;

            ret = ret.ToMD5Hash();

            return ret;
        }

        public override string ToString()
        { return base.AttributeName + ":" + base.ReverseOrder.ToString(); }
    }
}
