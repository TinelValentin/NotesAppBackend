using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Services
{
    public class OwnerServices : IOwnerServcice
    {

        static List<Owner> _owners = new List<Owner>
        {
           new Owner{ Id = new Guid("00000000-0000-0000-0000-000000000001"), Name= "first" },
           new Owner{ Id = new Guid("00000000-0000-0000-0000-000000000002"), Name= "second" },
           new Owner{ Id = new Guid("00000000-0000-0000-0000-000000000003"), Name= "third" }
        };

        public bool Create(Owner model)
        {
            _owners.Add(model);
            return true;
        }

        public bool Delete(Guid id)
        {
            int index = _owners.FindIndex(n => n.Id == id);
            if (index != -1)
            {
                _owners.RemoveAt(index);
                return true;
            }
            return false;
        }

        public Owner Get(Guid id)
        {
            int index = _owners.FindIndex(n => n.Id == id);
            if (index != -1)
            {
                return _owners[index];

            }
            return null;
        }

        public List<Owner> GetAll()
        {
            return _owners;
        }

        public bool Update(Guid id, Owner model)
        {
            int index = _owners.FindIndex(n => n.Id == id);
            if (index != -1)
            {
                _owners[index] = model;
                return true;
            }
            return false;
        }
    }
}
