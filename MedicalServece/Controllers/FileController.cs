using MedicalServece.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MedicalServece.Controllers
{
    public class FileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // file content as img
        // create method
        public ActionResult UploadMFile()
        {
            ViewBag.Cat_file = new SelectList(new[] { "اشاعه", "تحليل", "(تقارير الطبيب(روجته" });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadMFile(List<HttpPostedFileBase> files, FileContent FC)
        {
            ViewBag.Cat_file = new SelectList(new[] { "اشاعه", "تحليل", "(تقارير الطبيب(روجته" });
            var user = User.Identity.GetUserId();
            var Mfile = db.MFile.Where(f => f.UserID == user).SingleOrDefault();
            foreach (var file in files)
            {
                string path = Path.Combine(Server.MapPath("~/FileUpload"), file.FileName);
                file.SaveAs(path);

                //Area for TABLE data insert
                FC.F_id = Mfile.F_id;
                FC.filePath = file.FileName;
                FC.CreateDate = DateTime.Now;
                db.FileContent.Add(FC);
                db.SaveChanges();
            }
            return RedirectToAction("GetMfileAsImg");
        }
                       
        //get list of file as img
        public ActionResult GetMfileAsImg()
        {
            var userId = User.Identity.GetUserId();
            var Mfile = db.MFile.Where(f => f.UserID == userId).SingleOrDefault();
            List<FileContent> fc = db.FileContent.Where(f => f.F_id == Mfile.F_id).OrderBy(o => o.CreateDate).ToList();
            return View(fc);
        }
        //delete method as img
        public ActionResult DelImgMfile(int? id)
        {                         
            var res = db.FileContent.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }
        [HttpPost]
        public ActionResult DelImgMfile(int id)
        {
            FileContent fc = db.FileContent.Find(id);
            db.FileContent.Remove(fc);
            db.SaveChanges();
            return RedirectToAction("GetMfileAsImg");
        }

        //add tests Result as text using only  in tests
        public ActionResult TestsResult()
        {
            ViewBag.TC_Code = new SelectList(db.TestsLabContent.ToList(), "TC_Code", "TC_Content");


            return View();
        }
        public JsonResult AddTestsResult(List<ResultFile> RF)
        {
            var user = User.Identity.GetUserId();
            var Mfile = db.MFile.Where(f => f.UserID == user).SingleOrDefault();
            foreach (ResultFile li in RF)
            {
                li.F_id = Mfile.F_id;
                db.ResultFile.Add(li);
            }
           int insertedRecords = db.SaveChanges();
            return Json(insertedRecords);
        }
        // get all tests Result order by inset date
        public ActionResult TestsResultFile()
        {
            var user = User.Identity.GetUserId();
            var Mfile = db.MFile.Where(u => u.UserID == user).SingleOrDefault();
            List<ResultFile> rf = db.ResultFile.Where(r => r.F_id == Mfile.F_id).OrderBy(o=>o.InserDate).ToList();
            return View(rf);
        }
        //delete method
        public ActionResult DelResultFile(int? id)
        {
            var res = db.ResultFile.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }
        [HttpPost]
        public ActionResult DelResultFile(int id)
        {
            ResultFile res = db.ResultFile.Find(id);
            db.ResultFile.Remove(res);
            db.SaveChanges();
            return RedirectToAction("TestsResultFile");
        }

        //edit Result
        public ActionResult EditResult(int? id)
        {
            var res = db.ResultFile.Find(id);
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (res==null)
            {
                return HttpNotFound();
            }
            ViewBag.TC_Code = new SelectList(db.TestsLabContent.ToList(), "TC_Code", "TC_Content");
            return View(res);
        }
        [HttpPost]
        public ActionResult EditResult(ResultFile res)
        {
            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TestsResultFile");
        }

        //file to Dr
        [Authorize]                         
        [HttpPost]
        public ActionResult SendFileToDR(string id)
        {
            var Dr = db.Users.Find(id);
            var userId = User.Identity.GetUserId();
            FileForDr fd = new FileForDr();
            fd.Pa_URL = userId;
            fd.UserID = Dr.Id;
            fd.SendDate = DateTime.Now;
            db.FileForDr.Add(fd);
            db.SaveChanges();
            return RedirectToAction("DrDetiles","Home",new { id});

        }

        [Authorize]
        //Show Rescever file to DR
        public ActionResult GetFileSended()
        {    
            var dr = User.Identity.GetUserId();
            List<FileForDr> fd = db.FileForDr.Where(f => f.UserID == dr).ToList();
            foreach (var li in fd)
            {
                var day =  (DateTime.Now.Date-li.SendDate.Date).TotalDays;
                if (day>7)
                {
                    db.FileForDr.Remove(li);
                    db.SaveChanges();
                }
            }

            return View(fd);
        }

        [HttpGet]
        public ActionResult pateintDetailes(string url)
        {
            
            if (url !=null)
            {
                return View(db.Users.Where(u=>u.Id==url).SingleOrDefault());
            }
            else
            {
                return HttpNotFound();
            }
        }
        //Show Recever file detailes as img
        [HttpGet]
        public ActionResult DetailesFileSended(string url)
        {
            var dr = User.Identity.GetUserId();
            if (url != null)
            {

            var getURL = db.FileForDr.Where(f => f.Pa_URL == url).Single();
            var Mfile = db.MFile.Where(f => f.UserID == getURL.Pa_URL).SingleOrDefault();
                if (Mfile != null)
                {
                    List<FileContent> fc = db.FileContent.Where(f => f.F_id == Mfile.F_id).OrderBy(o => o.CreateDate).ToList();
                    if (fc != null)
                    {
                        return View(fc);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return RedirectToAction("GetFileSended");
            }

        }
        //show recever file Result
        public ActionResult DetileasResultFile(string url)
        {
            var dr = User.Identity.GetUserId();
            if (url != null)
            {

                var getURL = db.FileForDr.Where(f => f.Pa_URL == url).Single();
                var Mfile = db.MFile.Where(f => f.UserID == getURL.Pa_URL).SingleOrDefault();
                if (Mfile != null)
                {
                    List<ResultFile> fc = db.ResultFile.Where(f => f.F_id == Mfile.F_id).OrderBy(o => o.InserDate).ToList();
                    if (fc != null)
                    {
                        return View(fc);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return RedirectToAction("GetFileSended");
            }
        }

        // end file to Dr


        //send paper to get License
        [HttpGet]
        public ActionResult NurseLicense()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NurseLicense(HttpPostedFileBase CF, HttpPostedFileBase BT, HttpPostedFileBase Ce, NCheckUp NL)
        {           
            
            var user = User.Identity.GetUserId();
            string CFpath = Path.Combine(Server.MapPath("~/FileUpload/NurseLicense"), CF.FileName);
            CF.SaveAs(CFpath);
            string BTpath = Path.Combine(Server.MapPath("~/FileUpload/NurseLicense"), BT.FileName);
            BT.SaveAs(BTpath);

            string Cepath = Path.Combine(Server.MapPath("~/FileUpload/NurseLicense"), Ce.FileName);
            Ce.SaveAs(BTpath);

            NL.UserID = user;
            NL.CriminalFile = CF.FileName;
            NL.BloodTest = BT.FileName;
            NL.Certificate = Ce.FileName;
            NL.LicenseDate = DateTime.Now; 
            db.NCheckUp.Add(NL);
            db.SaveChanges();
            ViewBag.success = "انتظر مراجعه الحساب و رجاء متابعه البريد الالكتروني  الذي تم التسجيل به";
            return RedirectToAction("NRProfile", "Account");
        }
      
    }
}