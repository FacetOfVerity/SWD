using System.Collections.Generic;
using Newtonsoft.Json;
using SWD.Utils.Docx.Attributes;

namespace SWD.Domain.Models
{
    public class ModelProperty
    {
        public string Type { get; set; }

        public string Description { get; set; }

        [TemplateIgnore]
        [JsonProperty("properties")]
        public IDictionary<string, ModelProperty> Model { get; set; }
    }
}
