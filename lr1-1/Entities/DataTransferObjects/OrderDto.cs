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
        public Guid IdBuyer1 { get; set; }
        public Guid IdProduct { get; set; }
        
    }
}
