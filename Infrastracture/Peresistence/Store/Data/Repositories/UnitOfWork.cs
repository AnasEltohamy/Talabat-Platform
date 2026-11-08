using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistence.Store.Data.Repositories
{

    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        public readonly Dictionary<string, object> _repo = [];
        public IGinaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //get Type Name 
            var TypeName = typeof(TEntity).Name;
            if (_repo.ContainsKey(TypeName))
            {
                return _repo[TypeName] as IGinaricRepository<TEntity, TKey>;
            }
            else
            {
                var Repo = new GinaricRepository<TEntity, TKey>(context);
                _repo.Add(TypeName, Repo);
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
           return await context.SaveChangesAsync();
        }
    }
}
