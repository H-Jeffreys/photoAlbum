using PhotoAlbum.Core.DataService;
using PhotoAlbum.Core.Models.Interfaces;


namespace PhotoAlbum.Core.DomainServices
{
    public class PhotoAlbumDomainService : DomainService<IPhotoAlbumModel>
    {
        public PhotoAlbumDomainService(IUserEntityDataService<IPhotoAlbumModel> userEntityDataService) : base(userEntityDataService)
        {
        }
        
    }
}