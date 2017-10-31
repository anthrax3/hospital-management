using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class GetPatient
    {
        private ApplicationDbContext _context;
       

        public GetPatient(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<Patient> getAllPatients()
        {
            var patients = from p in _context.Patient
                           select p;

            List<Patient> patientList= new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);


            }

            return patientList;


        }

        public Patient getPatient(int id)
        {
            var patients = from p in _context.Patient
                           where p.PatientId == id
                           select p;

            Patient patient = new Patient();

            foreach (Patient p in patients)
            {
               patient= p;


            }

            return patient;


        }

        public List<Patient> getPatientByName(string name)
        {
            var patients = from p in _context.Patient
                           where (p.Fname.ToUpper()+ " " + p.Lname.ToUpper()).Contains(name.ToUpper())
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }

        public List<Patient> getPatientByAge(int age)
        {
            var patients = from p in _context.Patient
                           where p.Age==age
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }

        public List<Patient> getPatientByGender(string gender)
        {
            var patients = from p in _context.Patient
                           where p.Gender.ToUpper() == gender.ToUpper()
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }

        public List<Patient> getPatientByNameAndGender(string name,string gender)
        {
            var patients = from p in _context.Patient
                           where (p.Fname.ToUpper() + " " + p.Lname.ToUpper()).Contains(name.ToUpper()) && p.Gender.ToUpper() == gender.ToUpper()
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }

        public List<Patient> getPatientByNameAndAge(string name, int age)
        {
            var patients = from p in _context.Patient
                           where (p.Fname.ToUpper() + " " + p.Lname.ToUpper()).Contains(name.ToUpper()) && p.Age==age
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }

        public List<Patient> getPatientByAgeAndGender(int age,string gender)
        {
            var patients = from p in _context.Patient
                           where  p.Age == age && p.Gender.ToUpper() == gender.ToUpper()
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }

        public List<Patient> getPatientByNameAgeAndGender(string name, int age, string gender)
        {
            var patients = from p in _context.Patient
                           where (p.Fname.ToUpper() + " " + p.Lname.ToUpper()).Contains(name.ToUpper()) && p.Age == age && p.Gender.ToUpper() == gender.ToUpper()
                           select p;
            List<Patient> patientList = new List<Patient>();

            foreach (Patient p in patients)
            {
                patientList.Add(p);
            }

            return patientList;


        }


    }
}
