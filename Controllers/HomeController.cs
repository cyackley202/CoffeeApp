using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeAppRevisted.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CoffeeAppRevisted.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            if (_memoryCache.TryGetValue("message", out string message))
            {
                model.Message = message;
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult YouPickSomething()
        {
            return View(new YouPickSomethingViewModel());
        }

        public IActionResult FinalDestination(YouPickSomethingViewModel model)
        {
            _memoryCache.Set("message", model.Name);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}