using System.Collections.Generic;

namespace CoolPhotosAPI.Data.Entities
{
    public class CoolAppUser: Entity
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string SocNetworkId { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
