using System;
using System.Linq;
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
                CreationDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
                Title = model.Title
            };

            builder.FillTextContent(textFields);

            var list = new DocumentList<DocumentTable<ActionParameter>>
            {
                ListKey = "Actions",
                ListItems = model.Definitions.Select(action => new DocumentTable<ActionParameter>
                {
                    TableKey = "PropertyTable",
                    TableHeader = $"{action.Description} {action.Type} {action.Url}",
                    Rows = action.Properties
                }).ToList()
            };
            builder.FillTabelsContent(list);

            return builder.GetExportFile($"{model.Title} documentation");
        }
    }
}
