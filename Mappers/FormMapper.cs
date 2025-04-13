using ASP_One_Examination.Models;
using Business.Dtos;
using System.Runtime.CompilerServices;

namespace ASP_One_Examination.Mappers;

public static class FormMapper
{
    public static SignUpFormData ToDto(this UserSignUpForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,
        Password = form.Password,
    };

    public static SignInFormData ToDto(this UserSignInForm form) => new()
    {
        Email = form.Email,
        Password = form.Password,
        IsPersistent = false
    };
}
