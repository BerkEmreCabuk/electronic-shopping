using System;

namespace ElectronicShopping.Api.Features.Authenticate.Models
{
    public class TokenModel
    {
        public TokenModel()
        {
            expires_in = TimeSpan.FromDays(1).TotalSeconds.ToString();
            token_type = "Bearer";
        }

        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }
}
