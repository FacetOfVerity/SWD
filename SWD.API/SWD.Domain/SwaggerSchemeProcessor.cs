using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWD.Domain.Models;

namespace SWD.Domain
{
    public class SwaggerSchemeProcessor
    {
        private JObject _jObject;

        public SwaggerApiModel GetSchemeModel(object jsonScheme)
        {
            _jObject = JObject.FromObject(jsonScheme);

            var actions = new List<ActionDefinition>();
            foreach (var path in _jObject["paths"].Children())
            {
                var url = ((JProperty) path).Name;
                foreach (var methods in path.Values())
                {
                    foreach (var method in methods)
                    {
                        var type = ((JProperty) methods).Name;
                        var description = method["summary"].Value<string>();
                        var parameters = (JArray)method["parameters"];
                        actions.Add(new ActionDefinition
                        {
                            Url = url,
                            Type = type,
                            Description = description,
                            Properties = parameters.Select(ProcessParameter).ToList()
                        });
                    }
                }
            }

            return new SwaggerApiModel
            {
                Title = _jObject.SelectToken("info", false)?.SelectToken("title")?.Value<string>(),
                Definitions = actions
            };
        }

        private ActionParameter ProcessParameter(JToken parameter)
        {
            IDictionary<string, ModelProperty> model = null;

            var schema = parameter.SelectToken("schema", false);

            var @ref = schema?.SelectToken("$ref", false);
            if (@ref != null)
            {
                var strRef = @ref.ToObject<string>();
                var definition = _jObject["definitions"][strRef.Split('/')[2]];
                model = definition["properties"].ToObject<Dictionary<string, JToken>>()
                    .ToDictionary(a => a.Key, a => ProcessProperty(a.Value));
            }

            return new ActionParameter
            {
                Name = parameter["name"]?.ToString(),
                Description = parameter["description"]?.ToString(),
                Required = Boolean.Parse(parameter["required"]?.ToString()),
                Source = parameter["in"]?.ToString(),
                Model = model,
                Type = model != null ? "object" : parameter["type"]?.ToString()
            };
        }

        private ModelProperty ProcessProperty(JToken prop)
        {
            IDictionary<string, ModelProperty> model = null;

            var @ref = prop.SelectToken("$ref", false);
            if (@ref != null)
            {
                var strRef = @ref.ToObject<string>();
                var definition = _jObject["definitions"][strRef.Split('/')[2]];
                model = definition["properties"].ToObject<Dictionary<string, JToken>>()
                    .ToDictionary(a => a.Key, a => ProcessProperty(a.Value));
            }
            return new ModelProperty
            {
                Description = prop["description"]?.ToString(),
                Model = model,
                Type = model != null ? "object" : prop["type"]?.ToString()
            };
        }
    }
}
