using System;
using System.Text.RegularExpressions;

namespace Puma.Prey.Common.Validation
{
    public static class Validator
    {
        public static string ValidateNameBlackList(string input)
        {
            throw new NotImplementedException();
        }

        public static string ValidateNameWhiteList(string input)
        {
            throw new NotImplementedException();

        }

        public static bool IsValidFileName(string input)
        {
            Regex r = new Regex(@"[A-Za-z0-9]+\.[a-z]{3}");
            return r.IsMatch(input);
        }

    }
}
