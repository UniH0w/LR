using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
       Task<PagedList<Order>> GetAllOrderAsync(Guid BuyerId, OrderParameters orderParameters, bool trackChanges);
        Task <Order> GetOrderAsync(Guid BuyerId, Guid Id, bool trackChanges);
        
        void CreateOrder(Guid BuyerId, Order order);
        void DeleteOrder(Order order);
    }
}
