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
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class SyncCookieRun : SyncBase
    {
        #region fields
        //see SyncBase
        #endregion

        #region constructor

        internal SyncCookieRun(string cookieRunName, string cookieRunPath, byte[] binCookie = null, ReplicationCursorCollection replInfo = null)
        { LoadInternal(cookieRunName, cookieRunPath, binCookie, replInfo); }

        #endregion

        #region methods

        private void LoadInternal(string cookieRunName, string cookieRunPath, byte[] binCookie, ReplicationCursorCollection replInfo)
        {
            CookieName = cookieRunName;

            CookiePath = cookieRunPath;

            CookieDate = Directory.GetCreationTime(CookiePath);

            LoadCookie(binCookie);

            LoadUsn(replInfo);
        }

        private void LoadCookie(byte[] binCookie)
        {
            if (binCookie != null)
            { SerializeCookie(binCookie); }

            else if (GlobalHelper.PathContainsFile(CookiePath, COOKIE_NAME))
            {
                using (FileStream fs = new FileStream(Path.Combine(CookiePath, COOKIE_NAME), FileMode.Open, FileAccess.Read))
                {
                    Cookie = (byte[])new BinaryFormatter().Deserialize(fs);

                    fs.Close();
                }
            }
        }

        private void SerializeCookie(byte[] binCookie)
        {
            Cookie = binCookie;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(CookiePath, FileMode.Create))
                {
                    formatter.Serialize(fs, binCookie);

                    fs.Flush();

                    fs.Close();
                }
            }

            catch (Exception ex)
            { ex.ToDummy(); }
        }

        private void LoadUsn(ReplicationCursorCollection replInfo)
        {
            if (replInfo != null)
            { SerializeUsn(replInfo); }

            else if (GlobalHelper.PathContainsFile(CookiePath, USN_FILE_NAME))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CookiePath, USN_FILE_NAME)))
                {
                    string infoline = String.Empty;

                    while (!sr.EndOfStream)
                    {
                        infoline = sr.ReadLine();

                        UsnVectors.Add((infoline.Split(';'))[0], Convert.ToInt64((infoline.Split(';'))[1]));
                    }

                    sr.Close();
                }
            }
        }

        private void SerializeUsn(ReplicationCursorCollection replInfo)
        {
            using (StreamWriter swriter = new StreamWriter(Path.Combine(CookiePath, USN_FILE_NAME), false))
            {
                foreach (ReplicationCursor replcursor in replInfo)
                {
                    if (!(replcursor.SourceServer == null))
                    {
                        swriter.WriteLine(replcursor.SourceServer + ";" + replcursor.UpToDatenessUsn);
                        UsnVectors.Add(replcursor.SourceServer, replcursor.UpToDatenessUsn);
                    }
                }

                swriter.Close();
            }
        }

        #endregion
    }
}
