using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Spcefication.JobSpecification
{
    public class JobWithFiltersForCountSpecification :BaseSpcification<Jop>
    {

        public JobWithFiltersForCountSpecification(JobtSpecParams specParams):base(J =>
      (string.IsNullOrEmpty(specParams.Search) || J.Name.ToLower().Contains(specParams.Search)) 
   
    )
        {

        }






    }
}
