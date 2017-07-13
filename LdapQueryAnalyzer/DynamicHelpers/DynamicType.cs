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

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DynamicType
    {
        #region fields

        public string Name;

        public Type SystemType;

        public string InvokeMethod;

        public Type[] InvokeArgumentTypes { get { return InnerInvokeArgumentTypes.ToArray(); } }

        public object[] InvokeArguments { get { return InnerInvokeArguments.ToArray(); } }

        private List<Type> InnerInvokeArgumentTypes = new List<Type>();

        private List<object> InnerInvokeArguments = new List<object>();

        #endregion

        #region methods

        public bool SetType(string typeName)
        {
            bool ret = false;

            Name = typeName;

            try
            {
                SystemType = Type.GetType(Name);

                ret = true;
            }

            catch (Exception ex)
            { ex.ToDummy(); }


            return ret;
        }

        public void AddInvokeArgumentType(string typeName, bool makeRefType = false)
        {            
            Type argumentType = null;

            try
            {
                argumentType = Type.GetType(typeName);

                if (makeRefType)
                { argumentType = argumentType.MakeByRefType(); }

                InnerInvokeArgumentTypes.Add(argumentType); 
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        public void AddInvokeArgument(string typeName, string value)
        {
            object argument = null;



            InnerInvokeArguments.Add(argument); 
        }

        #endregion
    }
}
