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
            var doc1 = reader.ReadDoc1();
            var doc2 = reader.ReadDoc2();
            var doc3 = reader.ReadDoc3();

            var report = processor.Process(doc1, doc2, doc3);

            return View("Report", report);
        }
    }
}
