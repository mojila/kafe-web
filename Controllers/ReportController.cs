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

namespace Report.Controllers
{
    public class ReportController : Controller
    {
        private KafeContext context;

        public ReportController(KafeContext kafeContext) {
            context = kafeContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.tableOrders = new List<TableOrder>();
            ViewBag.users = new List<User>();
            ViewBag.tables = new List<Table>();
            ViewBag.tableOrderItems = new List<TableOrderItem>();
            ViewBag.orders = new List<KafeWeb.Models.Order>();
            ViewBag.menuItems = new List<MenuItem>();

            if (ModelState.IsValid) {
                ViewBag.tableOrders = context.TableOrders.ToList<TableOrder>();
                ViewBag.users = context.Users.ToList<User>();
                ViewBag.tables = context.Tables.ToList<Table>();
                ViewBag.tableOrderItems = context.TableOrderItems.ToList<TableOrderItem>();
                ViewBag.orders = context.Orders.ToList<KafeWeb.Models.Order>();
                ViewBag.menuItems = context.MenuItems.ToList<MenuItem>();
            }

            return View();
        }
    }
}