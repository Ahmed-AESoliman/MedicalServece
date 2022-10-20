using MedicalServece.Models;
using MedicalServece.Models.ModelView;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MedicalServece.Controllers
{
    [Authorize]
    public class ClinicController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clinic
        public ActionResult GetClinic()
        {
            var userId = User.Identity.GetUserId();
            List<Clinic> cl = db.Clinic.Where(c => c.UserID == userId).ToList();
            if (cl.Count()>0)
            {
                return View(cl);
            }
            else
            {
                return RedirectToAction("CreateClinic");
            }

        }

        // Create New Clinic
        public ActionResult CreateClinic()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClinic(Clinic clinic)
        {
            
            clinic.UserID = User.Identity.GetUserId();
            db.Clinic.Add(clinic);
            db.SaveChanges();
            
            return RedirectToAction("CreateAppointments");
        }
        // edit clininc data
        public ActionResult EditClinic()
        {
            var ID = User.Identity.GetUserId();
            var cl = db.Clinic.Where(c => c.UserID == ID).SingleOrDefault();
            Clinic clinic = new Clinic();
            clinic.BookingPrice = cl.BookingPrice;
            clinic.BuldingNumber = cl.BuldingNumber;
            clinic.City = cl.City;
            clinic.ClinicArea = cl.ClinicArea;
            clinic.ClinicName = cl.ClinicName;
            clinic.ClinicStreat = cl.ClinicStreat;
            clinic.ClininPhone = cl.ClininPhone;
            clinic.Floor = cl.Floor;
            clinic.SpecialMarque = cl.SpecialMarque;
            return View(clinic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClinic(Clinic clinic)
        {
            var ID = User.Identity.GetUserId();
            var cl = db.Clinic.Where(c => c.UserID == ID).SingleOrDefault();
            
            clinic.BookingPrice = cl.BookingPrice;
            clinic.BuldingNumber = cl.BuldingNumber;
            clinic.City = cl.City;
            clinic.ClinicArea = cl.ClinicArea;
            clinic.ClinicName = cl.ClinicName;
            clinic.ClinicStreat = cl.ClinicStreat;
            clinic.ClininPhone = cl.ClininPhone;
            clinic.Floor = cl.Floor;
            clinic.SpecialMarque = cl.SpecialMarque;
            db.Entry(clinic).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index","Home");

        }
        public ActionResult CreateAppointments()
        {
            ViewBag.days = new SelectList(
                new string[] 
               { "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعه", "السبت" });
            return View();
        }
        [HttpPost]
        public JsonResult AddAppointments(List<Appointments> ap)
        {
            var Cuser = User.Identity.GetUserId();
            var clinic = db.Clinic.Where(c => c.UserID == Cuser).Single();
            foreach (var item in ap)
            {
                item.C_id = clinic.C_Id;
                db.Appointments.Add(item);
            }
            int record=db.SaveChanges();
            return Json(record);
        } 
        public ActionResult GetAppointments()
        {
            var userID = User.Identity.GetUserId();
            var clinic = db.Clinic.Where(c => c.UserID == userID).SingleOrDefault();
            if (clinic!=null)
            {
                List<Appointments> app = db.Appointments.Where(a => a.C_id == clinic.C_Id).ToList();
                return View(app);
            }
            else
            {
              return RedirectToAction("CreateClinic");
            }
        }
        public ActionResult EditAppointments(int? id)
        {
            ViewBag.Day = new SelectList(
new string[]
{ "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعه", "السبت" });
            var ap = db.Appointments.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ap == null)
            {
                return HttpNotFound();
            }
            return View(ap);
        }
        [HttpPost]
        public ActionResult EditAppointments(Appointments ap)
        {
            db.Entry(ap).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetAppointments");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelAppointment(int id)
        {
            Appointments ap = db.Appointments.Find(id);
            db.Appointments.Remove(ap);
            db.SaveChanges();
            return RedirectToAction("GetAppointments");
        }
        public ActionResult GetRating()
        {
            var userId = User.Identity.GetUserId();
            List<RatingNR> rate = db.RatingNR.Where(r => r.UserID == userId).OrderBy(d=>d.datepost).ToList();
            return View(rate);

        }
    }
}