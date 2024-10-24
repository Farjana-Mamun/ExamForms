using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models.Accounts;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
}
