using MongoDB.Bson;
using MongoDB.Driver;
using NotesApi.NewFolder;
using NotesApi.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public class NoteCollectionServices : INoteCollectionService
    {
        private IMongoCollection<Note> _notes;



        public NoteCollectionServices(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _notes = database.GetCollection<Note>(settings.NoteCollectionName);
        }


        public async Task<Note> Get(Guid id)
        {
            var result = await _notes.FindAsync(note => note.Id == id);

            return result.FirstOrDefault();

        }
        public async Task<List<Note>> GetAll()
        {
            var result = await _notes.FindAsync(note => true);
            return result.ToList();
        }

        public async Task<bool> Create(Note note)
        {
            await _notes.InsertOneAsync(note);
            return true;
        }
        public async Task<bool> Update(Guid id, Note note)
        {
            note.Id = id;
            var result = await _notes.ReplaceOneAsync(note => note.Id == id, note);
            if (!result.IsAcknowledged && result.ModifiedCount == 0)
            {
                await _notes.InsertOneAsync(note);
                return false;
            }
            return true;
        }




            public async Task<bool> Delete(Guid id)
        {
            var result = await _notes.DeleteOneAsync(note => note.Id == id);
            if (!result.IsAcknowledged && result.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }




        public async Task<List<Note>> GetNotesByOwnerId(Guid ownerId)
        {
            var result = await _notes.FindAsync(note => note.OwnerId == ownerId);
            if (result.ToList().Count > 0)
                return result.ToList();
            else
                return null;
        }

        public async Task<bool> UpdateIdAndOwner(Guid id, Guid ownerId, Note model)
        {

            var update = await _notes.ReplaceOneAsync(n => n.OwnerId == ownerId && n.Id == id, model);
            if (!update.IsAcknowledged && update.ModifiedCount == 0)
            {
                await _notes.InsertOneAsync(model);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteIdAndOwner(Guid id, Guid ownerId)
        {
            var delete = await _notes.DeleteOneAsync(n => n.Id == id && n.OwnerId == ownerId);
            if (delete.IsAcknowledged && delete.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteOwner(Guid ownerId)
        {
            var delete = await _notes.DeleteManyAsync(n => n.OwnerId == ownerId);
            if (delete.IsAcknowledged && delete.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }




    }
}
