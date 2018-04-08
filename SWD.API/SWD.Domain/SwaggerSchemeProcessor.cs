using System;
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
                        var properties = (JArray)method["parameters"];
                        actions.Add(new ActionDefinition
                        {
                            Url = url,
                            Type = type,
                            Description = description,
                            Properties = properties.Select(prop =>
                            {
                                IDictionary<string, ModelProperty> model = null;

                                var schema = prop.SelectToken("schema", false);

                                var @ref = schema?.SelectToken("$ref", false);
                                if (@ref != null)
                                {
                                    var strRef = @ref.ToObject<string>();
                                    var definition = jObject["definitions"][strRef.Split('/')[2]];
                                    model = definition["properties"].ToObject<Dictionary<string, ModelProperty>>();
                                }

                                return new SwaggerProperty
                                {
                                    Name = prop["name"]?.ToString(),
                                    Description = prop["description"]?.ToString(),
                                    Required = Boolean.Parse(prop["required"]?.ToString()),
                                    Source = prop["in"]?.ToString(),
                                    Model = model,
                                    Type = model != null ? "object" : prop["type"]?.ToString()
                                };
                            }).ToList()
                        });
                    }
                }
            }

            return new SwaggerApiModel
            {
                Title = jObject.SelectToken("info", false)?.SelectToken("title")?.Value<string>(),
                Definitions = actions
            };
        }
    }
}
