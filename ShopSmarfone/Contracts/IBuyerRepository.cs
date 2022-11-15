using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBuyerRepository
    {
        Task<PagedList<Buyer>> GetAllBuyerAsync(bool trackChanges,BuyerParameters buyerParameters);
        Task <Buyer> GetBuyerAsync(Guid BuyerId, bool trackChanges);
        void CreateBuyer(Buyer buyer);
        void DeleteBuyer(Buyer buyer);
    }
}
