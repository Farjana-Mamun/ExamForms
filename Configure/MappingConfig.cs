using AutoMapper;
using ExamForms.Models;
using ExamForms.Models.Accounts;
using ExamForms.ViewModel;
using ExamForms.ViewModel.Account;

namespace ExamForms.Configure;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        CreateMap<Answer, AnswerViewModel>().ReverseMap();
        CreateMap<Comment, CommentViewModel>().ReverseMap();
        CreateMap<Form, FormViewModel>().ReverseMap();
        CreateMap<Like, LikeViewModel>().ReverseMap();
        CreateMap<Question, QuestionViewModel>().ReverseMap();
        CreateMap<QuestionOption, QuestionOptionViewModel>().ReverseMap();
        CreateMap<Tag, TagViewModel>().ReverseMap();
        CreateMap<Template, TemplateViewModel>().ReverseMap();
        CreateMap<TemplateSpecificUser, TemplateSpecificUserViewModel>().ReverseMap();
        CreateMap<Topic, TopicViewModel>().ReverseMap();
    }
}
