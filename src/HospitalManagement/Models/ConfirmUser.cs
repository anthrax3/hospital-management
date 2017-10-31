using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class ConfirmUser
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public string SignUpAs { get; set; }


    }
}
