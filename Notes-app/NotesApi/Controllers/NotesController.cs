using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApi.NewFolder;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Services;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {

        INoteCollectionService _noteCollectionService;

        public NotesController(INoteCollectionService noteCollectionService)
        {
            this._noteCollectionService = noteCollectionService;

            _noteCollectionService = noteCollectionService ?? throw new ArgumentNullException(nameof(noteCollectionService));

        }



        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            if (note == null)
                return BadRequest("Note can't be null!");
            _noteCollectionService.Create(note);

            return CreatedAtRoute("getNoteById", note.Id, note);
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_noteCollectionService.GetAll());
        }

        [HttpGet("ownerId")]
        public IActionResult GetNotesByOwner(Guid owner)
        {
            if (owner == null)
                return BadRequest("id can't be null!");

            List<Note> ownerNotes = _noteCollectionService.GetNotesByOwnerId(owner);
            if (ownerNotes.Count == 0)
                return NotFound("No note has been found!");
            
            return Ok(ownerNotes);
        }

        [HttpGet("id", Name = "getNoteById")]
        public IActionResult getNotesByNoteId(Guid id)
        {
            if (id == null)
                return BadRequest("id can't be null!");
            Note note = _noteCollectionService.Get(id);
            if (note == null)
                return NotFound("There is no note with this id!");

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {

                return BadRequest("Note can't be null!");

            }
            bool update = _noteCollectionService.Update(id, note);
            if (!update)
            {
                return Post(note);
                //return NotFound("Id dosen't exist!");
            }
            
            return Ok(_noteCollectionService.GetAll());
        }

        [HttpPut("{noteId}/{ownerId}")]
        public IActionResult updateByNoteAndOwner(Guid noteId, Guid ownerId, Note newNote)

        {
            if (noteId == null || ownerId == null)
            {
                return BadRequest("One id is null!");
            }

            bool update = _noteCollectionService.UpdateIdAndOwner(noteId, ownerId, newNote);

            if (!update)
            {
                return Post(newNote);
            }

            
            return Ok(_noteCollectionService.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            if (id == null)
            {
                return BadRequest("id can't be null!");
            }
            bool delete = _noteCollectionService.Delete(id);
            if (!delete)
            {
                return NotFound("Note dosen't exist!");
            }
           

            return Ok(_noteCollectionService.GetAll());
        }

        [HttpDelete("{noteId}/{ownerId}")]
        public IActionResult DeleteNote(Guid noteId, Guid ownerId)
        {
            if (noteId == null || ownerId == null)
            {
                return BadRequest("id can't be null!");
            }
            bool delete = _noteCollectionService.DeleteIdAndOwner(noteId, ownerId);

            if (!delete)
            {
                return NotFound("Note dosen't exist!");
            }
           

            return Ok(_noteCollectionService.GetAll());
        }

        [HttpDelete("owner/{ownerId}")]
        public IActionResult deleteAllNotes(Guid ownerId)
        {
            if (ownerId == null)
            {
                return BadRequest("id can't ben null!");

            }
            bool delete = _noteCollectionService.DeleteOwner(ownerId);

            if (!delete)
            {
                return NotFound("Note doesn't exist!");
            }

            
            return Ok(_noteCollectionService.GetAll());
        }
    }
}

