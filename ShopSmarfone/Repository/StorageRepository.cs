﻿using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class StorageRepository : RepositoryBase<Storage>, IStorageRepository
    {
        public StorageRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
        {
        }
        public async Task <IEnumerable<Storage>> GetAllStorageAsync(Guid BuyerId, bool trackChanges) =>
          await FindAll(trackChanges).OrderBy(c => c.FullNameProduct).ToListAsync();
        public async Task <Storage> GetStorageAsync(Guid ProductId, Guid id, bool trackChanges) =>
          await  FindByCondition(e => e.ProductId.Equals(ProductId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateStorage(Guid ProductId, Storage storage)
        {
            storage.ProductId = ProductId;
            Create(storage);
        }
        public void DeleteStorage(Storage storage)
        {
            Delete(storage);
        }
    }
}