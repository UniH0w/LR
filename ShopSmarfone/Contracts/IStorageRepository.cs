using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStorageRepository
    {
        Task <PagedList<Storage>> GetAllStorageAsync(Guid ProductId, StorageParameters storageParameters, bool trackChanges);
        Task <Storage> GetStorageAsync(Guid ProductId, Guid Id, bool trackChanges);
        void CreateStorage(Guid ProductId, Storage storage);
        void DeleteStorage(Storage storage);
    }
}
