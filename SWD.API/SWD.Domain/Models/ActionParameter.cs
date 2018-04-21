using System.Collections.Generic;
using Newtonsoft.Json;
using SWD.Utils.Docx.Attributes;

namespace SWD.Domain.Models
{
    public class ActionParameter
    {
        public string Name { get; set; }

        [JsonProperty("in")]
        public string Source { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        [TemplateIgnore]
        [JsonProperty("properties")]
        public IDictionary<string, ModelProperty> Model { get; set; }
    }
}
