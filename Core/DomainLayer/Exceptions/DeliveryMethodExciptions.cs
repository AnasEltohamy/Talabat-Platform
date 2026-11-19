using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodExciptions(int Id): NotFoundExcption($"Delivery Method Id : {Id} Not Found")
    {
    }
}
