using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalServece.Models
{
    public class MajorSpecialization
    {
        [Key]
        public int Major_ID { get; set; }
        [Required]
        [DisplayName("التخصص الرئيسي")]
        public string Title { get; set; }
        public virtual ICollection<SubSpecialization> subSpecialization { get; set; }

    }
    public class SubSpecialization
    {
        [Key]
        public int Sub_ID { get; set; }
        [Required]
        [DisplayName("التخصص الفرعي")]
        public string Title { get; set; }
        public int Major_ID { get; set; }
        public virtual MajorSpecialization majorSpecialization { get; set; }
    }
    public class NRSpecialization
    {
        [Key]
        public int S_ID { get; set; }
        [Required]
        public string Title { get; set; }
    }

}