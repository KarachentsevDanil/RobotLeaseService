using Newtonsoft.Json;
using RLS.BLL.DTOs.Users;

namespace RLS.WebApi.Models.Users
{
    public class TokenResponseModel
    {
        public GetUserDto User { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}