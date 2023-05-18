using MaliLb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MaliLb.Areas.Reader.Controllers
{
    [Area("Visitor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(!User.IsInRole(Roles.Admin))
            {
                return Redirect("/Identity/Account/Login");
            }
            else 
            { 
                return View(); 
            }
        }

        public IActionResult KFULib()
        {
            return Redirect("https://kpfu.ru/library"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}