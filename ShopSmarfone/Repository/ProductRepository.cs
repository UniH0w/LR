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
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
        {
        }
        public async Task <IEnumerable<Product>> GetAllProductsAsync(bool trackChanges) =>
          await FindAll(trackChanges).OrderBy(c => c.Price).ToListAsync();

        public async Task <Product> GetProductsAsync(Guid ProductId, bool trackChanges) 
            => await FindByCondition(c => c.Id.Equals(ProductId), trackChanges).SingleOrDefaultAsync();
        public void CreateProduct(Product product)
        {
            Create(product);
        }
        public void DeleteProduct(Product product)
        {
            Delete(product);
        }
        public async Task<PagedList<Product>> GetAllProductAsync(bool trackChanges, ProductParameters productParameters)
        {
            var product = await FindAll(trackChanges).OrderBy(e => e.NameModels).ToListAsync();
            return PagedList<Product>.ToPagedList(product, productParameters.PageNumber, productParameters.PageSize);
        }


    }
}


  
