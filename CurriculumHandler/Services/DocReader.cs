using CurriculumHandler.Interfaces;
using ClosedXML.Excel;

namespace CurriculumHandler.Services
{
    public class DocReader : IDocReader
    {
        public XLWorkbook GetBook(IFormFile file) => new(file.OpenReadStream());
        public List<string> GetSheetNames(XLWorkbook book)
        {
            var list = new List<string>();
            foreach(var sheet in book.Worksheets)
            {
                list.Add(sheet.Name);
            }
            return list;
        }
    }
}
