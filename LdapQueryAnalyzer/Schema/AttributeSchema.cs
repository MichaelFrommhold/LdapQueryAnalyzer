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
    public class AttributeSchema
    {
        #region fields

        public string Name = null;

        public ActiveDirectorySyntax Syntax;
        public bool Checked = false;
        public bool UnkownSyntax = false;
        public bool MatchInChain = false;
        public bool BitWise = false;
        public bool IsConstructed = false;
        public bool IsLinked = false;
        public bool SyntaxFromCache;

        public List<int> ClassIndices = new List<int> { 0 };

        #endregion

        #region constructor

        public AttributeSchema()
        { }

        public AttributeSchema(string ldapName, ActiveDirectorySyntax adSyntax, bool flags, long linkId)
        {
            Name = ldapName;
            Syntax = adSyntax;
            SyntaxFromCache = true;

            IsLinked = (linkId > 0);

            IsConstructed = flags;

            CheckSyntax();
        }

        public AttributeSchema(ActiveDirectorySchemaProperty entry)
        {            
            Name = entry.Name;
            Syntax = entry.Syntax;
            SyntaxFromCache = false;

            CheckSyntax();
        }

        #endregion

        #region methods

        public void UpdateSyntax(ActiveDirectorySyntax adSyntax, bool isBad = false)
        {
            Syntax = adSyntax;
            SyntaxFromCache = false;
            UnkownSyntax = isBad;
        }

        private void CheckSyntax()
        {
            if ((Syntax == ActiveDirectorySyntax.Int) || (Syntax == ActiveDirectorySyntax.Enumeration))
            { BitWise = true; }

            else if ((Syntax == ActiveDirectorySyntax.DN) && (IsLinked))
            { MatchInChain = true; }
        }

        public override string ToString()
        { return Name; }

        #endregion
    }
}
