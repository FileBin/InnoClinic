using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;

namespace ServicesAPI.Presentation.Controllers;


[ApiController]
[Authorize]
[Route("api/categories")]
[ExcludeFromCodeCoverage]
public class SpecializationsController(ISpecializationsService categoriesService) : ControllerBase {
    /// <summary>
    /// Creates new Specialization entry in database
    /// </summary>
    /// <param name="createRequest">Info used for create Specialization in database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Guid of created Specialization</returns>
    [HttpPost]
    [Authorize(Policy = Config.ServicesPolicy)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] SpecializationCreateRequest createRequest, CancellationToken cancellationToken) {
        var id = await categoriesService.CreateAsync(createRequest, cancellationToken);
        return Created(Url.Link(nameof(GetById), new { id }), id);
    }


    /// <summary>
    /// Returns one page of all Specializations
    /// </summary>
    /// <param name="pageDesc">Page descriptor</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of SpecializationDto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SpecializationResponse>))]
    public async Task<IActionResult> GetPage([FromQuery] PageDesc pageDesc, CancellationToken cancellationToken) {
        var specializationsResponse = await categoriesService.GetPageAsync(pageDesc, cancellationToken);
        return Ok(specializationsResponse);
    }


    /// <summary>
    /// Returns Specialization by given id if it exists
    /// </summary>
    /// <param name="id">Guid of Specialization</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>SpecializationDto if result is Ok</returns>
    [HttpGet]
    [Route("{id:guid}", Name = nameof(GetById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecializationResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) {
        var specializationResponse = await categoriesService.GetByIdAsync(id, cancellationToken);
        return Ok(specializationResponse);
    }

    /// <summary>
    /// Updates Specialization by given id if it exists
    /// </summary>
    /// <param name="id">Guid of Specialization</param>
    /// <param name="updateRequest">Update info used to update Specialization</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok result if success</returns>
    [HttpPut]
    [HttpPatch]
    [Authorize(Policy = Config.ServicesPolicy)]
    [Route("{id:guid}", Name = nameof(Update))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromBody] SpecializationUpdateRequest updateRequest, CancellationToken cancellationToken) {
        await categoriesService.UpdateAsync(id, updateRequest, cancellationToken);
        return Ok();
    }


    /// <summary>
    /// Deletes Specialization by given id
    /// </summary>
    /// <param name="id">Guid of Specialization</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok if Specialization is deleted</returns>
    [HttpDelete]
    [Authorize(Policy = Config.ServicesPolicy)]
    [Route("{id:guid}", Name = nameof(Delete))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) {
        await categoriesService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}
