using MedicalServece.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MedicalServece.Controllers
{
    public class SpecializationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //** Get Metheds **//
        //Get Major Specialization
        public ActionResult GetMajorSpecialization()
        {
            return View(db.MajorSpecialization.ToList());
        }
        //Get Sub Specialization
        public ActionResult GetSubSpecialization()
        {
            return View(db.SubSpecialization.ToList());
        }
        //Get NR
        public ActionResult GetNRSpecialization()
        {
            return View(db.NRSpecialization.ToList());
        }

        // Detiles of Major Specialization
        public ActionResult DetilesSpecialization(int? id)
        {
            var SP = db.MajorSpecialization.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(SP);
        }
        //** All Create Mehtheds
        // Create Major Specialization
        public ActionResult CreateMajorSpecialization()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMajorSpecialization(MajorSpecialization majorSpecialization)
        {
            if (ModelState.IsValid)
            {
                db.MajorSpecialization.Add(majorSpecialization);
                db.SaveChanges();
                return RedirectToAction("GetMajorSpecialization");
            }
            return View(majorSpecialization);
        }
        // Create NR Specialization
        public ActionResult CreateNrSpecialization()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNrSpecialization(NRSpecialization NrSpecialization)
        {
            if (ModelState.IsValid)
            {
                db.NRSpecialization.Add(NrSpecialization);
                db.SaveChanges();
                return RedirectToAction("GetNRSpecialization");
            }
            return View(NrSpecialization);
        }
        //Create sub Specialization
        public ActionResult CreateSubSpecialization()
        {
            ViewBag.Major_ID = new SelectList(db.MajorSpecialization, "Major_ID", "Title");

            return View();
        }
        [HttpPost]
        public ActionResult CreateSubSpecialization(SubSpecialization subSpecialization)
        {
            if (ModelState.IsValid)
            {
                db.SubSpecialization.Add(subSpecialization);
                db.SaveChanges();
                return RedirectToAction("GetSubSpecialization");
            }
            return View(subSpecialization);
        }
        //** Edit Metheds **//
        // Edit Major Specialization
        public ActionResult EditMajorSpecialization(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SP = db.MajorSpecialization.Find(id);
            if (SP==null)
            {
                return HttpNotFound();
            }
            return View(SP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMajorSpecialization(MajorSpecialization majorSpecialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorSpecialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetMajorSpecialization");
            }
            return View(majorSpecialization);
        }
        // Edit Major Specialization
        public ActionResult EditNrSpecialization(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SP = db.NRSpecialization.Find(id);
            if (SP == null)
            {
                return HttpNotFound();
            }
            return View(SP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNrSpecialization(NRSpecialization nrSpecialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nrSpecialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetNRSpecialization");
            }
            return View(nrSpecialization);
        }
        // Edit Sub Specialization
        public ActionResult EditSubSpecialization(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SP = db.SubSpecialization.Find(id);
            if (SP == null)
            {
                return HttpNotFound();
            }
            return View(SP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubSpecialization(SubSpecialization subSpecialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subSpecialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetSubSpecialization");
            }
            return View(subSpecialization);
        }
        //** Delete Metheds **//
        //Deles Major
        public ActionResult DeleteMajorSpecialization(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SP = db.MajorSpecialization.Find(id);
            if (SP == null)
            {
                return HttpNotFound();
            }
            return View(SP);
        }
        [HttpPost]
        public ActionResult DeleteMajorSpecialization(int id)
        {
            MajorSpecialization SP = db.MajorSpecialization.Find(id);
            db.MajorSpecialization.Remove(SP);
            db.SaveChanges();
            return RedirectToAction("GetMajorSpecialization");
        }
        //delete sub
        public ActionResult DeleteSubSpecialization(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var SP = db.SubSpecialization.Find(id);
            if (SP == null)
            {
                return HttpNotFound();
            }
            return View(SP);
        }
        [HttpPost]
        public ActionResult DeleteSubSpecialization(int id)
        {
            SubSpecialization SP = db.SubSpecialization.Find(id);
            db.SubSpecialization.Remove(SP);
            db.SaveChanges();
            return RedirectToAction("GetSubSpecialization");
        }
    }
}