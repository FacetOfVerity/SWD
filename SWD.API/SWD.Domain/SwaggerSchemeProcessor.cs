using Newtonsoft.Json.Linq;
using SWD.Domain.Models;

namespace SWD.Domain
{
    public class SwaggerSchemeProcessor
    {
        public SwaggerApiModel GetSchemeModel(object jsonScheme)
        {
            var jObject = JObject.FromObject(jsonScheme);

            return new SwaggerApiModel
            {
                Title = jObject["info"]["title"].Value<string>()
            };
        }
    }
}
