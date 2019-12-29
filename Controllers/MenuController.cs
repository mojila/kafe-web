using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    public class MenuController : Controller {
        [HttpGet]
        public IActionResult Index() {
            ViewBag.orderSuccess = "false";

            if (TempData["orderSuccess"] != null) {
                ViewBag.orderSuccess = "true";
            }

            return View();
        }   
    }
}