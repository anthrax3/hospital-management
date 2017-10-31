using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HospitalManagement.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using HospitalManagement.ViewModels.Admin;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagement.Controllers
{
    public class DoctorsController : Controller
    {
        private ApplicationDbContext _context;
        private IApplicationEnvironment _hostingEnvironment;


        public DoctorsController(ApplicationDbContext context, IApplicationEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

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


        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditPatientPage()
        {
            GetPatient getPatient = new GetPatient(_context);
            List<Patient> patients = getPatient.getAllPatients();


            return View(patients);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditPatient(string editPatient)
        {
            if (editPatient == null)
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

            HttpContext.Session.SetString("PatientId", editPatient);



            return View(p);

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

                await m.updatePatient(int.Parse(HttpContext.Session.GetString("PatientId")), model, count);

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

                HttpContext.Session.Remove("PatientId");
                return RedirectToAction("EditPatientPage");


            }

            return RedirectToAction("EditPatientPage");


        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult HistoryPage()
        {
            GetPatient getPatient = new GetPatient(_context);
            List<Patient> patients = getPatient.getAllPatients();


            return View(patients);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ContactAdminPage()
        {


            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AskConfirm(string question)
        {

            ManageQueries m = new ManageQueries(_context);
           await m.addQuery(question, HttpContext.Session.GetString("Email"));



            return View();

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult SearchPatientPage()
        {
            GetPatient getPatient = new GetPatient(_context);
            List<Patient> patients = getPatient.getAllPatients();


            return View(patients);
        }

        [HttpGet]
        public JsonResult GetPatients(string PatientName,string PatientAge, string PatientGender,string SortBy, string Order)
        {

            GetPatient getPatients = new GetPatient(_context);
            List<Patient> patients = new List<Patient>();


            if (PatientName == null && PatientAge == null && PatientGender == "Select")
            {
                patients = getPatients.getAllPatients();

            }
            else if (PatientName != null && PatientAge == null && PatientGender == "Select")
            {
                patients = getPatients.getPatientByName(PatientName);
            }
            else if (PatientName == null && PatientAge != null && PatientGender == "Select")
            {
                patients = getPatients.getPatientByAge(int.Parse(PatientAge));

            }
            else if (PatientName == null && PatientAge == null && PatientGender != "Select")
            {
                patients = getPatients.getPatientByGender(PatientGender);

            }

            else if (PatientName != null && PatientAge != null && PatientGender == "Select")
            {
                patients = getPatients.getPatientByNameAndAge(PatientName,int.Parse(PatientAge));

            }
            else if (PatientName != null && PatientAge == null && PatientGender != "Select")
            {
                patients = getPatients.getPatientByNameAndGender(PatientName, PatientGender);

            }
            else if (PatientName == null && PatientAge != null && PatientGender != "Select")
            {
                patients = getPatients.getPatientByAgeAndGender(int.Parse(PatientAge), PatientGender);

            }
            else if (PatientName != null && PatientAge != null && PatientGender != "Select")
            {
                patients = getPatients.getPatientByNameAgeAndGender(PatientName,int.Parse(PatientAge), PatientGender);

            }


            //sorting
            if (SortBy == "Fname")
            {
                if(Order== "Ascending")
                patients = patients.OrderBy(o => o.Fname).ToList();
                else
                    patients = patients.OrderByDescending(o => o.Fname).ToList();
            }
            else if (SortBy == "Lname")
            {

                if (Order == "Ascending")
                    patients = patients.OrderBy(o => o.Lname).ToList();
                else
                    patients = patients.OrderByDescending(o => o.Lname).ToList();

            }

            else if (SortBy == "Age")
            {
                if (Order == "Ascending")
                    patients = patients.OrderBy(o => o.Age).ToList();
                else
                    patients = patients.OrderByDescending(o => o.Age).ToList();
            }

            else if (SortBy == "Gender")
            {
                if (Order == "Ascending")
                    patients = patients.OrderBy(o => o.Gender).ToList();
                else
                    patients = patients.OrderByDescending(o => o.Gender).ToList();

            }



           
                

            return Json(patients);


        }


        // GET: Doctors
        public IActionResult Index()
        {
            return View(_context.Doctor.ToList());
        }

        // GET: Doctors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Doctor doctor = _context.Doctor.Single(m => m.DoctorId == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
             
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctor.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Doctor doctor = _context.Doctor.Single(m => m.DoctorId == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Update(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Doctor doctor = _context.Doctor.Single(m => m.DoctorId == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = _context.Doctor.Single(m => m.DoctorId == id);
            _context.Doctor.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
