using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalServece.Models
{
    public class NCheckUp
    {
        [Key]
        public int N_ID { get; set; }
        [Required]
        public string CriminalFile { get; set; }
        [Required]
        public string BloodTest{ get; set; }
        public string Certificate { get; set; }
        public DateTime LicenseDate { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}