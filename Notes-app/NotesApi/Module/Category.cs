using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi
{
    public class Category
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public Category(string name,string id)
        {
            Name = name;
            Id = id;
        }
    }
}
