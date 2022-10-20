using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalServece.Models.ModelView
{
    public class DrDetilesView
    {
        public IEnumerable<ApplicationUser> user { get; set; }
        public IEnumerable<RatingDR> rateing { get; set; }
        public IEnumerable<Clinic> clinic { get; set; }
    }
}