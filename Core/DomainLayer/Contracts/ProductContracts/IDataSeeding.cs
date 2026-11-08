using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts.ProductContracts
{
    public interface IDataSeeding
    {
        public Task DataSeedAsync();
        public Task IdentityDataSeedAsync();

    }
}
