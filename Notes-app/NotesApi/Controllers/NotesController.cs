using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApi.NewFolder;
using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private static List<Note> _notes = new List<Note> { new Note { Id = new Guid("00000000-0000-0000-0000-000000000001"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "First Note", Description = "First Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000002"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Second Note", Description = "Second Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000003"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Third Note", Description = "Third Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000004"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000005"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fifth Note", Description = "Fifth Note Description" }
        };


        public NotesController() { }



        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            if (note == null)
                return BadRequest("Note can't be null!");
            _notes.Add(note);
            return CreatedAtRoute("getNoteById", note.Id, note);
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }

        [HttpGet("ownerId")]
        public IActionResult GetNotesByOwner(Guid owner)
        {
            List<Note> ownerNotes = new List<Note>();

            foreach (Note note in _notes)
            {
                if (note.OwnerId == owner)
                    ownerNotes.Add(note);
            }
            return Ok(ownerNotes);
        }

        [HttpGet("id", Name = "getNoteById")]
        public IActionResult getNotesByNoteId(Guid id)
        {
            List<Note> notesById = new List<Note>();

            foreach (Note note in _notes)
            {
                if (note.Id == id)
                    notesById.Add(note);
            }
            return Ok(notesById);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {

                return BadRequest("Note can't be null!");

            }
            int index = _notes.FindIndex(n => n.Id == id);
            if (index == -1)
            {
                return Post(note);
                //return NotFound("Id dosen't exist!");
            }
            _notes[index] = note;
            _notes[index].Id = id;
            return Ok(_notes);
        }

        [HttpPut("{noteId}/{ownerId}")]
        public IActionResult updateByNoteAndOwner(Guid noteId, Guid ownerId, Note newNote)

        {
            if (noteId == null || ownerId == null)
            {
                return BadRequest("One id is null!");
            }

            int index = _notes.FindIndex(n => n.Id == noteId && n.OwnerId == ownerId);

            if (index == -1)
            {
                return Post(newNote);
            }

            _notes[index] = newNote;
            return Ok(_notes);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            if (id == null)
            {
                return BadRequest("id can't be null!");
            }
            int index = _notes.FindIndex(n => n.Id == id);
            if (index == -1)
            {
                return NotFound("Note dosen't exist!");
            }
            _notes.RemoveAt(index);

            return Ok(_notes);
        }

        [HttpDelete("{noteId}/{ownerId}")]
        public IActionResult DeleteNote(Guid noteId, Guid ownerId)
        {
            if (noteId == null || ownerId == null)
            {
                return BadRequest("id can't be null!");
            }
            int index = _notes.FindIndex(n => n.Id == noteId && n.OwnerId == ownerId);
            if (index == -1)
            {
                return NotFound("Note dosen't exist!");
            }
            _notes.RemoveAt(index);

            return Ok(_notes);
        }

        [HttpDelete("owner/{ownerId}")]
        public IActionResult deleteAllNotes(Guid ownerId)
        {
            if(ownerId==null)
            {
                return BadRequest("id can't ben null!");

            }

            int index = _notes.FindIndex(n => n.OwnerId == ownerId);

            if(index==-1)
            {
                return NotFound("Note doesn't exist!");
            }

            _notes.RemoveAt(index);
            return Ok(_notes);
        }
    }
}

