using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.DataService.Implementations
{
    public class PhotoAlbumDataService : IUserEntityDataService<IPhotoAlbumModel>
    {
        private readonly IUserEntityDataService<IAlbumModel> _albumDataRepository;
        private readonly IEntityDataRepository<PhotoModel> _photoDataRepository;

        public PhotoAlbumDataService(IUserEntityDataService<IAlbumModel> albumDataRepository, IEntityDataRepository<PhotoModel> photoDataRepository)
        {
            _albumDataRepository = albumDataRepository;
            _photoDataRepository = photoDataRepository;
        }
        
        private static async Task<List<IPhotoAlbumModel>> JoinPhotosToAlbums(Task<IList<IAlbumModel>> albums, Task<IList<PhotoModel>> photos)
        {
            var photoAlbums = new List<IPhotoAlbumModel>();

            if ((await albums).Count == 0)
            {
                return photoAlbums;
            }
            
            foreach (var album in await albums)
            {
                var photoAlbum = new PhotoAlbumModel() { UserId = album.UserId, Id = album.Id, Title = album.Title };

                photoAlbum.Photos = (await photos).Where(p => p.AlbumId == photoAlbum.Id).ToList();

                photoAlbums.Add(photoAlbum);
            }

            return photoAlbums;
        }
        
        public virtual async Task<IList<IPhotoAlbumModel>> GetByUserId(int userId)
        {
            var albums = _albumDataRepository.GetByUserId(userId);
            var photos = _photoDataRepository.GetAll();

            var photoAlbums = await JoinPhotosToAlbums(albums, photos);

            return photoAlbums;
        }
        
        public async Task<IList<IPhotoAlbumModel>> GetAll()
        {
            var albums = _albumDataRepository.GetAll();
            var photos = _photoDataRepository.GetAll();

            await Task.WhenAll(albums, photos);
            
            var photoAlbums = await JoinPhotosToAlbums(albums, photos);
            
            return photoAlbums;
        }
    }
}