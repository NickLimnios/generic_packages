using System.Collections.Generic;

namespace inttrust.restsharp.client
{
    public static class HeadersHelper
    {
        public static Dictionary<string, string> GetHeaders(string token) => new Dictionary<string, string>
        {
            { "Authorization", "Bearer " + token }
        };
    }
}
