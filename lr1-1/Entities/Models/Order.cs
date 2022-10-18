using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        [Column("OrderId")]
        public Guid Id { get; set; }
        
        public Guid IdProduct1 { get; set; }
        public Guid IdBuyer1 { get; set; }
    }
}
