using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public string PurchaseName { get; set; }
        public int Price { get; set; }

    }
}
