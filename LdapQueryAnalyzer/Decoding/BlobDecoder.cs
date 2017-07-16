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
using System.Security.Principal;
using System.Text;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    internal class BlobDecoder
    {
        #region fields

        public Dictionary<string, DecoderSequence> ReplFields = new Dictionary<string, DecoderSequence> { };
        public Dictionary<string, Dictionary<string, DecoderSequence>> ArrayFields = new Dictionary<string, Dictionary<string, DecoderSequence>> { };
        #endregion

        #region constructor

        internal BlobDecoder()
        { }

        #endregion

        public object DecodeField(byte[] value, DecoderSequence seq)
        {
            object ret = null;

            if (!seq.IsArray)
            {
                byte[] subval = new byte[seq.Length];

                Array.Copy(value, seq.StartPoint, subval, 0, seq.Length);

                ret = DecodeFieldInternal(subval, seq);
            }

            return ret;
        }

        public string DecodeFieldString(byte[] value, DecoderSequence seq)
        {
            string ret = string.Empty;

            if (!seq.IsArray)
            {
                byte[] subval = new byte[seq.Length];

                Array.Copy(value, seq.StartPoint, subval, 0, seq.Length);

                ret = DecodeFieldStringInternal(subval, seq);
            }

            return ret;
        }

        public List<string> Decode(byte[] value)
        {
            List<string> ret = new List<string> { };

            foreach (KeyValuePair<string, DecoderSequence> field in ReplFields)
            {
                string temp = String.Format("\t\t<{0}: ", field.Key);

                if (!field.Value.IsArray)
                {
                    byte[] subval = new byte[field.Value.Length];

                    Array.Copy(value, field.Value.StartPoint, subval, 0, field.Value.Length);

                    temp += DecodeFieldInternal(subval, field.Value);

                    ret.Add(temp);
                }

                else
                {
                    ret.Add(temp);

                    Dictionary<string, DecoderSequence> subfields = ArrayFields[field.Key];

                    int start = field.Value.StartPoint;

                    while (value.Length >= start + field.Value.Length)
                    {
                        foreach (KeyValuePair<string, DecoderSequence> subfield in subfields)
                        {
                            temp = String.Format("\t\t\t<{0}: ", subfield.Key);

                            byte[] arrayval = new byte[subfield.Value.Length];

                            Array.Copy(value, subfield.Value.StartPoint + start, arrayval, 0, subfield.Value.Length);

                            temp += DecodeFieldInternal(arrayval, subfield.Value);

                            ret.Add(temp);
                        }

                        start += field.Value.Length;
                    }

                    ret.Add("\t\t>");
                }
            }

            return ret;
        }

        protected string DecodeFieldStringInternal(byte[] subValue, DecoderSequence seq)
        {
            string ret = String.Empty;

            ret = DecodeFieldInternal(subValue, seq).ToString();

            ret = String.Format("{0}>", ret);

            return ret;
        }

        private object DecodeFieldInternal(byte[] subValue, DecoderSequence seq)
        {
            object ret = null;

            switch (seq.FieldType)
            {
                case FIELD_TYPES.i:

                    switch (seq.Length)
                    {
                        case 1:

                            ret = (int)subValue[0];

                            break;

                        case 4:

                            ret = BitConverter.ToInt32(subValue, 0);

                            break;

                        case 8:

                            ret = BitConverter.ToInt64(subValue, 0);

                            break;
                    }

                    break;

                case FIELD_TYPES.u:
                    ret = Encoding.UTF8.GetString(subValue);

                    break;

                case FIELD_TYPES.a:

                    int ce = CheckEncoding(subValue);

                    switch (ce)
                    {
                        case 4:
                            ret = Encoding.ASCII.GetString(subValue);

                            break;

                        case 8:
                            ret = Encoding.UTF8.GetString(subValue);

                            break;

                        case 32:
                            ret = Encoding.UTF32.GetString(subValue);

                            break;
                    }

                    break;

                case FIELD_TYPES.D:

                    Int64 lval = BitConverter.ToInt64(subValue, 0);

                    // we got seconds, not ticks (100 nanosecond steps)
                    lval = lval * (Int64)Math.Pow(10, 7);

                    ret = DateTime.FromFileTime(lval);

                    break;

                case FIELD_TYPES.d:

                    long ht = BitConverter.ToInt32(subValue, 0);

                    long lt = BitConverter.ToInt32(subValue, 4);

                    if (lt < 0)
                    { ht += 1; }

                    lval = (ht << 32);

                    lval = lval + lt;

                    ret = DateTime.FromFileTime(lval);

                    break;

                case FIELD_TYPES.G:

                    ret = new Guid(subValue);

                    break;

                case FIELD_TYPES.S:

                    ret = new SecurityIdentifier(subValue, 0);

                    break;
            }

            return ret;
        }

        protected int CheckEncoding(byte[] value)
        {
            int ret = 4;

            if (value.Select(x => x > 127).FirstOrDefault())
            {
                ret = 8;

                if (value.Select(x => x > 195).FirstOrDefault())
                { ret = 32; }
            }

            return ret;
        }
    }

}
