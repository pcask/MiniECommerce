using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.Facebook
{
    public class FbAccessTokenValidationDto
    {
        [JsonPropertyName("data")]
        public responseProperties Data { get; set; }
    }

    public class responseProperties
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
    }

    //"{\"data\":{\"app_id\":\"1778592152516667\",\"type\":\"USER\",\"application\":\"Mini E-Commerce\",\"data_access_expires_at\":1677700922,\"expires_at\":1669932000,\"is_valid\":true,\"scopes\":[\"email\",\"public_profile\"],\"user_id\":\"10226366740807193\"}}"
}
