using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BuyerRepository : RepositoryBase<Buyer>, IBuyerRepository
    {
        public BuyerRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
        {
        }
        public async Task <IEnumerable<Buyer>> GetAllBuyerAsync(bool trackChanges) =>
           await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
        public  async Task <Buyer> GetBuyerAsync(Guid BuyerId,bool trackChanges) =>
          await  FindByCondition(c => c.Id.Equals(BuyerId), trackChanges).SingleOrDefaultAsync();
        public void CreateBuyer(Buyer buyer)
        {
            Create(buyer);
        }
        public void DeleteBuyer(Buyer buyer)
        {
            Delete(buyer);
        }
        public async Task<PagedList<Buyer>> GetAllBuyerAsync(bool trackChanges,BuyerParameters buyerParameters)
        {
            var buyer = await FindAll(trackChanges)
                .Search(buyerParameters.Search)
                .Sort(buyerParameters.OrderBy)
                .ToListAsync();
            return PagedList<Buyer>.ToPagedList(buyer , buyerParameters.PageNumber, buyerParameters.PageSize);
        }
    }
}
