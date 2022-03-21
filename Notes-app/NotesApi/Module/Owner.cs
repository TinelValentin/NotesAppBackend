using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi
{
    public class Owner
    {
        [Required]public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
