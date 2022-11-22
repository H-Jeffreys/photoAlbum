using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using PhotoAlbum.Core.DataService;
using PhotoAlbum.Core.DataService.Implementations;
using PhotoAlbum.Core.DomainServices;
using PhotoAlbum.Core.Exceptions;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Models.Interfaces;
using Shouldly;
using Xunit;

namespace PhotoAlbum.Test
{
    public class PhotoAlbumTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IUserEntityDataService<IAlbumModel>> _albumService;
        private readonly Mock<IEntityDataRepository<PhotoModel>> _photoDataService;
        private readonly PhotoAlbumDataService _photoAlbumDataService;
        private readonly PhotoAlbumDomainService _photoAlbumDomainService;

        public PhotoAlbumTest()
        {
            _fixture = new Fixture();
            _albumService = new Mock<IUserEntityDataService<IAlbumModel>>();
            _photoDataService = new Mock<IEntityDataRepository<PhotoModel>>();
            _photoAlbumDataService = new PhotoAlbumDataService(_albumService.Object, _photoDataService.Object);

            _photoAlbumDomainService = new PhotoAlbumDomainService(_photoAlbumDataService);
        }
        
        
        [Fact]
        public async Task TestPhotoAlbumDataServiceGetAll()
        {
            
            var photos = _fixture.Build<PhotoModel>().With(s => s.AlbumId, 1).CreateMany(20).ToList();
            var album = _fixture.Build<AlbumModel>().With(s => s.Id, 1).Create();
            var photoAlbum = new PhotoAlbumModel
            {
                UserId = album.UserId, Id = album.Id, Title = album.Title,
                Photos = photos
            };


            _albumService.Setup(x => x.GetAll()).ReturnsAsync(new List<IAlbumModel>(){album});
            _photoDataService.Setup(x => x.GetAll()).ReturnsAsync(photos);

            var results = await _photoAlbumDomainService.GetAll();

            var photoAlbumModels = results.ToList();
            photoAlbumModels.ShouldNotBeNull();
            photoAlbumModels.ToList().Count.ShouldBe(1);

            var albumResult = photoAlbumModels.First();
            
            Assert.Equal(photoAlbum.Photos, albumResult.Photos);
            albumResult.Photos.ShouldBeEquivalentTo(photoAlbum.Photos);
            albumResult.Id.ShouldBe(photoAlbum.Id);
            albumResult.UserId.ShouldBe(photoAlbum.UserId);
            albumResult.Title.ShouldBe(photoAlbum.Title);
        }
        
        [Fact]
        public async Task TestPhotoAlbumDataServiceGetByUserId()
        {
            var albums = _fixture.Build<AlbumModel>().CreateMany(3).ToList();
            var photos = new List<PhotoModel>();

            foreach (var item in albums.Select((value, i) => new { i, value }))
            {
                var album = item.value;
                var index = item.i;

                album.UserId = index;

                var albumPhotos = _fixture.Build<PhotoModel>().With(s => s.AlbumId, album.Id).CreateMany(5*index).ToList();
                photos.AddRange(albumPhotos);
                _albumService.Setup(x => x.GetByUserId(index)).ReturnsAsync(new List<IAlbumModel>(){album});
            }

            _photoDataService.Setup(x => x.GetAll()).ReturnsAsync(photos);
            var results = await _photoAlbumDomainService.GetById(1);


            var photoAlbumModels = results.ToList();
            photoAlbumModels.ShouldNotBeNull();
            photoAlbumModels.ToList().Count.ShouldBe(1);
            photoAlbumModels.First().Photos.Count().ShouldBe(5);
        }
        
        [Fact]
        public async Task TestInvalidIdException()
        {
            _albumService.Setup(x => x.GetByUserId(1)).ReturnsAsync(new List<IAlbumModel>(){});
            _photoDataService.Setup(x => x.GetAll()).ReturnsAsync(new List<PhotoModel>(){});
            var photoAlbumDataService = new Mock< PhotoAlbumDataService>(_albumService.Object, _photoDataService.Object);
            photoAlbumDataService.Setup(x => x.GetByUserId(1)).ReturnsAsync(new List<IPhotoAlbumModel>(){});
            var exception = await Assert.ThrowsAsync<NonExistentEntityException<IPhotoAlbumModel>>(() => _photoAlbumDomainService.GetById(1));
            exception.Message.ShouldBe($"No {nameof(IPhotoAlbumModel)} exists for given userId 1.");
        }
        
        [Fact]
        public async Task TestNullIdException()
        {
            _albumService.Setup(x => x.GetByUserId(1)).ReturnsAsync(new List<IAlbumModel>(){});
            _photoDataService.Setup(x => x.GetAll()).ReturnsAsync(new List<PhotoModel>(){});
            var photoAlbumDataService = new Mock< PhotoAlbumDataService>(_albumService.Object, _photoDataService.Object);
            photoAlbumDataService.Setup(x => x.GetByUserId(1)).ReturnsAsync(new List<IPhotoAlbumModel>(){});
            await Assert.ThrowsAsync<NullParameterException>(() => _photoAlbumDomainService.GetById(null));
        }
    }
}