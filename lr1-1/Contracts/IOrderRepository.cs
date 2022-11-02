using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrder(Guid BuyerId,bool trackChanges);
        Order GetOrder(Guid BuyerId,Guid id, bool trackChanges);
        void CreateOrder(Guid ProductId, Guid BuyerId, Order order);
    }
}
