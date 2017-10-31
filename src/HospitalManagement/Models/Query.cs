using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Models
{
    public class Query
    {
        public int QueryId { get; set; }
        public string DoctorEmail { get; set; }
        public string Question { get; set; }

    }
}
