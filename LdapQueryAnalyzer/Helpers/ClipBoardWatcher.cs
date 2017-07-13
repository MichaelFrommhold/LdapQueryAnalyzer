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
using System.Threading;
using System.Windows.Forms;

namespace CodingFromTheField.LdapQueryAnalyzer
{    
    public class ClipBoardWatcher
    {
        #region fields

        private bool StopWatch = false;
        private Int32 Hash = 0;

        #endregion

        #region constructor

        public ClipBoardWatcher()
        { LoadInternal(); }

        #endregion

        #region methods

        public void StopWatching()
        { StopWatch = true; }

        private void LoadInternal()
        { 
            Thread stat = new Thread(new ThreadStart(InternalStartWatching));
            stat.SetApartmentState(ApartmentState.STA);
            stat.Start(); 
        }

        private void InternalStartWatching()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                Thread stat = new Thread(new ThreadStart(InternalStartWatching));
                stat.SetApartmentState(ApartmentState.STA);
                stat.Start();
            }

            else
            {

                while (!StopWatch)
                {
                    try
                    { 
                        bool ret = Clipboard.ContainsText();

                        if (ret)
                        {
                            ret = false;

                            Int32 temp = Clipboard.GetText().GetHashCode();

                            if (Hash != temp)
                            {
                                Hash = temp;

                                ret = true;
                            }
                        }

                        GlobalEventHandler.RaiseClipBoardChanged(ret); 
                    }

                    catch (Exception ex)
                    { ex.ToDummy(); }
                    
                    Thread.Sleep(100);
                }                
            }
        }

        #endregion
    } 
}
