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
using System.DirectoryServices.Protocols;
using System.Linq;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class FilterInfo
    {
        #region constants

        public const string FilterVersion = "2";

        public static string EndFilter = "|#|ENDFILTER|#|" + Environment.NewLine;

        protected const string SeperateAttribs = "|#|";
        protected const string WrapFilter = "|#|FILTER|#|";
        protected const string WrapAttributes = "|#|ATTRIBS|#|";
        protected const string WrapASQ = "|#|ASQ|#|";
        protected const string WrapSort= "|#|SORT|#|";
        protected const string WrapBase = "|#|BASE|#|";
        protected const string WrapScope = "|#|SCOPE|#|";
        protected const string WrapDC = "|#|DCINSTANCE|#|";
        protected const string WrapPort = "|#|PORT|#|";

        #endregion

        #region fields

        public string Filter { get; set; }

        public int Scope { get; set; }

        public string SearchBase { get; set; }

        public string[] Attributes { get; set; }

        public string[] ASQ { get; set; }

        public SortInfo Sort { get; set;}

        public int Port { get; set; }

        public string DC { get; set; }

        #endregion

        #region constructors

        public FilterInfo(string line)
        { LoadInternal(line); }

        public FilterInfo()
        { }

        #endregion

        #region methods

        protected void LoadInternal(string line)
        {
            string templine = "";

            Filter = "0";

            if (ExtractValues(line, WrapFilter, out templine))
            { Filter = templine; }


            Attributes = new string[] { "0" };

            if (ExtractValues(line, WrapAttributes, out templine))
            {
                string[] temp = templine.Split(new string[] { SeperateAttribs }, StringSplitOptions.RemoveEmptyEntries);

                if (temp.Count() > 0)
                { Attributes = temp; }
            }


            ASQ = null;

            if (ExtractValues(line, WrapASQ, out templine))
            {
                string[] temp = templine.Split(new string[] { SeperateAttribs }, StringSplitOptions.RemoveEmptyEntries);

                if (temp.Count() > 0)
                { ASQ = temp; }
            }


            SearchBase = "0";

            if (ExtractValues(line, WrapBase, out templine))
            { SearchBase = templine; }


            Sort = null;

            if (ExtractValues(line, WrapSort, out templine))
            {
                string[] temp = templine.Split(new string[] { SeperateAttribs }, StringSplitOptions.RemoveEmptyEntries);

                if (temp.Count() > 0)
                { 
                    string[] sortmp = temp[0].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                    if (sortmp.Count() > 0)
                    {
                        if (sortmp[0] != "0")
                        {
                            Sort = new SortInfo();

                            Sort.AttributeName = sortmp[0];

                            bool tbool = false;

                            if (sortmp.Count() > 1)
                            { bool.TryParse(sortmp[1], out tbool); }

                            Sort.ReverseOrder = tbool;
                        }
                    }
                }
            }

            Scope = -1;

            if (ExtractValues(line, WrapScope, out templine))
            {
                int tscope = -1;

                if (Int32.TryParse(templine, out tscope))
                { Scope = tscope; }
            }


            DC = "0";
            
            if (ExtractValues(line, WrapDC, out templine))
            { DC = templine; }


            Port = 0;

            if (ExtractValues(line, WrapPort, out templine))
            {
                int tport = -1;

                if (Int32.TryParse(templine, out tport))
                { Port = tport; }
            }
        }

        public string[] Items()
        {
            List<string> ret = new List<string> { "" };

            ret.Add(Filter);

            ret.Add(FormattedAttributes(Attributes));

            ret.Add(FormattedAttributes(ASQ));

            ret.Add(FormattedSort());

            ret.Add(SearchBase == "" ? "rootDSE" : SearchBase);

            ret.Add(DC);

            ret.Add(((SearchScope)Scope).ToString());

            ret.Add(Port.ToString());

            return ret.ToArray();
        }

        public string ToLine()
        {
            string ret = "";

            ret = WrapFilter + Filter + WrapFilter;

            ret += WrapAttributes + FormattedAttributes(Attributes, true) + WrapAttributes;

            ret += WrapASQ + FormattedAttributes(ASQ, true) + WrapASQ;

            ret += WrapSort + FormattedSort(true) + WrapSort;

            ret += WrapBase + SearchBase + WrapBase;

            ret += WrapScope + Scope.ToString() + WrapScope;

            ret += WrapDC + DC + WrapDC;

            ret += WrapPort + Port.ToString() + WrapPort;

            ret += EndFilter;

            return ret;
        }

        protected string FormattedAttributes(string[] list, bool forStore = false)
        {
            string ret = forStore ? "0" : "none";

            if ((list != null) && (list.Count() != 0))
            {
                ret = forStore ? String.Join(SeperateAttribs, list) : String.Join(", ", list);
            }

            if ((!forStore) && (ret == "0"))
            { ret = "none"; }

            return ret;
        }

        protected string FormattedSort(bool forStore = false)
        {
            string ret = forStore ? "0" : "none";

            if (Sort != null)
            {      
                ret = Sort.ToString();
            }

            return ret;
        }

        protected bool ExtractValues(string line, string wrap, out string value)
        {
            bool ret = false;

            value = null;

            int posa = 0;

            int posb = 0;

            posa = line.IndexOf(wrap);

            posb = line.IndexOf(wrap, posa + 1);

            if ((posa != -1) && (posb != -1))
            {
                value = line.Substring(posa, posb - posa);

                value = value.Replace(wrap, string.Empty);

                if ((value != null) && (value.Length > 0))
                { ret = true ; }
            }

            return ret;
        }

        public string ToMD5Hash()
        { return ToLine().ToMD5Hash(); }

        public override string ToString()
        { return ToLine(); }

        #endregion
    }

}
