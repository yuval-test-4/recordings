using Microsoft.EntityFrameworkCore;
using Recordings.APIs;
using Recordings.APIs.Common;
using Recordings.APIs.Dtos;
using Recordings.APIs.Errors;
using Recordings.APIs.Extensions;
using Recordings.Infrastructure;
using Recordings.Infrastructure.Models;

namespace Recordings.APIs;

public abstract class VideosServiceBase : IVideosService
{
    protected readonly RecordingsDbContext _context;

    public VideosServiceBase(RecordingsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one video
    /// </summary>
    public async Task<Video> CreateVideo(VideoCreateInput createDto)
    {
        var video = new VideoDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            video.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            video.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<VideoDbModel>(video.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one video
    /// </summary>
    public async Task DeleteVideo(VideoWhereUniqueInput uniqueId)
    {
        var video = await _context.Videos.FindAsync(uniqueId.Id);
        if (video == null)
        {
            throw new NotFoundException();
        }

        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many videos
    /// </summary>
    public async Task<List<Video>> Videos(VideoFindManyArgs findManyArgs)
    {
        var videos = await _context
            .Videos.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return videos.ConvertAll(video => video.ToDto());
    }

    /// <summary>
    /// Meta data about video records
    /// </summary>
    public async Task<MetadataDto> VideosMeta(VideoFindManyArgs findManyArgs)
    {
        var count = await _context.Videos.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one video
    /// </summary>
    public async Task<Video> Video(VideoWhereUniqueInput uniqueId)
    {
        var videos = await this.Videos(
            new VideoFindManyArgs { Where = new VideoWhereInput { Id = uniqueId.Id } }
        );
        var video = videos.FirstOrDefault();
        if (video == null)
        {
            throw new NotFoundException();
        }

        return video;
    }

    /// <summary>
    /// Update one video
    /// </summary>
    public async Task UpdateVideo(VideoWhereUniqueInput uniqueId, VideoUpdateInput updateDto)
    {
        var video = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            video.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(video).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Videos.Any(e => e.Id == video.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a customer record for video
    /// </summary>
    public async Task<Customer> GetCustomer(VideoWhereUniqueInput uniqueId)
    {
        var video = await _context
            .Videos.Where(video => video.Id == uniqueId.Id)
            .Include(video => video.Customer)
            .FirstOrDefaultAsync();
        if (video == null)
        {
            throw new NotFoundException();
        }
        return video.Customer.ToDto();
    }
}
