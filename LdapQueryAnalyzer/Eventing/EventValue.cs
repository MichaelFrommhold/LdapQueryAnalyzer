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
    public class EventValue
    {
        #region fields

        public EventValueTypes DefaultValueType { get; private set; }

        public Type DefaultType { get; private set; }

        public EventValueTypes ValueTypes { get; private set; }

        public List<object> ObjectValues { get; private set; }

        public List<string> StringValues { get; private set; }

        public object ObjectValue { get; private set; }

        public string StringValue { get; private set; }

        public bool BoolValue { get; private set; }

        public int IntValue { get; private set; }

        public long LongValue { get; private set; }

        #endregion

        #region constructors

        public EventValue()
        { LoadInternal(); }

        #endregion

        #region methods

        private void LoadInternal()
        {
            this.ObjectValues = new List<object> { };
            this.StringValues = new List<string> { };
        }

        public void SetDefaultValue<T>(T value)
        {
            this.DefaultValueType = this.ValueTypeFromType<T>();

            this.SetTypedValue<T>(value);
        }

        public void SetTypedValue<T>(T value)
        {
            EventValueTypes ttype = this.ValueTypeFromType<T>();

            switch (ttype)
            {
                case EventValueTypes.ObjectList:

                    this.ObjectValues.Add(value);
                    break;

                case EventValueTypes.StringList:

                    this.StringValues.Add(value.ToString());
                    break;

                case EventValueTypes.Object:

                    this.ObjectValue = value;
                    break;

                case EventValueTypes.String:

                    this.StringValue = value.ToString();
                    break;

                case EventValueTypes.Bool:

                    bool btemp = false;
                    Boolean.TryParse(value.ToString(), out btemp);

                    this.BoolValue = btemp;
                    break;

                case EventValueTypes.Int:

                    int itemp = 0;
                    int.TryParse(value.ToString(), out itemp);

                    this.IntValue = itemp;
                    break;

                case EventValueTypes.Long:

                    long ltemp = 0;
                    long.TryParse(value.ToString(), out ltemp);

                    this.LongValue = ltemp;
                    break;
            }
        }

        public dynamic GetTypedValue<T>()
        {
            dynamic ret = default(T);

            EventValueTypes ttype = this.ValueTypeFromType<T>();

            switch (ttype)
            {
                case EventValueTypes.ObjectList:

                    ret = this.ObjectValues;
                    break;

                case EventValueTypes.StringList:

                    ret = this.StringValues;
                    break;

                case EventValueTypes.Object:

                    ret = this.ObjectValue;
                    break;

                case EventValueTypes.String:

                    ret = this.StringValue;
                    break;

                case EventValueTypes.Bool:

                    ret = this.BoolValue;
                    break;

                case EventValueTypes.Int:

                    ret = this.IntValue;
                    break;

                case EventValueTypes.Long:

                    ret = this.LongValue;
                    break;
            }

            return ret;
        }

        public override string ToString()
        {
            string ret = String.Empty;

            short hasdata = 0;

            if ((this.ValueTypes & EventValueTypes.ObjectList) == EventValueTypes.ObjectList)
            {
                ret += String.Join("|", this.ObjectValues.Select(val => val.ToString()));

                hasdata++;
            }

            if ((this.ValueTypes & EventValueTypes.ObjectList) == EventValueTypes.ObjectList)
            {
                ret = (hasdata > 0) ? ret + " ++ " : ret;

                ret += String.Join("|", this.StringValues);

                hasdata++;
            }

            if ((this.ValueTypes & EventValueTypes.Object) == EventValueTypes.Object)
            {
                ret = (hasdata > 0) ? ret + " ++ " : ret;

                ret += this.ObjectValue.ToString();

                hasdata++;
            }

            if ((this.ValueTypes & EventValueTypes.String) == EventValueTypes.String)
            {
                ret = (hasdata > 0) ? ret + " ++ " : ret;

                ret += this.StringValue;

                hasdata++;
            }

            if ((this.ValueTypes & EventValueTypes.Bool) == EventValueTypes.Bool)
            {
                ret = (hasdata > 0) ? ret + " ++ " : ret;

                ret += this.BoolValue.ToString();

                hasdata++;
            }

            if ((this.ValueTypes & EventValueTypes.Int) == EventValueTypes.Int)
            {
                ret = (hasdata > 0) ? ret + " ++ " : ret;

                ret += this.IntValue.ToString();

                hasdata++;
            }

            if ((this.ValueTypes & EventValueTypes.Long) == EventValueTypes.Long)
            {
                ret = (hasdata > 0) ? ret + " ++ " : ret;

                ret += this.LongValue.ToString();

                hasdata++;
            }

            return ret;
        }

        #endregion
    }
}
