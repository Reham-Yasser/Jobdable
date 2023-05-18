using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Spcefication
{
    public class SpcificationEvaluator<T>:BaseSpcification<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecification<T> spce)
        {
            var Query = inputQuery;
            if(spce.Criteria != null)
                Query = Query.Where(spce.Criteria);
            if(spce.OrderBy != null)
              Query =  Query.OrderBy(spce.OrderBy);
            if(spce.OrderByDescending != null)
                Query = Query.OrderByDescending(spce.OrderByDescending);
            if(spce.IsPagingEnabled)
                Query = Query.Skip(spce.Skip).Take(spce.Take);
            Query = spce.Includes.Aggregate(Query, (currentQuery, include) => currentQuery.Include(include));

            return Query;
        }
    }
}
