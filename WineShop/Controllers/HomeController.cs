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
using System.Text.Json;
using System.Net.Http;

namespace WineShop.Controllers
{
    public class HomeController : Controller
    {
        private WineShopDBContext db = new WineShopDBContext();
        private JsonDocument jDoc;
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

        //public IActionResult WineList()
        //{
        //    List<Items> wineList = db.Items.ToList();
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        Global.walletAmount = db.Wallet.Find(db.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name).Id).WalletAmount;
        //    }
        //    return View(wineList);
        //}

        public async Task<IActionResult> WineList()
        {
            var tempList = new List<Items>();
            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("https://localhost:44334/api/Wine/GetWineList"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    tempList = JsonSerializer.Deserialize<List<Items>>(stringResponse);
                }
            }
            return View(tempList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
