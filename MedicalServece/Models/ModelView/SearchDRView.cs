using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalServece.Models.ModelView
{
    public class SearchDRView
    {
        // DR data
        public int? id { get; set; }
        public string FullUserName { get; set; }
        public string Gender { get; set; }
        public string ProfessionalTitle { get; set; }
        public string FullProfessionalTitle { get; set; }
        public string ImageUser { get; set; }
        public string Summray { get; set; }
        public string Country { get; set; }
        public string MajorSpecialization { get; set; }
        public string SubSpecialization { get; set; }
        //public string UserType { get; set; }

        ////Clinic Data
        //public string ClinicName { get; set; }
        //public string ClininPhone { get; set; }
        //public string BookingPrice { get; set; }
        //public string City { get; set; }
        //public string ClinicArea { get; set; }
        //public string ClinicStreat { get; set; }
        //public string BuldingNumber { get; set; }
        //public string Floor { get; set; }
        //public string SpecialMarque { get; set; }

    }
    //public class SearchLogic
    //{
    //    private ApplicationDbContext db;
    //    public SearchLogic()
    //    {
    //        db = new ApplicationDbContext();
    //    }
    //    public IQueryable<ApplicationUser> GetDoctor(SearchDRView DR)
    //    {
    //        var result = db.Users.AsQueryable();
    //        if (DR!=null)
    //        {
    //            if (DR.id.HasValue)
    //            {
    //                result = result.Where(r => r.Id == Convert.ToString(DR.id));
    //            }
    //            if (!string.IsNullOrEmpty(DR.FullUserName))
    //            {
    //                result = result.Where(r => r.FullUserName.Contains(DR.FullUserName));
    //            }
                
    //        }
    //        return result;
    //    }
    //}
}