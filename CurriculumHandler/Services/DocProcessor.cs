using ClosedXML.Excel;
using CurriculumHandler.Constants;
using CurriculumHandler.Enums;
using CurriculumHandler.Interfaces;
using CurriculumHandler.Models;

namespace CurriculumHandler.Services
{
    public class DocProcessor : IDocProcessor
    {
        public DocReport Process(IXLWorkbook book1, IXLWorkbook book2, IXLWorkbook book3)
        {
            var reportList = new List<RowReport>();
            var sheet1 = book1.Worksheets.Worksheet(2);
            //var sheet1 = book1.Worksheets.Worksheet(2);
            var sheet3 = book3.Worksheets.Worksheet(2);

            //var names1 = SelectNames(sheet1, DocType.Doc1);
            //SelectNames(sheet2, DocType.Doc1);
            var names3 = SelectNames(sheet3, DocType.Doc3).Where(i => !string.IsNullOrEmpty(i));

            var criteriaList = CompareCriteriaSet.GetCriterias;
            foreach (var name in names3)
            {
                var row3 = SelectRow(name, sheet3, DocType.Doc3);
                var row1 = SelectRow(name, sheet1, DocType.Doc1);

                if(row1 != null)
                {
                    var rowReport = ProcessRows(row1, null, row3, criteriaList);
                    rowReport.RowName = name;
                    reportList.Add(rowReport);
                }
            }
            
            return new DocReport { RowReports = reportList, Result = !reportList.Where(i => i.Result == false).Any() };
        }

        private RowReport ProcessRows(IXLRow row1, IXLRow row2, IXLRow row3, IList<AttributeMapping> criteriaList)
        {
            var cellReports = new List<CellReport>();
            foreach (var criteria in criteriaList)
            {
                var cell1 = row1.Cell(criteria.Offset1);
                var cell3 = row3.Cell(criteria.Offset3);
                var result = cell1.Value.ToString() == cell3.Value.ToString();
                var message = result ? $"Значення: \"{cell3.Value}\"" : $"Атрибут: {criteria.AttributeName}. Значення: \"{cell3.Value}\". Очікування: \"{cell1.Value}\".";
                var cellReport = new CellReport 
                {
                    Address = cell3.Address.ToString(),
                    Result = result,
                    Message = message
                };
                cellReports.Add(cellReport);
            }
            return new RowReport
            {
                CellReports = cellReports,
                Result = !cellReports.Where(i => !i.Result).Any()
            };
        }

        private IXLRow SelectRow(string name, IXLWorksheet sheet, DocType type)
        {
            var targetColumnId = type switch
            {
                DocType.Doc1 => 2,
                DocType.Doc2 => 0,
                DocType.Doc3 => 4,
                _ => throw new NotImplementedException(),
            };
            var nameColumn = sheet.Column(targetColumnId);
            var nameCell = nameColumn.Cells(c => c.Value.ToString() == name).FirstOrDefault();
            if (nameCell == null) return null;
            var rowNumber = nameCell.Address.RowNumber;
            return sheet.Row(rowNumber);
        }

        private List<string> SelectNames(IXLWorksheet sheet, DocType type)
        {
            var targetColumnId = type switch
            {
                DocType.Doc1 => 2,
                DocType.Doc2 => 0,
                DocType.Doc3 => 4,
                _ => throw new NotImplementedException(),
            };

            var list = new List<string>();
            var nameColumn = sheet.Column(targetColumnId);
            foreach (var cell in nameColumn.Cells())
            {
                list.Add(cell.GetValue<string>());
            }
            return list;
        }
    }
}
