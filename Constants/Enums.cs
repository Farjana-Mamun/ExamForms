using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.Constants;

public static class Enums
{
    public enum AppRoleEnums
    {
        Admin = 1,
        User = 2,
        Anynomous = 3
    }

    public enum TemplateQuestionTypeEnum
    {
        [Display(Name = "Single Line")]
        Single_Line = 1,

        [Display(Name = "Multiple Line")]
        Multiple_Line = 2,

        [Display(Name = "Positive Integer")]
        Positive_Integer = 3,

        [Display(Name = "Checkbox")]
        Checkbox = 4,
    }
}
