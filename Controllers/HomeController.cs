using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using KafeWeb.Models;
using KafeWeb.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KafeWeb.Controllers
{
    public class HomeController : Controller
    {
        private KafeContext context;
        public HomeController(KafeContext kafeContext)
        {
            context = kafeContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.isError = "false";
            ViewBag.errorMessage = "";

            if (HttpContext.Session.GetString("username") != null) {
                return Redirect("/Menu");
            }

            if (TempData["isError"] != null) {
                ViewBag.isError = TempData["isError"].ToString();
                ViewBag.errorMessage = TempData["errorMessage"].ToString();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password){
            if (ModelState.IsValid) {
                try {
                    User user = context.Users.Where(d => d.Username == Username && d.Password == Password).FirstOrDefault<User>();
                    HttpContext.Session.SetString("username", user.Name);

                    return Redirect("/Menu");
                } catch (NullReferenceException e) {
                    Debug.WriteLine(e);

                    TempData["isError"] = "true";
                    TempData["errorMessage"] = "Username atau Password salah.";
                    return RedirectToAction("Index");
                }
            }

            return Content($"Username: {Username}, Password: {Password}");
        }

        [HttpGet]
        public IActionResult Logout() {
            HttpContext.Session.Remove("username");

            return Redirect("/Home");
        }
    }
}
