using System;
using Newtonsoft.Json;

namespace Test.Model.DTO.Down
{
    public class OriginDownDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
