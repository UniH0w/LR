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
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasData
                (
                new Buyer
                {
                    Id = new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"),
                    Name = "Sergey",
                    Family ="Kosov",
                    MiddleName = "Alexandrovich"
                },
                new Buyer
                {
                    Id = new Guid("280ddbaf-77ba-452f-8674-524d838e359a"),
                    Name = "Ivan",
                    Family = "Salticov",
                    MiddleName = "Alexandrovich"
                }
                );
        }

    }
}
