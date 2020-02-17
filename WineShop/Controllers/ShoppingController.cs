using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineShop.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WineShop.Controllers
{
    [Authorize]
    public class ShoppingController : Controller
    {
        private WineShopDBContext db = new WineShopDBContext();
        
        private void GetData()
        {
            foreach (var item in db.Items)
            {

            }
            foreach (var status in db.OrderStatuses)
            {

            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuyItem(int item)
        {
            if (db.Items.Find(item) != null)
            {
                if (db.Items.Find(item).Quantity <= 0)
                {
                    return View("~/Views/Shared/OopsError.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/OopsError.cshtml");
            }

            var currentUser = db.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name);

            try
            {
                if (db.Wallet.Find(currentUser.Id).WalletAmount >= db.Items.Find(item).Price)
                {
                    CreateOrder(currentUser, item);
                    return RedirectToAction("WineList", "Home");
                }
                else
                {
                    return View("~/Views/Shared/OopsError.cshtml");
                }
            }
            catch
            {
                return View("~/Views/Shared/OopsError.cshtml");
            }


        }

        public IActionResult PreviousOrderList()
        {
            List<Orders> orderList = new List<Orders>();
            var currentUser = db.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name);
            GetData();
            foreach (var order in db.Orders)
            {
                if(order.UserId == currentUser.Id && order.OrderStatus.OrderStatusId == 1)
                {
                    orderList.Add(order);
                }
            }

            return View("~/Views/Home/PreviousOrderList.cshtml", orderList);
        }

        public IActionResult RefundedOrdersList()
        {
            List<Orders> orderList = new List<Orders>();
            var currentUser = db.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name);
            GetData();
            foreach (var order in db.Orders)
            {
                if (order.UserId == currentUser.Id && order.OrderStatus.OrderStatusId == 2)
                {
                    orderList.Add(order);
                }
            }

            return View("~/Views/Home/RefundedOrderList.cshtml", orderList);
        }

        public IActionResult AddNewItem()
        {
            GetData();
            NewItem newItem = new NewItem();
            newItem.WineCategoryList = db.WineCategories.ToList();
            newItem.WineTypeList = db.WineTypes.ToList();
            return View("~/Views/Home/AddNewItem.cshtml", newItem);
        }

        public IActionResult CreateNewItem(Items newItem)
        {
            try
            {
                Items createItem = new Items()
                {
                    Name = newItem.Name,
                    Description = newItem.Description,
                    Price = newItem.Price,
                    Quantity = newItem.Quantity,
                    WineCategoryId = newItem.WineCategoryId,
                    WineTypeId = newItem.WineTypeId
                };
                db.Items.Add(newItem);
                db.SaveChanges();
                return RedirectToAction("WineList", "Home");
            }
            catch
            {
                return View("~/Views/Shared/OopsError.cshtml");
            }
        }

        public IActionResult VerifyRefund(int order)
        {
            GetData();
            if (db.Orders.Find(order) != null && db.Orders.Find(order).OrderStatusId == 1)
            {
                return View("~/Views/Home/VerifyRefund.cshtml", order);
            }
            else
            {
                return View("~/Views/Shared/OopsError.cshtml");
            }
        }

        public IActionResult RefundOrder(int refundOrder)
        {
            GetData();
            if(db.Orders.Find(refundOrder) != null && db.Orders.Find(refundOrder).OrderStatusId == 1)
            {
                try
                {
                    var currentUser = db.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name);
                    var returningOrder = db.Orders.Find(refundOrder);

                    db.Wallet.Find(currentUser.Id).WalletAmount += returningOrder.OrderTotal;
                    db.Items.Find(returningOrder.ItemId).Quantity += returningOrder.Quantity;
                    db.Orders.Find(refundOrder).OrderStatusId = 2;

                    db.SaveChanges();

                    return RedirectToAction("PreviousOrderList", "Shopping");
                }
                catch
                {
                    return View("~/Views/Shared/OopsError.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/OopsError.cshtml");
            }
        }

        void CreateOrder(AspNetUsers currentUser, int item)
        {
            db.Orders.Add(new Orders
            {
                OrderDate = DateTime.Now,
                OrderStatusId = 1,
                UserId = currentUser.Id,
                ItemId = item,
                Quantity = 1,
                OrderTotal = db.Items.Find(item).Price
            });
            SubtractInventory(item);
            SubtractFunds(currentUser, item);
            db.SaveChanges();

            //HttpContext.Session.SetString("walletAmount", db.Wallet.Find(currentUser.Id).WalletAmount.ToString());
        }

        void SubtractInventory(int item)
        {
            db.Items.Find(item).Quantity -= 1;
        }

        void SubtractFunds(AspNetUsers currentUser, int item)
        {
            db.Wallet.Find(currentUser.Id).WalletAmount -= db.Items.Find(item).Price;
        }
    }
}