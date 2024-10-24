using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ExamForms.Constants.Enums;

namespace ExamForms.ViewModel;

public partial class QuestionViewModel
{
    public QuestionViewModel()
    {
        QuestionOptions = new List<QuestionOptionViewModel>
        {
            new QuestionOptionViewModel
            {
                QuestionOptionId = 1,
                OptionName = "Yes",
                IsCorrectAnswer = true
            },
            new QuestionOptionViewModel
            {
                QuestionOptionId = 2,
                OptionName = "No",
                IsCorrectAnswer = false
            }
        };
    }

    public int QuestionId { get; set; }

    public int TemplateId { get; set; }
    [Display(Name = "Question Title")]

    public string QuestionTitle { get; set; } = null!;

    [Display(Name = "Question Type")]
    public string QuestionType { get; set; } = null!;
    public int SelectedOptionId { get; set; }

    public string Description { get; set; } = null!;

    [Display(Name = "Is displayed in the table of the filled-out forms?")]
    public bool IsDisplayed { get; set; }
    public int DisplayOrder { get; set; }

    public TemplateQuestionTypeEnum QuestionTypeEnum { get; set; }
    public virtual IEnumerable<QuestionOptionViewModel> QuestionOptions { get; set; } = new List<QuestionOptionViewModel>();
}
