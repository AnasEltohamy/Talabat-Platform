using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistence.Store.Data
{
    public static class SpecificationEvaluator
    {
        
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>
            (
                IQueryable<TEntity> InputQuery, ISpecification<TEntity, TKey> spec
            ) where TEntity : BaseEntity<TKey>

        {

            var Query = InputQuery;

            if (spec.Criteria is not null)
            {
                Query = Query.Where( spec.Criteria );
            }
            if (spec.OrderByAsc is not null)
            {
                Query = Query.OrderBy(spec.OrderByAsc);

            }
            if (spec.OrderByDesc is not null)
            {
                Query= Query.OrderByDescending(spec.OrderByDesc); 
            }
            if (spec.IncludeExpressions is not null )
            {
                Query = spec.IncludeExpressions.Aggregate(Query,(X,Y)=>X.Include(Y));
                
            }

            return Query;


        }


    }
}
