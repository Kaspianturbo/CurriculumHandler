using CurriculumHandler.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurriculumHandler.ViewModel
{
    public class SheetPickerModel
    {
        public Files Files { get; set; }
        public List<SelectListItem> Doc1Sheets { get; set; }
        public List<SelectListItem> Doc2Sheets { get; set; }
        public Files Data { get; set; }
    }
}
