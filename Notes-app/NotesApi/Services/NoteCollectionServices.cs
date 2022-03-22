using NotesApi.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public class NoteCollectionServices : INoteCollectionService
    {
        private static List<Note> _notes = new List<Note> { new Note { Id = new Guid("00000000-0000-0000-0000-000000000001"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "First Note", Description = "First Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000002"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Second Note", Description = "Second Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000003"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Third Note", Description = "Third Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000004"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000005"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        bool ICollectService<Note>.Create(Note model)
        {
            _notes.Add(model);
            return true;
        }

        bool ICollectService<Note>.Delete(Guid id)
        {
            int index = _notes.FindIndex(n => n.Id == id);
            if (index !=-1)
            {
                _notes.RemoveAt(index);
                return true;
            }
            return false;
        }

        bool INoteCollectionService.DeleteOwner(Guid ownerId)
        {
            int index = _notes.FindIndex(n => n.OwnerId == ownerId);
            if (index != -1)
            {
                _notes.RemoveAt(index);
                return true;
            }
            return false;
        }

        bool INoteCollectionService.DeleteIdAndOwner(Guid id, Guid ownerId)
        {
            int index = _notes.FindIndex(n => n.Id == id&&n.OwnerId==ownerId);
            if (index != -1)
            {
                _notes.RemoveAt(index);
                return true;
            }
            return false;
        }



        Note ICollectService<Note>.Get(Guid id)
        {
            int index = _notes.FindIndex(n => n.Id == id);
            if (index != -1)
            {
                return _notes[index];
                
            }
            return null;
        }

        List<Note> ICollectService<Note>.GetAll()
        {
            return _notes;
        }

        List<Note> INoteCollectionService.GetNotesByOwnerId(Guid ownerId)
        {
            List<Note> ownerList = new List<Note>();

            foreach(Note note in _notes)
            {
                if (note.OwnerId == ownerId)
                    ownerList.Add(note);
            }
            return ownerList;
        }

        bool ICollectService<Note>.Update(Guid id, Note model)
        {
            int index = _notes.FindIndex(n => n.Id == id);
            if (index != -1)
            {
                _notes[index] = model;
                return true; 
            }
            return false;
        }

        bool INoteCollectionService.UpdateIdAndOwner(Guid id,Guid ownerId, Note model)
        {
            int index = _notes.FindIndex(n => n.Id == id&&n.OwnerId==ownerId);
            if (index != -1)
            {
                _notes[index] = model;
                return true;
            }
            return false;
        }
    }
}
