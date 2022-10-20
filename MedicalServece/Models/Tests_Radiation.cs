using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalServece.Models
{
    public class TestsLab
    {
        [Key]
        public int T_Code { get; set; }
        [Required]
        [DisplayName("اسم معمل التحليل")]
        public string T_Name { get; set; }
        [Required]
        [DisplayName("الهاتف")]
        public string T_Phone { get; set; }
        [Required]
        [DisplayName("العنوان")]
        public string T_Address { get; set; }

        public virtual ICollection<TestsLabContent> testsLabContents { get; set; }
    }
    public class TestsLabContent
    {
        [Key]
        public int TC_Code { get; set; }
        [Required]
        [DisplayName("اسم التحليل")]
        public string TC_Content { get; set; }
        public string T_unit { get; set; }
        public string T_refRange { get; set; }
        public int Tcategory_Code { get; set; }
        public virtual Testscategory Tcategory { get; set; }
        public virtual ICollection<TestsLab> testsLab { get; set; }
        public virtual ICollection<ResultFile> resultFile { get; set; }
    }
    public class Testscategory
    {
        [Key]
        public int Tcategory_Code { get; set; }
        [Required]
        [DisplayName("نوع التحليل")]
        public string TC_category { get; set; }
        public virtual ICollection<TestsLabContent> testsLabContent { get; set; }
    }
    public class RadiationLab
    {
        [Key]
        public int R_Code { get; set; }
        [Required]
        [DisplayName("اسم المركز")]
        public string R_Name { get; set; }
        [Required]
        [DisplayName("رقم الهاتف")]
        public string R_Phone { get; set; }
        [Required]
        [DisplayName("العنوان")]
        public string R_Address { get; set; }
        public virtual ICollection<RadiationLabContent> radiationLabContent { get; set; }

    }
    public class RadiationLabContent
    {
        [Key]
        public int RC_Code { get; set; }
        [Required]
        [DisplayName("اسم الاشعه")]
        public string RC_Content { get; set; }
        public int Rcategory_Code { get; set; }
        public virtual Radiationcategory Rcategory{ get; set; }
        public virtual ICollection<RadiationLab> radiationLab { get; set; }
    }
    public class Radiationcategory
    {
        [Key]
        public int Rcategory_Code { get; set; }
        [Required]
        [DisplayName("نوع الاشعه")]
        public string RC_category { get; set; }
        public virtual ICollection<RadiationLabContent> radiationLabContent { get; set; }
    }

}