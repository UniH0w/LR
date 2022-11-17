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
    public static class RepositoryStorageExtensions
    {
        public static IQueryable<Storage> Search(this IQueryable<Storage> storages, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return storages;
            var lowerCase = search.Trim().ToLower();
            return storages.Where(e => e.FullNameProduct.ToLower().Contains(lowerCase));
        }
        public static IQueryable<Storage> Sort (this IQueryable<Storage> storages, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return storages.OrderBy(e => e.FullNameProduct);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Storage>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return storages.OrderBy(e => e.FullNameProduct);
            return storages.OrderBy(orderQuery);
        }
    }
}
