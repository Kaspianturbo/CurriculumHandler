using ClosedXML.Excel;
using CurriculumHandler.Models;

namespace CurriculumHandler.Interfaces
{
    public interface IDocProcessor
    {
        DocReport Process(IXLWorkbook book1, IXLWorkbook book2, IXLWorkbook book3);
    }
}
