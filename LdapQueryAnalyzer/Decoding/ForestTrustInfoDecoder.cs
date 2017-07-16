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
using System.Security.Principal;

namespace CodingFromTheField.LdapQueryAnalyzer
{
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

            ret.AddFormatted("\t\t<{0}: {1}", "Version", this.Version);

            ret.AddFormatted("\t\t<{0}:", "NamingInfo");

            ret.AddFormatted("\t\t\t<{0}: {1}", "Flags", this.NameInfo.Flags.ToString());

            ret.AddFormatted("\t\t\t<{0}: {1}", "Timestamp", this.NameInfo.Timestamp.ToString());

            ret.AddFormatted("\t\t\t<{0}: {1}", "SID", this.NameInfo.SID.ToString());

            ret.AddFormatted("\t\t\t<{0}: {1}", "DnsName", this.NameInfo.DnsName);

            ret.AddFormatted("\t\t\t<{0}: {1}", "NetBiosName", this.NameInfo.NetBiosName);

            foreach (TrustInfoSuffix suffix in this.Suffixes)
            {
                ret.AddFormatted("\t\t<{0}:", "SuffixInfo");

                ret.AddFormatted("\t\t\t<{0}: {1}", "Flags", suffix.Flags.ToString());

                ret.AddFormatted("\t\t\t<{0}: {1}", "Timestamp", suffix.Timestamp.ToString());

                ret.AddFormatted("\t\t\t<{0}: {1}", "Suffix", suffix.Suffix);

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
}
