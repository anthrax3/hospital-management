using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class UserDetail
    {
        private ApplicationDbContext _context;

        public UserDetail(ApplicationDbContext context)
        {
            _context = context;

        }

        public User getUserDetailsAdmin(string email)
        {
            var admin = from ad in _context.Admin
                        where ad.Email == email
                        select ad;

            User user = new User();
        
            foreach (var x in admin)
            {
                user.About = x.About;
                user.Address = x.Address;
                user.City = x.City;
                user.Contact = x.Contact;
                user.Country = x.Country;
                user.DateOfBirth = x.DateOfBirth;
                user.Email = x.Email;
                user.Fname = x.Fname;
                user.Gender = x.Gender;
                user.Lname = x.Lname;
                user.Password = x.Password;

               
            }



            return user;


        }

        public User getUserDetailsDoctor(string email)
        {
            var doc = from ad in _context.Doctor
                        where ad.Email == email
                        select ad;

            User user = new User();

            foreach (var x in doc)
            {
                user.About = x.About;
                user.Address = x.Address;
                user.City = x.City;
                user.Contact = x.Contact;
                user.Country = x.Country;
                user.DateOfBirth = x.DateOfBirth;
                user.Email = x.Email;
                user.Fname = x.Fname;
                user.Gender = x.Gender;
                user.Lname = x.Lname;
                user.Password = x.Password;


            }



            return user;



        }


    }
}
