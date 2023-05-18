using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Spcefication.JobSpecification
{
    public class JobtSpecParams
    {


        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;


        private int pageSize = 50;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public string? Sort { get; set; }

        public int? HirerId { get; set; }

       // public int? TypeId { get; set; }



        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
