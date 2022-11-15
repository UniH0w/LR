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

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
           await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();
        public void CreateCompany(Company company) => Create(company);
        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
        public async Task<PagedList<Company>> GetAllCompaniesAsync(bool trackChanges, CompanyParameters parameters)
        {
            var companies = await FindAll(trackChanges).OrderBy(e => e.Name).ToListAsync();
            return PagedList<Company>.ToPagedList(companies, parameters.PageNumber, parameters.PageSize);
        }
    }
}
