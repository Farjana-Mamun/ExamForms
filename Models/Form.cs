using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models;

public partial class Form
{
    public int FormId { get; set; }

    public int? TemplateId { get; set; }

    public string? SubmittedBy { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Template? Template { get; set; }
}
