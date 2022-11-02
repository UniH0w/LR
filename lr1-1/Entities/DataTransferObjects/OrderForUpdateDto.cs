using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrderForUpdateDto
    {
        public string PurchaseName { get; set; }
        public IEnumerable<BuyerForCreationDto> Buyers { get; set; }

    }
}
