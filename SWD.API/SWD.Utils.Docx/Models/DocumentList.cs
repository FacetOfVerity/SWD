using System.Collections.Generic;

namespace SWD.Utils.Docx.Models
{
    /// <summary>
    /// Модель данных, представляемых в документе в виде списка
    /// </summary>
    public class DocumentList<T>
    {
        /// <summary>
        /// Идентификатор списка в шаблоне
        /// </summary>
        public string ListKey { get; set; }
        /// <summary>
        /// Элементы списка
        /// </summary>
        public List<T> ListItems { get; set; }
    }
}
