using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class StorageForManipulationDto
    {
        [Required(ErrorMessage = " Full Name Product is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the  Full Name Product is 30 characters.")]
        public string FullNameProduct { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity is required and it can't be lower than 1")]
        public int Quantity { get; set; }
    }
}
