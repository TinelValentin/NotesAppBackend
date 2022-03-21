using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        static List<Owner> owners = new List<Owner>
        {
           new Owner{ Id = new Guid("00000000-0000-0000-0000-000000000001"), Name= "first" },
           new Owner{ Id = new Guid("00000000-0000-0000-0000-000000000002"), Name= "second" },
           new Owner{ Id = new Guid("00000000-0000-0000-0000-000000000003"), Name= "third" }
        };
        [HttpGet]
        public IActionResult GetOwner(Guid id)
        {
            foreach (Owner owner in owners)
            {
                if (owner.Id == id)
                    return Ok(owner);
            }
            return BadRequest("id does not exist!");
        }

        [HttpPost]
        public IActionResult PostOwner([FromBody] Owner newOwner)
        {
            owners.Add(newOwner);
            return Ok(newOwner);
        }

        [HttpDelete("{ownerId}")]
        public IActionResult DeleteOwner(Guid ownerId)
        {
            if (ownerId == null)
            {
                return BadRequest("Id is null!");
            }

            int index = owners.FindIndex(n => n.Id == ownerId);
            if (index == -1)
            {
                return NotFound("id dosen't exist!");
            }

            owners.RemoveAt(index);
            return Ok(owners);
        }


        [HttpPut("{ownerId}")]
        public IActionResult updateOwner(Guid ownerId, Owner newOwner)
        {
            if(ownerId==null)
            {
                return BadRequest("owner id can't be null!");
            }
            if(newOwner==null)
            {
                return BadRequest("new Owner can't be null");

            }

            int index = owners.FindIndex(n => n.Id == ownerId);
            if(index==-1)
            {
                return BadRequest("Owner does not exist!");
            }

            owners[index] = newOwner;
            return Ok(owners);
        }
    }
}
