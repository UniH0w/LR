using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        [Column("OrderId")]
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        [Required(ErrorMessage = "PurchaseName address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the PurchaseName is 60 characte")]
        public string PurchaseName { get; set; }
        public int Price { get; set; }
    }
}
