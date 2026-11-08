using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Basket
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemsDTO> items { get; set; } = [];
    }
}
