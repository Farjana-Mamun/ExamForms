using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Like
{
    public int LikeId { get; set; }

    public int? TemplateId { get; set; }

    public int? UserId { get; set; }

    public DateTime? LikedAt { get; set; }

    public virtual Template? Template { get; set; }
}
