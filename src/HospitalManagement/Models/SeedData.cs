using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HospitalManagement.Models
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (context.Database == null)
            {
                throw new Exception("DB is null");
            }

          if (context.Admin.Any())
          {
          return;   // DB has been seeded
          }

            context.Admin.AddRange(
                new Admin
                {
                    Fname = "Hamza",
                    Lname = "Saleem",
                    Email = "hamzasaleemzpr@gmail.com",
                    Password="password",
                    DateOfBirth="23/06/1996",
                    Country="Pakistan",
                    City="Lahore",
                    Contact="03023839058",
                    Address="Railway Officers Colony, Walton Lahore",
                    Gender = "Male",           
                    About="Normal is boring"
       
               }
                );

          context.Patient.AddRange(
                 new Patient
                 {

                     Fname="Ali",
                     Lname="Ahmad",
                     Email="ali@gmail.com",
                     BloodGroup="O+",
                     Contact="03023838998",
                     Address="Walton Lahore",
                     Age=20,
                     Gender="Male",
                     Detail="Nothing Special",
                     History = "Nothing Special",
                     Diagnosis = "Nothing Special",
                     Comment = "Nothing Special"
                     

                 },

                 new Patient
                 {

                     Fname = "Wahab",
                     Lname = "Riaz",
                     Email = "wahab@gmail.com",
                     BloodGroup = "O-",
                     Contact = "03029988776",
                     Address = "Cantt. Lahore",
                     Age = 24,
                     Gender = "Male",
                     Detail = "Nothing Special",
                     History = "Nothing Special",
                     Diagnosis = "Nothing Special",
                     Comment = "Nothing Special"


                 },

                  new Patient
                  {

                      Fname = "Umer",
                      Lname = "Gull",
                      Email = "umer@gmail.com",
                      BloodGroup = "A-",
                      Contact = "03022233232",
                      Address = "Johar Town, Lahore",
                      Age = 30,
                      Gender = "Male",
                      Detail = "Nothing Special",
                      History = "Nothing Special",
                      Diagnosis = "Nothing Special",
                      Comment = "Nothing Special"


                  }

              
            );





            context.SaveChanges();
        }


    }
}
