using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts.ProductContracts
{
    public interface IUnitOfWork
    {
        IGinaricRepository<TEntity, TKey> GetRepository <TEntity, TKey>() where TEntity:BaseEntity<TKey>;
        Task<int> SaveChangesAsync();
    }
}
