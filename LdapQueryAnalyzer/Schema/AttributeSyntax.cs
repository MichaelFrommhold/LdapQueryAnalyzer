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

using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class AttributeSyntax : ADBase
    {
        #region fields
        //see ADFields
        #endregion

        #region constructor

        internal AttributeSyntax()
            : base()
        {
            AggregateSyntaxDecoder();

            LDAPSyntaxDecoder();

        }

        #endregion

        #region methods

        public ActiveDirectorySyntax DecodeLDAPAttributeSyntax(string attributeSyntax, int omSyntax)
        {
            ActiveDirectorySyntax ret = ActiveDirectorySyntax.CaseIgnoreString;

            if (ForestBase.LDAPSyntaxes.ContainsKey(attributeSyntax))
            {
                if (ForestBase.LDAPSyntaxes[attributeSyntax].ContainsKey(omSyntax))
                { ret = ForestBase.LDAPSyntaxes[attributeSyntax][omSyntax]; }
            }

            return ret;
        }

        private void LDAPSyntaxDecoder()
        {
            ForestBase.LDAPSyntaxes = new Dictionary<string, Dictionary<int, ActiveDirectorySyntax>> { };

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.1", new Dictionary<int, ActiveDirectorySyntax>() { { 127, ActiveDirectorySyntax.DN } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.2", new Dictionary<int, ActiveDirectorySyntax>() { { 6, ActiveDirectorySyntax.Oid } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.3", new Dictionary<int, ActiveDirectorySyntax>() { { 27, ActiveDirectorySyntax.CaseExactString } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.4", new Dictionary<int, ActiveDirectorySyntax>() { { 20, ActiveDirectorySyntax.CaseIgnoreString } });

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.5", new Dictionary<int, ActiveDirectorySyntax>() { { 19, ActiveDirectorySyntax.PrintableString } });
            ForestBase.LDAPSyntaxes["2.5.5.5"].AddSafe(22, ActiveDirectorySyntax.IA5String);

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.6", new Dictionary<int, ActiveDirectorySyntax>() { { 18, ActiveDirectorySyntax.NumericString } });

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.7", new Dictionary<int, ActiveDirectorySyntax>() { { 127, ActiveDirectorySyntax.DNWithBinary } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.8", new Dictionary<int, ActiveDirectorySyntax>() { { 1, ActiveDirectorySyntax.Bool } });

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.9", new Dictionary<int, ActiveDirectorySyntax>() { { 2, ActiveDirectorySyntax.Int } });
            ForestBase.LDAPSyntaxes["2.5.5.9"].AddSafe(10, ActiveDirectorySyntax.Enumeration);

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.10", new Dictionary<int, ActiveDirectorySyntax>() { { 4, ActiveDirectorySyntax.OctetString } });
            ForestBase.LDAPSyntaxes["2.5.5.10"].AddSafe(127, ActiveDirectorySyntax.ReplicaLink);

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.11", new Dictionary<int, ActiveDirectorySyntax>() { { 23, ActiveDirectorySyntax.UtcTime } });
            ForestBase.LDAPSyntaxes["2.5.5.11"].AddSafe(24, ActiveDirectorySyntax.GeneralizedTime);

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.12", new Dictionary<int, ActiveDirectorySyntax>() { { 64, ActiveDirectorySyntax.DirectoryString } });

            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.13", new Dictionary<int, ActiveDirectorySyntax>() { { 127, ActiveDirectorySyntax.PresentationAddress } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.14", new Dictionary<int, ActiveDirectorySyntax>() { { 127, ActiveDirectorySyntax.DNWithString } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.15", new Dictionary<int, ActiveDirectorySyntax>() { { 66, ActiveDirectorySyntax.SecurityDescriptor } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.16", new Dictionary<int, ActiveDirectorySyntax>() { { 65, ActiveDirectorySyntax.Int64 } });
            ForestBase.LDAPSyntaxes.AddSafe("2.5.5.17", new Dictionary<int, ActiveDirectorySyntax>() { { 4, ActiveDirectorySyntax.Sid } });
        }

        private void AggregateSyntaxDecoder()
        {
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.7", ActiveDirectorySyntax.Bool);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.27", ActiveDirectorySyntax.Int);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.906", ActiveDirectorySyntax.Int64);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.2", ActiveDirectorySyntax.AccessPointDN);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.903", ActiveDirectorySyntax.DNWithBinary);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.904", ActiveDirectorySyntax.DNWithString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.12", ActiveDirectorySyntax.DN);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.1221", ActiveDirectorySyntax.ORName);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.43", ActiveDirectorySyntax.PresentationAddress);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.1362", ActiveDirectorySyntax.CaseExactString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.24", ActiveDirectorySyntax.GeneralizedTime);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.26", ActiveDirectorySyntax.IA5String);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.907", ActiveDirectorySyntax.SecurityDescriptor);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.36", ActiveDirectorySyntax.NumericString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.38", ActiveDirectorySyntax.Oid);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.40", ActiveDirectorySyntax.OctetString);
            ForestBase.AggregateSyntaxes.AddSafe("OctetString", ActiveDirectorySyntax.OctetString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.44", ActiveDirectorySyntax.PrintableString);
            ForestBase.AggregateSyntaxes.AddSafe("1.2.840.113556.1.4.905", ActiveDirectorySyntax.CaseIgnoreString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.15", ActiveDirectorySyntax.DirectoryString);
            ForestBase.AggregateSyntaxes.AddSafe("1.3.6.1.4.1.1466.115.121.1.53", ActiveDirectorySyntax.UtcTime);

            #region "decoding info"
            /*
                Boolean				 			1.3.6.1.4.1.1466.115.121.1.7
                Enumeration			 			1.3.6.1.4.1.1466.115.121.1.27
                Integer				 			1.3.6.1.4.1.1466.115.121.1.27
                LargeInteger		 			1.2.840.113556.1.4.906
                Object(Access-Point) 			1.3.6.1.4.1.1466.115.121.1.2
                Object(DN-Binary)				1.2.840.113556.1.4.903
                Object(DN-String)				1.2.840.113556.1.4.904
                Object(DS-DN)					1.3.6.1.4.1.1466.115.121.1.12
                Object(OR-Name)					1.2.840.113556.1.4.1221
                Object(Presentation-Address)	1.3.6.1.4.1.1466.115.121.1.43
                Object(Replica-Link)			OctetString
                String(Case)					1.2.840.113556.1.4.1362
                String(Generalized-Time)		1.3.6.1.4.1.1466.115.121.1.24
                String(IA5)						1.3.6.1.4.1.1466.115.121.1.26
                String(NT-Sec-Desc)				1.2.840.113556.1.4.907
                String(Numeric)					1.3.6.1.4.1.1466.115.121.1.36
                String(Object-Identifier)		1.3.6.1.4.1.1466.115.121.1.38
                String(Octet)					1.3.6.1.4.1.1466.115.121.1.40
                String(Printable)				1.3.6.1.4.1.1466.115.121.1.44
                String(Sid)						1.3.6.1.4.1.1466.115.121.1.40
                String(Teletex)					1.2.840.113556.1.4.905
                String(Unicode)					1.3.6.1.4.1.1466.115.121.1.15
                String(UTC-Time)				1.3.6.1.4.1.1466.115.121.1.53
             */
            #endregion
        }

        #endregion
    }
}
