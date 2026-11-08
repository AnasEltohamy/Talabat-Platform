using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistence.Store.Data.Repositories
{
    public class GinaricRepository<TEntity, Tkey> : IGinaricRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GinaricRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> Spec)
        {
           return await SpecificationEvaluator.CreateQuery(_context.Set<TEntity>(), Spec).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> Spec)
        {
            return await SpecificationEvaluator.CreateQuery(_context.Set<TEntity>(), Spec).FirstOrDefaultAsync();
        }


        public async Task<TEntity?> GetByIdAsync(Tkey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
           await _context.AddAsync(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
        }

       
       

        public void UpdateAsync(TEntity entity)
        {
             _context.Update(entity);
        }
    
    }
}
