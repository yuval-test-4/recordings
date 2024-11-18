using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recordings.APIs;
using Recordings.APIs.Common;
using Recordings.APIs.Dtos;
using Recordings.APIs.Errors;

namespace Recordings.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one customer
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one customer
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many customers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one customer
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one customer
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple videos records to customer
    /// </summary>
    [HttpPost("{Id}/videos")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectVideos(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] VideoWhereUniqueInput[] videosId
    )
    {
        try
        {
            await _service.ConnectVideos(uniqueId, videosId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple videos records from customer
    /// </summary>
    [HttpDelete("{Id}/videos")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectVideos(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] VideoWhereUniqueInput[] videosId
    )
    {
        try
        {
            await _service.DisconnectVideos(uniqueId, videosId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple videos records for customer
    /// </summary>
    [HttpGet("{Id}/videos")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Video>>> FindVideos(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] VideoFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindVideos(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple videos records for customer
    /// </summary>
    [HttpPatch("{Id}/videos")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateVideos(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] VideoWhereUniqueInput[] videosId
    )
    {
        try
        {
            await _service.UpdateVideos(uniqueId, videosId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
