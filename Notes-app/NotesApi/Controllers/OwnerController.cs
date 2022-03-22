using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetOwner(Guid id)
        {
            if (id == null)
                return BadRequest("id is null!");
            Owner requestedOwner = _ownerCollectionService.Get(id);
            if (requestedOwner == null)
                return BadRequest("id does not exist!");
            return Ok(_ownerCollectionService.GetAll());
        }

        [HttpPost]
        public IActionResult PostOwner([FromBody] Owner newOwner)
        {
            if (newOwner == null)
                return BadRequest("Id is null!");

            _ownerCollectionService.Create(newOwner);
            return Ok(newOwner);
        }

        [HttpDelete("{ownerId}")]
        public IActionResult DeleteOwner(Guid ownerId)
        {
            if (ownerId == null)
            {
                return BadRequest("Id is null!");
            }

            bool delete = _ownerCollectionService.Delete(ownerId);
            if (!delete)
            {
                return NotFound("id dosen't exist!");
            }


            return Ok(_ownerCollectionService.GetAll());
        }


        [HttpPut("{ownerId}")]
        public IActionResult updateOwner(Guid ownerId, Owner newOwner)
        {
            if (ownerId == null)
            {
                return BadRequest("owner id can't be null!");
            }
            if (newOwner == null)
            {
                return BadRequest("new Owner can't be null");

            }

            bool update = _ownerCollectionService.Update(ownerId, newOwner);
            if (!update)
            {
                return BadRequest("Owner does not exist!");
            }


            return Ok(_ownerCollectionService.GetAll());
        }
    }
}
