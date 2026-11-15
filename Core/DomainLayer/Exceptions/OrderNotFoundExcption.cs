using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class OrderNotFoundExcption(Guid guid):NotFoundExcption($"Product With id = {guid} is not found")
    {
    }
}
