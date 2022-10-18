using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Manufacturer
    {
        [Column("ManufacturerId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name Manufacturer is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string NameManufacturer { get; set; }
        
    }
}
    
    
