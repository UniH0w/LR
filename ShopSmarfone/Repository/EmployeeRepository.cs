using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.RequestFeatures;
using Repository.Extensions;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync(Guid companyId, bool trackChanges) =>
          await  FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        .OrderBy(e => e.Name).ToListAsync();
        public async Task <Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }
        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
        public async Task<PagedList<Employee>> GetEmployeeAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employee = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                .Search(employeeParameters.Search)
                .Sort(employeeParameters.OrderBy)
                .ToListAsync();
            return PagedList<Employee>.ToPagedList(employee, employeeParameters.PageNumber, employeeParameters.PageSize);
        }
    }
}
