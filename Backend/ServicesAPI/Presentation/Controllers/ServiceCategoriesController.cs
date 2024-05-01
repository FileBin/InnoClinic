using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;

namespace ServicesAPI.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/categories")]
[ExcludeFromCodeCoverage]
public class ServiceCategoriesController(IServiceCategoriesService categoriesService) : ControllerBase {
    /// <summary>
    /// Creates new Category entry in database
    /// </summary>
    /// <param name="createRequest">Info used for create Category in database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Guid of created Category</returns>
    [HttpPost]
    [Authorize(Policy = Config.ServicesPolicy)]
    // BUG: I can't use ProducesResponseTypeAttribute because it breaks my exception handler 
    // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] ServiceCategoryCreateRequest createRequest, CancellationToken cancellationToken) {
        var id = await categoriesService.CreateAsync(createRequest, cancellationToken);
        return Created(Url.Link($"{nameof(ServiceCategoriesController)}.{nameof(GetById)}", new { id }), id);
    }


    /// <summary>
    /// Returns one page of all Categories
    /// </summary>
    /// <param name="pageDesc">Page descriptor</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of CategoryDto</returns>
    [HttpGet]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ServiceCategoryResponse>))]
    public async Task<IActionResult> GetPage([FromQuery] PageDesc pageDesc, CancellationToken cancellationToken) {
        var categoriesResponse = await categoriesService.GetPageAsync(pageDesc, cancellationToken);
        return Ok(categoriesResponse);
    }


    /// <summary>
    /// Returns Category by given id if it exists
    /// </summary>
    /// <param name="id">Guid of Category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>CategoryDto if result is Ok</returns>
    [HttpGet]
    [Route("{id:guid}", Name = $"{nameof(ServiceCategoriesController)}.{nameof(GetById)}")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceCategoryResponse))]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) {
        var categoryResponse = await categoriesService.GetByIdAsync(id, cancellationToken);
        return Ok(categoryResponse);
    }

    /// <summary>
    /// Updates Category by given id if it exists
    /// </summary>
    /// <param name="id">Guid of Category</param>
    /// <param name="updateRequest">Update info used to update Category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok result if success</returns>
    [HttpPut]
    [HttpPatch]
    [Authorize(Policy = Config.ServicesPolicy)]
    [Route("{id:guid}", Name = $"{nameof(ServiceCategoriesController)}.{nameof(Update)}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ServiceCategoryUpdateRequest updateRequest, CancellationToken cancellationToken) {
        await categoriesService.UpdateAsync(id, updateRequest, cancellationToken);
        return Ok();
    }


    /// <summary>
    /// Deletes Category by given id
    /// </summary>
    /// <param name="id">Guid of Category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok if Category is deleted</returns>
    [HttpDelete]
    [Authorize(Policy = Config.ServicesPolicy)]
    [Route("{id:guid}", Name = $"{nameof(ServiceCategoriesController)}.{nameof(Delete)}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) {
        await categoriesService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}
