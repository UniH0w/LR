using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProductAsync(bool trackChanges, ProductParameters productParameters);
        Task <Product> GetProductsAsync(Guid ProductId, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
