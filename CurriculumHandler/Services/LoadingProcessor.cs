using ClosedXML.Excel;
using CurriculumHandler.Constants;
using CurriculumHandler.Enums;
using CurriculumHandler.Interfaces;
using CurriculumHandler.Models;

namespace CurriculumHandler.Services
{
    public class LoadingProcessor : IDocProcessor
    {
        public DocReport Process(IXLWorkbook book2, IEnumerable<string> range2, IXLWorkbook book3, IEnumerable<string> range3)
        {
            var reportList = new List<RowReport>();
            var sheet3 = book3.Worksheets.Worksheet(2);
            var sheet2 = book2.Worksheets.Worksheet(3);

            var names3 = SelectNames(sheet3, DocType.Doc3).Where(i => !string.IsNullOrEmpty(i));
            
            var criteriaList = CompareCriteriaSet.GetCriterias;
            foreach (var name in names3)
            {
                var row3 = SelectRow(name, sheet3, DocType.Doc3);
                var row2 = SelectRow(name, sheet2, DocType.Doc2);

                RowReport rowReport;

                if (row2 != null)
                {
                    rowReport = ProcessRows(row2, row3, criteriaList);
                }
                else
                {
                    rowReport = new RowReport
                    {
                        Result = RowResult.Skipped,
                        CellReports = new List<CellReport>()
                    };
                }
                rowReport.RowName = name;
                reportList.Add(rowReport);
            }
            return new DocReport { RowReports = reportList, Result = !reportList.Where(i => i.Result == RowResult.Failed).Any() };
        }

        #region Private helpers
        private RowReport ProcessRows(IXLRow row2, IXLRow row3, IList<AttributeMapping> criteriaList)
        {
            var cellReports = new List<CellReport>();
            foreach (var criteria in criteriaList)
            {
                var cell1 = row2.Cell(criteria.Offset2);
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
                Result = !cellReports.Where(i => !i.Result).Any() ? RowResult.Successed : RowResult.Failed
            };
        }

        private IXLRow SelectRow(string name, IXLWorksheet sheet, DocType type)
        {
            var targetColumnId = type switch
            {
                DocType.Doc1 => 2,
                DocType.Doc2 => 2,
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
        #endregion
    }
}
