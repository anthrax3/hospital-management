using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class ManageQueries
    {
        private ApplicationDbContext _context;

        public ManageQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task addQuery(string query,string docEmail)
        {

            Query q = new Query();
            q.DoctorEmail = docEmail;
            q.Question = query;
               

             _context.Query.Add(q);


            await _context.SaveChangesAsync();



        }





    }

}
