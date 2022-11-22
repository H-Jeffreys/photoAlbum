using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Core.DomainServices;
using PhotoAlbum.Core.Models.Interfaces;
using PhotoAlbum.Core.WebApi;

namespace PhotoAlbum.Controllers
{

    [ApiController]
    [Route("api/photoAlbums")]
    [ProducesResponseType(typeof(List<IPhotoAlbumModel>), (int) HttpStatusCode.OK)]
    public class PhotoAlbumController : WebApiControllerBase<IPhotoAlbumModel>
    {
        public PhotoAlbumController(DomainService<IPhotoAlbumModel> photoAlbumDomainService, ILogger<PhotoAlbumController> logger) : base(photoAlbumDomainService, logger)
        {
        }
        
        
    }
}


