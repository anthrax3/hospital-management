using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Controller
    {
        private ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public IEnumerable<Patient> GetPatient()
        {
            return _context.Patient;
        }

        // GET: api/Patients/5
        [HttpGet("{id}", Name = "GetPatient")]
        public IActionResult GetPatient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Patient patient = _context.Patient.Single(m => m.PatientId == id);

            if (patient == null)
            {
                return HttpNotFound();
            }

            return Ok(patient);
        }


        [HttpGet("PatientName/{PatientName}", Name = "GetPatientByName")]
        public IActionResult GetPatientByName(string PatientName)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            GetPatient getPatients = new GetPatient(_context);
            List<Patient> patients = new List<Patient>();


            patients = getPatients.getPatientByName(PatientName);

           

            if (patients.Count() == 0)
            {
                return HttpNotFound();
            }

            return Ok(patients);
        }

        [HttpGet("PatientAge/{PatientAge}", Name = "GetPatientByAge")]
        public IActionResult GetPatientByAge(string PatientAge)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            GetPatient getPatients = new GetPatient(_context);
            List<Patient> patients = new List<Patient>();


            patients = getPatients.getPatientByAge(int.Parse(PatientAge));



            if (patients.Count() == 0)
            {
                return HttpNotFound();
            }

            return Ok(patients);
        }

        [HttpGet("PatientGender/{PatientGender}", Name = "GetPatientByGender")]
        public IActionResult GetPatientByGender(string PatientGender)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            GetPatient getPatients = new GetPatient(_context);
            List<Patient> patients = new List<Patient>();


            patients = getPatients.getPatientByGender(PatientGender);



            if (patients.Count() == 0)
            {
                return HttpNotFound();
            }

            return Ok(patients);
        }



        [HttpGet("PatientName/{PatientName}/PatientAge/{PatientAge}/PatientGender/{PatientGender}/SortBy/{SortBy}/Order/{Order}", Name = "GetPatientByAll")]
        public IActionResult GetPatientByAll(string PatientName, string PatientAge,string PatientGender,string SortBy,string Order)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
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
                patients = getPatients.getPatientByNameAndAge(PatientName, int.Parse(PatientAge));

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
                patients = getPatients.getPatientByNameAgeAndGender(PatientName, int.Parse(PatientAge), PatientGender);

            }


            //sorting
            if (SortBy == "Fname")
            {
                if (Order == "Ascending")
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



            if (patients.Count() == 0)
            {
                return HttpNotFound();
            }

            return Ok(patients);
        }



        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public IActionResult PutPatient(int id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != patient.PatientId)
            {
                return HttpBadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/Patients
        [HttpPost]
        public IActionResult PostPatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Patient.Add(patient);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.PatientId))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetPatient", new { id = patient.PatientId }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Patient patient = _context.Patient.Single(m => m.PatientId == id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            _context.Patient.Remove(patient);
            _context.SaveChanges();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Count(e => e.PatientId == id) > 0;
        }
    }
}