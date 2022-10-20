using MedicalServece.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalServece.Models;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MedicalServece.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult searchDR()
        {
            ViewBag.Specialization = new SelectList(db.MajorSpecialization.ToList(),"Title", "Title");
            var result = db.Clinic.ToList();
            return View(result);
        }
        [HttpPost]
        public ActionResult searchDR(string DName)
        {
            ViewBag.Specialization = new SelectList(db.MajorSpecialization.ToList(), "Title", "Title");
            var gender = Request["Gender"];
            var title = Request["Title"];//الاسم المهني
            var specialization = Request["Specialization"];//التخصص الطبي
            var area = Request["Area"];
            if (title == " ")
            {
                title = "استشاري";
            }
            var result = new List<Clinic>();

            if (gender != "all")
            {
                result = (from c in db.Clinic
                          where c.user.FullUserName.Contains(DName) &&
                                 c.user.Gender.Contains(gender) &&
                                 c.ClinicArea.Contains(area) &&
                                 c.user.MajorSpecialization.Contains(specialization) &&
                                 c.user.ProfessionalTitle.Equals(title)
                          select c).ToList();
            }
            else
            {
                result = (from c in db.Clinic
                          where c.user.FullUserName.Contains(DName) &&
                                 c.user.MajorSpecialization.Contains(specialization) ||
                                 c.user.ProfessionalTitle.Equals(title)
                          select c).ToList();
            }

            return View(result);

        }
        [HttpGet]
        public ActionResult DrDetiles(string id)
        {
            var dr = db.Users.Find(id);
            var cl = db.Clinic.Where(c => c.UserID == dr.Id).SingleOrDefault();
            return View(cl);
        }
        [HttpPost]
        //public ActionResult RatingDR(decimal rate,string com, int C_id)
        //{
        //    var user = User.Identity.GetUserId();
        //    var dr = db.Clinic.Find(C_id);
        //    RatingDR rat = new RatingDR();
        //    rat.C_id = C_id;
        //    rat.datepost = DateTime.Now;
        //    rat.UserID = user;
        //    rat.conment = com;
        //    rat.rate = rate;
        //    db.RatingDR.Add(rat);
        //    db.SaveChanges();
        //    return RedirectToAction("DrDetiles","Home",new {id=user });
        //}
        public ActionResult SearchNR()
        {
            ViewBag.Specialization = new SelectList(db.NRSpecialization.ToList(), "Title", "Title");

            var role = db.Roles.Where(r => r.Name == "ممرض").SingleOrDefault();
            var NR = db.Users.Where(u => u.Roles.Select(ro => ro.RoleId).Contains(role.Id)).ToList();
            return View(NR);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchNR(string NRName)
        {
            ViewBag.Specialization = new SelectList(db.NRSpecialization.ToList(), "Title", "Title");
            var gender = Request["Gender"];
            var specialization = Request["Specialization"];//التخصص 
            var area = Request["Area"];
            var result = new List<ApplicationUser>();

            if (gender != "all")
            {
                result = (from c in db.Users
                          where c.FullUserName.Contains(NRName) &&
                                 c.Gender.Contains(gender) &&
                                 c.Area.Contains(area) &&
                                 c.NActive.Equals(true)&&
                                 c.IsActive.Equals(true) &&
                                 c.MajorSpecialization.Contains(specialization) 
                          select c).ToList();
            }
            else
            {
                result = (from c in db.Users
                          where c.FullUserName.Contains(NRName) &&
                                 c.MajorSpecialization.Contains(specialization) 
                          select c).ToList();
            }

            return View(result);

        }
        public ActionResult NrDetiles(string id)
        {
            var NR = db.Users.Where(u=>u.Id==id).SingleOrDefault();
            return View(NR);
        }
        [HttpPost]
        public ActionResult RatingNR(decimal rate, string com, string id)
        {
            string user = User.Identity.GetUserId();
            RatingNR rat = new RatingNR();

            rat.UserName = db.Users.Where(u=>u.Id==user).Single().FullUserName;
            rat.datepost = DateTime.Now;   
            rat.UserID = id;
            rat.conment = com;
            rat.rate = rate;
            db.RatingNR.Add(rat);
            db.SaveChanges();
            return RedirectToAction("NrDetiles", "Home", new { id });
        }
        [HttpGet]
        public ActionResult getRadiationMenu()
        {
            return View(db.Radiationcategory.ToList());
        }
        [HttpGet]
        public  ActionResult ShowLabs(string radiation)
        {
            string query =
                "select r.* from RadiationLabs as r join RadiationLabRadiationLabContents as LC on r.R_Code = LC.RadiationLab_R_Code join RadiationLabContents as c on c.RC_Code = LC.RadiationLabContent_RC_Code "+ 
                "where c.RC_Content ='"+@radiation+"'";
            var center = db.RadiationLab.SqlQuery(query).ToList();
            
            return View(center);
        }
        public ActionResult CenterDetials(int id)
        {
            var center = db.RadiationLab.Where(c => c.R_Code == id).SingleOrDefault();

            return View(center);
        }

        public ActionResult getLabsMenu()
        {
            return View(db.Testscategory.ToList());
        }
        [HttpGet]
        public ActionResult ShowTestLabs(string lab)
        {
            string query =
                "select r.* from TestsLabs as t join TestsLabTestsLabContents as tC on t.T_Code = tC.TestsLab_T_Code join TestsLabContents as c on c.TC_Code = tC.TestsLabContents_TC_Code " +
                "where c.TC_Content ='" + lab + "'";
            var center = db.TestsLab.SqlQuery(query).ToList();

            return View(center);
        }
        public ActionResult LabsDetials(int id)
        {
            var center = db.TestsLab.Where(c => c.T_Code == id).SingleOrDefault();

            return View(center);
        }
        //public ActionResult CRoom()
        //{

        //}


    }
}