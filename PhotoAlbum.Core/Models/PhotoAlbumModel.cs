using System.Collections.Generic;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Models
{
    public class PhotoAlbumModel: IPhotoAlbumModel
    {
        public int UserId { get; set; }
        
        public int Id { get; set; }
        
        public string Title { get; set; }

        public IEnumerable<PhotoModel> Photos { get; set; }
    }
}