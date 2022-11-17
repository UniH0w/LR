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
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employee, uint minAge, uint maxAge) =>
            employee.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        public static IQueryable<Employee> Search(this IQueryable<Employee> employee, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employee;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employee.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Employee> Sort(this IQueryable<Employee> employee, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employee.OrderBy(e => e.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return employee.OrderBy(e => e.Name);
            return employee.OrderBy(orderQuery);
        }
    }
}




