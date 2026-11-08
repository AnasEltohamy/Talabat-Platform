using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts.ProductContracts
{
    public interface ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {

        public Expression<Func<TEntity, bool>> Criteria { get; } 
        public List<Expression<Func<TEntity, object>>> IncludeExpressions {  get; }
        public Expression<Func<TEntity, object>> OrderByAsc { get;  }
        public Expression<Func<TEntity, object>> OrderByDesc { get; }


    }
}
