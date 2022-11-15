using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class StorageDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
      
        public string FullNameProduct { get; set; }

        public int Quantity { get; set; }
    }
}
