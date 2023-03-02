using Microsoft.AspNetCore.Mvc;

namespace SP2023_Assignment3_lrichardson.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
