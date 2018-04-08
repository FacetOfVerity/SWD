using SWD.Domain.DocumentBuilder;
using SWD.Utils.Docx.Models;

namespace SWD.Domain
{
    public class SwaggerDocumentationService
    {
        private readonly SwaggerSchemeProcessor _schemeProcessor;
        private readonly DocxDocumentBuilder _documentBuilder;

        public SwaggerDocumentationService(SwaggerSchemeProcessor schemeProcessor, DocxDocumentBuilder documentBuilder)
        {
            _schemeProcessor = schemeProcessor;
            _documentBuilder = documentBuilder;
        }

        public ExportFileModel GenerateDocumentationFile(object jsonScheme)
        {
            var data = _schemeProcessor.GetSchemeModel(jsonScheme);

            return _documentBuilder.BuildDocument(data);
        }
    }
}
