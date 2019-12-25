using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers
{
    public class OrderController : Controller {
        // GET
        public IActionResult Index() {
            return View();
        }   
    }
}