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
            ViewBag.total = 0;
            ViewBag.user = "";
            ViewBag.tables = new List<Table>();

            if (ModelState.IsValid) {
                List<KafeWeb.Models.Menu> menus = context.Menus.ToList<KafeWeb.Models.Menu>();
                List<KafeWeb.Models.Order> orders = context.Orders.Where(d => d.DoneStatus == false).ToList<KafeWeb.Models.Order>();
                List<Table> tables = context.Tables.ToList<Table>();
                tables.ForEach(d => {
                    TableOrder tableOrder = context.TableOrders.Where(e => e.Table.Id == d.Id).FirstOrDefault<TableOrder>();

                    if ( tableOrder != null && tableOrder.DoneStatus == false) {
                        d.UseStatus = true;
                    }
                });
                ViewBag.tables = tables;

                KafeWeb.Models.User user = context.Users.Where(d => d.Username == HttpContext.Session.GetString("username"))
                    .FirstOrDefault<KafeWeb.Models.User>();
                int total = 0;
                menus.ForEach(el => {
                    if (el.Picture == "") {
                        el.Picture = "/images/noimage.png";
                    }
                });

                orders.ForEach(el => {
                    total += el.menu.Price * el.quantity;
                });

                ViewBag.menus = menus;
                ViewBag.orders = orders;
                ViewBag.total = total;
                ViewBag.user = user;
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
        
        [HttpGet("/Menu/CreateOrder/{id}", Name = "Create_Order")]
        public IActionResult CreateOrder(int id) {
            KafeWeb.Models.Menu menu = context.Menus
                .Where(d => d.Id == id).FirstOrDefault<KafeWeb.Models.Menu>();
            KafeWeb.Models.Order order = new KafeWeb.Models.Order();

            order.menu = menu;
            order.quantity = int.Parse(HttpContext.Request.Query["quantity"]);
            order.DoneStatus = false;
            context.Orders.AddRange(order);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("/Menu/CancelOrder/{id}", Name = "Cancel_Order")]        
        public IActionResult CancelOrder(int id) {
            KafeWeb.Models.Order order = context.Orders.Where(d => d.Id == id).FirstOrDefault<KafeWeb.Models.Order>();
            context.Orders.Remove(order);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}