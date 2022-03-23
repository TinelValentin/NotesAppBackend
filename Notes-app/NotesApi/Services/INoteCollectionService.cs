using NotesApi.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public interface INoteCollectionService : ICollectService<Note>
    {
        Task<List<Note>> GetNotesByOwnerId(Guid ownerId);

        Task<bool> UpdateIdAndOwner(Guid id, Guid ownerId, Note model);

        Task<bool> DeleteIdAndOwner(Guid id, Guid ownerId);

        Task<bool> DeleteOwner(Guid ownerId);
    }
}
