using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp_SqlProject.Models;

namespace WebApp_SqlProject.Controllers
{
    public class UserController : Controller
    {
        private UserDBContext db = new UserDBContext();
        public ActionResult Creation()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            if(ModelState.IsValid)
            {
                model.DateCreated = DateTime.Now; // setting the date;
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Display");
            }
            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) 
        {
            User model = db.Users.Find(id);
            if(model != null)
            {
                db.Users.Remove(model);
                db.SaveChanges();
            }
            
            return RedirectToAction("Display");
        }

        public ActionResult Display()
        {
            var model = db.Users.ToList();

            return View(model);
        }
        // GET: User/Edit
        public ActionResult Edit(int id)
        {
            User model = db.Users.Find(id);
            if(model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "UserEmail,UserPassword")] User model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(id); // Find the person by ID
                if (user == null)
                {
                    return HttpNotFound(); // Person not found
                }

                // Manually update the properties of the person
                user.UserEmail = model.UserEmail;
                user.UserPassword = model.UserPassword;

    

                db.SaveChanges(); // Save changes to the database
                return RedirectToAction("Display"); // Redirect to the list view, for example
            }

            return View(model); // If model state is not valid, return to the view
        }


    }
}