using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kafe.Models;
using Kafe.Data;
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
            return View();
        }

        [HttpPost]
        public string Login(string Username, string Password){
            if (ModelState.IsValid) {
                List<User> users = context.Users.ToList<User>();

                return users.First<User>().Name;
            }

            return "Error database connection";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
