using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWD.Domain.Models;

namespace SWD.Domain
{
    public class SwaggerSchemeProcessor
    {
        public SwaggerApiModel GetSchemeModel(object jsonScheme)
        {
            var jObject = JObject.FromObject(jsonScheme);

            var actions = new List<ActionDefinition>();
            foreach (var path in jObject["paths"].Children())
            {
                var url = ((JProperty) path).Name;
                foreach (var methods in path.Values())
                {
                    foreach (var method in methods)
                    {
                        var type = ((JProperty) methods).Name;
                        var description = method["summary"].Value<string>();
                        var properties = method["parameters"];
                        actions.Add(new ActionDefinition
                        {
                            Url = url,
                            Type = type,
                            Description = description,
                            Properties = properties.ToObject<List<SwaggerProperty>>()
                        });
                    }
                }
            }

            return new SwaggerApiModel
            {
                Title = jObject["info"]["title"].Value<string>(),
                Definitions = actions
            };
        }
    }
}
