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
            ViewBag.orders = new List<KafeWeb.Models.Order>();
            ViewBag.tableOrderItems = new List<TableOrderItem>();

            if (ModelState.IsValid) {
                ViewBag.tables = context.Tables.ToList<Table>();
                ViewBag.orders = context.Orders.ToList<KafeWeb.Models.Order>();
                ViewBag.tableOrderItems = context.TableOrderItems.ToList<TableOrderItem>();
            }

            if (HttpContext.Session.GetString("username") == null) {
                TempData["isError"] = "true";
                TempData["errorMessage"] = "Mohon login terlebih dahulu.";

                return Redirect("/Home");
            }

            return View();
        }

        [HttpGet("/Order/TableDone/{id}", Name = "Table_Done")]
        public IActionResult TableDone(int id) {
            Table table = context.Tables.Where(d => d.Id == id).FirstOrDefault<Table>();
            table.UseStatus = false;

            context.Tables.Update(table);

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}