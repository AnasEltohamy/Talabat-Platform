using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class ProductNotFoundExcption(int id) : NotFoundExcption($"Product With id = {id} is not found")
    {

    }
}
