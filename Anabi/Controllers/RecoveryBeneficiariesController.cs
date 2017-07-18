﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Anabi.DataAccess.Abstractions.Repositories;
using Anabi.DataAccess.Ef.DbModels;
using Anabi.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Anabi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RecoveryBeneficiariesController : BaseController
    {
        private readonly IGenericRepository<RecoveryBeneficiaryDb> repository;
        public RecoveryBeneficiariesController(IGenericRepository<RecoveryBeneficiaryDb> repo)
        {
            repository = repo;
        }

        System.Linq.Expressions.Expression<Func<RecoveryBeneficiaryDb, RecoveryBeneficiary>> selector = c => new RecoveryBeneficiary()
        {
            Id = c.Id,
            Name = c.Name            
        };

        // GET: api/RecoveryBeneficiaries
        [HttpGet]
        public async Task<IEnumerable<RecoveryBeneficiary>> Get()
        {
            try
            {

                return await repository.GetAll().Select(selector).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // GET: api/RecoveryBeneficiaries/5
        [HttpGet("{id}")]
        public async Task<RecoveryBeneficiary> Get(int id)
        {
            try
            {

                return await repository.FindBy(p => p.Id == id).Select(selector).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/RecoveryBeneficiaries
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RecoveryBeneficiaries/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}