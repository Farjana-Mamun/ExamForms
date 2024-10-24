using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? TemplateId { get; set; }

    public string? QuestionType { get; set; }

    public int? SelectedOptionId { get; set; }

    public string? QuestionTitle { get; set; }

    public string? Description { get; set; }

    public bool? IsDisplayed { get; set; }

    public int? DisplayOrder { get; set; }

    //public virtual Template? Template { get; set; }
}
