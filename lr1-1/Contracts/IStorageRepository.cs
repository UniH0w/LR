using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStorageRepository
    {
        IEnumerable<Storage> GetAllStorage(bool trackChanges);
        Storage GetStorage(Guid storageId, bool trackChanges);
    }
}
