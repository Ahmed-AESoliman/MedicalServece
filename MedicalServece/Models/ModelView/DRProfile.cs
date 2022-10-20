using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalServece.Models.ModelView
{
    public class DRProfile
    {
        
        [Display(Name = "الاسم بالكامل")]
        public string FullUserName { get; set; }

       
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
      
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime Birthdate { get; set; }
 
        [Display(Name = "الجنس")]
        public string Gender { get; set; }
        [Display(Name = "الدرجه المهنيه")]
        public string ProfessionalTitle { get; set; }//استاذ او اخصائي و استشاري
        [Display(Name = "الاسم المهني")]
        public string FullProfessionalTitle { get; set; }//الاسم المهني بالكامل
        public string ImageUser { get; set; }

        [Display(Name = "نبذه عن ")]
        public string Summray { get; set; }
        [Display(Name = "بلد التخرخ")]
        public string Country { get; set; }
        [Display(Name = "المستوي التعليمي")]
        public string Degree { get; set; }//الدرجه العلميه
        [Display(Name = "سنه التخرج ")]
        public string ExYear { get; set; }//سنه الخبره او التخرج
        [Display(Name = "التخصص الؤئيسي")]
        public string MajorSpecialization { get; set; }
        [Display(Name = "التخصص الفؤعي")]
        public string SubSpecialization { get; set; }
    }
}