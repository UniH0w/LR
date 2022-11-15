using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Storage
    {
        [Column("StrorageId")]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = " FullNameProduct Models is a required field.")]
        public string FullNameProduct { get; set; }
        [Required(ErrorMessage = " Quantity Models is a required field.")]
        public int Quantity { get; set; }
    }
}
