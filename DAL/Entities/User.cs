using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string? Image { get; set; }
        public string? Cv { get; set; }
        public string? Skils { get; set; }
        public string? Company { get; set; }
        public List<Jop>? Jop { get; set; }
    }
}
