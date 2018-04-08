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
                CreationDate = DateTime.Now.ToString("dd.MM.yyyy hh:mm"),
                Title = model.Title
            };

            builder.FillTextContent(textFields);

            //var table = new DocumentTable<ActionDefinition>("", model.Definitions);
            //builder.FillTableContent()
            var list = new DocumentList<DocumentTable<SwaggerProperty>>
            {
                ListKey = "Actions",
                ListItems = model.Definitions.Select(action => new DocumentTable<SwaggerProperty>
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
