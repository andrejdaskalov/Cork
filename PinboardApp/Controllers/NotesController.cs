using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PinboardApp.Models;

namespace PinboardApp.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: Notes
        // lists notes in a pinboard
        public ActionResult Index(int? id)
        {
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var user = db.Users.Find(User.Identity.GetUserId());
            var pinboard = user.Pinboards.FirstOrDefault(p => p.ID == id);
            if (pinboard == null)
            {
                pinboard = user.Pinboards.FirstOrDefault();
            }
            return View(pinboard);
        }

        // // GET: Notes/Details/5
        // public ActionResult Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //     }
        //     Note note = db.Notes.Find(id);
        //     if (note == null)
        //     {
        //         return HttpNotFound();
        //     }
        //     return View(note);
        // }

        // GET: Notes/Create
        public ActionResult Create(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CreateViewModel model = new CreateViewModel {Note = new Note(), PinboardId = id};
            return View(model);
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = db.Users.Find(User.Identity.GetUserId());
                var pinboard = user.Pinboards.FirstOrDefault(p => p.ID == model.PinboardId);
                if (pinboard is null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                model.Note.Pinboard = pinboard;

                //TODO:function to find next available spot to be added
                model.Note.X = "0px";
                model.Note.Y = "0px";

                db.Notes.Add(model.Note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePosition(int? id)
        {
            Note noteToUpdate = db.Notes.Find(id);
            var user = db.Users.Find(User.Identity.GetUserId());
            if (!user.Pinboards.Contains(noteToUpdate.Pinboard))
            {
                return new HttpUnauthorizedResult("You are not authorized to modify this Note.");
            }
            if (noteToUpdate != null)
            {
                Stream req = Request.InputStream;
                req.Seek(0, SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                NotePositionViewModel model = JsonConvert.DeserializeObject<NotePositionViewModel>(json);

                noteToUpdate.X = model.X;
                noteToUpdate.Y = model.Y;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            if (!user.Pinboards.Contains(note.Pinboard))
            {
                return new HttpUnauthorizedResult("You are not authorized to modify this Note.");
            }
            return View(note);
        }

        

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Data,Type,X,Y")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            if (!user.Pinboards.Contains(note.Pinboard))
            {
                return new HttpUnauthorizedResult("You are not authorized to modify this Note.");
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
            db.SaveChanges();
            return RedirectToAction("Index");
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
