using Recordings.APIs.Common;
using Recordings.APIs.Dtos;

namespace Recordings.APIs;

public interface IVideosService
{
    /// <summary>
    /// Create one video
    /// </summary>
    public Task<Video> CreateVideo(VideoCreateInput video);

    /// <summary>
    /// Delete one video
    /// </summary>
    public Task DeleteVideo(VideoWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many videos
    /// </summary>
    public Task<List<Video>> Videos(VideoFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about video records
    /// </summary>
    public Task<MetadataDto> VideosMeta(VideoFindManyArgs findManyArgs);

    /// <summary>
    /// Get one video
    /// </summary>
    public Task<Video> Video(VideoWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one video
    /// </summary>
    public Task UpdateVideo(VideoWhereUniqueInput uniqueId, VideoUpdateInput updateDto);

    /// <summary>
    /// Get a customer record for video
    /// </summary>
    public Task<Customer> GetCustomer(VideoWhereUniqueInput uniqueId);
}
