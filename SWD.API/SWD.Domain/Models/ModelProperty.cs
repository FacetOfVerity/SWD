using System.Collections.Generic;
using SWD.Utils.Docx.Attributes;

namespace SWD.Domain.Models
{
    public class ModelProperty
    {
        public string Type { get; set; }

        public string Description { get; set; }

        [TemplateIgnore]
        public IDictionary<string, ModelProperty> Model { get; set; }
    }
}
