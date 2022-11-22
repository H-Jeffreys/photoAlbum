using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Core.DataService;
using PhotoAlbum.Core.DataService.Implementations;
using PhotoAlbum.Core.DomainServices;
using PhotoAlbum.Core.Mappers;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPhotoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEntityMapper<PhotoModel, PhotoJson>, PhotoMapper>();
            
            services.AddSingleton(new PhotoServiceConfiguration()
            {
                RelativeUrl = configuration["PhotoServiceSettings:RelativeUrl"]
            });
            
            services.AddHttpClient("PhotoService", client =>
            {
                client.BaseAddress = new Uri(configuration["PhotoServiceSettings:BaseUrl"]);
                
            });
            services.AddSingleton<IEntityDataRepository<PhotoModel>, PhotoRepository>();
        }


        public static void AddAlbumServices(this IServiceCollection services, IConfiguration configuration)
        {
            
           
            services.AddSingleton<IEntityMapper<IAlbumModel, AlbumJson>, AlbumMapper>();
            
            services.AddSingleton(new AlbumServiceConfiguration()
            {
                RelativeUrl = configuration["AlbumServiceSettings:RelativeUrl"]
            });
            
            services.AddHttpClient("AlbumService", client =>
            {
                client.BaseAddress = new Uri(configuration["AlbumServiceSettings:BaseUrl"]);
                
            });
            
            services.AddSingleton<IUserEntityDataService<IAlbumModel>, AlbumRepository>();
        }
        
        public static void AddPhotoAlbumServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserEntityDataService<IPhotoAlbumModel>, PhotoAlbumDataService>();
            services.AddSingleton<DomainService<IPhotoAlbumModel>, PhotoAlbumDomainService>();
        }

    }
}