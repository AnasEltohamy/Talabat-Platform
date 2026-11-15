using AutoMapper;
using DomainLayer.Contracts.BasketContracts;
using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Models.IdentityModuls;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Basket;
using Service.Identity;
using Service.Orders;
using Service.Products;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager
        (
            IUnitOfWork unitOfWork ,
            IMapper mapper, 
            IBasketReposatory basketReposatory ,
            UserManager<AppUser> userManager ,
            IConfiguration _configuration
        ) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyServiceManager = new Lazy<IProductService>
            (
                () => new ProductService(unitOfWork, mapper)
            );
        private readonly Lazy<IBasketService> LazyBasketManager = new Lazy<IBasketService>
            (
                () => new BasketService(basketReposatory, mapper)
            );

        private readonly Lazy<IAuthenticationService> LazyAuthenticationService = new Lazy<IAuthenticationService>
            (
                () => new AuthenticationService(userManager, mapper, _configuration)
            );
        private readonly Lazy<IOrderService> LazyOrderService = new Lazy<IOrderService>
            (
                () => new OrderService( basketReposatory,  mapper,  unitOfWork)
            );



        public IProductService productService => LazyServiceManager.Value;
        public IBasketService basketService => LazyBasketManager.Value;
        public IAuthenticationService authenticationService => LazyAuthenticationService.Value;
        public IOrderService OrderService => LazyOrderService.Value;
    }
}
