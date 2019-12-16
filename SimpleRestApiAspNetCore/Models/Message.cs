using System;
using Newtonsoft.Json;

namespace SimpleRestApiAspNetCore.Models
{
    public class Message
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTimeOffset Created { get; set; }

    }
}
