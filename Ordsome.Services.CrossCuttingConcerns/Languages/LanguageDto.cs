using System;
using Newtonsoft.Json;

namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public class LanguageDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("code")]
        public string LanguageCode { get; set; }
        [JsonProperty("name")]
        public string LanguageName { get; set; }
        [JsonProperty("nativeName")]
        public string LanguageNativeName { get; set; }
    }
}