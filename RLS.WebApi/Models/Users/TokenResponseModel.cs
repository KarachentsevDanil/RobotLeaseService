using Newtonsoft.Json;
using RLS.BLL.DTOs.Users;

namespace RLS.WebApi.Models.Users
{
    public class TokenResponseModel
    {
        public GetUserDto User { get; set; }

        [JsonProperty("token")]
        public string AccessToken { get; set; }

        [JsonProperty("tokenExpireData")]
        public int ExpiresIn { get; set; }
    }
}