using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSBelt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

namespace CSBelt.Controllers
{
    public class HomeController : Controller
    {
        private CSBeltContext db;
        private int? uid 
        {
            get 
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }
        public HomeController(CSBeltContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
