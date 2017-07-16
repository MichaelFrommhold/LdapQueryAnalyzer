using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DomainInfo
    {
        #region internal fields

        private string inName;

        private string inSID;

        private List<DomainControllerHelper> inDCList;

        #endregion

        #region fields

        public string Name
        {
            get { return inName; }
            set { inName = value; }
        }

        public string SID
        {
            get { return inSID; }
            set { inSID = value; }
        }

        public List<DomainControllerHelper> DCList
        {
            get { return inDCList; }

            set 
            {
                List<DomainControllerHelper> temp = new List<DomainControllerHelper>{};

                temp.AddRange(value);

                inDCList = temp;
            }
        }

        #endregion

        #region constructor
        internal DomainInfo()
        { }

        #endregion

        #region methods
        private void LoadInternal()
        { }

        public override string ToString()
        { return Name; }

        #endregion
    }
}
