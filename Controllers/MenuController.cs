using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using KafeWeb.Models;
using KafeWeb.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Menu.Controllers
{
    public class MenuController : Controller {
        private KafeContext context;

        public MenuController(KafeContext kafeContext) {
            context = kafeContext;
        }

        [HttpGet]
        public IActionResult Index() {
            ViewBag.orderSuccess = "false";
            ViewBag.menus = new List<KafeWeb.Models.Menu>();
            ViewBag.orders = new List<KafeWeb.Models.Order>();

            if (ModelState.IsValid) {
                List<KafeWeb.Models.Menu> menus = context.Menus.ToList<KafeWeb.Models.Menu>();
                menus.ForEach(el => {
                    if (el.Picture == "") {
                        el.Picture = "/images/noimage.png";
                    }
                });

                ViewBag.menus = menus;
            }

            if (TempData["orderSuccess"] != null) {
                ViewBag.orderSuccess = "true";
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