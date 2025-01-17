﻿using Cedekap.Core.Models.Entities;
using Cedekap.Core.Results;
using Cedekap.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cedekap.API.Controllers
{
    /// <summary>
    /// Defines <see cref="Store"/> api controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService storeService;

        /// <summary>
        /// Initialize a new instance of <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="service">Store Controller service.</param>
        public StoreController(IStoreService service)
        {
            storeService = service;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Produces(typeof(IEnumerable<StoreResult>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<StoreResult> storeList = await storeService.GetAll();
            return Ok(storeList);
        }
    }
}