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
                    BuyerId = new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"),
                    PurchaseName = "vigodnay sdelca",
                    Price = 1500
                },
                new Order
                {
                    Id = new Guid("43a0b53b-8eff-45a7-a0cd-724eaa2eefc3"),
                    BuyerId = new Guid("280ddbaf-77ba-452f-8674-524d838e359a"),
                    PurchaseName = "ne vigodnay sdelca",
                    Price = 3000

                }
       ); ; ; 
        }
    }
}
