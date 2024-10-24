using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int? FormId { get; set; }

    public int? QuestionId { get; set; }

    public string? AnswerText { get; set; }

    public int? AnswerInt { get; set; }

    public virtual Form? Form { get; set; }
}
