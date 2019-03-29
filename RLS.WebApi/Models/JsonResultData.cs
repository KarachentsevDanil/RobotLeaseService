using Newtonsoft.Json;

namespace RLS.WebApi.Models
{
    public class JsonResultData
    {
        public bool IsSuccess { get; set; } = true;

        public string Data { get; set; }

        public string ErrorMessage { get; set; }

        public static JsonResultData Success(object data = null)
        {
            return new JsonResultData
            {
                Data = JsonConvert.SerializeObject(data ?? string.Empty)
            };
        }

        public static JsonResultData Error(string errorMessage, object errorData = null)
        {
            return new JsonResultData
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}