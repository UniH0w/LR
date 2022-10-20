using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Order> GetAllOrder(bool trackChanges) =>
          FindAll(trackChanges).OrderBy(c => c.Id).ToList();

        public Order GetOrder(Guid orderId, bool trackChanges) => FindByCondition(c => c.Id.Equals(orderId), trackChanges).SingleOrDefault();
    }
}
