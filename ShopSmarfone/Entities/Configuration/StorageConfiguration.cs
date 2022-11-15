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
    public class StorageConfiguration : IEntityTypeConfiguration<Storage> { 
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
            builder.HasData
            (
            new Storage
            {
                Id = new Guid("93362477-fbdf-4909-8a17-707e6617f306"), 
               
                ProductId = new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"),
                Quantity = 20,
                FullNameProduct = "Apple Iphone X"




            },
            new Storage
            {
                Id = new Guid("26b89402-b58f-4e0f-9095-85abea9c6a08"),
                ProductId = new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                Quantity = 45,
                FullNameProduct = "Samsung S22"
            }
    ) ;
    }
}

}


