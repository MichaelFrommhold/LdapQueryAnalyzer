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
using System.Threading;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    //http://msdn.microsoft.com/en-us/library/aa645739(v=vs.71).aspx
    //http://msdn.microsoft.com/en-us/library/system.eventargs(v=vs.110).aspx

    public delegate void GlobalEvent(object sender, GlobalEventArgs args = null);

    public class GlobalEventHandler
    {
        #region events

        public static event GlobalEvent FormExited;
        public static event GlobalEvent DynamicDllUpdated;
        public static event GlobalEvent SubsequentQuery;
        public static event GlobalEvent SignalError;
        
        public static event GlobalEvent ClipBoardChanged;

        public static event GlobalEvent CountOfDCs;
        public static event GlobalEvent DCDiscovered;

        public static event GlobalEvent UDPPingProceeded;
        public static event GlobalEvent DCPingProceeded;
        public static event GlobalEvent DiscoveredCurrentDomain;
        public static event GlobalEvent DiscoveredCurrentDomainNCs;
        public static event GlobalEvent DiscoveredForest;
        public static event GlobalEvent DiscoveredSchema;
        public static event GlobalEvent DomainsUpdated;
        public static event GlobalEvent FinishedDiscovering;

        public static event GlobalEvent SearchCancelled;
        public static event GlobalEvent QueryCompleted;
        public static event GlobalEvent DecodingCompleted;
        public static event GlobalEvent AsyncQureyIsCompleted;
        public static event GlobalEvent AsyncPartialResult;
        public static event GlobalEvent ParallelQueriesCompleted;
        
        public static AutoResetEvent[] Signaler = new AutoResetEvent[1] { new AutoResetEvent(false) };
        public static event GlobalEvent DecodingProcess;

        public static event GlobalEvent SettingChanged;
        public static event GlobalEvent SettingRead;

        public static event GlobalEvent GuiBasePathSelected;
        public static event GlobalEvent GuiBaseObjectSelected;
        public static event GlobalEvent GuiAsqPathSelected;
        public static event GlobalEvent WizardPathSelected;

        public static event GlobalEvent ConnectionSignaled;

        public static event GlobalEvent CredentialsSignaled;

        public static event GlobalEvent FilterSignaled;

        public static event GlobalEvent DoBackup;

        public static event GlobalEvent StartSearch;
        public static event GlobalEvent ContinueSearch;

        public static event GlobalEvent NestingFound;

        #endregion

        #region .Net



        #endregion

        #region common

        public static void RaiseFormExited(string name)
        {
            if (FormExited != null)
            { FormExited(name); }
        }     

        public static void RaiseDynamicDllUpdated(DynamicTypeLoader dynDll)
        {
            if (DynamicDllUpdated != null)
            { DynamicDllUpdated(dynDll); }
        }

        public static void RaiseSubsequentQuery(long ms)
        {
            if (SubsequentQuery != null)
            { SubsequentQuery(ms); }
        }

        public static void RaiseErrorOccured(string errorMsg, bool showText = false)
        {
            if (SignalError != null)
            { SignalError(errorMsg, new GlobalEventArgs() { BoolVal = new bool[] { showText } }); }
        }

        public static void RaiseMessageOccured(string msg, bool showText = false)
        {
            if (SignalError != null)
            { SignalError(msg, new GlobalEventArgs() { BoolVal = new bool[] { showText } }); }
        }

        public static void RaiseDebugInfoOccured(string msg, params object[] args)
        {
            if (SignalError != null)
            { 
                try
                { msg = string.Format(msg, args); }
                catch {}

                SignalError(msg, new GlobalEventArgs() { BoolVal = new bool[] { false } }); 
            }
        }

        public static void RaiseStartSearch()
        {
            if (StartSearch != null)
            { StartSearch(null, null); }
        }

        public static void RaiseContinueSearch()
        {
            if (ContinueSearch != null)
            { ContinueSearch(null, null); }
        }


        #endregion

        #region ClipBoardWatcher

        public static void RaiseClipBoardChanged(bool hasText)
        {
            if (ClipBoardChanged != null)
            { ClipBoardChanged(hasText); }
        }

        #endregion

        #region DomainInfo

        public static void RaiseCountOfDCs(int count)
        {
            if (CountOfDCs != null)
            { CountOfDCs(count); }
        }

        public static void RaiseDCDiscovered(long ms)
        {
            if (DCDiscovered != null)
            { DCDiscovered(ms); }
        }

        #endregion

        #region ForestInfo     

        public static void RaiseUDPPingProceeded(long ms, long overall)
        {
            if (UDPPingProceeded != null)
            { UDPPingProceeded(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDCPingProceeded(string dcName, long ms, long overall)
        {
            if (UDPPingProceeded != null)
            { UDPPingProceeded(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDiscoveredCurrentDomain(long ms, long overall)
        {
            if (DiscoveredCurrentDomain != null)
            { DiscoveredCurrentDomain(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDiscoveredCurrentDomainNCs(long ms, long overall)
        {
            if (DiscoveredCurrentDomainNCs != null)
            { DiscoveredCurrentDomainNCs(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDiscoveredForest(long ms, long overall)
        {
            if (DiscoveredForest != null)
            { DiscoveredForest(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDiscoveredSchema(long ms, long overall)
        {
            if (DiscoveredSchema != null)
            { DiscoveredSchema(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDomainsUpdated(long ms, long overall)
        {
            if (DomainsUpdated != null)
            { DomainsUpdated(ms, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseFinishedDiscovering()
        {
            if (FinishedDiscovering != null)
            { FinishedDiscovering(null); }
        }

        #endregion

        #region ADHelper

        public static void RaiseSearchCancelled()
        {
            if (SearchCancelled != null)
            { SearchCancelled(null, null); }
        }

        public static void ClearSearchCancelled()
        {
            if (SearchCancelled != null)
            { SearchCancelled = null; }
        }

        public static void RaiseQueryCompleted(List<SearchResultEntry> entries, SearchRequestExtender infoStore, QUERY_RESULT_EVENT_TYPE resultType)
        {
            if (QueryCompleted != null)
            { QueryCompleted(infoStore, new GlobalEventArgs() { Entries = entries, ResultEventType = resultType }); }
        }

        public static void RaiseAsyncQureyIsCompleted()
        {
            if (AsyncQureyIsCompleted != null)
            { AsyncQureyIsCompleted(null); }
        }

        public static void RaiseAsyncPartialResult(List<SearchResultEntry> entries, bool isComplete)
        {
            if (AsyncPartialResult != null)
            { AsyncPartialResult(entries, new GlobalEventArgs() { BoolVal = new bool[] { isComplete } }); }
        }

        public static void ClearAsyncPartialResult()
        {
            if (AsyncPartialResult != null)
            { AsyncPartialResult = null; }
        }
        

        public static void RaiseParallelQueriesCompleted()
        {
            if (ParallelQueriesCompleted != null)
            { ParallelQueriesCompleted(null); }

            RaiseAsyncQureyIsCompleted();
        }

        #endregion

        #region Gui

        public static void SignalerReset()
        { Signaler[0].Reset(); }

        public static void SignalAutoReset()
        { Signaler[0].Set(); }

        public static void RaiseDecodingProcess(long current, long overall)
        {
            if (DecodingProcess != null)
            { DecodingProcess(current, new GlobalEventArgs() { LongVal = new long[] { overall } }); }
        }

        public static void RaiseDecodingCompleted()
        {
            if (DecodingCompleted != null)
            { DecodingCompleted(null); }
        }

        #endregion

        #region SettingsHandler

        public static void RaiseSettingChanged(string settingName, object value)
        {
            if (SettingChanged != null)
            { SettingChanged(settingName, new GlobalEventArgs() { ObjectVal = new object[] { value } }); }
        }

        public static void RaiseSettingRead(string settingName, bool value)
        {
            if (SettingRead != null)
            { SettingRead(settingName, new GlobalEventArgs() { BoolVal = new bool[] { value } }); } 
        }

        #endregion

        #region LDAPBrowser

        public static void RaiseGuiBasePathSelected(string path)
        {
            if (GuiBasePathSelected != null)
            { GuiBasePathSelected(path); }
        }

        public static void RaiseGuiBaseObjectSelected(ADObjectInfo objectInfo, bool queryGui)
        {
            if (GuiBaseObjectSelected != null)
            { GuiBaseObjectSelected(objectInfo, new GlobalEventArgs() { BoolVal = new bool[] { queryGui } }); }
        }

        public static void RaiseGuiAsqPathSelected(string path)
        {
            if (GuiAsqPathSelected != null)
            { GuiAsqPathSelected(path, new GlobalEventArgs() { BoolVal = new bool[] { false } }); }
        }

        public static void RaiseWizardPathSelected(string path)
        {
            if (WizardPathSelected != null)
            { WizardPathSelected(path, new GlobalEventArgs() { BoolVal = new bool[] { false } }); }
        }

        #endregion

        #region Connection

        public static void RaiseConnectionSignaled(Credentials credInfo)
        {
            if (ConnectionSignaled != null)
            { ConnectionSignaled(credInfo); }
        }

        #endregion

        #region User

        public static void RaiseCredentialsSignaled(Credentials credInfo)
        {
            if (CredentialsSignaled != null)
            { CredentialsSignaled(credInfo); }
        }

        #endregion

        #region QueryBuilder

        public static void RaiseFilterSignaled(string filterText, bool unLoaded)
        {
            if (FilterSignaled != null)
            { FilterSignaled(filterText, new GlobalEventArgs() { BoolVal = new bool[] { unLoaded } }); }
        }

        #endregion

        #region AttributetypeAssociator

        public static void RaiseDoBackup(string path, bool showText = false)
        {
            if (DoBackup != null)
            { DoBackup(path, new GlobalEventArgs() { BoolVal = new bool[] { showText } }); }
        }

        #endregion

        #region WhoAmI

        public static void RaiseNestingFound(object sender, GlobalEventArgs args)
        {
            if (NestingFound != null)
            { NestingFound(sender, args); }
        }

        public static void RaiseNestingFound(string groupSid, SidInfo memberInfo)
        {
            GlobalEventArgs nargs = new GlobalEventArgs();

            EventValue sid = new EventValue();

            sid.SetDefaultValue<string>(groupSid);

            nargs.Values.Add("group", sid);

            EventValue member = new EventValue();

            member.SetDefaultValue<SidInfo>(memberInfo);

            nargs.Values.Add("member", member);

            RaiseNestingFound(null, nargs);
        }


        #endregion
    }
}
