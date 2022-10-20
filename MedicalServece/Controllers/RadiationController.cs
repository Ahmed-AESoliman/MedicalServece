using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalServece.Models;
using System.Net;
using System.Data.Entity;
using System.Dynamic;
using MedicalServece.Models.ModelView;
using System.Data.Entity.Infrastructure;

namespace MedicalServece.Controllers
{
    public class RadiationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //** All Get Metheds **//
         //GET all Radiation Categoryt 
        public ActionResult GetAllRadiationcategoryt()
        {
            return View(db.Radiationcategory.ToList());
        }

         //GET all Radiation Categoryt Details with Content
        public ActionResult RadiationcategoryDetails(int? code)
        {
            var RC = db.Radiationcategory.Find(code);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(RC);
        }

         //GET all Radiation Content(Radiation Name)
        public ActionResult GetAllRadiationLabContent()
        {
            return View(db.RadiationLabContent.ToList());
        }

         //GET all Radiation Center
        public ActionResult GetAllRadiationCenter()
        {
            return View(db.RadiationLab.ToList());
        }

         // Get Radiation Center Details
        public ActionResult RadiationCenterDetails(int? Code)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadiationLab radiationLab = db.RadiationLab.Find(Code);
            if (radiationLab == null)
            {
                return HttpNotFound();
            }
            return View(radiationLab);
        }

        //** All Create Metheds **//
         // Create New Radiation Categoryt
        public ActionResult CreateRadiationcategoryt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRadiationcategoryt(Radiationcategory Rcat)
        {
            if (ModelState.IsValid)
            {
                db.Radiationcategory.Add(Rcat);
                db.SaveChanges();
                return RedirectToAction("GetAllRadiationcategoryt");
            }
            return View(Rcat);
        }
         // Create New Radiation Content
        public ActionResult CreateRadiationLabContent()
        {
            ViewBag.Rcategory_Code = new SelectList(db.Radiationcategory, "Rcategory_Code", "RC_category");
            return View();
        }
        [HttpPost]
        public ActionResult CreateRadiationLabContent(RadiationLabContent RLC)
        {
            if (ModelState.IsValid)
            {
                db.RadiationLabContent.Add(RLC);
                db.SaveChanges();
                return RedirectToAction("GetAllRadiationLabContent");
            }

            return View(RLC);

        }

         // Create New Radiation Center
        public ActionResult CreateRadiationCenter()
        {
            var radiationCenter = new RadiationLab();
            radiationCenter.radiationLabContent = new List<RadiationLabContent>();
            GetRadiationLabContent(radiationCenter);
            return View();
        }
        [HttpPost]
        public ActionResult CreateRadiationCenter(RadiationLab radiationLab, string[] selectedContent)
        {
            if (selectedContent != null)
            {
                radiationLab.radiationLabContent = new List<RadiationLabContent>();
                foreach (var content in selectedContent)
                {
                    var contentToAdd = db.RadiationLabContent.Find(int.Parse(content));
                    radiationLab.radiationLabContent.Add(contentToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.RadiationLab.Add(radiationLab);
                db.SaveChanges();
                return RedirectToAction("GetAllRadiationCenter");
            }
            GetRadiationLabContent(radiationLab);
            return View(radiationLab);
        }

        //** All Edit Metheds **//

         // Edit Radiation Categoryt
        public ActionResult EditRadiationcategoryt(int? code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiationcategory RCat = db.Radiationcategory.Find(code);
            if (RCat == null)
            {
                return HttpNotFound();
            }
            return View(RCat);
        }
        [HttpPost]
        public ActionResult EditRadiationcategoryt(Radiationcategory RCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(RCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllRadiationcategoryt");
            }
            return View(RCat);
        }


         // Edit Radiation Content
        public ActionResult EditRadiationLabContent(int? Code)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadiationLabContent RLC = db.RadiationLabContent.Find(Code);
            if (RLC == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rcategory_Code = new SelectList(db.Radiationcategory, "Rcategory_Code", "RC_category");
            return View(RLC);
        }
        [HttpPost]
        public ActionResult EditRadiationLabContent(RadiationLabContent RLC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(RLC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllRadiationLabContent");
            }
            return View(RLC);
        }
        //Edit Radiation Center
        public ActionResult EditRadiationCenter(int? Code)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadiationLab radiationLab = db.RadiationLab
                .Include(r => r.radiationLabContent)
                .Where(r => r.R_Code == Code)
                .Single();
            GetRadiationLabContent(radiationLab);
            if (radiationLab == null)
            {
                return HttpNotFound();
            }
            return View(radiationLab);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRadiationCenter(int? Code, string[] selectedContent)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var radiationLab = db.RadiationLab
                .Include(r => r.radiationLabContent)
                .Where(r => r.R_Code == Code)
                .Single();
            if (TryUpdateModel(radiationLab,"",
                new string[] { "R_Name","R_Phone", "R_Address" }))
            {
                try
                {
                    UpdateRadiationLabContent(selectedContent, radiationLab);
                    db.SaveChanges();
                  return RedirectToAction("GetAllRadiationCenter");

                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again");
                }
            }
            if (radiationLab == null)
            {
                return HttpNotFound();
            }
            GetRadiationLabContent(radiationLab);
            return View(radiationLab);
        }

        //** All Delete Metheds **// 

        //Delete Radiation Category

        public ActionResult DeleteRadiationcategory(int? Code)
        {

            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var RCat = db.Radiationcategory.Find(Code);
            if (RCat == null)
            {
                return HttpNotFound();
            }
            return View(RCat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRadiationcategory(int Code)
        {
            Radiationcategory Rcat = db.Radiationcategory.Find(Code);
            db.Radiationcategory.Remove(Rcat);
            db.SaveChanges();
            return RedirectToAction("GetAllRadiationcategoryt");
        }
        //Delete Radiation Content

        public ActionResult DeleteRadiationLabContent(int? Code)
        {

            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var RLC = db.RadiationLabContent.Find(Code);
            if (RLC == null)
            {
                return HttpNotFound();
            }
            return View(RLC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRadiationLabContent(int Code)
        {
            RadiationLabContent RC = db.RadiationLabContent.Find(Code);
            db.RadiationLabContent.Remove(RC);
            db.SaveChanges();
            return RedirectToAction("GetAllRadiationLabContent");
        }

        // Delete Radiation Center

        public ActionResult DeleteRadiationLab(int? Code)
        {

            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var RL = db.RadiationLab.Find(Code);
            if (RL == null)
            {
                return HttpNotFound();
            }
            return View(RL);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRadiationLab(int Code)
        {
            RadiationLab RC = db.RadiationLab.Find(Code);
            db.RadiationLab.Remove(RC);
            db.SaveChanges();
            return RedirectToAction("GetAllRadiationCenter");
        }

        // Other Help Methed
        private void GetRadiationLabContent(RadiationLab radiationLab)
        {
            var allContent = db.RadiationLabContent;
            var centerContent = new HashSet<int>(radiationLab.radiationLabContent.Select(c => c.RC_Code));
            var viewModel = new List<AssignedContentLab>();
            foreach (var content in allContent)
            {
                viewModel.Add(new AssignedContentLab
                {
                    ID = content.RC_Code,
                    Title = content.RC_Content,
                    Assigned = centerContent.Contains(content.RC_Code)
                });
            }
            ViewBag.ContentLab = viewModel;
        }
        private void UpdateRadiationLabContent(string[] selectedContent, RadiationLab radiationLab)
        {
            if (selectedContent == null)
            {
                radiationLab.radiationLabContent = new List<RadiationLabContent>();
                return;
            }

            var selectedContentHS = new HashSet<string>(selectedContent);
            var RadiationLabContent = new HashSet<int>
                (radiationLab.radiationLabContent.Select(c => c.RC_Code));
            foreach (var content in db.RadiationLabContent)
            {
                if (selectedContentHS.Contains(content.RC_Code.ToString()))
                {
                    if (!RadiationLabContent.Contains(content.RC_Code))
                    {
                        radiationLab.radiationLabContent.Add(content);
                    }
                }
                else
                {
                    if (RadiationLabContent.Contains(content.RC_Code))
                    {
                        radiationLab.radiationLabContent.Remove(content);
                    }
                }
            }
        }

    }
}