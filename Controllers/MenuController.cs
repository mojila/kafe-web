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
            ViewBag.menus = new List<MenuItem>();
            ViewBag.orders = new List<KafeWeb.Models.Order>();
            ViewBag.total = 0;
            ViewBag.user = "";
            ViewBag.tables = new List<Table>();

            if (ModelState.IsValid) {
                List<MenuItem> menus = context.MenuItems.ToList<MenuItem>();
                List<KafeWeb.Models.Order> orders = context.Orders.Where(d => d.DoneStatus == false).ToList<KafeWeb.Models.Order>();
                ViewBag.tables = context.Tables.Where(d => d.UseStatus == false).ToList<Table>();

                KafeWeb.Models.User user = context.Users.Where(d => d.Username == HttpContext.Session.GetString("username"))
                    .FirstOrDefault<KafeWeb.Models.User>();
                int total = 0;
                menus.ForEach(el => {
                    if (el.Picture == "") {
                        el.Picture = "/images/noimage.png";
                    }
                });

                orders.ForEach(el => {
                    total += el.MenuItem.Price * el.quantity;
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
            MenuItem menu = context.MenuItems
                .Where(d => d.Id == id).FirstOrDefault<MenuItem>();
            KafeWeb.Models.Order order = new KafeWeb.Models.Order();

            order.MenuItem = menu;
            order.IdMenuItem = menu.Id;
            order.quantity = int.Parse(HttpContext.Request.Query["quantity"]);
            order.DoneStatus = false;

            context.Orders.Add(order);

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

        [HttpGet]
        public IActionResult ConfirmOrder() {
            List<string> orders = HttpContext.Request.Query["orders"].ToString().Split(",").ToList<string>();
            int user = int.Parse(HttpContext.Request.Query["user"]);
            int table = int.Parse(HttpContext.Request.Query["table"]);
            Table selectedTable = context.Tables.Where(e => e.Id == table).FirstOrDefault<Table>();
            selectedTable.UseStatus = true;
            context.Tables.Update(selectedTable);

            TableOrder tableOrder = new TableOrder();
            tableOrder.DoneStatus = false;
            tableOrder.Date = DateTime.Now;
            tableOrder.User = context.Users.Where(e => e.Id == user).FirstOrDefault<User>();
            tableOrder.Table = selectedTable;
            tableOrder.IdTable = selectedTable.Id;

            context.TableOrders.Add(tableOrder);

            var saveTableOrder = context.SaveChangesAsync();

            if (saveTableOrder.IsCompletedSuccessfully) {
                orders.ForEach(d => {
                    KafeWeb.Models.Order order = context.Orders.Where(x => x.Id == int.Parse(d)).FirstOrDefault<KafeWeb.Models.Order>();
                    order.DoneStatus = true;
                    
                    context.Orders.Update(order);

                    var saveOrder = context.SaveChangesAsync();

                    if (saveOrder.IsCompletedSuccessfully) {
                        List<TableOrder> lastTableOrder = context.TableOrders.ToList<TableOrder>();

                        TableOrderItem tableOrderItem = new TableOrderItem();
                        tableOrderItem.Order = order;
                        tableOrderItem.TableOrder = lastTableOrder.Last<TableOrder>();
                        tableOrderItem.IdTableOrder = lastTableOrder.Last<TableOrder>().Id;

                        context.TableOrderItems.Add(tableOrderItem);

                        context.SaveChanges();
                    }
                });
            } 

            return Redirect("/Order");
        }
    }
}