using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Order.Controllers
{
    public class OrderController : Controller {
        // GET
        public IActionResult Index() {
            if (HttpContext.Session.GetString("username") == null) {
                TempData["isError"] = "true";
                TempData["errorMessage"] = "Mohon login terlebih dahulu.";

                return Redirect("/Home");
            }

            return View();
        }   
    }
}