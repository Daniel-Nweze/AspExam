using ASP_One_Examination.Models;
using Business.Dtos;

namespace ASP_One_Examination.Mappers;

public static class FormMapper
{
    public static SignUpFormData ToDto(this UserSignUpForm form)
    {
        var fullName = form.FullName.Trim().Split(' ', 2); // ChatGpt genererad
        var firstName = fullName[0]; // ChatGpt genererad
        var lastName = fullName.Length > 1 ? fullName[1] : ""; // ChatGpt genererad

        return new SignUpFormData
        {
            FirstName = firstName,
            LastName = lastName,
            Email = form.Email,
            Password = form.Password,
        };
    }

    public static SignInFormData ToDto(this UserSignInForm form) => new()
    {
        Email = form.Email,
        Password = form.Password,
        IsPersistent = form.RememberMe
    };
}
