using System.Collections.Generic;
using System.Linq;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.Core.Mappers
{
    public class PhotoMapper: IEntityMapper<PhotoModel, PhotoJson>
    {
        public IList<PhotoModel> Map(IEnumerable<PhotoJson> albums)
        {
            var mappedAlbums = albums.Select(album => new PhotoModel() {AlbumId = album.AlbumId, Id = album.Id, Title = album.Title, URl = album.URl, ThumbnailUrl = album.ThumbnailUrl}).ToList();

            return mappedAlbums.ToList();
        }
    }
}