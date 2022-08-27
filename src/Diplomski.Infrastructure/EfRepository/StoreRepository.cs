﻿using Diplomski.Core.Models.Entities;
using Diplomski.Core.Repositories;
using Diplomski.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.EfRepository
{
    /// <summary>
    /// Defines repository for user.
    /// </summary>
    public class StoreRepository : RepositoryBase<Store, Guid>, IStoreRepository
    {
        /// <summary>
        /// Initialize new instance of the <see cref="DiplomskiDbContext"/> class.
        /// </summary>
        /// <param name="context">Diplomski planning context.</param>
        public StoreRepository(DiplomskiDbContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetAllStoreNames()
        {
            return await GetTableQueryable()
                        .AsNoTracking()
                        .Select(s => s.Name)
                        .ToListAsync();
        }

        /// <inheritdoc />
        public async override Task<Store> GetById(Guid id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
