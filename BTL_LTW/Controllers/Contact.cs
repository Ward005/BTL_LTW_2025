using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
