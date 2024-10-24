using System;
using System.Collections.Generic;

namespace ExamForms.ViewModel;

public partial class FormViewModel
{
    public FormViewModel()
    {
        Questions = new List<QuestionViewModel>();
        Answers = new List<AnswerViewModel>();
    }

    public int FormId { get; set; }

    public int TemplateId { get; set; }

    public string SubmittedBy { get; set; }

    public DateTime SubmittedDate { get; set; }

    public virtual IEnumerable<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    public virtual IEnumerable<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();

    public virtual TemplateViewModel Template { get; set; } = null!;
}
