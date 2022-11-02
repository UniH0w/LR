using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetAllManufacturer(bool trackChanges);
        Manufacturer GetManufacturer(Guid manufacturerId, bool trackChanges);
        void CreateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Manufacturer manufacturer);
    }
}
