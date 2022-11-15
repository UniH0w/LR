using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Column("ProductId")]
        public Guid Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required(ErrorMessage = "Name Models is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Position is 30 characters.")]
        public string NameModels { get; set; }

    }
}
