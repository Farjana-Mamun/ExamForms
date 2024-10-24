using System;
using System.Collections.Generic;

namespace ExamForms.ViewModel;

public partial class LikeViewModel
{
    public int LikeId { get; set; }

    public int TemplateId { get; set; }

    public int UserId { get; set; }

    public DateTime LikedAt { get; set; }

    public virtual TemplateViewModel Template { get; set; } = null!;
}
