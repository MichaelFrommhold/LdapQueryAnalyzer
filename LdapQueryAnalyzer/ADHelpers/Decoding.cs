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
using System.Text;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class Decoder : ADHelper
    {
        #region fields

        #endregion

        #region constructor

        internal Decoder() : base()
        { }

        #endregion

        #region methods

        public void CheckCustomAttributes(ref string[] attributes)
        {
            if (attributes == null)
            { return; }

            string allattribs = String.Join("|", attributes).ToLowerInvariant();

            if ((allattribs.Contains("userparameters")) && (!allattribs.Contains("samaccountname")))
            {
                List<string> temp = new List<string>(attributes);

                temp.Add("sAMAccountName");

                attributes = temp.ToArray();
            }

            if ((allattribs.Contains("memberof")) && (!allattribs.Contains("primarygroupid")) && MainBase.UserSettings.DecodePrimaryGroupID)
            {
                List<string> temp = new List<string>(attributes);

                temp.Add("primarygroupid");

                attributes = temp.ToArray();
            }
        }

        public List<string> TryCustomDecoding(DirectoryAttribute attrib, SearchResultEntry entry, ref bool match, ref bool exclusive, ActiveDirectorySyntax aSyntax = ActiveDirectorySyntax.CaseIgnoreString)
        {
            exclusive = false;
            
            match = true;

            List<string> ret = new List<string>();

            bool isbad = false;

            #region userparamters

            if (attrib.Name.ToLowerInvariant() == "userparameters")
            {
                if (MainBase.UserSettings.DecodeUserParameters)
                {
                    DirectoryAttribute desam = entry.Attributes["sAMAccountName"];

                    string sam = DecodeStringData(desam, ActiveDirectorySyntax.CaseIgnoreString, out isbad, true)[0];

                    DateTime start = DateTime.Now;

                    WtsApi userparams = new WtsApi(ForestBase.CurrentDC, sam);

                    GlobalEventHandler.RaiseSubsequentQuery((long)DateTime.Now.Subtract(start).TotalMilliseconds);

                    ret = userparams.Print();

                    if (!userparams.HasError)
                    { exclusive = true; }
                }
            }

            #endregion

            #region memberof

            else if (attrib.Name.ToLowerInvariant() == "memberof")
            {
                if (MainBase.UserSettings.DecodePrimaryGroupID)
                {
                    try
                    {                        
                        DirectoryAttribute deprid = entry.Attributes["primaryGroupID"];

                        string pgid = DecodePrimaryGroupId(deprid, true);

                        ret.Add(pgid.Replace("<", "<(primaryGroupID) "));
                    }

                    catch (Exception ex)
                    { ex.ToDummy(); }
                }
            }

            #endregion

            #region primarygroupid

            else if (attrib.Name.ToLowerInvariant() == "primarygroupid")
            {
                try
                {
                    DirectoryAttribute deprid = entry.Attributes["primaryGroupID"];

                    string pgid = DecodePrimaryGroupId(deprid, MainBase.UserSettings.DecodePrimaryGroupID);
                    
                    ret.Add(pgid);

                    exclusive = true;
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            #endregion

            #region logonhours

            else if (attrib.Name.ToLowerInvariant() == "logonhours")
            {
                try
                {
                    ret = DecodeLogonHours(attrib);

                    exclusive = true;
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            #endregion

            #region dSaSignature

            else if (attrib.Name.ToLowerInvariant() == "dsasignature")
            {
                try
                {
                    ret = DecodeDsaSignature(attrib);

                    exclusive = true;
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            #endregion

            #region tokenGroups  tokenGroupsGlobalAndUniversal tokenGroupsNoGCAcceptable

            else if ((attrib.Name.ToLowerInvariant() == "tokengroups") || (attrib.Name.ToLowerInvariant() == "tokengroupsglobalanduniversal") || (attrib.Name.ToLowerInvariant() == "tokengroupsglobalanduniversal"))
            {
                if (MainBase.UserSettings.ResolveSids)
                {
                    ret.AddRange(DecodeByteData(attrib, ActiveDirectorySyntax.Sid, out isbad, true));

                    exclusive = true;
                }
            }

            #endregion

            #region trust

            else if (attrib.Name.ToLowerInvariant() == "msds-trustforesttrustinfo")
            {
                foreach (byte[] value in attrib.GetValues(typeof(byte[])))
                {
                    ret.AddRange(new ForestTrustInfoDecoder(value).ToString());

                    exclusive = true;
                }
            }

            #endregion

            #region replication

            else if (attrib.Name.ToLowerInvariant() == "repluptodatevector")
            {
                foreach (byte[] value in attrib.GetValues(typeof(byte[])))
                {
                    ret.AddRange(new ReplUpToDateVector().Decode(value));

                    exclusive = true;
                }
            }
            
            else if (attrib.Name.ToLowerInvariant() == "replpropertymetadata")
            {
                //ActiveDirectoryReplicationMetadata metadata = CurrentDC
            }

            else if (aSyntax == ActiveDirectorySyntax.ReplicaLink)
            {
                
            }
            
            #endregion

            

            else
            { match = false; }

            return ret;
        }

        public List<string> TryDynamicTypeDecoding(DirectoryAttribute attrib, ref bool match)
        {
            match = false;

            List<string> ret = new List<string>();

            DynamicAttributeAssociator assoc = ForestBase.ADHelperDynamicDLL.AssociatorList.GetValueSafe<string, DynamicAttributeAssociator>(attrib.Name);
            
            if (assoc != null)
            {
                switch (assoc.AssociateFrom)
                {
                    case ASSOCIATE_FROM.ENUM:

                        ret = DynamicEnumDecoding(attrib, ref match);

                        break;

                    case ASSOCIATE_FROM.BERCONVERTER:

                        ret = DynamicBerDecoding(attrib, assoc, ref match);

                        break;

                    case ASSOCIATE_FROM.DICTIONARY:

                        ret = DynamicDictDecoding(attrib, assoc, ref match);

                        break;
                }
            }

            return ret;
        }

        public List<string> DynamicEnumDecoding(DirectoryAttribute attrib, ref bool match)
        {
            List<string> ret = new List<string>();

            Type entype = ForestBase.ADHelperDynamicDLL.GetEnumFromDynamicAssociator(attrib.Name);

            if (entype == null)
            { return ret; }

            foreach (string value in attrib.GetValues(typeof(string)))
            {                
                object val = null; 

                try
                {
                    long ltemp = 0;

                    long.TryParse(value, out ltemp);

                    bool success = ltemp.EnumTryParse(entype, out val);

                    if (success)
                    { ret.Add(string.Format("\t\t<{0}> ({1})", val.ToString(), value)); }

                    else
                    {
                        val = String.Join(", ", ltemp.EnumParse(entype).ToArray());

                        ret.Add(string.Format("\t\t<{0}> ({1})", val.ToString(), value));
                    }

                    match = true;
                }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    ret.Add(string.Format("\t\t<not decoded>: {0}", value.ToString()));
                }
            }

            return ret;
        }

        public List<string> DynamicBerDecoding(DirectoryAttribute attrib, DynamicAttributeAssociator assoc, ref bool match)
        {
            List<string> ret = new List<string>();

            DynamicBerConverter dynber = ForestBase.ADHelperDynamicDLL.BerConverterList.GetValueSafe<string, DynamicBerConverter>(assoc.BerConverterName);

            if (dynber != null)
            {
                List<string> fields = new List<string>();

                try
                {
                    foreach (byte[] value in attrib.GetValues(typeof(byte[])))
                    {
                        fields = dynber.DecodePrintable(value);

                        if (fields.Count != 0)
                        { ret.AddRange(fields); }

                        else
                        { ret.Add(string.Format("\t\t<not decoded>: {0}", ASCIIEncoding.Unicode.GetString(value))); }
                    }

                    match = true;
                }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    ret.Add(string.Format("\t\t<not decoded>: {0}", attrib[0].GetType().ToString()));
                }
            }

            return ret;
        }

        public List<string> DynamicDictDecoding(DirectoryAttribute attrib, DynamicAttributeAssociator assoc, ref bool match)
        {
            List<string> ret = new List<string>();

            Dictionary<string, string> dyndict = ForestBase.ADHelperDynamicDLL.DictionaryList.GetValueSafe<string, Dictionary<string, string>>(assoc.DictionaryName);

            if (dyndict != null)
            {
                try
                {
                    foreach (string value in attrib.GetValues(typeof(string)))
                    {
                        if (dyndict.ContainsKey(value))
                        { ret.Add(String.Format("\t\t<{0}> ({1})", dyndict[value], value)); }

                        else
                        { ret.Add(String.Format("\t\t<not decoded>: ({0})", value)); }
                    }
                    
                    match = true;
                }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    ret.Add(string.Format("\t\t<not decoded>: {0}", attrib[0].GetType().ToString()));
                }
            }

            return ret;
        }

        public List<string> DecodeSearchResults(List<SearchResultEntry> result, SearchRequestExtender infoStore)
        {
            List<string> ret = new List<string> { };

            foreach (SearchResultEntry sre in result)
            {                
                ret.Add(String.Format("DN: <{0}>", sre.DistinguishedName));

                if (sre.Attributes.Values.Count == 0)
                {
                    ret.Add("\t<no such attribute(s)>");
                }

                else
                {
                    if (infoStore.Attributes == null)
                    {
                        Array.Resize(ref infoStore.Attributes, sre.Attributes.AttributeNames.Count);

                        sre.Attributes.AttributeNames.CopyTo(infoStore.Attributes, 0);
                    }

                    foreach (string attribname in infoStore.Attributes)
                    {
                        List<string> tempret = new List<string> { };

                        bool isempty = false;

                        tempret.Add(string.Format("\t<{0}>:", attribname));

                        if (!(sre.Attributes.Contains(attribname)))
                        { 
                            tempret.Add("\t\t<not present>");

                            isempty = true;
                        }

                        else
                        {
                            DirectoryAttribute deAttrib = sre.Attributes[attribname];

                            if (deAttrib.Count == 0)
                            { 
                                tempret.Add("\t\t<not set>");

                                isempty = true;
                            }

                            else
                            {
                                bool isbad;

                                ActiveDirectorySyntax asyntax = GetAttributeSyntax(attribname, out isbad);

                                if (isbad)
                                { tempret.Add(string.Format("\t\t<UnKnown syntax> {0}", deAttrib.Name)); }

                                else
                                {
                                    switch (asyntax)
                                    {
                                        case ActiveDirectorySyntax.OctetString:
                                        case ActiveDirectorySyntax.Sid:

                                            tempret.AddRange(DecodeByteData(deAttrib, asyntax, out isbad));
                                            break;

                                        case ActiveDirectorySyntax.Int64:
                                        case ActiveDirectorySyntax.GeneralizedTime:
                                        case ActiveDirectorySyntax.UtcTime:

                                            string plaind = null;

                                            tempret.AddRange(DecodeInt64Data(deAttrib, asyntax, out isbad, out plaind));
                                            break;


                                        case ActiveDirectorySyntax.SecurityDescriptor:

                                            tempret.AddRange(DecodeSDData(deAttrib, asyntax));
                                            break;

                                        case ActiveDirectorySyntax.Int:

                                            tempret.AddRange(DecodeIntData(deAttrib, asyntax, out isbad));
                                            break;

                                        default:

                                            tempret.AddRange(DecodeStringData(deAttrib, asyntax, out isbad));
                                            break;
                                    }

                                    if (isbad)
                                    { MarkAttributeAsBad(ForestBase.ForestName, attribname); }
                                }
                            }
                        }

                        if ((!isempty) || (isempty && !MainBase.UserSettings.IgnoreEmpty))
                        {
                            ret.AddRange(tempret);
                        }
                    }
                }
            }

            if (ForestBase.SchemaCacheIsDirty)
            { SaveCache(); }

            return ret;
        }

        public List<string> DecodeByteData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad, bool resolveSid = false, bool rawdata = false)
        {
            List<String> ret = new List<string> { };
            isBad = false;

            foreach (byte[] value in attrib.GetValues(typeof(byte[])))
            {
                object val = null;

                try
                {
                    if (syntax == ActiveDirectorySyntax.Sid)
                    {
                        if (MainBase.UserSettings.DecodeSID)
                        { 
                            val = new SecurityIdentifier(value, 0);

                            if (MainBase.UserSettings.ResolveSids)
                            {
                                val = String.Format("{0} ({1})", DecodeSID((SecurityIdentifier)val, true), val.ToString());
                            }
                        }
                            
                        else
                        { val = string.Format("(must not decode) {0}", DecodeByteToHex(value)); }
                    }

                    else if (syntax == ActiveDirectorySyntax.OctetString)
                    { val = DecodeOctetString(value, MainBase.UserSettings.DecodeGUID, MainBase.UserSettings.DecodeOctetStrings, attrib.Name.ToLowerInvariant().Contains("guid")); }

                    else if (syntax == ActiveDirectorySyntax.ReplicaLink)
                    { val = DecodeOctetString(value, MainBase.UserSettings.DecodeGUID, MainBase.UserSettings.DecodeReplicaLinks, false); }
                        
                    else
                    { val = DecodeByteToHex(value); }

                    ret.Add((rawdata) ? val.ToString() : string.Format("\t\t<{0}>", val.ToString()));
                }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    isBad = true;

                    ret.Add(string.Format("\t\t<not decoded>: {0}", DecodeByteToHex(value)));
                }
            }

            return ret;
        }

        public List<string> DecodeStringData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad, bool rawdata = false)
        {
            List<String> ret = new List<string> { };
            isBad = false;

            foreach (string value in attrib.GetValues(typeof(string)))
            {
                try
                { ret.Add((rawdata) ? value.ToString() : string.Format("\t\t<{0}>", value.ToString())); }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    isBad = true;

                    ret.Add(string.Format("\t\t<not decoded>: {0}", attrib[0].GetType().ToString()));
                }
            }

            return ret;
        }

        public List<string> DecodeIntData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad)
        {
            List<String> ret = new List<string> { };
            isBad = false;

            foreach (string value in attrib.GetValues(typeof(string)))
            {
                try
                { ret.Add(string.Format("\t\t<{0}>", value.ToString())); }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    isBad = true;

                    ret.Add(string.Format("\t\t<not decoded>: {0}", value.ToString()));
                }
            }

            return ret;
        }

        public List<string> DecodeInt64Data(DirectoryAttribute attrib, ActiveDirectorySyntax syntax, out bool isBad, out string plainDataFirst)
        {
            List<String> ret = new List<string> { };
            isBad = false;
            plainDataFirst = null;

            string defaultdata = (new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToString();

            foreach (string value in attrib.GetValues(typeof(string)))
            {
                if (attrib.Name.ToLowerInvariant().StartsWith("usn"))
                { ret.Add(string.Format("\t\t<{0}>", value)); }

                else
                {
                    object val = null;

                    try
                    {
                        if (syntax == ActiveDirectorySyntax.Int64)
                        {
                            Int64 temp = 0;
                            Int64.TryParse(value, out temp);

                            if (temp < 0)
                            {
                                value.TryParsePositiveNumber(out temp);

                                TimeSpan intervall = new TimeSpan(temp);

                                double maxhit = (double)(intervall.Ticks / TimeSpan.MaxValue.Ticks);

                                maxhit = (maxhit < 0) ? -maxhit : maxhit;

                                if (maxhit == 1)
                                { val = "never"; }

                                else
                                { val = intervall.ToString(); }
                            }

                            else if (temp == 0)
                            { val = "none"; }

                            else if (temp >= DateTime.MaxValue.Ticks)
                            { val = "never"; }

                            else
                            {
                                try
                                { val = DateTime.FromFileTime(temp).ToUniversalTime(); }

                                catch (Exception ex)
                                { val = "never"; ex.ToDummy(); }
                            }
                        }

                        else if (syntax == ActiveDirectorySyntax.GeneralizedTime)
                        {
                            DateTime temp;

                            if (DateTime.TryParseExact(value, ForestBase.GENERALIZED_TIME_FORMAT, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, out temp))
                            { val = (temp < DateTime.FromFileTimeUtc(0)) ? DateTime.FromFileTimeUtc(0) : temp; }

                            else
                            { val = "not decoded"; }
                        }

                        else if (syntax == ActiveDirectorySyntax.UtcTime)
                        {
                            DateTime temp;

                            if (DateTime.TryParseExact(value, ForestBase.GENERALIZED_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out temp))
                            { val = (temp < DateTime.FromFileTimeUtc(0)) ? DateTime.FromFileTimeUtc(0) : temp; }

                            else
                            { val = "not decoded"; }
                        }

                        else
                        { val = "not decoded"; }

                        if (val.GetType() != typeof(string))
                        {
                            DateTime vtime = (DateTime)val;
                            vtime = vtime.Subtract(new TimeSpan(vtime.TimeOfDay.Ticks));

                            if (vtime.Ticks == (new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks))
                            { val = "never"; }

                            else
                            {
                                defaultdata = val.ToString();

                                val = String.Format("{0} (UTC)", val.ToString());
                            }
                        }
                    }

                    catch (Exception ex)
                    { val = "not decoded"; isBad = true; ex.ToDummy(); }

                    if (plainDataFirst == null)
                    { plainDataFirst = defaultdata; }

                    ret.Add(string.Format("\t\t<{0}> ({1})", val.ToString(), value));
                }
            }

            return ret;
        }

        public List<string> DecodeSDData(DirectoryAttribute attrib, ActiveDirectorySyntax syntax)
        {
            List<String> ret = new List<string> { };

            foreach (byte[] value in attrib.GetValues(typeof(byte[])))
            {
                try
                {
                    CommonSecurityDescriptor oCSD = new CommonSecurityDescriptor(true, true, value, 0);

                    if (!MainBase.UserSettings.DecodeSD)
                    { ret.Add(String.Format("\t\t(must not decode) SDDL: <{0}>", oCSD.GetSddlForm(AccessControlSections.All))); }

                    else
                    {
                        if (oCSD.Owner != null)
                        { ret.Add(String.Format("\t\tOwner: <{0}>", DecodeSID(oCSD.Owner, MainBase.UserSettings.ResolveSids))); }

                        if (oCSD.Group != null)
                        { ret.Add(String.Format("\t\tPrimary group: <{0}>", DecodeSID(oCSD.Group, MainBase.UserSettings.ResolveSids))); }

                        ret.Add(String.Format("\t\tControl flags: <{0}>", oCSD.ControlFlags.ToString())); 

                        ret.Add(String.Format("\t\tAccess Rules:"));

                        if (oCSD.DiscretionaryAcl != null)
                        { ret.AddRange(WalkACL(oCSD.DiscretionaryAcl, MainBase.UserSettings.ResolveSids)); }

                        try
                        {
                            ret.Add(String.Format("\t\t{0}Audit Rules:", Environment.NewLine));

                            if (oCSD.SystemAcl != null)
                            { ret.AddRange(WalkACL(oCSD.SystemAcl, MainBase.UserSettings.ResolveSids)); }
                        }

                        catch (Exception ex)
                        {
                            ex.ToDummy();
                        }
                    }
                }

                catch (Exception ex)
                {
                    ex.ToDummy();

                    ret.Add(string.Format("\t\t<not decoded>: {0}", attrib[0].GetType().ToString()));
                }
            }

            return ret;
        }

        public string DecodeSID(SecurityIdentifier trustee, bool translateSid = true)
        {
            string ret = null;

            if (!translateSid)
            { ret = trustee.ToString() + " (must not resolve)"; }

            else
            {
                try
                {
                    ret = ForestBase.SidCache.GetValueSafe<SecurityIdentifier, string>(trustee);

                    if (ret == null)
                    { ret = trustee.Translate(typeof(NTAccount)).Value; }
                }

                catch (Exception ex)
                {                    
                    ret = trustee.ToString() + " (could not resolve)"; ;

                    GlobalEventHandler.RaiseErrorOccured("ERROR resolving " + trustee.ToString());

                    ex.ToDummy();
                }

                ForestBase.SidCache.AddSafe<SecurityIdentifier, string>(trustee, ret);
            }

            return ret;
        }

        public string DecodeOctetString(byte[] value, bool mustDecodeGuid, bool mustDecode, bool supposeGuid = false)
        {
            string ret = "(must not decode)";

            bool returnhexstring = true;

            try
            {
                if ((supposeGuid) && (mustDecodeGuid))
                {
                    ret = new Guid(value).ToString();

                    returnhexstring = false;
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            if ((returnhexstring) && (mustDecode))
            { ret = DecodeByteToHex(value); }

            return ret;
        }

        public string DecodeByteToHex(byte[] value)
        {
            string ret = String.Empty;

            ret = BitConverter.ToString(value, 0);

            return ret;
        }

        public string DecodeAccessMask(int accessMask)
        {
            string ret = "0x" + accessMask.ToString("x8");

            try
            {
                AD_ACCESS_MASK admask = (AD_ACCESS_MASK)accessMask;

                ret = String.Format("{0} ({1})", admask.ToString(), ret);
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        public List<string> DecodeOIDs(ObjectAce ace)
        {
            List<string> ret = new List<string> { };

            string valA = null;
            string valB = null;

            if (ace.ObjectAceFlags == ObjectAceFlags.None)
            { return ret; }

            if ((ace.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.ObjectAceTypePresent)
            {
                valA = NameFromOID(ace.ObjectAceType);
            }

            if ((ace.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.InheritedObjectAceTypePresent)
            {
                valB = NameFromOID(ace.InheritedObjectAceType);
            }

            if (valA != null)
            { ret.Add(String.Format("\t\t\t    Objecttype: <{0}>", valA)); }

            if (valB != null)
            { ret.Add(String.Format("\t\t\t    InheritedObjectType: <{0}>", valB)); }

            return ret;
        }

        public string DecodePrimaryGroupId(DirectoryAttribute attrib, bool mustDecode)
        {
            string ret = String.Empty;

            bool isbad = false;

            if (mustDecode)
            {
                ret = DecodeStringData(attrib, ActiveDirectorySyntax.CaseIgnoreString, out isbad, true)[0];

                if (ForestBase.DomainSids.ContainsKey(ForestBase.CurrentDomain))
                {
                    string psid = ForestBase.DomainSids[ForestBase.CurrentDomain] + "-" + ret;

                    SearchRequestExtender srex = new SearchRequestExtender();

                    string[] attribs = new string[] { "distinguishedName" };

                    List<SearchResultEntry> results = Query(ForestBase.CurrentDC, "<SID=" + psid + ">", "(objectClass=*)", attribs, SearchScope.Base, ReferralChasingOptions.None, new QueryControl() { CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_DECODING }, returnResults:true);

                    DirectoryAttribute dadn = results[0].Attributes["distinguishedName"];

                    string dn = DecodeStringData(dadn, ActiveDirectorySyntax.DirectoryString, out isbad, true)[0];

                    ret = string.Format("\t\t<{0}: {1}>", ret, dn);
                }
            }

            else
            { ret = DecodeStringData(attrib, ActiveDirectorySyntax.CaseIgnoreString, out isbad)[0]; }

            return ret;
        }

        public List<string> DecodeLogonHours(DirectoryAttribute attrib)
        {
            List<string> ret = new List<string>() { "\t\tSun:\t", "\t\tMon:\t", "\t\tTue:\t", "\t\tWen:\t", "\t\tThu:\t", "\t\tFri:\t", "\t\tSat:\t" };

            List<char[]> bhours = new List<char[]>();

            char[] th;

            foreach (byte[] value in attrib.GetValues(typeof(byte[])))
            {
                for (int cnt = 1; cnt < value.Count(); cnt++)
                {
                    th = NormalizeCharHours(value[cnt]);

                    bhours.Add(th);
                }

                th = NormalizeCharHours(value[0]);

                bhours.Add(th);
            }

            string temp = String.Empty;

            int daycnt = 0;

            for (int cnt = 0; cnt < (bhours.Count - 2); cnt += 3)
            {
                temp = String.Empty;

                int inv = 0;

                for (int icnt = cnt; icnt < cnt + 3; icnt++)
                { 
                    temp += HoursFromCharArray(bhours[icnt], inv);

                    inv++;
                }

                ret[daycnt] += temp;

                daycnt++;
            }

            return ret;
        }

        public List<string> DecodeDsaSignature(DirectoryAttribute attrib)
        {
            List<string> ret = new List<string> { };

            foreach (byte[] value in attrib.GetValues(typeof(byte[])))
            { ret.AddRange(DsaSignature.Decode(value)); }

            return ret;
        }

        public List<String> DecodeDSHeuristics(DirectoryAttribute attrib)
        {
            List<string> ret = new List<string> { };



            return ret;
        }

        public ActiveDirectorySyntax GetAttributeSyntax(string name, out bool isBad)
        {
            ActiveDirectorySyntax ret = ActiveDirectorySyntax.CaseIgnoreString;

            bool syntaxfromcache = true;
            bool notfound = true;
            bool incache = false;
            isBad = false;

            AttributeSchema attribute = null; ;

            if (name.Contains(";"))
            { name = name.Substring(0, name.IndexOf(";")); }

            try
            {
                if (ForestBase.AttributeCache.ContainsKey(name))
                {
                    attribute = ForestBase.AttributeCache[name];

                    ret = attribute.Syntax;
                    syntaxfromcache = attribute.SyntaxFromCache;

                    isBad = attribute.UnkownSyntax;

                    notfound = false;
                }

                if ((syntaxfromcache) && (ret == ActiveDirectorySyntax.OctetString))
                {
                    incache = AttributeIsInCache(ForestBase.ForestName, name, out ret, out isBad);

                    if ((incache) || (isBad))
                    {
                        attribute.UpdateSyntax(ret, isBad);

                        return ret;
                    }

                    else
                    {
                        ActiveDirectorySyntax temp = SyntaxFromLiveSchema(name);

                        attribute.UpdateSyntax(temp);

                        ret = temp;

                        AddAttributeToCache(ForestBase.ForestName, name, ret, false);

                        return ret;
                    }
                }

                if (notfound) { }
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        private ActiveDirectorySyntax SyntaxFromLiveSchema(string name)
        {
            ActiveDirectorySyntax ret = ActiveDirectorySyntax.CaseIgnoreString;

            string filter = String.Format("(&(objectClass=attributeSchema)(lDAPDisplayName={0}))", name);

            string[] attributes = new string[] { "attributeSyntax", "oMSyntax" };

            List<SearchResultEntry> result = Query(ForestBase.CurrentDC, ForestBase.SchemaNamingContext,
                                                  filter, attributes, SearchScope.Subtree,
                                                  ReferralChasingOptions.None, 
                                                  new QueryControl() { CurrentResultEventType = QUERY_RESULT_EVENT_TYPE.FROM_DECODING }, 
                                                  returnResults: true);

            if (result.Count != 0)
            {
                try
                {
                    string attributesyntax = result[0].Attributes["attributeSyntax"][0].ToString();

                    int omsyntax = -1;

                    Int32.TryParse(result[0].Attributes["oMSyntax"][0].ToString(), out omsyntax);

                    ret = DecodeLDAPAttributeSyntax(attributesyntax, omsyntax);
                }

                catch (Exception ex)
                { ex.ToDummy(); }
            }

            return ret;
        }

        private List<string> WalkACL(CommonAcl acl, bool translateSid = true)
        {
            List<String> ret = new List<string> { };

            foreach (Object oACE in acl)
            {
                if (oACE.GetType().Name == "CommonAce")
                {
                    CommonAce oReadAce = (CommonAce)oACE;
                    
                    ret.Add(String.Format("\t\t\tType: <{0}> Flags: <{1}>", oReadAce.AceType, oReadAce.AceFlags));
                    ret.Add(String.Format("\t\t\tTrustee: <{0}>", DecodeSID(oReadAce.SecurityIdentifier, translateSid)));
                    ret.Add(String.Format("\t\t\t  AccessMask: <{0}> ({1})", DecodeAccessMask(oReadAce.AccessMask), oReadAce.IsInherited ? "inherited right" : "direct right"));
                    ret.Add(String.Format("\t\t\t  inheritance: <{0}> propagation: <{1}>", oReadAce.InheritanceFlags, oReadAce.PropagationFlags));

                }

                else if (oACE.GetType().Name == "ObjectAce")
                {
                    ObjectAce oReadAce = (ObjectAce)oACE;

                    ret.Add(String.Format("\t\t\tType: <{0}> Flags: <{1}>", oReadAce.AceType, oReadAce.AceFlags));
                    ret.Add(String.Format("\t\t\tTrustee: <{0}>", DecodeSID(oReadAce.SecurityIdentifier, translateSid)));
                    ret.Add(String.Format("\t\t\t  AccessMask: <{0}> ({1})", DecodeAccessMask(oReadAce.AccessMask), oReadAce.IsInherited ? "inherited right" : "direct right"));

                    ret.AddRange(DecodeOIDs(oReadAce));

                    ret.Add(String.Format("\t\t\t  inheritance: <{0}> propagation: <{1}>", oReadAce.InheritanceFlags, oReadAce.PropagationFlags));
                }

                ret.Add(String.Format("\t\t\t{0}", new string('-', 60)));
            }

            return ret;
        }

        private string NameFromOID(Guid oid)
        {
            string ret = null;

            ret = ForestBase.RightsGuids.GetValueSafe<string, string>(oid.ToString());

            if (ret == null)
            { ret = oid.ToString(); }

            return String.Format("{0} ({1})", ret, oid.ToString());
        }

        private char[] NormalizeCharHours(byte val)
        {
            char[] ret = new char[] { };

            int thcnt = 0;

            ret = (Convert.ToString(val, 2)).ToCharArray();

            Array.Reverse(ret);

            thcnt = ret.Count();

            if (thcnt != 8)
            {
                Array.Resize(ref ret, 8);

                for (int cnt = thcnt; cnt < 8; cnt++)
                { ret[cnt] = '0'; }
            }

            return ret;
        }

        private string HoursFromCharArray(char[] hours, int intervall)
        {
            string ret = String.Empty;

            bool decode = true;

            Dictionary<int, List<string>> hlist = new Dictionary<int, List<string>>();

            hlist.Add(0, new List<string> { "00 ", "01 ", "02 ", "03 ", "04 ", "05 ", "06 ", "07 "});
            hlist.Add(1, new List<string> { "08 ", "09 ", "10 ", "11 ", "12 ", "13 ", "14 ", "15 "});
            hlist.Add(2, new List<string> { "16 ", "17 ", "18 ", "19 ", "20 ", "21 ", "22 ", "23 "});

            if (hours.Count() == 1)
            {
                if (hours[0] == '0')
                { decode = false; }
            }

            if (decode)
            {
                for (int cnt = 0; cnt < hours.Count(); cnt++)
                {
                    if (hours[cnt] == '0')
                    { hlist[intervall][cnt] = "__ "; }
                }
            }

            ret = String.Join("  ", hlist[intervall].ToArray(), 0, hlist[intervall].Count);

            hlist.Clear();

            return ret;
        }
    
        #endregion
    }

    internal class UserParameters
    {

    }

    internal class ForestTrustInfoDecoder : BlobDecoder
    {
        #region nested

        public class TrustInfoSuffix : BlobDecoder
        {
            #region fields

            //protected DecoderSequence inDataLenSeq = new DecoderSequence(0, 4);

            protected TrustFlagsSuffix inFlags;

            protected DateTime inTimestamp;

            protected string inSuffix;


            public TrustFlagsSuffix Flags { get { return this.inFlags; } }

            public DateTime Timestamp { get { return this.inTimestamp; } }

            public string Suffix { get { return this.inSuffix; } }

            #endregion

            #region ctor

            public TrustInfoSuffix(byte[] value, DecoderSequence dataSeq, int flags, DateTime timeStamp)
            { LoadInternal(value, dataSeq, flags, timeStamp); }

            #endregion

            #region methods

            protected void LoadInternal(byte[] value, DecoderSequence dataSeq, int flags, DateTime timeStamp)
            {
                this.inFlags = (TrustFlagsSuffix)Enum.Parse(typeof(TrustFlagsSuffix), flags.ToString());

                this.inTimestamp = timeStamp;

                dataSeq.FieldType = FIELD_TYPES.u;

                this.inSuffix = (string)DecodeField(value, dataSeq);
            }

            #endregion
        }

        public class TrustInfoName : BlobDecoder
        {
            #region fields

            protected DecoderSequence inDataLenSeq = new DecoderSequence(0, 4);

            protected TrustFlagsName inFlags;

            protected DateTime inTimestamp;

            protected SecurityIdentifier inSID;

            protected string inDnsName;

            protected string inNetBios;


            public TrustFlagsName Flags { get { return this.inFlags; } }

            public DateTime Timestamp { get { return this.inTimestamp; } }

            public SecurityIdentifier SID { get { return this.inSID; } }

            public string DnsName { get { return this.inDnsName; } }

            public string NetBiosName { get { return this.inNetBios; } }

            #endregion

            #region ctor

            public TrustInfoName(byte[] value, int flags, DateTime timeStamp)
            { LoadInternal(value, flags, timeStamp); }

            #endregion

            #region methods

            protected void LoadInternal(byte[] value, int flags, DateTime timeStamp)
            {
                int len = 0;

                this.inFlags = (TrustFlagsName)Enum.Parse(typeof(TrustFlagsName), flags.ToString());

                this.inTimestamp = timeStamp;


                DecoderSequence dataseq = NextSequence(value, ref len, FIELD_TYPES.S);

                this.inSID = (SecurityIdentifier)DecodeField(value, dataseq);

                dataseq = NextSequence(value, ref len, FIELD_TYPES.u);

                this.inDnsName = (string)DecodeField(value, dataseq);

                NextSequence(value, ref len, FIELD_TYPES.u);

                this.inNetBios = (string)DecodeField(value, dataseq);
            }

            protected DecoderSequence NextSequence(byte[] value, ref int len, FIELD_TYPES fieldtype)
            {
                DecoderSequence ret = new DecoderSequence(0, 0);

                this.inDataLenSeq.StartPoint = this.inDataLenSeq.StartPoint + len;

                len = (int)DecodeField(value, this.inDataLenSeq);

                this.inDataLenSeq.StartPoint = this.inDataLenSeq.StartPoint + this.inDataLenSeq.Length;

                ret = new DecoderSequence(this.inDataLenSeq.StartPoint, len, fieldtype);

                return ret;
            }

            #endregion
        }

        #endregion

        #region internal fields

        protected DecoderSequence inVersionSeq = new DecoderSequence(0, 4);

        protected DecoderSequence inRecordCountSeq = new DecoderSequence(4, 4);


        protected DecoderSequence inRecordLenSeq = new DecoderSequence(0, 4);

        protected DecoderSequence inFlagsSeq = new DecoderSequence(4, 4);

        protected DecoderSequence inTimeStampSeq = new DecoderSequence(8, 8, FIELD_TYPES.d);

        protected DecoderSequence inRecordTypeSeq = new DecoderSequence(16, 1);

        protected DecoderSequence inRecordDataSeq { get { return new DecoderSequence(8, this.inValue.Length - 8); } }


        protected byte[] inValue;

        protected int inVersion;

        protected int inRecordCount;

        protected List<TrustInfoSuffix> inSuffixes = new List<TrustInfoSuffix> { };

        protected TrustInfoName inNameInfo;

        #endregion

        #region public fields

        public int Version { get { return this.inVersion; } }

        public int RecordCount { get { return this.inRecordCount; } }

        public List<TrustInfoSuffix> Suffixes { get { return this.inSuffixes; } }

        public TrustInfoName NameInfo { get { return this.inNameInfo; } }

        #endregion

        #region ctor

        public ForestTrustInfoDecoder(byte[] value)
        { LoadInternal(value); }

        #endregion

        #region public methods

        public new List<string> ToString()
        {
            List<string> ret = new List<string> { };

            ret.Add(String.Format("\t\t<{0}: {1}", "Version", this.Version));

            ret.Add(String.Format("\t\t<{0}:", "NamingInfo"));

            ret.Add(String.Format("\t\t\t<{0}: {1}", "Flags", this.NameInfo.Flags.ToString()));

            ret.Add(String.Format("\t\t\t<{0}: {1}", "Timestamp", this.NameInfo.Timestamp.ToString()));

            ret.Add(String.Format("\t\t\t<{0}: {1}", "SID", this.NameInfo.SID.ToString()));

            ret.Add(String.Format("\t\t\t<{0}: {1}", "DnsName", this.NameInfo.DnsName));

            ret.Add(String.Format("\t\t\t<{0}: {1}", "NetBiosName", this.NameInfo.NetBiosName));

            foreach (TrustInfoSuffix suffix in this.Suffixes)
            {
                ret.Add(String.Format("\t\t<{0}:", "SuffixInfo"));

                ret.Add(String.Format("\t\t\t<{0}: {1}", "Flags", suffix.Flags.ToString()));

                ret.Add(String.Format("\t\t\t<{0}: {1}", "Timestamp", suffix.Timestamp.ToString()));

                ret.Add(String.Format("\t\t\t<{0}: {1}", "Suffix", suffix.Suffix));

            }

            return ret;
        }

        #endregion

        #region internal methods

        protected void LoadInternal(byte[] value)
        {
            this.inValue = value;

            this.inVersion = (int)DecodeField(this.inValue, this.inVersionSeq);

            this.inRecordCount = (int)DecodeField(this.inValue, this.inRecordCountSeq);

            WalkRecords();
        }

        protected void WalkRecords()
        {
            int len = 0;

            int startpoint = this.inRecordDataSeq.StartPoint;

            int datalen = this.inRecordDataSeq.Length;

            byte[] records = new byte[datalen];

            Array.Copy(this.inValue, startpoint, records, 0, datalen);

            for (int cnt = 1; cnt <= this.RecordCount; cnt++)
            {
                DecoderSequence recseq = new DecoderSequence(startpoint, datalen);

                len = (int)DecodeField(records, this.inRecordLenSeq);

                WalkRecord(records, recseq, len);

                startpoint = len + 4;

                datalen = datalen - (len + 4);

                Array.Copy(records, startpoint, records, 0, datalen);
            }
        }

        protected void WalkRecord(byte[] recordData, DecoderSequence recSeq, int recLen)
        {
            DateTime timestamp = DateTime.MinValue;

            int flags = 0;

            TrustRecordType recordtype = 0;

            flags = (int)DecodeField(recordData, inFlagsSeq);

            timestamp = (DateTime)DecodeField(recordData, inTimeStampSeq);

            recordtype = (TrustRecordType)DecodeField(recordData, inRecordTypeSeq);

            if (recordtype == TrustRecordType.ForestTrustDomainInfo)
            {
                DecodeNameData(recordData, recLen, flags, timestamp);
            }

            else
            {
                DecodeSuffixData(recordData, recLen, flags, timestamp);
            }
        }

        protected void DecodeSuffixData(byte[] recordData, int recLen, int flags, DateTime timeStamp)
        {
            this.inSuffixes.Add(new TrustInfoSuffix(recordData, GetCurrentRecordSeq(recLen), flags, timeStamp));
        }

        protected void DecodeNameData(byte[] recordData, int recLen, int flags, DateTime timeStamp)
        {
            byte[] value = new byte[recLen - 17];

            Array.Copy(recordData, 17, value, 0, value.Length);

            this.inNameInfo = new TrustInfoName(value, flags, timeStamp);
        }

        protected DecoderSequence GetCurrentRecordSeq(int curLen)
        {
            return new DecoderSequence(21, curLen - 17, FIELD_TYPES.a);
        }

        #endregion
    }


    internal class ReplUpToDateVector : BlobDecoder
    {
        internal ReplUpToDateVector()
        {
            base.ReplFields.Add("dwVersion", new DecoderSequence(0, 4));
            base.ReplFields.Add("dwReserved1", new DecoderSequence(4, 4));
            base.ReplFields.Add("V2.cNumCursors", new DecoderSequence(8, 4));
            base.ReplFields.Add("V2.dwReserved2", new DecoderSequence(12, 4));
            base.ReplFields.Add("rgCursors", new DecoderSequence(16, 32, isArray:true));

            Dictionary<string, DecoderSequence> subfields = new Dictionary<string, DecoderSequence> { };

            subfields.Add("uuidDsa", new DecoderSequence(0, 16, FIELD_TYPES.G));
            subfields.Add("usnHighPropUpdate", new DecoderSequence(16, 8));
            subfields.Add("timeLastSyncSuccess", new DecoderSequence(24, 8, FIELD_TYPES.D));

            base.ArrayFields.Add("rgCursors", subfields);
        }
    }


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


    internal class DecoderSequence
    {
        public int StartPoint;
        public int Length;
        public bool IsArray;
        public FIELD_TYPES FieldType;

        internal DecoderSequence(int start, int length, FIELD_TYPES fieldType = FIELD_TYPES.i, bool isArray = false)
        {
            StartPoint = start;

            Length = length;

            IsArray = isArray;

            FieldType = fieldType;
        }
    }

    internal enum TrustRecordType
    {
        ForestTrustTopLevelName = 0,
        ForestTrustTopLevelNameEx,
        ForestTrustDomainInfo
    }

    /// <summary>
    /// big endian
    /// </summary>
    [Flags]
    internal enum TrustFlagsSuffix : int
    {
        LSA_TLN_NONE = 0x00000000,
        LSA_TLN_DISABLED_NEW = 0x00000001,
        LSA_TLN_DISABLED_ADMIN = 0x00000002,
        LSA_TLN_DISABLED_CONFLICT = 0x00000004
    }

    /// <summary>
    /// big endian
    /// </summary>
    [Flags]
    internal enum TrustFlagsName : int
    {
        LSA_NAME_NONE = 0x00000000,
        LSA_SID_DISABLED_ADMIN = 0x00000001,
        LSA_SID_DISABLED_CONFLICT = 0x00000002,
        LSA_NB_DISABLED_ADMIN = 0x00000004,
        LSA_NB_DISABLED_CONFLICT = 0x00000008
    }

    internal enum FIELD_TYPES
    {
        i, //numeic
        b, // byte
        a, //string
        u, //unicode string
        O, // Octet
        v, // reserved
        G, // Guid
        D, // DateTime
        d, // FileTime(LargeInteger)
        S  // Sid
    }

}
