using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Buyer
    {
        [Column("BuyerId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Family name is a required field.")]
        public string Family { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Middle name is a required field.")]
        public string MiddleName { get; set; }
       
    }
}
