using Microsoft.AspNetCore.Mvc;

namespace Student_Course_Management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
