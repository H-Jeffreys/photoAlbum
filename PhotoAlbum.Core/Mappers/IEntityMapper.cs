using System.Collections.Generic;

namespace PhotoAlbum.Core.Mappers
{
    public interface IEntityMapper<TEntity, in TUEntity> where TEntity : class where TUEntity: class, new()
    {

        IList<TEntity> Map(IEnumerable<TUEntity> albums);
    }
}