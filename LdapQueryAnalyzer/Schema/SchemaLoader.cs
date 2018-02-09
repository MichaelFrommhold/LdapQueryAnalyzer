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
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SchemaLoader : Decoder
    {
        #region fields
        // see ADBase
        #endregion

        #region constructor

        public SchemaLoader()
            : base()
        { }

        #endregion

        #region methods

        protected void GetAggregateSchema(string dcName)
        {
            DateTime modified = DateTime.MinValue;

            string[] attribs = new string[] { "modifyTimeStamp", "schemaInfo" };

            List<SearchResultEntry> schemainfo = Query(dcName, ForestBase.SchemaNamingContext,
                                                    "(objectClass=*)", attribs, SearchScope.Base,
                                                    ReferralChasingOptions.None,
                                                    new QueryControl() { CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_SCHEMA },
                                                    returnResults: true);

            Int32 updatever = 0;

            if (base.HasError)
            { base.SetError("Get Schema: " + base.ErrorMSG); }

            else
            {
                if (schemainfo[0].Attributes.Contains("schemaInfo"))
                {
                    DirectoryAttribute dainfo = schemainfo[0].Attributes["schemaInfo"];

                    DecodeSchemaInfo(dainfo, out updatever);
                }                

                //DirectoryAttribute dastamp = schemainfo[0].Attributes["modifyTimeStamp"];

                //bool temp = false;

                //string plaind = null;

                //DecodeInt64Data(dastamp, ActiveDirectorySyntax.GeneralizedTime, out temp, out plaind);

                //DateTime.TryParse(plaind, out modified);
            }

            LoadCacheFromXML(ForestBase.ForestName, updatever);

            if (base.MustLoadSchemaFromAD)
            {
                GlobalEventHandler.RaiseLoadingSchema();

                string[] attributes = new string[] { "attributeSecurityGUID", "schemaIDGUID", "lDAPDisplayName", 
                                                     "attributeSyntax", "oMSyntax", "isDefunct", "systemFlags", "linkID" };

                List<SearchResultEntry> attributeSchema = Query(dcName, ForestBase.SchemaNamingContext,
                                                    "(objectClass=attributeSchema)", attributes, SearchScope.Subtree,
                                                    ReferralChasingOptions.None,
                                                    new QueryControl() { AutoPage = true, CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_SCHEMA },
                                                    returnResults: true);

                attributes = new string[] { "systemMustContain", "systemMayContain", "systemAuxiliaryClass", 
                                            "subClassOf", "mustContain", "mayContain", "lDAPDisplayName", 
                                            "auxiliaryClass", "isDefunct", "schemaIDGUID" };

                List<SearchResultEntry> classschema = Query(dcName, ForestBase.SchemaNamingContext,
                                                    "(objectClass=classSchema)", attributes, SearchScope.Subtree,
                                                    ReferralChasingOptions.None,
                                                    new QueryControl() { AutoPage = true, CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_SCHEMA },
                                                    returnResults: true);

                string[] attribs1 = new string[] { "displayName", "rightsGuid" };

                List<SearchResultEntry> extendedrights = Query(dcName, ForestBase.ConfigurationNamingContext,
                                                    "(objectClass=controlAccessRight)", attribs1, SearchScope.Subtree,
                                                    ReferralChasingOptions.None,
                                                    new QueryControl() { AutoPage = true, CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_SCHEMA },
                                                    returnResults: true);

                DownloadSchemaCache(attributeSchema, classschema, extendedrights, updatever);
                //DownloadSchemaCache(attributeSchema, classschema, extendedrights, modified);
            }

            LoadSchemaFomCache();

            ForestBase.SchemaLoaded = true;

            ForestBase.AttributesLoaded = true;
        }

        #endregion
    }
}
