using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public  static class RepositoryOrderExtensions
    {
        public static IQueryable<Order> Search(this IQueryable<Order> orders, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return orders;
            var lowerCase = search.Trim().ToLower();
            return orders.Where(e => e.PurchaseName.ToString().Trim().ToLower().Contains(lowerCase));
        }
        public static IQueryable<Order> Sort(this IQueryable<Order> orders, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return orders.OrderBy(e => e.Price);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Order>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return orders.OrderBy(e => e.Price);
            return orders.OrderBy(orderQuery);
        }
    }
}
