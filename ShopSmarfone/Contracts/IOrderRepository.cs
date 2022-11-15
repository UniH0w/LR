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
       Task <IEnumerable<Order>> GetAllOrderAsync(Guid BuyerId, bool trackChanges);
        Task <Order> GetOrderAsync(Guid BuyerId, Guid Id, bool trackChanges);
        
        void CreateOrder(Guid BuyerId, Order order);
        void DeleteOrder(Order order);
    }
}
