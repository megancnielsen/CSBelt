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
    public class HobbiesController : Controller
    {
        private CSBeltContext db;
        private int? uid 
        {
            get 
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }
        public HobbiesController(CSBeltContext context)
        {
            db = context;
        }

        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Hobby> allHobbies = db.Hobbies
            .Include(hobby => hobby.Adds)
            .ToList();

            return View(allHobbies);
        }

        [HttpGet("/New")]
        public IActionResult New()
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("/Create")]
        public IActionResult Create(Hobby newHobby)
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                newHobby.UserId =(int)uid;
                db.Add(newHobby);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("New");
        }

        [HttpGet("/Hobby/{hobbyId}")]
        public IActionResult HobbyDetails(int hobbyId)
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Hobby selectedHobby = db.Hobbies
                .Include(hobby => hobby.Adds)
                .ThenInclude(Add => Add.AddedBy)
                .FirstOrDefault(hobby => hobby.HobbyId == hobbyId);
                

            ViewBag.hobbyId = hobbyId;
            return View("HobbyDetails", selectedHobby);
        }

        [HttpGet("/hobby/{hobbyId}/delete")]
        public IActionResult Delete(int hobbyId)
        {
            Hobby dbHobby = db.Hobbies.FirstOrDefault(hobby => hobby.HobbyId == hobbyId);
            db.Hobbies.Remove(dbHobby);
            db.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost("/Hobbies/{hobbyId}/Add")] // all params must be in url, only simple types
        public IActionResult Add(int hobbyId)
        {

            Add existingAdd = db.Adds.FirstOrDefault(add => add.HobbyId == hobbyId && add.UserId == (int)uid);

            if (existingAdd == null)
            {
                Add newAdd = new Add()
                {
                    HobbyId = hobbyId,
                    UserId = (int)uid,
                };

                db.Adds.Add(newAdd);
            }
            else
            {
                db.Adds.Remove(existingAdd);
            }
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}