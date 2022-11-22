using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoAlbum.Core.DataService
{
    public interface IEntityDataRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
    }
}