using ExamForms.Manager;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamForms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly TemplateManager templateManager;

        public HomeController(ILogger<HomeController> logger
            , TemplateManager templateManager)
        {
            this.logger = logger;
            this.templateManager = templateManager;
        }

        public async Task<IActionResult> Index()
        {
            TemplateViewModel model = new TemplateViewModel();
            model.TemplatesByUser = await templateManager.GettAllTemplateAsync();
            model.MostPopularTemplates = await templateManager.MostPopularTemplateAsync();
            model.Tag = await templateManager.GetAllTagAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
