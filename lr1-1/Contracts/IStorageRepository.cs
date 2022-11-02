using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStorageRepository
    {
        IEnumerable<Storage> GetAllStorage(Guid BuyerId,bool trackChanges);
        Storage GetStorage(Guid BuyerId, Guid Id, bool trackChanges);
        void CreateStorage(Guid BuyerId, Storage storage);
        void DeleteStorage(Storage storage);
    }
}
