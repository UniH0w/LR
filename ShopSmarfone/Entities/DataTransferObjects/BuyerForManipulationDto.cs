using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class BuyerForManipulationDto
    {
        [Required(ErrorMessage = " Family is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Family is 60 characters.")]
        public string Family { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 60 characte")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Middle name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Midddle name is 60 characte")]
        public string MiddleName { get; set; }
    }
}
