using System.Collections.Generic;

namespace SWD.Utils.Docx.Models
{
    /// <summary>
    /// Модель запоняемой таблицы в шаблоне
    /// </summary>
    /// <typeparam name="TRow">Модель кортежа данных для заполняемой таблицы (наименование свойств модели должно совпадать ключами соответствующих Content Controls)</typeparam>
    public class DocumentTable<TRow> where TRow : class
    {
        /// <summary>
        /// Идентификатор таблицы в шаблоне
        /// </summary>
        public string TableKey { get; set; }

        /// <summary>
        /// Заголовок таблицы
        /// </summary>
        public string TableHeader { get; set; }

        /// <summary>
        /// Строки таблицы
        /// </summary>
        public List<TRow> Rows { get; set; }

        public DocumentTable()
        {
            
        }

        /// <summary/>
        public DocumentTable(string tableKey, List<TRow> rows)
        {
            TableKey = tableKey;
            Rows = rows;
        }
    }
}
