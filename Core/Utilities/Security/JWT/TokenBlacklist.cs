using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class TokenBlacklist
    {
        private static readonly List<string> _blacklistedTokens = new List<string>();

        public static void AddToken(string token)
        {
            _blacklistedTokens.Add(token);
        }

        public static bool IsTokenBlacklisted(string token)
        {
            return _blacklistedTokens.Contains(token);
        }
    }

}
