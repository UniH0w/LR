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
        public IEnumerable<Product>GetAllProducts(Guid BuyerID, bool trackChanges) =>
           FindAll(trackChanges).OrderBy(c => c.NameModels).ToList();

        public Product GetProducts(Guid manufacturerId, Guid Id, bool trackChanges) => FindByCondition(c =>c.ManufacturerId.Equals(manufacturerId) && c.Id.Equals(Id), trackChanges).SingleOrDefault();
        public void CreateProduct(Guid ManufacturerId, Product product)
        {
            product.ManufacturerId = ManufacturerId;
            Create(product);
        }
        public void DeleteProduct(Product product)
        {
            Delete(product);
        }
    }
}

