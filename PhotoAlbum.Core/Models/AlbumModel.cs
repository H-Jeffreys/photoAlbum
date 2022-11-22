using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Models
{
    public class AlbumModel: IAlbumModel
    {
        
        public int UserId { get; set; }
        
        public int Id { get; set; }
        
        public string Title { get; set; }
        
    }
}