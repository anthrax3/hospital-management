using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using HospitalManagement.ViewModels.Account;
using HospitalManagement.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using HospitalManagement.Services;
using System.IO;
using System.Web;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private  IApplicationEnvironment _hostingEnvironment;



        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IEmailSender emailSender,
           ApplicationDbContext context,
           IApplicationEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;



        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Startup()
        {

            if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "")
            {
                 _signInManager.SignOutAsync();

                HttpContext.Session.Clear();
                return RedirectToAction("LoginPage");


            }
            else if (HttpContext.Session.GetString("Type") == "admin")
                return RedirectToAction("Home", "Admin");
            else if (HttpContext.Session.GetString("Type") == "doctor")
                return RedirectToAction("Home", "Doctors");
            


              return  RedirectToAction("LoginPage");

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(string Token, string Email)
        {

            UserConfirmation u = new UserConfirmation(_context);
            await u.MakeUserMember(Token, Email);

            return RedirectToAction("LoginPage");
        }




        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Home(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {

                LoginVerify login = new LoginVerify(_context);


                if (login.verifyDoctorDetails(model.Email, model.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Home", "Doctors");


                    }


                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View("LoginPage", model);
                    }




                }

                else if (login.verifyAdminDetails(model.Email, model.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Home", "Admin");

                    }


                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View("LoginPage", model);
                    }

                }



            }


            return View("LoginPage", model);


        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUpPage()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model, IFormFile file)
        {
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.LoginPassword);
                if (result.Succeeded)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".gif") || fileName.EndsWith(".bmp") || fileName.EndsWith(".tiff"))

                    {

                        var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\profiledat\\" + model.Email + ".jpg";
                        await file.SaveAsAsync(filePath);
                        
                    }

                    
                    await MessageServices.SendEmail(model, _context);


                    return RedirectToAction("EmailConfPage");
                }
                else
                return View("SignUpPage");
            }

            return View("SignUpPage");

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            
            HttpContext.Session.Clear();
           
            return RedirectToAction("LoginPage");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ProfileViewPage()
        {
            if (HttpContext.Session.GetString("Email") != "" && HttpContext.Session.GetString("Email") != null)
            {
               
                User user = new User();
                user.Email = HttpContext.Session.GetString("Email");
                user.Fname = HttpContext.Session.GetString("FName");
                user.Lname = HttpContext.Session.GetString("LName");
                user.About = HttpContext.Session.GetString("About");
                user.Address = HttpContext.Session.GetString("Address");
                user.City = HttpContext.Session.GetString("City");
                user.Contact = HttpContext.Session.GetString("Contact");
                user.Country = HttpContext.Session.GetString("Country");
                user.DateOfBirth = HttpContext.Session.GetString("DateOfBirth");
                user.Gender = HttpContext.Session.GetString("Gender");
                user.ProfilePicture= HttpContext.Session.GetString("ProfilePicture");
                




                return View(user);
            }

            return View("LoginPage");
            
        }


      

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditProfile()
        {
            EditProfileViewModel s = new EditProfileViewModel();
            s.Email= HttpContext.Session.GetString("Email");
            s.Fname = HttpContext.Session.GetString("FName");
            s.Lname = HttpContext.Session.GetString("LName");
            s.About = HttpContext.Session.GetString("About");
            s.Address = HttpContext.Session.GetString("Address");
            s.City = HttpContext.Session.GetString("City");
            s.ContactNo = HttpContext.Session.GetString("Contact");
            s.Country = HttpContext.Session.GetString("Country");
            s.ProfilePicture= HttpContext.Session.GetString("ProfilePicture");


            return View(s);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProfileChanges(EditProfileViewModel model, IFormFile file)
        {
            model.Email= HttpContext.Session.GetString("Email"); 
            model.ProfilePicture = HttpContext.Session.GetString("ProfilePicture");


            if (ModelState.IsValid)
            {
                ModifyUser m = new ModifyUser(_context);
                await m.updateUser(model);

                if (file != null)
                {
                    var fullPath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\profiledat\\" + model.Email + ".jpg";
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }


                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".gif") || fileName.EndsWith(".bmp") || fileName.EndsWith(".tiff"))

                    {

                        var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\profiledat\\" + model.Email + ".jpg";
                        await file.SaveAsAsync(filePath);

                    }
                }


                HttpContext.Session.SetString("FName", model.Fname);
                HttpContext.Session.SetString("LName", model.Lname);
                HttpContext.Session.SetString("Address", model.Address);
                HttpContext.Session.SetString("City", model.City);
                HttpContext.Session.SetString("Contact", model.ContactNo);
                HttpContext.Session.SetString("Country", model.Country);

                if (model.About != null)
                    HttpContext.Session.SetString("About", model.About);
                else
                    HttpContext.Session.SetString("About", "");



            }

            if(HttpContext.Session.GetString("Type")=="admin")
            return RedirectToAction("Home", "Admin");
            else if(HttpContext.Session.GetString("Type") == "doctor")
                return RedirectToAction("Home", "Doctors");



            return RedirectToAction("LoginPage");



        }


        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmailConfPage()
        {
            return View();
        }


    }
}
