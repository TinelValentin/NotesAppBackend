using MongoDB.Bson;
using MongoDB.Driver;
using NotesApi.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public class OwnerServices : IOwnerServcice
    {
        private IMongoCollection<Owner> _owners;

        public OwnerServices(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _owners = database.GetCollection<Owner>(settings.OwnerCollectionName);
        }

        public async Task<bool> Create(Owner model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
           await _owners.InsertOneAsync(model);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _owners.DeleteOneAsync(n => n.Id == id);
            if(!result.IsAcknowledged&&result.DeletedCount==0)
            {
                return false;
            }
            return true;
        }

       

        public async Task<Owner> Get(Guid id)
        {
            var result = await _owners.FindAsync(n => n.Id == id);
            return result.FirstOrDefault();
        }

       

        public async Task<List<Owner>> GetAll()
        {
            var result = await _owners.FindAsync(n =>true);
            return result.ToList();
        }

        public async Task<bool> Update(Guid id, Owner model)
        {
            var result = await _owners.ReplaceOneAsync(n => n.Id == id, model);
            if(!result.IsAcknowledged&&result.ModifiedCount==0)
            {
                await _owners.InsertOneAsync(model);
                return false;
            }
            return true;
        }

        
    }
}
