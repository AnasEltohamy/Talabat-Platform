using AutoMapper;
using DomainLayer.Contracts.BasketContracts;
using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModuls;
using DomainLayer.Models.OrderModel;
using DomainLayer.Models.OrderModuls;
using DomainLayer.Models.ProductModels;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DTOs.Identity;
using Shared.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Orders
{
    public class OrderService : IOrderService
    {
        
        private readonly IBasketReposatory _basketReposatory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketReposatory basketReposatory, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _basketReposatory = basketReposatory;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO, string email)
        {
            //1- BasketId =>  Get Basket User By Method
            var userBasket = await _basketReposatory.GetCustomerBasketAsync(orderDTO.BasketId);

            if (userBasket is null )
            {
                throw new BasketNotFoundException(orderDTO.BasketId);
            }

            List<OrderItem> listOfOrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in userBasket.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id);
                if (product is null)
                {
                    throw new ProductNotFoundExcption(item.Id);
                }
                var orderItems = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity

                };

                listOfOrderItems.Add(orderItems);
            }




            // 2- AddressDTO => Address
            var address = _mapper.Map<AddressDTO, OrderAddress>(orderDTO.addressDTO);

            // 3- DeliveryMethodId => Get DeliveryMethod(Name) By Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId)
                ?? throw new DeliveryMethodExciptions(orderDTO.DeliveryMethodId);

            // 4- GetTotl

            var total = listOfOrderItems.Sum(O => O.Price * O.Quantity);

            //5- Create Order
            var order = new Order(email, address, deliveryMethod, listOfOrderItems, total);

            await _unitOfWork.GetRepository<Order ,Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order , OrderToReturnDTO>(order);
             
        }

        public async Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTO>>(deliveryMethod);
        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string email)
        {
            var Spec = new OrderWithIncludesSpecifications(email);
            var allOrders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDTO>>(allOrders);
        }

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(Guid guid)
        {
            var Spec = new OrderWithIncludesSpecifications(guid);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            if (order is null)
            {
                throw new OrderNotFoundExcption(guid);
            }
            return _mapper.Map<Order, OrderToReturnDTO>(order);
        }
    }
}
