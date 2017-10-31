using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class UserConfirmation
    {

        private ApplicationDbContext _context;

        public UserConfirmation(ApplicationDbContext context)
        {

            _context = context;
            
        }


        public async Task MakeUserMember(string token,string email)
        {

            var c =
                from user in _context.ConfirmUser
                where user.Email==email && user.Token==token
                select user;

            foreach (var u in c)
            {
                if (u.SignUpAs == "doctor")
                {

                    _context.Doctor.AddRange(
                        new Doctor
                        {
                            About = u.About,
                            Address = u.Address,
                            City = u.City,
                            Contact = u.Contact,
                            Country = u.Country,
                            DateOfBirth = u.DOB,
                            Email = u.Email,
                            Fname = u.Fname,
                            Gender = u.Gender,
                            Lname = u.Lname,
                            Password = u.Password,
                            ProfilePicture = u.Email + ".jpg"
                   
                        }

                        );

                   
                }
                else if (u.SignUpAs == "admin")
                {
                    _context.Admin.AddRange(
                       new Admin
                       {
                           About = u.About,
                           Address = u.Address,
                           City = u.City,
                           Contact = u.Contact,
                           Country = u.Country,
                           DateOfBirth = u.DOB,
                           Email = u.Email,
                           Fname = u.Fname,
                           Gender = u.Gender,
                           Lname = u.Lname,
                           Password = u.Password,
                           ProfilePicture = u.Email + ".jpg"
                       }

                       );




                }

                _context.ConfirmUser.Remove(u);
               
                
                break;
            }

            await _context.SaveChangesAsync();


        }



    }
}
