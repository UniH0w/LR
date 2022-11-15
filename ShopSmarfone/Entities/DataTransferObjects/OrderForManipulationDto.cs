using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class OrderForManipulationDto
    {
        [Required(ErrorMessage = "Purchase Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Purchase Name is 60 characte")]
        public string PurchaseName { get; set; }

        [Range(1000, int.MaxValue, ErrorMessage = "Price is required and it can't be lower than 1000")]
        public int Price { get; set; }
    }
}
