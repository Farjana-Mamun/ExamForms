using AutoMapper;
using ExamForms.Models;
using ExamForms.Repository;
using ExamForms.ViewModel;
using System.Security.Principal;

namespace ExamForms.Manager
{
    public class FormsManager
    {
        private readonly FormsRepository formRepository;
        private readonly QuestionRepository questionRepository;
        private readonly IMapper mapper;

        public FormsManager(FormsRepository formRepository
            , QuestionRepository questionRepository
            , IMapper mapper)
        {
            this.formRepository = formRepository;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async Task<List<FormViewModel>> GetFormsByTemplateIdAsync(int id)
        {
            try
            {
                List<Form> forms = await formRepository.GetFormsByTemplateId(id);
                return mapper.Map<List<FormViewModel>>(forms);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FormViewModel> GetFormByIdAsync(int id, int templateId)
        {
            try
            {
                FormViewModel formViewModel = new FormViewModel();
                Form form = await formRepository.GetFormById(id);
                var questions = await questionRepository.GetQuestionsByTemplateId(templateId);
                formViewModel = mapper.Map<FormViewModel>(form);
                formViewModel.Questions = mapper.Map<List<QuestionViewModel>>(questions);
                return formViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveFormAsync(FormViewModel model, IIdentity User)
        {
            try
            {
                model.SubmittedBy = User.Name;
                model.SubmittedDate = DateTime.Now;
                Form form = mapper.Map<Form>(model);
                return await formRepository.SaveForm(form);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
