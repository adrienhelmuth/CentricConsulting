using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentricConsulting.DALContext;
using CentricConsulting.Models;

namespace CentricConsulting.Controllers
{
    public class RecognitionsController : Controller
    {
        private CentricContext db = new CentricContext();

        // GET: Recognitions
        public ActionResult Index()
        {
            var recognition = db.Recognition.Include(r => r.Giver).Include(r => r.UserDetails);
            return View(recognition.ToList());
        }

        // GET: Recognitions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: Recognitions/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeGivingRecog = new SelectList(db.userDetails, "ID", "fullName");
            ViewBag.ID = new SelectList(db.userDetails, "ID", "fullName");
            return View();
        }

        // POST: Recognitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CurentDateTime,RecognitionComments,EmployeeGivingRecog,award")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                recognition.ID = Guid.NewGuid();
                db.Recognition.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeGivingRecog = new SelectList(db.userDetails, "ID", "fullName", recognition.EmployeeGivingRecog);
            ViewBag.ID = new SelectList(db.userDetails, "ID", "fullName", recognition.ID);
            return View(recognition);
        }

        // GET: Recognitions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeGivingRecog = new SelectList(db.userDetails, "ID", "Email", recognition.EmployeeGivingRecog);
            ViewBag.ID = new SelectList(db.userDetails, "ID", "Email", recognition.ID);
            return View(recognition);
        }

        // POST: Recognitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CurentDateTime,RecognitionComments,EmployeeGivingRecog,award")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeGivingRecog = new SelectList(db.userDetails, "ID", "Email", recognition.EmployeeGivingRecog);
            ViewBag.ID = new SelectList(db.userDetails, "ID", "Email", recognition.ID);
            return View(recognition);
        }

        // GET: Recognitions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // POST: Recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Recognition recognition = db.Recognition.Find(id);
            db.Recognition.Remove(recognition);
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
