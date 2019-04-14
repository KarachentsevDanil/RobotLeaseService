using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace RLS.WebApi.Extensions
{
    public static class HttpExtensions
    {
        public static string GetBearerToken(this HttpRequest request)
        {
            var authHeader = request.Headers[HeaderNames.Authorization];
            return authHeader;
        }
    }
}