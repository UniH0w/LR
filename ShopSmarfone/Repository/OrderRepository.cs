using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
   : base(repositoryContext)
        {
        }
        public async Task <IEnumerable<Order>> GetAllOrderAsync(Guid BuyerId, bool trackChanges) =>
           await FindByCondition(e => e.BuyerId.Equals(BuyerId), trackChanges)
        .OrderBy(e => e.PurchaseName).ToListAsync();

        public async Task <Order> GetOrderAsync(Guid BuyeryId, Guid Id, bool trackChanges) =>
          await   FindByCondition
             (
                 e => e.BuyerId.Equals(BuyeryId) && e.Id.Equals(Id), trackChanges
             ).SingleOrDefaultAsync();
        public void CreateOrder(Guid BuyerId, Order order)
        {
            order.BuyerId = BuyerId;
            Create(order);
        }
        public void DeleteOrder(Order order)
        {      
            Delete(order);
        }
        
    }
}
