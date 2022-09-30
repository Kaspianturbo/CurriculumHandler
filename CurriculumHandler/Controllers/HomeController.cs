using CurriculumHandler.Interfaces;
using CurriculumHandler.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumHandler.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocReader reader;
        private readonly IDocProcessor processor;

        public HomeController(IDocReader reader, IDocProcessor processor)
        {
            this.reader = reader;
            this.processor = processor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessFiles(Files files)
        {
            if (files.File1 == null || files.File2 == null || files.File3 == null)
            {
                return RedirectToAction("Index", "Home", "alert");
            }
            var book1 = reader.GetBook(files.File1);
            var book2 = reader.GetBook(files.File2);
            var book3 = reader.GetBook(files.File3);

            var report = processor.Process(book1, book2, book3);
            report.DocName = files.File3.FileName;
            return View("Report", report);
        }
    }
}
