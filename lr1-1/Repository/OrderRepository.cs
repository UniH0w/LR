using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
   : base(repositoryContext)
        {
            
        }
        public void CreateOrder(Guid ProductId, Guid BuyerId, Order order)
        {
            order.IdProduct1 = ProductId;
            order.IdBuyer1 = BuyerId;
            Create(order);
        }
        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        //public IEnumerable<Order> GetAllOrder(bool trackChanges) =>
        //  FindAll(trackChanges).OrderBy(c => c.Id).ToList();
        public IEnumerable<Order> GetAllOrder(Guid BuyerId, bool trackChanges) =>
            FindByCondition(e => e.IdBuyer1.Equals(BuyerId), trackChanges)
        .OrderBy(e => e.PurchaseName);

        public Order GetOrder( Guid BuyerId,Guid id, bool trackChanges)
            => FindByCondition(c =>  c.Id.Equals(BuyerId) && c.Id.Equals(id), trackChanges).SingleOrDefault();
    }
    
}
