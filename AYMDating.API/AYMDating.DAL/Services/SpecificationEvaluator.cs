using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Services
{
    internal class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
