using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HospitalManagement.Models;
using Microsoft.AspNet.Authorization;
using HospitalManagement.ViewModels.Admin;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Hosting;

namespace HospitalManagement.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private IApplicationEnvironment _hostingEnvironment;


        public AdminController(ApplicationDbContext context, IApplicationEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddPatientPage()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPatient(PatientViewModel model, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                ModifyPatient m = new ModifyPatient(_context);
                await m.addPatient(model.Fname, model.Lname,model.Email, model.Gender, model.PatientAge, model.BloodGroup, model.ContactNo, model.Address, model.PatientDetails, model.PatientHistory, model.DoctorDiagnosis, model.Comments, model.Email + ".jpg");
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".gif") || fileName.EndsWith(".bmp") || fileName.EndsWith(".tiff"))

                {

                    var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\profiledat\\" + model.Email + ".jpg";
                    await file.SaveAsAsync(filePath);

                }



                return RedirectToAction("Home");


            }

            return View("AddPatientPage", model);


        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ModifyPatientPage()
        {

            GetPatient getPatient = new GetPatient(_context);
            List<Patient> patients = getPatient.getAllPatients();


            return View(patients);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            User user = new User();
            user.Email = HttpContext.Session.GetString("Email");
            user.Fname = HttpContext.Session.GetString("FName");

            return View(user);
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePatients(IEnumerable<int> PatientsToDeleteCheckboxes)
        {

            if (PatientsToDeleteCheckboxes.Count<int>() != 0)
            {
                ModifyPatient mP = new ModifyPatient(_context);


                foreach (var p in PatientsToDeleteCheckboxes)
                {
                    await mP.deletePatient(p);

                }


            }

           
           


          return RedirectToAction("ModifyPatientPage");


        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePatientChanges(PatientViewModel model, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                ModifyPatient m = new ModifyPatient(_context);
                int count = 0;
                if (file != null)
                    count = 1;

                await m.updatePatient(int.Parse( HttpContext.Session.GetString("PatientId")), model,count);

                if(file!=null)
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

                HttpContext.Session.Remove("PatientId");
                return RedirectToAction("ModifyPatientPage");


            }

            return RedirectToAction("ModifyPatientPage");


        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuery(IEnumerable<int> QueriesToDeleteCheckboxes)
        {

            if (QueriesToDeleteCheckboxes.Count<int>() != 0)
            {
                GetQueries mP = new GetQueries(_context);


                foreach (var p in QueriesToDeleteCheckboxes)
                {
                    await mP.deleteQuery(p);

                }


            }

            return RedirectToAction("ViewQueriesPage");


        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePhoto()
        {

            ModifyPatient m = new ModifyPatient(_context);
           await  m.deletePatientPhoto(int.Parse(HttpContext.Session.GetString("PatientId")),_hostingEnvironment);
            

            return RedirectToAction("EditPatientPage");


        }

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditPatientPage(string editPatient)
        {
            if(editPatient==null)
                editPatient = HttpContext.Session.GetString("PatientId");


            GetPatient model = new GetPatient(_context);
            Patient pa = model.getPatient(int.Parse(editPatient));
            PatientViewModel p = new PatientViewModel();
            p.Address = pa.Address;
            p.BloodGroup = pa.BloodGroup;
            p.Comments = pa.Comment;
            p.ContactNo = pa.Contact;
            p.DoctorDiagnosis = pa.Diagnosis;
            p.Email = pa.Email;
            p.Fname = pa.Fname;
            p.Gender = pa.Gender;
            p.Lname = pa.Lname;
            p.PatientAge = pa.Age;
            p.PatientDetails = pa.Detail;
            p.PatientHistory = pa.History;
            p.ProfilePicture = pa.ProfilePicture;

            HttpContext.Session.SetString("PatientId",editPatient);



            return View(p);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewQueriesPage()
        {
            GetQueries getAllQ = new GetQueries(_context);
            

            return View(getAllQ.getAllQueries());

        }


    }
}
