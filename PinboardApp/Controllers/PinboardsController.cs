using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PinboardApp.Models;

namespace PinboardApp.Controllers
{
    [Authorize]
    public class PinboardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pinboards
        public ActionResult Index()
        {
            return View(db.Pinboards.ToList());
        }

        // GET: Pinboards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pinboard pinboard = db.Pinboards.Find(id);
            if (pinboard == null)
            {
                return HttpNotFound();
            }
            return View(pinboard);
        }

        // GET: Pinboards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pinboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Pinboard pinboard)
        {
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var user = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Pinboards.Add(pinboard);
                user.Pinboards.Add(pinboard);
                db.SaveChanges();
                return RedirectToAction("Index","Notes");
            }

            return View(pinboard);
        }

        // GET: Pinboards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pinboard pinboard = db.Pinboards.Find(id);
            if (pinboard == null)
            {
                return HttpNotFound();
            }
            return View(pinboard);
        }

        // POST: Pinboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Pinboard pinboard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pinboard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Notes");
            }
            return View(pinboard);
        }

        // GET: Pinboards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pinboard pinboard = db.Pinboards.Find(id);

            if (pinboard == null)
            {
                return HttpNotFound();
            }
            return View(pinboard);
        }

        // POST: Pinboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pinboard pinboard = db.Pinboards.Find(id);
            db.Pinboards.Remove(pinboard);
            db.SaveChanges();
            return RedirectToAction("Index","Notes");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
