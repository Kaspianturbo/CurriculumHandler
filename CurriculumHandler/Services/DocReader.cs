using CurriculumHandler.Interfaces;
using CurriculumHandler.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CurriculumHandler.Services
{
    public class DocReader : IDocReader
    {
        public XLWorkbook GetBook(IFormFile file) => new(file.OpenReadStream());
    }
}
