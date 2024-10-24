using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Template
{
    public int TemplateId { get; set; }
    public string UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? TopicId { get; set; }

    public string? Tags { get; set; }

    public string? Image { get; set; }

    public string? AccessMode { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TemplateSpecificUser> TemplateSpecificUsers { get; set; } = new List<TemplateSpecificUser>();
}
