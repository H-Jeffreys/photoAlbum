using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.DataService
{
    public interface IUserEntityDataService<TEntity>: IEntityDataRepository<TEntity> where TEntity : class, IUserIdModel
    {
        Task<IList<TEntity>> GetByUserId(int id);
    }
}