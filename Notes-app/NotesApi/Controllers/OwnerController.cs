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
        static List<Owner> owners = new List<Owner>();
        [HttpGet]
        public IActionResult GetOwner(Guid id)
        {
            foreach(Owner owner in owners)
            {
                if (owner.Id == id)
                    return Ok(owner);
            }
            return BadRequest(id);
        }

        [HttpPost]
        public IActionResult PostOwner([FromBody] Owner newOwner)
        {
            owners.Add(newOwner);
            return Ok(newOwner);
        }
    }
}
