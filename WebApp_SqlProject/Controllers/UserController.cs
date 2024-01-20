using System;
using System.Collections.Generic;
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
                return RedirectToAction("Read");
            }
            return View(model);
        }
        public ActionResult Delete(string email)
        {
            if(email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User model = db.Users.FirstOrDefault(user => user.UserEmail == email);
            if(model == null)
            {
                return HttpNotFound();
            }
            
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string email) 
        {
            User model = db.Users.FirstOrDefault(user => user.UserEmail == email);
            db.Users.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Read");
        }

        public ActionResult Read()
        {
            var model = db.Users.ToList();

            return View(model);
        }

    }
}