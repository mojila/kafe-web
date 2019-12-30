using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KafeWeb.Data;
using KafeWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace Order.Controllers
{
    public class OrderController : Controller {
        private KafeContext context;
        
        public OrderController(KafeContext kafeContext) {
            context = kafeContext;
        }

        [HttpGet]
        public IActionResult Index() {
            ViewBag.tables = new List<Table>();

            if (ModelState.IsValid) {
                ViewBag.tables = context.Tables.ToList<Table>();
            }

            if (HttpContext.Session.GetString("username") == null) {
                TempData["isError"] = "true";
                TempData["errorMessage"] = "Mohon login terlebih dahulu.";

                return Redirect("/Home");
            }

            return View();
        }
    }
}