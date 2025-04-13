using System.ComponentModel.DataAnnotations;

namespace ASP_One_Examination.Models;

public class UserSignInForm
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = string.Empty;
}
