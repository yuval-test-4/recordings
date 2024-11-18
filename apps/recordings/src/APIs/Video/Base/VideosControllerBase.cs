using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recordings.APIs;
using Recordings.APIs.Common;
using Recordings.APIs.Dtos;
using Recordings.APIs.Errors;

namespace Recordings.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class VideosControllerBase : ControllerBase
{
    protected readonly IVideosService _service;

    public VideosControllerBase(IVideosService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one video
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Video>> CreateVideo(VideoCreateInput input)
    {
        var video = await _service.CreateVideo(input);

        return CreatedAtAction(nameof(Video), new { id = video.Id }, video);
    }

    /// <summary>
    /// Delete one video
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteVideo([FromRoute()] VideoWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteVideo(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many videos
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Video>>> Videos([FromQuery()] VideoFindManyArgs filter)
    {
        return Ok(await _service.Videos(filter));
    }

    /// <summary>
    /// Meta data about video records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> VideosMeta([FromQuery()] VideoFindManyArgs filter)
    {
        return Ok(await _service.VideosMeta(filter));
    }

    /// <summary>
    /// Get one video
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Video>> Video([FromRoute()] VideoWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Video(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one video
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateVideo(
        [FromRoute()] VideoWhereUniqueInput uniqueId,
        [FromQuery()] VideoUpdateInput videoUpdateDto
    )
    {
        try
        {
            await _service.UpdateVideo(uniqueId, videoUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a customer record for video
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] VideoWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
