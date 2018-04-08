using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SWD.Domain;
using SWD.Domain.Models;

namespace SWD.API.Controllers
{
    [Route("Documents")]
    public class DocumentsController : Controller
    {
        private readonly SwaggerDocumentationService _documentationService;


        public DocumentsController(SwaggerDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        /// <summary>
        /// Генерация документа по схеме
        /// </summary>
        /// <param name="jsonScheme">Схема</param>
        [HttpPut]
        public FileStreamResult GetDocument([FromBody] object jsonScheme)
        {
            var result = _documentationService.GenerateDocumentationFile(jsonScheme);

            return File(result.Stream, MediaTypeNames.Application.Octet, result.FileName);
        }
    }
}
