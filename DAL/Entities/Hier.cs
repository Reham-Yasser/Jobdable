using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Hier : User
    {
        public string? Company { get; set; }
        public List<Jop>? Jop { get; set; } 
    }
}
