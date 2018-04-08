using System;
using SWD.Domain.Models;
using SWD.Utils.Docx;
using SWD.Utils.Docx.Models;

namespace SWD.Domain.DocumentBuilder
{
    public class DocxDocumentBuilder
    {
        private readonly IDocumentBuilderProvider _builderProvider;

        public DocxDocumentBuilder(IDocumentBuilderProvider builderProvider)
        {
            _builderProvider = builderProvider;
        }

        public ExportFileModel BuildDocument(SwaggerApiModel model)
        {
            var builder = _builderProvider.GetBuilder("template");
            var textFields = new TextFieldsModel
            {
                CreationDate = DateTime.Now.ToString("dd.MM.yyyy hh:mm"),
                Title = model.Title
            };

            builder.FillTextContent(textFields);

            return builder.GetExportFile($"{model.Title} documentation");
        }
    }
}
