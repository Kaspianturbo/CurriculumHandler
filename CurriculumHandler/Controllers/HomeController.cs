using ClosedXML.Excel;
using CurriculumHandler.Interfaces;
using CurriculumHandler.Models;
using CurriculumHandler.Services;
using CurriculumHandler.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumHandler.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocReader reader;
        private readonly IDocProcessor processor;
        private static XLWorkbook _book1;
        private static XLWorkbook _book2;
        private static XLWorkbook _book3;
        public HomeController(IDocReader reader, LoadingProcessor processor)
        {
            this.reader = reader;
            this.processor = processor;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Loading()
        {
            return View("Loading");
        }

        public IActionResult Working()
        {
            return View("Working");
        }

        [HttpPost]
        public IActionResult ProcessLoading(SheetPickerModel sheetPickerModel)
        {
            var report = processor.Process(
                _book2, 
                sheetPickerModel.Doc1Sheets.Where(i => i.Selected).Select(i=> i.Text), 
                _book3, 
                sheetPickerModel.Doc2Sheets.Where(i => i.Selected).Select(i => i.Text));
            return View("Report", report);
        }

        [HttpPost]
        public IActionResult ProcessWorking(Files files)
        {
            var bool1 = reader.GetBook(files.File1);
            var bool2 = reader.GetBook(files.File2);
            var sheetPickerModel = new SheetPickerModel
            {
                Data = files,
                Doc1Sheets = reader.GetSheetNames(bool1).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = i, Selected = false }).ToList(),
                Doc2Sheets = reader.GetSheetNames(bool2).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = i, Selected = false }).ToList(),
            };
            return View("SheetPicker", sheetPickerModel);
            //if (files.File1 == null || files.File2 == null)
            //{
            //    return RedirectToAction("Index", "Home", "alert");
            //}
            //var book1 = reader.GetBook(files.File1);
            //var book2 = reader.GetBook(files.File2);

            //var report = processor.Process(book1, book2);
            //report.DocName = files.File2.FileName;
            //return View("Report", report);
        }

        [HttpPost]
        public IActionResult SheetPicker(Files files)
        {
            _book2 = reader.GetBook(files.File2);
            _book3 = reader.GetBook(files.File3);
            var sheetPickerModel = new SheetPickerModel
            {
                Data = files,
                Doc1Sheets = reader.GetSheetNames(_book2).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = i, Selected = false }).ToList(),
                Doc2Sheets = reader.GetSheetNames(_book3).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = i, Selected = false }).ToList(),
            };

            return View("SheetPicker", sheetPickerModel);
        }
    }
}
