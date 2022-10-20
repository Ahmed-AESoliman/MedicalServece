using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MedicalServece.Models;
using System.Data.Entity;
using MedicalServece.Models.ModelView;

namespace MedicalServece.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                                                                                                     

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password,false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "تاكد من البريد الالكتروني و كلمه ");
                    return View(model);
            }
            
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
            ViewBag.UserType = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(),"Name", "Name");

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            MFile MF = new MFile();
            if (ModelState.IsValid)
            {
                ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
                ViewBag.UserType = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "Name", "Name");
                var user = new ApplicationUser {
                    FullUserName = model.FullUserName,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    Birthdate = model.Birthdate,
                    IsActive = true,
                    NActive = (model.UserType=="ممرض") ? false : true
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
 
                    await UserManager.AddToRoleAsync(user.Id, model.UserType);

                    MF.CreateDate = DateTime.Now;
                    MF.UserID = user.Id;
                    db.MFile.Add(MF);
                    db.SaveChanges();
                    if (model.UserType.Contains("ممرض"))
                    {
                        return RedirectToAction("EditNerusProfile");
                    }else if (model.UserType.Contains("طبيب"))
                    {
                        return RedirectToAction("EditDoctorProfile");

                    }
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // get Dr detiles
        [Authorize]
        public ActionResult DRProfile()
        {
            var Dr = User.Identity.GetUserId();
            return View(db.Users.Where(u => u.Id == Dr).SingleOrDefault());
        }
        [Authorize]
        public ActionResult NRProfile()
        {
            var Dr = User.Identity.GetUserId();
            Nrupdate(Dr);
            return View(db.Users.Where(u => u.Id == Dr).SingleOrDefault());
        }
        //
        //edit profile Doctor
        [Authorize]
        public ActionResult EditDoctorProfile()
        {
            ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
            ViewBag.MajorSpecialization = new SelectList(db.MajorSpecialization.ToList(), "Title", "Title");
            ViewBag.SubSpecialization = new SelectList(db.SubSpecialization.ToList(), "Title", "Title");
            var UserID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            EditProfileDoctor profile = new EditProfileDoctor();
            profile.FullUserName = user.FullUserName;
            profile.Email = user.Email;
            profile.Birthdate = user.Birthdate;
            profile.PhoneNumber = user.PhoneNumber;
            profile.Gender = user.Gender;
            profile.ProfessionalTitle = user.ProfessionalTitle;
            profile.FullProfessionalTitle = user.FullProfessionalTitle;
            profile.Summray = user.Summray;
            profile.ImageUser = user.ImageUser;
            profile.Country = user.Country;
            profile.Degree = user.Degree;
            profile.ExYear = user.ExYear;
            profile.MajorSpecialization = user.MajorSpecialization;
            profile.SubSpecialization = user.SubSpecialization;


            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctorProfile(EditProfileDoctor profile)
        {
            ViewBag.MajorSpecialization = new SelectList(db.MajorSpecialization.ToList(), "Title", "Title");
            ViewBag.SubSpecialization = new SelectList(db.SubSpecialization.ToList(), "Title", "Title");
            var UserID = User.Identity.GetUserId();
            var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            CurrentUser.FullUserName = profile.FullUserName;
            CurrentUser.Email = profile.Email;
            CurrentUser.Birthdate = profile.Birthdate;
            CurrentUser.Gender = profile.Gender;
            CurrentUser.PhoneNumber = profile.PhoneNumber;
            CurrentUser.ProfessionalTitle = profile.ProfessionalTitle;
            CurrentUser.FullProfessionalTitle = profile.FullProfessionalTitle;
            CurrentUser.Summray = profile.Summray;
            CurrentUser.ImageUser = profile.ImageUser;
            CurrentUser.Country = profile.Country;
            CurrentUser.Degree = profile.Degree;
            CurrentUser.ExYear = profile.ExYear;
            CurrentUser.MajorSpecialization = profile.MajorSpecialization;
            CurrentUser.SubSpecialization = profile.SubSpecialization;
            CurrentUser.UserName = profile.Email;

            db.Entry(CurrentUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DRProfile");
        }

        // edit Nuers profile
        [Authorize]
        public ActionResult EditNerusProfile()
        {
            ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
            ViewBag.MajorSpecialization = new SelectList(db.NRSpecialization.ToList(), "Title", "Title");
            var UserID = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            EditProfileDoctor profile = new EditProfileDoctor();
            profile.FullUserName = user.FullUserName;
            profile.Email = user.Email;
            profile.Birthdate = user.Birthdate;
            profile.PhoneNumber = user.PhoneNumber;
            profile.Gender = user.Gender;
            profile.ProfessionalTitle = user.ProfessionalTitle;
            profile.FullProfessionalTitle = user.FullProfessionalTitle;
            profile.Summray = user.Summray;
            profile.ImageUser = user.ImageUser;
            profile.Country = user.Country;
            profile.Degree = user.Degree;
            profile.ExYear = user.ExYear;
            profile.MajorSpecialization = user.MajorSpecialization;
   

            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNerusProfile(EditProfileDoctor profile)
        {
            ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
            ViewBag.MajorSpecialization = new SelectList(db.NRSpecialization.ToList(), "Title", "Title");
            var UserID = User.Identity.GetUserId();
            var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            CurrentUser.FullUserName = profile.FullUserName;
            CurrentUser.Email = profile.Email;
            CurrentUser.Birthdate = profile.Birthdate;
            CurrentUser.Gender = profile.Gender;
            CurrentUser.PhoneNumber = profile.PhoneNumber;
            CurrentUser.ProfessionalTitle = profile.ProfessionalTitle;
            CurrentUser.FullProfessionalTitle = profile.FullProfessionalTitle;
            CurrentUser.Summray = profile.Summray;
            CurrentUser.ImageUser = profile.ImageUser;
            CurrentUser.Country = profile.Country;
            CurrentUser.Degree = profile.Degree;
            CurrentUser.ExYear = profile.ExYear;
            CurrentUser.MajorSpecialization = profile.MajorSpecialization;
            CurrentUser.UserName = profile.Email;

            db.Entry(CurrentUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("NRProfile");
        }
        //
        //give Nurse the License
        [Authorize]
        public ActionResult NurseNotAllow()
        {
            var users = db.Users.Where(u => u.NActive == false&&u.NCheckUp.Count()>0);
            return View(users.ToList());
        }
        [HttpGet]
        [Authorize]
        public ActionResult License(string id)
        {
            var u = db.Users.Where(s => s.Id == id).SingleOrDefault();
            var check = db.NCheckUp.Where(c => c.UserID == id).SingleOrDefault();
            LicenseToNRView n = new LicenseToNRView();
            n.Area = u.Area;
            n.Birthdate = u.Birthdate;
            n.BloodTest = check.BloodTest;
            n.Certificate = check.Certificate;
            n.Country = u.Country;
            n.CriminalFile = check.CriminalFile;
            n.Degree = u.Degree;
            n.ExYear = u.ExYear;
            n.FullProfessionalTitle = u.FullProfessionalTitle;
            n.FullUserName = u.FullUserName;
            n.Gender = u.Gender;
            n.id = u.Id;
            n.ImageUser = u.ImageUser;
            n.LicenseDate = check.LicenseDate;
            n.MajorSpecialization = u.MajorSpecialization;
            n.NActive = u.NActive;
            n.ProfessionalTitle = u.ProfessionalTitle;
            n.Summray = u.Summray;
            return View(n);
        }
        [HttpPost]
        public ActionResult License(LicenseToNRView nr)
        {
            var u = db.Users.Where(s => s.Id == nr.id).SingleOrDefault();
            var check = db.NCheckUp.Where(s => s.UserID == nr.id).SingleOrDefault();
            nr.Area = u.Area;
            nr.Birthdate = u.Birthdate;
            nr.BloodTest = check.BloodTest;
            nr.Certificate = check.Certificate;
            nr.Country = u.Country;
            nr.CriminalFile = check.CriminalFile;
            nr.Degree = u.Degree;
            nr.ExYear = u.ExYear;
            nr.FullProfessionalTitle = u.FullProfessionalTitle;
            nr.FullUserName = u.FullUserName;
            nr.Gender = u.Gender;
            nr.ImageUser = u.ImageUser;
            nr.LicenseDate = check.LicenseDate;
            nr.MajorSpecialization = u.MajorSpecialization;
            nr.ProfessionalTitle= u.ProfessionalTitle;
            nr.Summray = u.Summray;
            u.NActive = true;
            db.Entry(u).State= System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("NurseNotAllow");
        }
        [Authorize]
        public ActionResult GetProfile()
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == userID).SingleOrDefault();
            EditProFile pro = new EditProFile();
            pro.Birthdate = user.Birthdate;
            pro.Email = user.Email;
            pro.FullUserName = user.FullUserName;
            pro.Gender = user.Gender;
            pro.PhoneNumber = user.PhoneNumber;
            return View(pro);
        }
        [Authorize]
        public ActionResult EditProfile()
        {
            ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == userID).SingleOrDefault();
            EditProFile pro = new EditProFile();
            pro.Birthdate = user.Birthdate;
            pro.Email = user.Email;
            pro.FullUserName = user.FullUserName;
            pro.Gender = user.Gender;
            pro.PhoneNumber = user.PhoneNumber;
            return View(pro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult EditProfile(EditProFile pro)
        {
            ViewBag.Gender = new SelectList(new[] { "ذكر", "انثي" });
            var userID = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == userID).SingleOrDefault();
            pro.Birthdate = user.Birthdate;
            pro.Email = user.Email;
            pro.FullUserName = user.FullUserName;
            pro.Gender = user.Gender;
            pro.PhoneNumber = user.PhoneNumber;
            user.UserName = pro.Email;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetProfile");
        }

        public ActionResult enableNR( bool IsActive)
        {
            var userId = User.Identity.GetUserId();
            var userdate = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (IsActive==true)
            {
                userdate.IsActive = false;
                db.Entry(userdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                userdate.IsActive = true;
                db.Entry(userdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("NRProfile");
        }

        public void Nrupdate( string userId)
        {
            var userdate = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            var NrLic = db.NCheckUp.Where(n => n.UserID == userId).SingleOrDefault();
            if (NrLic!=null)
            {
                var day = DateTime.Now.Date - NrLic.LicenseDate;
                if (day.TotalDays>=90)
                {
                    userdate.NActive = false;
                    db.Entry(userdate).State = System.Data.Entity.EntityState.Modified;
                    db.NCheckUp.Remove(NrLic);
                    db.SaveChanges();

                }
            }

        }


        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}