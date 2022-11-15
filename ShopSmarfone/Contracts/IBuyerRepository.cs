using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBuyerRepository
    {
        Task <IEnumerable<Buyer>> GetAllBuyerAsync(bool trackChanges);
        Task <Buyer> GetBuyerAsync(Guid BuyerId, bool trackChanges);
        void CreateBuyer(Buyer buyer);
        void DeleteBuyer(Buyer buyer);
    }
}
