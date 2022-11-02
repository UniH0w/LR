﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBuyerRepository
    {
        IEnumerable<Buyer> GetAllBuyer(bool trackChanges);
        Buyer GetBuyer(Guid buyerId, bool trackChanges);
        void CreateBuyer(Buyer buyer);
        void DeleteBuyer(Buyer buyer);

    }
}
