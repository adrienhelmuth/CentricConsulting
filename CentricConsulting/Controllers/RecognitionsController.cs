﻿using System;
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
    public class RecognitionsController : Controller
    {
        private CentricContext db = new CentricContext();

        // GET: Recognitions
        public ActionResult Index(string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                var testusers = from r in db.Recognition select r;
                if (!String.IsNullOrEmpty(searchString))
                {
                    testusers = testusers.Where(r => r.RecognitionComments.Contains(searchString));
                    //|| r.userDetails.Contains(searchString));
                    // if here, users were found so view them
                    return View(testusers.ToList());
                }

                return View(db.Recognition.ToList());
            }
            else
            {
                return View("PleaseLogInToView");
            }

            //var Recognition = db.Recognition.Include(r => r.userDetails).Include(r => r.award).Include(r => r.Giver);

            //if (id != null)
            //{
            //    Recognition = db.Recognition.Where(r=> r.ID == id).Include(r => r.userDetails).Include(r => r.award).Include(r => r.Giver);
            //    ViewBag.Awardee = emp;
            //    var awards = (from aw in Recognition
            //                  group aw by new
            //                  { r = aw.userDetails.ID, a = aw.award } into g
            //                  select new
            //                  { receiverID = g.Key.r, awardID = g.Key.a, AwardCount = g.Count() });
            //    ViewBag.AwardList = awards.ToList();

            //    return View("Awards");
            //}
            //else
            //{
            //    return View(Recognition.ToList());
            //}

            //var recognition = db.Recognition.Include(r => r.Giver).Include(r => r.userDetails);
            //return View(recognition.ToList());
        }

        public ActionResult Awards(Guid? id, string emp)
        {
            var Recognition = db.Recognition.Include(r => r.userDetails).Include(r => r.award).Include(r => r.Giver);

            if (id != null)
            {
                Recognition = db.Recognition.Where(r => r.ID == id).Include(r => r.userDetails).Include(r => r.award).Include(r => r.Giver);
                ViewBag.Awardee = emp;
                var awards = (from aw in Recognition
                              group aw by new
                              { r = aw.userDetails.ID, a = aw.award } into g
                              select new
                              { receiverID = g.Key.r, awardID = g.Key.a, AwardCount = g.Count() });
                ViewBag.AwardList = awards.ToList();

                return View("Awards");
            }
            else
            {
                return View(Recognition.ToList());
            }

        }


        // GET: Recognitions/Details/5
        public ActionResult Details(int? id)
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
        [Authorize]
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
        public ActionResult Create([Bind(Include = "EmployeeRecognitionID,CurentDateTime,RecognitionComments,EmployeeGivingRecog,ID,award")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Recognition.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeGivingRecog = new SelectList(db.userDetails, "ID", "fullName", recognition.EmployeeGivingRecog);
            ViewBag.ID = new SelectList(db.userDetails, "ID", "fullName", recognition.ID);
            return View(recognition);
        }

        // GET: Recognitions/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "EmployeeRecognitionID,CurentDateTime,RecognitionComments,EmployeeGivingRecog,ID,award")] Recognition recognition)
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

        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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

