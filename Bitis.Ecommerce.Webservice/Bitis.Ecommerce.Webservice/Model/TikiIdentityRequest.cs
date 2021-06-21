using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitis.Ecommerce.Webservice.Model
{
    public interface ITikiResult
    {
        [JsonProperty("statusCode")]
        int? StatusCode { get; set; }
        [JsonProperty("error")]
        string Error { get; set; }
        [JsonProperty("message")]
        string Message { get; set; }
    }
    public class TikiIdentityRequest
    {
        [JsonProperty("clientId")]
        public string Client_ID { get; set; }
        [JsonProperty("clientSecret")]
        public string Client_Secret { get; set; }
    }
    public class TikiIdentityResponse //nhận token tiki trả về
    {
        [JsonProperty("jwt")]
        public string Token { get; set; }
    }
}
