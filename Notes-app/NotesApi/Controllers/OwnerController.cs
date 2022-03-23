﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NotesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerServcice _ownerCollectionService;

        public OwnerController(IOwnerServcice ownerCollectionService)
        {
            this._ownerCollectionService = ownerCollectionService;
            _ownerCollectionService = ownerCollectionService ?? throw new ArgumentNullException(nameof(ownerCollectionService));
        }
        [HttpGet]
        public async Task<IActionResult> GetOwner(Guid id)
        {
            if (id == null)
                return BadRequest("id is null!");

            Owner requestedOwner = await _ownerCollectionService.Get(id);
            if (requestedOwner == null)
                return BadRequest("id does not exist!");
            return Ok(await _ownerCollectionService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> PostOwner([FromBody] Owner newOwner)
        {
            if (newOwner == null)
                return BadRequest("Id is null!");

            await _ownerCollectionService.Create(newOwner);
            return Ok(newOwner);
        }

        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> DeleteOwner(Guid ownerId)
        {
            if (ownerId == null)
            {
                return BadRequest("Id is null!");
            }

            bool delete = await  _ownerCollectionService.Delete(ownerId);
            if (!delete)
            {
                return NotFound("id dosen't exist!");
            }


            return Ok(await _ownerCollectionService.GetAll());
        }


        [HttpPut("{ownerId}")]
        public async Task<IActionResult> updateOwner(Guid ownerId, Owner newOwner)
        {
            if (ownerId == null||newOwner==null)
            {
                return BadRequest("owner id can't be null!");
            }


            var update = await _ownerCollectionService.Update(ownerId, newOwner);
            if (!update)
            {
                return BadRequest("Owner does not exist!");
            }


            return Ok(await _ownerCollectionService.GetAll());
        }
    }
}
