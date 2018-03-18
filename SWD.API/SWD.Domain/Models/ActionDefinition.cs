using System.Collections.Generic;

namespace SWD.Domain.Models
{
    public class ActionDefinition
    {
        public string Url { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public List<SwaggerProperty> Properties { get; set; }
    }
}
