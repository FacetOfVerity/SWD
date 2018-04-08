using System.Collections.Generic;
using SWD.Utils.Docx.Attributes;

namespace SWD.Domain.Models
{
    public class ActionDefinition
    {
        public string Url { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        [TemplateIgnore]
        public List<SwaggerProperty> Properties { get; set; }
    }
}
