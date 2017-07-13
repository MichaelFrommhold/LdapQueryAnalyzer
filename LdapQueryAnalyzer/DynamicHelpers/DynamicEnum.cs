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

using System.Globalization;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DynamicEnum
    {
        #region fields

        public bool IsDirty = true;

        public bool HasValues { get { return (Values.Count > 0); } }

        public string Name;

        public bool SetAsFlags = false;

        public Type InheritedType = typeof(int);

        public Dictionary<string, string> StringValues = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public Dictionary<string, object> Values = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region methods

        public void AddEnumField(string name, string val)
        {
            int ivalue = 0;
            long lvalue = 0;

            string temp = val;

            if (StringValues.ContainsKey(name))
            { return;  }

            if (Values.ContainsKey(name))
            { return; }

            if (temp.ToLowerInvariant().Contains("0x") || temp.ToLowerInvariant().Contains("&h"))
            {
                bool isminus = temp.StartsWith("-");

                if (isminus)
                { temp = temp.Substring(1); }

                temp = temp.Substring(2);

                if (int.TryParse(temp, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ivalue))
                {
                    StringValues.Add(name, val);

                    Values.Add(name, ivalue);
                }

                else if (long.TryParse(temp, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out lvalue))
                {
                    if (isminus)
                    { lvalue = lvalue * -1; }

                    InheritedType = typeof(long);

                    StringValues.Add(name, val);

                    Values.Add(name, lvalue);
                }
            }

            else
            {
                if (int.TryParse(val, out ivalue))
                {
                    StringValues.Add(name, val);

                    Values.Add(name, ivalue);
                }

                else if (long.TryParse(val, out lvalue))
                {
                    InheritedType = typeof(long);

                    StringValues.Add(name, val);

                    Values.Add(name, lvalue);
                }
            }
        }

        #endregion
    }
}
