﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Settings
{
    public interface IMongoDbSettings
    {
        string OwnerCollectionName { get; set; }
        string NoteCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}