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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class StatsData
    {
        #region fields

        public int CallTime;
        public long EntriesReturned;
        public long EntriesVisited;
        public string Filter;
        public string Index;

        public List<DynamicBerConverterField> Fields;

        #endregion

        #region constructor

        public StatsData(List<StatsData> statistics)
        {
            Filter = statistics[0].GetValueFromList("Filter").ToString();
            Index = statistics[0].GetValueFromList("Index").ToString();

            foreach (StatsData stats in statistics)
            {
                int ctime = (int)statistics[0].GetValueFromList("CallTime");

                CallTime += ctime;
            }

            EntriesVisited = (int)statistics.Last().GetValueFromList("EntriesVisited");
            EntriesReturned = (int)statistics.Last().GetValueFromList("EntriesReturned");

            Fields = statistics.Last().Fields;

            RemoveFieldsFromList(new List<string> { "Filter", "Index", "CallTime", "EntriesVisited", "EntriesReturned" });
        }

        public StatsData(byte[] statsControlValue)
        {
            DynamicBerConverter dynber = MainBase.DynamicDll.BerConverterList.Where(b => b.Key == "StatsData").FirstOrDefault().Value;

            Fields = dynber.Decode(statsControlValue);            
        }

        #endregion

        #region methods

        public List<string[]> ToListView(long ms)
        {
            List<string[]> ret = new List<string[]> { };

            ret.Add(new string[2] { "codeTime", ms.ToString() });
            ret.Add(new string[2] { "callTime", CallTime.ToString() });

            string cost = ((double)EntriesReturned / EntriesVisited).DynamicStringRounder(true);

            string success = string.Format("{0} ({1})", EntriesReturned.ToString(), cost);
            
            ret.Add(new string[2] { "entriesReturned", success });

            ret.Add(new string[2] { "entriesVisited", EntriesVisited.ToString() });

            foreach (DynamicBerConverterField bf in Fields)
            { ret.Add(new string[2] { bf.Name, bf.Value.ToString() }); }

            return ret;
        }

        public string FormatToString(long ms)
        {
            string ret = String.Empty;

            string success = string.Format("{0} ({1})", EntriesReturned.ToString(), ((double)EntriesReturned / EntriesVisited).ToString("p"));

            string tfilter = Filter;

            if (Filter.Contains(Environment.NewLine))
            {
                tfilter = Filter.Replace(Environment.NewLine, Environment.NewLine + "\t\t");
            }

            ret = String.Format("Statistics:{6}" + 
                                "\tcodeTime: {0} (ms){6}" +
                                "\tcallTime: {1} (ms){6}" +
                                "\tentriesReturned: {2}{6}" +
                                "\tentriesVisited: {3} {6}" +
                                "\tfilter: {6}\t\t{4}{6}" +
                                "\tindex: {6}\t\t{5}{6}",
                                ms,
                                CallTime,
                                success,
                                EntriesVisited,
                                tfilter,
                                Index,
                                Environment.NewLine);

            foreach (DynamicBerConverterField bf in Fields)
            { ret = ret + "\t" + bf.Printable; }

            return ret;
        }

        private object GetValueFromList(string valName)
        {
            object ret = (int)0;

            try
            {
                DynamicBerConverterField retfield = Fields.Where(f => String.Equals(f.Name, valName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                if (retfield != null)
                {
                    if (retfield.Value != null)
                    { ret = retfield.Value; }
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        private void RemoveFieldsFromList(List<string> valNames)
        {
            foreach (string name in valNames)
            {
                try
                {
                    DynamicBerConverterField retfield = Fields.Where(f => String.Equals(f.Name, name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                    if (retfield != null)
                    { Fields.Remove(retfield); }
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }
        }

        #endregion
    }
}
