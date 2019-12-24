using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    public class MenuController : Controller {
        // GET
        public IActionResult Index() {
            return View();
        }   
    }
}