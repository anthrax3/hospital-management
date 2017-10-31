using HospitalManagement.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class ModifyUser
    {
        private ApplicationDbContext _context;

        public ModifyUser(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task updateUser( EditProfileViewModel model)
        {

            var admin = from p in _context.Admin
                           where p.Email == model.Email
                           select p;


            foreach (var pa in admin)
            {
                pa.Address = model.Address;
                pa.Contact = model.ContactNo;
                pa.Fname = model.Fname;
                pa.Lname = model.Lname;
               pa.Address= model.Country;
               pa.City= model.City;
               pa.About=  model.About;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }




            var doc = from p in _context.Doctor
                        where p.Email == model.Email
                        select p;


            foreach (var pa in doc)
            {
                pa.Address = model.Address;
                pa.Contact = model.ContactNo;
                pa.Fname = model.Fname;
                pa.Lname = model.Lname;
                pa.Address = model.Country;
                pa.City = model.City;
                pa.About = model.About;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }







        }

        public async Task updateAboutMe(string email,string aboutme)
        {

            var admin = from p in _context.Admin
                        where p.Email == email
                        select p;


            foreach (var pa in admin)
            {
                
                pa.About = aboutme;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }




            var doc = from p in _context.Doctor
                      where p.Email == email
                      select p;


            foreach (var pa in doc)
            {
               
                pa.About = aboutme;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }







        }



    }
}
