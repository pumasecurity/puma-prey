using System;
using System.Collections.Generic;
using System.Linq;
using System.DirectoryServices;

namespace Puma.Prey.Common.Data
{
    public class Ldap
    {
        private const string _SA_USER_ID = "secret value";
        private const string _SA_PASSWORD = "secret password";

        public string FindUser(string username)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://DC=example.com,DC=COM,CN=Users", _SA_USER_ID, _SA_PASSWORD);

            DirectorySearcher searcher = new DirectorySearcher(entry);
            searcher.SearchScope = SearchScope.Subtree;
            searcher.Filter = $"(samaccountname=DOMAIN\\{username})";
            SearchResult result = searcher.FindOne();
            return result?.GetDirectoryEntry()?.Username;
        }
    }
}
