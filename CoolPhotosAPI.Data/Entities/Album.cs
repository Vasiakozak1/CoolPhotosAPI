using System;
using System.Collections.Generic;
using System.Text;

namespace CoolPhotosAPI.Data.Entities
{
    public class Album: Entity
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid OwnerGuid { get; set; }
        public CoolAppUser Owner { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<PhotosAlbumsConnection> Photos { get; set; }
    }
}
