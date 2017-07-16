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
    public class GroupInfo : SidInfo
    {
        #region fields

        public List<SidInfo> Members { get; set; }

        public List<string> SidList { get; set; }

        #endregion

        #region constructors

        public GroupInfo(string sddlSid, string name)
            : base(sddlSid, name)
        {
            this.Members = new List<SidInfo> { };

            this.SidList = new List<string> { };

            GlobalEventHandler.NestingFound += HandleNesting;
        }

        ~GroupInfo()
        { GlobalEventHandler.NestingFound -= HandleNesting; }

        #endregion

        #region methods

        private void HandleNesting(object sender, GlobalEventArgs args)
        {
            EventValue gval = args.Values.GetValueSafe("Group");

            string groupsid = gval.GetTypedValue<string>();

            EventValue mval = args.Values.GetValueSafe("Member");

            SidInfo member = (SidInfo)mval.GetTypedValue<object>();

            if (!this.SidList.Contains(groupsid))
            {
                this.Members.Add(member);

                this.SidList.Add(member.Sid);

                GlobalEventHandler.RaiseNestingFound(this.Sid, member);
            }
        }

        #endregion
    }

}
