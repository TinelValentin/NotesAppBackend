using NotesApi.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public interface INoteCollectionService : ICollectService<Note>
    {
        List<Note> GetNotesByOwnerId(Guid ownerId);

        bool UpdateIdAndOwner(Guid id, Guid ownerId, Note model);

        bool DeleteIdAndOwner(Guid id, Guid ownerId);

        bool DeleteOwner(Guid ownerId);
    }
}
