using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
        {
        }
        public IEnumerable<Product>GetAllProducts(bool trackChanges) =>
           FindAll(trackChanges).OrderBy(c => c.NameModels).ToList();

        public Product GetProducts(Guid productId, bool trackChanges) => FindByCondition(c => c.Id.Equals(productId), trackChanges).SingleOrDefault();
    }
}

