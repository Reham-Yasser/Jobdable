using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee : User
    {
        public string? Cv { get; set; }
        public string? Skils { get; set; }

        
    }
}
