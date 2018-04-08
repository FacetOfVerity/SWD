namespace SWD.Utils.Docx
{
    public interface IDocumentBuilderProvider
    {
        ITemplateDocumentBuilder GetBuilder(string templateAlias);
    }
}
