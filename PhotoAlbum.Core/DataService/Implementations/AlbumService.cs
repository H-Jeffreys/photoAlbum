using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Core.Mappers;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.DataService.Implementations
{
  
    public class AlbumRepository: EntityDataRepository<IAlbumModel, AlbumJson>, IUserEntityDataService<IAlbumModel>
    {

        public AlbumRepository(IHttpClientFactory httpClientFactory, ILogger<AlbumRepository> logger, IEntityMapper<IAlbumModel, AlbumJson> mapper, AlbumServiceConfiguration serviceConfiguration) : base(httpClientFactory, logger,"AlbumService", mapper, serviceConfiguration)
        {
        }
        
        public async Task<IList<IAlbumModel>> GetByUserId(int userId)
        {
            var client = HttpClientFactory.CreateClient(ServiceName);
            var response = await client.GetAsync(ServiceConfiguration.RelativeUrl);
            var responseEntities = await response.Content.ReadAsAsync<IEnumerable<AlbumJson>>();
            var filteredResponseEntities = responseEntities.Where(e => e.UserId == userId).ToList();
            return Mapper.Map(filteredResponseEntities);
        }
    }
}