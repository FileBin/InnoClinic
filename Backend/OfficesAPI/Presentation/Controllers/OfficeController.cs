using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Application.Contracts.Models;
using OfficesAPI.Application.Contracts.Services;
using Shared.Domain.Models;

namespace OfficesAPI.Presentation.Controllers;

[ApiController]
[Route("api/offices")]
public class OfficeController(IOfficeService officeService) : ControllerBase {

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OfficeCreateDto officeCreateDto, CancellationToken cancellationToken) {
        var id = await officeService.CreateAsync(officeCreateDto, cancellationToken);
        return Created(Url.Link(nameof(GetById), new { id }), id);
    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery] PageDesc pageDesc, CancellationToken cancellationToken) {
        var officeDtos = await officeService.GetPageAsync(pageDesc, cancellationToken);
        return Ok(officeDtos);
    }

    [HttpGet]
    [Route("{id:guid}", Name = nameof(GetById))]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) {
        var officeDto = await officeService.GetByIdAsync(id, cancellationToken);
        return Ok(officeDto);
    }


    [HttpPut]
    [Route("{id:guid}", Name = nameof(Update))]
    public async Task<IActionResult> Update(Guid id, [FromBody] OfficeUpdateDto officeUpdateDto, CancellationToken cancellationToken) {
        await officeService.UpdateAsync(id, officeUpdateDto, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("{id:guid}", Name = nameof(Delete))]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) {
        await officeService.DeleteAsync(id, cancellationToken);
        return Ok();
    }

}
