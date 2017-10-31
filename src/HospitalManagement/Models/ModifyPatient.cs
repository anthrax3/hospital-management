using HospitalManagement.ViewModels.Admin;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class ModifyPatient
    {
        private ApplicationDbContext _context;

        public ModifyPatient(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task addPatient(string fname, string lname,string email, string gender, int age,string bG,string contact, string address, string detail,string history, string diagnosis, string comments,string profile)
        {

            Patient p = new Patient();
            p.Address = address;
            p.Age = age;
            p.BloodGroup = bG;
            p.Comment = comments;
            p.Contact = contact;
            p.Detail = detail;
            p.Diagnosis = diagnosis;
            p.Email = email;
            p.Fname = fname;
            p.Gender = gender;
            p.History = history;
            p.Lname = lname;
            p.ProfilePicture = profile;

            _context.Patient.Add(p);
            await _context.SaveChangesAsync();


           
        }

        public async Task deletePatient(int id)
        {

          var patients=  from p in _context.Patient
            where p.PatientId == id
            select p;


            foreach (var p in patients)
            {
                _context.Patient.Remove(p);

            }

            await _context.SaveChangesAsync();



        }

        public async Task updatePatient(int id, PatientViewModel model,int count)
        {

            var patients = from p in _context.Patient
                           where p.PatientId == id
                           select p;


            foreach (var pa in patients)
            {
                pa.Address = model.Address;
                pa.BloodGroup = model.BloodGroup;
                pa.Comment= model.Comments ;
                pa.Contact = model.ContactNo;
                pa.Diagnosis = model.DoctorDiagnosis;
                pa.Email = model.Email;
                pa.Fname = model.Fname;
                pa.Gender = model.Gender;
                pa.Lname = model.Lname;
                pa.Age = model.PatientAge;
                pa.Detail = model.PatientDetails;
                pa.History = model.PatientHistory;
                if (count == 1)
                    pa.ProfilePicture = model.Email + ".jpg";


            }

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
               
            }


        }

        public async Task deletePatientPhoto(int id, IApplicationEnvironment hostingEnvironment)
        {

            string email="";
            var patients = from p in _context.Patient
                           where p.PatientId == id
                           select p;


            foreach (var pa in patients)
            {
                pa.ProfilePicture = null;
                email = pa.Email;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }

            var fullPath = hostingEnvironment.ApplicationBasePath + "\\wwwroot\\profiledat\\" + email + ".jpg";
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }


        }

    }
}
