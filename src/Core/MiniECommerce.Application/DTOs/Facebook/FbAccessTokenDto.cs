using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.Facebook
{
    public class FbAccessTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }

    //"{\"access_token\":\"1778592152516667|JTCtGB0cJGWB5dTcDE2Bzo6tv2s\",\"token_type\":\"bearer\"}"
}
