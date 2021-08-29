using Newtonsoft.Json;

namespace MicroCRM.Models.Token
{
    public class TokenViewModel
    {
        #region Public properties

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public string Role { get; set; }

        #endregion
    }
}
