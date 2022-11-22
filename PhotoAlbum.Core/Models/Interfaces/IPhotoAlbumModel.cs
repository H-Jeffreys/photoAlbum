using System.Collections.Generic;

namespace PhotoAlbum.Core.Models.Interfaces
{
    public interface IPhotoAlbumModel: IUserIdModel
    {
        public new int Id { get; set; }
        public new int UserId { get; set; }
        public new string Title { get; set; }
        
        public IEnumerable<PhotoModel> Photos { get; set; }
        // <---- adding this solves the problem
    }
}