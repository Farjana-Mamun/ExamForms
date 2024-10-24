using ExamForms.Constants;
using ExamForms.Manager;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExamForms.Areas.Templates.Controllers
{
    [Authorize]
    [Area(nameof(Templates))]
    public class QuestionController : Controller
    {
        private readonly QuestionManager questionManager;

        public QuestionController(QuestionManager questionManager)
        {
            this.questionManager = questionManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SaveQuestionModal(int questionId, int templateId)
        {
            QuestionViewModel model = new QuestionViewModel();
            model = await questionManager.GetQuestionByIdAsync(questionId);
            return PartialView("~/Areas/Templates/Views/Shared/_TemplateQuestionAddModal.cshtml", model);
        }

        public IActionResult AddQuestionCheckbox(int questionType)
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            return PartialView("~/Areas/Templates/Views/Shared/_AddQuestionCheckboxModal.cshtml", questionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTemplateQuestion(QuestionViewModel model)
        {
            if (model.QuestionId == 0)
                model.QuestionId = await questionManager.AddTemplateQuestionAsync(model, User.Identity);
            else
                model.QuestionId = await questionManager.UpdateTemplateQuestionAsync(model, User.Identity);

            List<QuestionViewModel> questions = new List<QuestionViewModel>();
            questions = await questionManager.GetQuestionsByTemplateIdAsync(model.TemplateId);

            return PartialView("~/Areas/Templates/Views/Shared/_TemplateQuestionList.cshtml", questions);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTemplateQuestion(int questionId)
        {
            await questionManager.DeleteTemplateQuestionAsync(questionId);

            List<QuestionViewModel> questions = new List<QuestionViewModel>();
            questions = await questionManager.GetAllQuestionsAsync();

            return PartialView("~/Areas/Templates/Views/Shared/_TemplateQuestionList.cshtml", questions);
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuestionOrder([FromBody] List<QuestionOrderViewModel> questionOrderList)
        {
            await questionManager.SaveQuestionOrderAsync(questionOrderList);
            return Json(new { success = true });
        }
    }
}
