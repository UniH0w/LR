using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {

        public ManufacturerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        { 
        }
        public IEnumerable<Manufacturer> GetAllManufacturer(bool trackChanges) =>
          FindAll(trackChanges).OrderBy(c => c.NameManufacturer).ToList();
        public Manufacturer GetManufacturer(Guid manufacturerId, bool trackChanges) => FindByCondition(c => c.Id.Equals(manufacturerId), trackChanges).SingleOrDefault();
    }
}



