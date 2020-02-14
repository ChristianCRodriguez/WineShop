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
            foreach (var order in db.Orders)
            {
                if(order.UserId == currentUser.Id)
                {
                    orderList.Add(order);
                }
            }

            return View("~/Views/Home/PreviousOrderList.cshtml", orderList);
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