﻿using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.RequestFeatures;

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
        public async Task<PagedList<Order>> GetAllOrderAsync(Guid BuyeryId, OrderParameters orderParameters, bool trackChanges)
        {
            var order = await FindByCondition(e => e.BuyerId.Equals(BuyeryId), trackChanges).OrderBy(e => e.PurchaseName).ToListAsync();
            return PagedList<Order>.ToPagedList(order, orderParameters.PageNumber, orderParameters.PageSize);
        }

    }
}
