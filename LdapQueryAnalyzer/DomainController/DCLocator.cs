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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class DCLocator
    {
        #region helper

        private static void HandleError(int rc, ref DomainControllerHelper dch, string domainDNS, string siteName, string flags)
        {
            dch.Success = false;

            dch.ErrorString = "HResult: " + rc;

            switch (rc)
            {
                case 8:
                    dch.ErrorString = string.Format("{0} (Not enough storage is available to process this command!)", dch.ErrorString);
                    break;

                case 997:
                    dch.ErrorString = string.Format("{0} (No DC in domain: {1}, site: {2} answered!)", dch.ErrorString, Convert.ToString(domainDNS), Convert.ToString(siteName));
                    break;

                case 1004:
                    dch.ErrorString = string.Format("{0} (Invalid flags: {1}!", dch.ErrorString, flags);
                    break;

                case 1211:
                    dch.ErrorString = string.Format("{0} (The format of the specified domain name is invalid: {1}!)", dch.ErrorString, Convert.ToString(domainDNS));
                    break;

                case 1355:
                    dch.ErrorString = string.Format("{0} (The specified domain either does not exist or could not be contacted: {1}!)", dch.ErrorString, Convert.ToString(domainDNS));
                    break;

                default:
                    dch.ErrorString = string.Format("{0} (Unknown error!)", dch.ErrorString);
                    break;
            }
        }

        private static void HandleError(int rc, ref DCInfo dch, string domainDNS, string siteName, string flags)
        {
            dch.Success = false;

            dch.ErrorString = "HResult: " + rc;

            switch (rc)
            {
                case 8:
                    dch.ErrorString = string.Format("{0} (Not enough storage is available to process this command!)", dch.ErrorString);
                    break;

                case 997:
                    dch.ErrorString = string.Format("{0} (No DC in domain: {1}, site: {2} answered!)", dch.ErrorString, Convert.ToString(domainDNS), Convert.ToString(siteName));
                    break;

                case 1004:
                    dch.ErrorString = string.Format("{0} (Invalid flags: {1}!", dch.ErrorString, flags);
                    break;

                case 1211:
                    dch.ErrorString = string.Format("{0} (The format of the specified domain name is invalid: {1}!)", dch.ErrorString, Convert.ToString(domainDNS));
                    break;

                case 1355:
                    dch.ErrorString = string.Format("{0} (The specified domain either does not exist or could not be contacted: {1}!)", dch.ErrorString, Convert.ToString(domainDNS));
                    break;

                default:
                    dch.ErrorString = string.Format("{0} (Unknown error!)", dch.ErrorString);
                    break;
            }
        }

        #endregion

        #region DsGetDcOpen / DsgetDcNext / DsGetDcClose -> DomainController
        
        public static List<DomainControllerHelper> DnsGetDCs(string domainDNS, string siteName, string forestName, NetApi32.DS_OPEN_FLAGS flags, NetApi32.DS_OPTION_FLAGS siteOptions, bool doUdpPing = false)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            DomainControllerHelper errdc = new DomainControllerHelper() { Success = false };

            int iret = -1;

            IntPtr iphandle = IntPtr.Zero;

            try
            {
                if (domainDNS == null)
                    domainDNS = Environment.ExpandEnvironmentVariables("%USERDNSDOMAIN%");

                iret = NetApi32.DsGetDcOpen(domainDNS, siteOptions, siteName, IntPtr.Zero, forestName, flags, out iphandle);
            }

            catch (Win32Exception Win32Ex)
            { errdc.ErrorString = Win32Ex.ErrorCode + " (Win32: " + Win32Ex.Message + ")"; }

            catch (Exception ex)
            { errdc.ErrorString = ex.Message; }

            if (iret == 0)
            {
                string dnsName = null;

                IntPtr socketcnt = IntPtr.Zero;
                IntPtr sockets = IntPtr.Zero;

                while ((iret == (int)NetApi32.ERROR_LIST.ERROR_SUCCESS) || (iret == (int)NetApi32.ERROR_LIST.ERROR_FILEMARK_DETECTED))
                {
                    errdc = new DomainControllerHelper() { Success = false };

                    try
                    { iret = NetApi32.DsGetDcNext(iphandle, ref socketcnt, out sockets, out dnsName); }

                    catch (Win32Exception Win32Ex)
                    { errdc.ErrorString = Win32Ex.ErrorCode + " (Win32: " + Win32Ex.Message + ")"; }

                    catch (Exception ex)
                    { errdc.ErrorString = ex.Message; }

                    if ((iret == (int)NetApi32.ERROR_LIST.ERROR_SUCCESS) || (iret == (int)NetApi32.ERROR_LIST.ERROR_FILEMARK_DETECTED))
                    {
                        if (doUdpPing)
                        {
                            DateTime start = DateTime.Now;

                            GlobalEventHandler.RaiseMessageOccured(String.Format("UDPPing against {0} ({1})", dnsName, domainDNS));

                            ret.Add(DomainControllerHelper.UDPPing(dnsName));

                            double dur = DateTime.Now.Subtract(start).TotalMilliseconds;

                            GlobalEventHandler.RaiseMessageOccured(String.Format("UDPPing {0} in {1} ms", dnsName, dur.ToString()));
                        }

                        else
                        { ret.Add(new DomainControllerHelper(domainDNS, dnsName)); }
                    }

                    else
                    {
                        if (ret.Count == 0)
                        {
                            HandleError(iret, ref errdc, domainDNS, siteName, flags.ToString());

                            ret.Add(errdc);
                        }
                    }

                }
            }

            else
            {
                if (ret.Count == 0)
                {
                    HandleError(iret, ref errdc, domainDNS, siteName, flags.ToString());

                    ret.Add(errdc);
                }
            }

            if (iphandle != IntPtr.Zero)
            { NetApi32.DsGetDcClose(iphandle); }

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetForestdDCs(string forestName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(forestName, null, forestName, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY, NetApi32.DS_OPTION_FLAGS.DS_OPTION_NONE, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetForestdWritableDCs(string forestName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(forestName, null, forestName, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_WRITABLE_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_OPTION_NONE, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetDomaindDCs(string domainDNS, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, null, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY, NetApi32.DS_OPTION_FLAGS.DS_OPTION_NONE, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetDomaindWritableDCs(string domainDNS, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, null, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_WRITABLE_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_OPTION_NONE, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetForestSiteWeightedDCs(string forestName, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(forestName, siteName, forestName, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY, NetApi32.DS_OPTION_FLAGS.DS_NOTIFY_AFTER_SITE_RECORDS, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetForestSiteWeightedWritableDCs(string forestName, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(forestName, siteName, forestName, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_WRITABLE_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_NOTIFY_AFTER_SITE_RECORDS, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetDomainSiteWeightedDCs(string domainDNS, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, siteName, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY, NetApi32.DS_OPTION_FLAGS.DS_NOTIFY_AFTER_SITE_RECORDS, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetDomainSiteWeightedWritableDCs(string domainDNS, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, siteName, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_WRITABLE_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_NOTIFY_AFTER_SITE_RECORDS, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetForestSiteDCs(string forestName, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(forestName, siteName, forestName, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY, NetApi32.DS_OPTION_FLAGS.DS_ONLY_DO_SITE_NAME, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetForestWritableSiteDCs(string forestName, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(forestName, siteName, forestName, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_WRITABLE_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_ONLY_DO_SITE_NAME, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetDomainSiteDCs(string domainDNS, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, siteName, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY, NetApi32.DS_OPTION_FLAGS.DS_ONLY_DO_SITE_NAME, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetDomainSiteWritableDCs(string domainDNS, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, siteName, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_WRITABLE_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_ONLY_DO_SITE_NAME, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetSiteWeightedGCs(string domainDNS, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, siteName, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_GC_SERVER_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_NOTIFY_AFTER_SITE_RECORDS, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetSiteGCs(string domainDNS, string siteName, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, siteName, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_GC_SERVER_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_ONLY_DO_SITE_NAME, doUdpPing);

            return ret;
        }

        public static List<DomainControllerHelper> DnsGetGCs(string domainDNS, bool doUdpPing = true)
        {
            List<DomainControllerHelper> ret = new List<DomainControllerHelper> { };

            ret = DnsGetDCs(domainDNS, null, null, NetApi32.DS_OPEN_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_OPEN_FLAGS.DS_GC_SERVER_REQUIRED, NetApi32.DS_OPTION_FLAGS.DS_OPTION_NONE, doUdpPing);

            return ret;
        }

        #endregion

        #region DsGetDcname -> DomainController

        public static DomainControllerHelper GetDC(string domainDNS, string siteName, NetApi32.DS_FLAGS flags)
        {
            DomainControllerHelper ret = new DomainControllerHelper() { Success = false };

            int iret = -1;
            IntPtr ipinfo = IntPtr.Zero;
            NetApi32.DOMAIN_CONTROLLER_INFO stcinfo = default(NetApi32.DOMAIN_CONTROLLER_INFO);

            if (domainDNS == null)
                domainDNS = String.Empty;

            try
            { iret = NetApi32.DsGetDcName(null, domainDNS, new Guid(), siteName, flags, ref ipinfo); }

            catch (Win32Exception Win32Ex)
            { ret.ErrorString = Win32Ex.ErrorCode + " (Win32: " + Win32Ex.Message + ")"; }

            catch (Exception ex)
            { ret.ErrorString = ex.Message; }


            if (iret == 0)
            {
                ret.Success = true;

                stcinfo = (NetApi32.DOMAIN_CONTROLLER_INFO)Marshal.PtrToStructure(ipinfo, typeof(NetApi32.DOMAIN_CONTROLLER_INFO));

                NetApi32.NetApiBufferFree(ipinfo);

                ret = DomainControllerHelper.FromStruct(stcinfo);
            }

            else
            { HandleError(iret, ref ret, domainDNS, siteName, flags.ToString()); }

            return ret;
        }

        public static DomainControllerHelper GetCachedDC(string domainDNS)
        {
            DomainControllerHelper ret = new DomainControllerHelper();

            ret = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_BACKGROUND_ONLY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME);

            return ret;
        }

        public static DomainControllerHelper GetCachedGC(string domainDNS)
        {
            DomainControllerHelper ret = new DomainControllerHelper();

            ret = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_BACKGROUND_ONLY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return ret;
        }

        public static DomainControllerHelper GetCachedWDC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_BACKGROUND_ONLY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;
        }

        public static DomainControllerHelper GetAnyDC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME);

            return scRet;
        }

        public static DomainControllerHelper GetAnyGC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return scRet;
        }

        public static DomainControllerHelper GetAnyWDC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;
        }

        public static DomainControllerHelper GetClostesDC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_TRY_NEXTCLOSEST_SITE);

            return scRet;
        }

        public static DomainControllerHelper GetClostesGC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_TRY_NEXTCLOSEST_SITE | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return scRet;
        }

        public static DomainControllerHelper GetClostestWDC(string domainDNS)
        {
            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_TRY_NEXTCLOSEST_SITE | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;
        }

        public static DomainControllerHelper GetSiteDC(string callInfo)
        {

            DomainControllerHelper scRet = new DomainControllerHelper();
            string domainDNS = null;
            string SiteName = null;


            if (callInfo.IndexOf(" ") > 0)
            {
                SiteName = callInfo.Split(Convert.ToChar(" "))[0];

                domainDNS = callInfo.Split(Convert.ToChar(" "))[1];
            }

            else
            {
                SiteName = callInfo;
            }

            scRet = GetDC(domainDNS, SiteName, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME);

            return scRet;

        }

        public static DomainControllerHelper GetSiteGC(string callInfo)
        {

            DomainControllerHelper scRet = new DomainControllerHelper();
            string domainDNS = null;
            string SiteName = null;


            if (callInfo.IndexOf(" ") > 0)
            {
                SiteName = callInfo.Split(Convert.ToChar(" "))[0];

                domainDNS = callInfo.Split(Convert.ToChar(" "))[1];
            }

            else
            {
                SiteName = callInfo;
            }

            scRet = GetDC(domainDNS, SiteName, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return scRet;

        }

        public static DomainControllerHelper GetSiteWDC(string callInfo)
        {

            DomainControllerHelper scRet = new DomainControllerHelper();
            string domainDNS = null;
            string SiteName = null;


            if (callInfo.IndexOf(" ") > 0)
            {
                SiteName = callInfo.Split(Convert.ToChar(" "))[0];

                domainDNS = callInfo.Split(Convert.ToChar(" "))[1];
            }

            else
            {
                SiteName = callInfo;
            }

            scRet = GetDC(domainDNS, SiteName, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;

        }

        public static DomainControllerHelper GetPDC(string domainDNS)
        {

            DomainControllerHelper scRet = new DomainControllerHelper();

            scRet = GetDC(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED | NetApi32.DS_FLAGS.DS_PDC_REQUIRED);

            return scRet;

        }

        #endregion

        #region DsGetDcname -> DCInfo

        public static DCInfo DsGetDCName(string domainDNS, string siteName, NetApi32.DS_FLAGS flags)
        {
            DCInfo ret = new DCInfo() { Success = false };

            int iret = -1;
            IntPtr ipinfo = IntPtr.Zero;
            NetApi32.DOMAIN_CONTROLLER_INFO stcinfo = default(NetApi32.DOMAIN_CONTROLLER_INFO);

            if (domainDNS == null)
                domainDNS = String.Empty;

            try
            { iret = NetApi32.DsGetDcName(null, domainDNS, new Guid(), siteName, flags, ref ipinfo); }

            catch (Win32Exception Win32Ex)
            { ret.ErrorString = Win32Ex.ErrorCode + " (Win32: " + Win32Ex.Message + ")"; }

            catch (Exception ex)
            { ret.ErrorString = ex.Message; }


            if (iret == 0)
            {
                ret.Success = true;

                stcinfo = (NetApi32.DOMAIN_CONTROLLER_INFO)Marshal.PtrToStructure(ipinfo, typeof(NetApi32.DOMAIN_CONTROLLER_INFO));

                NetApi32.NetApiBufferFree(ipinfo);

                ReturnDCINFO(stcinfo, ref ret);
            }

            else
            { HandleError(iret, ref ret, domainDNS, siteName, flags.ToString()); }

            return ret;
        }

        public static DCInfo DsGetCachedDCName(string domainDNS)
        {
            DCInfo ret = new DCInfo();

            ret = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_BACKGROUND_ONLY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME);

            return ret;
        }

        public static DCInfo DsGetCachedGCName(string domainDNS)
        {
            DCInfo ret = new DCInfo();

            ret = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_BACKGROUND_ONLY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return ret;
        }

        public static DCInfo DsGetCachedWDCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_BACKGROUND_ONLY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;
        }

        public static DCInfo DsGetAnyDCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME);

            return scRet;
        }

        public static DCInfo DsGetAnyGCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return scRet;
        }

        public static DCInfo DsGetAnyWDCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;
        }

        public static DCInfo DsGetClostesDCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_TRY_NEXTCLOSEST_SITE);

            return scRet;
        }

        public static DCInfo DsGetClostesGCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_TRY_NEXTCLOSEST_SITE | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return scRet;
        }

        public static DCInfo DsGetClostestWDCName(string domainDNS)
        {
            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_TRY_NEXTCLOSEST_SITE | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;
        }

        public static DCInfo DsGetSiteDCName(string callInfo)
        {
            DCInfo scRet = new DCInfo();
            string domainDNS = null;
            string SiteName = null;


            if (callInfo.IndexOf(" ") > 0)
            {
                SiteName = callInfo.Split(Convert.ToChar(" "))[0];

                domainDNS = callInfo.Split(Convert.ToChar(" "))[1];
            }

            else
            {
                SiteName = callInfo;
            }

            scRet = DsGetDCName(domainDNS, SiteName, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME);

            return scRet;

        }

        public static DCInfo DsGetSiteGCName(string callInfo)
        {
            DCInfo scRet = new DCInfo();
            string domainDNS = null;
            string SiteName = null;


            if (callInfo.IndexOf(" ") > 0)
            {
                SiteName = callInfo.Split(Convert.ToChar(" "))[0];

                domainDNS = callInfo.Split(Convert.ToChar(" "))[1];
            }

            else
            {
                SiteName = callInfo;
            }

            scRet = DsGetDCName(domainDNS, SiteName, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_GC_SERVER_REQUIRED);

            return scRet;

        }

        public static DCInfo DsGetSiteWDCName(string callInfo)
        {
            DCInfo scRet = new DCInfo();
            string domainDNS = null;
            string SiteName = null;


            if (callInfo.IndexOf(" ") > 0)
            {
                SiteName = callInfo.Split(Convert.ToChar(" "))[0];

                domainDNS = callInfo.Split(Convert.ToChar(" "))[1];
            }

            else
            {
                SiteName = callInfo;
            }

            scRet = DsGetDCName(domainDNS, SiteName, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED);

            return scRet;

        }

        public static DCInfo DsGetPDCName(string domainDNS)
        {

            DCInfo scRet = new DCInfo();

            scRet = DsGetDCName(domainDNS, null, NetApi32.DS_FLAGS.DS_FORCE_REDISCOVERY | NetApi32.DS_FLAGS.DS_DIRECTORY_SERVICE_REQUIRED | NetApi32.DS_FLAGS.DS_RETURN_DNS_NAME | NetApi32.DS_FLAGS.DS_WRITABLE_REQUIRED | NetApi32.DS_FLAGS.DS_PDC_REQUIRED);

            return scRet;

        }

        private static void ReturnDCINFO(NetApi32.DOMAIN_CONTROLLER_INFO DomainInfo, ref DCInfo dc)
        {
            dc.DCName = DomainInfo.DomainControllerName.Replace("\\\\", null);
            dc.DCAddress = DomainInfo.DomainControllerAddress.Replace("\\\\", null);
            if (DomainInfo.DomainControllerAddressType == 1)
            {
                dc.IsIPAddress = true;
            }
            else
            {
                dc.IsNetBIOSAddress = true;
            }

            dc.DomainName = DomainInfo.DomainName;
            dc.DomainGUID = DomainInfo.DomainGuid;
            dc.DNSForestname = DomainInfo.DnsForestName;
            dc.DCSiteName = DomainInfo.DcSiteName;
            dc.ClientSiteName = DomainInfo.ClientSiteName;


            dc.IsPDC = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_PDC_FLAG);
            dc.IsGC = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_GC_FLAG);
            dc.IsLDAPServer = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_LDAP_FLAG);
            dc.IsDC = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_DS_FLAG);
            dc.IsKDC = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_KDC_FLAG);
            dc.IsTimerServer = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_TIMESERV_FLAG);
            dc.IsInClosestSite = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_CLOSEST_FLAG);
            dc.IsWritable = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_WRITABLE_FLAG);
            dc.IsGoodTimerServer = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_GOOD_TIMESERV_FLAG);
            dc.IsNonDomainNC = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_NDNC_FLAG);
            dc.HasSelectedSecrets = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_SELECT_SECRET_DOMAIN_6_FLAG);
            dc.HasAllSecrets = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_FULL_SECRET_DOMAIN_6_FLAG);
            dc.HasWebService = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_WS_FLAG);
            dc.IsDirectoryService2012 = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_DS_8_FLAG);
            dc.IsDirectoryService2012R2 = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_DS_9_FLAG);
            //dc.FromPing = CBool(DomainInfo.Flags And NetApi32.DS_RETURNED_FLAGS.DS_PING_FLAG)
            dc.DCIsDNSName = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_DNS_CONTROLLER_FLAG);
            dc.DomainIsDNSName = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_DNS_DOMAIN_FLAG);
            //dc.ForestIsDNSName = Convert.ToBoolean(DomainInfo.Flags & NetApi32.DS_RETURNED_FLAGS.DS_DNS_FOREST_FLAG);            

            dc.NCInfo = new NamingContextsInfo(dc.DCName);
        }

        #endregion
    }
}
