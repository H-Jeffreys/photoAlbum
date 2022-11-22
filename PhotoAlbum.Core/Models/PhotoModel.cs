using System;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Models
{
    public class PhotoModel : IEntityWithId
    {
        
        public int AlbumId { get; set; }
            
        public int Id { get; set; }

        public string Title { get; set; }
        
        public Uri URl { get; set; }
        
        public Uri ThumbnailUrl { get; set; }
    }
}