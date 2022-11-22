using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Core.DomainServices;
using PhotoAlbum.Core.Exceptions;
using PhotoAlbum.Core.Models.Interfaces;


namespace PhotoAlbum.Core.WebApi
{
    
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public abstract class WebApiControllerBase<TDomain> : ControllerBase
        where TDomain : class, IUserIdModel
    {
        private readonly DomainService<TDomain> _domainService;
        private readonly ILogger<WebApiControllerBase<TDomain>> _logger;

        protected WebApiControllerBase(DomainService<TDomain> domainService, ILogger<WebApiControllerBase<TDomain>> logger)
        {
            _domainService = domainService;
            _logger = logger;
        }

        private async Task<IActionResult> WrapFunctionCallWithReturn(Func<Task<IEnumerable<TDomain>>> function)
        {
            try
            {
                var results = await function.Invoke();
                return Ok(results);

            }
            catch (NonExistentEntityException<TDomain> e)
            {
                return NotFound(e.Message);

            }
            catch (NullParameterException e)
            {
                return ValidationProblem(e.Message);
            }
            catch (HttpDependencyException e)
            {
                return  new StatusCodeResult((int) e.StatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Produces("application/json")]
        [HttpGet("allPhotos")]
        public async Task<IActionResult> GetAll()
        {
            return await WrapFunctionCallWithReturn(_domainService.GetAll);
        }

        [Produces("application/json")]
        [HttpGet("userPhotos")]
        public async Task<IActionResult> Get([FromQuery] int? userId)
        {
            return await WrapFunctionCallWithReturn(() => _domainService.GetById(userId));
        }
        
    }
}