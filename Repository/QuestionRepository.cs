using ExamForms.Data;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ExamForms.Repository
{
    public class QuestionRepository
    {
        private readonly ExamFormDbContext context;

        public QuestionRepository(ExamFormDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            try
            {
                return await context.Questions.OrderBy(x => x.DisplayOrder).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SaveQuestionOrder(List<QuestionOrderViewModel> questionOrderList)
        {
            try
            {
                foreach (var item in questionOrderList)
                {
                    var question = await context.Questions.FindAsync(item.QuestionId);
                    if (question != null)
                    {
                        question.DisplayOrder = item.DisplayOrder;
                        context.Questions.Update(question);
                    }
                }
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddTemplateQuestion(Question model)
        {
            try
            {
                await context.Questions.AddAsync(model);
                return await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateTemplateQuestion(Question model)
        {
            try
            {
                context.Questions.Update(model);
                return await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTemplateQuestion(int id)
        {
            try
            {
                Question question = await context.Questions.FindAsync(id);
                if (question != null)
                {
                    context.Questions.Remove(question);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Question> GetQuestionById(int id)
        {
            try
            {
                return await context.Questions.Where(x => x.QuestionId == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Question>> GetQuestionsByTemplateId(int id)
        {
            try
            {
                return await context.Questions.Where(x => x.TemplateId == id).OrderBy(x => x.DisplayOrder).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
