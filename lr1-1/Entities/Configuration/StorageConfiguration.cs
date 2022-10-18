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
                //Id = Guid.NewGuid(),
                BuyerID = new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"),
                Quantity = 20
                



            },
            new Storage
            {
                Id = new Guid("26b89402-b58f-4e0f-9095-85abea9c6a08"),
                BuyerID = new Guid("280ddbaf-77ba-452f-8674-524d838e359a"),
                Quantity = 45
            }
    ) ;
    }
}

}


