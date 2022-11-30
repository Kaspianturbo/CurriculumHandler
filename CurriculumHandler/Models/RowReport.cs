using CurriculumHandler.Enums;

namespace CurriculumHandler.Models
{
    public class RowReport
    {
        public RowResult Result { get; set; }
        public string? RowName { get; set; }
        public IList<CellReport>? CellReports { get; set; }
    }
}
