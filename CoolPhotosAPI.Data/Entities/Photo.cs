using System;
using System.Collections.Generic;
using System.Text;

namespace CoolPhotosAPI.Data.Entities
{
    public class Photo: Entity
    {
        public string Path { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid OwnerGuid { get; set; }
        public virtual CoolAppUser Owner { get; set; }
        public virtual ICollection<PhotosAlbumsConnection> Albums { get; set; }
    }
}
