using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
            new Product
            {
                Id = new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"),
                ManufacturerId = new Guid("5e37de8c-6df8-43f0-9cf9-c6963a479fc5"),
                NameModels = "Iphone x",
                Price = 20000
            },
            new Product
            {
                Id = new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                ManufacturerId = new Guid("61b5cc85-d737-4ab9-80f1-dc173809797a"),
                NameModels = "S22",
                Price = 15000
            }
   );
        }
    }

}
   