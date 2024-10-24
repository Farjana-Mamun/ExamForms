using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? TemplateId { get; set; }

    public int? UserId { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Template? Template { get; set; }
}
