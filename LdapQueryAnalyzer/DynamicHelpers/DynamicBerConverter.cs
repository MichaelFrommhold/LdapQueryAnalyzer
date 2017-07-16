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
    public class DynamicBerConverter
    {
        #region fields

        public bool IsDirty = false;

        public string Name;

        public bool IncludeTags = false;

        public Dictionary<int, string> ConversionRules = new Dictionary<int, string>();

        public Dictionary<int, string> ConversionRulesPrint = new Dictionary<int, string>();

        public Dictionary<int, Dictionary<int, string>> FieldNames = new Dictionary<int, Dictionary<int, string>>();

        public Dictionary<int, Dictionary<int, Type>> FieldTypes = new Dictionary<int,Dictionary<int,Type>>();

        public int Size { get { return FieldNames.Count; } }

        #endregion

        #region methods

        public int SetConversionRule(string rule, int curPos = -1)
        {
            if (rule == null) return curPos;

            int berpos = curPos;

            if (berpos == -1)
            { berpos = FieldNames.Count; }

            FieldTypes.Add(berpos, new Dictionary<int, Type>());

            FieldNames.Add(berpos, new Dictionary<int, string>());

            string crule = String.Empty;
            string crulep = String.Empty;

            int step = IncludeTags ? 1 : 0;

            for (int cnt = 0; cnt < rule.Length; cnt += step + 1)
            {
                string f = rule.Substring(cnt + step, 1);

                InitialFields(berpos, (cnt + step), f, ref crule);

                crulep += rule.Substring(cnt, step + 1) + " ";                
            }

            crule = "{" + rule + "}"; 
            

            ConversionRules.Add(berpos, crule);

            ConversionRulesPrint.Add(berpos, crulep);

            return berpos;
        }

        public void AddFieldName(string name, int curPos = -1)
        {
            int berpos = curPos;

            if (berpos == -1)
            { berpos = (FieldNames.Count == 0) ? FieldNames.Count : FieldNames.Count - 1; }

            try
            {
                int pos = FieldNames[berpos].Where(f => f.Value == "internal unknown").FirstOrDefault().Key;

                FieldNames[berpos][pos] = name;
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public List<DynamicBerConverterField> Decode(byte[] val)
        {
            List<DynamicBerConverterField> ret = new List<DynamicBerConverterField>();

            object[] decoded = null;

            int berpos = -1;

            foreach (int curpos in ConversionRules.Keys)
            {
                try                
                {
                    decoded = BerConverter.Decode(ConversionRules[curpos], val);

                    berpos = curpos;

                    break;
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            if (berpos != -1)
            {
                try
                {
                    foreach (int pos in FieldNames[berpos].Keys)
                    {
                        DynamicBerConverterField bf = new DynamicBerConverterField();

                        bf.Name = FieldNames[berpos][pos];

                        bf.FieldType = FieldTypes[berpos][pos];

                        bf.Value = Convert.ChangeType(decoded[pos], bf.FieldType);

                        if (bf.FieldType == typeof(string))
                        { bf.Value = ((string)bf.Value).Replace("\0", String.Empty); } // \0 -> nothing returned due to missing perms?

                        ret.Add(bf);
                    }
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            return ret;
        }

        public List<string> DecodePrintable(byte[] val)
        {
            List<string> ret = new List<string>();

            List<DynamicBerConverterField> fields = Decode(val);

            if (fields.Count != 0)
            {
                foreach (DynamicBerConverterField field in fields)
                { ret.AddFormatted("\t\t{0}: {1}", field.Name, field.Value.ToString()); }
            }

            return ret;
        }

        public void RemoveFromBerList(int berPos)
        {
            ConversionRules.RemoveSafe<int, string>(berPos);

            ConversionRulesPrint.RemoveSafe<int, string>(berPos);

            FieldNames.RemoveSafe<int, Dictionary<int, string>>(berPos);

            FieldTypes.RemoveSafe<int, Dictionary<int, Type>>(berPos);
        }

        private void InitialFields(int pos, int cnt, string fieldID, ref string crule)
        {
            switch (fieldID)
            {
                case "i":

                    FieldTypes[pos].Add(cnt, typeof(int));

                    FieldNames[pos].Add(cnt, "internal unknown");

                    crule += IncludeTags ? "i" + fieldID : fieldID;

                    break;

                case "a":

                    FieldTypes[pos].Add(cnt, typeof(string));

                    FieldNames[pos].Add(cnt, "internal unknown");

                    crule += IncludeTags ? "i" + fieldID : fieldID;

                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
