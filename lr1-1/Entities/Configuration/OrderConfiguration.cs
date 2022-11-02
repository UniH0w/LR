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
    public class OrderConfiguration : IEntityTypeConfiguration <Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
                (
                new Order
                {
                    Id = new Guid("815eaeb2-2a27-45f0-b9b8-235bbe1fd08c"),
                    IdBuyer1 = new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"),
                    IdProduct1 = new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"),
                    PurchaseName = "vigodnay sdelca"
                },
                new Order
                {
                    Id = new Guid("43a0b53b-8eff-45a7-a0cd-724eaa2eefc3"),
                    IdBuyer1 = new Guid("280ddbaf-77ba-452f-8674-524d838e359a"),
                    IdProduct1 = new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                    PurchaseName = "ne vigodnay sdelca"

                }
       ) ; 
        }
    }
}
