using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Spcefication.JobFormSpec
{
    public class JobFormWithFilterForCountSpecification : BaseSpcification<JopForm>
    {

        public JobFormWithFilterForCountSpecification(JobFormSpecParams specParams) : base(Jf =>
      (string.IsNullOrEmpty(specParams.Search) || Jf.Name.ToLower().Contains(specParams.Search))

         )
        {

        }

    }
}
