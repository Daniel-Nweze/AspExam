using Microsoft.AspNetCore.Mvc;

namespace ASP_One_Examination.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Projects");
        }
    }
}
