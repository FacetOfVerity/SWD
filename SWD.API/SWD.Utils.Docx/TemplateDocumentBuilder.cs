using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SWD.Utils.Docx.Attributes;
using SWD.Utils.Docx.Extensions;
using SWD.Utils.Docx.Models;
using TemplateEngine.Docx;

namespace SWD.Utils.Docx
{
    /// <summary>
    /// Конструктор шаблонных docx документов
    /// </summary>
    public class TemplateDocumentBuilder : ITemplateDocumentBuilder, IDisposable
    {
        private readonly Stream _templateStream;
        private readonly Content _documentContent;

        /// <summary/>
        public TemplateDocumentBuilder(Stream templateStream)
        {
            _templateStream = templateStream;
            _documentContent = new Content();
        }

        /// <summary>
        /// Инициализация на основе файла-шаблона
        /// </summary>
        /// <param name="templatePath">Путь к файлу шаблона</param>
        public static TemplateDocumentBuilder Initialize(string templatePath)
        {
            return new TemplateDocumentBuilder(File.Open(templatePath, FileMode.Open, FileAccess.Read));
        }

        /// <summary>
        /// Заполнение текстовых полей шаблона (Content Controls)
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="textFields">Модель, содержащая данные для заполнения текстовых полей
        ///  шаблона (наименование свойств модели должно совпадать c ключами соответствующих Content Controls)</param>
        public ITemplateDocumentBuilder FillTextContent<TModel>(TModel textFields) where TModel : class
        {
            var members = GetModelMembers(typeof(TModel));

            foreach (var member in members)
            {
                var value = member.GetValue<string>(textFields);
                if (value != null)
                    _documentContent.Fields.Add(new FieldContent(member.Name, value));
            }

            return this;
        }

        /// <summary>
        /// Заполнение таблицы шаблона данными
        /// </summary>
        /// <typeparam name="TRow"><see cref="DocumentTable{TRow}"/></typeparam>
        /// <param name="table">Модель табличных данных</param>
        public ITemplateDocumentBuilder FillTableContent<TRow>(DocumentTable<TRow> table) where TRow : class
        {
            var tableContent = ConstructTableContent(table);
            _documentContent.Tables.Add(tableContent);

            return this;
        }

        ///<inheritdoc cref="ITemplateDocumentBuilder.FillListContent{TListItem}"/>
        public ITemplateDocumentBuilder FillListContent<TListItem>(DocumentList<TListItem> list) where TListItem : class
        {
            var listContent = new ListContent(list.ListKey);
            var members = GetModelMembers(typeof(TListItem));

            foreach (var row in list.ListItems)
            {
                var listItems = new List<IContentItem>();
                foreach (var member in members)
                {
                    var value = member.GetValue<string>(row);
                    if (value != null)
                        listItems.Add(new FieldContent(member.Name, value));
                }
                listContent.AddItem(listItems.ToArray());
            }

            _documentContent.Lists.Add(listContent);

            return this;
        }

        ///<inheritdoc cref="ITemplateDocumentBuilder.FillTabelsContent{TRow}"/>
        public ITemplateDocumentBuilder FillTabelsContent<TRow>(DocumentList<DocumentTable<TRow>> tabels)
            where TRow : class
        {
            var listContent = new ListContent(tabels.ListKey);
            foreach (var table in tabels.ListItems)
            {
                var tableContent = ConstructTableContent(table);
                listContent.AddItem(new ListItemContent("TableHeader", table.TableHeader).AddTable(tableContent));
            }

            _documentContent.Lists.Add(listContent);

            return this;
        }

        /// <summary>
        /// Построение документа
        /// </summary>
        public MemoryStream Build()
        {
            var result = new MemoryStream();
            _templateStream.CopyTo(result);

            var outputDocument = new TemplateProcessor(result).SetRemoveContentControls(true);

            outputDocument.FillContent(_documentContent);
            outputDocument.SaveChanges();

            result.Seek(0, SeekOrigin.Begin);

            return result;
        }

        /// <summary>
        /// Получение модели для экспорта в файл
        /// </summary>
        /// <param name="fileName">Имя файла (без расширения)</param>
        public ExportFileModel GetExportFile(string fileName)
        {
            return new ExportFileModel(Build(), $"{fileName}.docx");
        }

        ///<inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            _templateStream.Close();
        }

        private TableContent ConstructTableContent<TRow>(DocumentTable<TRow> table) where TRow : class
        {
            var tableContent = new TableContent(table.TableKey);
            var members = GetModelMembers(typeof(TRow));

            foreach (var row in table.Rows)
            {
                var rowItems = new List<IContentItem>();
                foreach (var member in members)
                {
                    var value = member.GetValue<string>(row);
                    if (value != null)
                        rowItems.Add(new FieldContent(member.Name, value));
                }
                tableContent.AddRow(rowItems.ToArray());
            }

            return tableContent;
        }

        /// <summary>
        /// Получение свойств и статических полей типа
        /// </summary>
        /// <param name="type">Тип</param>
        private MemberInfo[] GetModelMembers(Type type)
        {
            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(member => member.MemberType == MemberTypes.Property || member.MemberType == MemberTypes.Field)
                .Where(a => a.GetCustomAttribute<TemplateIgnoreAttribute>() == null)
                .ToArray();

            return members;
        }
    }
}
