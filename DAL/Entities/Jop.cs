using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Jop : BaseEntity

    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string UserId { get; set; }
        public int? DisabilityId { get; set; }

        public List<JopForm>? JopForms { get; set; } = new List<JopForm>();
            

    }
}
