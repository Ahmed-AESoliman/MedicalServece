using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalServece.Models
{
    public class Clinic
    {
        [Key]
        public int C_Id { get; set; }
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string ClininPhone { get; set; }
        [Required]
        public string BookingPrice { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string ClinicArea { get; set; }
        [Required]
        public string ClinicStreat { get; set; }
        [Required]
        public string BuldingNumber { get; set; }
        [Required]
        public string Floor { get; set; }
        [Required]
        public string SpecialMarque { get; set; }
        public virtual ICollection<Appointments> Appointments { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual ICollection<RatingDR> Rating { get; set; }
        [NotMapped]
        public decimal OverAllRating
        {
            get
            {
                if (Rating.Count > 0)
                {
                    return (Rating.Average(r => r.rate));
                }
                return (0);
            }
        }

    }
    public class Appointments
    {
        [Key]
        public int A_id { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string TimeFrom { get; set; }
        [Required]
        public string TimeTo { get; set; }
        public int C_id { get; set; }
        public virtual Clinic Clinic { get; set; }


    }
}