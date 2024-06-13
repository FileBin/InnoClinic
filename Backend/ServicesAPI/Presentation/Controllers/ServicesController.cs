using InnoClinic.Shared.Misc.Auth;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Services;

namespace ServicesAPI.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/services")]
[ExcludeFromCodeCoverage]
public class ServicesController(IServicesService servicesService, ClaimUserDescriptorFactory userDescriptorFactory) : ControllerBase {
    /// <summary>
    /// Creates new Service entry in database
    /// </summary>
    /// <param name="createRequest">Info used for create Service in database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Guid of created Service</returns>
    [HttpPost]
    [Authorize(Policy = Config.ServicesPolicy)]
    // BUG: I can't use ProducesResponseTypeAttribute because it breaks my exception handler 
    // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] ServiceCreateRequest createRequest, CancellationToken cancellationToken) {
        var id = await servicesService.CreateAsync(createRequest, cancellationToken);
        return Created(Url.Link($"{nameof(ServicesController)}.{nameof(GetById)}", new { id }), id);
    }


    /// <summary>
    /// Returns one page of all Services
    /// </summary>
    /// <param name="pageDesc">Page descriptor</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of ServiceDto</returns>
    [HttpGet]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ServiceResponse>))]
    public async Task<IActionResult> GetPage([FromQuery] PageDesc pageDesc, CancellationToken cancellationToken) {
        var user = userDescriptorFactory.CreateFrom(User);
        var servicesResponse = await servicesService.GetPageAsync(pageDesc, user, cancellationToken);
        return Ok(servicesResponse);
    }


    /// <summary>
    /// Returns Service by given id if it exists
    /// </summary>
    /// <param name="id">Guid of Service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ServiceDto if result is Ok</returns>
    [HttpGet]
    [Route("{id:guid}", Name = $"{nameof(ServicesController)}.{nameof(GetById)}")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponse))]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) {
        var user = userDescriptorFactory.CreateFrom(User);
        var serviceResponse = await servicesService.GetByIdAsync(id, user, cancellationToken);
        return Ok(serviceResponse);
    }

    /// <summary>
    /// Updates Service by given id if it exists
    /// </summary>
    /// <param name="id">Guid of Service</param>
    /// <param name="updateRequest">Update info used to update Service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok result if success</returns>
    [HttpPut]
    [HttpPatch]
    [Authorize(Policy = Config.ServicesPolicy)]
    [Route("{id:guid}", Name = $"{nameof(ServicesController)}.{nameof(Update)}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ServiceUpdateRequest updateRequest, CancellationToken cancellationToken) {
        await servicesService.UpdateAsync(id, updateRequest, cancellationToken);
        return Ok();
    }


    /// <summary>
    /// Deletes Service by given id
    /// </summary>
    /// <param name="id">Guid of Service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok if Service is deleted</returns>
    [HttpDelete]
    [Authorize(Policy = Config.ServicesPolicy)]
    [Route("{id:guid}", Name = $"{nameof(ServicesController)}.{nameof(Delete)}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) {
        await servicesService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}
