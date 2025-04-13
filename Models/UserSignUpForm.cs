using System.ComponentModel.DataAnnotations;

namespace ASP_One_Examination.Models;

public class UserSignUpForm
{
    [Required(ErrorMessage = "Förnamn krävs")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternamn krävs")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email krävs")]
    [RegularExpression("^\\S+@\\S+\\.\\S+$")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number", Prompt = "Enter your phone number")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Lösenord krävs")]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid password")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bekräfta lösenord")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Godkänn villkoren")]
    [Display(Name = "Terms & Conditions ", Prompt = "I accept the terms & conditions")]
    public bool TermsAndConditions { get; set; }
}
