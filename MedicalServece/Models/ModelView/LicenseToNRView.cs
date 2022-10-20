using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalServece.Models.ModelView
{
    public class LicenseToNRView
    {
        public string id { get; set; }
        public string FullUserName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string ProfessionalTitle { get; set; }
        public string FullProfessionalTitle { get; set; }
        public string ImageUser { get; set; }
        public string Summray { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Degree { get; set; }
        public string ExYear { get; set; }
        public string MajorSpecialization { get; set; }
        public bool NActive { get; set; }
        public string CriminalFile { get; set; }
        public string BloodTest { get; set; }
        public string Certificate { get; set; }
        public DateTime LicenseDate { get; set; }
    }
}