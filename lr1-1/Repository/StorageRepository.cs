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
        public void CreateStorage(Guid BuyerId, Storage storage)
        {
            storage.BuyerID = BuyerId;
            Create(storage);
        }
        public IEnumerable<Storage> GetAllStorage(Guid BuyerId,bool trackChanges) =>
          FindAll(trackChanges).OrderBy(c => c.Quantity).ToList();

        public Storage GetStorage(Guid BuyerId, Guid Id, bool trackChanges) 
            => FindByCondition(c => c.BuyerID.Equals(BuyerId) && c.Id.Equals(Id), trackChanges).SingleOrDefault();
    }
   
}
