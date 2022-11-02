using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(Guid manufacturer, bool trackChanges);
        Product GetProducts(Guid manufacturerId, Guid Id, bool trackChanges);
        void CreateProduct(Guid ManufacturerId, Product product);
        void DeleteProduct(Product product);
    }
}
