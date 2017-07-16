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
using System.Security.Principal;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class HexInfo
    {
        #region fields

        public bool IsGuid = false;

        #endregion

        #region constructor

        internal HexInfo() { }

        #endregion

        #region methods

        public string ToHex(string value, string name)
        {
            string ret = value;

            if (IsGuid)
            { ret = FromGuid(value); }

            return ret;
        }

        public string FromGuid(Guid value)
        {
            string ret = value.ToString();

            byte[] bar = value.ToByteArray();

            ret = FromByte(bar);

            return ret;
        }

        public string FromGuid(string value)
        {
            string ret = value;

            try
            {
                byte[] bar = new Guid(value).ToByteArray();

                ret = FromByte(bar);
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public string FromSid(SecurityIdentifier value)
        {
            string ret = value.Value;

            byte[] bar = new byte[value.BinaryLength];

            value.GetBinaryForm(bar, 0);

            ret = FromByte(bar);

            return ret;
        }

        public string FromSid(string value)
        {
            string ret = value;

            try
            {

                SecurityIdentifier sid = new SecurityIdentifier(value);

                byte[] bar = new byte[sid.BinaryLength];

                sid.GetBinaryForm(bar, 0);

                ret = FromByte(bar);
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public string FromNumeric(object value)
        {
            string ret = value.ToString();

            byte[] temp = new byte[] { };

            Type valtype = value.GetType();

            if (valtype.Equals(typeof(sbyte)))
            { temp = BitConverter.GetBytes((sbyte)value); }

            else if (valtype.Equals(typeof(byte)))
            { temp = BitConverter.GetBytes((byte)value); ; }

            else if (valtype.Equals(typeof(short)) || valtype.Equals(typeof(Int16)))
            { temp = BitConverter.GetBytes((short)value); }

            else if (valtype.Equals(typeof(ushort)) || valtype.Equals(typeof(UInt16)))
            { temp = BitConverter.GetBytes((ushort)value); }

            else if (valtype.Equals(typeof(int)) || valtype.Equals(typeof(Int32)))
            { temp = BitConverter.GetBytes((int)value); }

            else if (valtype.Equals(typeof(uint)) || valtype.Equals(typeof(UInt32)))
            { temp = BitConverter.GetBytes((uint)value); }

            else if (valtype.Equals(typeof(long)) || valtype.Equals(typeof(Int64)))
            { temp = BitConverter.GetBytes((long)value); }

            else if (valtype.Equals(typeof(ulong)) || valtype.Equals(typeof(UInt64)))
            { temp = BitConverter.GetBytes((ulong)value); }

            else if (valtype.Equals(typeof(float)) || valtype.Equals(typeof(Single)))
            { temp = BitConverter.GetBytes((float)value); }

            else if (valtype.Equals(typeof(double)))
            { temp = BitConverter.GetBytes((double)value); }

            if (BitConverter.IsLittleEndian)
            { Array.Reverse(temp); }

            byte[] bar = temp;

            ret = FromByte(bar);

            return ret;
        }

        private string FromByte(byte[] value)
        {
            string ret = String.Empty;

            try
            {
                ret = BitConverter.ToString(value, 0);

                ret = "\\" + ret.Replace("-", "\\");
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        #endregion

    }
}
