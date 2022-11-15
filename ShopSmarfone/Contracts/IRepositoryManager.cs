using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IBuyerRepository Buyer { get; }
        
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        IStorageRepository Storage { get; }
        Task SaveAsync();
    }
}
