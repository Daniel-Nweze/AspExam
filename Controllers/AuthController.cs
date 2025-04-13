using ASP_One_Examination.Mappers;
using ASP_One_Examination.Models;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_One_Examination.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;


    public IActionResult SignUp()
    {
        ViewBag.ErrorMessage = "Formuläret är inte korrekt ifyllt.";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(UserSignUpForm form)
    {
        if (!ModelState.IsValid)
            return View(form);


        var dto = form.ToDto();
        var result = await _authService.SignUpAsync(dto);
        if (result.Succeeded)
            return RedirectToAction("SignIn", "Auth");

        ViewBag.ErrorMessage = result.Error;
        return View(form);

    }

    public IActionResult SignIn(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(UserSignInForm form, string returnUrl = "~/")
    {
        if (ModelState.IsValid)
        {
            var dto = form.ToDto();
            var result = await _authService.SignInAsync(dto);
            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Projects");
            }
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Felaktig e-postadress eller lösenord";
        return View(form);
    }

    public new async Task<IActionResult> SignOut()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}


