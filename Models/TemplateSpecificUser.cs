using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class TemplateSpecificUser
{
    public int TemplateSpecificUserId { get; set; }

    public string? UserId { get; set; }

    public int? TemplateId { get; set; }

    public virtual Template? Template { get; set; }
}
