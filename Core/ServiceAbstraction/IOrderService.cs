using DomainLayer.Models.OrderModel;
using DomainLayer.Models.OrderModuls;
using Shared.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO ,string email);
        Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string email);
        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id);
    }
}
