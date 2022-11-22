using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbum.Core.DataService;
using PhotoAlbum.Core.Exceptions;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.DomainServices
{
    public class DomainService<TDomain>
    where TDomain : class, IUserIdModel
{
    private readonly IUserEntityDataService<TDomain> _userEntityDataService;

    protected DomainService(IUserEntityDataService<TDomain> userEntityDataService)
    {
        _userEntityDataService = userEntityDataService;
    }

    public async Task<IEnumerable<TDomain>> GetAll()
    {
        return await _userEntityDataService.GetAll();
    }

    public async Task<IEnumerable<TDomain>> GetById(int? userId)
    {
        if (!userId.HasValue)
        {
            ThrowExceptionForNullInputEntity("userId");
        }
        
        
        // ReSharper disable once PossibleInvalidOperationException
        var entitiesInDataService = await _userEntityDataService.GetByUserId(userId.Value);

        if (!entitiesInDataService.Any())
        {
            ThrowExceptionForNonExistentEntity(userId);
        }
            

        return entitiesInDataService;
    }

    private static void ThrowExceptionForNullInputEntity(string paramName)
    {
        throw new NullParameterException(paramName);
    }

    private static void ThrowExceptionForNonExistentEntity(int? idValue)
    {
        throw new NonExistentEntityException<TDomain>(idValue);
    }
}
}