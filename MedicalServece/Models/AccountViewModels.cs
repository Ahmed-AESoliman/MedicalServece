using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MedicalServece.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "الاسم بالكامل")]
        public string FullUserName { get; set; }

        [Required]
        [EmailAddress]              
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمه المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمه المرور")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime Birthdate { get; set; }
        [Required]
        [Display(Name = "الجنس")]
        public string Gender { get; set; }

        [Required]
        public string UserType { get; set; }
    }
    public class EditProFile
    {

        [Required]
        [Display(Name = "الاسم بالكامل")]
        public string FullUserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime Birthdate { get; set; }
        [Required]
        [Display(Name = "الجنس")]
        public string Gender { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool NActive { get; set; }
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class EditProfileDoctor
    {
        [Required]
        [Display(Name = "الاسم بالكامل")]
        public string FullUserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime Birthdate { get; set; }
        [Required]
        [Display(Name = "الجنس")]
        public string Gender { get; set; }
        [Required]
        public string ProfessionalTitle { get; set; }//استاذ او اخصائي و استشاري
        [Required]
        public string FullProfessionalTitle { get; set; }//الاسم المهني بالكامل
        public string ImageUser { get; set; }

        [Required]
        public string Summray { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Degree { get; set; }//الدرجه العلميه
        [Required]
        public string ExYear { get; set; }//سنه الخبره او التخرج
        [Required]
        public string MajorSpecialization { get; set; }
        [Required]
        public string SubSpecialization { get; set; }
    }

}
