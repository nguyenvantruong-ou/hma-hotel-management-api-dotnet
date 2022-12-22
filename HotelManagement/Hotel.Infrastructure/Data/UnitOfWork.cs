﻿using Microsoft.EntityFrameworkCore;
using NET.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Infrastructure.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        public UnitOfWork(TContext context) => _context  = context;

        public async Task CompleteAsync()
        {
            _context.SaveChanges();
        }
    }
}
 