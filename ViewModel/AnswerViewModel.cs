using System;
using System.Collections.Generic;

namespace ExamForms.ViewModel;

public partial class AnswerViewModel
{
    public int AnswerId { get; set; }

    public int FormId { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; } = null!;

    public int? AnswerInt { get; set; }

    public virtual FormViewModel Form { get; set; } = null!;
}
