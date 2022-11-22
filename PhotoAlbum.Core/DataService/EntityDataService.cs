using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Core.Exceptions;
using PhotoAlbum.Core.Mappers;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.Core.DataService
{
  
    public class EntityDataRepository<TEntity, TUEntity>: IEntityDataRepository<TEntity> where TEntity : class where TUEntity : class, new()
    {
        private protected readonly IHttpClientFactory HttpClientFactory;
        private protected readonly string ServiceName;
        private protected readonly ServiceConfiguration ServiceConfiguration;
        private protected readonly IEntityMapper<TEntity, TUEntity> Mapper;
        private readonly ILogger<EntityDataRepository<TEntity, TUEntity>> _logger;
        protected EntityDataRepository(IHttpClientFactory httpClientFactory, ILogger<EntityDataRepository<TEntity, TUEntity>> logger, string serviceName, IEntityMapper<TEntity, TUEntity> mapper, ServiceConfiguration serviceConfiguration)
        {
            HttpClientFactory = httpClientFactory;
            ServiceName = serviceName;
            _logger = logger;
            Mapper = mapper;
            ServiceConfiguration = serviceConfiguration;
        }

        private async Task<IEnumerable<TUEntity>> GetEntities()
        {
            var client = HttpClientFactory.CreateClient(ServiceName);
            var response = await client.GetAsync(ServiceConfiguration.RelativeUrl);

            if (!response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                var errorMsg =
                    $"Error when calling {ServiceName} endpoint - HTTP StatusCode - {response.StatusCode} - {stringContent}";
                _logger.LogError(HttpUtility.UrlEncode(errorMsg));
                throw new HttpDependencyException(ServiceName, response.StatusCode,
                    "Error when calling {_serviceName} endpoint");
            }


            var responseEntities = await response.Content.ReadAsAsync<IEnumerable<TUEntity>>();
            return responseEntities;
        }

        public async Task<IList<TEntity>> GetAll()
        {
            var responseEntities = await GetEntities();
            return Mapper.Map(responseEntities);
        }
    }
}