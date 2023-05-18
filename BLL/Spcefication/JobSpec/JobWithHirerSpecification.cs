using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Spcefication.JobSpecification
{
    public class JobWithHirerSpecification : BaseSpcification<Jop>
    {

        //this Constructor is used to get All Jobs
        public JobWithHirerSpecification(JobtSpecParams specParams):
               base(J=>
            (string.IsNullOrEmpty(specParams.Search)||J.Name.ToLower().Contains(specParams.Search))
            )
        {
    

        AddOrderBy(P=>P.Name);  //Default

        ApplyPaging(specParams.PageSize* (specParams.PageIndex - 1),specParams.PageSize);

            if (string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "salaryAse":
                        AddOrderBy(J=>J.Salary);
                        break;
                    case "salaryDesc":
                        AddOrderByDescending(J =>J.Salary);
                        break;
                        default:
                        AddOrderBy(J=>J.Name);
                        break;
                }
}
        }


        //this Constructor is used to get a Specific Job with id 
        public JobWithHirerSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(J => J.JopForms);
     
        }

    }
}
