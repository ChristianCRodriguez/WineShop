using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WineShop.Data;
using WineShop.Models;
using Microsoft.AspNetCore.Http;

namespace WineShop.Controllers
{
    public class HomeController : Controller
    {
        private WineShopDBContext db = new WineShopDBContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult WineList()
        {
            List<Items> wineList = db.Items.ToList();
            if (User.Identity.IsAuthenticated)
            {
                Global.walletAmount = db.Wallet.Find(db.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name).Id).WalletAmount;
            }
            return View(wineList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
