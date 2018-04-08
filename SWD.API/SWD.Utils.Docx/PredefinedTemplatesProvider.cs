using System;
using System.Collections.Generic;

namespace SWD.Utils.Docx
{
    public class PredefinedTemplatesProvider : IDocumentBuilderProvider, IDisposable
    {
        private readonly IDictionary<string, string> _predefinedTemplatesPaths;
        private TemplateDocumentBuilder _builder;

        public PredefinedTemplatesProvider(PredefinedTemplatesOptions options)
        {
            _predefinedTemplatesPaths = options.TemplatesSource;
        }

        public ITemplateDocumentBuilder GetBuilder(string templateIdentifier)
        {
            if (_builder == null)
            {
                _builder = TemplateDocumentBuilder.Initialize(_predefinedTemplatesPaths[templateIdentifier]);
            }

            return _builder;
        }

        public void Dispose()
        {
            _builder.Dispose();
        }
    }
}
