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

using System.IO;
using System.Reflection;

namespace CodingFromTheField.LdapQueryAnalyzer
{
    public class GlobalHelper
    {
        #region file system

        public static string CurrentDirectory { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }

        public static string PathInCurrentDirectory(string value, out bool success) 
        {
            string ret = String.Empty;
            
            success = false;

            ret = Path.Combine(CurrentDirectory, value);

            if (!Directory.Exists(ret))
            {
                try
                { 
                    Directory.CreateDirectory(ret);

                    success = true;
                }

                catch (Exception ex)
                { SetError(String.Format("PathInCurrentDirectory: {0} ({1})", ex.Message, ex.GetType().Name)); }
            }

            return ret;
        }

        public static string PathCombine(string root, string subpath, out bool success) 
        {
            string ret = String.Empty;

            success = false;

            ret = Path.Combine(root, subpath);

            if (!Directory.Exists(ret))
            {
                try
                {
                    Directory.CreateDirectory(ret);

                    success = true;
                }

                catch (Exception ex)
                { SetError(String.Format("PathCombine: {0} ({1})", ex.Message, ex.GetType().Name)); }
            }

            return ret;
        }

        public static Dictionary<string, string> FoldersInPath(string path, string searchPattern = "*")
        {
            Dictionary<string, string> ret = new Dictionary<string, string> { };

            foreach (DirectoryInfo folder in new DirectoryInfo(path).GetDirectories(searchPattern, SearchOption.TopDirectoryOnly))
            { ret.Add(folder.Name, folder.FullName); }

            return ret;
        }

        public static Dictionary<string, string> FilesInPath(string path, string searchPattern = "*")
        {
            Dictionary<string, string> ret = new Dictionary<string, string> { };

            foreach (FileInfo file in new DirectoryInfo(path).GetFiles(searchPattern, SearchOption.TopDirectoryOnly))
            { ret.Add(file.Name, file.FullName); }

            return ret;
        }

        public static FileInfo[] FileInfoInPath(string path, string searchPattern = "*")
        {
            FileInfo[] ret = null;

            ret = new DirectoryInfo(path).GetFiles(searchPattern, SearchOption.TopDirectoryOnly);            

            return ret;
        }

        public static bool PathContainsFile(string path, string searchPattern)
        {
            bool ret = false;

            ret = (Directory.GetFiles(path, searchPattern).Count() != 0);

            return ret;
        }

        public static bool CreateDirectory(string path)
        {
            bool ret = false;

            try
            {
                Directory.CreateDirectory(path);

                ret = true;
            }

            catch (Exception ex)
            { ex.ToDummy(); }

            return ret;
        }

        #endregion

        #region error handling

        public static void SetError(string errorMsg)
        { GlobalEventHandler.RaiseErrorOccured("ERROR " + errorMsg); }

        #endregion
    }
}
