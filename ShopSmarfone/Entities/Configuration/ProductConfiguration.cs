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
                NameModels = "Iphone x",
                Price = 20000
            },
            new Product
            {
                Id = new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                NameModels = "S22",
                Price = 15000
            }
   );
        }
    }

}
   