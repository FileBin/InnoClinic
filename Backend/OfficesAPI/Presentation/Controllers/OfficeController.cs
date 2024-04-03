using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Application.Contracts.Models.Requests;
using OfficesAPI.Application.Contracts.Models.Responses;
using OfficesAPI.Application.Contracts.Services;
using Shared.Domain.Models;

namespace OfficesAPI.Presentation.Controllers;

[ApiController]
[Route("api/offices")]
[ExcludeFromCodeCoverage]
public class OfficeController(IOfficeService officeService) : ControllerBase {

    /// <summary>
    /// Creates new Office entry in database
    /// </summary>
    /// <param name="officeCreateDto">Info used for create office in database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Guid of created office</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] OfficeCreateRequest officeCreateDto, CancellationToken cancellationToken) {
        var id = await officeService.CreateAsync(officeCreateDto, cancellationToken);
        return Created(Url.Link(nameof(GetById), new { id }), id);
    }

    
    /// <summary>
    /// Returns one page of all Offices
    /// </summary>
    /// <param name="pageDesc">Page descriptor</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of OfficeDto</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfficeResponse>))]
    public async Task<IActionResult> GetPage([FromQuery] PageDesc pageDesc, CancellationToken cancellationToken) {
        var officeDtos = await officeService.GetPageAsync(pageDesc, cancellationToken);
        return Ok(officeDtos);
    }


    /// <summary>
    /// Returns Office by given id if it exists
    /// </summary>
    /// <param name="id">Guid of office</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>OfficeDto if result is Ok</returns>
    [HttpGet]
    [Route("{id:guid}", Name = nameof(GetById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfficeResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) {
        var officeDto = await officeService.GetByIdAsync(id, cancellationToken);
        return Ok(officeDto);
    }

    /// <summary>
    /// Updates Office by given id if it exists
    /// </summary>
    /// <param name="id">Guid of office</param>
    /// <param name="officeUpdateDto">Update info used to update office</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok result if success</returns>
    [HttpPut]
    [Route("{id:guid}", Name = nameof(Update))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromBody] OfficeUpdateRequest officeUpdateDto, CancellationToken cancellationToken) {
        await officeService.UpdateAsync(id, officeUpdateDto, cancellationToken);
        return Ok();
    }

    
    /// <summary>
    /// Deletes office by given id
    /// </summary>
    /// <param name="id">Guid of office</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ok if office is deleted</returns>
    [HttpDelete]
    [Route("{id:guid}", Name = nameof(Delete))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) {
        await officeService.DeleteAsync(id, cancellationToken);
        return Ok();
    }

}
