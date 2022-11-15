using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class BuyerDto
    {
        public Guid Id { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
    }
}
