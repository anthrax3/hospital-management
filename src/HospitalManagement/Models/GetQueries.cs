using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class GetQueries
    {
        private ApplicationDbContext _context;


        public GetQueries(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<Query> getAllQueries()
        {
            var queries = from p in _context.Query
                           select p;

            List<Query> queryList = new List<Query>();

            foreach (Query p in queries)
            {
                queryList.Add(p);


            }

            return queryList;


        }

        public async Task deleteQuery(int id)
        {

            var queries = from p in _context.Query
                           where p.QueryId == id
                           select p;


            foreach (var p in queries)
            {
                _context.Query.Remove(p);

            }

            await _context.SaveChangesAsync();

        }

    }
}
