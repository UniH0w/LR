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
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            {
                builder.HasData
                (
                new Manufacturer
                {
                    Id = new Guid("5e37de8c-6df8-43f0-9cf9-c6963a479fc5"),
                    NameManufacturer = "Apple"
                   
                },
                new Manufacturer
                {
                    Id = new Guid("61b5cc85-d737-4ab9-80f1-dc173809797a"),
                    NameManufacturer  = "Samsung"
                    

                }
        );
            }
        }
    }
}

