using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SWD.Domain.Models
{
    public class SwaggerProperty
    {
        public string Name { get; set; }

        [JsonProperty("in")]
        public string Source { get; set; }

        public string Format { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }
    }
}
