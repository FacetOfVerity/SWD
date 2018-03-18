using Microsoft.AspNetCore.Mvc;
using SWD.Domain;
using SWD.Domain.Models;

namespace SWD.API.Controllers
{
    [Route("Documents")]
    public class DocumentsController : Controller
    {
        private readonly SwaggerSchemeProcessor _processor;

        public DocumentsController(SwaggerSchemeProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Генерация документа по схеме
        /// </summary>
        /// <param name="jsonScheme">Схема</param>
        [HttpPut]
        public SwaggerApiModel GetDocument([FromBody] object jsonScheme)
        {
            return _processor.GetSchemeModel(jsonScheme);
        }
    }
}
