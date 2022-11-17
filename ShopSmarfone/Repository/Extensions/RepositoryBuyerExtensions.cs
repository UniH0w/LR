using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryBuyerExtensions
    {
        public static IQueryable<Buyer> Search (this IQueryable<Buyer> buyers, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return buyers;
            var lowerCase = search.Trim().ToLower();
            return buyers.Where(e => e.Name.ToLower().Contains(lowerCase));
        }
        public static IQueryable<Buyer> Sort (this IQueryable<Buyer> buyers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return buyers.OrderBy(e => e.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Buyer>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return buyers.OrderBy(e => e.Name);
            return buyers.OrderBy(orderQuery);
        }
    }
}
