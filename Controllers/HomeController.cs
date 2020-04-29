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
        
        [HttpGet("")]
        public IActionResult Index()
        {
            if (uid != null)
            {
                return RedirectToAction("Dashboard", "Hobbies");
            }
            return View();
        }

        [HttpPost("/register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "is taken");
                    return View("Index");
                }
                if (db.Users.Any(user => user.Username == newUser.Username))
                {
                    ModelState.AddModelError("Username", "is taken");
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("FirstName", newUser.FirstName);
            return RedirectToAction("Dashboard", "Hobbies");
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            User dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", "Try again");
            }
            else
            {
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult passwordCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (passwordCompareResult == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Email or password is incorrect.");
                }
            }

            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            HttpContext.Session.SetString("FirstName", dbUser.FirstName);
            return RedirectToAction("Dashboard", "Hobbies");
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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