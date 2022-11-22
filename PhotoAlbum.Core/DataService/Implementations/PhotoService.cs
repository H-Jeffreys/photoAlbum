using System.Net.Http;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Core.Mappers;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.Core.DataService.Implementations
{
  
    public class PhotoRepository: EntityDataRepository<PhotoModel, PhotoJson>
    {
     
        public PhotoRepository(IHttpClientFactory httpClientFactory, ILogger<PhotoRepository> logger, IEntityMapper<PhotoModel, PhotoJson> mapper, PhotoServiceConfiguration serviceConfiguration) : base(httpClientFactory,  logger,"PhotoService", mapper, serviceConfiguration)
        {
        }
    }
}