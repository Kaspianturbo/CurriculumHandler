using ClosedXML.Excel;

namespace CurriculumHandler.Interfaces
{
    public interface IDocReader
    {
        XLWorkbook GetBook(IFormFile file);
    }
}
