﻿using Cedekap.Core.Exceptions;
using Cedekap.Core.Models.Entities;
using Cedekap.Core.Requests;
using Cedekap.Core.Results;
using Cedekap.Core.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedekap.Core.Services.Implementations
{
    /// <summary>
    /// Defines <see cref="Store"/> service.
    /// </summary>
    public class StoreService : ServiceBase, IStoreService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="StoreService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public StoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        /// <inheritdoc />
        public async Task CreateOrUpdate(StoreCreateUpdateRequest request)
        {
            //Check if request has a non empty Guid
            if (request.Id == Guid.Empty)
            {
                await Create(request);
            }
            else
            {
                await Update(request);
            }
        }

        /// <inheritdoc />
        public async Task Delete(Guid id)
        {
            Store store = await unitOfWork.Store.GetById(id);
            if (store is null)
            {
                throw new EntityNotFoundException(typeof(Store), id);
            }

            unitOfWork.Store.Delete(store);
            await unitOfWork.Commit();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<StoreResult>> GetAll()
        {
            IEnumerable<Store> storeList = await unitOfWork.Store.GetAll();
            return storeList.Select(p => new StoreResult(p));
        }

        /// <inheritdoc />
        public async Task<StoreResult> GetById(Guid id)
        {
            Store store = await unitOfWork.Store.GetById(id);
            if (store is null)
            {
                throw new EntityNotFoundException(typeof(Store), id);
            }

            return new StoreResult(store);
        }

        /// <inheritdoc />
        private async Task Create(StoreCreateUpdateRequest request)
        {
            Store newStore = new Store()
            {
                Name = request.Name,
                PostCode = request.PostCode,
                Address = request.Address,
                Place = request.Place,
            };

            await unitOfWork.Store.Add(newStore);
            await unitOfWork.Commit();
        }
        /// <inheritdoc />
        private async Task Update(StoreCreateUpdateRequest request)
        {
            Store store = await unitOfWork.Store.GetById(request.Id);
            if (store is null)
            {
                throw new EntityNotFoundException(typeof(Store), request.Id);
            }

            store.Name = request.Name;
            store.PostCode = request.PostCode;
            store.Address = request.Address;
            store.Place = request.Place;

            unitOfWork.Store.Update(store);
            await unitOfWork.Commit();
        }
    }
}
