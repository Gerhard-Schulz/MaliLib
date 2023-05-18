using Microsoft.AspNetCore.Mvc;

namespace MaliLb.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
