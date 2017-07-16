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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public static class ExtensionMethods
    {
        #region constants

        public const string WhiskyBite = "Always carry a flagon of whiskey in case of snakebite and furthermore always carry a small snake.";

        #endregion

        #region dynamic method invocation

        public static object InvokeMethod(this object obj, string methodName, ref object[] args)
        {
            object ret = null;

            try
            {
                MethodInfo inmethod = obj.GetType().GetMethod(methodName);

                if (inmethod == null)
                { return ret; }

                object[] retargs = new object[args.Length];

                ret = inmethod.Invoke(obj, args);

                Array.Copy(args, retargs, args.Length);

                args = retargs;
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static object InvokeMethod(this object obj, string methodName, BindingFlags flags, Type[] argTypes, ref object[] args)
        {
            object ret = null;

            try
            {
                MethodInfo inmethod = obj.GetType().GetMethod(methodName, flags, null, CallingConventions.Any, argTypes, null);

                if (inmethod == null)
                { return ret; }

                object[] retargs = new object[args.Length];

                ret = inmethod.Invoke(obj, args);

                Array.Copy(args, retargs, args.Length);

                args = retargs;
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

       
        public static object InvokeSafe(this Type sType, string methodName)
        {
            object ret = null;

            if (sType == null) return ret;

            object[] args = new object[] { }; ;
            bool success;

            ret = sType.InvokeSafe(null, methodName, BindingFlags.Static | BindingFlags.Public, null, ref args, out success);

            return ret;
        }

        public static object InvokeSafe(this Type sType, string methodName, Type[] argTypes, ref object[] args, out bool success)
        {
            object ret = null;

            string exmsg = String.Empty;

            success = false;

            if (sType == null)
            { exmsg = "Object reference not set to an instance of an object."; ; }

            else
            { ret = sType.InvokeSafe(null, methodName, BindingFlags.Static | BindingFlags.Public, argTypes, ref args, out success); }

            return ret;
        }

        public static object InvokeSafe(this object obj, string methodName)
        {
            object ret = null;

            if (obj == null) return ret;

            object[] args = new object[] { };
            bool success;

            ret = obj.InvokeSafe(methodName, BindingFlags.Public | BindingFlags.Instance, null, ref args, out success);

            return ret;
        }

        public static object InvokeSafe(this object obj, string methodName, BindingFlags flags, Type[] argTypes, ref object[] args, out bool success)
        {
            object ret = null;

            string exmsg = String.Empty;

            success = false;

            if (obj == null)
            { exmsg = "Object reference not set to an instance of an object."; ; }

            else
            { ret = obj.GetType().InvokeSafe(obj, methodName, flags, argTypes, ref args, out success); }

            return ret;
        }

        internal static object InvokeSafe(this Type sType, object obj, string methodName, BindingFlags flags, Type[] argTypes, ref object[] args, out bool success)
        {
            object ret = null;

            string exmsg = String.Empty;

            success = false;

            try
            {
                if (argTypes == null)
                { argTypes = Type.EmptyTypes; }

                MethodInfo inmethod = sType.GetMethod(methodName, flags, null, CallingConventions.Any, argTypes, null);

                if (inmethod == null)
                { return ret; }

                object[] retargs = new object[args.Length];

                ret = inmethod.Invoke(obj, args);

                Array.Copy(args, retargs, args.Length);

                args = retargs;

                success = true;
            }

            catch (Exception ex)
            { exmsg = ex.Message; }

            if (!success)
            {
                Array.Resize<object>(ref args, 1);
                args[0] = exmsg;
            }

            return ret;
        }

        #endregion

        #region dynamic enum casting

        static List<Type> ValTypes = new List<Type>() { typeof(int), typeof(long), typeof(uint), typeof(ulong), typeof(string) };

        public static bool EnumTryParse(this object val, Type enType, out object convEnum)
        {
            bool ret = false;

            MethodInfo encast = null;

            MethodInfo typedencast = null;

            convEnum = null;

            try
            {
                encast = typeof(ExtensionMethods).GetMethod("ToEnum", BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Any, new Type[] { typeof(object), typeof(bool).MakeByRefType() }, null);

                if (encast == null)
                { return ret; }

                typedencast = encast.MakeGenericMethod(enType);

                if (typedencast == null)
                { return ret; }

                object[] args = new object[] { val, ret };

                convEnum = typedencast.Invoke(val, args);

                ret = (bool)args[1];
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static List<string> EnumParse(this object val, Type enType)
        {
            List<string> ret = new List<string>();

            MethodInfo encast = null;

            MethodInfo typedencast = null;

            encast = typeof(ExtensionMethods).GetMethod("Parse", BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Any, new Type[] { typeof(object) }, null);

            if (encast == null)
            { return ret; }

            typedencast = encast.MakeGenericMethod(enType);

            if (typedencast == null)
            { return ret; }

            ret = (List<string>)typedencast.Invoke(val, new object[] { val });

            return ret;
        }

        static T ToEnum<T>(object val, out bool success)
        {
            success = false;

            T ret = default(T);

            ret = ToEnum<T>(val, null, out success);

            return ret;
        }

        static T ToEnum<T>(object val, Type valType, out bool success)
        {
            T ret = default(T);

            success = false;

            Type innertype = val.GetType();

            try
            {
                if (innertype == typeof(string))
                { 
                    ret = (T)Enum.Parse(typeof(T), val.ToString(), true);

                    long ltemp = 0;

                    success = !long.TryParse(ret.ToString(), out ltemp);
                }

                else if (valType != null)
                {
                    ret = (T)Enum.ToObject(typeof(T), Convert.ChangeType(val, valType));

                    long ltemp = 0;

                    success = !long.TryParse(ret.ToString(), out ltemp);
                }

                else
                {
                    foreach (Type vtype in ValTypes)
                    {
                        ret = ToEnum<T>(val, vtype, out success);

                        if (success)
                        { break; }
                    }
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        static Type TrySafe<T>(object val)
        {
            Type ret = null;

            try
            {
                ret = typeof(int);

                if (Enum.IsDefined(typeof(T), (int)val))
                { return ret; }

            }

            catch (Exception ex)
            { ex.ToDummy(); }

            try
            {
                ret = typeof(long);

                if (Enum.IsDefined(typeof(T), (long)val))
                { return ret; }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            try
            {
                ret = typeof(uint);

                if (Enum.IsDefined(typeof(T), (uint)val))
                { return ret; }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            try
            {
                ret = typeof(ulong);

                if (Enum.IsDefined(typeof(T), (ulong)val))
                { return ret; }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            try
            {
                ret = typeof(string);

                if (Enum.IsDefined(typeof(T), (string)val))
                { return ret; }

            }

            catch (Exception ex)
            { ex.ToDummy(); }

            ret = null;

            return ret;
        }

        static List<string> Parse<T>(object val)
        {
            List<string> ret = new List<string>();

            foreach (T field in Enum.GetValues(typeof(T)))
            {
                if (((long)val & (long)Convert.ChangeType(field, typeof(long))) == (long)Convert.ChangeType(field, typeof(long)))
                {
                    ret.Add(field.ToString());

                    val = (long)val ^ (long)Convert.ChangeType(field, typeof(long));

                    if ((long)val == 0)
                    { break; }
                }
            }

            if ((long)val != 0)
            { ret.Add("no hit for: " + val.ToString()); }

            return ret;
        }

        #endregion

        #region events

        #endregion

        #region array

        public static void AddSafe(this string[] value, string newValue, ref string[] ret)
        {
            int len = 1;

            if (value != null)
            { len = value.Length + 1; }

            ret = new string[len];

            if (value != null)
            { Array.Copy(value, ret, len - 1); }

            ret[len - 1] = newValue;
        }

        public static void RemoveSafe(this string[] value, int position, ref string[] ret)
        {
            int len = 0;

            if (value != null)
            {
                if (value.Length != 0)
                { len = value.Length - 1; }
            }

            ret = new string[len];

            if (value != null)
            {
                Array.Copy(value, 0, ret, 0, position);

                Array.Copy(value, position + 1, ret, position, len - position);
            }
        }

        public static bool ContainsSubString(this string[] value, string match, MATCH_POSITION where)
        {
            bool ret = false;

            foreach (string item in value)
            {
                switch (where)
                {
                    case MATCH_POSITION.STARTSWITH:
                        ret = item.ToLowerInvariant().StartsWith(match.ToLowerInvariant());

                        break;

                    case MATCH_POSITION.INSTRING:
                        ret = item.ToLowerInvariant().Contains(match.ToLowerInvariant());
                        break;

                    case MATCH_POSITION.ENDSWITH:
                        ret = item.ToLowerInvariant().EndsWith(match.ToLowerInvariant());

                        break;
                }

                if (ret)
                { break; }
            }

            return ret;
        }

        #endregion

        #region string

        // to numeric positive parse extension
        public static bool TryParsePositiveNumber(this string value, out Int64 retVal)
        {
            bool ret = false;

            retVal = 0;

            ret = Int64.TryParse(value, out retVal);

            retVal = (retVal < 0) ? -retVal : retVal;

            return ret;
        }

        public static string ToMD5Hash(this string value)
        {
            string ret = value;

            if (value == null) return ret;

            MD5 csp = new MD5CryptoServiceProvider();

            byte[] bval = Encoding.Default.GetBytes(value);

            byte[] bhash = csp.ComputeHash(bval);

            ret = BitConverter.ToString(bval);

            return ret;
        }


        public static string TrimChar(this string value, string charToTrim, TRIM_CONTROL trimControl = TRIM_CONTROL.Beginning | TRIM_CONTROL.End)
        {
            string ret = value;

            if ((value.StartsWith(charToTrim)) && ((trimControl & TRIM_CONTROL.Beginning) == TRIM_CONTROL.Beginning))
            {
                int cnt = 0;

                foreach (char c in ret)
                {
                    if (c == Convert.ToChar(charToTrim))
                    { cnt++; }

                    else
                    { break; }
                }

                ret = ret.Substring(cnt);
            }

            if ((value.EndsWith(charToTrim)) && ((trimControl & TRIM_CONTROL.End) == TRIM_CONTROL.End))
            {
                int cnt = 0;

                char[] retarray = ret.ToCharArray();

                for (int pos = retarray.Length - 1; pos > -1; pos--)
                {
                    if (retarray[pos] == Convert.ToChar(charToTrim))
                    { cnt++; }

                    else
                    { break; }
                }

                ret = ret.Substring(0, ret.Length - cnt);
            }

            return ret;
        }
        
        public static bool IsRangeAttribute(this string value, out string attributeName, out int lowVal, out int maxVal)
        {
            bool ret = false;

            attributeName = value; 
            
            lowVal = 0; maxVal = 0;

            if (value.Contains(";range="))
            {
                ret = true;

                attributeName = value.Substring(0, value.IndexOf(";range="));

                string range = value.Remove(0, attributeName.Length + 7);

                string[] vals = range.Split(new char[] { '-' });
                
                if (!int.TryParse(vals[0], out lowVal))
                { ret = false; }

                if (!int.TryParse(vals[1], out maxVal))
                { ret = false; }
            }

            return ret;
        }

        public static string ToFileNameValid(this string value)
        {
            string ret = value;

            foreach (char forbidden in Path.GetInvalidFileNameChars())
            {
                if (ret.Contains(forbidden))
                { ret = ret.Replace(forbidden, '_'); }
            }

            return ret;
        }

        public static string ToPathNameValid(this string value)
        {
            string ret = value;

            foreach (char forbidden in Path.GetInvalidPathChars())
            {
                if (ret.Contains(forbidden))
                { ret = ret.Replace(forbidden, '_'); }
            }

            return ret;
        }

        public static SecureString ToSecureString(this string value)
        {
            SecureString ret = null;

            if (value != null)
            { 
                try
                {
                    ret = new SecureString();

                    foreach (char cval in value.ToCharArray())
                    { ret.AppendChar(cval); }                
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            return ret;
        }

        public static SecureString ToSecureStringSecure(this string value, ref string val)
        {
            SecureString ret = null;

            if (value != null)
            {
                try
                {
                    ret = new SecureString();

                    foreach (char cval in value.ToCharArray())
                    { ret.AppendChar(cval); }

                    val = WhiskyBite;
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            return ret;
        }

        public static string ToEscapedLdapComponent(this string value)
        {
            string ret = String.Empty;

            if (value == null) return value;

            if (value.Length == 0) return value;

            List<char> badchars = new List<char> {',', '+', '"', '\\', '<', '>', ';', '=', '/', '\n', '\r' };

            char[] aval = value.ToArray();

            int cnt = 1;

            foreach (char citem in aval)
            {
                if (cnt == 1)
                {
                    if (value.StartsWith("#") || value.StartsWith(" ") || badchars.Contains(citem))
                    { ret = @"\"; }
                }

                else if (cnt == aval.Length)
                {
                    if ((citem == ' ') || badchars.Contains(citem))
                    { ret += @"\"; }
                }

                ret += citem.ToString();

                cnt++;
            }

            return ret;
        }

        public static string ToUnsecureString(this SecureString value)
        {
            string ret = String.Empty;

            try
            { ret = Marshal.PtrToStringUni(Marshal.SecureStringToBSTR(value)); }
            
            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static bool ToBool(this string value)
        {
            bool ret = true;

            if (bool.TryParse(value, out ret))
            {
                return ret;
            }

            else
            {
                double num;

                value.IsNumeric(out num);

                ret = (num == 0) ? false : true;
            }

            return ret;
        }

        public static bool WriteToFile(this string value, string filePath, bool append, Encoding encoding)
        {
            bool ret = false;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append, encoding))
                {
                    writer.Write(value);

                    writer.Flush();

                    ret = true;
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static bool WriteToFile(this string value, string filePath, bool append)
        {
            bool ret = false;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append))
                {
                    writer.Write(value);

                    writer.Flush();

                    ret = true;
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static bool ReadFromFile(this string value, Encoding encoding, out string data)
        {
            bool ret = false;

            data = String.Empty;

            if (!File.Exists(value))
            { return ret; }

            try
            {
                using (StreamReader reader = new StreamReader(value, encoding))
                {
                    data = reader.ReadToEnd();

                    ret = true;
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static bool ReadFromFile(this string value, out string data)
        {
            bool ret = false;

            data = String.Empty;

            if (!File.Exists(value))
            { return ret; }

            try
            {
                using (StreamReader reader = new StreamReader(value))
                {
                    data = reader.ReadToEnd();

                    ret = true;
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }
       
        #endregion

        #region numeric

        public static bool IsNumeric(this object value)
        {
            bool ret = false;

            double temp;

            ret = value.IsNumeric(out temp);

            return ret;
        }

        public static bool IsNumeric(this object value, out double numericValue)
        {
            bool ret = false;

            ret = double.TryParse(value.ToString(), out numericValue);

            return ret;
        }

        public static bool IsNumeric(this object value, out int numericValue)
        {
            bool ret = false;

            ret = int.TryParse(value.ToString(), out numericValue);

            return ret;
        }
        
        public static string DynamicStringRounder(this double value, bool makePercent = false, int maxDigits = 15)
        {
            string ret = value.ToString("000.00###");

            double temp = value;

            if (makePercent)
            { temp = temp * 100; }

            int decimals = temp.GetDecimals();

            int digits = 0;

            temp = temp.DynamicRounder(ref digits, maxDigits);

            string decprov = new string('0', decimals);
            string digprov = new string('0', digits);

            string formatprov = (digits == 0) ? String.Format("{0}", decprov) : String.Format("{0}.{1}", decprov, digprov);

            ret = temp.ToString(formatprov);

            ret = makePercent ? ret + " %" : ret;

            return ret;
        }

        public static double DynamicRounder(this double value, int maxDigits = 15)
        {
            int digits = 0;

            return value.DynamicRounder(ref digits, maxDigits);
        }

        public static double DynamicRounder(this double value, ref int digits, int maxDigits = 15)
        {
            double ret = value;

            digits = value.GetDigits(maxDigits);

            ret = Math.Round(ret, digits);

            return ret;
        }

        public static int GetDecimals(this double value)
        {
            int ret = 1;

            if (value >= 10)
            {
                double log = Math.Log10(value);

                ret = (int)Math.Ceiling(log);
            }

            return ret;
        }

        public static int GetDigits(this double value, int maxDigits = 15)
        {
            int ret = 0;

            double temp = value;

            temp = Math.Abs(value);

            temp -= (long)temp;

            if (temp > 0)
            {
                for (ret = 0; ret <= maxDigits; ret++)
                {
                    double innertemp = temp * Math.Pow(10, ret);

                    if (innertemp >= 1)
                    { break; }
                }
            }

            return ret;
        }



        public static string DynamicStringRounder(this Single value, bool makePercent = false, int maxDigits = 15)
        { return ((double)value).DynamicStringRounder(makePercent, maxDigits); }

        public static double DynamicRounder(this Single value, int maxDigits = 15)
        { return ((double)value).DynamicRounder(maxDigits); }

        public static double DynamicRounder(this Single value, ref int digits, int maxDigits = 15)
        { return ((double)value).DynamicRounder(ref digits, maxDigits); }

        public static int GetDecimals(this Single value)
        { return ((double)value).GetDecimals(); }

        public static int GetDigits(this Single value, int maxDigits = 15)
        { return ((double)value).GetDigits(maxDigits); }

        #endregion

        #region controls

        public static bool IsInRange(this KeyPressEventArgs value, char lowerChar, char upperChar)
        {
            bool ret = false;

            ret = ((value.KeyChar >= lowerChar) && (value.KeyChar <= upperChar));

            return ret;
        }

        public static bool IsNumeric(this KeyPressEventArgs value)
        {
            bool ret = false;

            ret = value.KeyChar.ToString().IsNumeric();

            return ret;
        }

        public static bool IsEnter(this KeyPressEventArgs value)
        {
            bool ret = false;

            ret = (value.KeyChar == (char)13);

            return ret;
        }

        public static bool IsBackSpace(this KeyPressEventArgs value)
        {
            bool ret = false;

            ret = (value.KeyChar == (char)8);

            return ret;
        }

        public static bool EqualsChar(this KeyPressEventArgs value, char compareChar)
        {
            bool ret = false;

            ret = (value.KeyChar == compareChar);

            return ret;
        }

        public static void SelectItem(this ComboBox value, object item)
        {
            int fi = 0;

            try
            { fi = value.FindString(item.ToString(), 0); }

            catch (Exception ex)
            { ex.ToDummy(); }

            value.SelectedIndex = fi;
        }

        #region thread safety

        public static void SetText(this Control ctrl, string value)
        { GlobalControlHandler.ControlSetText(ctrl, value); }

        public static string GetText(this Control ctrl)
        {
            string ret = null;

            ret = GlobalControlHandler.ControlGetText(ctrl);

            return ret;
        }

        public static void Check(this Control ctrl, bool value)
        { GlobalControlHandler.ControlCheck(ctrl, value); }

        public static bool IsChecked(this Control ctrl)
        {
            bool ret = false;

            ret = GlobalControlHandler.ControlIsChecked(ctrl);

            return ret;
        }

        public static void SetValue(this Control ctrl, decimal value)
        { GlobalControlHandler.ControlSetValue(ctrl, value); }

        public static decimal GetValue(this Control ctrl)
        {
            decimal ret = 0;

            ret = GlobalControlHandler.ControlGetValue(ctrl);

            return ret;
        }

        public static object GetTag(this Control ctrl)
        { return GlobalControlHandler.ControlGetTag(ctrl); }

        public static void SetTag(this Control ctrl, object tag)
        { GlobalControlHandler.ControlSetTag(ctrl, tag); }

        public static void ClearItems(this Control ctrl)
        { GlobalControlHandler.ControlClearItems(ctrl); }

        public static void AddItemRange(this Control ctrl, object[] values)
        { GlobalControlHandler.ControlAddItemRange(ctrl, values);  }

        public static void AddItem(this Control ctrl, object value)
        { GlobalControlHandler.ControlAddItem(ctrl, value); }

        public static void SortItems(this Control ctrl, bool sort)
        { GlobalControlHandler.ControlSortItems(ctrl, sort); }

        public static void SetVisibility(this Control ctrl, bool visible)
        { GlobalControlHandler.ControlSetVisibility(ctrl, visible); }

        public static void SetState(this ToolStripMenuItem menu, bool enabled)
        { GlobalControlHandler.MenuSetState(menu, enabled); }

        public static void SetCheckedState(this ToolStripMenuItem menu, bool check)
        { GlobalControlHandler.MenuSetCheckedState(menu, check); }

        public static void SetText(ToolStripTextBox item, string value)
        { GlobalControlHandler.MenuSetText(item, value); }

        public static void SetIndex(this Control ctrl, int index)
        { GlobalControlHandler.ControlSetIndex(ctrl, index); }

        public static void SetLines(this TextBox ctrl, string[] value)
        { GlobalControlHandler.UpdateTextBox(ctrl, value.ToList()); }

        public static void SetLines(this TextBox ctrl, List<string> value)
        { GlobalControlHandler.UpdateTextBox(ctrl, value); }

        public static List<string> GetLines(this Control ctrl)
        {
            List<string> ret = new List<string> { };
            
            ret = GlobalControlHandler.GetLinesFromTextBox(ctrl);

            return ret;
        }

        public static ListView.ColumnHeaderCollection GetColumns(this ListView ctrl)
        { return GlobalControlHandler.ListViewGetColumns(ctrl); }

        public static void SetColumnWidth(this ListView ctrl, 
                                          int col, 
                                          ColumnHeaderAutoResizeStyle autoSet = ColumnHeaderAutoResizeStyle.ColumnContent, 
                                          int width = -1)
        { GlobalControlHandler.ListViewSetColumnWidth(ctrl, col, autoSet, width); }

        public static object GetColumnTag(this ListView ctrl, int col)
        { return GlobalControlHandler.ListViewGetColumnTag(ctrl, col); }

        public static void SetColumnTag(this ListView ctrl, int col, object tag)
        { GlobalControlHandler.ListViewSetColumnTag(ctrl, col, tag); }

        public static List<ListViewItem> GetItems(this ListView ctrl)
        { return GlobalControlHandler.ListViewGetItems(ctrl); }

        public static bool RemoveSelectedItem(this ListView ctrl)
        { return GlobalControlHandler.ListViewRemoveSelectedItem(ctrl); }

        public static bool RemoveSelectedItems(this ListView ctrl)
        { return GlobalControlHandler.ListViewRemoveSelectedItems(ctrl); }

        public static void ClearItems(this ListView ctrl)
        { GlobalControlHandler.ListViewClearItems(ctrl); }

        public static void ResetSelectedItem(this ListView ctrl)
        { GlobalControlHandler.ListViewResetSelectedItem(ctrl); }

        public static bool AddItem(this ListView ctrl, ListViewItem item, bool unique)
        { return GlobalControlHandler.InsertListViewItem(ctrl, item, -1, unique); }
        
        public static bool AddItem(this ListView ctrl, ListViewItem item, int pos, bool unique)
        { return GlobalControlHandler.InsertListViewItem(ctrl, item, pos, unique); }

        public static bool AddItem(this ListView ctrl, object tag, params string[] subItems)
        { return GlobalControlHandler.ListViewInsertItemAt(ctrl, tag, -1, false, subItems); }

        public static bool AddItem(this ListView ctrl, object tag, bool unique, params string[] subItems)
        { return GlobalControlHandler.ListViewInsertItemAt(ctrl, tag, -1, unique, subItems); }

        public static bool InsertItemAt(this ListView ctrl, ListViewItem item, int pos, bool unique)
        { return GlobalControlHandler.InsertListViewItem(ctrl, item, pos, unique); }

        public static void InsertItemAt(this ListView ctrl, object tag, int pos, params string[] subItems)
        { GlobalControlHandler.ListViewInsertItemAt(ctrl, tag, pos, false, subItems); }

        public static bool InsertItemAt(this ListView ctrl, object tag, int pos, bool unique, params string[] subItems)
        { return GlobalControlHandler.ListViewInsertItemAt(ctrl, tag, pos, unique, subItems); }
        
        #endregion

        #endregion

        #region dictionary

        public static bool AddSafe<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value, bool force = false)
        {
            bool ret = false;

            try
            {
                if (!dict.ContainsKey(key))
                { dict.Add(key, value); ret = true; }

                else if (force)
                {
                    dict[key] = value;
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static TValue GetValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> value, TKey key)
        {
            TValue ret = default(TValue);

            if (value.ContainsKey(key))
            { ret = value[key]; }

            return ret;
        }

        public static bool RemoveSafe<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            bool ret = false;

            try
            {
                if (dict.ContainsKey(key))
                { dict.Remove(key); ret = true; }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static TValue GetValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, object defValue = null)
        {
            TValue ret = default(TValue);

            if (defValue != null)
            { ret = (TValue)defValue; }

            try
            {
                if (dict.ContainsKey(key))
                { ret = dict[key]; }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static Dictionary<TKey, TValue> Reset<TKey, TValue>(this Dictionary<TKey, TValue> dic)
        {
            dic.InvokeSafe("Clear");

            return dic;
        }

        public static Dictionary<TKey, TValue> OrderByKeySafe<TKey, TValue>(this Dictionary<TKey, TValue> dic, bool ascending = true)
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(dic);

            IOrderedEnumerable<KeyValuePair<TKey, TValue>> temp;

            try
            {
                if (ascending)
                { temp = ret.OrderBy(d => d.Key); }

                else
                { temp = ret.OrderByDescending(d => d.Key); } 

                dic.InvokeSafe("Clear");

                foreach (KeyValuePair<TKey, TValue> kvp in temp)
                { dic.Add(kvp.Key, kvp.Value); }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return dic;
        }

        public static Dictionary<TKey, TValue> OrderByValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> dic, bool ascending = true)
        {
            Dictionary<TKey, TValue> ret = dic;

            try
            {
                if (ascending)
                { ret = dic.OrderBy(d => d.Value).ToDictionary(k => k.Key, v => v.Value); }

                else
                { ret = dic.OrderByDescending(d => d.Value).ToDictionary(k => k.Key, v => v.Value); }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static Dictionary<TKey, TValue> OrderByKeyLengthSafe<TKey, TValue>(this Dictionary<TKey, TValue> dic, bool ascending = true)
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(dic);

            IOrderedEnumerable<KeyValuePair<TKey, TValue>> temp;

            try
            {
                if (ascending)
                { temp = ret.OrderBy(d => d.Key.ToString().Length); }

                else
                { temp = ret.OrderByDescending(d => d.Key.ToString().Length); }

                dic.InvokeSafe("Clear");

                foreach (KeyValuePair<TKey, TValue> kvp in temp)
                { dic.Add(kvp.Key, kvp.Value); }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return dic;
        }

        #endregion

        #region list

        public static void AddFormatted(this List<string> value, string item, params object[] args)
        {
            try
            { item = string.Format(item, args); }
            catch { }

            value.Add(item);
        }

        public static void DisposeSafe<TItem>(this List<TItem> value)
        {
            foreach (TItem item in value)
            { item.InvokeSafe("Dispsoe"); }

            value.Clear();
        }

        public static bool AddSafe<TItem>(this List<TItem> value, TItem item, bool force = false)
        {
            bool ret = false;

            try
            {
                if (!value.Contains(item))
                { value.Add(item); ret = true; }

                else if (force)
                {
                    value.Remove(item);

                    value.Add(item);
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static bool AddRangeSafe<TItem>(this List<TItem> value, List<TItem> itemList)
        {
            bool ret = false;

            foreach (TItem item in itemList)
            {
                if (!value.AddSafe(item))
                { ret = false; }
            }

            return ret;
        }

        public static List<TItem> OrderByItemLength<TItem>(this List<TItem> value, bool ascending = true)
        {
            List<TItem> ret = new List<TItem> { };

            if (ascending)
            { ret = value.OrderBy(i => i.ToString().Length).ToList(); }

            else
            { ret = value.OrderByDescending(i => i.ToString().Length).ToList(); }

            return ret;
        }

        public static List<TItem> OrderBy<TItem>(this List<TItem> value, bool ascending = true)
        {
            List<TItem> ret = new List<TItem> { };

            if (ascending)
            { ret = value.OrderBy(i => i).ToList(); }

            else
            { ret = value.OrderByDescending(i => i).ToList(); }

            return ret;
        }

        public static List<TItem> OrderByField<TItem>(this List<TItem> value, string fieldName, bool ascending = true, int startFieldValueAt = 0)
        {
            List<TItem> ret = value;

            ret.Sort(
                delegate(TItem a, TItem b)
                {
                    int retval = 1;

                    try
                    {
                        PropertyInfo pia = a.GetType().GetProperty(fieldName);

                        if (pia == null) retval = -1;

                        PropertyInfo pib = b.GetType().GetProperty(fieldName);

                        if (pib == null)
                        {
                            retval = (retval == 1) ? -1 : 0;

                            return retval;
                        }

                        object vala = pia.GetValue(a, null);

                        if (vala == null) retval = -1;

                        object valb = pib.GetValue(b, null);

                        if (valb == null)
                        {
                            retval = (retval == 1) ? -1 : 0;

                            return retval;
                        }

                        double dvala;

                        if (vala.IsNumeric(out dvala))
                        {
                            double dvalb;

                            valb.IsNumeric(out dvalb);

                            retval = dvala.CompareTo(dvalb);
                        }

                        else
                        {
                            retval = vala.ToString().Substring(startFieldValueAt).CompareTo(valb.ToString().Substring(startFieldValueAt));
                        }
                    }

                    catch (Exception ex)
                    { ex.ToDummy(); }

                    return retval;
                });

            if (!ascending)
            { ret.Reverse(); }

            return ret;
        }

        public static TItem GetItemSafe<TItem>(this List<TItem> value, int position)
        {
            TItem ret = default(TItem);

            if (value.Count > position)
            { ret = value[position]; }

            return ret;
        }

        public static List<TItem> RemoveSafe<TItem>(this List<TItem> value, TItem item)
        {
            if (value.Contains(item))
            { value.Remove(item); }

            return value;
        }

        public static List<TItem> RemoveRangeSafe<TItem>(this List<TItem> value, List<TItem> range)
        {
            foreach (TItem item in range)
            { value.RemoveSafe(item); }

            return value;
        }

        public static List<TItem> Reset<TItem>(this List<TItem> value)
        {
            value.InvokeSafe("Clear");

            return new List<TItem> { };
        }

        public static List<TItem> SortByField<TItem>(this List<TItem> value, string fieldName)
        {
            List<TItem> ret = value;

            ret.Sort(
                delegate(TItem a, TItem b)
                {
                    int retval = 1;

                    try
                    {
                        PropertyInfo pia = a.GetType().GetProperty(fieldName);

                        if (pia == null) retval = -1;

                        PropertyInfo pib = b.GetType().GetProperty(fieldName);

                        if (pib == null)
                        {
                            retval = (retval == 1) ? -1 : 0;

                            return retval;
                        }

                        object vala = pia.GetValue(a, null);

                        if (vala == null) retval = -1;

                        object valb = pib.GetValue(b, null);

                        if (valb == null)
                        {
                            retval = (retval == 1) ? -1 : 0;

                            return retval;
                        }

                        double dvala;

                        if (vala.IsNumeric(out dvala))
                        {
                            double dvalb;

                            valb.IsNumeric(out dvalb);

                            retval = dvala.CompareTo(dvalb);
                        }

                        else
                        {
                            retval = vala.ToString().CompareTo(valb);
                        }
                    }

                    catch (Exception ex)
                    { ex.ToDummy(); }

                    return retval;
                });

            return ret;
        }

        public static string GetMatchingItem(this List<string> value, string match)
        {
            string ret = String.Empty;

            //List<string> temp = value.AsParallel().Where(x => x.StartsWith(match, StringComparison.InvariantCultureIgnoreCase)).ToList();
            List<string> temp = value.Where(x => x.StartsWith(match, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if ((temp!= null) && (temp.Count > 0))
            { ret = temp[0]; }

            return ret;
        }

        public static T GetItemByField<T>(this List<T> value, string fieldName, object fieldValue)
        {
            T ret = default(T);

            PropertyInfo pi = ret.GetType().GetProperty(fieldName);

            if (pi != null)
            {
                foreach (T item in value)
                {
                    if (pi.GetValue(item, null) == fieldValue)
                    {
                        ret = item;

                        break;
                    }
                }
            }

            return ret;
        }

        #endregion

        #region DirectoryAttribute
       
        public static DirectoryAttribute GetStringAttributeSafe(this SearchResultEntry value, string attributeName, out string stringValue)
        {
            DirectoryAttribute ret = null;

            stringValue = String.Empty;

            if (value.Attributes.Contains(attributeName))
            {
                ret = value.Attributes[attributeName];

                foreach (string val in ret.GetValues(typeof(string)))
                {
                    stringValue = val;

                    break;
                }
            }

            return ret;
        }

        public static DirectoryAttribute GetStringAttributeSafe(this SearchResultEntry value, string attributeName, out List<string> stringValues)
        {
            DirectoryAttribute ret = null;

            stringValues = new List<string> { };

            if (value.Attributes.Contains(attributeName))
            {
                ret = value.Attributes[attributeName];

                foreach (string val in ret.GetValues(typeof(string)))
                { stringValues.AddSafe(val); }
            }

            return ret;
        }

        public static DirectoryAttribute GetAttributeSafe<T>(this SearchResultEntry value, string attributeName, out List<T> objectValues)
        {
            DirectoryAttribute ret = null;

            objectValues = new List<T> { };

            if (value.Attributes.Contains(attributeName))
            {
                ret = value.Attributes[attributeName];

                foreach (T val in ret.GetValues(typeof(T)))
                { objectValues.AddSafe(val); }
            }

            return ret;
        }
    
        #endregion

        #region objects

        public static string GetMD5Hash(this object value)
        {
            string ret = "";

            if (value == null) return ret;

            ret = value.ToString();

            string hash = (string)value.InvokeSafe("ToMD5Hash");

            if (hash != null)
            { ret = hash; }

            return ret;
        }

        public static T MakeCopy<T>(this T value) where T:new()
        {
            T ret = new T();

            foreach (MemberInfo member in value.GetType().GetMembers())
            {
                if (member.MemberType == MemberTypes.Field)
                {
                    FieldInfo fi = value.GetType().GetField(member.Name);

                    object val = fi.GetValue(value);

                    fi.SetValue(ret, val);
                }

                else if (member.MemberType == MemberTypes.Property)
                {
                    PropertyInfo pi = value.GetType().GetProperty(member.Name);

                    if (pi.CanWrite)
                    {
                        object val = pi.GetValue(value, null);

                        pi.SetValue(ret, val, null);
                    }
                }
            }

            return ret;
        }

        public static object GetInternalPropertyValue(this object value, string propertyName)
        {
            object ret = null;

            try
            {
                PropertyInfo pi = value.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);

                if (pi != null)
                { ret = pi.GetValue(value, null); }
            }

            catch { }


            return ret;
        }

        public static object GetPropertyValue(this object value, string propertyName)
        {
            object ret = null;

            try
            {
                PropertyInfo pi = value.GetType().GetProperty(propertyName);

                if (pi != null)
                { ret = pi.GetValue(value, null); }
            }

            catch { }


            return ret;
        }

        public static List<string> GetPropertyNames(this object value, bool includeInternal = false)
        {
            List<string> ret = new List<string> { };

            try
            {
                BindingFlags bf = (includeInternal) ? BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic : BindingFlags.Public;
                PropertyInfo[] pilist = value.GetType().GetProperties(bf);

                ret = pilist.Select(p => p.Name).ToList();

                //foreach (PropertyInfo pi in pilist)
                //{ ret.Add(pi.Name); }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public static void SetPropertyValue(this object value, string propertyName, object propertyValue)
        {
            try
            {
                PropertyInfo pi = value.GetType().GetProperty(propertyName);

                if (pi != null)
                { pi.SetValue(value, propertyValue, null); }
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public static string SerializeThis<T>(this T value)
        {
            string ret = string.Empty;

            try
            {
                XmlSerializer ser = new XmlSerializer(value.GetType());

                using (StringWriter writer = new StringWriter())
                {
                    ser.Serialize(writer, value);

                    ret = writer.ToString();
                }
            }

            catch { }

            return ret;
        }

        public static bool SerializeThis<T>(this T value, string filePath, bool append = false)
        {
            bool ret = false;

            string data = string.Empty;

            try
            {
                XmlSerializer ser = new XmlSerializer(value.GetType());

                using (StringWriter writer = new StringWriter())
                {
                    ser.Serialize(writer, value);

                    data = writer.ToString();

                    data.WriteToFile(filePath, append);
                }

                ret = true;
            }

            catch { }

            return ret;
        }

        public static bool DeSerializeFromString<T>(this string value, out T classObject) where T :new()
        {
            bool ret = false;

            classObject = new T();

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));

                using (StringReader reader = new StringReader(value))
                {  
                    try
                    { 
                        classObject = (T)ser.Deserialize(reader);

                        ret = true;
                    }

                    catch { }
                }
            }
            
            catch { }

            return ret;
        }

        public static bool DeSerializeFromFile<T>(this string value, out T classObject) where T : new()
        {
            bool ret = false;

            classObject = new T();

            try
            {
                string data;

                if (value.ReadFromFile(out data))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));

                    using (StringReader reader = new StringReader(data))
                    {
                        try
                        {
                            classObject = (T)ser.Deserialize(reader);

                            ret = true;
                        }

                        catch { }
                    }
                }
            }

            catch { }

            return ret;
        }

        #endregion

        #region Exception extensions

        public static void ToDummy(this Exception ex)
        { }

        #endregion

        #region Helping extensions

        public static bool TryParseType(this string val, out Type retType)
        {
            bool ret = false;

            retType = null;

            try
            { 
                retType = Type.GetType(val);

                ret = true;
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        #endregion

        #region  WindowsIdentity

        public static bool IsElevated(this WindowsIdentity value)
        {
            bool ret = false;

            if (value != null)
            {
                try
                {
                    //WindowsPrincipal princ = new WindowsPrincipal(value);

                    ret = value.Groups.Contains(new SecurityIdentifier("S-1-5-32-544"));

                    //ret = princ.IsInRole(WindowsBuiltInRole.Administrator);
                }

                catch (Exception) { }
            }

            return ret;
        }

        #endregion

        #region SecurityIdentifier

        public static bool IsWellKnownSid(this string val, int breakHere)
        {
            bool ret = false;

            SecurityIdentifier sid = new SecurityIdentifier(val);

            ret = sid.IsWellKnownSid(breakHere);

            return ret;
        }

        public static bool IsWellKnownSid(this SecurityIdentifier val, int breakHere)
        {
            bool ret = false;

            foreach (WellKnownSidType stype in Enum.GetValues(typeof(WellKnownSidType)))
            {
                if ((int)stype >= breakHere)
                { break; }

                if (val.IsWellKnown(stype))
                {
                    ret = true;

                    break;
                }
            }

            return ret;
        }

        #endregion

        #region EventValue

        public static EventValueTypes ValueTypeFromType<T>(this EventValue val)
        {
            EventValueTypes ret = EventValueTypes.Object;

            Dictionary<Type, EventValueTypes> temp = new Dictionary<Type, EventValueTypes> { };

            temp.Add(typeof(List<string>), EventValueTypes.StringList);
            temp.Add(typeof(List<object>), EventValueTypes.ObjectList);
            temp.Add(typeof(string), EventValueTypes.String);
            temp.Add(typeof(bool), EventValueTypes.Bool);
            temp.Add(typeof(int), EventValueTypes.Int);
            temp.Add(typeof(long), EventValueTypes.Long);
            temp.Add(typeof(object), EventValueTypes.Object);

            if (temp.ContainsKey(typeof(T)))
            { ret = temp[typeof(T)]; }

            return ret;
        }

        #endregion
    }
}
