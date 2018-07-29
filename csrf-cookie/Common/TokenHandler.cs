using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csrf_cookie.Common
{
    public class TokenHandler
    {
        private static IDictionary<string, string> _CSRFToken;

        public TokenHandler()
        {
            if (_CSRFToken == null)
            {
                _CSRFToken = new Dictionary<string, string>();
            }

        }

        public string GenerateCSRFToken(string key)
        {
            string value = Convert.ToBase64String(Encoding.ASCII.GetBytes(key));
            _CSRFToken.Add(key, value);

            return _CSRFToken[key];
        }

        public string GetCSRFToken(string key)
        {
            return _CSRFToken[key];
        }
    }
}
