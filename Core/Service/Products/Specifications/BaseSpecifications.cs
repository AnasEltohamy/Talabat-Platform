using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Products.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications()
        {

        }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }
        

        public Expression<Func<TEntity, bool>> Criteria { get; private set; } = default!; 
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        public Expression<Func<TEntity,object>> OrderByAsc { get; private set; } 
        public Expression<Func<TEntity,object>> OrderByDesc { get; private set; } 





        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }


        protected void AddOrderByAsc(Expression<Func<TEntity, object>> expression)
        {
            OrderByAsc = expression;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }



    }
}
