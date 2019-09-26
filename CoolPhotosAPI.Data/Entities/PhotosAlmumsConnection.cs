using System;

namespace CoolPhotosAPI.Data.Entities
{
    public class PhotosAlbumsConnection
    {
        public Guid PhotoGuid { get; set; }
        public Photo Photo { get; set; }
        public Guid AlbumGuid { get; set; }
        public Album Album { get; set; }
    }
}
