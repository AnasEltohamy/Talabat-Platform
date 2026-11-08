using AutoMapper;
using DomainLayer.Contracts.BasketContracts;
using DomainLayer.Models.BasketModels;
using ServiceAbstraction;
using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Basket
{
    public class BasketService(IBasketReposatory _basketReposatory, IMapper _mapper) : IBasketService
    {
        public async Task<CustomerBasketDTO> CreateOrUpdateBasketAsync(CustomerBasketDTO Basket)
        {
            var MapObject = _mapper.Map<CustomerBasketDTO,CustomerBasket>(Basket);
            var Result = await _basketReposatory.CreateOrUpdateBasketAsync(MapObject);
            if (Result is null)
            {
                throw new Exception("Create Or Update Basket is Fill :( !!!"); 
            }
            return await GetBasketAsync(Basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _basketReposatory.DeleteBasketAsync(id);
        }

        public async Task<CustomerBasketDTO> GetBasketAsync(string id)
        {
            var Item = await _basketReposatory.GetCustomerBasketAsync(id);
            if (Item is not null)
            {
                var MapObject = _mapper.Map<CustomerBasket, CustomerBasketDTO>(Item);
                return MapObject;
            }
            else
            {
                throw new Exception("Basket Not Found !!");
            }
           
        }
    }
}
