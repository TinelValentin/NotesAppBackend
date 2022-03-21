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
        static List<Note> _notes = new List<Note> { new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "First Note", Description = "First Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Second Note", Description = "Second Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Third Note", Description = "Third Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        public NotesController() { }



        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            _notes.Add(note);
            return CreatedAtRoute("getNoteById",note.Id, note);
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
    }
}

