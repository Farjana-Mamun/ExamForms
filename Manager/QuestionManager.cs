using AutoMapper;
using ExamForms.Constants;
using ExamForms.Models;
using ExamForms.Repository;
using ExamForms.ViewModel;
using System.Security.Principal;

namespace ExamForms.Manager
{
    public class QuestionManager
    {
        private readonly QuestionRepository questionRepository;
        private readonly IMapper mapper;

        public QuestionManager(QuestionRepository questionRepository
            , IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async Task<List<QuestionViewModel>> GetAllQuestionsAsync()
        {
            try
            {
                List<Question> questions = await questionRepository.GetAllQuestions();
                List<QuestionViewModel> results = new List<QuestionViewModel>();
                return mapper.Map<List<QuestionViewModel>>(questions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SaveQuestionOrderAsync(List<QuestionOrderViewModel> questionOrderList)
        {
            try
            {                
                return await questionRepository.SaveQuestionOrder(questionOrderList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddTemplateQuestionAsync(QuestionViewModel model, IIdentity? User)
        {
            try
            {
                Question question = mapper.Map<Question>(model);
                return await questionRepository.AddTemplateQuestion(question);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateTemplateQuestionAsync(QuestionViewModel model, IIdentity? User)
        {
            try
            {
                Question question = mapper.Map<Question>(model);
                return await questionRepository.UpdateTemplateQuestion(question);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTemplateQuestionAsync(int id)
        {
            try
            {
                await questionRepository.DeleteTemplateQuestion(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<QuestionViewModel> GetQuestionByIdAsync(int id)
        {
            try
            {
                QuestionViewModel questionViewModel = new QuestionViewModel();
                var questionOptions = questionViewModel.QuestionOptions;
                Question question = await questionRepository.GetQuestionById(id);
                if (question != null)
                    questionViewModel = mapper.Map<QuestionViewModel>(question);
                questionViewModel.QuestionOptions = questionOptions;
                return questionViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<QuestionViewModel>> GetQuestionsByTemplateIdAsync(int id)
        {
            try
            {
                List<QuestionViewModel> questionViewModel = new List<QuestionViewModel>();
                var questionOptions = questionViewModel.FirstOrDefault()?.QuestionOptions;
                var question = await questionRepository.GetQuestionsByTemplateId(id);
                if (question != null)
                    questionViewModel = mapper.Map<List<QuestionViewModel>>(question);

                foreach (var item in questionViewModel)
                {
                    if (item.QuestionType == Enums.TemplateQuestionTypeEnum.Checkbox.ToString())
                        item.QuestionOptions = questionOptions;
                }
                return questionViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
