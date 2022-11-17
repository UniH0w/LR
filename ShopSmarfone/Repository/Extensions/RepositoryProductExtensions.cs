using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryProductExtensions
    {
        public static IQueryable<Product> FilterProduct(this IQueryable<Product> products, uint minAge, uint maxAge) =>
           products.Where(e => (e.Price >= minAge && e.Price <= maxAge));
        public static IQueryable<Product> Search(this IQueryable<Product> products, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return products;
            var lowerCase = search.Trim().ToLower();
            return products.Where(e => e.NameModels.ToString().Trim().ToLower().Contains(lowerCase));
        }
        public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.NameModels);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Order>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return products.OrderBy(e => e.NameModels);
            return products.OrderBy(orderQuery);
        }
    }
}
