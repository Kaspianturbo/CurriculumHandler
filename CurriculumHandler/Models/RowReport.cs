namespace CurriculumHandler.Models
{
    public class RowReport
    {
        public bool Result { get; set; }
        public string? RowName { get; set; }
        public IList<CellReport>? CellReports { get; set; }
    }
}
