using System.IO;
using SWD.Utils.Docx.Models;

namespace SWD.Utils.Docx
{
    /// <summary>
    /// Конструктор шаблонных документов
    /// </summary>
    public interface ITemplateDocumentBuilder
    {
        /// <summary>
        /// Заполнение текстовых полей шаблона (Content Controls)
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="textFields">Модель, содержащая данные для заполнения текстовых полей
        ///  шаблона (наименование свойств модели должно совпадать c ключами соответствующих Content Controls)</param>
        ITemplateDocumentBuilder FillTextContent<TModel>(TModel textFields) where TModel : class;

        /// <summary>
        /// Заполнение таблицы шаблона данными
        /// </summary>
        /// <typeparam name="TRow"><see cref="DocumentTable{TRow}"/></typeparam>
        /// <param name="table">Модель табличных данных</param>
        ITemplateDocumentBuilder FillTableContent<TRow>(DocumentTable<TRow> table) where TRow : class;

        /// <summary>
        /// Построение документа
        /// </summary>
        MemoryStream Build();

        /// <summary>
        /// Получение модели для экспорта в файл
        /// </summary>
        /// <param name="fileName">Имя файла (без расширения)</param>
        ExportFileModel GetExportFile(string fileName);
    }
}
