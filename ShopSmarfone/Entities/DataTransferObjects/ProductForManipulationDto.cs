using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class ProductForManipulationDto
    {
        [Range(1000, int.MaxValue, ErrorMessage = "Price is required and it can't be lower than 1000")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Name Models is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name Models is 30 characters.")]
        public string NameModels { get; set; }
    }
}
