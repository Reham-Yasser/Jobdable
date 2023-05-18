using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Spcefication.DisabilitySpc
{
    public class DisabilityWithFilterForCountSpecification : BaseSpcification<Disability>
    {
        //this Constructor is used to get All Jobs
        public DisabilityWithFilterForCountSpecification(DisabilitySpecParams specParams) : base(Jf =>
      (string.IsNullOrEmpty(specParams.Search) || Jf.Name.ToLower().Contains(specParams.Search))

         )
        {

        }

    }
}
