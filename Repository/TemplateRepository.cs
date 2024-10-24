using ExamForms.Data;
using ExamForms.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamForms.Repository
{
    public class TemplateRepository
    {
        private readonly ExamFormDbContext context;

        public TemplateRepository(ExamFormDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Template>> GetAllTemplate()
        {
            try
            {
                return await context.Templates.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Template>> GetAllTemplateByUserId(string id)
        {
            try
            {
                return await context.Templates.Where(x => x.UserId == id).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Template> GetTemplateById(int id)
        {
            try
            {
                return await context.Templates.Where(x => x.TemplateId == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Topic>> GettAllTopic()
        {
            try
            {
                return await context.Topics.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<string>> GetAllTagName()
        {
            try
            {
                return await context.Tags.Select(t => t.TagName).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Tag>> GetAllTag()
        {
            try
            {
                return await context.Tags.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateTemplate(Template model, List<Tag> tags)
        {
            try
            {
                await context.Templates.AddAsync(model);
                await context.SaveChangesAsync();

                var unavailableTags = tags
                    .Where(tag => !context.Tags.Select(t => t.TagName).ToList()
                        .Contains(tag.TagName))
                    .ToList();

                if (unavailableTags.Any())
                {
                    await context.Tags.AddRangeAsync(unavailableTags);
                    await context.SaveChangesAsync();
                }
                return model.TemplateId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateTemplate(Template model, List<Tag> tags)
        {
            try
            {
                context.Templates.Update(model);
                await context.SaveChangesAsync();

                var unavailableTags = tags
                    .Where(tag => !context.Tags.Select(t => t.TagName).ToList()
                        .Contains(tag.TagName))
                    .ToList();

                if (unavailableTags.Any())
                {
                    await context.Tags.AddRangeAsync(unavailableTags);
                    await context.SaveChangesAsync();
                }
                return model.TemplateId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTemplate(int id)
        {
            try
            {
                Template template = await context.Templates.FindAsync(id);
                if (template != null)
                {
                    context.Templates.Remove(template);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Comment>> GetCommentsByTemplateId(int templateId)
        {
            return await context.Comments
                .Where(c => c.TemplateId == templateId)
                .ToListAsync();
        }

        public async Task<bool> AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<Like>> GetLikesByTemplateId(int templateId)
        {
            return await context.Likes
                .Where(l => l.TemplateId == templateId)
                .ToListAsync();
        }

        public async Task<bool> AddLike(Like like)
        {
            var existingLike = await context.Likes
                .FirstOrDefaultAsync(l => l.TemplateId == like.TemplateId && l.UserId == like.UserId);

            if (existingLike != null)
            {
                return false;
            }

            context.Likes.Add(like);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Template>> MostPopularTemplate()
        {
            var topTemplateIds = await context.Forms.GroupBy(f => f.TemplateId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new
                {
                    TemplateId = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            var templateIdList = topTemplateIds.Select(t => t.TemplateId).ToList();

            return await context.Templates
                .Where(x => templateIdList.Contains(x.TemplateId))
                .ToListAsync();
        }

        public int TemplateCount(int id)
        {
            return context.Forms.Where(x => x.TemplateId == id).Count();
        }
    }
}
