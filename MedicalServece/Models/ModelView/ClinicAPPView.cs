using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalServece.Models.ModelView
{
    public class ClinicAPPView
    {
        public int id { get; set; }
        public string day { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public bool Assigned { get; set; }
    }
}