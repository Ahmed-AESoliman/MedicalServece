using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalServece.Models;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MedicalServece.Models.ModelView;

namespace MedicalServece.Controllers
{
    public class TestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //** All Get Methdes **//
        //GET all Tests Categoryt 
        public ActionResult GetAllTestscategory()
        {
            return View(db.Testscategory.ToList());
        }

        //GET all Tests Categoryt Details with Content
        public ActionResult TestscategoryDetails(int? code)
        {
            var TC = db.Testscategory.Find(code);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(TC);
        }
        //GET all Tests Content(Tests Name)
        public ActionResult GetAllTestsLabContent()
        {
            return View(db.TestsLabContent.ToList());
        }
        //GET all Tests Center
        public ActionResult GetAllTetstCenter()
        {
            return View(db.TestsLab.ToList());
        }

        // Get Tests Center Details
        public ActionResult TestsCenterDetails(int? Code)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestsLab testsLab = db.TestsLab.Find(Code);
            if (testsLab == null)
            {
                return HttpNotFound();
            }
            return View(testsLab);
        }

        //** All Create Metheds **//
        // Create New Tests Categoryt
        public ActionResult CreateTestscategoryt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTestscategoryt(Testscategory Tcat)
        {
            if (ModelState.IsValid)
            {
                db.Testscategory.Add(Tcat);
                db.SaveChanges();
                return RedirectToAction("GetAllTestscategory");
            }
            return View(Tcat);
        }
        // Create New Tests Content
        public ActionResult CreateTetstLabContent()
        {
            ViewBag.Tcategory_Code = new SelectList(db.Testscategory, "Tcategory_Code", "TC_category");
            return View();
        }
        [HttpPost]
        public ActionResult CreateTetstLabContent(TestsLabContent TLC)
        {
            if (ModelState.IsValid)
            {
                db.TestsLabContent.Add(TLC);
                db.SaveChanges();
                return RedirectToAction("GetAllTestsLabContent");
            }

            return View(TLC);

        }

        // Create New Tests Center
        public ActionResult CreateTestsCenter()
        {
            var testsCenter = new TestsLab();
            testsCenter.testsLabContents = new List<TestsLabContent>();
            GetTestsLabContent(testsCenter);
            return View();
        }
        [HttpPost]
        public ActionResult CreateTestsCenter(TestsLab TestsLab, string[] selectedContent)
        {
            if (selectedContent != null)
            {
                TestsLab.testsLabContents = new List<TestsLabContent>();
                foreach (var content in selectedContent)
                {
                    var contentToAdd = db.TestsLabContent.Find(int.Parse(content));
                    TestsLab.testsLabContents.Add(contentToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.TestsLab.Add(TestsLab);
                db.SaveChanges();
                return RedirectToAction("GetAllTestsCenter");
            }
            GetTestsLabContent(TestsLab);
            return View(TestsLab);
        }

        //** All Edit Metheds **//

        // Edit Tests Categoryt
        public ActionResult EditTestscategoryt(int? code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testscategory TCat = db.Testscategory.Find(code);
            if (TCat == null)
            {
                return HttpNotFound();
            }
            return View(TCat);
        }
        [HttpPost]
        public ActionResult EditTestscategoryt(Testscategory TCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllTestscategoryt");
            }
            return View(TCat);
        }
        // Edit Tests Content
        public ActionResult EditTestsLabContent(int? Code)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestsLabContent TLC = db.TestsLabContent.Find(Code);
            if (TLC == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tcategory_Code = new SelectList(db.Testscategory, "Tcategory_Code", "TC_category");
            return View(TLC);
        }
        [HttpPost]
        public ActionResult EditTestsLabContent(TestsLabContent TLC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TLC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllTestsLabContent");
            }
            return View(TLC);
        }
        //Edit Tests Center
        public ActionResult EditTestsCenter(int? Code)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestsLab testsLab = db.TestsLab
                .Include(r => r.testsLabContents)
                .Where(r => r.T_Code == Code)
                .Single();
            GetTestsLabContent(testsLab);
            if (testsLab == null)
            {
                return HttpNotFound();
            }
            return View(testsLab);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTestsCenter(int? Code, string[] selectedContent)
        {
            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var testsLab = db.TestsLab
                .Include(r => r.testsLabContents)
                .Where(r => r.T_Code == Code)
                .Single();
            if (TryUpdateModel(testsLab, "",
                new string[] { "T_Name", "T_Phone", "T_Address" }))
            {
                try
                {
                    UpdateTestsLabContent(selectedContent, testsLab);
                    db.SaveChanges();
                    return RedirectToAction("GetAllTestsCenter");

                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again");
                }
            }
            if (testsLab == null)
            {
                return HttpNotFound();
            }
            GetTestsLabContent(testsLab);
            return View(testsLab);
        }

        //** All Delete Metheds **// 

        //Delete Tests Category

        public ActionResult DeleteTestscategory(int? Code)
        {

            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TCat = db.Testscategory.Find(Code);
            if (TCat == null)
            {
                return HttpNotFound();
            }
            return View(TCat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTestscategory(int Code)
        {
            Testscategory Tcat = db.Testscategory.Find(Code);
            db.Testscategory.Remove(Tcat);
            db.SaveChanges();
            return RedirectToAction("GetAllTestscategoryt");
        }
        //Delete Tests Content

        public ActionResult DeleteTestsLabContent(int? Code)
        {

            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TLC = db.TestsLabContent.Find(Code);
            if (TLC == null)
            {
                return HttpNotFound();
            }
            return View(TLC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTestsLabContent(int Code)
        {
            TestsLabContent TC = db.TestsLabContent.Find(Code);
            db.TestsLabContent.Remove(TC);
            db.SaveChanges();
            return RedirectToAction("GetAllTestsLabContent");
        }

        // Delete Laboratory

        public ActionResult DeleteTestsLab(int? Code)
        {

            if (Code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var TL = db.TestsLab.Find(Code);
            if (TL == null)
            {
                return HttpNotFound();
            }
            return View(TL);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTestsLab(int Code)
        {
            TestsLab TC = db.TestsLab.Find(Code);
            db.TestsLab.Remove(TC);
            db.SaveChanges();
            return RedirectToAction("GetAllTestsCenter");
        }

        // Other Help Methed
        private void GetTestsLabContent(TestsLab testLab)
        {
            var allContent = db.TestsLabContent;
            var centerContent = new HashSet<int>(testLab.testsLabContents.Select(c => c.TC_Code));
            var viewModel = new List<AssignedContentLab>();
            foreach (var content in allContent)
            {
                viewModel.Add(new AssignedContentLab
                {
                    ID = content.TC_Code,
                    Title = content.TC_Content,
                    Assigned = centerContent.Contains(content.TC_Code)
                });
            }
            ViewBag.ContentLab = viewModel;
        }
        private void UpdateTestsLabContent(string[] selectedContent, TestsLab testsLab)
        {
            if (selectedContent == null)
            {
                testsLab.testsLabContents = new List<TestsLabContent>();
                return;
            }

            var selectedContentHS = new HashSet<string>(selectedContent);
            var testsLabContent = new HashSet<int>
                (testsLab.testsLabContents.Select(c => c.TC_Code));
            foreach (var content in db.TestsLabContent)
            {
                if (selectedContentHS.Contains(content.TC_Code.ToString()))
                {
                    if (!testsLabContent.Contains(content.TC_Code))
                    {
                        testsLab.testsLabContents.Add(content);
                    }
                }
                else
                {
                    if (testsLabContent.Contains(content.TC_Code))
                    {
                        testsLab.testsLabContents.Remove(content);
                    }
                }
            }
        }

    }
}