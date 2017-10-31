using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class PatientSelected
    {
        public int PatientId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Detail { get; set; }
        public string History { get; set; }
        public string Diagnosis { get; set; }
        public string Comment { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsSelected { get; set; }

    }
}
