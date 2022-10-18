﻿using Contracts;
using Entities.Models;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BuyerRepository1 : RepositoryBase<Buyer>, IBuyerRepository
    {
        public BuyerRepository1(RepositoryContext repositoryContext)
    : base(repositoryContext)
        {
        }
    }
}

