using System.IO;

namespace SWD.Utils.Docx.Models
{
    /// <summary>
    /// Модель экспортируемого файла
    /// </summary>
    public class ExportFileModel
    {
        /// <summary>
        /// Данные файла
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Имя файла (с расширением)
        /// </summary>
        public string FileName { get; }

        /// <summary/>
        public ExportFileModel(Stream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
        }
    }
}
