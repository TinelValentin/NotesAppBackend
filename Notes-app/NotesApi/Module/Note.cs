using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.NewFolder
{
    public class Note
    {
        public string? Title { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [BsonId]
        public Guid? Id { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
