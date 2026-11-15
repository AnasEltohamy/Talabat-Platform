using DomainLayer.Models.OrderModuls;
using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModel
{
    public class Order:BaseEntity<Guid>
    {

        //  UserEmail(String)
        //  OrderDate(DatetimeOffset)
        //  Status(Order Status)
        //  Shipping Address(OrderAddress)
        //  DeliveryMethod(DeliveryMethod)
        //  Items(ICollection<OrderItem>)
        //  SubTotal(decimal)
        //  Total
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }


        public string UserEmail { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }


        public int DeliveryMethodId { get; set; } //FK
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now!;
        public OrderStatus OrderStatus { get; set; } = default!;
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}
