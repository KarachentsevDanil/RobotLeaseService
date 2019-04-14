using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;

namespace RLS.WebApi.Extensions
{
    public static class HttpExtensions
    {
        public static string GetBearerToken(this HttpRequest request)
        {
            var authHeader = request.Headers[HeaderNames.Authorization].ToString();
            return authHeader.Replace("Bearer", string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();
        }
    }
}