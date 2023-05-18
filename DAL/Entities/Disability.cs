using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Disability : BaseEntity
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public List<Jop> Jops { get; set; } = new List<Jop>();
    }
}
