using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class QuestionOption
{
    public int QuestionOptionId { get; set; }

    public int? QuestionId { get; set; }

    public string? OptionName { get; set; }

    public bool? IsCorrectAnswer { get; set; }

    public virtual Question? Question { get; set; }
}
