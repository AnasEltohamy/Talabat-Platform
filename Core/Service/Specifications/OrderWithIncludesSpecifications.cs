using DomainLayer.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderWithIncludesSpecifications:BaseSpecifications<Order,Guid>
    {
        public OrderWithIncludesSpecifications(string email):base(E=>E.UserEmail == email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDesc(O=>O.OrderDate);
        }
        public OrderWithIncludesSpecifications(Guid id) : base(E => E.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDesc(O => O.OrderDate);
        }

    }
}
