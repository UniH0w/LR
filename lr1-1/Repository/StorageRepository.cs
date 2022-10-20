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
    public class StorageRepository : RepositoryBase<Storage>, IStorageRepository
    {
        public StorageRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
        {
        }
        public IEnumerable<Storage> GetAllStorage(bool trackChanges) =>
          FindAll(trackChanges).OrderBy(c => c.BuyerID).ToList();

        public Storage GetStorage(Guid storageId, bool trackChanges) => FindByCondition(c => c.Id.Equals(storageId), trackChanges).SingleOrDefault();
    }
}
