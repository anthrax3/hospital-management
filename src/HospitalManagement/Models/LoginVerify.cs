using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class LoginVerify
    {
        private ApplicationDbContext _context;
        private static IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        
        public LoginVerify(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public  bool verifyAdminDetails(string uname, string pass)
        {

            var admin = from ad in _context.Admin
                        where ad.Email == uname && ad.Password == pass
                        select ad;

           int count = 0;
            foreach (var x in admin)
            {
                _session.SetString("Email", x.Email);
                _session.SetString("FName", x.Fname);
                _session.SetString("LName", x.Lname);
                _session.SetString("Type", "admin");
                _session.SetString("Address", x.Address);
                _session.SetString("City", x.City);
                _session.SetString("Contact", x.Contact);
                _session.SetString("Country", x.Country);
                _session.SetString("DateOfBirth", x.DateOfBirth);
                _session.SetString("Gender", x.Gender);
                if(x.ProfilePicture==null)
                    _session.SetString("ProfilePicture", "");
                else
                _session.SetString("ProfilePicture", x.ProfilePicture);

                if (x.About == null)
                    _session.SetString("About", "");
                else
                    _session.SetString("About", x.About);





                count++;
            }

            
            if (count==1)
                return true;
            return false;
           
        }

        public bool verifyDoctorDetails(string uname, string pass)
        {

            var doctor = from d in _context.Doctor
                        where d.Email == uname && d.Password == pass
                        select d;

            int count = 0;
            foreach (var x in doctor)
            {

                _session.SetString("Email", x.Email);
                _session.SetString("FName", x.Fname);
                _session.SetString("LName", x.Lname);

                _session.SetString("Type", "doctor");

                _session.SetString("Address", x.Address);
                _session.SetString("City", x.City);
                _session.SetString("Contact", x.Contact);
                _session.SetString("Country", x.Country);
                _session.SetString("DateOfBirth", x.DateOfBirth);
                _session.SetString("Gender", x.Gender);

                if (x.ProfilePicture == null)
                    _session.SetString("ProfilePicture", "");
                else
                    _session.SetString("ProfilePicture", x.ProfilePicture);

                if (x.About == null)
                    _session.SetString("About", "");
                else
                    _session.SetString("About", x.About);


                count++;
            }


            if (count == 1)
                return true;
            return false;

        }



    }

}
