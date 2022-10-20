using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalServece.Models
{
    public class RatingDR
    {
        [Key]
        public int ratingID { get; set; }
        public string conment { get; set; }
        public decimal rate { get; set; }
        public DateTime datepost { get; set; }
        public string UserID { get; set; }
        public int C_id { get; set; }
        public virtual Clinic clinic { get; set; }
        public virtual ApplicationUser user { get; set; }


    }
    public class RatingNR
    {
        [Key]
        public int ratingID { get; set; }
        public string conment { get; set; }
        public decimal rate { get; set; }
        public DateTime datepost { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public virtual ApplicationUser user { get; set; }


    }
}