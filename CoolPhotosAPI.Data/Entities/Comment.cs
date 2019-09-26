using System;

namespace CoolPhotosAPI.Data.Entities
{
    public class Comment: Entity
    {
        public string Text { get; set; }

        public Guid AlbumGuid { get; set; }
        public virtual Album Album { get; set; }
    }
}