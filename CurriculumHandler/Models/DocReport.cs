namespace CurriculumHandler.Models
{
    public class DocReport
    {
        public bool Result { get; set; }
        public string? DocName { get; set; }
        public IList<RowReport>? RowReports { get; set; }
    }
}
