using ClosedXML.Excel;
using CurriculumHandler.Models;

namespace CurriculumHandler.Interfaces
{
    public interface IDocProcessor
    {
        DocReport Process(IXLWorkbook book1, IEnumerable<string> range1, IXLWorkbook book2, IEnumerable<string> range2);
    }
}
