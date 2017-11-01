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
using Microsoft.AspNet.Identity;

namespace CentricConsulting.Controllers
{
    public class userDetailsController : Controller
    {
        private CentricContext db = new CentricContext();

        // GET: userDetails
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(db.userDetails.ToList());
            }
            else
            {
                return View("NotAuthenticated");
            }
            
        }

        // GET: userDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // GET: userDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,firstName,lastName,PhoneNumber,Office,Position,hireDate")] userDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                userDetails.ID = memberID;
                db.userDetails.Add(userDetails);
                db.SaveChanges();
                return RedirectToAction("Index");


                //userDetails.ID = Guid.NewGuid();
                //db.userDetails.Add(userDetails);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(userDetails);
        }

        // GET: userDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }

            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (userDetails.ID == memberID)
            {
                return View(userDetails);
            }
            else
            {
                return View("NotAllowed");
                //return View("NotAuthenticated");
            }

        }

        // POST: userDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,firstName,lastName,PhoneNumber,Office,Position,hireDate")] userDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        // GET: userDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: userDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            userDetails userDetails = db.userDetails.Find(id);
            db.userDetails.Remove(userDetails);
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
