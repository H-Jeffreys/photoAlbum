using System.Collections.Generic;
using System.Linq;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Mappers
{
    public class AlbumMapper: IEntityMapper<IAlbumModel, AlbumJson>
    {
        public IList<IAlbumModel> Map(IEnumerable<AlbumJson> albums)
        {
            var mappedAlbums = new List<IAlbumModel>(albums.Select(album => new AlbumModel() { Id = album.Id, UserId = album.UserId, Title = album.Title }).ToList());

            return mappedAlbums.ToList();
        }
    }
}