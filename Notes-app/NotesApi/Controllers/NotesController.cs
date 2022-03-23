using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApi.NewFolder;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Services;
using MongoDB.Bson;

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
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            if (note == null)
                return BadRequest("Note can't be null!");
            await _noteCollectionService.Create(note);


            return CreatedAtRoute("getNoteById", note.Id, note);
        }
        /// <summary>
        /// get all notes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            return Ok(await _noteCollectionService.GetAll());
        }

        [HttpGet("ownerId")]
        public async Task<IActionResult> GetNotesByOwner(Guid owner)
        {
            if (owner == null)
                return BadRequest("id can't be null!");

            List<Note> ownerNotes = await _noteCollectionService.GetNotesByOwnerId(owner);
            if (ownerNotes.Count == 0)
                return NotFound("No note has been found!");

            return Ok(ownerNotes);
        }

        [HttpGet("id", Name = "getNoteById")]
        public async Task<IActionResult> getNotesByNoteId(Guid id)
        {
            if (id == null)
                return BadRequest("id can't be null!");
            var note = await _noteCollectionService.Get(id);
            if (note == null)
                return NotFound("There is no note with this id!");

            return Ok(await _noteCollectionService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {

                return BadRequest("Note can't be null!");

            }
            bool update = await _noteCollectionService.Update(id, note);
            if (!update)
            {

                return Ok(await _noteCollectionService.Create(note));

            }

            return Ok(await _noteCollectionService.GetAll());
        }

        [HttpPut("{noteId}/{ownerId}")]
        public async Task<IActionResult> updateByNoteAndOwner(Guid noteId, Guid ownerId, Note newNote)

        {
            if (noteId == null || ownerId == null)
            {
                return BadRequest("One id is null!");
            }

            bool update = await _noteCollectionService.UpdateIdAndOwner(noteId, ownerId, newNote);

            if (!update)
            {
                return Ok(await _noteCollectionService.Create(newNote));
            }


            return Ok(await _noteCollectionService.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            if (id == null)
            {
                return BadRequest("id can't be null!");
            }
            var delete = await _noteCollectionService.Delete(id);
            if (!delete)
            {
                return NotFound("Note dosen't exist!");
            }


            return Ok( await _noteCollectionService.GetAll());
        }



        [HttpDelete("{noteId}/{ownerId}")]
        public async Task<IActionResult> DeleteNote(Guid noteId, Guid ownerId)
        {
            if (noteId == null || ownerId == null)
            {
                return BadRequest("id can't be null!");
            }
            bool delete = await _noteCollectionService.DeleteIdAndOwner(noteId, ownerId);

            if (!delete)
            {
                return NotFound("Note dosen't exist!");
            }


            return Ok(await _noteCollectionService.GetAll());
        }

        [HttpDelete("owner/{ownerId}")]
        public async Task<IActionResult> deleteAllNotes(Guid ownerId)
        {
            if (ownerId == null)
            {
                return BadRequest("id can't ben null!");

            }
            bool delete = await _noteCollectionService.DeleteOwner(ownerId);

            if (!delete)
            {
                return NotFound("Note doesn't exist!");
            }


            return Ok( await _noteCollectionService.GetAll());
        }
    }
}

