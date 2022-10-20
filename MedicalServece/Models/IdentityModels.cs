using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MedicalServece.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
        public string SubSpecialization { get; set; }
        public bool NActive { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Clinic> Clinic { get; set; }
        public virtual ICollection<RatingDR> Rating { get; set; }
        public virtual ICollection<RatingNR> RatingNR { get; set; }
         public virtual ICollection<FileForDr> FileForDr { get; set; }

        public virtual ICollection<NCheckUp> NCheckUp { get; set; }
        [NotMapped]
        public decimal OverAllRating
        {
            get
            {
                if (RatingNR.Count > 0)
                {
                    return (RatingNR.Average(r => r.rate));
                }
                return (0);
            }
        }




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType

            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<MedicalServece.Models.Radiationcategory> Radiationcategory { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.RadiationLabContent> RadiationLabContent { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.RadiationLab> RadiationLab { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.Testscategory> Testscategory { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.TestsLabContent> TestsLabContent { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.TestsLab> TestsLab { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.MajorSpecialization> MajorSpecialization { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.SubSpecialization> SubSpecialization { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.NRSpecialization> NRSpecialization { get; set; }

        public System.Data.Entity.DbSet<MedicalServece.Models.Clinic> Clinic { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.Appointments> Appointments { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.MFile> MFile { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.FileContent> FileContent { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.RatingDR> RatingDR { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.NCheckUp> NCheckUp { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.RatingNR> RatingNR { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.ResultFile> ResultFile { get; set; }
        public System.Data.Entity.DbSet<MedicalServece.Models.FileForDr> FileForDr { get; set; }


    }
}