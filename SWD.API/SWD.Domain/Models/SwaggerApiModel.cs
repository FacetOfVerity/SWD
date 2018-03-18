using System.Collections.Generic;

namespace SWD.Domain.Models
{
    public class SwaggerApiModel
    {
        public string Title { get; set; }

        public List<ActionDefinition> Definitions { get; set; }
    }
}
